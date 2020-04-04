using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Job.TokensStatistics.MsSqlRepositories.Migrations
{
    public partial class EarnedAndBurnedStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "last_burned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "last_earned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "total_burned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "total_earned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_burned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount");

            migrationBuilder.DropColumn(
                name: "last_earned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount");

            migrationBuilder.DropColumn(
                name: "total_burned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot");

            migrationBuilder.DropColumn(
                name: "total_earned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot");
        }
    }
}
