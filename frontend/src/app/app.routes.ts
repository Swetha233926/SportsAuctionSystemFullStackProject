import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { PlayermanagementComponent } from './playermanagement/playermanagement.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthGuard } from './auth.guard';
import { TeammanagementComponent } from './teammanagement/teammanagement.component';
import { AuctionmanagementComponent } from './auctionmanagement/auctionmanagement.component';
import { AuctionComponent } from './auction/auction.component';
import { BidsmanagementComponent } from './bidsmanagement/bidsmanagement.component';
import { FinancemanagementComponent } from './financemanagement/financemanagement.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    {
      path: 'player',
      component: PlayermanagementComponent,
      canActivate: [AuthGuard],
      data: { roles: ['PlayerAgent','Admin'] },
    },
    {
      path:'team',
      component:TeammanagementComponent,
      canActivate:[AuthGuard],
      data:{roles:['TeamManager','Admin']}
    },
    {
      path:'auction',
      component:AuctionmanagementComponent,
      canActivate:[AuthGuard],
      data:{roles:['Auctioneer','Admin']}
    },
    {
      path:'bids',
      component:BidsmanagementComponent,
      canActivate:[AuthGuard],
      data:{roles:['Admin','TeamManager','Auctioneer']}
    },
    {
      path:'financeReports',
      component:FinancemanagementComponent,
      canActivate:[AuthGuard],
      data:{roles:['Admin','TeamManager','Analyst']}
    },
    { path: 'login', component: LoginComponent },
    { path: 'registration', component: RegisterComponent },
    {path:'auctions',component:AuctionComponent},
    { path: 'unauthorized', component: UnauthorizedComponent },
    { path: '**', redirectTo: 'home' },
  ];
  

