﻿#pragma checksum "..\..\..\AttendanceWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "77D948B5160547BCC28B38B0AE5FD6030BBDC28D"
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


namespace WpfApp {
    
    
    /// <summary>
    /// AttendanceWindow
    /// </summary>
    public partial class AttendanceWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\AttendanceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgAttendanceList;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\AttendanceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PresentButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\AttendanceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AbsentButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\AttendanceWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ViewButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp;component/attendancewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AttendanceWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dgAttendanceList = ((System.Windows.Controls.DataGrid)(target));
            
            #line 7 "..\..\..\AttendanceWindow.xaml"
            this.dgAttendanceList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgAttendanceList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PresentButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\AttendanceWindow.xaml"
            this.PresentButton.Click += new System.Windows.RoutedEventHandler(this.PresentButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AbsentButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\AttendanceWindow.xaml"
            this.AbsentButton.Click += new System.Windows.RoutedEventHandler(this.AbsentButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ViewButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\AttendanceWindow.xaml"
            this.ViewButton.Click += new System.Windows.RoutedEventHandler(this.ViewAttendanceButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

