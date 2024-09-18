Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Settings
    Dim cons As New ConnectionString

    Private Sub TextBox_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Insert_Click(sender As Object, e As RoutedEventArgs)
        Dim user As String = Username.Text
        Dim passwords As String = Password.Password
        Dim firstNames As String = Firstname.Text
        Dim lastNames As String = Lastname.Text
        Dim emails As String = Email.Text
        If String.IsNullOrWhiteSpace(user) OrElse String.IsNullOrWhiteSpace(passwords) _
               OrElse String.IsNullOrWhiteSpace(firstNames) OrElse String.IsNullOrWhiteSpace(lastNames) _
               OrElse String.IsNullOrWhiteSpace(emails) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        Dim saltBytes As Byte() = GenerateSalt(16)
        Dim hash As Byte() = HashPassword(passwords, saltBytes)

        InsertCashier(user, hash, saltBytes, firstNames, lastNames, emails)
    End Sub
    Private Function GenerateSalt(size As Integer) As Byte()
        Using rng As New RNGCryptoServiceProvider()
            Dim saltBytes(size - 1) As Byte
            rng.GetBytes(saltBytes)
            Return saltBytes
        End Using
    End Function

    Public Function InsertCashier(username As String, passwordHash As Byte(), salt As Byte(), firstName As String, lastName As String, email As String)



        Dim query As String = "INSERT INTO [dbo].[Admin] ([Username], [PasswordHash], [Salt], [FirstName], [LastName], [Email]) " &
                              "VALUES (@Username, @PasswordHash, @Salt, @FirstName, @LastName, @Email)"

        Using connection As New SqlConnection(cons.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = username
                command.Parameters.Add(New SqlParameter("@PasswordHash", SqlDbType.VarBinary, passwordHash.Length)).Value = passwordHash
                command.Parameters.Add(New SqlParameter("@Salt", SqlDbType.VarBinary, salt.Length)).Value = salt
                command.Parameters.Add(New SqlParameter("@FirstName", SqlDbType.NVarChar, 50)).Value = firstName
                command.Parameters.Add(New SqlParameter("@LastName", SqlDbType.NVarChar, 50)).Value = lastName
                command.Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 100)).Value = email

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Admin record inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                    Return True
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    Return False
                End Try
            End Using
        End Using


    End Function



    Private Function HashPassword(password As String, salt As Byte()) As Byte()
        Using sha256 As SHA256 = SHA256.Create()
            Dim passwordWithSalt As Byte() = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray()
            Return sha256.ComputeHash(passwordWithSalt)
        End Using
    End Function

    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Lastname_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Firstname_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Username_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Email_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Username_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub
End Class
