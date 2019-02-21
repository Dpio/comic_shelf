import { BaseModel } from './base.model';

export class CollectionModel extends BaseModel {
    name: string;
    description: string;
    isPublic: boolean;
    id: number;
}
