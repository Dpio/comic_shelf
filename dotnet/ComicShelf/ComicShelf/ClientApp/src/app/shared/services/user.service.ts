import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AuthenticateResponse } from '../models/authenticate.model';

@Injectable()
export class UserService {

    constructor(private http: HttpClient) {
    }

    getUser(id: number): Observable<AuthenticateResponse> {
        const apiUrl = environment.baseApiUrl;
            return this.http.get<AuthenticateResponse>(apiUrl + '/User/' + id);
    }
}
