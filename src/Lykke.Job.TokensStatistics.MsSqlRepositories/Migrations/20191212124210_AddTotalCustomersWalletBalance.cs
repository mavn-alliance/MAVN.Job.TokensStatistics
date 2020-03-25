using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Job.TokensStatistics.MsSqlRepositories.Migrations
{
    public partial class AddTotalCustomersWalletBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "last_total_tokens_in_customers_wallets",
                schema: "tokens_statistics",
                table: "last_token_total_amount",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                @"UPDATE [tokens_statistics].[last_token_total_amount] SET [last_total_tokens_in_customers_wallets] = [last_total_amount]");

            migrationBuilder.AddColumn<string>(
                name: "total_tokens_in_customers_wallets",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(
                @"UPDATE [tokens_statistics].[daily_tokens_snapshot] SET [total_tokens_in_customers_wallets] = [total_tokens]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_total_tokens_in_customers_wallets",
                schema: "tokens_statistics",
                table: "last_token_total_amount");

            migrationBuilder.DropColumn(
                name: "total_tokens_in_customers_wallets",
                schema: "tokens_statistics",
                table: "daily_tokens_snapshot");
        }
    }
}
