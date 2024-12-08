import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { RegisterService } from '../../services/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink], // Import necessary Angular modules
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'], // Corrected key to 'styleUrls'
})
export class RegisterComponent {
  user = {
    username: '',
    email: '',
    passwordHash: '',
    role: '',
  };

  errorMessage: string = '';
  successMessage: string = '';

  constructor(private registerService:RegisterService,private router:Router){}


  onSubmit(): void {
    this.registerService.register(this.user).subscribe({
      next: (response) => {
        console.log('Registration successful:', response);
        this.successMessage = 'Registration successful!';
        // Redirect to home page
        this.router.navigate(['/login']);
      },
      error: (error) => {
        alert('Registration failed: ' + (error.error || 'Please try again.'));
        console.error('Registration failed:', error);
        this.errorMessage = error.error || 'Registration failed. Please try again.';
      }
    });
}
}
