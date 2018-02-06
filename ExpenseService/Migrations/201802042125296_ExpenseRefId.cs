namespace ExpenseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpenseRefId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "ReferenceId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "ReferenceId");
        }
    }
}
