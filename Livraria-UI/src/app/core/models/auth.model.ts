import { BaseModel } from "./base.model";

export interface IUsuario extends BaseModel {
    nome: string;
    role: string;
}

export interface ILoginResponse {
    token: string;
    usuario: IUsuario;
}