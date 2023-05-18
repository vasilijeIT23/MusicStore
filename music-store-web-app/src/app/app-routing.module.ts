import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CustomersComponent } from './customers/customers.component';
import { ProductsComponent } from './products/products.component';
import { ProductTypesComponent } from './product-types/product-types.component';
import { WarehousesComponent } from './warehouses/warehouses.component';
import { StockComponent } from './stock/stock.component';
import { CartComponent } from './cart/cart.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { EditCustomerComponent } from './customers/edit-customer/edit-customer.component';
import { EditProductComponent } from './products/edit-product/edit-product.component';

const routes: Routes = [
  { path: 'customers', component: CustomersComponent},
  { path: 'customers/edit/:id', component: EditCustomerComponent},
  { path: 'products', component: ProductsComponent},
  { path: 'products/edit/:id', component: EditProductComponent},
  { path: 'warehouses', component: WarehousesComponent},
  { path: 'stock', component: StockComponent},
  { path: 'cart', component: CartComponent},
  { path: 'productTypes', component: ProductTypesComponent},
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
