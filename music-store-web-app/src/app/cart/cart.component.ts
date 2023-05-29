import { Component } from '@angular/core';
import { Cart, CartClient, CartItem, CustomerClient, EmptyCartCommand, RemoveCartItemCommand } from '../api/api-reference';
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
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.cartClient.getAll().subscribe(result => {
      if (result !== null) {
        this.cart = result.filter(x => x.customer!.id === this.id);
        if (this.cart.length === 1) {
          this.cartItems = this.cart[0]?.cartItems!;
        }
      }
      else {
        this.snackBar.open("Something went wrong")
        console.error("Empty request")
      }
    });
  }


  removeFromCart(cartItemId: string) {
    this.customerClient.removeFromCart(new RemoveCartItemCommand({
      cartItemId: cartItemId
    })
    ).subscribe(_ => {
      this.snackBar.open("Cart item removed successfully")
      this.router.navigate([this.router.url])
    });
  }

  emptyCart() {
    if (this.cart[0].cartItems?.length! > 0) {
      this.customerClient.emptyCart(new EmptyCartCommand({
        customerId: this.id
      })).subscribe(_ => {
        this.snackBar.open("Cart emptied successfully");
        this.router.navigate([this.router.url]);
      });
    }
    else {
      this.snackBar.open("Cart is empty");
    }
  }

  onCheckout() {
    if (this.cart[0].cartItems?.length! > 0) {
      this.router.navigate([`stripe`]);
    }
    else {
      this.snackBar.open("Cart is empty");
    }
  }
}
