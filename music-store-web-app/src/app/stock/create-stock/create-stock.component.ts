import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateStockCommand, StockClient, Warehouse, WarehouseClient } from 'src/app/api/api-reference';

@Component({
  selector: 'app-create-stock',
  templateUrl: './create-stock.component.html',
  styleUrls: ['./create-stock.component.css']
})
export class CreateStockComponent {
  constructor(private route: ActivatedRoute,
    private stockClient: StockClient,
    private warehouseClient: WarehouseClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  productId: string | undefined;

  warehouses: Warehouse[] = [];

  formGroup = new FormGroup({
    warehouse: new FormControl('', { nonNullable: true }),
    quantity: new FormControl(0, { nonNullable: true }),
  });

  ngOnInit() {
    this.warehouseClient.getAll().subscribe(result => {
      if (result !== null) {
        this.warehouses = result;
      }
      else {
        this.snackBar.open("Something went wrong");
        console.error("Null response from server");
      }
    });

    this.productId = this.route.snapshot.paramMap.get('id') ?? undefined;
  }
  onSubmit() {
    this.stockClient.create(new CreateStockCommand({
      productId: this.productId,
      warehouseId: this.warehouses.find(x => x.name === this.formGroup.controls.warehouse.value)?.id,
      quantity: this.formGroup.controls.quantity.value
    })).subscribe(_ => {
      this.snackBar.open('Stock created');
      this.router.navigate([`/stock/`]);
    });
  }

}
