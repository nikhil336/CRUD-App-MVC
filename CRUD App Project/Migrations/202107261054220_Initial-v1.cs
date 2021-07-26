namespace CRUD_App_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 10),
                        Description = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 15),
                        DOJ = c.DateTime(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 25),
                        Address = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.Mobile, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Salaries",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SalaryAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Salaries", "Id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Salaries", new[] { "Id" });
            DropIndex("dbo.Employees", new[] { "Email" });
            DropIndex("dbo.Employees", new[] { "Mobile" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropTable("dbo.Salaries");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
        }
    }
}
