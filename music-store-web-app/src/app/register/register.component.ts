import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateCustomerCommand, CustomerClient } from '../api/api-reference';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private route: ActivatedRoute,
    private client: CustomerClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  formGroup = new FormGroup({
    firstName: new FormControl('', { nonNullable: true }),
    lastName: new FormControl('', { nonNullable: true }),
    email: new FormControl('',{ nonNullable: true , validators: [Validators.required, Validators.email]}),
    username: new FormControl('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
  });

  ngOnInit(){
  }

  register() {
    this.client.create(new CreateCustomerCommand({
      firstName: this.formGroup.controls.firstName.value,
      lastName: this.formGroup.controls.lastName.value,
      email: this.formGroup.controls.email.value,
      username: this.formGroup.controls.username.value,
      password: this.formGroup.controls.password.value,
    })).subscribe(_ => {
      this.router.navigate([`/customers/`]);
    });
  }
}

