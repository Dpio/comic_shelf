import { BaseModel } from './base.model';
import { CollectionModel } from './collection.model';
import { ComicCollectionModel } from './comicCollection.model';

export class UserCollectionModel extends BaseModel {
    id: number;
    collectionId: number;
    comicCollectionId: number;
    collections: Array<CollectionModel>;
    comicCollections: Array<ComicCollectionModel>;
}
