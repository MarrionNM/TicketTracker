import { Routes } from '@angular/router';
import { TicketsComponent } from './pages/tickets/tickets.component';
import { CreateTicketComponent } from './pages/create-ticket/create-ticket.component';
import { TicketDetailsComponent } from './pages/ticket-details/ticket-details.component';

export const routes: Routes = [
  { path: '', redirectTo: 'tickets', pathMatch: 'full' },
  { path: 'tickets', component: TicketsComponent },

  { path: 'ticket/create', component: CreateTicketComponent },
  { path: 'ticket/edit/:id', component: CreateTicketComponent },

  { path: 'ticket/:id', component: TicketDetailsComponent },
];
