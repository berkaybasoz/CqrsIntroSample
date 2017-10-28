using CqrsIntro.Aggregate;
using CqrsIntro.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsIntro.Command
{
    public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
    {
        private readonly IWriteRepository<Task> writeRepository;

        public CreateTaskCommandHandler(  IWriteRepository<Task> writeRepository)
        {
            if (writeRepository == null)
            {
                throw new ArgumentNullException("writeRepository");
            }
            this.writeRepository = writeRepository;
        }

        public void Execute(CreateTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ArgumentException("Title is not specified", "command");
            }
             
            var task = new Task();
            task.Title = command.Title;
            task.UserName = command.UserName;
            task.IsCompleted = command.IsCompleted;
            task.CreatedDate = command.CreatedOn;
            task.LastUpdatedDate = command.UpdatedOn;

            writeRepository.Add(task);

            writeRepository.Save();
        }

    }
}
