﻿#pragma checksum "..\..\..\..\Authentication\SignUpView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "418DA18D79F751B7419207725F2039DE6FBAA537"
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
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using WalletsWPF.Authentication;


namespace WalletsWPF.Authentication {
    
    
    /// <summary>
    /// SignUpView
    /// </summary>
    public partial class SignUpView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\Authentication\SignUpView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbLogin;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Authentication\SignUpView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbLastName;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Authentication\SignUpView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox TbPassword;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Authentication\SignUpView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BSignUp;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Authentication\SignUpView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BClose;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Authentication\SignUpView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BGotoSignIn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WalletsWPF;V1.0.0.0;component/authentication/signupview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Authentication\SignUpView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TbLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.TbLastName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TbPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 32 "..\..\..\..\Authentication\SignUpView.xaml"
            this.TbPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.TbPassword_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BSignUp = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.BClose = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.BGotoSignIn = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
