import { Component,OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuctionService } from '../../services/auction.service';
import { PlayerService } from '../../services/player.service';
import { TeamService } from '../../services/team.service';
import { Auction } from '../../models/Auction';
import { Player } from '../../models/player';
import { Team } from '../../models/team';
import { Router } from '@angular/router';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.scss'],
  standalone: true,
  imports: [CommonModule]
})
export class AuctionComponent implements OnInit {
  ongoingAuctions: Auction[] = [];
  upcomingAuctions: Auction[] = [];
  completedAuctions:Auction[]=[];
  playerDetails:Player[]=[];
  teamDetails:Team[]=[];
  isViewingOngoingAuction:boolean=true;
  isViewingUpcomingAuction:boolean=false;
  isViewingCompletedAuction:boolean=false;
  isViewingPlayers:boolean=false;
  isViewingTeams:boolean=false;
  errorMessage = '';
  userRole:string|null=null;

  constructor(private auctionService: AuctionService,private playerService: PlayerService,private teamService: TeamService,private router:Router,private loginService:LoginService) {}

  ngOnInit(): void {
    this.fetchOngoingAuctions();
    this.fetchUpcomingAuctions();
    this.fetchCompletedAuctions();
    this.fetchPlayerDeatils();
    this.fetchTeamDetails();
    this.userRole=this.loginService.getUserRole();
  }

  isLoggedIn(): boolean {
    return this.loginService.isLoggedIn();  // Check if the user is logged in
  }

  userHasRole(role: string): boolean {
    return this.userRole === role;  // Check if the user has a specific role
  }


  viewBidsForAuction(auctionId:number,sport:string):void{
    this.router.navigate(['/bids'],{queryParams:{auctionId,sport}});
  }

  navigateToFinanceReports(): void {
    this.router.navigate(['/financeReports']); // Navigate to the desired route
  }

  fetchOngoingAuctions(): void {
    this.auctionService.getOngoingAuctionsByStatus().subscribe({
      next: (data) => {
        this.ongoingAuctions = data;
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to load ongoing auction. Please try again.';
        alert('Failed to load ongoing auction: ' + errorMessage);
        this.errorMessage = 'Failed to load ongoing auctions: ' + err.message;
      },
    });
  }

  fetchUpcomingAuctions(): void {
    this.auctionService.getUpcomingAuctionsByStatus().subscribe({
      next: (data) => {
        this.upcomingAuctions = data;
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to load upcoming auction. Please try again.';
        alert('Failed to load upcoming auction: ' + errorMessage);
        this.errorMessage = 'Failed to load upcoming auctions: ' + err.message;
      },
    });
  }

  fetchCompletedAuctions(): void {
    this.auctionService.getCompletedAuctionsByStatus().subscribe({
      next: (data) => {
        this.completedAuctions = data;
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to load completed auction. Please try again.';
        alert('Failed to load completed auction: ' + errorMessage);
        this.errorMessage = 'Failed to load Completed auctions: ' + err.message;
      },
    });
  }

  fetchPlayerDeatils():void{
    this.playerService.getAllPlayers().subscribe({
      next:(data)=>{
        this.playerDetails=data;
      },
      error:err=>{
        const errorMessage = err?.error?.message || err?.error || 'Failed to load player details. Please try again.';
        alert('Failed to load player details: ' + errorMessage);
        this.errorMessage='Failed to load the player details'+err.message;
      }
    })
  }

  fetchTeamDetails():void{
    this.teamService.getAllTeams().subscribe({
      next:(data)=>{
        this.teamDetails=data;
      },
      error:err=>{
        const errorMessage = err?.error?.message || err?.error || 'Failed to load team details. Please try again.';
        alert('Failed to load team details: ' + errorMessage);
        this.errorMessage='Failed to load team details'+err.message;
      }
    })
  }


  //show ongoing auctionsdetails
  showOngoingAuctions(): void {
    this.isViewingOngoingAuction = true;
    this.isViewingUpcomingAuction = false;
    this.isViewingTeams = false;
    this.isViewingPlayers = false;
    this.isViewingCompletedAuction=false;
  }

  showUpcomingAuctions(): void {
    this.isViewingOngoingAuction = false;
    this.isViewingUpcomingAuction = true;
    this.isViewingTeams = false;
    this.isViewingPlayers = false;
    this.isViewingCompletedAuction=false;
  }

  showCompletedAuctions(): void {
    this.isViewingOngoingAuction = false;
    this.isViewingUpcomingAuction = false;
    this.isViewingTeams = false;
    this.isViewingPlayers = false;
    this.isViewingCompletedAuction=true;
  
  }

  showTeams(): void {
    this.isViewingOngoingAuction = false;
    this.isViewingUpcomingAuction = false;
    this.isViewingTeams = true;
    this.isViewingPlayers = false;
    this.isViewingCompletedAuction=false;
  }

  showPlayers(): void {
    this.isViewingOngoingAuction = false;
    this.isViewingUpcomingAuction = false;
    this.isViewingTeams = false;
    this.isViewingPlayers = true;
    this.isViewingCompletedAuction=false;
  }
}
