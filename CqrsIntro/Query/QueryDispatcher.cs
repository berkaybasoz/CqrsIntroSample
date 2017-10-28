using CqrsIntro.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsIntro.Query
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IContainer container;

        public QueryDispatcher(IContainer container)
        {
            this.container = container;
        } 

        public TResult Query<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
              if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            var handler = container.Resolve<IQueryHandler<TQuery, TResult>>();

            if (handler == null)
            {
                throw new QueryHandlerNotFoundException(typeof(TQuery));
            }

            return handler.Query(query);
        }
    }
}
