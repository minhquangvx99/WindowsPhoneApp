#pragma checksum "C:\Users\nmqhd\WindowsPhoneApp\MHSM_PhoneApp\LoginPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8C1351DF640239FE2D1A915BB33297EC"
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
    
    
    public partial class LoginPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.TextBlock txtTitle;
        
        internal System.Windows.Controls.TextBox txtUsername;
        
        internal System.Windows.Controls.PasswordBox passwordBox;
        
        internal System.Windows.Controls.TextBox passwordTxtBox;
        
        internal System.Windows.Controls.CheckBox showPassword;
        
        internal System.Windows.Controls.Button btnLogin;
        
        internal System.Windows.Controls.Button btnLoginCus;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/MHSM_PhoneApp;component/LoginPage.xaml", System.UriKind.Relative));
            this.txtTitle = ((System.Windows.Controls.TextBlock)(this.FindName("txtTitle")));
            this.txtUsername = ((System.Windows.Controls.TextBox)(this.FindName("txtUsername")));
            this.passwordBox = ((System.Windows.Controls.PasswordBox)(this.FindName("passwordBox")));
            this.passwordTxtBox = ((System.Windows.Controls.TextBox)(this.FindName("passwordTxtBox")));
            this.showPassword = ((System.Windows.Controls.CheckBox)(this.FindName("showPassword")));
            this.btnLogin = ((System.Windows.Controls.Button)(this.FindName("btnLogin")));
            this.btnLoginCus = ((System.Windows.Controls.Button)(this.FindName("btnLoginCus")));
        }
    }
}

