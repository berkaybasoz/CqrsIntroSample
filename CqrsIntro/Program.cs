using CqrsIntro;
using CqrsIntro.Aggregate;
using CqrsIntro.Command;
using CqrsIntro.Context;
using CqrsIntro.IoC;
using CqrsIntro.Query;
using CqrsIntro.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBaseInitializer<TaskContext>.InitializedDatabase();

            IContainer container = new SimpleIocContainer();

            BootStrapper.Configure(container);

            ICommandDispatcher commandDispatcher = container.Resolve<ICommandDispatcher>();

            IQueryDispatcher queryDispatcher = container.Resolve<IQueryDispatcher>();




            var createCommand = new CreateTaskCommand { Title = "CQRS Örneği", UserName = "Berkay Başöz", IsCompleted = false, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now };

            commandDispatcher.Execute(createCommand);


            var getTasksQuery = new GetTasksQuery();

            getTasksQuery.Predicate = (t) => t.IsCompleted == false;

            IQueryable<Task> tasks = queryDispatcher.Query<GetTasksQuery, IQueryable<Task>>(getTasksQuery);


            Console.WriteLine("Bitmemiş tasklar getiriliyor.");

            foreach (var task in tasks.ToList())
            {
                Console.WriteLine(task);
            }

            var lastTask = tasks.ToList().LastOrDefault();


            var changeCommand = new ChangeTaskStatusCommand { TaskId =24, IsCompleted = true, UpdatedOn = DateTime.Now.AddMinutes(5) };

            commandDispatcher.Execute(changeCommand);

            Console.ReadLine();
        }
    }


}
