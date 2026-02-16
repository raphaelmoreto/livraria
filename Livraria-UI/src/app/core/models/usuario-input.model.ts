export interface IUsuarioInput {
    nome: string | null;
    usuario: string | null;
    email: string | null;
    senha: string | null;
    fk_perfil: number | null;
}