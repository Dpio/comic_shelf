import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserCollectionModel } from '../models/userCollection.model';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';

@Injectable()
export class UserCollectionService {
    apiUrl = environment.baseApiUrl;
    constructor(
        private http: HttpClient
    ) {
    }

    getCollection(id: number): Observable<Array<UserCollectionModel>> {
        return this.http.get<Array<UserCollectionModel>>(this.apiUrl + '/UserCollection/getUserCollection/' + id);
    }

    createCollection(userCollection: UserCollectionModel): Observable<UserCollectionModel> {
        return this.http.post<UserCollectionModel>(this.apiUrl + '/UserCollection/addToUserCollection', userCollection);
    }

    deleteCollection(id: number) {
        this.http.delete(this.apiUrl + '/UserCollection/deleteCollectionFromUserCollection/' + id);
    }
}
