import { BaseModel } from './base.model';
import { CollectionModel } from './collection.model';

export class UserModel extends BaseModel {
    GoogleId: string;
    Name: string;
    GivenName: string;
    Email: string;
    Picture: string;
    Collections: Array<CollectionModel>;
}

