Imports System.Data
Imports System.Data.SqlClient

Public Class EditEmployee

    Dim editID As Integer
    Public Sub New(cashierID As Integer, usernames As String, firstNames As String, lastNames As String, emails As String)
        InitializeComponent()

        Username.Text = usernames
        Firstname.Text = firstNames
        Lastname.Text = lastNames
        Email.Text = emails
        editID = cashierID.ToString()

    End Sub
    Dim con As New ConnectionString
    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Update_Click(sender As Object, e As RoutedEventArgs)
        Dim cashierIDs As Integer = Integer.Parse(editID)
        Dim usernames As String = Username.Text
        Dim firstNames As String = Firstname.Text
        Dim lastNames As String = Lastname.Text
        Dim emails As String = Email.Text

        Dim result As Boolean = UpdateCashier(cashierIDs, usernames, firstNames, lastNames, emails)
        If result Then
            Me.Close()
        End If
    End Sub

    Public Function UpdateCashier(cashierID As Integer, username As String, firstName As String, lastName As String, email As String) As Boolean

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
                    Return True
                Else
                    MessageBox.Show("No record found to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
                    Return False
                End If

            End Using
        End Using

    End Function

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Username_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Username_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub
End Class
