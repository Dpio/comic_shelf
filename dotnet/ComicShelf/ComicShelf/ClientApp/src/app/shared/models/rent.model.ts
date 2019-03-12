import { BaseModel } from './base.model';
import moment = require('moment');
import { UserModel } from './user.model';
import { ComicModel } from './comic.model';

export class RentModel extends BaseModel {
    id: number;
    giverId: number;
    receiverId: number;
    status: RentStatus;
    comicId: number;
    startDate: moment.Moment;
    endDate: moment.Moment;
    comic: ComicModel;
    giver: UserModel;
    receiver: UserModel;

    deserializee(input: any): RentModel {
        Object.assign(this, input);
        this.comic = new ComicModel().deserialize(input.comic);
        this.giver = new UserModel().deserialize(input.giver);
        this.receiver = new UserModel().deserialize(input.receiver);
        return this;
    }
}

export enum RentStatus {
    pending = 1,
    inProgress,
    complete
}
