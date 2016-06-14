namespace OperationLog.Database.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Short(nullable: false, identity: true),
                        DepartmentName = c.String(maxLength: 30, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        OperationId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        StationAddress = c.String(maxLength: 6, fixedLength: true, unicode: false),
                        StationIpAddress = c.Int(nullable: false),
                        TableName = c.String(maxLength: 21, fixedLength: true, unicode: false),
                        TableDescription = c.String(maxLength: 50, fixedLength: true, unicode: false),
                        OperationType_OperationTypeId = c.Short(nullable: false),
                        Program_ProgramId = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        User_UserId = c.Short(nullable: false),
                        Department_DepartmentId = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("dbo.OperationTypes", t => t.OperationType_OperationTypeId)
                .ForeignKey("dbo.Programs", t => t.Program_ProgramId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Departments", t => t.Department_DepartmentId)
                .Index(t => t.OperationType_OperationTypeId)
                .Index(t => t.Program_ProgramId)
                .Index(t => t.User_UserId)
                .Index(t => t.Department_DepartmentId);
            
            CreateTable(
                "dbo.OperationTypes",
                c => new
                    {
                        OperationTypeId = c.Short(nullable: false, identity: true),
                        TypeName = c.String(maxLength: 30, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.OperationTypeId);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        ProgramId = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        ProgramName = c.String(maxLength: 30, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.ProgramId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Short(nullable: false, identity: true),
                        UserName = c.String(maxLength: 36, fixedLength: true, unicode: false),
                        NetworkUserName = c.String(maxLength: 41, fixedLength: true, unicode: false),
                        UserType_UserTypeId = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserTypes", t => t.UserType_UserTypeId)
                .Index(t => t.UserType_UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        UserTypeId = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        TypeName = c.String(maxLength: 30, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.UserTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Operations", "Department_DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Users", "UserType_UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Operations", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Operations", "Program_ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Operations", "OperationType_OperationTypeId", "dbo.OperationTypes");
            DropIndex("dbo.Users", new[] { "UserType_UserTypeId" });
            DropIndex("dbo.Operations", new[] { "Department_DepartmentId" });
            DropIndex("dbo.Operations", new[] { "User_UserId" });
            DropIndex("dbo.Operations", new[] { "Program_ProgramId" });
            DropIndex("dbo.Operations", new[] { "OperationType_OperationTypeId" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
            DropTable("dbo.Programs");
            DropTable("dbo.OperationTypes");
            DropTable("dbo.Operations");
            DropTable("dbo.Departments");
        }
    }
}
