export interface Persona {
    id?: string | null | undefined;
    nombreCompleto?: string | null | undefined;
    identificacion?: string | null | undefined;
    edad?: number | null | undefined;
    genero?: string | null | undefined;
    estaActiva?: boolean | null | undefined;
    atributosAdicionales?: string | null | undefined;
    maneja?: boolean | null | undefined;
    usaLentes?: boolean | null | undefined;
    diabetico?: boolean | null | undefined;
    padeceOtraEnfermedad?: boolean | null | undefined;
    cualOtraEnfermedad?: string | null | undefined;
  }