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

    End Sub

    Private Sub AddbtnClick(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub FetchCashierData()
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


End Class
