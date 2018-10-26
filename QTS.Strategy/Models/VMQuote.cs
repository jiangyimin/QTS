using GMSDK;
using Caliburn.Micro;
using System;

namespace QTS.Strategy.Models
{
    public class VMQuote : PropertyChangedBase
    {
        private readonly Quote quote;
        public VMQuote(Quote quote)
        {
            this.quote = quote;
        }
        
        public float BidPrice
        {
            get { return quote.bidPrice; }
        }
        public long BidVolumn
        {
            get { return quote.bidVolume; }
        }

        public float AskPrice
        {
            get { return quote.askPrice; }
        }
        public long AskVolumn
        {
            get { return quote.askVolume; }
        }

    }
}
