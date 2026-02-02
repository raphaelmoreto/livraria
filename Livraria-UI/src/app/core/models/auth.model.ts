import { BaseModel } from "./base.model";

export interface Usuario extends BaseModel {
    tipo: string;
}

export interface LoginResponse {
    token: string;
    usuario: Usuario;
}