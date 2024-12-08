import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BidsmanagementComponent } from './bidsmanagement.component';

describe('BidsmanagementComponent', () => {
  let component: BidsmanagementComponent;
  let fixture: ComponentFixture<BidsmanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BidsmanagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(BidsmanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
