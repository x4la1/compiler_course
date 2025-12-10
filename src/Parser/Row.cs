using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser;

public class Row
{
    private readonly decimal[] values;

    public Row(params decimal[] values)
    {
        this.values = values;
    }

    public int ColumnCount => values.Length;

    public decimal this[int index]
    {
        get => values[index];
    }
}