import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Player } from '../models/player';

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  private apiUrl = 'https://localhost:7178/api/Player'; 

  constructor(private http: HttpClient) {}

  // Get all players
  getAllPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(this.apiUrl);
  }

  // Create a new player
  createPlayer(player: Player): Observable<Player> {
    const token = localStorage.getItem('authToken'); 
    const userRole = localStorage.getItem('userRole'); 

   
    if (!token || userRole !== 'PlayerAgent') {
      console.error('User is not authorized or token is missing.');
      return new Observable();  
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.post<Player>(this.apiUrl, player,{headers});
  }

  // Update an existing player
  updatePlayer(id: number, player: Player): Observable<void> {
    const token = localStorage.getItem('authToken');
    const userRole = localStorage.getItem('userRole');

    if (!token || userRole !== 'PlayerAgent') {
      console.error('User is not authorized or token is missing.');
      return new Observable(); 
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    const url = `${this.apiUrl}/${id}`;
    return this.http.put<void>(url, player, { headers });
  }

  //Get Player By Id
  getPlayerById(id: number): Observable<Player> {
    const token = localStorage.getItem('authToken');
    const userRole = localStorage.getItem('userRole');

    if (!token || userRole !== 'PlayerAgent') {
      console.error('User is not authorized or token is missing.');
      return new Observable(); 
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<Player>(`${this.apiUrl}/${id}`,{headers});
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

  // Get active players by status and sport
  getActivePlayersByStatusAndSport(sport: string): Observable<Player[]> {
    return this.http.get<Player[]>(
      `${this.apiUrl}/status/active/sport/${sport}`
    );
  }

  // Get inactive players by status and sport
  getInactivePlayersByStatusAndSport(sport: string): Observable<Player[]> {
    return this.http.get<Player[]>(
      `${this.apiUrl}/status/inactive/sport/${sport}`
    );
  }

  

  //get player by sport 
  getPlayerBySport(sport:string):Observable<Player[]>{
    return this.http.get<Player[]>(`${this.apiUrl}/sport/${sport}`)
  }

}
