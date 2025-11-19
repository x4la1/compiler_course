using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Execution;

public class Context
{
    private readonly Stack<Scope> scopes = [];

    public Context()
    {
        PushScope(new Scope());
    }

    public void PushScope(Scope scope)
    {
        scopes.Push(scope);
    }

    public void PopScope()
    {
        if (scopes.Count > 1)
        {
            scopes.Pop();
        }
    }

    /// <summary>
    /// Возвращает значение переменной или константы.
    /// </summary>
    public decimal GetValue(string name)
    {
        foreach (Scope s in scopes.Reverse())
        {
            if (s.TryGetVariable(name, out decimal variable))
            {
                return variable;
            }
        }

        throw new ArgumentException($"Variable '{name}' is not defined");
    }

    /// <summary>
    /// Присваивает (изменяет) значение переменной.
    /// </summary>
    public void AssignVariable(string name, decimal value)
    {
        foreach (Scope s in scopes.Reverse())
        {
            if (s.TryAssignVariable(name, value))
            {
                return;
            }
        }

        throw new ArgumentException($"Variable '{name}' is not defined");
    }

    /// <summary>
    /// Определяет переменную в текущей области видимости.
    /// </summary>
    public void DefineVariable(string name, decimal value)
    {
        if (!scopes.Peek().TryDefineVariable(name, value))
        {
            throw new ArgumentException($"Variable '{name}' is already defined in this scope");
        }
    }
}