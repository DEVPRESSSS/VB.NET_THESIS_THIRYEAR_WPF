Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class Sales
    Dim con As New ConnectionString


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FetchSale()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FetchSale()
        Dim sales As New ObservableCollection(Of Sale)()



        Dim query As String = "SELECT * FROM Sales"
        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                sales.Add(New Sale With {
                .SaleID = Convert.ToInt32(row("SalesID")),
                .CashierID = Convert.ToInt32(row("CashierID").ToString()),
                .SaleDate = row("SaleDate"),
                .TotalAmount = Convert.ToDouble(row("TotalAmount"))
            })
            Next

            salesDataGrid.ItemsSource = sales
        End Using
    End Sub
End Class
