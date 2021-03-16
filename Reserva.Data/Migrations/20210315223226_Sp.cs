using Microsoft.EntityFrameworkCore.Migrations;

namespace Reserva.Data.Migrations
{
    public partial class Sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR REPLACE FUNCTION public.getbooking(contact_id integer)
            RETURNS TABLE(Id int4, Title character varying, Date timestamp without time zone, Ranking int4, Favorite boolean, ContactId int4)
            LANGUAGE plpgsql
            AS $function$
                BEGIN
                    return QUERY select ""Id"",""Title"", ""Date"",""Ranking"", ""Favorite"", ""ContactId"" from ""Bookings"" b where ""ContactId"" = contact_id;
                END;
                $function$
            ;";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
