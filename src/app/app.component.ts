import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'music-store-web-app';

  links = ['customers', 'products', 'cart', 'store'];
  titles = ['Customers', 'Products', 'Cart', 'Store'];

  isCustomer = true;
  isManager = true;

  constructor(private router: Router)
  {
  }

  myMap = new Map<string, string>([
    ['Product', 'products'],
    ['Cart', 'carts'],
    ['Store', 'store'],
    ['Customer', 'customers'],
  ]);

 redirect(route: string){
  this.router.navigate([`/` + route + `/`]);
 }
}
