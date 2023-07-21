grammar Turbine;

turbine: defect*;
defect: 'Defect:' defectDescription defectDetails;
defectDescription: (STRING | TEXT) 'at' sitePosition; // Allow both STRING and TEXT for defectDescription
defectDetails: '{' detailSection* '}';
detailSection: keyValuePair;
keyValuePair: key ':' value;
key: TEXT; // Use the updated TEXT rule instead of KEYWORD
value: (STRING | TEXT | NUMBER | DATE | TIME); // Allow both STRING and TEXT for value

sitePosition: 'SITE' (NUMBER | TEXT) ',' 'Position' (NUMBER | TEXT); // Update sitePosition rule to allow NUMBER or TEXT

STRING: '"' (~["\\\r\n])* '"';
NUMBER: [0-9]+;
DATE: NUMBER NUMBER SEPARATOR MONTH SEPARATOR NUMBER;
TIME: NUMBER NUMBER COLON NUMBER NUMBER;

SEPARATOR: '-';
COLON: ':';
TEXT: [a-zA-Z]+[0-9]*; // Allow single words with optional numbers

MONTH: 'JAN' | 'FEB' | 'MAR' | 'APR' | 'MAY' | 'JUN' | 'JUL' | 'AUG' | 'SEP' | 'OCT' | 'NOV' | 'DEC';

WS: [ \t\r\n]+ -> channel(HIDDEN);
