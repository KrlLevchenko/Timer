using FluentMigrator;

namespace Dber.Migration
{
    [Migration(2021_05_06_12_11)]
    public class _2021_05_06_12_11_CreateRunTable: AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("runs")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("Date").AsDateTime2().NotNullable();
        }
    }
}