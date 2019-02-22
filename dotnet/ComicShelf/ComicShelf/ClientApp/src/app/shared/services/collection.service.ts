import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CollectionModel } from '../models/collection.model';
import { ComicModel } from '../models/comic.model';

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

    getUserCollectionNames(id: number): Observable<Array<String>> {
        return this.http.get<Array<String>>(this.apiUrl + '/Collection/getUserCollectionNames/' + id);
    }

    getCollectionByName(name: String): Observable<CollectionModel> {
        return this.http.get<CollectionModel>(this.apiUrl + '/Collection/getCollectionByName/' + name);
    }

    getComics(collectionId: number, userId: number): Observable<Array<ComicModel>> {
        return this.http.get<Array<ComicModel>>(this.apiUrl + '/Collection/getComics/' + collectionId + '/' + userId);
    }
}
