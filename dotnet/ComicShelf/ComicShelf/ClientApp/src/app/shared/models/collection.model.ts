import { BaseModel } from './base.model';
import { ComicModel } from './comic.model';
import { ComicCollectionModel } from './comicCollection.model';

export class CollectionModel extends BaseModel {
    name: string;
    description: string;
    isPublic: boolean;
    isWantList: boolean;
    userId: number;
    id: number;
    comicsCollection: Array<ComicCollectionModel>;
}
