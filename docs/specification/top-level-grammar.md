# Грамматика верхнего уровня языка Init

## Примеры кода

### Пример кода
```
float x = 10;
print(x);
input(x);
print(x);
```

```
float a;
float b = 5;
a = b + 3;
print(a);
```
Функция, ветвление и цикл
```
func abs(x: float): float {
    if (x < 0) {
        return -x;
    } else {
        return x;
    }
}

float n;
input(n);
float i = 1;
float sum = 0;
while (i <= n) {
    sum = sum + i;
    i = i + 1;
    if (sum > 100) {
        break;
    }
}
print("Sum:", sum);
```
Цикл с постусловием и switch
```
float x = 0;
repeat {
    x = x + 1;
    if (x == 3) break;
} until (x >= 5);

switch (x) {
    case 1: print("One"); break;
    case 2: print("Two"); break;
    case 3: print("Three");
    case 4: print("Four"); break;
    default: print("Other");
}
```

---

## Ключевые особенности языка

- Язык **императивный**.
- Поддерживается один числовой тип: **float**.
- Ветвления (if/else) и циклы (while, repeat) — это инструкции.
- Поддерживаются пользовательские функции, возвращающие float.
- Проблема висячего else решена: else всегда связывается с ближайшим if без else (достигается структурой грамматики).
- switch работает без автоматического break: возможен fall-through.
- Программа состоит из **последовательности инструкций** на верхнем уровне.
- Нет объявления `main`, `PROGRAM` или точки входа — выполнение начинается с первой инструкции.
- **Переменные объявляются явно**: `тип имя [= выражение];`
- **Константы** `Pi` и `Euler` встроены и доступны глобально.
- **Инструкции ввода-вывода**: `print(...)` и `input(variable)`.
- Блоки кода обрамляются фигурными скобками `{ }`.
- Присваивание **не является выражением**, а только инструкцией.
- **Любое выражение не может быть отдельной инструкцией**, если оно не вызывает побочных эффектов (например, `print(...)` разрешён, `x + 1` — нет).

---

## Семантические правила

1. **Объявление переменной** должно происходить **до её использования** в той же области видимости.
2. **Повторное объявление** переменной с тем же именем **в одной области видимости запрещено**.
3. **Функция** должна быть объявлена до вызова.
4. В `switch` выражение и метки `case` должны быть числовыми.
5. `break` можно использовать только внутри циклов или switch.
6. В `return` должно быть выражение (возврат значения обязателен).
7. Функции должны иметь хотя бы один `return`.
8. Область видимости:
   - **Глобальная** — для переменных, объявленных на верхнем уровне.
   - **Локальная** — для переменных внутри блока `{ ... }`.
9. **Инструкция `input`** принимает **только идентификатор** (нельзя писать `input(x + 1)`).
10. **Инструкция `print`** принимает **список выражений**, разделённых запятыми.
11. Тип переменной **определяется при объявлении** и **не может меняться**.
12. Инструкции завершаются точкой с запятой `;`.

---

## Грамматика в нотации EBNF
```
(* Программа = последовательность инструкций на верхнем уровне *)
program = { top_level_item } ;

top_level_item = function_declaration
               | statement ;

(* Объявление функции *)
function_declaration = "func" , identifier , "(" , [ parameter_list ] , ")" , ":" , return_type , block ;

parameter_list = parameter , { "," , parameter } ;
parameter = identifier , ":" , variable_type ;  (* void не допускается в параметрах *)

(* Инструкция *)
statement = 
      variable_declaration
    | assignment_statement
    | print_statement
    | input_statement
    | if_statement
    | while_statement
    | repeat_statement
    | switch_statement
    | return_statement
    | break_statement
    | ";" ;  (* пустая инструкция *)

(* Объявление переменной — void запрещён *)
variable_declaration = variable_type , identifier , [ "=" , expression ] , ";" ;

(* Типы для переменных *)
variable_type = "float" | "bool" | "string" ;

(* Типы для возврата функции *)
return_type = "float" | "bool" | "string" | "void" ;

(* Присваивание *)
assignment_statement = identifier , "=" , expression , ";" ;

(* Вывод *)
print_statement = "print" , "(" , [ expression , { "," , expression } ] , ")" , ";" ;

(* Ввод *)
input_statement = "input" , "(" , identifier , ")" , ";" ;

(* Условный оператор *)
if_statement = "if" , "(" , expression , ")" , statement , [ "else" , statement ] ;

(* Цикл с предусловием *)
while_statement = "while" , "(" , expression , ")" , statement ;

(* Цикл с постусловием *)
repeat_statement = "repeat" , block , "until" , "(" , expression , ")" ;

(* Switch *)
switch_statement = "switch" , "(" , expression , ")" , "{" , { case_clause } , [ default_clause ] , "}" ;

case_clause = "case" , expression , ":" , { statement } ;
default_clause = "default" , ":" , { statement } ;

(* Возврат из функции *)
return_statement = "return" , expression , ";" ;

(* Прерывание цикла или switch *)
break_statement = "break" , ";" ;

(* Блок инструкций *)
block = "{" , { statement } , "}" ;

(* Типы данных *)
type_identifier = "float" | "bool" | "string" | "void" ;

(* Выражения и идентификаторы — из expressions-grammar.md *)
(* expression = ... ; *)
(* identifier = ... ; *)

```