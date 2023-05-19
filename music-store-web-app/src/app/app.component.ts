import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'music-store-web-app';

  isCustomer = true;
  isManager = true;

  constructor(private router: Router)
  {
  }

 redirect(route: string){
  this.router.navigate([`/` + route + `/`]);
 }
}
