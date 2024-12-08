import { Component } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { Login } from '../../models/Login';
import { Router } from '@angular/router';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  user: Login = {
    email: '',
    password: '',
    id:0
  };

  errorMessage: string = '';

  constructor(private loginService: LoginService, private router: Router) {}

  onSubmit(): void {
    this.loginService.login(this.user).subscribe({
      next: (response) => {
        console.log('Login successful:', response);
  
        // Save the token and role if available
        if (response.token && response.role) {
          this.loginService.saveAuthData(response.token, response.role,response.id); // Save both token and role
        } else {
          console.error('Missing token or role in response');
        }
  
        
        this.router.navigate(['/home']);
      },
      error: (error) => {
        console.error('Login failed:', error);
        this.errorMessage = error.error?.message || 'Login failed. Please try again.';
      },
    });
  }
  
}
