using GMSDK;
using Caliburn.Micro;
using System;
using System.Collections.Generic;

namespace QTS.Strategy.Models
{
    public class VMTick
    {
        private readonly Tick tick;
        public VMTick(Tick tick)
        {
            this.tick = tick;
        }
        
        public string Symbol
        {
            get { return tick.symbol; }
        }

        public DateTime CreateAt
        {
            get { return tick.createdAt; }
        }
        public float Price
        {
            get { return tick.price; }
        }
        public float Open
        {
            get { return tick.open; }
        }

        public float High
        {
            get { return tick.high; }
        }

        public float Low
        {
            get { return tick.low; }
        }

        public double CumVolumn
        {
            get { return tick.cumVolume; }
        }

        public double CumAmount
        {
            get { return tick.cumAmount; }
        }
        public int LastVolumn
        {
            get { return tick.lastVolume; }
        }

        public double LastAmount
        {
            get { return tick.lastAmount; }
        }

        public Quote[] Quotes
        {
            get { return tick.quotes; }
        }
    }
}
