﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BootVerhuurWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Nzc2NjE5QDMyMzAyZTMzMmUzMGd1bDJuL0U4UUZJOVpmZVlBWjYvSU9uazROWXJITUg4blFINmc0SEtEaE09");
        }
    }
}
