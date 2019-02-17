import { BaseModel } from './base.model';
import { ComicCollectionModel } from './comicCollection.model';
import * as moment from 'moment';

export class ComicModel extends BaseModel {
    id: number;
    title: string;
    issue: string;
    volume: number;
    publisher: string;
    series: string;
    scriptWriter: string;
    draftsman: string;
    translator: string;
    cover: string;
    premierDate: moment.Moment;
    note: string;
    image: File;
    ComicsCollections: Array<ComicCollectionModel>;
}
