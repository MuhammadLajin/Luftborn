import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product, ResponseDto, CreateProduct } from '../models/product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  constructor(private http: HttpClient) {}
  GetAllProducts(): Observable<ResponseDto<Product[]>> {
    return this.http.get<ResponseDto<Product[]>>(
      'http://localhost:6140/api/Product/GetAllProducts'
    );
  }
  GetProductById(id: number): Observable<ResponseDto<Product>> {
    return this.http.get<ResponseDto<Product>>(
      'http://localhost:6140/api/Product/GetProductById?Id=' + id
    );
  }
  DeleteProduct(id: number): Observable<ResponseDto<boolean>> {
    return this.http.delete<ResponseDto<boolean>>(
      `http://localhost:6140/api/Product/DeleteProduct?id=${id}&UserId=1`
    );
  }
  AddProduct(CreateProduct: CreateProduct): Observable<ResponseDto<Product>> {
    return this.http.post<ResponseDto<Product>>(
      `http://localhost:6140/api/Product/CreateProduct?UserId=1`,
      CreateProduct
    );
  }
  UpdateProduct(product: Product): Observable<ResponseDto<Product>> {
    return this.http.put<ResponseDto<Product>>(
      `http://localhost:6140/api/Product/UpdateProduct?UserId=1`,
      product
    );
  }
}
