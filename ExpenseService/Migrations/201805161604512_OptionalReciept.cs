namespace ExpenseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalReciept : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages");
            DropIndex("dbo.Expenses", new[] { "Reciept_Id" });
            AlterColumn("dbo.Expenses", "Reciept_Id", c => c.Guid());
            CreateIndex("dbo.Expenses", "Reciept_Id");
            AddForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages");
            DropIndex("dbo.Expenses", new[] { "Reciept_Id" });
            AlterColumn("dbo.Expenses", "Reciept_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Expenses", "Reciept_Id");
            AddForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages", "Id", cascadeDelete: true);
        }
    }
}
