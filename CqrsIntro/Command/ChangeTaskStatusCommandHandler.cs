using CqrsIntro.Aggregate;
using CqrsIntro.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsIntro.Command
{
    public class ChangeTaskStatusCommandHandler : ICommandHandler<ChangeTaskStatusCommand>
    {
        private readonly IReadRepository<Task> readRepository;
        private readonly IWriteRepository<Task> writeRepository;

        private const string title = "CQRS Task";

        public ChangeTaskStatusCommandHandler(IReadRepository<Task> readRepository, IWriteRepository<Task> writeRepository)
        {
            if (readRepository == null)
            {
                throw new ArgumentNullException("readRepository");
            }

            if (writeRepository == null)
            {
                throw new ArgumentNullException("writeRepository");
            }

            this.readRepository = readRepository;
            this.writeRepository = writeRepository;
        }

        public void Execute(ChangeTaskStatusCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (command.TaskId <= 0)
            {
                throw new ArgumentException("Id is not specified", "command");
            }

            Task task = new Task();

            task.Id = command.TaskId;

            writeRepository.DetectUpdate(task);//Sadece değişen alanları veritabanına yansıtmak için entity değişikliklerini algılamaya başlıyoruz. writeRepository.Update kullanılabilirdi ama öncesinde repository üzerinden task entitysini select etmek gerekeceği için iki kez veritabanına gitmek istemiyoruz.

            task.IsCompleted = command.IsCompleted;

            task.LastUpdatedDate = command.UpdatedOn;

            writeRepository.Save();

        }

    }
}
