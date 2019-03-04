import { BaseModel } from './base.model';
import { CollectionModel } from './collection.model';
import { ComicModel } from './comic.model';

export class ComicCollectionModel extends BaseModel {
    id: number;
    collectionId: number;
    comicId: number;
    comic: ComicModel;
    collection: CollectionModel;
}
