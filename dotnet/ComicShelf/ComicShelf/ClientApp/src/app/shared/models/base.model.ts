export class BaseModel {
    public deserialise(obj: any) {
        Object.assign(this, obj);
    }
    public deserialize(input: any) {
        Object.assign(this, input);
        return this;
      }
}
