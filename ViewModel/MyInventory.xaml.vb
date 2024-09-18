Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class MyInventory

    Dim con As New ConnectionString()
    Public Sub New()


        ' This call is required by the designer.
        InitializeComponent()
        FetchProductData()
        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Public Sub FetchProductData()

        Dim prod As New ObservableCollection(Of ProductInventory)()

        Dim query As String = "SELECT * FROM Inventory"
        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                prod.Add(New ProductInventory With {
                 .InventoryID = Convert.ToInt32(row("InventoryID")),
                .ProductID = Convert.ToInt32(row("ProductID")),
                .CurrentStock = Convert.ToInt32(row("Quantity")),
                .OriginalStock = Convert.ToInt32(row("OriginalStock")),
                .LastUpdated = row("LastUpdated")
            })
            Next

            productDataGrid.ItemsSource = prod
        End Using
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim deleteButton As Button = CType(sender, Button)
        Dim selectedProduct As ProductInventory = CType(deleteButton.DataContext, ProductInventory)
        Dim productID As String = selectedProduct.ProductID

        Dim delete As MsgBoxResult = MessageBox.Show("Are you sure you want to delete this product record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If delete = MsgBoxResult.Yes Then
            Dim deleteInventoryQuery As String = "DELETE FROM Inventory WHERE ProductID = @ProductID"
            Dim deleteProductQuery As String = "DELETE FROM Product WHERE ProductID = @ProductID"

            Try
                Using connection As New SqlConnection(con.connectionString)
                    connection.Open()
                    Using transaction As SqlTransaction = connection.BeginTransaction()
                        Try
                            Using cmd As New SqlCommand(deleteInventoryQuery, connection, transaction)
                                cmd.Parameters.AddWithValue("@ProductID", productID)
                                cmd.ExecuteNonQuery()
                            End Using

                            Using cmd As New SqlCommand(deleteProductQuery, connection, transaction)
                                cmd.Parameters.AddWithValue("@ProductID", productID)
                                cmd.ExecuteNonQuery()
                            End Using

                            transaction.Commit()
                            MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)

                            FetchProductData()
                        Catch ex As Exception
                            transaction.Rollback()
                            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                        End Try
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("An error occurred while connecting to the database: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End Try
        End If
    End Sub

End Class
