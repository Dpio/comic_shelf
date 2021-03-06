import { BaseModel } from './base.model';
import * as moment from 'moment';

export class UpdateRentModel extends BaseModel {
    id: number;
    giverId: number;
    receiverId: number;
    status: RentStatus;
    comicId: number;
    startDate: moment.Moment;
    endDate: moment.Moment;
}

export enum RentStatus {
    pending = 1,
    nProgress,
    complete,
    pendingNew
}
