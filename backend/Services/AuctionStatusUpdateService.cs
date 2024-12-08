using Microsoft.Extensions.Hosting;
using SportsAuctionSystem.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
namespace SportsAuctionSystem.Services
{
    public class AuctionStatusUpdateService:IHostedService,IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private System.Threading.Timer _timer;
        public AuctionStatusUpdateService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        // Start the background service
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new System.Threading.Timer(UpdateAuctionStatuses, null, TimeSpan.Zero, TimeSpan.FromMinutes(1)); // Check every minute
            return Task.CompletedTask;
        }

        // Method that runs every minute to check and update auction statuses
        private async void UpdateAuctionStatuses(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var auctionService = scope.ServiceProvider.GetRequiredService<AuctionService>();
                var bidService = scope.ServiceProvider.GetRequiredService<BidsService>();
                var teamService = scope.ServiceProvider.GetRequiredService<TeamService>();
                var playerService = scope.ServiceProvider.GetRequiredService<PlayerService>();
                var financeService = scope.ServiceProvider.GetRequiredService<FinanceService>();
                var auctionResultsService = scope.ServiceProvider.GetRequiredService<AuctionResultsService>();
                var reportsService = scope.ServiceProvider.GetRequiredService<ReportsService>();
                var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();
                var userService=scope.ServiceProvider.GetRequiredService<UserService>();

                var auctions = await auctionService.GetAllAuctionsAsync();
                var bids = await bidService.GetAllBidsAsync();
                var teams = await teamService.GetAllTeamsAsync();
                var players = await playerService.GetAllPlayersAsync();
                var currentDateTime = DateTime.Now;

                foreach (var auction in auctions)
                {
                    // Check if the auction is 'Upcoming' and if the start time has passed
                    if (auction.Status == "Upcoming" && auction.Date.Add(auction.StartTime) <= currentDateTime)
                    {
                        auction.Status = "Ongoing";
                        await auctionService.UpdateAuctionStatusAsync(auction);
                    }
                    // Check if the auction is 'Ongoing' and if the end time has passed
                    else if (auction.Status == "Ongoing" && auction.Date.Add(auction.EndTime) <= currentDateTime)
                    {
                        auction.Status = "Completed"; // Mark auction as completed
                        await auctionService.UpdateAuctionStatusAsync(auction);

                        // Create finance records for the completed auction
                        foreach (var bid in bids.Where(b => b.AuctionId == auction.AuctionId && b.Status == "Winning"))
                        {
                            var team = await teamService.GetTeamByIdAsync(bid.TeamId);
                            var player = await playerService.GetPlayerByIdAsync(bid.PlayerId);
                            var teamuser = await userService.GetUserById(team.ManagerId);


                            // Create a finance record for the player purchase
                            var finance = new Finance
                            {
                                TeamId = team.TeamId,
                                TransactionType = "Player Purchase",
                                Amount = bid.BidAmount,
                                Date = DateTime.Now,
                                Details = $"Purchased {player.PlayerName} for {bid.BidAmount} in auction {auction.AuctionId}"
                            };
                            await financeService.CreateFinanceRecordAsync(finance);

                            //create new auction result for the auctionresult table
                            var auctionResult = new AuctionResults
                            {
                                AuctionId = auction.AuctionId,
                                PlayerId = player.PlayerId,
                                WinningTeamId = team.TeamId,
                                FinalPrice = bid.BidAmount,
                                Status="sold"
                            };
                            await auctionResultsService.AddAuctionResultAsync(auctionResult);

                            var reportData = new
                            {
                                AuctionId = auction.AuctionId,
                                TotalTeamsParticipated = bids.Where(b => b.AuctionId == auction.AuctionId).Select(b => b.TeamId).Distinct().Count(),
                                TotalPlayersSold = bids.Count(b => b.AuctionId == auction.AuctionId && b.Status == "Winning"),
                                RevenueGenerated = bids.Where(b => b.AuctionId == auction.AuctionId && b.Status == "Winning").Sum(b => b.BidAmount)
                            };

                            var report = new Reports
                            { 
                                Type = "Auction Summary",
                                GeneratedDate = DateTime.Now,
                                Data = JsonSerializer.Serialize(reportData),
                                CreatedBy = 1
                            };
                            await reportsService.CreateReportAsync(report);

                            // Update team budget and expenditure
                            bid.Status = "Won Bid";
                            team.Budget -= bid.BidAmount;
                            team.TotalExpenditure += bid.BidAmount;

                            // Mark player as sold (Inactive)
                            player.Status = "Inactive";

                            // Notify team manager
                            await notificationService.NotifyTeamManager(teamuser.UserId, teamuser.Email, team.TeamName, bid.BidAmount, player.PlayerName);

                            // Notify the player about the bid update
                            await notificationService.NotifyPlayer(player.PlayerId, player.Email, team.TeamName, bid.BidAmount);

                            // Save updates
                            await bidService.UpdateBidAsync(bid);
                            await teamService.UpdateTeamAsync(team);
                            await playerService.UpdatePlayerAsync(player);
                        }
                    }
                }
            }
        }

        // Stop the background service
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        // Dispose of the timer
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
