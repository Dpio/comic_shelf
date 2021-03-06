import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CollectionModel } from '../models/collection.model';
import { ComicCollectionModel } from '../models/comicCollection.model';
import { ComicModel } from '../models/comic.model';
import { UserModel } from '../models/user.model';

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

    deleteCollection(id: number): Observable<void> {
        return this.http.delete<void>(this.apiUrl + '/Collection/' + id);
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

    getWantListForUser(id: number): Observable<Array<CollectionModel>> {
        return this.http.get<Array<CollectionModel>>(this.apiUrl + '/Collection/getWantListForUser/' + id);
    }

    getComicCollectionByCollectionId(id: number): Observable<Array<ComicCollectionModel>> {
        return this.http.get<Array<ComicCollectionModel>>(this.apiUrl + '/Collection/getComicCollectionsByCollectionId/' + id);
    }

    findUsersWithComic(userId: number, comicId: number): Observable<Array<UserModel>> {
        return this.http.get<Array<UserModel>>(this.apiUrl + '/Collection/findUsersWithComic/' + userId + '/' + comicId);
    }

    updateCollection(collection: CollectionModel): Observable<CollectionModel> {
        return this.http.put<CollectionModel>(this.apiUrl + '/Collection', collection);
    }
}
