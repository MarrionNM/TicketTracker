import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Ticket } from '../models/Ticket';
import { Observable } from 'rxjs';
import { DataResponse } from '../models/DataResponse';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getTickets(q?: string, sort?: string): Observable<DataResponse<Ticket[]>> {
    let params = new HttpParams();

    if (q) params = params.set('q', q);
    if (sort) params = params.set('sort', sort);

    return this.http.get<DataResponse<Ticket[]>>(`${this.baseUrl}ticket`, {
      params,
    });
  }

  getTicket(id: number) {
    return this.http.get<DataResponse<Ticket>>(`${this.baseUrl}ticket/${id}`);
  }

  createTicket(data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}ticket`, data);
  }

  updateTicket(id: string, payload: any) {
    return this.http.put(`${this.baseUrl}ticket/${id}`, payload);
  }
}
