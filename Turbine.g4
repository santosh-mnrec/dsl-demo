grammar Turbine;

turbine: section (STATEMENT_SEP section)* ;

section:
    defectSection
    | reporterSection
    | detailsSection 
    | summarySection
    | keyValueSection
    | objectSections;

defectSection: 'CREATE DEFECT' defectDescription siteDefect positionDefect locationDefect detailsSection;

defectDescription: 'DESCRIPTION' STRING;
siteDefect: 'SITE' TEXT;
positionDefect: 'POSITION' TEXT;
locationDefect: 'LOCATION' TEXT;
detailsSection: 'DETAILS ARE' detail+;

detail: ('TYPE' TEXT) | ('SEVERITY' TEXT) | ('ACTIONS' TEXT) | ('COMMENT' STRING);
timezone: ('UTC' | 'GMT' | 'EST' | 'PST');

reporterSection: 'reported by:' STRING ('date:' DATE)? ('time:' TIME)?;

summarySection: 'Summary:' STRING;

keyValueSection: keyValueProperty+;
keyValueProperty: TEXT '=' TEXT;

objectSections: NAME (prop)* ;

prop: MULTI_LEVEL (key | keyValueSection)*;
key: TEXT '=' TEXT;

CREATE: 'Create defect';
FOUND: 'for site';
AND: 'and';
NAME: '--' TEXT;
WHERE: 'where';
WITH: 'details are';
DATE: NUMBER '-' MONTH '-' NUMBER;
TIME: NUMBER ':' NUMBER;
NUMBER: [0-9]+;
MONTH: 'JAN' | 'FEB' | 'MAR' | 'APR' | 'MAY' | 'JUN' | 'JUL' | 'AUG' | 'SEP' | 'OCT' | 'NOV' | 'DEC';
STRING: '"' (ESC | SAFECODEPOINT)* '"';
SEPARATOR: '-';
STATEMENT_SEP: ';;;';
TEXT: [a-zA-Z0-9]+;
COLON: ':';

MULTI_LEVEL: '---';
SEP: '--';

fragment ESC: '\\' (["\\/bfnrt] | UNICODE);
fragment UNICODE: 'u' HEX HEX HEX HEX;
fragment HEX: [0-9a-fA-F];
fragment SAFECODEPOINT: ~["\\\u0000-\u001F];
WS: [ \t\r\n]+ -> channel(HIDDEN);
