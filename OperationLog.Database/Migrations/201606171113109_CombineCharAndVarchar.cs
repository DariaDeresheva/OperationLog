namespace OperationLog.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CombineCharAndVarchar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Operations", "Program_ProgramId", "dbo.Programs");
            DropForeignKey("dbo.Users", "UserType_UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.Operations", new[] { "Program_ProgramId" });
            DropIndex("dbo.Users", new[] { "UserType_UserTypeId" });
            DropPrimaryKey("dbo.Programs");
            DropPrimaryKey("dbo.UserTypes");
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.Operations", "StationAddress", c => c.String(maxLength: 6, fixedLength: true, unicode: false));
            AlterColumn("dbo.Operations", "TableName", c => c.String(maxLength: 21, unicode: false));
            AlterColumn("dbo.Operations", "Program_ProgramId", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("dbo.OperationTypes", "TypeName", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.Programs", "ProgramId", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("dbo.Programs", "ProgramName", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(maxLength: 36, unicode: false));
            AlterColumn("dbo.Users", "UserType_UserTypeId", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("dbo.UserTypes", "UserTypeId", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("dbo.UserTypes", "TypeName", c => c.String(maxLength: 30, unicode: false));
            AddPrimaryKey("dbo.Programs", "ProgramId");
            AddPrimaryKey("dbo.UserTypes", "UserTypeId");
            CreateIndex("dbo.Operations", "Program_ProgramId");
            CreateIndex("dbo.Users", "UserType_UserTypeId");
            AddForeignKey("dbo.Operations", "Program_ProgramId", "dbo.Programs", "ProgramId");
            AddForeignKey("dbo.Users", "UserType_UserTypeId", "dbo.UserTypes", "UserTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserType_UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Operations", "Program_ProgramId", "dbo.Programs");
            DropIndex("dbo.Users", new[] { "UserType_UserTypeId" });
            DropIndex("dbo.Operations", new[] { "Program_ProgramId" });
            DropPrimaryKey("dbo.UserTypes");
            DropPrimaryKey("dbo.Programs");
            AlterColumn("dbo.UserTypes", "TypeName", c => c.String(maxLength: 30));
            AlterColumn("dbo.UserTypes", "UserTypeId", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.Users", "UserType_UserTypeId", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.Users", "UserName", c => c.String(maxLength: 36));
            AlterColumn("dbo.Programs", "ProgramName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Programs", "ProgramId", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.OperationTypes", "TypeName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Operations", "Program_ProgramId", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.Operations", "TableName", c => c.String(maxLength: 21));
            AlterColumn("dbo.Operations", "StationAddress", c => c.String(maxLength: 6));
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String(maxLength: 30));
            AddPrimaryKey("dbo.UserTypes", "UserTypeId");
            AddPrimaryKey("dbo.Programs", "ProgramId");
            CreateIndex("dbo.Users", "UserType_UserTypeId");
            CreateIndex("dbo.Operations", "Program_ProgramId");
            AddForeignKey("dbo.Users", "UserType_UserTypeId", "dbo.UserTypes", "UserTypeId");
            AddForeignKey("dbo.Operations", "Program_ProgramId", "dbo.Programs", "ProgramId");
        }
    }
}
