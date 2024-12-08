import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Login } from '../models/Login';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private baseUrl = 'https://localhost:7178/api/Auth/login'; 

  constructor(private http: HttpClient) {}

  // Login method
  login(userData: Login): Observable<any> {
    return this.http.post<any>(this.baseUrl, userData); 
  }

  getToken(): string | null {
    return localStorage.getItem('authToken'); 
  }

  // Save auth data (token and role)
  saveAuthData(token: string, role: string, id:number): void {
    localStorage.setItem('authToken', token);
    localStorage.setItem('userRole', role);
    localStorage.setItem('userId', id.toString());
  }

  // Check if the user is logged in
  isLoggedIn(): boolean {
    return !!localStorage.getItem('authToken');
  }

  // Get user role
  getUserRole(): string {
    return localStorage.getItem('userRole') || '';
  }

  //get user id
  getUserId(): number {
    const id = localStorage.getItem('userId');
    return id ? parseInt(id, 10) : 0; // Provide a fallback value if id is null
  }

  // Logout
  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userRole');
  }
}
