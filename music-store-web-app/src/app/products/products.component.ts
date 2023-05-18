import { Component, OnInit } from '@angular/core';
import { DeleteProductCommand, Product, ProductClient } from '../api/api-reference';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  displayedColumns: string[] = ['name', 'inStock', 'price', 'actions'];

  products: Product[] = [];
  
  constructor(private client: ProductClient, private router: Router) {}

  ngOnInit(){
    this.client.getAll().subscribe(result => {
      this.products = result;
    });
  }

  onDelete(query: DeleteProductCommand){
    this.client.delete(query).subscribe(_ => {
      this.products = this.products.filter(x => x.id !== query.id)
    })
  }

  onUpdate(product: Product) {
    this.router.navigate([`products/edit/${product.id}`]);
  }
}
