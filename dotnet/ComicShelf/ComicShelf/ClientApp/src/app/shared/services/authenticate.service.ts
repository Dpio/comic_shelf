import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthenticateService {

    constructor(
        private http: HttpClient
        ) {
    }

    signInWithGoogle(): Observable<string> {
        const apiUrl = environment.baseApiUrl;
        return this.http.post(apiUrl + '/Authentication/signInWithGoogle', {}, { responseType: 'text' });
    }

    logout() {
        localStorage.removeItem('currentUser');
    }
}
