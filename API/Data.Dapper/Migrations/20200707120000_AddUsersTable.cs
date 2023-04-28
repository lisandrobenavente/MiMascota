using FluentMigrator;

namespace Data.Dapper.Migrations
{
    [Migration(20200707120000)]
    public class AddUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Username").AsString(255).Unique().NotNullable()
                .WithColumn("Password").AsString(65535).NotNullable()
                .WithColumn("CreatedDate").AsDateTime().Nullable()
                .WithColumn("UpdatedDate").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
