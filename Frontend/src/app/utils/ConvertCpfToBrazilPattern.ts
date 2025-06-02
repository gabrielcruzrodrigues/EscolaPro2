export function convertCpfToBrazilPattern(cpf: string | number): string {
  const raw = cpf.toString().replace(/\D/g, '');

  if (raw.length !== 11) {
    throw new Error('CPF deve conter exatamente 11 dígitos.');
  }

  return raw.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
}