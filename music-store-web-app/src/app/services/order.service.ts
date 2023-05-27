import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Order {
    id: string;
    customer: Customer;
    orderDate: Date;
    price: number;
    paymentCompleted: boolean;
    payment: Payment;
    orderItems: Helper;
}

export interface Product{
    id: string;
    name: string;
    inStock: boolean;
    price: number;
    productType: ProductType;
    imagePath: string;
    reviews: Review[];
}

export interface Review {
    id: string;
    customer: Customer;
    product: Product;
    grade: Grade;
    description: string;
}

export interface Customer{
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    role: Role;
    status: Status;
    statusExpirationDate: Date | undefined;
    moneySpent: number;
    username: string;
    password: string;
    salt: string;
    orders: Order[];
    reviews: Review[];
}
    
export interface IPayment{
    id: string;
    paymentId: string;
    customerId: string;
    price: number;
}
    
export interface IOrderItem{
    id: string;
    product: Product;
    order: Order;
    quantity: number;
}

export interface ProductType {
    id: string;
    category: Category;
    name: string;
}

export interface OrderItem {
    id: string;
    product: Product;
    order: Order;
    quantity: number;
}

export interface Helper{
    id: string;
    values: OrderItem[];
}

export interface Payment{
    id: string;
    paymentId: string;
    customerId: string;
    price: number;
}

export enum Role {
    Regular = 1,
    Admin = 2,
}

export enum Status {
    Regular = 1,
    Advanced = 2,
}

export enum Grade {
    E = 1,
    D = 2,
    C = 3,
    B = 4,
    A = 5,
}

export enum Category {
    Instrument = 1,
    Equipmnet = 2,
}

@Injectable()
export class OrderService {
  private apiUrl = 'http://localhost:52720/api/orders/all';

  constructor(private http: HttpClient) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }
}