using Business.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Resolve
{
    public interface ICalculate
    { 
        DataResult Calculates(DataCalculate dataCalculate) ;
    }
}
