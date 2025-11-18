using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer;
public enum TokenType
{
    /// <summary>
    /// Ключевое слово if
    /// </summary>
    If,

    /// <summary>
    /// Ключевое слово else
    /// </summary>
    Else,

    /// <summary>
    /// Ключевое слово while
    /// </summary>
    While,

    /// <summary>
    /// Ключевое слово continue
    /// </summary>
    Continue,

    /// <summary>
    /// Ключевое слово break
    /// </summary>
    Break,

    /// <summary>
    /// Ключевое слово print
    /// </summary>
    Print,

    /// <summary>
    /// Ключевое слово input
    /// </summary>
    Input,

    /// <summary>
    /// Ключевое слово int (целочиселнный тип)
    /// </summary>
    IntType,

    /// <summary>
    /// Ключевое слово float (вещественное число)
    /// </summary>
    FloatType,

    /// <summary>
    /// Ключевое слово string (строковый тип)
    /// </summary>
    StringType,

    /// <summary>
    /// Ключевое слово bool (булевый тип)
    /// </summary>
    BoolType,

    /// <summary>
    /// Ключевое слово func (функция)
    /// </summary>
    Function,

    /// <summary>
    /// Ключевое слово return
    /// </summary>
    Return,

    /// <summary>
    /// Идентификатор
    /// </summary>
    Identifier,

    /// <summary>
    /// Численный литерал
    /// </summary>
    NumericLiteral,

    /// <summary>
    /// Однострочный строковый литерал
    /// </summary>
    StringLiteral,

    /// <summary>
    /// Оператор сложения
    /// </summary>
    PlusSign,

    /// <summary>
    /// Оператор вычитания
    /// </summary>
    MinusSign,

    /// <summary>
    /// Оператор умножения
    /// </summary>
    MultiplySign,

    /// <summary>
    /// Оператор деления
    /// </summary>
    DivideSign,

    /// <summary>
    /// Оператор деления нацело
    /// </summary>
    ExactDivideSign,

    /// <summary>
    /// Оператор деление по модулю
    /// </summary>
    ModuloSign,

    /// <summary>
    /// Оператор возведения в степень
    /// </summary>
    ExponentiationSign,

    /// <summary>
    /// Оператор сравнения равно
    /// </summary>
    EqualSign,

    /// <summary>
    /// Оператор сравнения не равно
    /// </summary>
    NotEqualSign,

    /// <summary>
    /// Оператор сравнения строго больше
    /// </summary>
    LessSign,

    /// <summary>
    /// Оператор сравнения строго больше
    /// </summary>
    GreaterSign,

    /// <summary>
    /// Оператор сравнения меньше или равно
    /// </summary>
    LessOrEqualSign,

    /// <summary>
    /// Оператор сравнения больше или равно
    /// </summary>
    GreaterOrEqualSign,

    /// <summary>
    /// Логический оператор НЕ "!"
    /// </summary>
    NotSign,

    /// <summary>
    /// Логический оператор И '&&'
    /// </summary>
    And,

    /// <summary>
    /// Оператор сравнения ИЛИ '||'
    /// </summary>
    Or,

    /// <summary>
    /// Оператор присваивания "="
    /// </summary>
    AssignSign,

    /// <summary>
    /// Запятая
    /// </summary>
    Comma,

    /// <summary>
    /// Разделитель ';'
    /// </summary>
    Semicolon,

    /// <summary>
    ///  Открывающая круглая скобка
    /// </summary>
    OpenParenthesis,

    /// <summary>
    ///  Закрывающая круглая скобка
    /// </summary>
    CloseParenthesis,

    /// <summary>
    ///  Открывающая фигурная скобка
    /// </summary>
    OpenBrace,

    /// <summary>
    ///  Закрывающая фигурная скобка
    /// </summary>
    CloseBrace,

    /// <summary>
    ///  Логическая истина
    /// </summary>
    True,

    /// <summary>
    ///  Логическая ложь
    /// </summary>
    False,

    /// <summary>
    ///  Число пи
    /// </summary>
    Pi,

    /// <summary>
    ///  Число эйлера
    /// </summary>
    Euler,

    /// <summary>
    ///  Конец файла.
    /// </summary>
    EndOfFile,

    /// <summary>
    ///  Недопустимая лексема.
    /// </summary>
    Error,
}