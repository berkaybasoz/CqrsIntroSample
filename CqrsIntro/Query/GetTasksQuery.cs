using CqrsIntro.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CqrsIntro.Query
{
    public class GetTasksQuery : IQuery
    {
        public Expression<Func<Task, bool>> Predicate { get; set; }

    }
}
