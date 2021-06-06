namespace HoangTrungThong_54.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_table_NhaCungCap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NhaCungCaps",
                c => new
                    {
                        MaNhaCungCap = c.Int(nullable: false, identity: true),
                        TenNhaCungCap = c.String(),
                    })
                .PrimaryKey(t => t.MaNhaCungCap);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NhaCungCaps");
        }
    }
}
