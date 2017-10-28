using CqrsIntro.Aggregate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CqrsIntro.Context
{
    public class TaskContext : BaseContext
    {
        public TaskContext()
            : base("name=TaskContext")
        {
        }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {  
            base.OnModelCreating(modelBuilder); 
        } 
    }
}
