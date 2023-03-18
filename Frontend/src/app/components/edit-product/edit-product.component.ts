import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from 'src/app/models/product.model';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css'],
})
export class EditProductComponent implements OnInit {
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
    private route: ActivatedRoute,
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

  ngOnInit() {
    var intValue: any = this.route.snapshot.paramMap.get('id');
    this.productId = intValue;
    this.GetProductById();
  }
  GetProductById(): void {
    this.productsService.GetProductById(this.productId).subscribe((data) => {
      this.product = data.Datalist;
      this.form.setValue({
        Name: this.product.Name,
        AmountAvailable: this.product.AmountAvailable,
        Cost: this.product.Cost,
        SellerId: this.product.SellerId,
      });
    });
  }
  backToProducts(): void {
    this.router.navigateByUrl('/productList');
  }
  editProduct(): void {
    this.productsService
      .UpdateProduct({
        ...this.form.value,
        Id: this.productId,
        SellerId: 1

      })
      .subscribe((data) => {
        this.backToProducts();
      });
  }
}
