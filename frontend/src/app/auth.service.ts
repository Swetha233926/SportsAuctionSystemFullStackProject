import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  // Mock method to check if the user is logged in
  isLoggedIn(): boolean {
    return !!localStorage.getItem('authToken'); // Replace with real authentication logic
  }

  // Mock method to get the current user's role
  getUserRole(): string {
    return localStorage.getItem('userRole') || ''; // Replace with your logic
  }
}
