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
import { EditWarehouseComponent } from './warehouses/edit-warehouse/edit-warehouse.component';
import { EditProductTypeComponent } from './product-types/edit-product-type/edit-product-type.component';
import { EditStockComponent } from './stock/edit-stock/edit-stock.component';
import { ProfileComponent } from './profile/profile.component';
import { OrdersComponent } from './orders/orders.component';
import { StripeComponent } from './stripe/stripe.component';
import { CreateStockComponent } from './stock/create-stock/create-stock.component';
import { PaymentsComponent } from './payments/payments.component';
import { AddToCartComponent } from './products/add-to-cart/add-to-cart.component';
import { UpdateCredentialsComponent } from './profile/update-credentials/update-credentials.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard] },
  { path: 'customers/edit/:id', component: EditCustomerComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'profile/edit/:id', component: EditCustomerComponent },
  { path: 'credentials/:id', component: UpdateCredentialsComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'products/create', component: EditProductComponent, canActivate: [AuthGuard] },
  { path: 'products/edit/:id', component: EditProductComponent, canActivate: [AuthGuard] },
  { path: 'warehouses', component: WarehousesComponent, canActivate: [AuthGuard] },
  { path: 'warehouses/edit/:id', component: EditWarehouseComponent, canActivate: [AuthGuard] },
  { path: 'warehouses/create', component: EditWarehouseComponent, canActivate: [AuthGuard] },
  { path: 'stock', component: StockComponent, canActivate: [AuthGuard] },
  { path: 'stock/create/:id', component: CreateStockComponent, canActivate: [AuthGuard] },
  { path: 'stock/edit/:id', component: EditStockComponent, canActivate: [AuthGuard] },
  { path: 'cart/:id', component: CartComponent },
  { path: 'add/to/cart/:id', component: AddToCartComponent },
  { path: 'stripe', component: StripeComponent },
  { path: 'orders', component: OrdersComponent, canActivate: [AuthGuard] },
  { path: 'orders/:id', component: OrdersComponent },
  { path: 'productTypes', component: ProductTypesComponent, canActivate: [AuthGuard] },
  { path: 'productTypes/edit/:id', component: EditProductTypeComponent, canActivate: [AuthGuard] },
  { path: 'productTypes/create', component: EditProductTypeComponent, canActivate: [AuthGuard] },
  { path: 'payments', component: PaymentsComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
