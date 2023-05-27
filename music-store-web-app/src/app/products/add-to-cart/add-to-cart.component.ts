import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AddToCartCommand, CustomerClient } from 'src/app/api/api-reference';

@Component({
  selector: 'app-add-to-cart',
  templateUrl: './add-to-cart.component.html',
  styleUrls: ['./add-to-cart.component.css']
})
export class AddToCartComponent implements OnInit {

  id: string | undefined;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private readonly snackBar: MatSnackBar,
    public dialog: MatDialog,
    private client: CustomerClient) { }

  formGroup = new FormGroup({
    quantity: new FormControl(0, { nonNullable: true }),
  });

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? undefined;
  }

  onAddToCart(){
    this.client.addToCart(new AddToCartCommand({
      productId: this.id,
      customerId: localStorage.getItem('id')!,
      quantity: this.formGroup.controls.quantity.value
    })).subscribe(_ => {
      this.snackBar.open('Added to cart');
      this.router.navigate([`products`]);
    });
  }

}
