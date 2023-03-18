import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProductComponent } from './components/add-product/add-product.component';
import { EditProductComponent } from './components/edit-product/edit-product.component';
import { ProductViewComponent } from './components/product-view/product-view.component';
import { ProductsListComponent } from './components/products-list/products-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'productList',
    pathMatch: 'full',
  },
  {
    path: 'productList',
    component: ProductsListComponent,
  },
  { path: 'viewProduct/:id', component: ProductViewComponent },
  { path: 'editProduct/:id', component: EditProductComponent },
  { path: 'addProduct', component: AddProductComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
