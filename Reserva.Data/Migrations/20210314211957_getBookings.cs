using Microsoft.EntityFrameworkCore.Migrations;

namespace Reserva.Data.Migrations
{
    public partial class getBookings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR REPLACE FUNCTION public.getbookig(contact_id integer)
            RETURNS TABLE(titulo character varying, fecha timestamp without time zone)
            LANGUAGE plpgsql
            AS $function$
                BEGIN
                    return QUERY select ""Title"", ""Date"" from ""Bookings"" b where ""ContactId"" = contact_id;
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
