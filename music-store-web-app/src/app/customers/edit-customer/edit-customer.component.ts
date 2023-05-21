import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer, CustomerClient, Role, Status, UpdateCustomerCommand } from 'src/app/api/api-reference';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent {
  role = new Map<Role, string>([
    [Role.Admin, 'Admin'],
    [Role.Regular, 'Regular']
  ]);

  status = new Map<Status, string>([
    [Status.Advanced, 'Advanced'],
    [Status.Regular, 'Regular']
  ])  

  constructor(private route: ActivatedRoute,
    private client: CustomerClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  isDisabled: boolean = true;    
  id: string = '';

  formGroup = new FormGroup({
    id: new FormControl('', { nonNullable: true }),
    firstName: new FormControl('', { nonNullable: true }),
    lastName: new FormControl('', { nonNullable: true }),
    email: new FormControl('',{ nonNullable: true , validators: [Validators.required, Validators.email]}),
    role: new FormControl<Role>(Role.Regular, { nonNullable: true }),
  });

  ngOnInit(){
    //const dialogRef = this.dialog.open(EditCustomerComponent, {restoreFocus: false});
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.client.getById(this.id).subscribe(data => {
      this.patchForm(data);
    });
  }
  onSubmit(){
    this.client.update(new UpdateCustomerCommand({
        id: this.formGroup.controls.id.value,
        firstName: this.formGroup.controls.firstName.value,
        lastName: this.formGroup.controls.lastName.value,
        email: this.formGroup.controls.email.value,
        role: this.formGroup.controls.role.value
    })).subscribe(_ => {
        this.snackBar.open('Customer updated');
        this.router.navigate([`/customers/`]);
    });
  }

  get email(){
    return this.formGroup.controls.email;
  }

  disabled(){
    console.log();
    if(this.route.snapshot.url[0].path === 'profile'){
      this.isDisabled = false;
      return this.isDisabled;
    }
    return this.isDisabled;
  }
  
  private readonly patchForm = (customer: Customer) => {
    this.formGroup.controls.id.patchValue(this.id);
    this.formGroup.controls.firstName.patchValue(customer.firstName!);
    this.formGroup.controls.lastName.patchValue(customer.lastName!);
    this.formGroup.controls.email.patchValue(customer.email!);
    this.formGroup.controls.role.patchValue(customer.role!);
  }
}
