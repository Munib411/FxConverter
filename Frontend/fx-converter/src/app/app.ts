import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CurrencyConversionComponent } from './components/currency-conversion/currency-conversion';
import { ConversionHistoryComponent } from './components/conversion-history/conversion-history';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    MatButtonModule,
    MatIconModule,
    CurrencyConversionComponent,
    ConversionHistoryComponent
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class AppComponent {
  title = 'fx-converter';
  activeTab: 'converter' | 'history' = 'converter';
}