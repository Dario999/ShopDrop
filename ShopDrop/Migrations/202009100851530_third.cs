namespace ShopDrop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "selller_id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "selller_id", c => c.Int(nullable: false));
        }
    }
}
