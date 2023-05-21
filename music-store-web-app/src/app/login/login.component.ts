import { Component } from '@angular/core';
import { ActivatedRoute, Router} from '@angular/router';
import { CustomerClient, AuthenticateCustomerCommand, GenerateJwtTokenCommand  } from '../api/api-reference';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup } from '@angular/forms';
import jwt_decode from 'jwt-decode'
import jwtDecode from 'jwt-decode';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  isAuthenticated = false;
  decoded: any | undefined;
  role: string | undefined;
  id: string | undefined;

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
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('id');
  }

  login() {
    this.client.authenticate(new AuthenticateCustomerCommand({
      username: this.formGroup.controls.username.value,
      password: this.formGroup.controls.password.value,
    })).subscribe(response => {
      this.client.generateJwtToken(new GenerateJwtTokenCommand({
        username: this.formGroup.controls.username.value
      })).subscribe(response => {
        localStorage.setItem('token', response!)

        this.decoded = jwt_decode(localStorage.getItem('token')!);
        this.role = this.decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        this.id = this.decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

        localStorage.setItem('role', this.role!)
        localStorage.setItem('id', this.id!);
        
        this.router.navigate([`/products/`]);
      });
    }, error => {
      this.isAuthenticated = false;
    });
  }
}


