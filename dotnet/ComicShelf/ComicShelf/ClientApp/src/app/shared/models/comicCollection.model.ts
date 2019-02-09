import { BaseModel } from './base.model';
import { ComicModel } from './comic.model';
import { UserModel } from './user.model';

export class ComicCollectionModel extends BaseModel {
    ComicId: number;
    UserId: number;
    User: UserModel;
    Comic: ComicModel;
}
