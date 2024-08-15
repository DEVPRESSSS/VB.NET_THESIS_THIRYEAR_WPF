Public Class LoginView
    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()

        End If
    End Sub
End Class
