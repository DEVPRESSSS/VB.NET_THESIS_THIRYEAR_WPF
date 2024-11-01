Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text
Imports System.Text.RegularExpressions

Public Class AddEmployee
    Dim cons As New ConnectionString
    Dim emp As Employee
    Public Sub New(employeeforma As Employee)


        InitializeComponent()

        emp = employeeforma
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
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

    Private Function HashPassword(password As String, salt As Byte()) As Byte()
        Using sha256 As SHA256 = SHA256.Create()
            ' Combine password and salt
            Dim passwordWithSalt As Byte() = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray()
            ' Compute hash
            Return sha256.ComputeHash(passwordWithSalt)
        End Using
    End Function

    Public Function InsertCashier(username As String, passwordHash As Byte(), salt As Byte(), firstName As String, lastName As String, email As String)



        Dim query As String = "INSERT INTO [dbo].[Cashier] ([Username], [PasswordHash], [Salt], [FirstName], [LastName], [Email]) " &
                              "VALUES (@Username, @PasswordHash, @Salt, @FirstName, @LastName, @Email)"

        Using connection As New SqlConnection(cons.connectionString)
            Using command As New SqlCommand(query, connection)
                ' Use SqlParameter with specific types and sizes
                command.Parameters.Add(New SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = username
                command.Parameters.Add(New SqlParameter("@PasswordHash", SqlDbType.VarBinary, passwordHash.Length)).Value = passwordHash
                command.Parameters.Add(New SqlParameter("@Salt", SqlDbType.VarBinary, salt.Length)).Value = salt
                command.Parameters.Add(New SqlParameter("@FirstName", SqlDbType.NVarChar, 50)).Value = firstName
                command.Parameters.Add(New SqlParameter("@LastName", SqlDbType.NVarChar, 50)).Value = lastName
                command.Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 100)).Value = email

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Cashier record inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                    emp.FetchCashierData()

                    clear()
                    Me.Close()
                    Return True
                Catch ex As Exception
                    MessageBox.Show("Username or email already exist, Please input unique value ", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    Return False
                End Try
            End Using
        End Using


    End Function

    Public Sub clear()
        Username.Text = ""
        Password.Password = ""
        Lastname.Text = ""
        Firstname.Text = ""
        Email.Text = ""
    End Sub

    Private Sub Clear(sender As Object, e As RoutedEventArgs)
        clear()
    End Sub

    Private Sub Username_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Username_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()

        End If
    End Sub

    Private Sub Firstname_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True ' Suppress non-letter keys
        End If
    End Sub

    Private Sub Lastname_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True ' Suppress non-letter keys
        End If
    End Sub

    Private Sub Email_TextChanged(sender As Object, e As TextChangedEventArgs)
        Dim emails As String = Email.Text
        Dim emailPattern As String = "^[^\s@]+@[^\s@]+\.[^\s@]+$"
        Dim regex As New Regex(emailPattern)

        If regex.IsMatch(emails) Then
            Email.Background = Brushes.White
        Else
            Email.BorderBrush = Brushes.LightPink
        End If
    End Sub

    Private Sub Email_LostFocus(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Email_LostFocus_1(sender As Object, e As RoutedEventArgs)
        Dim emailPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim emailInput As String = Email.Text.Trim()

        If Not Regex.IsMatch(emailInput, emailPattern) AndAlso Not String.IsNullOrEmpty(emailInput) Then
            MessageBox.Show("Invalid email address format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)

        End If
    End Sub

    Private Sub Password_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Space Then

            e.Handled = True
        End If
    End Sub

    Private Sub Email_PreviewKeyDown(sender As Object, e As KeyEventArgs)

        If e.Key = Key.Space Then

            e.Handled = True
        End If
    End Sub
End Class
