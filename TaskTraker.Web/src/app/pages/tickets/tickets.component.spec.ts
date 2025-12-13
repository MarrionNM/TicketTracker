import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TicketsComponent } from './tickets.component';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';

describe('TicketsComponent', () => {
  let component: TicketsComponent;
  let fixture: ComponentFixture<TicketsComponent>;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(async () => {
    apiServiceSpy = jasmine.createSpyObj('ApiService', ['getTickets']);

    await TestBed.configureTestingModule({
      imports: [TicketsComponent],
      providers: [
        { provide: ApiService, useValue: apiServiceSpy },
        {
          provide: Router,
          useValue: jasmine.createSpyObj('Router', ['navigate']),
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(TicketsComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to create ticket page', () => {
    const router = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    component.goToCreate();

    expect(router.navigate).toHaveBeenCalledWith(['/ticket/create']);
  });
});
