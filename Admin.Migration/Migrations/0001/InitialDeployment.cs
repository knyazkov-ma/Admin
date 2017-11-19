using FluentMigrator;

namespace Admin.Migration.Migrations
{
    [Migration(20171119175130, "InitialDeployment")]
    public class InitialDeployment : ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.EmbeddedScript("create-database.sql");
        }
    }
}