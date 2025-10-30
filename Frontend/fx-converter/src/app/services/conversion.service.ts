import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ConversionRequest } from '../models/conversion-request.model';
import { ConversionResponse } from '../models/conversion-response.model';
import { ConversionHistory } from '../models/conversion-history.model';

@Injectable({
  providedIn: 'root'
})
export class ConversionService {
  private apiUrl = `${environment.apiUrl}/api/conversion`;

  constructor(private http: HttpClient) { }

  convertCurrency(request: ConversionRequest): Observable<ConversionResponse> {
    return this.http.post<ConversionResponse>(this.apiUrl, request);
  }

  getConversionHistory(userId: string = 'default-user'): Observable<ConversionHistory[]> {
    return this.http.get<ConversionHistory[]>(`${this.apiUrl}/history?userId=${userId}`);
  }
}
