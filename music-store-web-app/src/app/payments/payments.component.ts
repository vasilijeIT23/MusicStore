import { Component } from '@angular/core';
import { Payment, PaymentClient } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-payments',
  templateUrl: './payments.component.html',
  styleUrls: ['./payments.component.css']
})
export class PaymentsComponent {
  displayedColumns: string[] = ['customerId', 'paymentId', 'amount'];

  payments: Payment[] = [];
  constructor(private client: PaymentClient,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.client.getAll().subscribe(result => {
      if (result !== null) {
        console.log(result);
        this.payments = result;
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
  }
}
