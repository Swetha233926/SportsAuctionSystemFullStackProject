import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionmanagementComponent } from './auctionmanagement.component';

describe('AuctionmanagementComponent', () => {
  let component: AuctionmanagementComponent;
  let fixture: ComponentFixture<AuctionmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AuctionmanagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AuctionmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
