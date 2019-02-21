import { Injectable } from '@angular/core';
import { ComicCollectionModel } from '../models/comicCollection.model';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ComicCollectionService {
    apiUrl = environment.baseApiUrl;
    constructor(
        private http: HttpClient
    ) {
    }

    getCollection(id: number): Observable<Array<ComicCollectionModel>> {
        return this.http.get<Array<ComicCollectionModel>>(this.apiUrl + '/ComicCollection/getComicsCollection/' + id);
    }

    createCollection(comicCollection: ComicCollectionModel): Observable<ComicCollectionModel> {
        return this.http.post<ComicCollectionModel>(this.apiUrl + '/ComicCollection/addToCollection', comicCollection);
    }

    deleteCollection(id: number) {
        this.http.delete(this.apiUrl + '/ComicCollection/deleteFromCollection/' + id);
    }
}
