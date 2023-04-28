using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Migrations
{

    [Migration(20230428120000)]
    public class _20230428120000_AddUserProfileTable:Migration
    {
        public override void Up()
        {
            Create.Table("UserProfile")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_UserProfile_Users_UserId", "Users", "Id")
                .WithColumn("FirstName").AsString(100).NotNullable()
                .WithColumn("LastName").AsString(100).NotNullable()
                .WithColumn("EmailAddress").AsString(255).Unique().NotNullable()
                .WithColumn("Phone").AsString(50).NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("UpdatedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_UserBooks_Users_UserId");
            Delete.Table("UserProfile");
        }
    }
}
