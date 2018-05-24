using PresentationLayer.Models;

namespace PresentationLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertApplicationRoles : DbMigration
    {
        public override void Up()
        {
            Sql("insert into AspNetRoles (Id,Name) values(newid() , '"+Common.Adminstration +"');");
            /*insert into AspNetUserRoles(UserId, RoleId) values('866c3196-9772-4d58-a4af-dadadacb4122', '94EC03E3-075C-4728-9012-765A59DB3E60');*/
        }
        
        public override void Down()
        {
        }
    }
}
