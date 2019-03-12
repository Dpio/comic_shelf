import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AuthenticateResponse } from '../shared/models/authenticate.model';
import { MenuItem } from './menu-items';
import { AppComponent } from '../app.component';
import { AuthenticateService } from '../shared/services/authenticate.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class NavMenuComponent extends AppComponent implements OnInit {
  isExpanded = false;
  currentUser: AuthenticateResponse = new AuthenticateResponse;
  menuItems: MenuItem[] = [
    new MenuItem('Homepage', 'home', '/'),
  ];
   base: AuthenticateService;
  constructor(authenticateService: AuthenticateService , toastr: ToastrService, router: Router) {
    super(authenticateService, toastr, router);
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  showMenuItem(menuItem): boolean {
    return true;
  }

  ngOnInit(): void {
    if (this.currentUser === null) {
      this.menuItems = [
        new MenuItem('Homepage', 'home', '/'),
      ];
    } else {
      this.menuItems = [
        new MenuItem('Homepage', 'home', '/'),
        new MenuItem('Comic', 'book', '/comic'),
        new MenuItem('Collection', 'star', '/collection'),
        new MenuItem('Rent', 'transfer', '/rent'),
      ];
    }
  }
}
