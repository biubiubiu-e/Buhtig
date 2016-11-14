﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Buhtig.Configs;

namespace Buhtig
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RuntimeConfigs.LoadDefaults();
            if (!Directory.Exists(RuntimeConfigs.LocalWorkSpaceConfig.WorkingDir))
            {
                Directory.CreateDirectory(RuntimeConfigs.LocalWorkSpaceConfig.WorkingDir);
            }
        }
    }
}
