import { Component, OnInit } from '@angular/core';
import { Product, ProductClient } from '../api/api-reference';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  displayedColumns: string[] = ['name', 'inStock', 'price'];

  products: Product[] = [];
  
  constructor(private client: ProductClient, private router: Router) {}

  ngOnInit(){
    this.client.getAll().subscribe(result => {
      this.products = result;
    });
  }
}
