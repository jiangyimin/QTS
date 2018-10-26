using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QTS.Strategy.EntityFramework
{
    public class Strategy
    {
        public const int MaxNameLength = 20;
        public const int MaxTypeLength = 20;

        public int Id { get; set; }

        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        [StringLength(MaxTypeLength)]
        public string Type { get; set; }

        public string GMID { get; set; }

        [ForeignKey("StrategyId")]
        public List<Instrument> Instruments { get; set; }
    }
}
