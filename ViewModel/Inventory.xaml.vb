Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class Inventory
    Dim con As New ConnectionString
    Private Sub AddNewProduct_Click(sender As Object, e As RoutedEventArgs)
        Dim openAddProduct As New AddProduct()

        openAddProduct.Show()
    End Sub

    Public Sub New()
        InitializeComponent()
        FetchCashierData()

    End Sub

    Private Sub FetchCashierData()

        Dim prod As New ObservableCollection(Of Product)()

        Dim query As String = "SELECT ProductID, ProductName, Price, Description, Category, Brand, Size, Color, CreatedAt FROM Product"

        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                prod.Add(New Product With {
                .ProductID = Convert.ToInt32(row("ProductID")),
                .ProductName = row("ProductName").ToString(),
                .Price = Convert.ToDouble(row("Price")),
                .Description = row("Description"),
                .Category = row("Category").ToString(),
                .Brand = row("Brand").ToString(),
                .Size = Convert.ToDecimal(row("Size")),
                .Color = row("Color").ToString(),
                .CreatedAt = row("CreatedAt")
            })
            Next

            productDataGrid.ItemsSource = prod
        End Using
    End Sub



    Private Sub EditBtn(sender As Object, e As RoutedEventArgs)
        Dim edit_button As Button = CType(sender, Button)

        Dim selected As Product = CType(edit_button.DataContext, Product)

        Dim edit_prodform As New EditProduct(
            Integer.Parse(selected.ProductID),
            selected.ProductName,
            selected.Price,
            selected.Description,
            selected.Category,
            selected.Brand,
            selected.Size,
            selected.Color
        )

        edit_prodform.Show()

    End Sub



End Class
