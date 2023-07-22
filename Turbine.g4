grammar Turbine;



turbine: defect* reporter? 
(details | summary)?;
defect:
    'There is a' TEXT 'found at'
     site 'and' postition
    location (AT_SITE site)?
    (defectType)?
    (severity)?
    (actions)?
    (comment)?
  ;

defectType: 'type:' (TEXT | STRING);


severity: 'severity:' (TEXT | STRING);
actions: 'actions:' TEXT;
comment: 'comment:' STRING;
location: ('external' | 'internal') 

TEXT*;
site: TEXT NUMBER;
postition:TEXT;
timezone: ('UTC' | 'GMT' | 'EST' | 'PST');

reporter: 'reported by:' STRING
    ('date:' DATE)?
    ('time:' TIME)?
    ;

details: 'Details:' STRING;
summary: 'Summary:' STRING;

YEAR: NUMBER NUMBER NUMBER NUMBER;
MONTH: JAN | FEB | MAR | APR | MAY | JUN | JUL | AUG | SEP | OCT | NOV | DEC;

THERE_IS_A: 'There is a';
FOUND_ON: 'found on';
AT_SITE: 'at site';
AT_DATE: 'at date';
AT_TIME: 'at time';

SEPARATOR: '-';
COLON: ':';

TEXT: [a-zA-Z]+;
STRING: '"' (ESC | SAFECODEPOINT)* '"';
DATE: NUMBER NUMBER 
SEPARATOR MONTH SEPARATOR NUMBER;
TIME: NUMBER NUMBER COLON NUMBER NUMBER;

JAN: 'JAN';
FEB: 'FEB';
MAR: 'MAR';
APR: 'APR';
MAY: 'MAY';
JUN: 'JUN';
JUL: 'JUL';
AUG: 'AUG';
SEP: 'SEP';
OCT: 'OCT';
NOV: 'NOV';
DEC: 'DEC';

NUMBER: [0-9]+;


fragment ESC: '\\' (["\\/bfnrt] | UNICODE);
fragment UNICODE: 'u' HEX HEX HEX HEX;
fragment HEX: [0-9a-fA-F];
fragment SAFECODEPOINT: ~["\\\u0000-\u001F];

WS: [ \t\r\n]+ -> channel(HIDDEN);