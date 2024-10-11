Imports System.Data
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class EditEmployee

    Dim editID As Integer
    Dim load As Employee
    Public Sub New(cashierID As Integer, usernames As String, firstNames As String, lastNames As String, emails As String, cashier As Employee)
        InitializeComponent()

        Username.Text = usernames
        Firstname.Text = firstNames
        Lastname.Text = lastNames
        Email.Text = emails
        editID = cashierID.ToString()
        load = cashier
    End Sub
    Dim con As New ConnectionString
    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs)
        Username.Text = ""
        Firstname.Text = ""
        Lastname.Text = ""
        Email.Text = ""
    End Sub
    Private Sub Refresh()
        load.FetchCashierData()
    End Sub
    Private Sub Update_Click(sender As Object, e As RoutedEventArgs)
        Dim cashierIDs As Integer = Integer.Parse(editID)
        Dim usernames As String = Username.Text
        Dim firstNames As String = Firstname.Text
        Dim lastNames As String = Lastname.Text
        Dim emails As String = Email.Text


        ' Validation check: ensure none of the fields are empty
        If String.IsNullOrWhiteSpace(usernames) Then
            MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Username.Focus() ' Set focus back to the Username field
            Return
        End If

        If String.IsNullOrWhiteSpace(firstNames) Then
            MessageBox.Show("First name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Firstname.Focus() ' Set focus back to the Firstname field
            Return
        End If

        If String.IsNullOrWhiteSpace(lastNames) Then
            MessageBox.Show("Last name cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Lastname.Focus() ' Set focus back to the Lastname field
            Return
        End If

        If String.IsNullOrWhiteSpace(emails) Then
            MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Email.Focus() ' Set focus back to the Email field
            Return
        End If

        UpdateCashier(cashierIDs, usernames, firstNames, lastNames, emails)

    End Sub

    Public Sub UpdateCashier(cashierID As Integer, username As String, firstName As String, lastName As String, email As String)
        Dim query As String = "UPDATE [dbo].[Cashier] SET [Username] = @Username, [FirstName] = @FirstName, [LastName] = @LastName, [Email] = @Email " &
                          "WHERE [CashierID] = @CashierID"

        Using connection As New SqlConnection(con.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = username
                command.Parameters.Add(New SqlParameter("@FirstName", SqlDbType.NVarChar, 50)).Value = firstName
                command.Parameters.Add(New SqlParameter("@LastName", SqlDbType.NVarChar, 50)).Value = lastName
                command.Parameters.Add(New SqlParameter("@Email", SqlDbType.NVarChar, 100)).Value = email
                command.Parameters.Add(New SqlParameter("@CashierID", SqlDbType.Int)).Value = editID


                connection.Open()
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MessageBox.Show("Cashier record updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                    Refresh()
                    Me.Close()
                Else
                    MessageBox.Show("No record found to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)

                End If

            End Using
        End Using

    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then
            DragMove()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub Username_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Username_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)

        WindowState = WindowState.Minimized
    End Sub

    Private Sub Firstname_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Lastname_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Lastname_LostFocus(sender As Object, e As RoutedEventArgs)

    End Sub


    Private Sub Email_TextChanged(sender As Object, e As TextChangedEventArgs)
        regex_email()

    End Sub

    Private Sub regex_email()


        ' Email validation regex
        Dim emails As String = Email.Text
        Dim emailPattern As String = "^[^\s@]+@[^\s@]+\.[^\s@]+$"
        Dim regex As New Regex(emailPattern)

        ' Validate the email format
        If regex.IsMatch(emails) Then
            ' If the email is valid, reset the background or border
            Email.Background = Brushes.White
            Email.BorderBrush = Brushes.Gray ' or any default border color
        Else
            ' If the email is invalid, mark it with a warning
            Email.BorderBrush = Brushes.LightPink
        End If
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        regex_email()

    End Sub

    Private Sub Email_LostFocus(sender As Object, e As RoutedEventArgs)
        Dim emailPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim emailInput As String = Email.Text.Trim()

        If Not Regex.IsMatch(emailInput, emailPattern) AndAlso Not String.IsNullOrEmpty(emailInput) Then
            MessageBox.Show("Invalid email address format.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)

        End If
    End Sub
End Class
