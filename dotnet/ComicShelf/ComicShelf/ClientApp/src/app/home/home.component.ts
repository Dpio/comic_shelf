import { Component, OnInit } from '@angular/core';
import { AuthenticateService } from '../shared/services/authenticate.service';
import { AuthenticateResponse } from '../shared/models/authenticate.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  currentUser: AuthenticateResponse = new AuthenticateResponse();

  constructor(private authenticateService: AuthenticateService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit(): void {
  }

}
