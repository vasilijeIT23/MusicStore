import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  isAuth = false;
  constructor(private router: Router) { };
  canActivate(_next: ActivatedRouteSnapshot, _state: RouterStateSnapshot): boolean | Promise<boolean> {
    this.isAuth = localStorage.getItem('role') === 'Admin' ? true : false;
    if (this.isAuth) {
      return true;
    }
    return this.router.navigate(['/products']);
  }

}
