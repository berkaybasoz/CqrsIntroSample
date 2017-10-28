using CqrsIntro.Aggregate;
using CqrsIntro.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsIntro.Query
{
    public class GetTasksQueryHandler : IQueryHandler<GetTasksQuery, IQueryable<Task>>
    {
        private readonly IReadRepository<Task> readRepository;

        public GetTasksQueryHandler(IReadRepository<Task> readRepository)
        {
            if (readRepository == null)
            {
                throw new ArgumentNullException("readRepository");
            }
            this.readRepository = readRepository;
        }

        public IQueryable<Task> Query(GetTasksQuery query)
        {
            return readRepository.GetAll(query.Predicate);
        }
    }
}
