namespace ShopDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "sellerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "sellerName");
        }
    }
}
