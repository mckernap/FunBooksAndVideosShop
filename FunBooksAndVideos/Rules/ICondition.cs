using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Rules
{
    public interface ICondition
    {
        bool IsSatisfied();
    }
}
