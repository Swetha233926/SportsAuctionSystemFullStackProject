import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../services/login.service';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  userRole: string | null = null;  // To store the role of the logged-in user

  constructor(private loginService: LoginService, private router: Router) {}

  ngOnInit(): void {
    // Retrieve the user role from localStorage (or from a service)
    this.userRole = this.loginService.getUserRole();
  }

  isLoggedIn(): boolean {
    return this.loginService.isLoggedIn();  // Check if the user is logged in
  }

  userHasRole(role: string): boolean {
    return this.userRole === role;  // Check if the user has a specific role
  }

  login(): void {
    // Navigate to the login page if the user is not logged in
    this.router.navigate(['/login']);
  }

  logout(): void {
    this.loginService.logout();  // Perform logout action
    this.router.navigate(['/login']);  // Redirect to login page after logout
  }

  // Function to navigate to ongoing auctions
  viewOngoingAuctions(): void {
    this.router.navigate(['/home']);  // Navigate to auctions page
  }

  // Function to navigate to player view page
  viewPlayers(): void {
    this.router.navigate(['/players']);  // Navigate to view players page
  }
}
