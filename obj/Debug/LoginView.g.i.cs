﻿#pragma checksum "..\..\LoginView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4FC89E0DC4C1663244E4765BA8B1EB4A7C4181EA8208FCDC43E41BFF5F1012F7"
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


namespace siKecil {
    
    
    /// <summary>
    /// LoginView
    /// </summary>
    public partial class LoginView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUsername;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\LoginView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nextPage;
        
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
            System.Uri resourceLocater = new System.Uri("/siKecil;component/loginview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LoginView.xaml"
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
            
            #line 9 "..\..\LoginView.xaml"
            ((siKecil.LoginView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Login_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtUsername = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\LoginView.xaml"
            this.txtUsername.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 42 "..\..\LoginView.xaml"
            this.txtPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.PasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.nextPage = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\LoginView.xaml"
            this.nextPage.Click += new System.Windows.RoutedEventHandler(this.btnNext_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 79 "..\..\LoginView.xaml"
            ((System.Windows.Controls.Label)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Signup_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

