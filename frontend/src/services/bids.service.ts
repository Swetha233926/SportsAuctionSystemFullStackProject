import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bids } from '../models/Bids';
@Injectable({
  providedIn: 'root'
})
export class BidsService {
  private apiUrl='https://localhost:7178/api/Bids';

  constructor(private http:HttpClient) { }
  //Get Bids by Auction id
  getBidsByAuctionId(id:number):Observable<Bids[]>{
    return this.http.get<Bids[]>(`${this.apiUrl}/auction/${id}`);
  }

  //post bids
  addBids(bid:Bids):Observable<Bids>{
    const token = localStorage.getItem('authToken'); 
      const userRole = localStorage.getItem('userRole'); 

      if (!token || userRole !== 'TeamManager') {
        console.error('User is not authorized or token is missing.');
        return new Observable();  
      }
  
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      return this.http.post<Bids>(this.apiUrl, bid,{headers});
  }
}
