export function familyTypeConvertInString(type: number): string {
     switch(type) {
          case 0:
               return "PAI";
          case 1:
               return "MÃE";
          default:
               return "Tipo desconhecido"
     }
}