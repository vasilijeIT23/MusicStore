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
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'customers', component: CustomersComponent},
  { path: 'customers/edit/:id', component: EditCustomerComponent},
  { path: 'products', component: ProductsComponent},
  { path: 'products/create', component: EditProductComponent, canActivate: [AuthGuard]},
  { path: 'products/edit/:id', component: EditProductComponent, canActivate: [AuthGuard]},
  { path: 'warehouses', component: WarehousesComponent, canActivate: [AuthGuard]},
  { path: 'stock', component: StockComponent, canActivate: [AuthGuard]},
  { path: 'cart', component: CartComponent},
  { path: 'productTypes', component: ProductTypesComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
