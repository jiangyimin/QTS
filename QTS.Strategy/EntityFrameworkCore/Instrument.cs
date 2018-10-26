using System.ComponentModel.DataAnnotations;

namespace QTS.Strategy.EntityFramework
{
    public class Instrument
    {
        public const int MaxCnLength = 6;
        public const int MaxNameLength = 10;
        public const int MaxExchangeLength = 6;

        public int Id { get; set; }

        [StringLength(MaxCnLength)]
        public string Cn { get; set; }

        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        [StringLength(MaxExchangeLength)]
        public string Exchange { get; set; }

        public string Symbol
        {
            get { return string.Format("{0}.{1}", Exchange, Cn); }
        }
    }
}
