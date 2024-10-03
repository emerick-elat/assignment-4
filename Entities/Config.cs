using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    internal sealed class Config : IConfig
    {
        public required string Path { get; set; }
        public required string SystemAccountNumber { get; set; }
        public required decimal InitialBalance { get; set; }
        public required string ConnectionString { get; set; }
    }
}
