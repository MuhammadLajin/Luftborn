export interface ResponseDto<T> {
  CommandMessage: string;
  IsValidReponse: boolean;
  Datalist: T;
  TotalCount: number;
  Status: number;
}

export interface Product {
  Id: number;
  Name: string;
  Cost: number;
  AmountAvailable: number;
  SellerId: number;
}
export interface CreateProduct {
  Name: string;
  Cost: number;
  AmountAvailable: number;
  SellerId: number;
}
