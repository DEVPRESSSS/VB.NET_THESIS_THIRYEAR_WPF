Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class EditMember

    Implements INotifyPropertyChanged

    Private _member As Member




    Public Sub New(member As Member)
        _member = member
    End Sub

    Public Overloads Property Name As String
        Get
            Return _member.Name
        End Get
        Set(value As String)
            If _member.Name <> value Then
                _member.Name = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Public Property Position As String
        Get
            Return _member.Position
        End Get
        Set(value As String)
            If _member.Position <> value Then
                _member.Position = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Public Property Email As String
        Get
            Return _member.Email
        End Get
        Set(value As String)
            If _member.Email <> value Then
                _member.Email = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Public Property Phone As String
        Get
            Return _member.Phone
        End Get
        Set(value As String)
            If _member.Phone <> value Then
                _member.Phone = value
                OnPropertyChanged()
            End If
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub




End Class
