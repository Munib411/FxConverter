import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { ConversionService } from '../../services/conversion.service';
import { ConversionHistory } from '../../models/conversion-history.model';

@Component({
  selector: 'app-conversion-history',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatCardModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatIconModule
  ],
  templateUrl: './conversion-history.html',
  styleUrls: ['./conversion-history.scss']
})
export class ConversionHistoryComponent implements OnInit {
  displayedColumns: string[] = ['conversionDate', 'fromAmount', 'fromCurrency', 'toAmount', 'toCurrency', 'exchangeRate'];
  conversionHistory: ConversionHistory[] = [];
  isLoading = false;

  constructor(
    private conversionService: ConversionService,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadConversionHistory();
  }

  loadConversionHistory(): void {
    this.isLoading = true;
    this.conversionService.getConversionHistory().subscribe({
      next: (history) => {
        this.conversionHistory = history;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading conversion history:', error);
        this.snackBar.open('Error loading conversion history', 'Close', { duration: 3000 });
        this.isLoading = false;
      }
    });
  }

  refreshHistory(): void {
    this.loadConversionHistory();
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleString();
  }

  formatAmount(amount: number): string {
    return amount.toFixed(2);
  }

  formatExchangeRate(rate: number): string {
    return rate.toFixed(4);
  }
}