namespace OperationLog.Database.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DropNetworkNameTableDescription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Operations", "TableDescription");
            DropColumn("dbo.Users", "NetworkUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "NetworkUserName", c => c.String(maxLength: 41, fixedLength: true, unicode: false));
            AddColumn("dbo.Operations", "TableDescription", c => c.String(maxLength: 50, fixedLength: true, unicode: false));
        }
    }
}
