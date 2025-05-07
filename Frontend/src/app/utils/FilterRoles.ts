import { Role } from "../types/Role";

export function filterRoles(roles: Role[]): Role[] {
     const newRoles: Role[] = [];
     const rolesForExclude: string[] = [
          '_MASTER'
     ]

     roles.forEach(role => {
          if (!rolesForExclude.some(p => role.name.includes(p))) {
               newRoles.push(role);
          }
     });

     return renameRoles(newRoles);
}

function renameRoles(roles: Role[]): Role[] {
     const roleNamesForRename: Record<string, string> = {
          'ADMINISTRACAO' : 'ADMINISTRAÇÃO'
     };

     return roles.map(role => {
          const matched = Object.keys(roleNamesForRename).find(key => role.name.includes(key));
          if (matched) {
               return { ...role, name: roleNamesForRename[matched] }
          }
          return role;
     })
}