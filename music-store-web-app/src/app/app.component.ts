import { Component } from '@angular/core';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'music-store-web-app';

  customerId = localStorage.getItem('id');
  //token: string = '';

  token = localStorage.getItem('token');
  role = localStorage.getItem('role');

  constructor(private router: Router)
  {
  }

  redirect(route: string){
    this.router.navigate([`/` + route + `/`]);
  }

  onCart(){
    this.router.navigate([`cart/${this.customerId}`]);
  }

}
