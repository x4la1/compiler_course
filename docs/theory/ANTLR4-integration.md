## Интеграция ANTLR4 в проект на C#

### Шаг 1: подготовка проекта
1. Убедитесь, что ваш проект нацелен на **.NET 8** и использует **C# 12**. Откройте файл проекта ```.csproj``` и проверьте элемент ```<TargetFramework>```:

```xml
<PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
</PropertyGroup>
```
<br>  

### Шаг 2: Установка необходимых пакетов NuGet
Вам понадобится основной пакет для сборки и стандартный рантайм.  
1. Установите пакет ```Antlr4BuildTasks``` (версия 12.11.0 актуальна на октябрь 2025) . Это обеспечит автоматическую генерацию парсера из файлов ```.g4```.

```bash
# Команда для .NET CLI (из корня проекта)
dotnet add package Antlr4BuildTasks --version 12.11.0
```
Альтернативно, в Visual Studio можно использовать ```Manage NuGet Packages for Solution``` и найти ```Antlr4BuildTasks```

2. Установите пакет рантайма:

```bash
dotnet add package Antlr4.Runtime.Standard --version 4.13.0
```

3. Проверьте, что выполнилось успешно:

```bash
dotnet restore
```

После установки структура зависимостей в ```.csproj``` файле должна выглядеть примерно так:

```xml
<ItemGroup>
    <PackageReference Include="Antlr4.Runtime.Standard" Version="4.13.0" />
    <PackageReference Include="Antlr4BuildTasks" Version="12.11.0" PrivateAssets="all" />
</ItemGroup>
```

<br>

### Шаг 3: Добавление грамматики в проект
1. Создайте файл грамматики (например, ```SimpleExpr.g4```) в папке проекта.  
2. Добавьте его в проект, явно указав элемент ```<Antlr4 />``` в файле ```.csproj```. Это необходимо для работы новых сборщиков:

```xml
<ItemGroup>
    <Antlr4 Include="SimpleExpr.g4" />
</ItemGroup>
```

3. Для примера используйте минимальную грамматику:

```antlr
grammar SimpleExpr;

prog:   expr EOF ;
expr:   expr ('*'|'/') expr
    |   expr ('+'|'-') expr
    |   INT
    |   '(' expr ')'
    ;
INT :   [0-9]+ ;
WS  :   [ \t\r\n]+ -> skip ;
```

```Примечание```: Файл должен быть сохранен в кодировке **UTF-8** без сигнатуры Byte Order Mark

<br>

### Шаг 4: Настройка параметров генерации кода (опционально)
Вы можете управлять генерацией кода, добавив свойства в элемент ```<Antlr4>``` в ```.csproj```:
- ```Visitor``` / ```Listener```: Генерировать интерфейсы посетителя (```true```) или слушателя (```false```) (по умолчанию ```Listener="true"```, ```Visitor="false"```).
- ```Package```: Задать пространство имен для сгенерированного кода.
- ```Error```: Строгий режим обработки ошибок.
- ```Abstract```: При (```true```) сгенерированные классы лексера и парсера будут ```abstract```.

Пример настройки для генерации ```Visitor```:

```xml
<Antlr4 Include="SimpleExpr.g4">
    <Visitor>true</Visitor>
    <Listener>false</Listener>
    <Package>MyApp.Generated</Package>
</Antlr4>
```

<br>

### Шаг 5: Сборка проекта и проверка
1. Выполните сборку:

```bash
dotnet build
```

Пакет ```Antlr4BuildTasks``` автоматически вызовет утилиту ANTLR для генерации лексера и парсера на C#.

2. Проверьте результат генерации. После успешной сборки в папке ```obj/Debug/net8.0/``` появятся сгенерированные файлы
```SimpleExprLexer.cs```, ```SimpleExprParser.cs```, ```SimpleExprListener.cs``` или ```SimpleExprVisitor.cs```.
Они автоматически включены в компиляцию.

<br>

### Шаг 6: Использование сгенерированного парсера
Создайте простой тест, чтобы убедиться, что интеграция работает. Добавьте в ```Program.cs``` следующий код:

```csharp
using Antlr4.Runtime;
using System.Text;

// укажите ваше пространство имен
namespace MyApp;

class Program
{
    static void Main(string[] args)
    {
        string input = "1 + 2 * (3 - 4)";
        var inputStream = new AntlrInputStream(input);
        var lexer = new SimpleExprLexer(inputStream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new SimpleExprParser(tokenStream);
        
        // запустите парсинг с начальным правилом 'prog' из грамматики
        var tree = parser.prog();
        
        // вывод дерева разбора в виде текста (LISP-подобный формат)
        Console.WriteLine($"Parse Tree: {tree.ToStringTree(parser)}");
    }
}
```

**Запустите проект и убедитесь, что код компилируется и выводит дерево разбора без ошибок.**

```bash
dotnet run
```

Ожидаемый вывод в консоли:

```plaintext
(prog (expr (expr 1) + (expr (expr 2) * (expr ( (expr (expr 3) - (expr 4)) )))) )
```
  















