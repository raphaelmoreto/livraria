import { BaseModel } from "src/app/core/models/base.model";

export interface ILivro extends BaseModel {
    titulo: string;
    isbn: string;
    dt_Publicacao: Date;
    preco: number;
    quantidade: number;
    categorias: string;
    subtitulo: string | null;
    autor: string | null;
};