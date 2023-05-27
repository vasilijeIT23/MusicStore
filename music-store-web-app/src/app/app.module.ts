import { Injectable, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomersComponent } from './customers/customers.component';
import { ProductsComponent } from './products/products.component';
import { ProductTypesComponent } from './product-types/product-types.component';
import { WarehousesComponent } from './warehouses/warehouses.component';
import { StockComponent } from './stock/stock.component';
import { CartComponent } from './cart/cart.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule } from '@angular/material/list';
import { EditCustomerComponent } from './customers/edit-customer/edit-customer.component';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { EditProductComponent } from './products/edit-product/edit-product.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { Observable } from 'rxjs';
import { EditWarehouseComponent } from './warehouses/edit-warehouse/edit-warehouse.component';
import { EditProductTypeComponent } from './product-types/edit-product-type/edit-product-type.component';
import { EditStockComponent } from './stock/edit-stock/edit-stock.component';
import { ProfileComponent } from './profile/profile.component';
import { OrdersComponent } from './orders/orders.component';
import { MatSortModule } from '@angular/material/sort';
import { StripeComponent } from './stripe/stripe.component';
import { NgLetModule } from 'ng-let';
import { CreateStockComponent } from './stock/create-stock/create-stock.component';
import { PaymentsComponent } from './payments/payments.component';
import { OrderService } from './services/order.service';
import { AddToCartComponent } from './products/add-to-cart/add-to-cart.component';
import { UpdateCredentialsComponent } from './profile/update-credentials/update-credentials.component';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');

    if (token) {
      const authRequest = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${token}`)
      });

      return next.handle(authRequest);
    }

    return next.handle(request);
  }
}

@NgModule({
  declarations: [
    AppComponent,
    CustomersComponent,
    ProductsComponent,
    ProductTypesComponent,
    WarehousesComponent,
    StockComponent,
    CartComponent,
    PageNotFoundComponent,
    EditCustomerComponent,
    EditProductComponent,
    LoginComponent,
    RegisterComponent,
    EditWarehouseComponent,
    EditProductTypeComponent,
    EditStockComponent,
    ProfileComponent,
    OrdersComponent,
    StripeComponent,
    CreateStockComponent,
    PaymentsComponent,
    AddToCartComponent,
    UpdateCredentialsComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    HttpClientModule,
    MatIconModule,
    MatCardModule,
    MatExpansionModule,
    MatMenuModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatSidenavModule,
    MatTabsModule,
    MatListModule,
    MatOptionModule,
    MatDialogModule,
    MatPaginatorModule,
    MatSortModule,
    NgLetModule
  ],
  providers: [
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 2500 } },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    OrderService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
