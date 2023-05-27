import { Component } from '@angular/core';
import { DeleteProductTypeCommand, ProductType, ProductTypeClient } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-types',
  templateUrl: './product-types.component.html',
  styleUrls: ['./product-types.component.css']
})
export class ProductTypesComponent {
  displayedColumns: string[] = ['name', 'category', 'actions'];

  productTypes: ProductType[] = [];
  constructor(private client: ProductTypeClient,
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.client.getAll().subscribe(result => {
      if (result !== null) {
        this.productTypes = result;
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
  }

  onDelete(query: DeleteProductTypeCommand) {
    this.client.delete(query).subscribe(_ => {
      this.productTypes = this.productTypes.filter(x => x.id !== query.id)
    })
  }

  onUpdate(productType: ProductType) {
    this.router.navigate([`productTypes/edit/${productType.id}`]);
  }

  onCreate() {
    this.router.navigate([`productTypes/create`]);
  }
}
