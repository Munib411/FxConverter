export interface ExchangeRate {
  base: string;
  date: string;
  rates: { [key: string]: number };
}
