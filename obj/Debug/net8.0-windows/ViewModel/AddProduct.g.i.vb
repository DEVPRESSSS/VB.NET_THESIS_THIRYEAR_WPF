﻿#ExternalChecksum("..\..\..\..\ViewModel\AddProduct.xaml","{ff1816ec-aa5e-4d10-87f7-6f4963833460}","13B77B02353B1F3720F7044C62254892655E4503")
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
'''AddProduct
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class AddProduct
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",54)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnMinimize As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",89)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnClose As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",128)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents ProductName As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",134)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Description As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",140)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents ComboCat As System.Windows.Controls.ComboBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",144)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Brand As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",152)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Insert As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",159)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Size As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",164)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Price As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",170)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Color As System.Windows.Controls.TextBox
    
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
        Dim resourceLocater As System.Uri = New System.Uri("/VB_THESIS_WPS;V1.0.0.0;component/viewmodel/addproduct.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",1)
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
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",17)
            AddHandler CType(target,AddProduct).MouseDown, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.Window_MouseDown)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            Me.btnMinimize = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",62)
            AddHandler Me.btnMinimize.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnMinimize_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            Me.btnClose = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",96)
            AddHandler Me.btnClose.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnClose_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 4) Then
            Me.ProductName = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",129)
            AddHandler Me.ProductName.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.ProductName_PreviewKeyDown)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",129)
            AddHandler Me.ProductName.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.ProductName_TextChanged)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 5) Then
            Me.Description = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",135)
            AddHandler Me.Description.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Description_PreviewKeyDown)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",135)
            AddHandler Me.Description.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Description_TextChanged)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.ComboCat = CType(target,System.Windows.Controls.ComboBox)
            Return
        End If
        If (connectionId = 7) Then
            Me.Brand = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",144)
            AddHandler Me.Brand.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Brand_PreviewKeyDown)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 8) Then
            Me.Insert = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",153)
            AddHandler Me.Insert.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Insert_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 9) Then
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",154)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Clear_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 10) Then
            Me.Size = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",160)
            AddHandler Me.Size.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Size_PreviewKeyDown)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",160)
            AddHandler Me.Size.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Size_TextChanged)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 11) Then
            Me.Price = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",165)
            AddHandler Me.Price.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Price_PreviewKeyDown)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",165)
            AddHandler Me.Price.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Price_TextChanged)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 12) Then
            Me.Color = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",171)
            AddHandler Me.Color.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Color_PreviewKeyDown)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\AddProduct.xaml",171)
            AddHandler Me.Color.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Color_TextChanged)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

