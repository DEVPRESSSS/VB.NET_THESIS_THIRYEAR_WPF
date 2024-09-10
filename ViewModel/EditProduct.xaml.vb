

Public Class EditProduct


    Public Sub New(Id As Integer, pname As String, price As Double, description As String, category As String, brand As String, size As Decimal, color As String)

        InitializeComponent()


    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)

        If e.LeftButton = MouseButtonState.Pressed Then
            DragMove()
        End If
    End Sub
End Class
