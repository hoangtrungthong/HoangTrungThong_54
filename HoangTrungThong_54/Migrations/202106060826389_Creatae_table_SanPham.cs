namespace HoangTrungThong_54.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creatae_table_SanPham : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        MaSanPham = c.Int(nullable: false, identity: true),
                        TenSanPham = c.String(),
                        MaNhaCungCap = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaSanPham);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SanPhams");
        }
    }
}
