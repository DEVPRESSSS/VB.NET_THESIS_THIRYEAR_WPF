Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography
Imports System.Text

Public Class LoginView
    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()

        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As RoutedEventArgs)
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        Else
            WindowState = WindowState.Maximized
        End If


    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Application.Current.Shutdown()
    End Sub

    Private Sub LoginBtn_Click(sender As Object, e As RoutedEventArgs)


        Dim admin As New Admin()
        Dim connections As New ConnectionString()

        admin.Username = Username.Text
        admin.Password = Password.Password

        Dim hashedPassword As Byte() = HashPassword(admin.Password)

        Dim query As String = "SELECT COUNT(1) FROM Admin WHERE Username = @Username AND PasswordHash = @PasswordHash"

        Using connection As New SqlConnection(connections.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Username", admin.Username)
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword)

                connection.Open()
                Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())

                If result = 1 Then
                    Dim dash As New MainWindow()
                    dash.Show()
                    Me.Close()
                Else
                    MessageBox.Show("Incorrect username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End If
            End Using
        End Using

    End Sub
    Private Function HashPassword(password As String) As Byte()
        Dim passwordBytes As Byte() = Encoding.UTF8.GetBytes(password)

        ' Create a new instance of SHA256
        Using sha256 As SHA256 = SHA256.Create()
            ' Compute the hash of the password
            Dim hashBytes As Byte() = sha256.ComputeHash(passwordBytes)

            ' Return the hashed password as a byte array
            Return hashBytes
        End Using
    End Function
End Class
