import { Injectable } from '@angular/core';
import { CanActivate, Router,ActivatedRouteSnapshot } from '@angular/router';
import { LoginService } from '../services/login.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private loginService: LoginService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    if (!this.loginService.isLoggedIn()) {
      this.router.navigate(['/login']); // Redirect if not logged in
      return false;
    }
  
    const requiredRoles = route.data['roles'] as Array<string>;
    const userRole = this.loginService.getUserRole();  // Get the saved role from localStorage
  
    console.log('Required Roles:', requiredRoles); // Debugging
    console.log('User Role:', userRole);             // Debugging
  
    if (requiredRoles && !requiredRoles.includes(userRole)) {
      this.router.navigate(['/unauthorized']); // Redirect if role doesn't match
      return false;
    }
  
    return true;
  }
  
}
