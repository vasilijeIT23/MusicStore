import { Component } from '@angular/core';
import { DeleteWarehouseCommand, Warehouse, WarehouseClient } from '../api/api-reference';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-warehouses',
  templateUrl: './warehouses.component.html',
  styleUrls: ['./warehouses.component.css']
})
export class WarehousesComponent {
  displayedColumns: string[] = ['name', 'capacity', 'actions'];

  warehouse: Warehouse[] = [];
  constructor(private client: WarehouseClient,
    private router: Router,
    public dialog: MatDialog,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.client.getAll().subscribe(result => {
      if (result !== null) {
        this.warehouse = result;
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });
  }

  onDelete(query: DeleteWarehouseCommand) {
    this.client.delete(query).subscribe(_ => {
      this.warehouse = this.warehouse.filter(x => x.id !== query.id)
    })
  }

  onUpdate(warehouse: Warehouse) {
    this.router.navigate([`warehouses/edit/${warehouse.id}`]);
  }

  onCreate() {
    this.router.navigate([`warehouses/create`]);
  }
}
