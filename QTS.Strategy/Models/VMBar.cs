using GMSDK;
using Caliburn.Micro;
using System;

namespace QTS.Strategy.Models
{
    public class VMBar : PropertyChangedBase
    {
        private Bar bar;
        public VMBar(Bar bar)
        {
            this.bar = bar;
        }
        
        public string Symbol
        {
            get { return bar.symbol; }
        }

        public DateTime BOB
        {
            get { return bar.bob; }
        }
        public float Open
        {
            get { return bar.open; }
        }

        public float Close
        {
            get { return bar.close; }
        }

        public float High
        {
            get { return bar.high; }
        }

        public float Low
        {
            get { return bar.low; }
        }

        public double Volumn
        {
            get { return bar.volume; }
        }

        public double Amount
        {
            get { return bar.amount; }
        }
    }
}
