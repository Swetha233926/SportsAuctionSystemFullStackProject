import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Team } from '../../models/team';
import { TeamService } from '../../services/team.service';

@Component({
  selector: 'app-teammanagement',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './teammanagement.component.html',
  styleUrl: './teammanagement.component.scss'
})
export class TeammanagementComponent implements OnInit{

  ngOnInit(): void {
    this.getTeams();
  }
   //adding team 
   teams: Team[]=[];
   errorMessage:string="";
   isAddingTeam: boolean = false;
   isViewingTeams:boolean = false;
   isUpdatingTeam:boolean=false;
   isDeletingTeam:boolean = false;
   selectedTeam:Team|null=null;
   teamId:number|null=null;
   teamIdToDelete:number|null=null;
   successMessage:string='';

   newTeam: Team={
    teamName:'',
    managerId:0,
    sport:'',
    budget:0,
    region:'',
    rosterSize:0,
    totalExpenditure:0
   }

   constructor(private teamService: TeamService){}

   // Show Add Player form
   showAddTeamForm(): void {
    this.isAddingTeam = true;
    this.isViewingTeams = false;
    this.isUpdatingTeam = false;
    this.isDeletingTeam = false;
  }
  
  // Show all teams
  showAllTeams(): void {
    this.isViewingTeams = true;
    this.isAddingTeam = false;
    this.isUpdatingTeam = false;
    this.isDeletingTeam = false;
  }
  
  // Show Update Team form
  showUpdateTeamForm(): void {
    this.isUpdatingTeam = true;
    this.isAddingTeam = false;
    this.isViewingTeams = false;
    this.isDeletingTeam = false;
  }
  
  // Show Delete Team form
  showDeleteTeamForm(): void {
    this.isDeletingTeam = true;
    this.isAddingTeam = false;
    this.isViewingTeams = false;
    this.isUpdatingTeam = false;
  }
  
   //method to get all team 
   getTeams(): void{
    this.teamService.getAllTeams().subscribe({
      next:(data)=>{
        this.teams=data;
        this.isViewingTeams=true;
      },
      error:(err)=>{
        const errorMessage = err?.error?.message || err?.error || 'Failed to load team. Please try again.';
        alert('Failed to load team: ' + errorMessage);
        this.errorMessage="Failed to load Teams: "+err.message;
      }
    })
   }

   // Fetch team details by ID
  getTeamById(): void {
    if (this.teamId !== null) {
      this.teamService.getTeamById(this.teamId).subscribe({
        next: (data) => {
          this.selectedTeam = data;  
          this.errorMessage = '';
        },
        error: (err) => {
          this.errorMessage = 'Team not found: ' + err.message;
          this.selectedTeam = null;
        }
      });
    }
  }

   // Add new team
  addTeam(): void {
    this.teamService.createPlayer(this.newTeam).subscribe({
      next: (data) => {
        this.teams.push(data); 
        this.isAddingTeam = false;
        this.getTeams(); 
      },
      error: (err) => {
        const errorMessage = err?.error?.message || err?.error || 'Failed to add team. Please try again.';
        alert('Failed to add team: ' + errorMessage);
        this.errorMessage = 'Failed to add team: ' + err.message;
      }
    });
  }

  // Cancel adding a team
  cancelAddTeam(): void {
    this.isAddingTeam = false;
  }

  // Update player details
  updateTeam(): void {
    if (this.selectedTeam && this.teamId !== null) {
      this.teamService.updatePlayer(this.teamId, this.selectedTeam).subscribe({
        next: () => {
          alert('Player updated successfully!');
          this.selectedTeam = null;
        },
        error: (err) => {
          const errorMessage = err?.error?.message || err?.error || 'Failed to update team. Please try again.';
          alert('Failed to add team: ' + errorMessage);
          this.errorMessage = 'Failed to update player: ' + err.message;
        }
      });
    }
  }

  // Cancel editing
  cancelEdit(): void {
    this.selectedTeam = null; 
    this.errorMessage = ''; 
    this.isUpdatingTeam= false;
  }

  // Delete a player by ID
  deleteTeam(): void {
    if (this.teamIdToDelete !== null) {
      this.teamService.deletePlayer(this.teamIdToDelete).subscribe({
        next: () => {
          this.successMessage = `Player with ID ${this.teamIdToDelete} deleted successfully.`;
          this.errorMessage = '';
          this.teamIdToDelete = null;  // Reset the input field
        },
        error: (err) => {
          const errorMessage = err?.error?.message || err?.error || 'Failed to delete team. Please try again.';
          alert('Failed to add team: ' + errorMessage);
          this.errorMessage = `Failed to delete player: ${err.message}`;
          this.successMessage = '';
        }
      });
    }
  }

  // Cancel the delete operation
  cancelDelete(): void {
    this.teamIdToDelete = null;
    this.successMessage = '';
    this.errorMessage = '';
  }
}
