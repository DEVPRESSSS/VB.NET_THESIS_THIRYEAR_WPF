Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class Employee

    Public Sub New()
        InitializeComponent()
        FetchCashierData()

    End Sub



    Dim con As New ConnectionString
    Private Sub EditBtn(sender As Object, e As RoutedEventArgs)

        Dim edit_Button As Button = CType(sender, Button)

        Dim selectedCashier As Cashier = CType(edit_Button.DataContext, Cashier)

        Dim passToEdit As New EditEmployee(
         Integer.Parse(selectedCashier.CashierID),
         selectedCashier.Username,
         selectedCashier.FirstName,
         selectedCashier.LastName,
         selectedCashier.Email,
         Me
     )

        passToEdit.Show()





    End Sub

    Private Sub AddbtnClick(sender As Object, e As RoutedEventArgs)
        Dim AddEmployee As New AddEmployee()
        AddEmployee.Show()
    End Sub

    Public Sub FetchCashierData()
        Dim converter As New BrushConverter()
        Dim myArray As String() = {"#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#6741D9", "#0CA678"}

        Dim cashiers As New ObservableCollection(Of Cashier)()

        Dim query As String = "SELECT * FROM Cashier"

        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For i As Integer = 0 To dataTable.Rows.Count - 1
                Dim row As DataRow = dataTable.Rows(i)

                Dim colorString As String = myArray(i Mod myArray.Length)

                Dim brush As Brush = CType(converter.ConvertFromString(colorString), Brush)

                Dim fname As String = row("FirstName").ToString()
                Dim fletter As String = If(fname.Length > 0, fname.Substring(0, 1), String.Empty)

                cashiers.Add(New Cashier With {
                    .Character = fletter,
                    .BgColor = brush,
                    .CashierID = row("CashierID").ToString(),
                    .Username = row("Username").ToString(),
                    .FirstName = row("FirstName").ToString(),
                    .LastName = row("LastName").ToString(),
                    .Email = row("Email").ToString(),
                    .CreatedAt = row("CreatedAt").ToString()
                })
            Next

            cashierDataGrid.ItemsSource = cashiers
        End Using
    End Sub

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub



    Private Sub Delete_Click(sender As Object, e As RoutedEventArgs)
        Dim deleteButton As Button = CType(sender, Button)

        Dim selectedCashier As Cashier = CType(deleteButton.DataContext, Cashier)

        Dim cashierId As String = selectedCashier.CashierID


        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this cashier?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then
            Dim query As String = "DELETE FROM Cashier WHERE CashierID = @CashierID"

            Using connection As New SqlConnection(con.connectionString)
                Try
                    connection.Open()
                    Dim cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@CashierID", cashierId)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Cashier deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)

                    FetchCashierData()

                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End Using
        End If

    End Sub

    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
