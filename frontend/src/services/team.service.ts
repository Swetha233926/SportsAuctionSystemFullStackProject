import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Team } from '../models/team';
@Injectable({
  providedIn: 'root'
})
export class TeamService {
  private apiUrl='https://localhost:7178/api/Team'
  constructor(private http: HttpClient) { }
  
  //get all teams
  getAllTeams():Observable<Team[]>{
    return this.http.get<Team[]>(this.apiUrl);
  }

  // Create a new team
  createPlayer(team: Team): Observable<Team> {
    const token = localStorage.getItem('authToken'); 
    const userRole = localStorage.getItem('userRole'); 

    if (!token || userRole !== 'TeamManager') {
      console.error('User is not authorized or token is missing.');
      return new Observable();  
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post<Team>(this.apiUrl, team,{headers});
  }

  // Update an existing team
  updatePlayer(id: number, team: Team): Observable<void> {
    const token = localStorage.getItem('authToken');
    const userRole = localStorage.getItem('userRole');

    if (!token || userRole !== 'TeamManager') {
      console.error('User is not authorized or token is missing.');
      return new Observable(); 
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<void>(url, team, { headers });
  }

  //Get Tean By Id
  getTeamById(id: number): Observable<Team> {
    const token = localStorage.getItem('authToken');
    const userRole = localStorage.getItem('userRole');

    if (!token || userRole !== 'TeamManager') {
      console.error('User is not authorized or token is missing.');
      return new Observable(); 
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Team>(`${this.apiUrl}/${id}`,{headers});
  }

  // Delete a player
  deletePlayer(id: number): Observable<void> {
    const token = localStorage.getItem('authToken');
    const userRole = localStorage.getItem('userRole');

    if (!token || userRole !== 'PlayerAgent') {
      console.error('User is not authorized or token is missing.');
      return new Observable(); 
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url, { headers });
  }

}
