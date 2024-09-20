Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

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
    Dim converter As New BrushConverter()
    Dim myArray As String() = {"#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#6741D9", "#0CA678"}
    Dim fletter As String
    Dim brush As Brush
    Public Sub FetchCashierData()


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

                brush = CType(converter.ConvertFromString(colorString), Brush)

                Dim fname As String = row("FirstName").ToString()
                fletter = If(fname.Length > 0, fname.Substring(0, 1), String.Empty)

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

        If Search.Text.Length > 0 Then
            FilterProductTable()

        Else
            FetchCashierData()

        End If

    End Sub

    Private Sub Search_Click(sender As Object, e As RoutedEventArgs)
        FilterProductTable()
    End Sub


    Private Sub FilterProductTable()

        Dim input As String = Search.Text.Trim()
        Dim products As New List(Of Cashier)()
        Dim q As String = "SELECT * FROM Cashier WHERE " &
                      "(UserName LIKE '%' + @UserName + '%' OR @UserName = '') " &
                      "OR (FirstName LIKE '%' + @FirstName + '%' OR @FirstName = '') " &
                      "OR (LastName LIKE '%' + @LastName + '%' OR @LastName = '') " &
                      "OR (Email LIKE '%' + @Email + '%' OR @Email = '') " &
                      "OR (CashierID = @CashierID OR @CashierID = '') "

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using cmd As New SqlCommand(q, connection)
                cmd.Parameters.AddWithValue("@UserName", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@FirstName", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@Email", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@LastName", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@CashierID", If(IsNumeric(input), input, DBNull.Value))

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    Dim i As Integer = 0
                    While reader.Read()
                        Dim colorString As String = myArray(i Mod myArray.Length)
                        brush = CType(converter.ConvertFromString(colorString), Brush)

                        Dim fname As String = reader("FirstName").ToString()
                        fletter = If(fname.Length > 0, fname.Substring(0, 1), String.Empty)

                        Dim filter As New Cashier() With {
                            .CashierID = Convert.ToInt32(reader("CashierID")),
                            .Username = reader("UserName").ToString(),
                            .FirstName = reader("FirstName").ToString(),
                            .Email = reader("Email").ToString(),
                            .LastName = reader("LastName").ToString(),
                            .Character = fletter,
                            .BgColor = brush,
                            .CreatedAt = reader("CreatedAt").ToString()
                        }

                        products.Add(filter)
                        i += 1
                    End While
                End Using
            End Using
        End Using

        cashierDataGrid.ItemsSource = products
    End Sub


    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)

    End Sub


End Class
