using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNet.Identity
{
    public class AppIdentityDbContext:IdentityDbContext<AppUser>
    {
        /// <summary>
        /// 数据库链接字符串的name
        /// </summary>
        public AppIdentityDbContext():base("MySqlDB")
        {

        }
        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }
    }
    public class IdentityDbInit:DropCreateDatabaseIfModelChanges<AppIdentityDbContext>
    {
        protected override void Seed(AppIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(AppIdentityDbContext context)
        {
            //Do Something
        }
    }
}