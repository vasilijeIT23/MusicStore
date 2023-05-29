import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Stock, StockClient, UpdateStockCommand } from 'src/app/api/api-reference';

@Component({
  selector: 'app-edit-stock',
  templateUrl: './edit-stock.component.html',
  styleUrls: ['./edit-stock.component.css']
})
export class EditStockComponent {
  constructor(private route: ActivatedRoute,
    private client: StockClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  id: string | undefined;

  formGroup = new FormGroup({
    id: new FormControl('', { nonNullable: true }),
    quantity: new FormControl(0, { nonNullable: true }),
  });

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id') ?? undefined;
    if (this.id) {
      this.client.getById(this.id).subscribe(data => {
        if (data !== null) {
          this.patchForm(data)
        }
        else {
          this.snackBar.open("Something went wrong");
          console.error("Null response from server");
        }
      });
    }
  }
  onSubmit() {
    if (this.formGroup.controls.quantity.value !== null || 
      typeof(this.formGroup.controls.quantity.value) !== "number") {
      this.client.update(new UpdateStockCommand({
        id: this.formGroup.controls.id.value,
        quantity: this.formGroup.controls.quantity.value
      })).subscribe(result => {
        if (result !== null) {
          this.snackBar.open('Stock updated');
          this.router.navigate([`/stock/`]);
        }
        else {
          this.snackBar.open("Something went wrong");
          console.error("Null response from server");
        }
      });
    }
    else {
      this.snackBar.open("Please insert valid data");
      console.error("Invalid input data");
    }
  }

  private readonly patchForm = (stock: Stock) => {
    this.formGroup.controls.id.patchValue(this.id!);
    this.formGroup.controls.quantity.patchValue(stock.quantity!);
  }
}
