namespace ShopDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ShoppingCart_Id", "dbo.ShoppingCarts");
            DropIndex("dbo.Products", new[] { "ShoppingCart_Id" });
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date_posted = c.DateTime(nullable: false),
                        is_sold = c.Boolean(nullable: false),
                        seller_id = c.String(),
                        product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.product_Id)
                .Index(t => t.product_Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        buyer_id = c.String(),
                        ad_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Ads", t => t.ad_Id)
                .Index(t => t.ad_Id);
            
            DropColumn("dbo.Products", "ShoppingCart_Id");
            DropTable("dbo.ShoppingCarts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Balance = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "ShoppingCart_Id", c => c.Int());
            DropForeignKey("dbo.Purchases", "ad_Id", "dbo.Ads");
            DropForeignKey("dbo.Ads", "product_Id", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "ad_Id" });
            DropIndex("dbo.Ads", new[] { "product_Id" });
            DropTable("dbo.Purchases");
            DropTable("dbo.Ads");
            CreateIndex("dbo.Products", "ShoppingCart_Id");
            AddForeignKey("dbo.Products", "ShoppingCart_Id", "dbo.ShoppingCarts", "Id");
        }
    }
}
