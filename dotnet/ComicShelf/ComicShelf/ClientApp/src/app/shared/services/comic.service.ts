import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ComicModel } from '../models/comic.model';


@Injectable()
export class ComicService {

    constructor(private http: HttpClient) {
    }

    getComics(): Observable<Array<ComicModel>> {
        const apiUrl = environment.baseApiUrl;
            return this.http.get<Array<ComicModel>>(apiUrl + '/Comic/getAll');
    }
}
