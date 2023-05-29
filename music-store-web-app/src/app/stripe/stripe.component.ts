import { Component, OnInit } from '@angular/core';
import { Stripe, StripeCardElement, loadStripe } from '@stripe/stripe-js';
import {
  CartClient, CreatePaymentIntentCommand, CreateStripeCustomerCommand,
  CustomerClient, ProcessPaymentCommand, PurchaseFromCartCommand,
  StripeApiClient, ConfirmPaymentIntentCommand, Customer2
} from '../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-stripe',
  templateUrl: './stripe.component.html',
  styleUrls: ['./stripe.component.css']
})
export class StripeComponent implements OnInit {
  stripe: Stripe | null = null;
  cardElement: StripeCardElement | undefined;
  customerId = localStorage.getItem('id');
  success: boolean = false;

  stripeCustomer: Customer2 | null = null;

  paymentForm: FormGroup;

  constructor(
    private cartClient: CartClient,
    private stripeClient: StripeApiClient,
    private customerClient: CustomerClient,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private formBuilder: FormBuilder
  ) { 
    this.paymentForm = this.formBuilder.group({
      currency: ['usd', { nonNullable: true }, Validators.required],
      description: ['', { nonNullable: true }, Validators.required]
    });
  }

  allowedCurrencies: string[] = ['usd', 'eur', 'gbp'];

  ngOnInit() {
    this.loadStripeLibrary();
  }

  async loadStripeLibrary() {
    const stripePublicKey =
      'pk_test_51NBHrNGzhsHSoBW763kKrBCMsA0rB9rMuRpAe0HcgqdoIa86bIicQldZqLzqEPShVwvnCfHXasKdyzXs9JOVdoge003jfuYKM2';
    this.stripe = await loadStripe(stripePublicKey);
    this.createCardElement();
  }

  createCardElement() {
    if (!this.stripe) {
      console.error('Stripe library not loaded.');
      return;
    }

    const elements = this.stripe.elements();
    this.cardElement = elements.create('card');
    this.cardElement.mount('#card-element');
  }
  async handleSubmit(e: Event) {
    e.preventDefault();

    if (!this.stripe || !this.cardElement) {
      console.error('Stripe library not loaded or card element not initialized.');
      return;
    }
    try {
      
      const forms = this.paymentForm;
      const form = document.getElementById('payment-form') as HTMLFormElement;
      const submitButton = form.querySelector('button[type="submit"]') as HTMLButtonElement;
      submitButton.disabled = true;


      const currency = forms['controls']['currency'].value;
      const description = forms['controls']['description'].value;

      this.stripeClient.createStripeCustomer(new CreateStripeCustomerCommand({
        customerId: this.customerId!,
      })).subscribe(response => {
        if (response !== null) {
          this.stripeCustomer = response;
          this.customerClient
            .purchaseProduct(new PurchaseFromCartCommand({
              customerId: this.customerId!
            })).subscribe(response => {
              if (response !== null) {
                this.stripeClient.createPaymentIntent(new CreatePaymentIntentCommand({
                  customer: this.stripeCustomer!,
                  id: this.customerId!,
                  currency: currency,
                  description: description,
                  orderId: response.id
                })).subscribe(result => {
                  if (response !== null) {
                    this.stripeClient.confirmPaymentIntent(new ConfirmPaymentIntentCommand({
                      id: result.id,
                      customerId: localStorage.getItem('id')!,
                    })).subscribe(response => {
                      if (response !== null) {
                        if (response.status === 'succeeded') {
                          this.snackBar.open("Payment succeeded")
                          this.router.navigate([`cart/${localStorage.getItem('id')}`]);
                        }
                        else {
                          this.snackBar.open("Payment failed");
                          this.router.navigate([`cart/${localStorage.getItem('id')}`]);
                        }
                      }
                      else {
                        this.snackBar.open("Something went wrong");
                        console.error("Null response from server");
                      }
                    });
                  }
                  else {
                    this.snackBar.open("Something went wrong");
                    console.error("Null response from server");
                  }
                });
              }
              else {
                this.snackBar.open("Something went wrong");
                console.error("Null response from server");
              }
            });
        }
        else {
          this.snackBar.open("Something went wrong");
          console.error("Null response from server");
        }
      });

      submitButton.disabled = false;
    }
    catch (error) {
      console.error('Error processing payment:', error);
    }
  }

  checkValueInArray(value: string): boolean {
    const stringArray: string[] = ['usd', 'eur', 'gbp'];
    return stringArray.includes(value);
  }
}
