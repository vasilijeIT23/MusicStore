import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
  constructor(private route: ActivatedRoute,
    private productClient: ProductClient,
    private productTypeClient: ProductTypeClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  id: string | undefined;
  productTypes: ProductType[] = [];
  isCreate: boolean = true;

  formGroup = new FormGroup({
    id: new FormControl('', { nonNullable: true }),
    name: new FormControl('', { nonNullable: true }),
    inStock: new FormControl(false, { nonNullable: true }),
    productType: new FormControl('', { nonNullable: true }),
    price: new FormControl(0, { nonNullable: true }),
  });

  ngOnInit(){
    this.productTypeClient.getAll().subscribe(result => {
      this.productTypes = result;
    })

    this.id = this.route.snapshot.paramMap.get('id') ?? undefined;
    if (this.id) {
      this.productClient.getById(this.id).subscribe(data => this.patchForm(data));
      this.isCreate = false;
    }
  }
  onSubmit(){
    if(this.id){
      this.productClient.update(new UpdateProductCommand({
          id: this.formGroup.controls.id.value,
          name: this.formGroup.controls.name.value,
          inStock: this.formGroup.controls.inStock.value,
          price: this.formGroup.controls.price.value
      })).subscribe(_ => {
          this.snackBar.open('Product updated');
          this.router.navigate([`/products/`]);
      });
    }
  else{
      this.productClient.create(new CreateProductCommand({
        name: this.formGroup.controls.name.value,
        productType: this.productTypes.find(x => x.name === this.formGroup.controls.productType.value)?.id,
        price: this.formGroup.controls.price.value
    })).subscribe(_ => {
        this.snackBar.open('Product created');
        this.router.navigate([`/products/`]);
    });
  }
}
  
  private readonly patchForm = (product: Product) => {
    this.formGroup.controls.id.patchValue(this.id!);
    this.formGroup.controls.name.patchValue(product.name!);
    this.formGroup.controls.inStock.patchValue(product.inStock!);
    this.formGroup.controls.price.patchValue(product.price!);
  }
}
