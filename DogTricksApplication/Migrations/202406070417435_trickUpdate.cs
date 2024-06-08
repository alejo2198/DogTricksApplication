namespace DogTricksApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trickUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tricks",
                c => new
                    {
                        TrickId = c.Int(nullable: false, identity: true),
                        TrickName = c.String(),
                        TrickVideoLink = c.String(),
                        TrickDescription = c.String(),
                        TrickDifficulty = c.String(),
                    })
                .PrimaryKey(t => t.TrickId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tricks");
        }
    }
}
