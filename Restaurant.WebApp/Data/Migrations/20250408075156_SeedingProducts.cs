using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(90)",
                maxLength: 90,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Месо");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 3, "Риба" },
                    { 4, "Паста" },
                    { 5, "Пица" },
                    { 6, "Суши" },
                    { 7, "Странични ястия" },
                    { 8, "Десерти" },
                    { 9, "Веган" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name" },
                values: new object[] { 1, "салата, авокадо", "https://i.imgur.com/ytAl6b7.jpg", "Салата с авокадо" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 2, 1, "салата, крутони, пиле", "https://i.imgur.com/46hWRKm.jpg", "Салата цезър", null, null },
                    { 3, 1, "салата, риба тон", "https://i.imgur.com/nRqrS2Z.jpg", "Салата с риба тон", null, null },
                    { 4, 1, "салата, лук, домати, краставица", "https://i.imgur.com/tGkD6Mz.jpg", "Гръцка салата", null, null },
                    { 5, 2, "свинско месо с зеленчуци", "https://i.imgur.com/Mud7P5Z.jpg", "Свинско", null, null },
                    { 6, 2, "кюфтета с картофи и зеленчуци", "https://i.imgur.com/eZEPgd8.jpg", "Кюфтета", null, null },
                    { 7, 2, "пилешко месо, зеленчуци", "https://i.imgur.com/4HNR1ZY.jpg", "Пилешко месо", null, null },
                    { 8, 2, "Телешко месо с картофи", "https://i.imgur.com/HeE8bQ5.jpg", "Телешко", null, null },
                    { 9, 4, "Карбонара", "https://i.imgur.com/n4TbgT7.jpg", "Паста Карбонара", null, null },
                    { 10, 4, "Болонезе", "https://i.imgur.com/ZG42J7n.jpg", "Паста Болонезе", null, null },
                    { 11, 4, "морски дарове с паста", "https://i.imgur.com/ea3XILH.jpg", "Паста с морски дарове", null, null },
                    { 12, 4, "кайма, кашкавал, босилек", "https://i.imgur.com/tWo1D41.jpg", "Лазаня", null, null },
                    { 13, 5, "моцарела, доматен сос", "https://i.imgur.com/mabeII0.jpg", "Пица с моцарела", null, null },
                    { 14, 5, "кюфтета, доматен сос, кашкавал", "https://i.imgur.com/mZItQiX.jpg", "Пица с кюфтета", null, null },
                    { 15, 5, "сирена от различен вид", "https://i.imgur.com/91ey2lO.jpg", "Пица четири сирена", null, null },
                    { 16, 5, "салам, чери домати, босиляк", "https://i.imgur.com/mQ8m6YF.jpg", "Пица салами", null, null },
                    { 17, 3, "сьомга на грил", "https://i.imgur.com/Iw86J6D.jpg", "Сьомга", null, null },
                    { 18, 3, "с резънчета лимон и салата", "https://i.imgur.com/uWiso3I.jpg", "Риба на скара", null, null },
                    { 19, 3, "порция изчистени скариди", "https://i.imgur.com/maOOr2c.jpg", "Скариди", null, null },
                    { 20, 3, "пържена риба с картофено пюре", "https://i.imgur.com/H35uMie.jpg", "Пържена риба", null, null },
                    { 21, 6, "Суши стил футомаки с рак", "https://i.imgur.com/I3HdRmg.png", "Футомаки Рак", null, null },
                    { 22, 6, "Суши стил нигири със сьомга", "https://i.imgur.com/9PELxEu.png", "Нигири сьомга", null, null },
                    { 23, 6, "Суши сет стил хосумаки", "https://i.imgur.com/BqDTtuo.png", "Хосумаки сет", null, null },
                    { 24, 6, "Суши пържен сет", "https://i.imgur.com/kGfrZXO.png", "Пържен сет", null, null },
                    { 25, 7, "Прясно изпържени картофи с кашкавалена покривка", "https://i.imgur.com/LEzjncV.png", "Пържени картофи", null, null },
                    { 26, 7, "боб зелен соров", "https://i.imgur.com/35Rfcar.jpg", "Зелен боб", null, null },
                    { 27, 7, "изпечени прясни картофи", "https://i.imgur.com/afBYXEq.jpg", "Печени картофи", null, null },
                    { 28, 7, "Прясно изпечени зеленчуци", "https://i.imgur.com/wrkStpo.jpg", "Печени зеленчуци", null, null },
                    { 29, 8, "пърче чийскейк", "https://i.imgur.com/b98zxYM.jpg", "Чийскейк", null, null },
                    { 30, 8, "Домашно приготвено тирамису", "https://i.imgur.com/ucIJMQO.jpg", "Тирамису", null, null },
                    { 31, 8, "Палачинки с шоколад", "https://i.imgur.com/16gDj6l.jpg", "Палачинки", null, null },
                    { 32, 8, "Гофрети с кленов сироп", "https://i.imgur.com/nY9Cgqf.jpg", "Гофрети", null, null },
                    { 33, 9, "Тофу салата", "https://i.imgur.com/D2nrUPV.jpg", "Тофу", null, null },
                    { 34, 9, "дъмплинги по китайски", "https://i.imgur.com/mqmYr9T.jpg", "Дъмплинги", null, null },
                    { 35, 9, "2 яца по стил бъркани", "https://i.imgur.com/co7Mn3A.jpg", "Бъркани яйца с тостове", null, null },
                    { 36, 9, "със сос сладко кисел и вътрешност", "https://i.imgur.com/CbdHjAT.jpg", "Запържени ролца", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(90)",
                oldMaxLength: 90);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Основни");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name" },
                values: new object[] { 2, "Black Angus месо, маруля, лук, барбекю сос", "https://images.unsplash.com/photo-1568901346375-23c9450c58cd?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8YnVyZ2VyfGVufDB8fDB8fHww", "Бургер" });
        }
    }
}
