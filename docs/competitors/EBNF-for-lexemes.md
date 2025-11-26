## ISO/IEC 14977 (C++ Standard)
В этом файле описаны правила Extended Backus-Naur Form для некоторых лексем языка C++

## Описанные лексемы:
- идентификатор
- литерал целого числа
- литерал вщественного числа
- литерал строки

## EBNF для идентификатора
В C++ идентификатор начинается с буквы или символа подчёркивания, а затем может содержать буквы, цифры и подчёркивания
```ebnf
identifier = identifier_start, { identifier_part } ;

identifier_start = letter | "_" ;

identifier_part = letter | digit | "_" ;

letter = "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K" | "L" | "M"
       | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V" | "W" | "X" | "Y" | "Z"
       | "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m"
       | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z" ;

digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;
```

## EBNF для литерала целого числа
Целочисленный литерал может быть в десятичной, восьмеричной, шестнадцатеричной или двоичной системе.  
Также может быть знаковым или беззнаковым.

```ebnf
integer = signed_integer | unsigned_integer ;

sign = "+" | "-" ;

signed_integer = [sign], unsigned_integer ;

unsigned_integer = integer_literal ;

integer_literal = decimal_literal | octal_literal | hexadecimal_literal | binary_literal ;

digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;

decimal_literal = "0" | non_zero_digit, { ["'"], digit } ; 

octal_literal = "0", { octal_digit } ;

hexadecimal_literal = hexadecimal_prefix, hexadecimal_digit, { hexadecimal_digit } ;

binary_literal = binary_prefix, binary_digit, { binary_digit } ;

hexadecimal_prefix = "0x" | "0X" ;

binary_prefix = "0b" | "0B" ;

non_zero_digit = "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;

octal_digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" ;

hexadecimal_digit = digit | "A" | "B" | "C" | "D" | "E" | "F"
                          | "a" | "b" | "c" | "d" | "e" | "f" ;

binary_digit = "0" | "1" ;
```

## EBNF для литерала числа с плавающей запятой
Число с плавающей запятой может быть записано в десятичной или экспоненциальной форме
```ebnf
floating_literal = decimal_floating_literal | hexadecimal_floating_literal ;

decimal_floating_literal = ( fractional_constant, [ exponent_part ], [ floating_suffix ] )
                         | ( digit_sequence, exponent_part, [ floating_suffix ] ) ;

fractional_constant = [ digit_sequence ], ".", digit_sequence    (* "123." и ".456" валидны в C++ *)
                    | digit_sequence, "." ;                      (* "123." = 123.0, ".456" = 0.456 *)
                                                                 (* узнал об этом когда писал md *)

exponent_part = ( "e" | "E" ), [ "+" | "-" ], digit_sequence ;

digit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;

digit_sequence = digit, { digit } ;

floating_suffix = "f" | "l" | "F" | "L" ;

hexadecimal_floating_literal = hexadecimal_prefix,
                               ( hexadecimal_fractional_constant | hexadecimal_digit_sequence ),
                               binary_exponent_part,
                               [ floating_suffix ] ;

hexadecimal_fractional_constant = [ hexadecimal_digit_sequence ], ".", hexadecimal_digit_sequence
                                | hexadecimal_digit_sequence, "." ;

binary_exponent_part = ( "p" | "P" ), [ "+" | "-" ], digit_sequence ;

hexadecimal_digit = digit | "A" | "B" | "C" | "D" | "E" | "F"
                          | "a" | "b" | "c" | "d" | "e" | "f" ;

hexadecimal_digit_sequence = hexadecimal_digit, { hexadecimal_digit } ;
```
## EBNF для литералов строк
Строковый литерал может быть обычным или с префиксом (например, `u8`, `u`, `U`, `L`). Также поддерживаются raw-строки (`R"(...)"`)
```ebnf
string_literal = [ encoding_prefix ], [ "R", raw_string ] | ordinary_string ;

encoding_prefix = "u8" | "u" | "U" | "L" ;

ordinary_string = """", { string_character }, """ ;

string_character = ~ (""" | "\" | newline) | escape_sequence ;

raw_string = "(", raw_string_delimiter, ")", { raw_character }, "(", raw_string_delimiter, ")" ;

raw_string_delimiter = { raw_delimiter_char } ;

raw_delimiter_char = letter | digit | "_" ;

raw_character = ? любой символ, кроме последовательности )delimiter" ? ;

escape_sequence =
    "\" , ( """ | "'" | "\" | "?" | "a" | "b" | "f" | "n" | "r" | "t" | "v"
    | octal_escape | hexadecimal_escape | universal_character_name ) ;

octal_escape = "\" , octal_digit, [ octal_digit, [ octal_digit ] ] ;

hexadecimal_escape = "\x", hexadecimal_digit, { hexadecimal_digit } ;

universal_character_name = "\u", hexadecimal_digit, hexadecimal_digit, hexadecimal_digit, hexadecimal_digit
                         | "\U", 8 * hexadecimal_digit ;
```












