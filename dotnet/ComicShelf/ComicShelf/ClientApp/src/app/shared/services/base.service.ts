import { BaseModel } from '../models/base.model';

export class BaseApiService {

    static getObjectArrayFromApi<T extends BaseModel>(arr: any[], type: new () => T): Array<T> {
        const value: Array<T> = [];
        arr.forEach(element => {
            const object: T = Object.create(type.prototype);
            object.deserialise(element);
            value.push(object);
        });
        return value;
    }
}
