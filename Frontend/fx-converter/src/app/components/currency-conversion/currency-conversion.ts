import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { MatIconModule } from '@angular/material/icon';
import { ConversionService } from '../../services/conversion.service';
import { CurrencyService } from '../../services/currency.service';
import { ConversionRequest } from '../../models/conversion-request.model';
import { ConversionResponse } from '../../models/conversion-response.model';
import { Currency } from '../../models/currency.model';

@Component({
  selector: 'app-currency-conversion',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatSnackBarModule,
    MatIconModule
  ],
  templateUrl: './currency-conversion.html',
  styleUrls: ['./currency-conversion.scss']
})
export class CurrencyConversionComponent implements OnInit {
  conversionForm: FormGroup;
  currencies: Currency[] = [];
  conversionResult: ConversionResponse | null = null;
  isLoading = false;
  isLoadingCurrencies = false;

  constructor(
    private fb: FormBuilder,
    private conversionService: ConversionService,
    private currencyService: CurrencyService,
    private snackBar: MatSnackBar
  ) {
    this.conversionForm = this.fb.group({
      amount: ['', [Validators.required, Validators.min(0.01)]],
      fromCurrency: ['', Validators.required],
      toCurrency: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadCurrencies();
  }

  loadCurrencies(): void {
    this.isLoadingCurrencies = true;
    this.currencyService.getCurrencies().subscribe({
      next: (currencies) => {
        this.currencies = currencies;
        this.isLoadingCurrencies = false;
      },
      error: (error) => {
        console.error('Error loading currencies:', error);
        this.snackBar.open('Error loading currencies', 'Close', { duration: 3000 });
        this.isLoadingCurrencies = false;
      }
    });
  }

  convertCurrency(): void {
    if (this.conversionForm.valid) {
      this.isLoading = true;
      const request: ConversionRequest = this.conversionForm.value;
      
      this.conversionService.convertCurrency(request).subscribe({
        next: (result) => {
          this.conversionResult = result;
          this.isLoading = false;
          this.snackBar.open('Conversion completed successfully!', 'Close', { duration: 3000 });
        },
        error: (error) => {
          console.error('Error converting currency:', error);
          this.snackBar.open('Error converting currency', 'Close', { duration: 3000 });
          this.isLoading = false;
        }
      });
    } else {
      this.snackBar.open('Please fill in all required fields', 'Close', { duration: 3000 });
    }
  }

  swapCurrencies(): void {
    const fromCurrency = this.conversionForm.get('fromCurrency')?.value;
    const toCurrency = this.conversionForm.get('toCurrency')?.value;
    
    this.conversionForm.patchValue({
      fromCurrency: toCurrency,
      toCurrency: fromCurrency
    });
  }

  getErrorMessage(fieldName: string): string {
    const field = this.conversionForm.get(fieldName);
    if (field?.hasError('required')) {
      return `${fieldName} is required`;
    }
    if (field?.hasError('min')) {
      return 'Amount must be greater than 0';
    }
    return '';
  }
}