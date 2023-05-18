import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; 

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
import {MatSidenavModule} from '@angular/material/sidenav'; 
import {MatTableModule} from '@angular/material/table'; 
import {MatTabsModule} from '@angular/material/tabs';
import {MatListModule} from '@angular/material/list';
import { EditCustomerComponent } from './customers/edit-customer/edit-customer.component';
import {MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBarModule} from '@angular/material/snack-bar';
import {MatButtonModule} from '@angular/material/button';
import{MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import{MatDialogModule} from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { MatCardModule} from '@angular/material/card';
import { MatExpansionModule } from '@angular/material/expansion';
import { EditProductComponent } from './products/edit-product/edit-product.component';

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
    EditProductComponent
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
    MatDialogModule
  ],
  providers: [
    {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { duration: 2500 } }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
