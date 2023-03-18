import { Component } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent {
  form: FormGroup = this.formBuilder.group({});
  product: Product = {
    Id: -1,
    Name: '',
    Cost: -1,
    AmountAvailable: -1,
    SellerId: -1,
  };
  productId: number = -1;
  constructor(
    private productsService: ProductsService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.initializeProductForm();
  }
  initializeProductForm() {
    this.form = this.formBuilder.group({
      Name: new FormControl('', Validators.required),
      AmountAvailable: new FormControl('', Validators.required),
      Cost: new FormControl('', Validators.required),
    });
  }

  backToProducts(): void {
    this.router.navigateByUrl('/productList');
  }
  addProduct(): void {
    this.productsService
      .AddProduct({
        ...this.form.value,
        SellerId: 1
      })
      .subscribe((data) => {
        this.backToProducts();
      });
  }
}
