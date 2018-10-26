using System;
using System.Data;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using GMSDK;

namespace QTS.Strategy.Ch03
{
    [Export(typeof(IScreen)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class GMFinanceViewModel : Screen
    {
        private readonly IEventAggregator _events;
        [ImportingConstructor]
        public GMFinanceViewModel(IEventAggregator events)
        {
            this._events = events;
            DisplayName = "03. GMFinance";
        }

        private string startDate = "2008-1-1";
        private string endDate = "2018-10-25";

        private string symbol = "SHSE.600000";
        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                NotifyOfPropertyChange(() => Symbol);
            }
        }

        private DataTable myTable = new DataTable();
        public DataTable MyTable
        {
            get { return myTable; }
            set
            {
                myTable = value;
                NotifyOfPropertyChange(() => MyTable);
            }
        }

        private string dataType = "主要财务指标:";
        public string DataType
        {
            get { return dataType; }
            set
            {
                dataType = value;
                NotifyOfPropertyChange(() => DataType);
            }
        }

        public void PrimFinanceIndicator()
        {
            DataType = "主要财务指标:";
            GMApi.SetToken(App.GMToken);
            GMData<DataTable> tbl = GMApi.GetFundamentalsN("prim_finance_indicator", Symbol, endDate, 30,
                "EBIT, EPSBASIC, OPNCFPS, ROEDILUTED");

            if (tbl.status == 0)
                MyTable = tbl.data;
        }

        public void DerivFinanceIndicator()
        {
            DataType = "deriv_finance_indicator:";
            GMApi.SetToken(App.GMToken);
            GMData<DataTable> tbl = GMApi.GetFundamentalsN("deriv_finance_indicator", symbol, endDate, 30,
                "ACCDEPRT,ACCPAYRT,ACCPAYTDAYS,ASSLIABRT,CASHCONVCYCLE,CRPS,CURRENTRT,EBIT,NFART");

            if (tbl.status == 0)
                MyTable = tbl.data;
        }

        public void IncomeStatement()
        {
            DataType = "income_statement:";
            GMApi.SetToken(App.GMToken);
            GMData<DataTable> tbl = GMApi.GetFundamentalsN("income_statement", symbol, endDate, 30,
                "ASSEIMPALOSS, BASICEPS, BIZCOST, BIZINCO");

            if (tbl.status == 0)
                MyTable = tbl.data;
        }

        public void BarData()
        {
            DataType = "Bar Data:";
        }
    }
}
