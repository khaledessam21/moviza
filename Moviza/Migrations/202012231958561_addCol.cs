namespace Moviza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "imagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "imagePath");
        }
    }
}
