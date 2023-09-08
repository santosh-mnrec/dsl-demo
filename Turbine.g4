grammar Turbine;

turbine: section (STATEMENT_SEP 
section)* ;

section:
    defectStatement
  
    | keyValueSection?
    | objectSections
 
    ;
defectStatement:
    'Create Defect with' defectBlock ('where' reporterClause)?;

defectBlock:
    '{'
        (descriptionProperty | siteProperty | positionProperty | locationProperty | dateProperty | timeProperty | detailsProperty)*
    '}';

descriptionProperty: 'Description' STRING;
siteProperty: 'Site' STRING;
positionProperty: 'Position' STRING;
locationProperty: 'Location' STRING;
dateProperty: 'Date' STRING; // You may want to refine the DATE format as needed
timeProperty: 'Time' STRING; // You may want to refine the TIME format as needed
detailsProperty: 'Details:' detailProperty+;

detailProperty:
    'Type' STRING
    | 'Severity' NUMBER
    | 'Actions' STRING
    | 'Comment' STRING
    | 'Failure Mode' STRING;

reporterClause:
    'Reporter' STRING;
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