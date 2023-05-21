import { Component } from '@angular/core';
import { DeleteProductTypeCommand, ProductType, ProductTypeClient } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-product-types',
  templateUrl: './product-types.component.html',
  styleUrls: ['./product-types.component.css']
})
export class ProductTypesComponent {
  displayedColumns: string[] = ['name', 'category', 'actions'];

  productTypes: ProductType[] = [];
  constructor(private client: ProductTypeClient, private router: Router, public dialog: MatDialog) {}

  ngOnInit(){
    this.client.getAll().subscribe(result => {
      this.productTypes = result;
    });
  }

  onDelete(query: DeleteProductTypeCommand){
    this.client.delete(query).subscribe(_ => {
      this.productTypes = this.productTypes.filter(x => x.id !== query.id)
    })
  }

  onUpdate(productType: ProductType){
    this.router.navigate([`productTypes/edit/${productType.id}`]);
  }

  onCreate() {
    this.router.navigate([`productTypes/create`]);
  }
}
