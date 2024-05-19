import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GetStatisticsResponse } from '../../models/dtos/statistics-response.model';
import { environment } from '../../../environments/environment';
import { GetOrganizationStatisticsResponse } from '../../models/dtos/organization-statistics-response.model';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  constructor(private http: HttpClient) { }

  getAllOrganizationsStatistics(): Observable<GetStatisticsResponse[]> {
    return this.http.get<GetStatisticsResponse[]>(`${environment.apiBaseUrl}/Statistics/admin-statistics`);
  }

  getOrganizationStatistics(id: string): Observable<GetOrganizationStatisticsResponse[]> {
    return this.http.get<GetOrganizationStatisticsResponse[]>(`${environment.apiBaseUrl}/Statistics/organization-statistics/${id}`);
  }
}
