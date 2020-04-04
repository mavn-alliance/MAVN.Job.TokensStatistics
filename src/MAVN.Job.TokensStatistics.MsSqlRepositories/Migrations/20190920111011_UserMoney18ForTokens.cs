using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Job.TokensStatistics.MsSqlRepositories.Migrations
{
    public partial class UserMoney18ForTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "last_total_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "last_earned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "last_burned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "total_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "total_earned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "total_burned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "last_total_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "last_earned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "last_burned_amount",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "total_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "total_earned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "total_burned_tokens",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
