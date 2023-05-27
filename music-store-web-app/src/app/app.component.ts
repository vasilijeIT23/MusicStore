import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'music-store-web-app';

  customerId = localStorage.getItem('id');
  token$ = this.authService.token$.asObservable();
  role$ = this.authService.role$.asObservable();

  constructor(private router: Router, private authService: AuthService) {
  }

  ngOnInit(): void {
    this.authService.token$.next(localStorage.getItem('token'))
  }

  redirect(route: string) {
    this.router.navigate([`/` + route + `/`]);
  }

  onCart() {
    this.router.navigate([`cart/${localStorage.getItem('id')}`]);
  }

  onOrder() {
    this.router.navigate([`orders/${localStorage.getItem('id')}`]);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('id');
    this.authService.token$.next(null)
    this.authService.role$.next(null)
    this.router.navigate([`login`]);
  }


}
