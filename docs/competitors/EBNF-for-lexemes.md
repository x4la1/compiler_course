## ISO 7185:1990 (Pascal Standard)
В этом файле описаны правила Extended Backus-Naur Form для некоторых лексем языка Pascal  
[Ссылка на стандарт](http://www.msiit.ru/doc/ecma/doc-iso1990-007185e.pdf)

## Описанные лексемы:
- идентификатор
- литерал целого числа
- литерал вщественного числа
- литерал строки 

## EBNF для идентификатора
В Pascal идентификатор начинается с буквы, а затем может содержать буквы и цифры 

```ebnf
identifier = letter, { letter | digit };

letter = "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K" | "L" | "M"
       | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V" | "W" | "X" | "Y" | "Z"
       | "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m"
       | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z" ;

digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;
```

## EBNF для литерала целого числа

```ebnf
integer = unsigned_integer | signed_integer;

unsigned_integer = digit_sequence;

signed_integer = [sign], unsigned_integer;

sign = "+" | "-";

digit_sequence = digit, {digit};
```

## EBNF для литерала числа с плавающей запятой
  
```ebnf
real = unsigned_real | signed_real;

unsigned_real = digit_sequence, ".", fractional_part, ["e", scale_factor] 
                | digit_sequence, "e", scale_factor 
                | ".", fractional_part, ["e", scale_factor];

signed_real = [sign], unsigned_real;

scale_factor = [sign], digit_sequence;

fractional_part = digit_sequence;
```

## EBNF для литералов строк

```ebnf
string_literal = "'", string_element, {string_element}, "'";

string_element = apostrophe_image | string_character;

apostrophe_image = "''";

string_character = ? one-of-a-set-of-implementation-defined-characters ?
```












