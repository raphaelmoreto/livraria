export type ResponseType = 'success' | 'error' | 'warning';

export interface IApiResponse {
    tipo: ResponseType;
    success: boolean;
    mensagem: string;
}