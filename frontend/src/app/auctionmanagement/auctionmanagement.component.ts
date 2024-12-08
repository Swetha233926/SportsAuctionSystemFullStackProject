import { CommonModule } from '@angular/common';
import { Auction } from '../../models/Auction';
import { AuctionService } from '../../services/auction.service';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-auctionmanagement',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './auctionmanagement.component.html',
  styleUrl: './auctionmanagement.component.scss'
})
export class AuctionmanagementComponent implements OnInit{
  auctions: Auction[] = [];  
  errorMessage: string = '';  
  isAddingAuction: boolean = false; 
  isViewingAuction: boolean = false;
  isUpdatingAuction: boolean = false;
  isDeletingAuction: boolean = false;
  selectedAuction: Auction | null = null;
  auctionId: number | null = null; 
  auctionIdToDelete: number | null = null; 
  successMessage: string = '';


  // Make sure the newPlayer is defined properly with the Player interfaces
  newAuction: Auction = {
    date:'',
    auctionName:'',
    sport:'',
    auctioneerId:0,
    startTime:'',
    endTime:'',
    status:''
  };

  constructor(private auctionService: AuctionService,private loginService:LoginService) { }

  ngOnInit(): void {
    this.getAuctions();
  }

  // Show Add Auction form
showAddAuctionForm(): void {
  this.isAddingAuction = true;
  this.isViewingAuction = false;
  this.isUpdatingAuction = false;
  this.isDeletingAuction = false;
}

// Show all auctions
showAllAuctions(): void {
  this.isViewingAuction = true;
  this.isAddingAuction = false;
  this.isUpdatingAuction = false;
  this.isDeletingAuction = false;
}

// Show Update Auction form
showUpdateAuctionForm(): void {
  this.isUpdatingAuction = true;
  this.isAddingAuction = false;
  this.isViewingAuction = false;
  this.isDeletingAuction = false;
}

// Show Delete Auction form
showDeleteAuctionForm(): void {
  this.isDeletingAuction = true;
  this.isAddingAuction = false;
  this.isViewingAuction = false;
  this.isUpdatingAuction = false;
}

// Method to get all players from the API
getAuctions(): void {
  this.auctionService.getAllAuctions().subscribe({
    next: (data) => {
      this.auctions = data;  
      this.isViewingAuction = true;
    },
    error: (err) => {
      const errorMessage = err?.error?.message || err?.error || 'Failed to load auction. Please try again.';
      alert('Failed to load auction: ' + errorMessage);
      this.errorMessage = 'Failed to load Auctions: ' + err.message;  
    }
  });
}

// Fetch auction details by ID
getAuctionById(): void {
  if (this.auctionId !== null) {
    this.auctionService.getAuctionById(this.auctionId).subscribe({
      next: (data) => {
        this.selectedAuction = data;  
        this.errorMessage = '';
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to load auction. Please try again.';
        alert('Failed to load auction: ' + errorMessage);
        this.errorMessage = 'Auction not found: ' + err.message;
        this.selectedAuction = null;
      }
    });
  }
}

// Add a new auction
addAuction(): void {
  this.auctionService.createAuction(this.newAuction).subscribe({
    next: (data) => {
      this.auctions.push(data); 
      this.isAddingAuction = false;
      this.getAuctions(); 
    },
    error: (err) => {
      const errorMessage = err?.error?.message || err?.error || 'Failed to add auction. Please try again.';
      alert('Failed to add auction: ' + errorMessage);
      this.errorMessage = 'Failed to add auction: ' + err.message;
    }
  });
}

// Cancel adding an auction
cancelAddAuction(): void {
  this.isAddingAuction = false;
}

// Update auction details
updateAuction(): void {
  const loggedInUserId = this.loginService.getUserId(); 
  if (this.selectedAuction && this.auctionId !== null) {
    this.auctionService.updateAuction(this.auctionId, this.selectedAuction).subscribe({
      next: () => {
        alert('Auction updated successfully!');
        this.selectedAuction = null;
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to update auction. Please try again.';
        alert('Failed to update auction: ' + errorMessage);
        this.errorMessage = 'Failed to update auction: ' + err.message;
      }
    });
  }
}

// Cancel editing
cancelEdit(): void {
  this.selectedAuction = null; // Clear the selected auction
  this.errorMessage = ''; // Clear any error messages
  this.isUpdatingAuction = false;
}

// Delete an auction by ID
deleteAuction(): void {
  if (this.auctionIdToDelete !== null) {
    
    this.auctionService.deleteAuction(this.auctionIdToDelete).subscribe({
      next: () => {
        this.successMessage = `Auction with ID ${this.auctionIdToDelete} deleted successfully.`;
        this.errorMessage = '';
        this.auctionIdToDelete = null;  // Reset the input field
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to delete auction. Please try again.';
        alert('Failed to delete auction: ' + errorMessage);
        this.errorMessage = `Failed to delete auction: ${err.message}`;
        this.successMessage = '';
      }
    });
  }
}

// Cancel the delete operation
cancelDelete(): void {
  this.auctionIdToDelete = null;
  this.successMessage = '';
  this.errorMessage = '';
}

}
