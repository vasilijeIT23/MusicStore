import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Category, CreateProductTypeCommand, ProductType, ProductTypeClient, UpdateProductTypeCommand } from 'src/app/api/api-reference';

@Component({
  selector: 'app-edit-product-type',
  templateUrl: './edit-product-type.component.html',
  styleUrls: ['./edit-product-type.component.css']
})
export class EditProductTypeComponent {

  category = new Map<Category, string>([
    [Category.Instrument, 'Instrument'],
    [Category.Equipmnet, 'Equipmnet'],
  ]);

  constructor(private route: ActivatedRoute,
    private client: ProductTypeClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  id: string | undefined;

  formGroup = new FormGroup({
    id: new FormControl('', { nonNullable: true }),
    name: new FormControl('', { nonNullable: true }),
    category: new FormControl<Category>(Category.Instrument, { nonNullable: true }),
  });

  ngOnInit(){
    this.id = this.route.snapshot.paramMap.get('id') ?? undefined;
    if (this.id) {
      this.client.getById(this.id).subscribe(data => this.patchForm(data));
    }
  }
  onSubmit(){
    if(this.id){
      this.client.update(new UpdateProductTypeCommand({
          id: this.formGroup.controls.id.value,
          name: this.formGroup.controls.name.value,
      })).subscribe(_ => {
          this.snackBar.open('Product type updated');
          this.router.navigate([`/productTypes/`]);
      });
    }
  else{
      this.client.create(new CreateProductTypeCommand({
        name: this.formGroup.controls.name.value,
        category: this.formGroup.controls.category.value
    })).subscribe(_ => {
        this.snackBar.open('Product type created');
        this.router.navigate([`/productTypes/`]);
    });
  }
}
  
  private readonly patchForm = (productType: ProductType) => {
    this.formGroup.controls.id.patchValue(this.id!);
    this.formGroup.controls.name.patchValue(productType.name!);
    this.formGroup.controls.category.patchValue(productType.category!);
  }
}
