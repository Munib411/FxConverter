export interface ConversionResponse {
  fromAmount: number;
  toAmount: number;
  fromCurrency: string;
  toCurrency: string;
  exchangeRate: number;
  conversionDate: string;
}
