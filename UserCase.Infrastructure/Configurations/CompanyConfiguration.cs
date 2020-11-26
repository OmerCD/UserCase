using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserCase.Core.Entities.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(new Company()
            {
                Id = 1,
                Bs = "harness real-time e-markets",
                Name = "Romaguera-Crona",
                CatchPhrase = "Multi-layered client-server neural-net"
            });
        }
    }
}