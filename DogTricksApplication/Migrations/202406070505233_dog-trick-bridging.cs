namespace DogTricksApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dogtrickbridging : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogxTricks",
                c => new
                    {
                        DogTrickId = c.Int(nullable: false, identity: true),
                        DogTrickDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DogTrickId);
            
            CreateTable(
                "dbo.DogxTrickDogs",
                c => new
                    {
                        DogxTrick_DogTrickId = c.Int(nullable: false),
                        Dog_DogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DogxTrick_DogTrickId, t.Dog_DogId })
                .ForeignKey("dbo.DogxTricks", t => t.DogxTrick_DogTrickId, cascadeDelete: true)
                .ForeignKey("dbo.Dogs", t => t.Dog_DogId, cascadeDelete: true)
                .Index(t => t.DogxTrick_DogTrickId)
                .Index(t => t.Dog_DogId);
            
            CreateTable(
                "dbo.TrickDogxTricks",
                c => new
                    {
                        Trick_TrickId = c.Int(nullable: false),
                        DogxTrick_DogTrickId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trick_TrickId, t.DogxTrick_DogTrickId })
                .ForeignKey("dbo.Tricks", t => t.Trick_TrickId, cascadeDelete: true)
                .ForeignKey("dbo.DogxTricks", t => t.DogxTrick_DogTrickId, cascadeDelete: true)
                .Index(t => t.Trick_TrickId)
                .Index(t => t.DogxTrick_DogTrickId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrickDogxTricks", "DogxTrick_DogTrickId", "dbo.DogxTricks");
            DropForeignKey("dbo.TrickDogxTricks", "Trick_TrickId", "dbo.Tricks");
            DropForeignKey("dbo.DogxTrickDogs", "Dog_DogId", "dbo.Dogs");
            DropForeignKey("dbo.DogxTrickDogs", "DogxTrick_DogTrickId", "dbo.DogxTricks");
            DropIndex("dbo.TrickDogxTricks", new[] { "DogxTrick_DogTrickId" });
            DropIndex("dbo.TrickDogxTricks", new[] { "Trick_TrickId" });
            DropIndex("dbo.DogxTrickDogs", new[] { "Dog_DogId" });
            DropIndex("dbo.DogxTrickDogs", new[] { "DogxTrick_DogTrickId" });
            DropTable("dbo.TrickDogxTricks");
            DropTable("dbo.DogxTrickDogs");
            DropTable("dbo.DogxTricks");
        }
    }
}
