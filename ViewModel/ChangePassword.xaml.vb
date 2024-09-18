Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Security.Cryptography
Imports System.Text

Public Class ChangePassword
    Dim Email As String

    Public Sub New(emails As String)
        InitializeComponent()
        Email = emails
    End Sub

    Dim cons As New ConnectionString

    Private Sub Change_Click(sender As Object, e As RoutedEventArgs)
        Dim pass1 As String = NewPassword.Text
        Dim pass2 As String = ConfirmPassword.Text

        If String.IsNullOrWhiteSpace(pass1) OrElse String.IsNullOrWhiteSpace(pass2) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        If pass1 <> pass2 Then
            MessageBox.Show("Passwords don't match", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        If VerifyEmailInAdmin(Email) Then
            UpdatePasswordInTable("Admin", pass2)
        ElseIf VerifyEmailInCashier(Email) Then
            UpdatePasswordInTable("Cashier", pass2)
        Else
            MessageBox.Show("Email not found in Admin or Cashier tables.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Public Function VerifyEmailInAdmin(email As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM Admin WHERE Email = @Email"

        Using connection As New SqlConnection(cons.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 100)).Value = email
                Try
                    connection.Open()
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                    Return count > 0
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    Return False
                End Try
            End Using
        End Using
    End Function

    Public Function VerifyEmailInCashier(email As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM Cashier WHERE Email = @Email"

        Using connection As New SqlConnection(cons.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 100)).Value = email
                Try
                    connection.Open()
                    Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())
                    Return count > 0
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    Return False
                End Try
            End Using
        End Using
    End Function

    ' Function to update the password in either Admin or Cashier table
    Public Sub UpdatePasswordInTable(tableName As String, newPassword As String)
        Dim saltBytes As Byte() = GenerateSalt(16)
        Dim passwordHash As Byte() = HashPassword(newPassword, saltBytes)

        Dim query As String = $"UPDATE [dbo].[{tableName}] SET [PasswordHash] = @PasswordHash, [Salt] = @Salt WHERE [Email] = @Email"

        Using connection As New SqlConnection(cons.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@PasswordHash", SqlDbType.VarBinary, passwordHash.Length)).Value = passwordHash
                command.Parameters.Add(New SqlParameter("@Salt", SqlDbType.VarBinary, saltBytes.Length)).Value = saltBytes
                command.Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 100)).Value = Email

                Try
                    connection.Open()
                    Dim rowsAffected As Integer = command.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Password updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)

                    Else
                        MessageBox.Show("Failed to update the password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End Using
        End Using
    End Sub

    ' Generate salt
    Private Function GenerateSalt(size As Integer) As Byte()
        Using rng As New RNGCryptoServiceProvider()
            Dim saltBytes(size - 1) As Byte
            rng.GetBytes(saltBytes)
            Return saltBytes
        End Using
    End Function

    ' Hash password with salt
    Private Function HashPassword(password As String, salt As Byte()) As Byte()
        Using sha256 As SHA256 = SHA256.Create()
            Dim passwordWithSalt As Byte() = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray()
            Return sha256.ComputeHash(passwordWithSalt)
        End Using
    End Function

    Private Sub Change_Click_1(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
