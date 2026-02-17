import { BaseModel } from "src/app/core/models/base.model";

export type ToastType = 'success' | 'error' | 'warning';

export interface IToast extends BaseModel {
    mensagem: string;
    type: ToastType;
}