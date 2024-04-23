namespace Project_Speaker.Migrations.SpeakerDB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        SpeakerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Realese = c.DateTime(nullable: false, storeType: "date"),
                        Purchasable = c.Boolean(nullable: false),
                        Picture = c.String(),
                        SpeakerModelId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SpeakerId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.SpeakerModels", t => t.SpeakerModelId, cascadeDelete: true)
                .Index(t => t.SpeakerModelId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.SpeakerModels",
                c => new
                    {
                        SpeakerModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.SpeakerModelId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SpeakerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.Speakers", t => t.SpeakerId, cascadeDelete: true)
                .Index(t => t.SpeakerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "SpeakerId", "dbo.Speakers");
            DropForeignKey("dbo.Speakers", "SpeakerModelId", "dbo.SpeakerModels");
            DropForeignKey("dbo.Speakers", "BrandId", "dbo.Brands");
            DropIndex("dbo.Stocks", new[] { "SpeakerId" });
            DropIndex("dbo.Speakers", new[] { "BrandId" });
            DropIndex("dbo.Speakers", new[] { "SpeakerModelId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.SpeakerModels");
            DropTable("dbo.Speakers");
            DropTable("dbo.Brands");
        }
    }
}
