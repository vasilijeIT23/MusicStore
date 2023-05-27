import { Component, OnInit, ViewChild } from '@angular/core';
import { Customer, CustomerClient, DeleteCustomerCommand } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  displayedColumns: string[] = ['firstName', 'lastName', 'email', 'role', 'status', 'statusExpirationDate', 'moneySpent', 'actions'];

  @ViewChild(MatPaginator) paginator = null;
  @ViewChild(MatSort) sort: MatSort = new MatSort;

  dataSource: MatTableDataSource<Customer> = new MatTableDataSource;

  customers: Customer[] = [];
  loggedCustomer: Customer[] = [];

  id: string = localStorage.getItem('id')!;
  role: string = localStorage.getItem('role')!;

  constructor(private client: CustomerClient,
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.client.getAll().subscribe(result => {
      if (result !== null) {
        this.customers = result;
        this.dataSource = new MatTableDataSource(this.customers);
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
    this.client.getById(this.id).subscribe(result => {
      if (result !== null) {
        this.loggedCustomer.push(result);
        console.log(this.loggedCustomer);
        console.log(this.customers);
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onDelete(query: DeleteCustomerCommand) {
    this.client.delete(query).subscribe(_ => {
      this.customers = this.customers.filter(x => x.id !== query.id)
    })
  }

  onUpdate(customer: Customer) {
    this.router.navigate([`customers/edit/${customer.id}`]);
  }

  onCart(customerId: string) {
    this.router.navigate([`cart/${customerId}`]);
  }

  onOrder(customerId: string) {
    this.router.navigate([`orders/${customerId}`]);
  }
}
