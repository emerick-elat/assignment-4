﻿namespace Entities
{
    public class Currency
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required char Symbol { get; set; }
        public required decimal ValueToUSD { get; set; }
        

    }
}
