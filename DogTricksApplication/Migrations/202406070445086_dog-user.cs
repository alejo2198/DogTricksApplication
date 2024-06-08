namespace DogTricksApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doguser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dogs", "DogOwner", c => c.String(maxLength: 128));
            CreateIndex("dbo.Dogs", "DogOwner");
            AddForeignKey("dbo.Dogs", "DogOwner", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dogs", "DogOwner", "dbo.AspNetUsers");
            DropIndex("dbo.Dogs", new[] { "DogOwner" });
            AlterColumn("dbo.Dogs", "DogOwner", c => c.Int(nullable: false));
        }
    }
}
