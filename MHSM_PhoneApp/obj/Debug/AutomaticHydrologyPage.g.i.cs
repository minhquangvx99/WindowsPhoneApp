#pragma checksum "C:\Work\Code\MHSM_PhoneApp\MHSM_PhoneApp\AutomaticHydrologyPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1D5D30BA1CDFA5088B80E196D3A0F71A"
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
    
    
    public partial class AutomaticHydrologyPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid RootLayout;
        
        internal System.Windows.Controls.Grid TitleBar;
        
        internal System.Windows.Controls.Image DrawerIcon;
        
        internal System.Windows.Controls.Button btnBack;
        
        internal System.Windows.Controls.Image avatarSmall;
        
        internal System.Windows.Controls.TextBox txtStationName;
        
        internal Microsoft.Phone.Controls.ListPicker mySizeList;
        
        internal Microsoft.Phone.Controls.ListPicker myProvinceList;
        
        internal System.Windows.Controls.Button btnSearch;
        
        internal System.Windows.Controls.Button btnDeleteFilter;
        
        internal System.Windows.Controls.TextBlock txtTitle3;
        
        internal System.Windows.Controls.ListBox listBuilding;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MHSM_PhoneApp;component/AutomaticHydrologyPage.xaml", System.UriKind.Relative));
            this.RootLayout = ((System.Windows.Controls.Grid)(this.FindName("RootLayout")));
            this.TitleBar = ((System.Windows.Controls.Grid)(this.FindName("TitleBar")));
            this.DrawerIcon = ((System.Windows.Controls.Image)(this.FindName("DrawerIcon")));
            this.btnBack = ((System.Windows.Controls.Button)(this.FindName("btnBack")));
            this.avatarSmall = ((System.Windows.Controls.Image)(this.FindName("avatarSmall")));
            this.txtStationName = ((System.Windows.Controls.TextBox)(this.FindName("txtStationName")));
            this.mySizeList = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("mySizeList")));
            this.myProvinceList = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("myProvinceList")));
            this.btnSearch = ((System.Windows.Controls.Button)(this.FindName("btnSearch")));
            this.btnDeleteFilter = ((System.Windows.Controls.Button)(this.FindName("btnDeleteFilter")));
            this.txtTitle3 = ((System.Windows.Controls.TextBlock)(this.FindName("txtTitle3")));
            this.listBuilding = ((System.Windows.Controls.ListBox)(this.FindName("listBuilding")));
        }
    }
}

