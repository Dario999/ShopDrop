namespace ShopDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Purchases", "ad_Id", "dbo.Ads");
            DropIndex("dbo.Purchases", new[] { "ad_Id" });
            AddColumn("dbo.Products", "date_posted", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "selller_id", c => c.Int(nullable: false));
            AddColumn("dbo.Purchases", "seller_id", c => c.String());
            AddColumn("dbo.Purchases", "product_Id", c => c.Int());
            CreateIndex("dbo.Purchases", "product_Id");
            AddForeignKey("dbo.Purchases", "product_Id", "dbo.Products", "Id");
            DropColumn("dbo.Purchases", "ad_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "ad_Id", c => c.Int());
            DropForeignKey("dbo.Purchases", "product_Id", "dbo.Products");
            DropIndex("dbo.Purchases", new[] { "product_Id" });
            DropColumn("dbo.Purchases", "product_Id");
            DropColumn("dbo.Purchases", "seller_id");
            DropColumn("dbo.Products", "selller_id");
            DropColumn("dbo.Products", "date_posted");
            CreateIndex("dbo.Purchases", "ad_Id");
            AddForeignKey("dbo.Purchases", "ad_Id", "dbo.Ads", "Id");
        }
    }
}
