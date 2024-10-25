﻿#ExternalChecksum("..\..\..\..\ViewModel\Inventory.xaml","{ff1816ec-aa5e-4d10-87f7-6f4963833460}","8FC9B94CABBBD59EE9E44854D478C219327C75F0")
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
'''Inventory
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class Inventory
    Inherits System.Windows.Controls.UserControl
    Implements System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector
    
    
    #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",69)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Search As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",88)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Print As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",98)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Images As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",119)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents productDataGrid As System.Windows.Controls.DataGrid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",157)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents datePickerFilter As System.Windows.Controls.DatePicker
    
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
        Dim resourceLocater As System.Uri = New System.Uri("/VB_THESIS_WPS;component/viewmodel/inventory.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",1)
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
            
            #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",58)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.AddNewProduct_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            Me.Search = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",75)
            AddHandler Me.Search.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Search_TextChanged)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            
            #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",78)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Search_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 4) Then
            Me.Print = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",89)
            AddHandler Me.Print.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Print_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 5) Then
            Me.Images = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",99)
            AddHandler Me.Images.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Images_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.productDataGrid = CType(target,System.Windows.Controls.DataGrid)
            Return
        End If
        If (connectionId = 8) Then
            Me.datePickerFilter = CType(target,System.Windows.Controls.DatePicker)
            
            #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",158)
            AddHandler Me.datePickerFilter.SelectedDateChanged, New System.EventHandler(Of System.Windows.Controls.SelectionChangedEventArgs)(AddressOf Me.datePickerFilter_SelectedDateChanged)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.3.0"),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")>  _
    Sub System_Windows_Markup_IStyleConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IStyleConnector.Connect
        If (connectionId = 7) Then
            
            #ExternalSource("..\..\..\..\ViewModel\Inventory.xaml",136)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.EditBtn)
            
            #End ExternalSource
        End If
    End Sub
End Class

