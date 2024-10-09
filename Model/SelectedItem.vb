Imports System.ComponentModel
Imports System.Windows.Media.Imaging

Public Class SelectedItem
    Implements INotifyPropertyChanged

    Private _quantity As Integer
    Private _subTotal As Decimal

    Public Property ProductID As Integer
    Public Property ProductName As String
    Public Property Size As String
    Public Property Price As Double

    Public Property GrandTotal As Decimal
    Public Property Quantity As Integer
        Get
            Return _quantity
        End Get
        Set(value As Integer)
            If _quantity <> value Then
                _quantity = value
                OnPropertyChanged("Quantity")
                SubTotal = _quantity * Price
            End If
        End Set
    End Property

    Public Property SubTotal As Decimal
        Get
            Return _subTotal
        End Get
        Set(value As Decimal)
            If _subTotal <> value Then
                _subTotal = value
                OnPropertyChanged("SubTotal")
            End If
        End Set
    End Property

    Public Property Picture As BitmapImage

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
