using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Migrations
{
    [Migration(20230506120002)]
    public class AddActionsTable:Migration
    {
        public override void Up()
        {
            Create.Table("Actions")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(255).Unique().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("UpdatedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Actions");
        }
    }
}
