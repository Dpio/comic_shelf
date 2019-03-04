import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CollectionModel } from '../models/collection.model';
import { ComicCollectionModel } from '../models/comicCollection.model';
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

    getCollectionsForUser(id: number): Observable<Array<CollectionModel>> {
        return this.http.get<Array<CollectionModel>>(this.apiUrl + '/Collection/getCollectionsForUser/' + id);
    }

    addComicToCollection(comicCollection: ComicCollectionModel): Observable<ComicCollectionModel> {
        return this.http.post<ComicCollectionModel>(this.apiUrl + '/Collection/addComicToCollection', comicCollection);
    }

    getComicsInCollection(id: number): Observable<Array<ComicModel>> {
        return this.http.get<Array<ComicModel>>(this.apiUrl + '/Collection/getComicsInCollection/' + id);
    }

    deleteComicFromCollection(id: number): Observable<void> {
       return this.http.delete<void>(this.apiUrl + '/Collection/deleteComicFromCollection/' + id);
    }

    getComicCollection(comicId: number, collectionId: number): Observable<ComicCollectionModel> {
        return this.http.get<ComicCollectionModel>(this.apiUrl + '/Collection/getComicCollection/' + comicId + '/' + collectionId);
    }

    getCollectionByName(name: string, userId: number): Observable<CollectionModel> {
        return this.http.get<CollectionModel>(this.apiUrl + '/Collection/getCollectionByName/' + name + '/' + userId);
    }
}
