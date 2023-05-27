import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ChangeCredentialsCommand, Customer, CustomerClient } from 'src/app/api/api-reference';

@Component({
  selector: 'app-update-credentials',
  templateUrl: './update-credentials.component.html',
  styleUrls: ['./update-credentials.component.css']
})
export class UpdateCredentialsComponent {
  constructor(private route: ActivatedRoute,
    private client: CustomerClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  customer: Customer | undefined;
  customerId: string | undefined;

  formGroup = new FormGroup({
    username: new FormControl('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
    confirmPassword: new FormControl('', { nonNullable: true }),
  });

  ngOnInit() {
    this.customerId = this.route.snapshot.paramMap.get('id') ?? undefined;
    this.client.getById(this.customerId!).subscribe(result => {
      if(result !== null){
        this.customer = result;
      }
      else{
        this.snackBar.open('Something went wrong')
        console.error('Customer not found');
      }
      
    })
  }

  onChangeCredentials() {
    if (this.formGroup.controls.password.value === this.formGroup.controls.confirmPassword.value) {
      this.client.changeCredentials(new ChangeCredentialsCommand({
        newPassword: this.formGroup.controls.password.value,
        username: this.formGroup.controls.username.value,
        oldPassword: this.customer?.password,
        email: this.customer?.email
      })).subscribe(_ => {
        this.snackBar.open("Credentials changed successfully");
        this.router.navigate(['/login']);
      });
    }
    else{
      this.snackBar.open("New password must match");
      console.error();
    }
  }
}
