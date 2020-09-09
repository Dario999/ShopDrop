namespace ShopDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ads", "product_Id", "dbo.Products");
            DropIndex("dbo.Ads", new[] { "product_Id" });
            DropTable("dbo.Ads");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Ads", "product_Id");
            AddForeignKey("dbo.Ads", "product_Id", "dbo.Products", "Id");
        }
    }
}
