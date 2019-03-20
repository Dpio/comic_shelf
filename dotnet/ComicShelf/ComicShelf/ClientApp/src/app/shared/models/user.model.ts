import { BaseModel } from './base.model';
import { CollectionModel } from './collection.model';

export class UserModel extends BaseModel {
    id: number;
    googleId: string;
    name: string;
    givenName: string;
    email: string;
    picture: string;
    collections: Array<CollectionModel>;
    isSelected = false;
}

