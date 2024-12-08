import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Auction } from '../models/Auction';

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  private apiUrl = 'https://localhost:7178/api/Auction'; 

  constructor(private http: HttpClient) {}
  
    // Get all Auctions
    getAllAuctions(): Observable<Auction[]> {
      const token = localStorage.getItem('authToken'); 
      const userRole = localStorage.getItem('userRole'); 
      if (!token || userRole !== 'Auctioneer') {
        console.error('User is not authorized or token is missing.');
        return new Observable();  
      }
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.get<Auction[]>(this.apiUrl, { headers });
    }

    //get ongoingauctions by status
    getOngoingAuctionsByStatus(): Observable<Auction[]> {
      return this.http.get<Auction[]>(`${this.apiUrl}/status/Ongoing`);
    }
    
    //get upcoming auctions by status
    getUpcomingAuctionsByStatus(): Observable<Auction[]> {
      return this.http.get<Auction[]>(`${this.apiUrl}/status/Upcoming`);
    }
    
    //get completed auctions by status
    getCompletedAuctionsByStatus(): Observable<Auction[]> {
      return this.http.get<Auction[]>(`${this.apiUrl}/status/Completed`);
    }
  
    // Create a new auction
    createAuction(auction: Auction): Observable<Auction> {
      const token = localStorage.getItem('authToken'); 
      const userRole = localStorage.getItem('userRole'); 

      if (!token || userRole !== 'Auctioneer') {
        console.error('User is not authorized or token is missing.');
        return new Observable();  
      }
  
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.post<Auction>(this.apiUrl, auction,{headers});
    }
  
    // Update an existing Auction
    updateAuction(id: number, auction: Auction): Observable<void> {
      const token = localStorage.getItem('authToken');
      const userRole = localStorage.getItem('userRole');
  
      if (!token || userRole !== 'Auctioneer') {
        console.error('User is not authorized or token is missing.');
        return new Observable(); 
      }
  
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      const url = `${this.apiUrl}/${id}`;
      return this.http.put<void>(url, auction, { headers });
    }
  
    //Get Auction By Id
    getAuctionById(id: number): Observable<Auction> {
      return this.http.get<Auction>(`${this.apiUrl}/${id}`);
    }
    
    // Delete an auction
    deleteAuction(id: number): Observable<void> {
      const token = localStorage.getItem('authToken');
      const userRole = localStorage.getItem('userRole');
  
      if (!token || userRole !== 'Auctioneer') {
        console.error('User is not authorized or token is missing.');
        return new Observable(); 
      }
  
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      const url = `${this.apiUrl}/${id}`;
      return this.http.delete<void>(url, { headers });
    }
}
