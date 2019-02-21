import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CollectionModel } from '../models/collection.model';

@Injectable()
export class CollectionService {
    apiUrl = environment.baseApiUrl;
    constructor(
        private http: HttpClient
    ) {
    }

    getCollection(id: number): Observable<CollectionModel> {
        return this.http.get<CollectionModel>(this.apiUrl + '/Collection/' + id);
    }

    createCollection(collection: CollectionModel): Observable<CollectionModel> {
        return this.http.post<CollectionModel>(this.apiUrl + '/Collection', collection);
    }

    deleteCollection(id: number) {
        this.http.delete(this.apiUrl + '/Collection/' + id);
    }

}
