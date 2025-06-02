export function convertFamilyEnumSexToString(sex: number): string {
     switch(sex) {
          case 0:
               return "MASCULINO";
          case 1: 
               return "FEMININO";
          default:
               return "DEFAULT";
     }
}