using System;
using System.Linq;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Windows.Threading;
using QTS.Strategy.Models;
using GMSDK;

namespace QTS.Strategy.Ch03
{
    [Export(typeof(IScreen)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class GMHistoryBarsViewModel : Screen
    {
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
        private readonly IEventAggregator _events;

        [ImportingConstructor]
        public GMHistoryBarsViewModel(IEventAggregator events)
        {
            this._events = events;
            DisplayName = "03. Hist Bars";
            HistBars = new BindableCollection<VMBar>();

            this.Activated += GMHistoryBarsViewModel_Activated;
        }

        private void GMHistoryBarsViewModel_Activated(object sender, ActivationEventArgs e)
        {
            if (App.CurrentInstrument != null)
                Symbol = App.CurrentInstrument.Symbol;
        }

        public BindableCollection<VMBar> HistBars { get; set; }
  
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

        private string startTime = "2018-3-12";
        public string StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                NotifyOfPropertyChange(() => StartTime);
            }
        }

        private string endTime = "2018-10-24";
        public string EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                NotifyOfPropertyChange(() => EndTime);
            }
        }

        private string frequency = "1d";
        public string Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
                NotifyOfPropertyChange(() => Frequency);
            }
        }

        public void GetHistBars()
        {
            int ret = GMApi.SetToken(App.GMToken);
            if (ret != 0) return;
            HistBars.Clear();

            GMDataList<Bar> bars = GMApi.HistoryBars(symbol, frequency, startTime, endTime);

            HistBars.Clear();
            if (bars.status == 0)
            {
                //HistBars.AddRange(bars.data);
                foreach (Bar bar in bars.data)
                    HistBars.Add(new VMBar(bar));

                _events.PublishOnUIThread(new ModelEvents(new List<object>(new
                   object[] { "Get Bars number = " + bars.data.Count })));

            }
        }     
    }
}
