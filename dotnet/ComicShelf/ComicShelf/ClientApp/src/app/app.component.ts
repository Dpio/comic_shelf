import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { AuthenticateResponse } from './shared/models/authenticate.model';
import { AuthenticateService } from './shared/services/authenticate.service';
import { HubConnection } from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';
import * as signalR from '@aspnet/signalr';
import { Router, NavigationEnd } from '@angular/router';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RentComponent } from './rents/rent.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  @ViewChild(NavMenuComponent) sidebar: NavMenuComponent;
  @ViewChild(RentComponent) rentComponent: RentComponent;
  title = 'app';
  private _hubConnection: HubConnection;
  currentUser: AuthenticateResponse = new AuthenticateResponse();

  constructor(
    private authenticateService: AuthenticateService,
    private toastr: ToastrService,
    private router: Router,
  ) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit(): void {
    // TODO: Reconnect if disconnected error status code 1006
    // Refactor to method
    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Debug)
      .withUrl('http://localhost:5000/notify', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();
    this._hubConnection = connection;
    this._hubConnection
      .start();

    this._hubConnection.on('BroadcastMessage', (msg: string) => {
      this.toastr.info(msg);
    });

    this._hubConnection.on('BroadcastMessageForUser', (userId: number, msg: string) => {
      if (userId === this.currentUser.id && msg) {
        this.toastr.info(msg).onTap.subscribe(() => {
          this.router.navigate(['rent']);
        });
      }
    });
  }

  signInWithGoogle() {
    this.authenticateService.signInWithGoogle().subscribe(response => {
      window.location.href = response;
    });
  }

  logout() {
    this.authenticateService.logout();
    window.location.reload();
  }

  getRentRequests() {
    this.sidebar.ngOnInit();
  }
}
