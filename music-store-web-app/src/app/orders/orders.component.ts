import { Component , OnInit} from '@angular/core';
import { Order, OrderClient } from '../api/api-reference';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  displayedColumns: string[] = ['name', 'capacity', 'actions'];

  custId: string = localStorage.getItem('id')!;
  orders: Order[] = [];
  id: string | undefined;

  showItems: boolean = false;
  hideItems: boolean = false;

  constructor(private orderClient: OrderClient,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar) {}


  ngOnInit(){
    this.orderClient.getAll().subscribe(result => {
      this.orders = result.filter(order => order?.customer?.id === this.custId);
      console.log(result)
    })
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
}
