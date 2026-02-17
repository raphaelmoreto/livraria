import { BaseModel } from "./base.model";

export interface IUsuario extends BaseModel {
    tipo: string;
}

export interface ILoginResponse {
    token: string;
    usuario: IUsuario;
}