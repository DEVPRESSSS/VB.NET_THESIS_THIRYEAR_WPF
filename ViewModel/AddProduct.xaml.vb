Imports System.Data
Imports System.Data.SqlClient
Imports System.Transactions

Public Class AddProduct

    Dim inv As Inventory
    Public Sub New(inventoryForm As Inventory)


        InitializeComponent()

        FetchCategory()

        inv = inventoryForm
    End Sub

    Dim con As New ConnectionString
    Private Sub ProductName_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub ProductName_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Description_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Description_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Size_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Back OrElse e.Key = Key.Delete OrElse e.Key = Key.Tab OrElse e.Key = Key.Left OrElse e.Key = Key.Right Then
            e.Handled = False
        ElseIf Not (e.Key >= Key.D0 AndAlso e.Key <= Key.D9 OrElse e.Key >= Key.NumPad0 AndAlso e.Key <= Key.NumPad9) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Size_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Brand_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Insert_Click(sender As Object, e As RoutedEventArgs)


        Dim pname As String = ProductName.Text
        Dim descrip As String = Description.Text
        Dim brands As String = Brand.Text
        Dim Sizes As String = Size.Text
        Dim Colors As String = Color.Text
        Dim prices As Decimal



        If Not Decimal.TryParse(Price.Text, prices) Then
            MessageBox.Show("Please enter a valid decimal value for the price.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(pname) OrElse String.IsNullOrWhiteSpace(descrip) _
               OrElse String.IsNullOrWhiteSpace(brands) OrElse String.IsNullOrWhiteSpace(Colors) _
               OrElse String.IsNullOrWhiteSpace(Sizes) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        Dim selectedCategory = CType(ComboCat.SelectedItem, Category)
        Dim selectedCatID As Integer = selectedCategory.CategoryID


        InsertCashier(pname, descrip, selectedCatID, brands, Sizes, Colors, prices)


    End Sub
    Public Sub FetchCategory()
        Dim query As String = "SELECT CategoryID, CategoryName FROM Category"
        ComboCat.Items.Clear()

        Using cons As New SqlConnection(con.connectionString)
            Using cmd As New SqlCommand(query, cons)
                cons.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim category As New Category() With {
                        .CategoryID = reader("CategoryID"),
                        .CategoryName = reader("CategoryName").ToString()
                    }
                        ComboCat.Items.Add(category)
                    End While
                End Using
            End Using
        End Using
    End Sub



    Public Function InsertCashier(pname As String, descrip As String, catIndex As Integer, brands As String, Sizes As String, Colors As String, prices As Decimal)

        Dim productId As Integer

        Dim checkQuery As String = "SELECT COUNT(*) FROM [dbo].[Product] WHERE ProductName = @ProductName"

        Dim insertQuery As String = "INSERT INTO [dbo].[Product] ([ProductName], [Description], [CategoryID], [Brand], [Size], [Color], [Price]) " &
                                "OUTPUT INSERTED.ProductID " &
                                "VALUES (@ProductName, @Description, @CategoryID, @Brand, @Size, @Color, @Price)"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using transaction As SqlTransaction = connection.BeginTransaction()
                Try
                    ' Check for existing product
                    Using checkCommand As New SqlCommand(checkQuery, connection, transaction)
                        checkCommand.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 150)).Value = pname
                        Dim count As Integer = Convert.ToInt32(checkCommand.ExecuteScalar())

                        If count > 0 Then
                            MessageBox.Show("A product with the same name already exists.", "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Error)
                            Return pname
                        End If
                    End Using

                    ' Insert into Product table
                    Using insertCommand As New SqlCommand(insertQuery, connection, transaction)
                        insertCommand.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 150)).Value = pname
                        insertCommand.Parameters.Add(New SqlParameter("@Description", SqlDbType.NVarChar, 500)).Value = descrip
                        insertCommand.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Int, 8)).Value = catIndex
                        insertCommand.Parameters.Add(New SqlParameter("@Brand", SqlDbType.NVarChar, 100)).Value = brands
                        insertCommand.Parameters.Add(New SqlParameter("@Size", SqlDbType.NVarChar, 50)).Value = Sizes
                        insertCommand.Parameters.Add(New SqlParameter("@Color", SqlDbType.NVarChar, 50)).Value = Colors
                        insertCommand.Parameters.Add(New SqlParameter("@Price", SqlDbType.Decimal)).Value = prices
                        insertCommand.Parameters("@Price").Precision = 18
                        insertCommand.Parameters("@Price").Scale = 2

                        productId = Convert.ToInt32(insertCommand.ExecuteScalar())
                        MessageBox.Show("Product record inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                    End Using

                    ' Insert into Inventory table
                    InsertToInventory(productId, connection, transaction)

                    ' Commit the transaction
                    transaction.Commit()

                Catch ex As Exception
                    ' Rollback if any error occurs
                    transaction.Rollback()
                    MessageBox.Show("Transaction failed: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End Using
        End Using


        inv.FetchProductData()
        Me.Hide()

    End Function


    Private Sub InsertToInventory(productId As Integer, connection As SqlConnection, transaction As SqlTransaction)

        Dim qty As Integer = Convert.ToInt32(Quantity.Text)

        Dim q As String = "INSERT INTO Inventory (ProductID, Quantity, OriginalStock, LastUpdated) " &
                          "VALUES (@ProductID, @Quantity, @OriginalStock, @LastUpdated)"

        Using command As New SqlCommand(q, connection, transaction)
            command.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int)).Value = productId
            command.Parameters.Add(New SqlParameter("@Quantity", SqlDbType.Int)).Value = qty
            command.Parameters.Add(New SqlParameter("@OriginalStock", SqlDbType.Int)).Value = qty
            command.Parameters.Add(New SqlParameter("@LastUpdated", SqlDbType.DateTime)).Value = DateTime.Now
            command.ExecuteNonQuery()
        End Using

    End Sub

    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Color_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Color_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Hide()
    End Sub

    Private Sub Price_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Price_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Back OrElse e.Key = Key.Delete OrElse e.Key = Key.Left OrElse e.Key = Key.Right Then
            Return
        End If

        If Not ((e.Key >= Key.D0 And e.Key <= Key.D9) OrElse
                (e.Key >= Key.NumPad0 And e.Key <= Key.NumPad9) OrElse
                (e.Key = Key.Decimal OrElse e.Key = Key.OemPeriod)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then
            DragMove()
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub

    Private Sub Quantity_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Not IsNumeric(e.Text) Then
            e.Handled = True ' Reject the input
        End If
    End Sub

    Private Sub Quantity_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Back OrElse e.Key = Key.Delete OrElse e.Key = Key.Tab OrElse e.Key = Key.Left OrElse e.Key = Key.Right Then
            e.Handled = False ' Allow these keys
        ElseIf Not (e.Key >= Key.D0 AndAlso e.Key <= Key.D9 OrElse e.Key >= Key.NumPad0 AndAlso e.Key <= Key.NumPad9) Then
            ' If the key is not a number, block it
            e.Handled = True
        End If
    End Sub

    Private Sub Size_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Not IsNumeric(e.Text) Then
            e.Handled = True ' Reject the input
        End If
    End Sub
End Class
