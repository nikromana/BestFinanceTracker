import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { MonthlySummary } from '../../../models/summary.model';

@Injectable({ providedIn: 'root' })
export class SummaryService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/Summary`;

  getMonthlySummary(year: number, month: number): Observable<MonthlySummary> {
    const params = new HttpParams().set('year', year).set('month', month);
    return this.http.get<MonthlySummary>(this.baseUrl, { params });
  }
}
