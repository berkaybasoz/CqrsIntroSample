using CqrsIntro.Aggregate;
using CqrsIntro.Command;
using CqrsIntro.Context;
using CqrsIntro.IoC;
using CqrsIntro.Query;
using CqrsIntro.Repository;
using System.Collections.Generic;
using System.Linq;

namespace CqrsIntro
{
    public static class BootStrapper
    {
        public static void Configure(IContainer container)
        {
            container.Register<IContainer, SimpleIocContainer>();//CommandDispatcher'ın constructorında ki IContainer için ekliyoruz

            container.Register<ICommandDispatcher, CommandDispatcher>();
            container.Register<ICommandHandler<CreateTaskCommand>, CreateTaskCommandHandler>();
            container.Register<ICommandHandler<ChangeTaskStatusCommand>, ChangeTaskStatusCommandHandler>();

            container.Register<IQueryDispatcher, QueryDispatcher>();
            container.Register<IQueryHandler<GetTasksQuery, IQueryable<Task>>, GetTasksQueryHandler>();

            container.Register<BaseContext, TaskContext>();
            container.Register<IReadRepository<Task>, BaseRepository<Task>>();
            container.Register<IWriteRepository<Task>, BaseRepository<Task>>();


        }
    }
}
