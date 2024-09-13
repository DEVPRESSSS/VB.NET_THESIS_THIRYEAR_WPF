Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

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

    Private Sub Remove_Click(sender As Object, e As RoutedEventArgs)
        Dim deleteButton As Button = CType(sender, Button)

        Dim selectedProduct As Product = CType(deleteButton.DataContext, Product)

        Dim productID As String = selectedProduct.ProductID


        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this Product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then
            Dim query As String = "DELETE FROM Product WHERE ProductID = @ProductID"

            Using connection As New SqlConnection(con.connectionString)
                Try
                    connection.Open()
                    Dim cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@ProductID", productID)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)

                    FetchProductData()

                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End Using
        End If
    End Sub
End Class
