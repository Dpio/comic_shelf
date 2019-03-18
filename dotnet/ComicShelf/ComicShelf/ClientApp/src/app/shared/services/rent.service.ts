import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { RentModel } from '../models/rent.model';
import { Observable } from 'rxjs/Observable';
import { UpdateRentModel } from '../models/updateRent.model';

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

    putRent(rent: UpdateRentModel): Observable<UpdateRentModel> {
        return this.http.put<UpdateRentModel>(this.apiUrl + '/Rent', rent);
    }

    deleteRent(id: number): Observable<void> {
        return this.http.delete<void>(this.apiUrl + '/Rent/' + id);
    }
    getRentRequestsCount(id: number): Observable<number> {
        return this.http.get<number>(this.apiUrl + '/Rent/GetNewRentRequestsCount/' + id);
    }

    GetPendingRequestsCountForComicByUser(userId: number, comicId: number): Observable<Array<RentModel>> {
        return this.http.get<Array<RentModel>>(this.apiUrl + '/Rent/GetPendingRequestsCountForComicByUser/' + userId + '/' + comicId);
    }
}
