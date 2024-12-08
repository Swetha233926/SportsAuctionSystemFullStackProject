// models/player.ts
export interface Player {
  playerId?:number,
  playerName: string;
  email:string;
  sport: string;
  age: number;
  country: string;
  position: string;
  basePrice: number;
  skills: string;
  performanceStats: string;
  agentId: number;
  status: string;
  currentBid?:number
}
