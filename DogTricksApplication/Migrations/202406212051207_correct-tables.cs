namespace DogTricksApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correcttables : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DogxTrickTricks", newName: "TrickDogxTricks");
            DropForeignKey("dbo.Dogs", "DogxTrick_DogTrickId", "dbo.DogxTricks");
            DropForeignKey("dbo.Tricks", "Dog_DogId", "dbo.Dogs");
            DropIndex("dbo.Dogs", new[] { "DogxTrick_DogTrickId" });
            DropIndex("dbo.Tricks", new[] { "Dog_DogId" });
            DropPrimaryKey("dbo.TrickDogxTricks");
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
            
            AddPrimaryKey("dbo.TrickDogxTricks", new[] { "Trick_TrickId", "DogxTrick_DogTrickId" });
            DropColumn("dbo.Dogs", "DogxTrick_DogTrickId");
            DropColumn("dbo.Tricks", "Dog_DogId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tricks", "Dog_DogId", c => c.Int());
            AddColumn("dbo.Dogs", "DogxTrick_DogTrickId", c => c.Int());
            DropForeignKey("dbo.DogxTrickDogs", "Dog_DogId", "dbo.Dogs");
            DropForeignKey("dbo.DogxTrickDogs", "DogxTrick_DogTrickId", "dbo.DogxTricks");
            DropIndex("dbo.DogxTrickDogs", new[] { "Dog_DogId" });
            DropIndex("dbo.DogxTrickDogs", new[] { "DogxTrick_DogTrickId" });
            DropPrimaryKey("dbo.TrickDogxTricks");
            DropTable("dbo.DogxTrickDogs");
            AddPrimaryKey("dbo.TrickDogxTricks", new[] { "DogxTrick_DogTrickId", "Trick_TrickId" });
            CreateIndex("dbo.Tricks", "Dog_DogId");
            CreateIndex("dbo.Dogs", "DogxTrick_DogTrickId");
            AddForeignKey("dbo.Tricks", "Dog_DogId", "dbo.Dogs", "DogId");
            AddForeignKey("dbo.Dogs", "DogxTrick_DogTrickId", "dbo.DogxTricks", "DogTrickId");
            RenameTable(name: "dbo.TrickDogxTricks", newName: "DogxTrickTricks");
        }
    }
}
