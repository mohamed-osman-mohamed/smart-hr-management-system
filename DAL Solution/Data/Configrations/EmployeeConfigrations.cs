using DAL_Solution.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Solution.Data.Configrations
{
    public class EmployeeConfigrations : BaseEntityConfigrations<Employee> , IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("nvarchar(50)")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)")
                   .IsRequired();
            builder.Property(e => e.Gender)
                   .HasConversion((gender) => gender.ToString(),
                                  (toGender) => (Gender) Enum.Parse(typeof(Gender), toGender));
            builder.Property(e => e.EmployeeType)
                   .HasConversion((employeetype) => employeetype.ToString(),
                                  (toEmployeeType) => (EmployeeType) Enum.Parse(typeof(EmployeeType), toEmployeeType));
            builder.OwnsOne(e => e.Address , EmpAdderss => EmpAdderss.WithOwner());
            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
            base.Configure(builder);
        }
    }
}
