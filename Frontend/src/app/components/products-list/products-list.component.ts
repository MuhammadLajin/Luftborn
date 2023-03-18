import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css'],
})
export class ProductsListComponent implements OnInit {
  productList: Product[] = [];

  constructor(
    private productsService: ProductsService,
    private router: Router
  ) {}

  ngOnInit() {
    this.GetAllProducts();
  }
  GetAllProducts(): void {
    this.productsService.GetAllProducts().subscribe((data) => {
      this.productList = data.Datalist;
    });
  }
  editProduct(product: Product): void {
    this.router.navigate([`/editProduct/${product.Id}`]);
  }
  viewProduct(product: Product): void {
    this.router.navigate([`/viewProduct/${product.Id}`]);
  }
  deleteProduct(product: Product, index: number): void {
    if (confirm('are you sure to delete product' + product.Name + '?')) {
      this.productsService.DeleteProduct(product.Id).subscribe((data) => {
        if (data.Datalist) {
          this.productList.splice(index, 1);
        }
      });
    }
  }
  addProduct(): void {
    this.router.navigate([`/addProduct`]);
  }
}
