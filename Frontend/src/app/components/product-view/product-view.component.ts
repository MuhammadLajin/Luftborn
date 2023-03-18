import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-product-view',
  templateUrl: './product-view.component.html',
  styleUrls: ['./product-view.component.css'],
})
export class ProductViewComponent implements OnInit {
  product: Product = {
    Id: -1,
    Name: '',
    Cost: -1,
    AmountAvailable: -1,
    SellerId: -1,
  };
  productId: number = -1;
  constructor(
    private route: ActivatedRoute,
    private productsService: ProductsService,
    private router: Router
  ) {}

  ngOnInit() {
    var intValue: any = this.route.snapshot.paramMap.get('id');
    this.productId = intValue;
    this.GetProductById();
  }
  GetProductById(): void {
    this.productsService.GetProductById(this.productId).subscribe((data) => {
      this.product = data.Datalist;
    });
  }
  backToProducts():void{
    this.router.navigateByUrl('/productList')
  }  
}
