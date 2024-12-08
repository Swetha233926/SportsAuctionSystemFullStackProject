import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FinancemanagementComponent } from './financemanagement.component';

describe('FinancemanagementComponent', () => {
  let component: FinancemanagementComponent;
  let fixture: ComponentFixture<FinancemanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FinancemanagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FinancemanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
