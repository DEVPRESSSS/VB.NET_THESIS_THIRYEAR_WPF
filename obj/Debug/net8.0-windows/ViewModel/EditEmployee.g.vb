﻿#ExternalChecksum("..\..\..\..\ViewModel\EditEmployee.xaml","{ff1816ec-aa5e-4d10-87f7-6f4963833460}","B1F1B5C94CBDBA2EDB5238318BDC5D662C2CBC7C")
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
'''EditEmployee
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class EditEmployee
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",71)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnMinimize As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",106)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents btnClose As System.Windows.Controls.Button
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",142)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Username As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",162)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Firstname As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",178)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Lastname As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",195)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Email As System.Windows.Controls.TextBox
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",216)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Update As System.Windows.Controls.Button
    
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
        Dim resourceLocater As System.Uri = New System.Uri("/VB_THESIS_WPS;component/viewmodel/editemployee.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",1)
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
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",16)
            AddHandler CType(target,EditEmployee).MouseDown, New System.Windows.Input.MouseButtonEventHandler(AddressOf Me.Window_MouseDown)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",17)
            AddHandler CType(target,EditEmployee).Loaded, New System.Windows.RoutedEventHandler(AddressOf Me.Window_Loaded)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 2) Then
            Me.btnMinimize = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",76)
            AddHandler Me.btnMinimize.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnMinimize_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 3) Then
            Me.btnClose = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",113)
            AddHandler Me.btnClose.Click, New System.Windows.RoutedEventHandler(AddressOf Me.btnClose_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 4) Then
            Me.Username = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",150)
            AddHandler Me.Username.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Username_PreviewKeyDown)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",151)
            AddHandler Me.Username.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Username_TextChanged)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 5) Then
            Me.Firstname = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",170)
            AddHandler Me.Firstname.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Firstname_PreviewKeyDown)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 6) Then
            Me.Lastname = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",186)
            AddHandler Me.Lastname.LostFocus, New System.Windows.RoutedEventHandler(AddressOf Me.Lastname_LostFocus)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",187)
            AddHandler Me.Lastname.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Lastname_PreviewKeyDown)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 7) Then
            Me.Email = CType(target,System.Windows.Controls.TextBox)
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",203)
            AddHandler Me.Email.TextChanged, New System.Windows.Controls.TextChangedEventHandler(AddressOf Me.Email_TextChanged)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",204)
            AddHandler Me.Email.LostFocus, New System.Windows.RoutedEventHandler(AddressOf Me.Email_LostFocus)
            
            #End ExternalSource
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",205)
            AddHandler Me.Email.PreviewKeyDown, New System.Windows.Input.KeyEventHandler(AddressOf Me.Email_PreviewKeyDown)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 8) Then
            Me.Update = CType(target,System.Windows.Controls.Button)
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",225)
            AddHandler Me.Update.Click, New System.Windows.RoutedEventHandler(AddressOf Me.Update_Click)
            
            #End ExternalSource
            Return
        End If
        If (connectionId = 9) Then
            
            #ExternalSource("..\..\..\..\ViewModel\EditEmployee.xaml",229)
            AddHandler CType(target,System.Windows.Controls.Button).Click, New System.Windows.RoutedEventHandler(AddressOf Me.Clear_Click)
            
            #End ExternalSource
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

