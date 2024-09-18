Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.TeamFoundation.Common

Public Class Inventory
    Dim con As New ConnectionString
    Private Sub AddNewProduct_Click(sender As Object, e As RoutedEventArgs)
        Dim openAddProduct As New AddProduct(Me)

        openAddProduct.Show()
    End Sub

    Public Sub New()
        InitializeComponent()
        FetchProductData()

    End Sub

    Public Sub FetchProductData()

        Dim prod As New ObservableCollection(Of Product)()

        Dim query As String = "SELECT p.ProductID, p.ProductName, p.Price, p.Description, p.CategoryID, c.CategoryName, p.Brand, p.Size, p.Color, p.CreatedAt " &
                          "FROM Product p JOIN Category c ON p.CategoryID = c.CategoryID"
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
                .Price = Convert.ToDecimal(row("Price")),
                .Description = row("Description"),
                .CategoryID = Convert.ToInt32(row("CategoryID")),
                 .CategoryName = row("CategoryName").ToString(),
                .Brand = row("Brand").ToString(),
                .Size = row("Size").ToString(),
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
            selected.CategoryName,
            selected.Brand,
            selected.Size,
            selected.Color,
            Me
        )

        edit_prodform.Show()

    End Sub



    Private Sub Search_Click(sender As Object, e As RoutedEventArgs)
        FilterProductTable()
    End Sub

    Private Sub FilterProductTable()
        Dim input As String = Search.Text.Trim()

        If input.IsNullOrEmpty() Then

            MessageBox.Show("Error", "Please provide a value", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If
        Dim products As New List(Of Product)()

        Dim q As String = "SELECT p.*, c.CategoryName FROM PRODUCT p " &
                      "INNER JOIN Category c ON p.CategoryID = c.CategoryID " &
                      "WHERE (p.ProductName LIKE '%' + @ProductName + '%' OR @ProductName = '') " &
                      "OR (p.Description LIKE '%' + @Description + '%' OR @Description = '') " &
                      "OR (p.Brand LIKE '%' + @Brand + '%' OR @Brand = '') " &
                      "OR (p.Size LIKE '%' + @Size + '%' OR @Size = '') " &
                      "OR (p.Color LIKE '%' + @Color + '%' OR @Color = '') " &
                      "OR (c.CategoryName LIKE '%' + @CategoryName + '%' OR @CategoryName = '') " &
                      "OR (p.Price = @Price OR @Price = '') " &
                      "OR (p.CategoryID = @CategoryID OR @CategoryID = '') " &
                      "OR (p.ProductID = @ProductID OR @ProductID = '')"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using cmd As New SqlCommand(q, connection)
                cmd.Parameters.AddWithValue("@ProductName", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@Brand", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@Size", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@Color", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@CategoryName", If(String.IsNullOrEmpty(input), "", input))

                cmd.Parameters.AddWithValue("@Price", If(IsNumeric(input), input, DBNull.Value))
                cmd.Parameters.AddWithValue("@CategoryID", If(IsNumeric(input), input, DBNull.Value))
                cmd.Parameters.AddWithValue("@ProductID", If(IsNumeric(input), input, DBNull.Value))

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim filter As New Product() With {
                        .ProductID = Convert.ToInt32(reader("ProductID")),
                        .ProductName = reader("ProductName").ToString(),
                        .Description = reader("Description").ToString(),
                        .Brand = reader("Brand").ToString(),
                        .Size = reader("Size").ToString(),
                        .Color = reader("Color").ToString(),
                        .Price = Convert.ToDecimal(reader("Price")),
                        .CategoryID = Convert.ToInt32(reader("CategoryID")),
                        .CategoryName = reader("CategoryName").ToString(),
                        .CreatedAt = Convert.ToDateTime(reader("CreatedAt"))
                    }

                        products.Add(filter)
                    End While
                End Using
            End Using
        End Using

        productDataGrid.ItemsSource = products
    End Sub


    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

        Dim input As String = Search.Text

        If input.Length = 0 Then

            FetchProductData()


        Else
            FilterProductTable()

        End If
    End Sub
End Class
