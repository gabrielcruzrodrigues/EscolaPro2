export type Companie = {
    id: number,
    name: string,
    connectionString: string,
    cnpj: string,
    status: boolean,
    createdAt: string,
    lastUpdatedAt: string
}

export type CreateCompanie = {
    name: string,
    connectionString: string,
    cnpj: string
}

export type ErrorResponseCreateCompanie = {
    error: {
        message: string,
        type: string,
        code: number
    }
}