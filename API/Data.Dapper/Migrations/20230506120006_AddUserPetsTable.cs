using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dapper.Migrations
{
    [Migration(20230506120006)]
    public class AddUserPetsTable:Migration
    {
        public override void Up()
        {
            Create.Table("UserPets")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_UserPets_Users_Id", "Users", "Id")
                .WithColumn("PetTypeId").AsGuid().NotNullable().ForeignKey("FK_UserPets_PetType_Id", "UserType", "Id")
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Age").AsInt32().NotNullable()
                .WithColumn("Neutered").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("SizeOrBreed").AsString(255).NotNullable()
                .WithColumn("HasPatologies").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("UpdatedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("UserPets");
        }
    }
}
