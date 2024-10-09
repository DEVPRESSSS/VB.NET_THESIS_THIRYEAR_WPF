Public Class Sale
    Public Property SaleID As Integer
    Public Property CashierID As Integer
    Public Property SaleDate As DateTime
    Public Property TotalAmount As Double
    Public Property SaleDetailsList As New List(Of SaleDetails)

End Class
