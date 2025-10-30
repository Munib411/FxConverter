export interface ConversionHistory {
  conversionId: string;
  fromCurrency: string;
  toCurrency: string;
  fromAmount: number;
  toAmount: number;
  exchangeRate: number;
  conversionDate: string;
}
