namespace Micro_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        idClient = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        surname = c.String(),
                        lastname = c.String(),
                        login = c.String(),
                        password = c.String(),
                        seriaPassport = c.String(),
                        numberPassport = c.String(),
                        deschotel = c.String(),
                        dataOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        countDaysLive = c.Int(),
                    })
                .PrimaryKey(t => t.idClient);
            
            CreateTable(
                "dbo.RegHotels",
                c => new
                    {
                        idLive = c.Int(nullable: false, identity: true),
                        idClient = c.Int(nullable: false),
                        idFlat = c.Int(nullable: false),
                        startLive = c.DateTime(nullable: false, storeType: "date"),
                        endLive = c.DateTime(nullable: false, storeType: "date"),
                        desclive = c.String(),
                    })
                .PrimaryKey(t => t.idLive)
                .ForeignKey("dbo.Clients", t => t.idClient, cascadeDelete: true)
                .ForeignKey("dbo.HotelRooms", t => t.idFlat, cascadeDelete: true)
                .Index(t => t.idClient)
                .Index(t => t.idFlat);
            
            CreateTable(
                "dbo.HotelRooms",
                c => new
                    {
                        idFlat = c.Int(nullable: false, identity: true),
                        nomer = c.String(nullable: false),
                        countPeople = c.Int(nullable: false),
                        category = c.String(),
                        payment = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idFlat);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegHotels", "idFlat", "dbo.HotelRooms");
            DropForeignKey("dbo.RegHotels", "idClient", "dbo.Clients");
            DropIndex("dbo.RegHotels", new[] { "idFlat" });
            DropIndex("dbo.RegHotels", new[] { "idClient" });
            DropTable("dbo.HotelRooms");
            DropTable("dbo.RegHotels");
            DropTable("dbo.Clients");
        }
    }
}
