﻿#ExternalChecksum("..\..\..\MainWindow.xaml","{ff1816ec-aa5e-4d10-87f7-6f4963833460}","9B83C5B83B1863F5836BBFE443553C5E657FB255")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports MahApps.Metro.IconPacks
Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Controls.Ribbon
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell
Imports VB_THESIS_WPS


'''<summary>
'''MainWindow
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class MainWindow
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\MainWindow.xaml",116)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Inventory As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\MainWindow.xaml",188)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents MainContentArea As System.Windows.Controls.ContentControl
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\MainWindow.xaml",224)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnMinimize As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\MainWindow.xaml",267)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnMaximize As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\MainWindow.xaml",312)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnClose As System.Windows.Controls.Button
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0")>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/VB_THESIS_WPS;component/mainwindow.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\MainWindow.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            
            #ExternalSource("..\..\..\MainWindow.xaml",10)
            AddHandler CType(target,MainWindow).MouseDown, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.Window_MouseDown)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            
            #ExternalSource("..\..\..\MainWindow.xaml",98)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Button_Click_2)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            
            #ExternalSource("..\..\..\MainWindow.xaml",108)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Button_Click_1)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 4) Then
            Me.Inventory = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\MainWindow.xaml",117)
            AddHandler Me.Inventory.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Inventory_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 5) Then
            
            #ExternalSource("..\..\..\MainWindow.xaml",129)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Sales_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            
            #ExternalSource("..\..\..\MainWindow.xaml",140)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Analysis_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 7) Then
            
            #ExternalSource("..\..\..\MainWindow.xaml",150)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.EmployeeClick)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 8) Then
            
            #ExternalSource("..\..\..\MainWindow.xaml",163)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Logout)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 9) Then
            Me.MainContentArea = CType(target,System.Windows.Controls.ContentControl)
            Return
        End If
        If (connectionId = 10) Then
            Me.btnMinimize = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\MainWindow.xaml",231)
            AddHandler Me.btnMinimize.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnMinimize_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 11) Then
            Me.btnMaximize = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\MainWindow.xaml",274)
            AddHandler Me.btnMaximize.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnMaximize_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 12) Then
            Me.btnClose = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\MainWindow.xaml",319)
            AddHandler Me.btnClose.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnClose_Click)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

