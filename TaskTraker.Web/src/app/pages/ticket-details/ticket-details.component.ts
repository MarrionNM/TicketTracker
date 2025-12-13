import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { Ticket } from '../../models/Ticket';
import { TagsComponent } from '../../components/tags/tags.component';

@Component({
  selector: 'app-ticket-details',
  standalone: true,
  imports: [CommonModule, TagsComponent],
  templateUrl: './ticket-details.component.html',
  styleUrls: ['./ticket-details.component.scss'],
})
export class TicketDetailsComponent {
  ticket?: Ticket;
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private api: ApiService,
    private router: Router
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.api.getTicket(id).subscribe({
      next: (res) => {
        this.ticket = res.data;
        this.loading = false;
      },
      error: () => {
        this.ticket = undefined;
        this.loading = false;
      },
    });
  }

  editTicket(id: number) {
    this.router.navigate(['/ticket/edit', id]);
  }

  back() {
    this.router.navigate(['/tickets']);
  }
}
