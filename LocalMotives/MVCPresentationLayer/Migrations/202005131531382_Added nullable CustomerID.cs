namespace MVCPresentationLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednullableCustomerID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CustomerID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CustomerID");
        }
    }
}
