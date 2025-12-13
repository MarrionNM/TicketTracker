import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Ticket } from '../../models/Ticket';
import { ApiService } from '../../services/api.service';
import { Router } from '@angular/router';
import { TagsComponent } from '../../components/tags/tags.component';

@Component({
  selector: 'app-tickets',
  standalone: true,
  imports: [CommonModule, FormsModule, TagsComponent],
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss'],
})
export class TicketsComponent {
  tickets: Ticket[] = [];
  searchText = '';
  sortOrder: '' | 'asc' | 'desc' = '';
  loading = false;

  constructor(private api: ApiService, private router: Router) {}

  ngOnInit() {
    this.fetchTickets();
  }

  fetchTickets() {
    this.loading = true;

    let sort = '';
    if (this.sortOrder === 'asc') sort = 'asc';
    if (this.sortOrder === 'desc') sort = 'desc';

    this.api.getTickets(this.searchText, sort).subscribe({
      next: (res) => {
        this.tickets = res.data;
        this.loading = false;
      },
      error: () => {
        this.tickets = [];
        this.loading = false;
      },
    });
  }

  applySearch() {
    this.fetchTickets();
  }

  applySort() {
    this.fetchTickets();
  }

  goToCreate() {
    this.router.navigate(['/ticket/create']);
  }

  openTicket(t: Ticket) {
    this.router.navigate(['/ticket', t.id]);
  }
}
