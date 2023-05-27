import { Component } from '@angular/core';
import { Stock, StockClient } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent {
  displayedColumns: string[] = ['product', 'warehouse', 'quantity', 'actions'];

  stock: Stock[] = [];
  constructor(private client: StockClient,
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.client.getAll().subscribe(result => {
      if (result !== null) {
        this.stock = result;
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
  }

  onUpdate(stock: Stock) {
    this.router.navigate([`stock/edit/${stock.id}`]);
  }
}
