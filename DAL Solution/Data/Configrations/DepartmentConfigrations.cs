using DAL_Solution.Models.Departments;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL_Solution.Data.Configrations
{
    internal class DepartmentConfigrations : BaseEntityConfigrations<Department>, IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Name).HasColumnType("VarChar(20)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("VarChar(10)").IsRequired();
            builder.Property(D => D.Description).HasColumnType("VarChar(100)").IsRequired(false);
            base.Configure(builder);
        }
    }
}
