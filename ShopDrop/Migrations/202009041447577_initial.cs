namespace ShopDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Image = c.String(),
                        Category_Id = c.Int(),
                        Category_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id1)
                .Index(t => t.Category_Id)
                .Index(t => t.Category_Id1);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Balance = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppingCartProducts",
                c => new
                    {
                        ShoppingCart_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_Id, t.Product_Id })
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ShoppingCart_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Category_Id1", "dbo.Categories");
            DropForeignKey("dbo.ShoppingCartProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ShoppingCartProducts", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Product_Id", "dbo.Products");
            DropIndex("dbo.ShoppingCartProducts", new[] { "Product_Id" });
            DropIndex("dbo.ShoppingCartProducts", new[] { "ShoppingCart_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id1" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Categories", new[] { "Product_Id" });
            DropTable("dbo.ShoppingCartProducts");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
