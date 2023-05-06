using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Migrations
{
    [Migration(20230506120003)]
    public class AddUserRoleTable:Migration
    {
        public override void Up()
        {
            Create.Table("UserRole")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_UserRolee_Users_Id", "Users", "Id")
                .WithColumn("RoleId").AsGuid().NotNullable().ForeignKey("FK_UserRole_Role_Id", "Role", "Id")
                .WithColumn("UserTypeId").AsGuid().NotNullable().ForeignKey("FK_UserRole_UserType_Id", "UserType", "Id")
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("UpdatedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("UserRole");
        }
    }
}
