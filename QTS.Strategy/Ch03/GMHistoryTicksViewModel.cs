using System;
using System.Linq;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Windows.Threading;
using QTS.Strategy.Models;
using GMSDK;
using System.Windows.Controls;

namespace QTS.Strategy.Ch03
{
    [Export(typeof(IScreen)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class GMHistoryTicksViewModel : Screen
    {
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
        private readonly IEventAggregator _events;

        [ImportingConstructor]
        public GMHistoryTicksViewModel(IEventAggregator events)
        {
            this._events = events;
            DisplayName = "03. Hist Ticks";
            HistTicks = new BindableCollection<VMTick>();
            HistQuotes = new BindableCollection<VMQuote>();

            this.Activated += GMHistoryTicksViewModel_Activated;
        }

        private void GMHistoryTicksViewModel_Activated(object sender, ActivationEventArgs e)
        {
            if (App.CurrentInstrument != null)
                Symbol = App.CurrentInstrument.Symbol;
        }

        public BindableCollection<VMTick> HistTicks { get; set; }
        public BindableCollection<VMQuote> HistQuotes { get; set; }

        private string symbol = "SZSE.300144";
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                NotifyOfPropertyChange(() => Symbol);
            }
        }

        private string startTime = "2018-10-23 9:0:0";
        public string StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                NotifyOfPropertyChange(() => StartTime);
            }
        }

        private string endTime = "2018-10-23 15:0:0";
        public string EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                NotifyOfPropertyChange(() => EndTime);
            }
        }

        public void GetHistTicks()
        {
            int ret = GMApi.SetToken(App.GMToken);
            if (ret != 0) return;
            HistTicks.Clear();

            GMDataList<Tick> ticks = GMApi.HistoryTicks(symbol, startTime, endTime);

            if (ticks.status == 0)
            {
                foreach (Tick tick in ticks.data)
                    HistTicks.Add(new VMTick(tick));

                _events.PublishOnUIThread(new ModelEvents(new List<object>(new
                   object[] { "Get Ticks number = " + ticks.data.Count })));
            }
        }

        public void HistTicks_SelectionChanged(DataGrid dg)
        {
            int rowindex = dg.SelectedIndex;

            HistQuotes.Clear();
            var v = HistTicks[rowindex].Quotes;
            for (int i = 0; i < v.Length; i++)
            {
                Quote q = v[i];
                if (q != null)
                    HistQuotes.Add(new VMQuote(q));
            }
        }
    }
}
