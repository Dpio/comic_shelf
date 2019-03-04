import { Component } from '@angular/core';
import { AuthenticateResponse } from './shared/models/authenticate.model';
import { AuthenticateService } from './shared/services/authenticate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  currentUser: AuthenticateResponse = new AuthenticateResponse();

  constructor(private authenticateService: AuthenticateService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
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
}
