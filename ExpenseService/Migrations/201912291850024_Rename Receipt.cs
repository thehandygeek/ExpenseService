namespace ExpenseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameReceipt : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RecieptImages", newName: "ReceiptImages");
            RenameColumn(table: "dbo.Expenses", name: "Reciept_Id", newName: "Receipt_Id");
            RenameIndex(table: "dbo.Expenses", name: "IX_Reciept_Id", newName: "IX_Receipt_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Expenses", name: "IX_Receipt_Id", newName: "IX_Reciept_Id");
            RenameColumn(table: "dbo.Expenses", name: "Receipt_Id", newName: "Reciept_Id");
            RenameTable(name: "dbo.ReceiptImages", newName: "RecieptImages");
        }
    }
}
