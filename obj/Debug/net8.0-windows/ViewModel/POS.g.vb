﻿#ExternalChecksum("..\..\..\..\ViewModel\POS.xaml","{ff1816ec-aa5e-4d10-87f7-6f4963833460}","F6C7036A9859E5A4E9E6CE5A44F6198B1CC6EAA1")
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
Imports MahApps.Metro.IconPacks.Converter
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
'''POS
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class POS
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",85)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents LogoutBtn As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",104)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnMaximize As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",143)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnMinimize As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",179)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnClose As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",225)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents shoeswrapper As System.Windows.Controls.ColumnDefinition
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",234)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents ProductList As System.Windows.Controls.ItemsControl
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",271)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents items As System.Windows.Controls.DataGrid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",304)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Subtotal As System.Windows.Controls.TextBlock
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",315)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Subtotalcount As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",323)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Change As System.Windows.Controls.TextBlock
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",333)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents ChangeCount As System.Windows.Controls.Label
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",341)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Change_Copy As System.Windows.Controls.TextBlock
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",371)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Search As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",384)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents SearchBtn As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",395)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents sellbtn As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\POS.xaml",406)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Bill As System.Windows.Controls.TextBox
    
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
        Dim resourceLocater As System.Uri = New System.Uri("/VB_THESIS_WPS;component/viewmodel/pos.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\..\ViewModel\POS.xaml",1)
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
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",15)
            AddHandler CType(target,POS).MouseDown, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.Window_MouseDown)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            Me.LogoutBtn = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",87)
            AddHandler Me.LogoutBtn.Click, New System.Windows.RoutedEventHandler(AddressOf Me.LogoutBtn_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            Me.btnMaximize = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",111)
            AddHandler Me.btnMaximize.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnMaximize_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 4) Then
            Me.btnMinimize = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",149)
            AddHandler Me.btnMinimize.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnMinimize_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 5) Then
            Me.btnClose = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",187)
            AddHandler Me.btnClose.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnClose_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.shoeswrapper = CType(target,System.Windows.Controls.ColumnDefinition)
            Return
        End If
        If (connectionId = 7) Then
            Me.ProductList = CType(target,System.Windows.Controls.ItemsControl)
            Return
        End If
        If (connectionId = 9) Then
            Me.items = CType(target,System.Windows.Controls.DataGrid)
            Return
        End If
        If (connectionId = 12) Then
            Me.Subtotal = CType(target,System.Windows.Controls.TextBlock)
            Return
        End If
        If (connectionId = 13) Then
            Me.Subtotalcount = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 14) Then
            Me.Change = CType(target,System.Windows.Controls.TextBlock)
            Return
        End If
        If (connectionId = 15) Then
            Me.ChangeCount = CType(target,System.Windows.Controls.Label)
            Return
        End If
        If (connectionId = 16) Then
            Me.Change_Copy = CType(target,System.Windows.Controls.TextBlock)
            Return
        End If
        If (connectionId = 17) Then
            Me.Search = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",378)
            AddHandler Me.Search.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Search_TextChanged)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 18) Then
            Me.SearchBtn = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",385)
            AddHandler Me.SearchBtn.Click, New System.Windows.RoutedEventHandler(AddressOf Me.SearchBtn_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 19) Then
            Me.sellbtn = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",396)
            AddHandler Me.sellbtn.Click, New System.Windows.RoutedEventHandler(AddressOf Me.sellbtn_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 20) Then
            Me.Bill = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",412)
            AddHandler Me.Bill.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Search_TextChanged)
            
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
        If (connectionId = 8) Then
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",256)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.AddItem_Click)
            
            #End ExternalSource
        End If
        If (connectionId = 10) Then
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",285)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Button_Click)
            
            #End ExternalSource
        End If
        If (connectionId = 11) Then
            
            #ExternalSource("..\..\..\..\ViewModel\POS.xaml",290)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Button_Click_1)
            
            #End ExternalSource
        End If
    End Sub
End Class

