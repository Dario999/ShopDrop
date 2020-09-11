namespace ShopDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "product_Id", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "product_Id" });
            AddColumn("dbo.Purchases", "productId", c => c.Int(nullable: false));
            DropColumn("dbo.Purchases", "product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "product_Id", c => c.Int());
            DropColumn("dbo.Purchases", "productId");
            CreateIndex("dbo.Purchases", "product_Id");
            AddForeignKey("dbo.Purchases", "product_Id", "dbo.Products", "Id");
        }
    }
}
