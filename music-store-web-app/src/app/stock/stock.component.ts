import { Component } from '@angular/core';
import { DeleteStockCommand, Stock, StockClient } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent {
  displayedColumns: string[] = ['product', 'warehouse', 'quantity', 'actions'];

  stock: Stock[] = [];
  constructor(private client: StockClient, private router: Router, public dialog: MatDialog) {}

  ngOnInit(){
    this.client.getAll().subscribe(result => {
      this.stock = result;
    });
  }

  onUpdate(stock: Stock) {
    this.router.navigate([`stock/edit/${stock.id}`]);
  }
}
