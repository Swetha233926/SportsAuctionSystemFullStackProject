import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Register {
  username: string;
  email: string;
  passwordHash: string;
  role: string;
}

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  private baseUrl = 'https://localhost:7178/api/Auth/register';

  constructor(private http: HttpClient) {}

  register(user: Register): Observable<any> {
    return this.http.post(this.baseUrl, user);
  }
}
