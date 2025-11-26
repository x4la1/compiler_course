using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Execution;
public interface IEnvironment
{
    public void WriteNumber(decimal result);

    public decimal ReadNumber();
}