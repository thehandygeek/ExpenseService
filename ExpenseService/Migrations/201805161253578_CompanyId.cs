namespace ExpenseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages");
            DropIndex("dbo.Expenses", new[] { "Reciept_Id" });
            AddColumn("dbo.Expenses", "CompanyId", c => c.String(nullable: false, defaultValue: "TCH"));
            AlterColumn("dbo.Expenses", "Reciept_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Expenses", "Reciept_Id");
            AddForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages");
            DropIndex("dbo.Expenses", new[] { "Reciept_Id" });
            AlterColumn("dbo.Expenses", "Reciept_Id", c => c.Guid());
            DropColumn("dbo.Expenses", "CompanyId");
            CreateIndex("dbo.Expenses", "Reciept_Id");
            AddForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages", "Id");
        }
    }
}
