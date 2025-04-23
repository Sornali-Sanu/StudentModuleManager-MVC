namespace MVCMDUsingJQAjax.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                        Dob = c.DateTime(nullable: false),
                        MobileNo = c.String(),
                        ImageUrl = c.String(),
                        IsEnrolled = c.Boolean(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.CourseModules",
                c => new
                    {
                        CourseModuleId = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                        Duration = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseModuleId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseModules", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseModules", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "CourseId" });
            DropTable("dbo.CourseModules");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
        }
    }
}
