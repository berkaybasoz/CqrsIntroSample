using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsIntro.Query
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery
    {
        TResult Query(TQuery query);
    }
}
