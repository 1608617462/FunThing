using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace FunThing
{
    public class MySqlInitializer:IDatabaseInitializer<AppIdentityDbContext>
    {
        public void InitializeDatabase(AppIdentityDbContext context)
        {
            if(!context.Database.Exists())
            {
                context.Database.Create();
            }
            else
            {
                var migrationHistoryTableExists = ((IObjectContextAdapter)context).ObjectContext.ExecuteStoreQuery<int>($"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema='FunthingDb' AND table_name='__MigrationHistory'");
                if(migrationHistoryTableExists.FirstOrDefault()==0)
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
        }
    }
}