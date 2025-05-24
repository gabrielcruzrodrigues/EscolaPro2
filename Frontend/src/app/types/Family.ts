export type Family = {
     id: number,
     address: string,
     cep: string,
     city: string,
     cpf: string,
     cpfFile: null,
     cpfFilePath: string,
     dateOfBirth: string
     email: string
     financialFile: null,
     proofOfResidenceFilePath: string,
     homeNumber: string
     image: null
     name: string
     nationality: string
     naturalness: string
     neighborhood: string
     phone: string
     rg: string
     rgDispatched: string
     rgDispatchedDate: string
     rgFile: null,
     rgFilePath: string
     sex: string
     state: string
     type: string,
     createdAt: string
     studentName: string
}

export type CreateFamily = {
     address: string,
     cep: string,
     city: string,
     cpf: string,
     cpfFile: null,
     dateOfBirth: string
     email: string
     financialFile: null
     homeNumber: string
     image: null
     name: string
     nationality: string
     naturalness: string
     neighborhood: string
     phone: string
     rg: string
     rgDispatched: string
     rgDispatchedDate: string
     rgFile: null
     sex: string
     state: string
     type: string
}