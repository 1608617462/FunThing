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

            //SetSqlGenerator����������MySql���������SqlServer����
           // SetSqlGenerator("MySql.Data.MySqlClient",new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory("MySql.Data.MySqlClient",(conn,schema)=>new MySqlHistoryContext(conn,schema));
        }
    }
}
