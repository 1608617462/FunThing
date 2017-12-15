namespace FunThing.Migrations
{
    using FunThing.Class;
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class MySqlConfiguration : DbMigrationsConfiguration
    {
        public MySqlConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            //SetSqlGenerator方法将生成MySql命令，而不是SqlServer命令
           // SetSqlGenerator("MySql.Data.MySqlClient",new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory("MySql.Data.MySqlClient",(conn,schema)=>new MySqlHistoryContext(conn,schema));
        }
    }
}
