using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Somadhan.Persistence.EF.Data.EntityConfigurations.Views;

public class EmployeeAverageSalary
{
    public string Name { get; set; } = default!;
    public decimal AverageSalary { get; set; }
}

public class EmployeeAverageSalaryConfiguration : IEntityTypeConfiguration<EmployeeAverageSalary>
{
    public void Configure(EntityTypeBuilder<EmployeeAverageSalary> entity)
    {
        entity.HasNoKey();
        entity.ToView("employee_average_salary");

        entity.Property(e => e.Name)
              .HasColumnName("name")
              .IsRequired();

        entity.Property(e => e.AverageSalary)
              .HasColumnName("average_salary")
              .HasColumnType("decimal(18,2)");
    }
}
