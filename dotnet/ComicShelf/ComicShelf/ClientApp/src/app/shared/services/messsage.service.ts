import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { MessageModel } from '../models/message.model';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class MessageService {
    apiUrl = environment.baseApiUrl;

    constructor(
        private http: HttpClient
    ) {
    }

    BroadcastMessageForUser(message: MessageModel): Observable<string> {
        return this.http.post<string>(this.apiUrl + '/Message/PostForUser', message);
    }
}
