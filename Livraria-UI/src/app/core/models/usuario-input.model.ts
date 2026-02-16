export interface IUsuarioInput {
    nome: string | null;
    email: string | null;
    senha: string | null;
    fk_perfil: number | null;
}