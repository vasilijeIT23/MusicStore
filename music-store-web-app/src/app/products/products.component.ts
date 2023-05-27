import { Component, OnInit } from '@angular/core';
import { DeleteProductCommand, Product, ProductClient } from '../api/api-reference';
import { Router } from '@angular/router';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  displayedColumns: string[] = ['name', 'inStock', 'price', 'actions'];
  id = localStorage.getItem('id');
  role = localStorage.getItem('role');

  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 20];

  currentPage: number = 0;
  totalItems: number = 0;

  filterText: string = '';
  sortBy: string = 'name';
  sortDirection: 'asc' | 'desc' = 'asc';


  products: Product[] = [];

  constructor(private productClient: ProductClient,
    private router: Router,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.router.navigate([this.router.url])
    this.productClient.getAll().subscribe(result => {
      if (result !== null) {
        this.products = result;
        this.totalItems = this.products.length;
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
  }

  onDelete(query: DeleteProductCommand) {
    this.productClient.delete(query).subscribe(_ => {
      this.products = this.products.filter(x => x.id !== query.id);
      this.totalItems = this.products.length;
      this.updatePaginator();
    });
  }

  onUpdate(product: Product) {
    this.router.navigate([`products/edit/${product.id}`]);
  }

  onCreate() {
    this.router.navigate([`products/create`]);
  }

  onPageChange(event: PageEvent) {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
  }

  updatePaginator() {
    const totalPages = Math.ceil(this.totalItems / this.pageSize);
    if (this.currentPage >= totalPages) {
      this.currentPage = Math.max(totalPages - 1, 0);
    }
  }

  getDisplayedProducts(): Product[] {
    const startIndex = this.currentPage * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.products.slice(startIndex, endIndex);
  }

  onAddToCart(productId: string) {
    this.router.navigate([`add/to/cart/${productId}`]);
  }

  public createImgPath = (serverPath: string) => {
    return `http://localhost:52720/${serverPath}`;
  }
}
