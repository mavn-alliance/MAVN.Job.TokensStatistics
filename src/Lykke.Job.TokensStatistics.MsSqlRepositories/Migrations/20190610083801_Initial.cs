using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Job.TokensStatistics.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tokens_statistics");

            migrationBuilder.CreateTable(
                name: "daily_tokens_snapshot",
                schema: "tokens_statistics",
                columns: table => new
                {
                    date = table.Column<string>(nullable: false),
                    total_tokens = table.Column<long>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_daily_tokens_snapshot", x => x.date);
                });

            migrationBuilder.CreateTable(
                name: "last_token_total_amount",
                schema: "tokens_statistics",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    last_total_amount = table.Column<long>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_last_token_total_amount", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "daily_tokens_snapshot",
                schema: "tokens_statistics");

            migrationBuilder.DropTable(
                name: "last_token_total_amount",
                schema: "tokens_statistics");
        }
    }
}
