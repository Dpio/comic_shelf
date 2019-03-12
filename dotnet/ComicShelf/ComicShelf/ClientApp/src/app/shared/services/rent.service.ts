import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { RentModel } from '../models/rent.model';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class RentService {
    apiUrl = environment.baseApiUrl;

    constructor(
        private http: HttpClient
    ) {
    }

    createRent(rent: RentModel): Observable<RentModel> {
        return this.http.post<RentModel>(this.apiUrl + '/Rent', rent);
    }

    GetRentsForUser(id: number): Observable<Array<RentModel>> {
        return this.http.get<Array<RentModel>>(this.apiUrl + '/Rent/GetRentsForUser/' + id);
    }

    putRent(rent: RentModel): Observable<RentModel> {
        return this.http.put<RentModel>(this.apiUrl + '/Rent', rent);
    }

    deleteRent(id: number): Observable<void> {
        return this.http.delete<void>(this.apiUrl + '/Rent/' + id);
    }
}
