import { BaseModel } from './base.model';

export class AuthenticateResponse extends BaseModel {
    id: number | undefined;
    givenName: string | undefined;
    email: string | undefined;
    token: string | undefined;
}
