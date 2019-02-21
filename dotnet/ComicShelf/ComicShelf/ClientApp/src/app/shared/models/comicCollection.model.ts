import { BaseModel } from './base.model';
import { ComicModel } from './comic.model';
import { UserModel } from './user.model';

export class ComicCollectionModel extends BaseModel {
    id: number;
    comicId: number;
    userId: number;
    user: UserModel;
    comic: ComicModel;
}
