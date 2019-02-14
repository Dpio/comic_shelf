import { BaseModel } from './base.model';
import { ComicCollectionModel } from './comicCollection.model';

export class ComicModel extends BaseModel {
    id: number;
    title: string;
    issue: string;
    publisher: string;
    startDate: Date;
    endDate: Date;
    note: string;
    image: File;
    ComicsCollections: Array<ComicCollectionModel>;
}
