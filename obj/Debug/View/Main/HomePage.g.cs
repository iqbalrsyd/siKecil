﻿#pragma checksum "..\..\..\..\View\Main\HomePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "88A343E531BA51D1F368A3EF7B50200ADEF5F0CF3D52DE22847261E9F6408750"
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
using siKecil.Infrastructure;


namespace siKecil.View.Main {
    
    
    /// <summary>
    /// HomePage
    /// </summary>
    public partial class HomePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock greetingText;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEventTitle;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar calendar;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageTextBlock;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageTextBlock1;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageTextBlock2;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageTextBlock3;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ageInMonths;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\View\Main\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image childImage;
        
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
            System.Uri resourceLocater = new System.Uri("/siKecil;component/view/main/homepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Main\HomePage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.greetingText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.txtEventTitle = ((System.Windows.Controls.TextBox)(target));
            
            #line 49 "..\..\..\..\View\Main\HomePage.xaml"
            this.txtEventTitle.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtEventTitle_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.calendar = ((System.Windows.Controls.Calendar)(target));
            
            #line 54 "..\..\..\..\View\Main\HomePage.xaml"
            this.calendar.Loaded += new System.Windows.RoutedEventHandler(this.Calendar_Loaded);
            
            #line default
            #line hidden
            
            #line 54 "..\..\..\..\View\Main\HomePage.xaml"
            this.calendar.SelectedDatesChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.Calendar_SelectedDatesChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 69 "..\..\..\..\View\Main\HomePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteSchedule_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MessageTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.MessageTextBlock1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.MessageTextBlock2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            
            #line 81 "..\..\..\..\View\Main\HomePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveSchedule_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.MessageTextBlock3 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.ageInMonths = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.childImage = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

