import { BaseModel } from './base.model';

export class AuthenticateResponse extends BaseModel {
    id: number | undefined;
    username: string | undefined;
    emailAddress: string | undefined;
    token: string | undefined;
}
