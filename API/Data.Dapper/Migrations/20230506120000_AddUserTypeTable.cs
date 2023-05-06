using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Migrations
{
    [Migration(20230506120000)]
    public class AddUserTypeTable : Migration
    {
        public override void Up()
        {
            Create.Table("UserType")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(255).Unique().NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("UpdatedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("UserType");
        }
    }
}
