﻿#pragma checksum "C:\Users\nmqhd\WindowsPhoneApp\MHSM_PhoneApp\WeatherInformationPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "44320B76032EB94AA1144ADF74ED90BE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MHSM_PhoneApp {
    
    
    public partial class WeatherInformationPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Canvas canvas;
        
        internal System.Windows.Media.Animation.Storyboard moveAnimation;
        
        internal System.Windows.Controls.Canvas LayoutRoot;
        
        internal System.Windows.Controls.Grid RootLayout;
        
        internal System.Windows.Controls.Grid TitleBar;
        
        internal System.Windows.Controls.Image DrawerIcon;
        
        internal System.Windows.Controls.Image avatarSmall;
        
        internal System.Windows.Controls.ListBox listBuilding;
        
        internal System.Windows.Controls.Button btnViewDetail;
        
        internal System.Windows.Controls.Button btnViewByDay;
        
        internal System.Windows.Controls.Grid grdCommands;
        
        internal System.Windows.Controls.Button btnHome;
        
        internal System.Windows.Controls.Button btnWeatherInformation;
        
        internal System.Windows.Controls.Button btnSendAlert1;
        
        internal System.Windows.Controls.Button btnLogin;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MHSM_PhoneApp;component/WeatherInformationPage.xaml", System.UriKind.Relative));
            this.canvas = ((System.Windows.Controls.Canvas)(this.FindName("canvas")));
            this.moveAnimation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("moveAnimation")));
            this.LayoutRoot = ((System.Windows.Controls.Canvas)(this.FindName("LayoutRoot")));
            this.RootLayout = ((System.Windows.Controls.Grid)(this.FindName("RootLayout")));
            this.TitleBar = ((System.Windows.Controls.Grid)(this.FindName("TitleBar")));
            this.DrawerIcon = ((System.Windows.Controls.Image)(this.FindName("DrawerIcon")));
            this.avatarSmall = ((System.Windows.Controls.Image)(this.FindName("avatarSmall")));
            this.listBuilding = ((System.Windows.Controls.ListBox)(this.FindName("listBuilding")));
            this.btnViewDetail = ((System.Windows.Controls.Button)(this.FindName("btnViewDetail")));
            this.btnViewByDay = ((System.Windows.Controls.Button)(this.FindName("btnViewByDay")));
            this.grdCommands = ((System.Windows.Controls.Grid)(this.FindName("grdCommands")));
            this.btnHome = ((System.Windows.Controls.Button)(this.FindName("btnHome")));
            this.btnWeatherInformation = ((System.Windows.Controls.Button)(this.FindName("btnWeatherInformation")));
            this.btnSendAlert1 = ((System.Windows.Controls.Button)(this.FindName("btnSendAlert1")));
            this.btnLogin = ((System.Windows.Controls.Button)(this.FindName("btnLogin")));
        }
    }
}

