export class BaseModel {
    public deserialise(obj: any) {
        Object.assign(this, obj);
    }
}
