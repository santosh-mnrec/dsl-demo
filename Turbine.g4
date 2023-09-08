grammar Turbine;

turbine: section (STATEMENT_SEP 
section)* ;

section:
    defectSection
    | reporterSection?
    | detailsSection? 
    | summarySection?
    | keyValueSection?
    | objectSections
 
    ;

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

objectSections: objectSection*;

objectSection: '#' TEXT 
(keyValueProperty | child)*;


child: '--' TEXT
(keyValueProperty)*;

keyValueSection: keyValueProperty+;
keyValueProperty:'+'? TEXT '=' TEXT;



CREATE: 'Create defect';
FOUND: 'for site';
AND: 'and';

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
PARENT:'#';
CHILD: '~';
SUBCHILD: '--';
MULTI_LEVEL: '---';


fragment ESC: '\\' (["\\/bfnrt] | UNICODE);
fragment UNICODE: 'u' HEX HEX HEX HEX;
fragment HEX: [0-9a-fA-F];
fragment SAFECODEPOINT: ~["\\\u0000-\u001F];
WS: [ \t\r\n]+ -> channel(HIDDEN);