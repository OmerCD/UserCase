using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using UserCase.Core.Entities;

namespace UserCase.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            builder.Property(e => e.Address).HasConversion(
                v => JsonConvert.SerializeObject(v, jsonSettings),
                v => JsonConvert.DeserializeObject<Address>(v, jsonSettings));
        }
    }
}
