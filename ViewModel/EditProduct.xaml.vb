

Imports System.Data
Imports System.Data.SqlClient

Public Class EditProduct
    Dim cons As New ConnectionString

    Dim editID As Integer

    Dim inventoryForm As Inventory

    Public Sub New(Id As Integer, pname As String, prices As Double, descrip As String, catname As String, brands As String, sizes As String, colors As String, inventory As Inventory)

        InitializeComponent()

        ProductName.Text = pname
        Description.Text = descrip
        Brand.Text = brands
        ComboCat.SelectedIndex = 0


        Size.Text = sizes
        Color.Text = colors
        Price.Text = prices
        editID = Id

        ComboCat.Items.Clear()

        ComboCat.Items.Add(catname)

        ComboCat.SelectedIndex = 0
        inventoryForm = inventory

    End Sub


    Private Sub RefreshForm()
        inventoryForm.FetchProductData()
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)

        WindowState = WindowState.Minimized
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Hide()
    End Sub

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

    Private Sub Clear(sender As Object, e As RoutedEventArgs)
        Clear()
    End Sub

    Private Sub Size_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Size_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Price_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Price_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Color_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Color_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Brand_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub



    Private Sub ComboCat_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)


    End Sub
    Public Sub FetchCategory()
        Dim query As String = "SELECT CategoryID, CategoryName FROM Category"
        ComboCat.Items.Clear()

        Using con As New SqlConnection(cons.connectionString)
            Using cmd As New SqlCommand(query, con)
                con.Open()

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

    Private Sub ComboCat_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)


        FetchCategory()

    End Sub

    Private Sub Update_Click(sender As Object, e As RoutedEventArgs)

        Dim pname As String = ProductName.Text
        Dim descrip As String = Description.Text
        Dim brands As String = Brand.Text
        Dim sizes As String = Size.Text
        Dim colors As String = Color.Text

        Dim prices As Decimal
        Dim catID As Integer


        If Not Decimal.TryParse(Price.Text, prices) Then
            MessageBox.Show("Please enter a valid decimal value for the price.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If


        If String.IsNullOrWhiteSpace(pname) OrElse String.IsNullOrWhiteSpace(descrip) _
               OrElse String.IsNullOrWhiteSpace(brands) OrElse String.IsNullOrWhiteSpace(colors) _
               OrElse String.IsNullOrWhiteSpace(sizes) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        If ComboCat.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a category.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        Else
            If TypeOf ComboCat.SelectedItem Is Category Then
                catID = CType(ComboCat.SelectedItem, Category).CategoryID
            ElseIf TypeOf ComboCat.SelectedItem Is String Then
                Dim selectedCategory As String = ComboCat.SelectedItem.ToString()
                Dim category As Category = FetchCategoryByName(selectedCategory)
                If category IsNot Nothing Then
                    catID = category.CategoryID
                Else
                    MessageBox.Show("Selected category does not exist.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
                    Return
                End If
            End If
        End If


        UpdateProduct(pname, descrip, brands, sizes, colors, prices, catID)



    End Sub
    Public Sub UpdateProduct(pname As String, descrip As String, brands As String, sizes As String, colors As String, price As Double, catID As Integer)

        Dim query As String = "UPDATE [dbo].[Product] SET [ProductName] = @ProductName, [Description] = @Description, [Brand] = @Brand, [Size] = @Size, [Color] = @Color,  [Price] = @Price, [CategoryID] = @CategoryID " &
                          "WHERE [ProductID] = @ProductID"

        Using connection As New SqlConnection(cons.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 50)).Value = pname
                command.Parameters.Add(New SqlParameter("@Description", SqlDbType.NVarChar, 50)).Value = descrip
                command.Parameters.Add(New SqlParameter("@Brand", SqlDbType.NVarChar, 100)).Value = brands
                command.Parameters.Add(New SqlParameter("@Size", SqlDbType.NVarChar, 50)).Value = sizes
                command.Parameters.Add(New SqlParameter("@Color", SqlDbType.NVarChar, 50)).Value = colors
                command.Parameters.Add(New SqlParameter("@Price", SqlDbType.Decimal)).Value = price
                command.Parameters("@Price").Precision = 18
                command.Parameters("@Price").Scale = 2
                command.Parameters.Add(New SqlParameter("@CategoryID", SqlDbType.Int)).Value = catID
                command.Parameters.Add(New SqlParameter("@ProductID", SqlDbType.Int)).Value = editID



                connection.Open()
                Dim rowsAffected As Integer = command.ExecuteNonQuery()
                If rowsAffected > 0 Then
                    MessageBox.Show("Product record updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                    ' UpdateInventoryQuantity(editID)
                    Clear()
                    RefreshForm()
                    Me.Close()

                Else
                    MessageBox.Show("No record found to update.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning)
                End If

            End Using
        End Using
    End Sub


    Public Function FetchCategoryByName(catName As String) As Category
        Dim query As String = "SELECT CategoryID, CategoryName FROM Category WHERE CategoryName = @CategoryName"
        Dim fetchedCategory As Category = Nothing

        Using con As New SqlConnection(cons.connectionString)
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.Add(New SqlParameter("@CategoryName", SqlDbType.NVarChar, 50)).Value = catName
                con.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        fetchedCategory = New Category() With {
                        .CategoryID = reader("CategoryID"),
                        .CategoryName = reader("CategoryName").ToString()
                    }
                    End If
                End Using
            End Using
        End Using

        Return fetchedCategory
    End Function

    Private Sub Clear()


        ProductName.Text = ""
        Description.Text = ""
        Brand.Text = ""
        Size.Text = ""
        Price.Text = ""
        Color.Text = ""

        ComboCat.Items.Clear()
    End Sub

    Private Sub ComboCat_MouseDoubleClick_1(sender As Object, e As MouseButtonEventArgs)

    End Sub
End Class
