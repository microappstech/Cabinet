using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cabinet.Data.Migrations
{
    public partial class RemoveUserName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "UserName",
            //    table: "Users");

            //migrationBuilder.RenameColumn(
            //    name: "Username",
            //    table: "Users",
            //    newName: "UserName");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserName",
            //    table: "Users",
            //    type: "nvarchar(256)",
            //    maxLength: 256,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "UserName",
            //    table: "Users",
            //    newName: "Username");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Username",
            //    table: "Users",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(256)",
            //    oldMaxLength: 256,
            //    oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserName",
            //    table: "Users",
            //    type: "nvarchar(256)",
            //    maxLength: 256,
            //    nullable: true);
        }
    }
}
