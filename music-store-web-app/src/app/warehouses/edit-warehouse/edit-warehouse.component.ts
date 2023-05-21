import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateWarehouseCommand, UpdateWarehouseCommand, Warehouse, WarehouseClient } from 'src/app/api/api-reference';

@Component({
  selector: 'app-edit-warehouse',
  templateUrl: './edit-warehouse.component.html',
  styleUrls: ['./edit-warehouse.component.css']
})
export class EditWarehouseComponent {
  constructor(private route: ActivatedRoute,
    private client: WarehouseClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  id: string | undefined;

  formGroup = new FormGroup({
    id: new FormControl('', { nonNullable: true }),
    name: new FormControl('', { nonNullable: true }),
    capacity: new FormControl(0, { nonNullable: true }),
  });

  ngOnInit(){
    this.id = this.route.snapshot.paramMap.get('id') ?? undefined;
    if (this.id) {
      this.client.getById(this.id).subscribe(data => this.patchForm(data));
    }
  }
  onSubmit(){
    if(this.id){
      this.client.update(new UpdateWarehouseCommand({
          id: this.formGroup.controls.id.value,
          name: this.formGroup.controls.name.value,
          capacity: this.formGroup.controls.capacity.value
      })).subscribe(_ => {
          this.snackBar.open('Warehouse updated');
          this.router.navigate([`/warehouses/`]);
      });
    }
  else{
      this.client.create(new CreateWarehouseCommand({
        name: this.formGroup.controls.name.value,
        capacity: this.formGroup.controls.capacity.value
    })).subscribe(_ => {
        this.snackBar.open('Warehouse created');
        this.router.navigate([`/warehouses/`]);
    });
  }
}
  
  private readonly patchForm = (warehouse: Warehouse) => {
    this.formGroup.controls.id.patchValue(this.id!);
    this.formGroup.controls.name.patchValue(warehouse.name!);
    this.formGroup.controls.capacity.patchValue(warehouse.capacity!);
  }
}
