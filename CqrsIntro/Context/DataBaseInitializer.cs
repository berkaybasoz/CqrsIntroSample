using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CqrsIntro.Context
{
    public class DataBaseInitializer<T> where T : DbContext, new()
    {
        private static bool isDbInitialized = false;
        private static object objLock = new object();

        public static void InitializedDatabase(bool force = false)
        {
            lock (objLock)
            {
                if (!isDbInitialized)
                {
                    Database.SetInitializer<T>(new InitializerIfNotExists<T>());
                    T instance = new T();
                    instance.Database.Initialize(force);
                    instance.Database.Log = m => System.Diagnostics.Debug.Write(m, "DataContext");
                    isDbInitialized = true;
                }
            }
        }
    }
}
