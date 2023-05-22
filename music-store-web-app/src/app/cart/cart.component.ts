import { Component } from '@angular/core';
import { Cart, CartClient, CartItem, CustomerClient, EmptyCartCommand, PurchaseFromCartCommand, RemoveCartItemCommand } from '../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  displayedColumns: string[] = ['name', 'capacity', 'actions'];

  custId: string = localStorage.getItem('id')!;
  cart: Cart[] = [];
  cartItems: CartItem[] = [];
  id: string | undefined;

  constructor(private cartClient: CartClient,
    private customerClient: CustomerClient,
     private router: Router,
      private route: ActivatedRoute,
      private snackBar: MatSnackBar) {}

  ngOnInit(){
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.cartClient.getAll().subscribe(result => {
      this.cart = result.filter(x => x.customer!.id === this.id);
      //console.log(result);
      if(this.cart.length === 1){
        console.log(this.cart[0]?.cartItems);
        this.cartItems = this.cart[0]?.cartItems!;
      }
    });
  }

  removeFromCart(cartItemId: string){
    this.customerClient.removeFromCart(new RemoveCartItemCommand({
      cartItemId: cartItemId
    })
    ).subscribe(_ => {
      this.snackBar.open("Cart item removed successfully")
    });
  }

  emptyCart(){
    this.customerClient.emptyCart(new EmptyCartCommand({
      customerId: this.id
    })).subscribe(_ => {
      this.snackBar.open("Cart emptied successfully");
    });
  }

  onCheckout(){
    this.customerClient.purchaseProduct(new PurchaseFromCartCommand({
      customerId: this.id
    })).subscribe(_ => {
      this.snackBar.open("Products purchased successfully!");
    });
  }
}
