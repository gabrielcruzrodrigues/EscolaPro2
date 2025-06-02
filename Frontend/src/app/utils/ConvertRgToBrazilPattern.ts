export function convertRgToBrazilPattern(rg: string | number): string {
  let raw = rg.toString().replace(/\D/g, '');

  if (raw.length < 7 || raw.length > 9) {
    throw new Error('RG deve conter entre 7 e 9 dígitos.');
  }

  // Preenche com zeros à esquerda até ter 9 dígitos
  raw = raw.padStart(9, '0');

  return raw.replace(/(\d{2})(\d{3})(\d{3})(\d{1})/, '$1.$2.$3-$4');
}