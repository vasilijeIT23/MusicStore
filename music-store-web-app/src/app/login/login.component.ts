import { Component } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { CustomerClient, AuthenticateCustomerCommand, GenerateJwtTokenCommand  } from '../api/api-reference';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  isAuthenticated = false;

  constructor(private route: ActivatedRoute,
    private client: CustomerClient,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog) { }

  formGroup = new FormGroup({
    username: new FormControl('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
  });

  ngOnInit(){
  }

  login() {
    this.client.authenticate(new AuthenticateCustomerCommand({
      username: this.formGroup.controls.username.value,
      password: this.formGroup.controls.password.value,
    })).subscribe();
    this.client.generateJwtToken(new GenerateJwtTokenCommand({
      username: this.formGroup.controls.username.value
    })).subscribe(_ => {
      this.router.navigate([`/products/`]);
    });
  }
}
