import { Component,OnInit } from '@angular/core';
import { Player } from '../../models/player';
import { PlayerService } from '../../services/player.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-playermanagement',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './playermanagement.component.html',
  styleUrl: './playermanagement.component.scss'
})
export class PlayermanagementComponent implements OnInit {

  players: Player[] = [];  
  errorMessage: string = '';  
  isAddingPlayer: boolean = false; 
  isViewingPlayers: boolean = false;
  isUpdatingPlayer: boolean = false;
  isDeletingPlayer: boolean = false;
  selectedPlayer: Player | null = null;
  playerId: number | null = null; 
  playerIdToDelete: number | null = null; 
  successMessage: string = '';


  // Make sure the newPlayer is defined properly with the Player interface
  newPlayer: Player = {
    playerName: '',
    email:'',
    sport: '',
    age: 0,
    country: '',
    position: '',
    basePrice: 0,
    skills: '',
    performanceStats: '',
    agentId: 0,
    status: 'Active'
  };

  constructor(private playerService: PlayerService) { }

  ngOnInit(): void {
    this.getPlayers();
  }

  // Method to get all players from the API
  getPlayers(): void {
    this.playerService.getAllPlayers().subscribe({
      next: (data) => {
        this.players = data;  
        this.isViewingPlayers = true;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load players: ' + err.message;  
      }
    });
  }

  // Fetch player details by ID
  getPlayerById(): void {
    if (this.playerId !== null) {
      this.playerService.getPlayerById(this.playerId).subscribe({
        next: (data) => {
          this.selectedPlayer = data;  
          this.errorMessage = '';
        },
        error: (err) => {
          this.errorMessage = 'Player not found: ' + err.message;
          this.selectedPlayer = null;
        }
      });
    }
  }

  // Show Add Player form
  showAddPlayerForm(): void {
    this.isAddingPlayer = true;
    this.isViewingPlayers = false;
    this.isUpdatingPlayer = false;
    this.isDeletingPlayer = false;
  }

  // Show all players
  showAllPlayers(): void {
    this.isViewingPlayers = true;
    this.isAddingPlayer = false;
    this.isUpdatingPlayer = false;
    this.isDeletingPlayer = false;
  }

  // Show Update Player form
  showUpdatePlayerForm(): void {
    this.isUpdatingPlayer = true;
    this.isAddingPlayer = false;
    this.isViewingPlayers = false;
    this.isDeletingPlayer = false;
  }

  // Show Delete Player form
  showDeletePlayerForm(): void {
    this.isDeletingPlayer = true;
    this.isAddingPlayer = false;
    this.isViewingPlayers = false;
    this.isUpdatingPlayer = false;
  }


  // Add new player
  addPlayer(): void {
    this.playerService.createPlayer(this.newPlayer).subscribe({
      next: (data) => {
        this.players.push(data); 
        this.isAddingPlayer = false;
        this.getPlayers(); 
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to add player. Please try again.';
        alert('Failed to add player: ' + errorMessage);
        this.errorMessage = 'Failed to add player: ' + err.message;
      }
    });
  }

  // Cancel adding a player
  cancelAddPlayer(): void {
    this.isAddingPlayer = false;
  }

  
  // Update player details
  updatePlayer(): void {
    if (this.selectedPlayer && this.playerId !== null) {
      this.playerService.updatePlayer(this.playerId, this.selectedPlayer).subscribe({
        next: () => {
          alert('Player updated successfully!');
          this.selectedPlayer = null;
        },
        error: (err) => {
          const errorMessage = err?.error?.message || err?.error || 'Failed to upadte player. Please try again.';
          alert('Failed to update player: ' + errorMessage);
          this.errorMessage = 'Failed to update player: ' + err.message;
        }
      });
    }
  }

  // Cancel editing
  cancelEdit(): void {
    this.selectedPlayer = null; // Clear the selected player
    this.errorMessage = ''; // Clear any error messages
    this.isUpdatingPlayer = false;
  
  }

  // Delete a player by ID
  deletePlayer(): void {
    if (this.playerIdToDelete !== null) {
      this.playerService.deletePlayer(this.playerIdToDelete).subscribe({
        next: () => {
          this.successMessage = `Player with ID ${this.playerIdToDelete} deleted successfully.`;
          this.errorMessage = '';
          this.playerIdToDelete = null;  // Reset the input field
        },
        error: (err) => {
          const errorMessage = err?.error?.message || err?.error || 'Failed to upadte player. Please try again.';
          alert('Failed to delete player: ' + errorMessage);
          this.errorMessage = `Failed to delete player: ${err.message}`;
          this.successMessage = '';
        }
      });
    }
  }

  // Cancel the delete operation
  cancelDelete(): void {
    this.playerIdToDelete = null;
    this.successMessage = '';
    this.errorMessage = '';
  }
}