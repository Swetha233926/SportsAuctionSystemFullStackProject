import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayermanagementComponent } from './playermanagement.component';

describe('PlayermanagementComponent', () => {
  let component: PlayermanagementComponent;
  let fixture: ComponentFixture<PlayermanagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlayermanagementComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PlayermanagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
