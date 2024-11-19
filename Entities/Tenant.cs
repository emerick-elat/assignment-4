using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string Server {  get; set; }
        public string DatabaseName {  get; set; }
        public string? DBUser {  get; set; }
        public string? DBPassword {  get; set; }
        public bool IsTrustedConnection {  get; set; }
        public bool TrusServerCertificate {  get; set; }

        [NotMapped]
        public string? DBConnectionString { 
            get => DBUser is not null && DBPassword is not null  
                ? $"Server={Server};Database={DatabaseName};User={DBUser};Password={DBPassword};Trusted_Connection={IsTrustedConnection};TrustServerCertificate={TrusServerCertificate};"
                : $"Server={Server};Database={DatabaseName};Trusted_Connection={IsTrustedConnection};TrustServerCertificate={TrusServerCertificate};"; 
        }
    }
}
