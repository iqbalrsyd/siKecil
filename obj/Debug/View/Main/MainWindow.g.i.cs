﻿#pragma checksum "..\..\..\..\View\Main\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "23AA2256D4B82798A2932E25A7FE7A9F2E8A04021078D8808B08C043045CD495"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using siKecil;


namespace siKecil.View.Main {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MenuIcon;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image BellIcon;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image HomeIcon;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image UserIcon;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image NotesIcon;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ChatIcon;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image InfoIcon;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image BookIcon;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\..\View\Main\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame mainFrame;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/siKecil;component/view/main/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Main\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MenuIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.BellIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            
            #line 77 "..\..\..\..\View\Main\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.HomePage_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.HomeIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            
            #line 90 "..\..\..\..\View\Main\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ProfilePage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.UserIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            
            #line 103 "..\..\..\..\View\Main\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DiaryAnakPage_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.NotesIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 9:
            this.ChatIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.InfoIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.BookIcon = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.mainFrame = ((System.Windows.Controls.Frame)(target));
            
            #line 156 "..\..\..\..\View\Main\MainWindow.xaml"
            this.mainFrame.Navigated += new System.Windows.Navigation.NavigatedEventHandler(this.mainFrame_Navigated);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

