﻿#pragma checksum "..\..\..\..\Hash Windows\Hash.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4DCEE6FBBACE6D1CF0CA1FE6D3CC55E04EE2B4BC48CD4799862084816FE5E06F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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


namespace SGet {
    
    
    /// <summary>
    /// Hash
    /// </summary>
    public partial class Hash : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 4 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SGet.Hash HashWindow;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbServerAddr;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddHashFile;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCheckSelectedFiles;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbHash;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel updatePanel;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer dgScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Hash Windows\Hash.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid hashGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/SGet;component/hash%20windows/hash.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Hash Windows\Hash.xaml"
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
            this.HashWindow = ((SGet.Hash)(target));
            return;
            case 2:
            this.lbServerAddr = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnAddHashFile = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\Hash Windows\Hash.xaml"
            this.btnAddHashFile.Click += new System.Windows.RoutedEventHandler(this.btnAddHashFile_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnCheckSelectedFiles = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\Hash Windows\Hash.xaml"
            this.btnCheckSelectedFiles.Click += new System.Windows.RoutedEventHandler(this.btnCheckSelectedFiles_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbHash = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.updatePanel = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 7:
            this.dgScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 8:
            this.hashGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

