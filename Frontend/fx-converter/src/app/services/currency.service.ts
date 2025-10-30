import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Currency } from '../models/currency.model';
import { ExchangeRate } from '../models/exchange-rate.model';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  private apiUrl = `${environment.apiUrl}/api/currency`;

  constructor(private http: HttpClient) { }

  getCurrencies(): Observable<Currency[]> {
    return this.http.get<Currency[]>(this.apiUrl);
  }

  getExchangeRates(baseCurrency: string = 'USD'): Observable<ExchangeRate> {
    return this.http.get<ExchangeRate>(`${this.apiUrl}/rates/${baseCurrency}`);
  }
}
