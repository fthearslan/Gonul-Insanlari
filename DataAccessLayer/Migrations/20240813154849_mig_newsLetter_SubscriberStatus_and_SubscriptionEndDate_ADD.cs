using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_newsLetter_SubscriberStatus_and_SubscriptionEndDate_ADD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcsriberStatus",
                table: "NewsLetters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDate",
                table: "NewsLetters",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubcsriberStatus",
                table: "NewsLetters");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndDate",
                table: "NewsLetters");
        }
    }
}
