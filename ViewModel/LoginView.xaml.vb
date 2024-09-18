Imports System.Data.SqlClient
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


        Dim connections As New ConnectionString()
        Dim usernames As String = Username.Text
        Dim passwords As String = Password.Password

        If String.IsNullOrWhiteSpace(usernames) OrElse String.IsNullOrWhiteSpace(passwords) Then
            MessageBox.Show("Username and password are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        Dim adminQuery As String = "SELECT PasswordHash, Salt FROM Admin WHERE Username = @Username"
        Dim cashierQuery As String = "SELECT PasswordHash, Salt FROM Cashier WHERE Username = @Username"

        Using connection As New SqlConnection(connections.connectionString)
            connection.Open()

            Using adminCommand As New SqlCommand(adminQuery, connection)
                adminCommand.Parameters.AddWithValue("@Username", usernames)

                Using adminReader As SqlDataReader = adminCommand.ExecuteReader()
                    If adminReader.Read() Then
                        Dim storedPasswordHash As Byte() = CType(adminReader("PasswordHash"), Byte())
                        Dim storedSalt As Byte() = CType(adminReader("Salt"), Byte())

                        Dim hashedPassword As Byte() = HashPassword(passwords, storedSalt)

                        If storedPasswordHash.SequenceEqual(hashedPassword) Then
                            Dim adminDash As New MainWindow()
                            adminDash.Show()
                            Me.Close()
                            Return
                        Else
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning)
                            Return
                        End If
                    End If
                End Using
            End Using

            Using cashierCommand As New SqlCommand(cashierQuery, connection)
                cashierCommand.Parameters.AddWithValue("@Username", usernames)

                Using cashierReader As SqlDataReader = cashierCommand.ExecuteReader()
                    If cashierReader.Read() Then
                        Dim storedPasswordHash As Byte() = CType(cashierReader("PasswordHash"), Byte())
                        Dim storedSalt As Byte() = CType(cashierReader("Salt"), Byte())

                        Dim hashedPassword As Byte() = HashPassword(passwords, storedSalt)

                        If storedPasswordHash.SequenceEqual(hashedPassword) Then


                            Dim main As New MainWindow()
                            main.Show()
                        Else
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error)
                        End If
                    Else
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error)
                    End If
                End Using
            End Using

        End Using

    End Sub
    Private Function HashPassword(password As String, salt As Byte()) As Byte()
        Dim passwordBytes As Byte() = Encoding.UTF8.GetBytes(password)
        Dim passwordWithSalt As Byte() = passwordBytes.Concat(salt).ToArray()

        Using sha256 As SHA256 = SHA256.Create()
            Return sha256.ComputeHash(passwordWithSalt)
        End Using
    End Function

    Private Sub TextBlock_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Password_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Username_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key >= Key.D0 AndAlso e.Key <= Key.D9 OrElse
      e.Key >= Key.NumPad0 AndAlso e.Key <= Key.NumPad9 Then
            ' Suppress the key press
            e.Handled = True
        End If
    End Sub

    Private Sub TextBlock_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        Dim forgotPasswordForm As New ForgotPassword()
        forgotPasswordForm.Show()
        Me.Close()
    End Sub
End Class
