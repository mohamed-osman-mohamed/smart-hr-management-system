using DAL_Solution.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL_Solution.Data.Configrations
{
    public class BaseEntityConfigrations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GetDate()").IsRequired();
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GetDate()");
        }
    }
}
