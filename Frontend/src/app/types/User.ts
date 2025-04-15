export type CreateUser = {
     name: string,
     email: string,
     password: string,
     role: number
}

export type UpdateUser = {
     id: number,
     name: string,
     email: string,
     password: string,
     role: number
}

export type ResponseCreateUser = {
     name: string,
     email: string,
     password: string,
     role: number,
     createdAt: string,
     lastUpdatedAt: string
     lastAccess: string
}

export type User = {
     id: number,
     name: string,
     email: string,
     password: string,
     role: number,
     createdAt: string,
     lastUpdatedAt: string
     lastAccess: string
}

export type ErrorResponseCreateUser = {
     error: {
          message: string,
          type: string,
          code: number
     }
}