namespace DogTricksApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dogs",
                c => new
                    {
                        DogId = c.Int(nullable: false, identity: true),
                        DogName = c.String(),
                        DogAge = c.Int(nullable: false),
                        DogBreed = c.String(),
                        DogBirthday = c.DateTime(nullable: false),
                        DogOwner = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Dogs");
        }
    }
}
