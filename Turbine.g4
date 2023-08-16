grammar Turbine;

turbine: section (STATEMENT_SEP section)* ;

section:
    defectSection 
    | reporterSection
    | detailsSection 
    | summarySection
    | keyValueSection
    | rootSection;

rootSection: NAME (nested)* ;

nested: NESTED (key | keyValueSection)*;
key: TEXT '=' TEXT;

NESTED: '---';
SEP: '--';
NAME: '--' TEXT;

defectSection: 
    'There is a' defectDescription 'found at' site 'and' position location (AT_SITE site)? defectProperties;

defectDescription: TEXT;
defectProperties: (defectProperty | SEPARATOR)*;

defectProperty: defectType | severity | actions | comment;

defectType: 'type:' (TEXT | STRING);
severity: 'severity:' (TEXT | STRING);
actions: 'actions:' TEXT;
comment: 'comment:' STRING;

location: ('external' | 'internal');
site: TEXT NUMBER;
position: TEXT;
timezone: ('UTC' | 'GMT' | 'EST' | 'PST');

reporterSection: 'reported by:' STRING ('date:' DATE)? ('time:' TIME)?;
detailsSection: 'Details:' STRING;
summarySection: 'Summary:' STRING;

keyValueSection: keyValueProperty+;
keyValueProperty: TEXT '=' TEXT;

DATE: NUMBER NUMBER SEPARATOR MONTH SEPARATOR NUMBER;
TIME: NUMBER NUMBER COLON NUMBER NUMBER;
NUMBER: [0-9]+;
MONTH: 'JAN' | 'FEB' | 'MAR' | 'APR' | 'MAY' | 'JUN' | 'JUL' | 'AUG' | 'SEP' | 'OCT' | 'NOV' | 'DEC';
STRING: '"' (ESC | SAFECODEPOINT)* '"';
SEPARATOR: '-';
STATEMENT_SEP: ';;;';
TEXT: [a-zA-Z]+;
COLON: ':';

AT_SITE: 'at site';

fragment ESC: '\\' (["\\/bfnrt] | UNICODE);
fragment UNICODE: 'u' HEX HEX HEX HEX;
fragment HEX: [0-9a-fA-F];
fragment SAFECODEPOINT: ~["\\\u0000-\u001F];
WS: [ \t\r\n]+ -> channel(HIDDEN);
