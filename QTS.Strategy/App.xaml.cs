using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QTS.Strategy
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // Global
        public static string GMToken = "53c431a3d8227b670c4bc6198f7570abc361b532";
        public static EntityFramework.Strategy CurrentStrategy = null;
        public static EntityFramework.Instrument CurrentInstrument = null;
    }
}
