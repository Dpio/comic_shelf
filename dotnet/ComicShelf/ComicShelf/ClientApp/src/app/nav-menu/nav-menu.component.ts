import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { MenuItem } from './menu-items';
import { RentService } from '../shared/services/rent.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  currentUser: AuthenticateResponse = new AuthenticateResponse;
  requestCount: number;
  menuItems: MenuItem[] = [
    new MenuItem('Homepage', 'home', '/'),
  ];
  constructor(private rentService: RentService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit(): void {
    if (this.currentUser === null) {
      this.menuItems = [
        new MenuItem('Homepage', 'home', '/'),
      ];
    } else {
      this.rentService.getRentRequestsCount(this.currentUser.id).subscribe(count => {
        const requestCount = count;
        if (requestCount === 0) {
          this.menuItems = [
            new MenuItem('Homepage', 'home', '/'),
            new MenuItem('Comic', 'book', '/comic'),
            new MenuItem('Collection', 'star', '/collection'),
            new MenuItem('Rent', 'transfer', '/rent'),
          ];
        } else {
          this.menuItems = [
            new MenuItem('Homepage', 'home', '/'),
            new MenuItem('Comic', 'book', '/comic'),
            new MenuItem('Collection', 'star', '/collection'),
            new MenuItem('Rent' + ' ' + requestCount, 'transfer', '/rent'),
          ];
        }
      });
    }
  }
}
