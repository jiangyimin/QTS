using System.Collections.Generic;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Reflection;
using QTS.Strategy.EntityFramework;
using QTS.Strategy.Models;
using System.Linq;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using System;

namespace QTS.Strategy.Ch01
{
    [Export(typeof(IScreen)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddStrategyViewModel : Screen
    {
        private readonly IEventAggregator _events;
        [ImportingConstructor]
        public AddStrategyViewModel(IEventAggregator events)
        {
            this._events = events;
            DisplayName = "01. 添加策略";

            StrategyCollection = new BindableCollection<EntityFramework.Strategy>();
            InstrumentCollection = new BindableCollection<Instrument>();

            using (var context = new QTSDbContext())
            {
                StrategyCollection.AddRange(context.Strategies.Include(s => s.Instruments).ToList());
            }
        }

        public BindableCollection<EntityFramework.Strategy> StrategyCollection { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
       
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }

        public void AddStrategy()
        {
            if (string.IsNullOrEmpty(Name)) return;

            // Save to DB
            using (var context = new QTSDbContext())
            {
                var strategy = new EntityFramework.Strategy();
                strategy.Name = Name;
                strategy.Type = Type;
                context.Strategies.Add(strategy);
                context.SaveChanges();

                StrategyCollection.Add(strategy);
                _events.PublishOnUIThread(new ModelEvents(new List<object>(new
                    object[] { "Add strategy to DB: name = " + Name })));
            }
        }

        public void StrategyCollection_SelectionChanged(DataGrid dg)
        {
            int rowindex = dg.SelectedIndex;
            App.CurrentStrategy = StrategyCollection[rowindex];

            InstrumentCollection.Clear();
            InstrumentCollection.AddRange(App.CurrentStrategy.Instruments);
        }

        #region Instrument
        public BindableCollection<Instrument> InstrumentCollection { get; set; }

        private string name2;
        public string Name2
        {
            get { return name2; }
            set
            {
                name2 = value;
                NotifyOfPropertyChange(() => Name2);
            }
        }

        private string cn;
        public string Cn
        {
            get { return cn; }
            set
            {
                cn = value;
                NotifyOfPropertyChange(() => Cn);
            }
        }

        private string exchange;
        public string Exchange
        {
            get { return exchange; }
            set
            {
                exchange = value;
                NotifyOfPropertyChange(() => Exchange);
            }
        }

        public void AddInstrument()
        {
            if (App.CurrentStrategy == null || string.IsNullOrEmpty(Cn)) return;

            // Save to DB
            using (var context = new QTSDbContext())
            {
                var strategy = context.Strategies.FirstOrDefault(s => s.Id == App.CurrentStrategy.Id);

                var instrument = new Instrument();
                instrument.Cn = Cn;
                instrument.Name = Name2;
                instrument.Exchange = Exchange;
                if (strategy.Instruments == null)
                    strategy.Instruments = new List<Instrument>();
                strategy.Instruments.Add(instrument);
                context.SaveChanges();

                InstrumentCollection.Add(instrument);
                _events.PublishOnUIThread(new ModelEvents(new List<object>(new
                    object[] { "Add instrument to Current Strategy: name = " + Name2 })));
            }
        }

        public void InstrumentCollection_SelectionChanged(DataGrid dg)
        {
            int rowindex = dg.SelectedIndex;
            App.CurrentInstrument = InstrumentCollection[rowindex];
        }

        #endregion
    }
}
