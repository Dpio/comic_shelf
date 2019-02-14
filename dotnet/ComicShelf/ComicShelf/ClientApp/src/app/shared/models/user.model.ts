import { BaseModel } from './base.model';
import { ComicCollectionModel } from './comicCollection.model';

export class UserModel extends BaseModel {
    GoogleId: string;
    Name: string;
    GivenName: string;
    Email: string;
    Picture: string;
    ComicsCollections: Array<ComicCollectionModel>;
}

