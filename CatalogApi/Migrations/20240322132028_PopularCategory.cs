using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogApi.Migrations
{
    /// <inheritdoc />
    public partial class PopularCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into Categories(Name, UrlImage) VALUES ('Bebidas', 'bebidas.jpg')");
            mb.Sql("insert into Categories(Name, UrlImage) VALUES ('Lanches', 'lanches.jpg')");
            mb.Sql("insert into Categories(Name, UrlImage) VALUES ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
