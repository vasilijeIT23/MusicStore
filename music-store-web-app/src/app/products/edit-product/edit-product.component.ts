import { HttpClient, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateProductCommand, Product, ProductClient, ProductType, ProductTypeClient, UpdateProductCommand } from 'src/app/api/api-reference';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})

export class EditProductComponent {
  progress: number = 0;
  message: string | undefined;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private route: ActivatedRoute,
    private productClient: ProductClient,
    private productTypeClient: ProductTypeClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog,
    private http: HttpClient) { }

  id: string | undefined;
  productTypes: ProductType[] = [];
  isCreate: boolean = true;

  pathToImage: string | undefined;

  formGroup = new FormGroup({
    id: new FormControl('', { nonNullable: true }),
    name: new FormControl('', { nonNullable: true }),
    productType: new FormControl('', { nonNullable: true }),
    price: new FormControl(0, { nonNullable: true }),
  });

  ngOnInit() {
    this.productTypeClient.getAll().subscribe(result => {
      this.productTypes = result;
    })

    this.id = this.route.snapshot.paramMap.get('id') ?? undefined;
    if (this.id) {
      this.productClient.getById(this.id).subscribe(data => {
        if (data !== null) {
          this.patchForm(data)
        }
        else {
          this.snackBar.open("Something went wrong");
          console.error("Null response from server");
        }
      });
      this.isCreate = false;
    }
  }
  onSubmit() {
    if (this.id) {
      this.productClient.update(new UpdateProductCommand({
        id: this.formGroup.controls.id.value,
        name: this.formGroup.controls.name.value,
        price: this.formGroup.controls.price.value,
        imagePath: this.pathToImage
      })).subscribe(_ => {
        this.snackBar.open('Product updated');
        this.router.navigate([`/products/`]);
      });
    }
    else {
      this.productClient.create(new CreateProductCommand({
        name: this.formGroup.controls.name.value,
        productType: this.productTypes.find(x => x.name === this.formGroup.controls.productType.value)?.id,
        price: this.formGroup.controls.price.value,
        imagePath: this.pathToImage
      })).subscribe(result => {
        this.snackBar.open('Product created');

        this.router.navigate([`stock/create/${result.id}`]);
      });
    }
  }

  private readonly patchForm = (product: Product) => {
    this.formGroup.controls.id.patchValue(this.id!);
    this.formGroup.controls.name.patchValue(product.name!);
    this.formGroup.controls.price.patchValue(product.price!);
  }

  uploadFile = (files: string | any) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post<any>('http://localhost:52720/api/upload', formData, { reportProgress: true, observe: 'events' })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress)
            this.progress = Math.round(100 * event.loaded / event.total!);
          else if (event.type === HttpEventType.Response) {
            this.message = 'Upload success.';
            this.pathToImage = event.body.dbPath;
            this.onUploadFinished.emit(event.body);
          }
        },
        error: (err: HttpErrorResponse) => console.log(err)
      });
  }
}
