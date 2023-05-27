import { Component , OnInit} from '@angular/core';
import { Order, OrderClient } from '../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  displayedColumns: string[] = ['name', 'capacity', 'actions'];


  role = localStorage.getItem('role');
  orders: Order[] = [];
  id: string | undefined;

  showItems: boolean = false;
  hideItems: boolean = false;

  constructor(private orderClient: OrderClient,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private orderService: OrderService) {}


  ngOnInit(){
    this.id = this.route.snapshot.paramMap.get('id')!;
    if(this.id !== null)
    {
      this.orderService.getOrders().subscribe(
        (orders: any) => {
          this.orders = orders.filter((order: { customer: { id: string | undefined; }; }) => order?.customer?.id === this.id);
        },
        (error: any) => {
          console.error('Failed to retrieve orders:', error);
        }
      );
    }
    else {
      this.orderService.getOrders().subscribe(
        (orders: any) => {
          this.orders = orders;
        },
        (error: any) => {
          console.error('Failed to retrieve orders:', error);
        }
      );
    }
  }

  getOrderItemsValues(order: any): any[] {
    if (order.orderItems && order.orderItems["$values"]) {
      console.log(order.orderItems["$values"]);
      return order.orderItems['$values'];
    }
    return [];
  }

  toggleOrderItems(){
    if(this.showItems === false)
      this.showItems = true;
    else
      this.showItems = false;
  }

  isOrderExpanded(){
    return this.showItems;
  }

  allOrders(){
    this.router.navigate([`orders`]);
  }
}
