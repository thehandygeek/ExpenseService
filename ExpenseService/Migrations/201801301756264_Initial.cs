namespace ExpenseService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 12, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Reciept_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RecieptImages", t => t.Reciept_Id)
                .Index(t => t.Reciept_Id);
            
            CreateTable(
                "dbo.RecieptImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageData = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Expiry = c.DateTime(nullable: false),
                        SessionId = c.String(nullable: false),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "Reciept_Id", "dbo.RecieptImages");
            DropIndex("dbo.Expenses", new[] { "Reciept_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Sessions");
            DropTable("dbo.RecieptImages");
            DropTable("dbo.Expenses");
        }
    }
}
