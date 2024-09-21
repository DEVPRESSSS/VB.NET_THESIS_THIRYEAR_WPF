Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Microsoft.IdentityModel.Tokens
Imports Microsoft.TeamFoundation.Build.WebApi
Imports Microsoft.TeamFoundation.Common

Public Class POS
    Dim con As New ConnectionString
    Private selectedLists As ObservableCollection(Of SelectedItem)
    Private _cashier As String
    Dim cashierID As String = String.Empty
    Public Sub New(cashier As String)
        InitializeComponent()
        disable()
        selectedLists = New ObservableCollection(Of SelectedItem)()

        _cashier = cashier
    End Sub


    Private Sub retrieve()

        Dim query As String = "SELECT CashierID FROM Cashier WHERE Username = @Username"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Username", _cashier)

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing Then
                    cashierID = result.ToString()
                Else
                    MessageBox.Show("Username not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning)
                End If
            End Using
        End Using
    End Sub


    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Dim Response As MessageBoxResult

        Response = MessageBox.Show("Confirmation", "Are you sure you want to exit?", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If Response = MsgBoxResult.Yes Then

            Dim login As New LoginView()
            login.Show()
            Me.Close()

        End If
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As RoutedEventArgs)
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        Else
            WindowState = WindowState.Maximized
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub

    Private Sub SearchBtn_Click(sender As Object, e As RoutedEventArgs)
        FilterProductTable()
    End Sub

    Private Sub FilterProductTable()
        Dim input As String = Search.Text.Trim()

        If String.IsNullOrEmpty(input) Then
            MessageBox.Show("Please input a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        Dim q As String = "SELECT p.ProductID, p.ProductName, p.Size, p.Price, i.Quantity, c.CategoryName " &
                  "FROM PRODUCT p " &
                  "INNER JOIN Category c ON p.CategoryID = c.CategoryID " &
                  "INNER JOIN Inventory i ON p.ProductID = i.ProductID " &
                  "WHERE p.ProductName = @ProductName OR p.ProductID = @ProductID"


        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using cmd As New SqlCommand(q, connection)
                cmd.Parameters.AddWithValue("@ProductName", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@ProductID", If(IsNumeric(input), input, DBNull.Value))

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            ID.Content = reader("ProductID").ToString()
                            ProductName.Text = reader("ProductName").ToString()
                            Size.Text = reader("Size").ToString()
                            Price.Text = reader("Price").ToString()

                            CurrentStock.Text = reader("Quantity").ToString()
                        End While
                    Else
                        MessageBox.Show("Product Name or Product ID does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                        Clear()
                        Return
                    End If
                End Using
            End Using
        End Using

    End Sub

    Private Sub Clear()
        ID.Content = ""
        ProductName.Text = ""
        Size.Text = ""
        Price.Text = ""
        Quantity.Text = ""
        Search.Text = ""
        CurrentStock.Text = ""
    End Sub



    Private Sub AddItem_Click(sender As Object, e As RoutedEventArgs)

        Dim qty As Integer

        If String.IsNullOrEmpty(ProductName.Text) OrElse String.IsNullOrEmpty(Size.Text) OrElse String.IsNullOrEmpty(Price.Text) Then
            MessageBox.Show("Please fill in all fields before adding.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        If Quantity.Text > CurrentStock.Text Then

            MessageBox.Show("Quantity is greater than the current stock", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return


        Else
            If Integer.TryParse(Quantity.Text, qty) Then


                Dim newItem As New SelectedItem() With {
                        .ProductID = Convert.ToInt32(ID.Content),
                        .ProductName = ProductName.Text,
                        .Size = Size.Text,
                        .Price = Convert.ToDecimal(Price.Text),
                        .Quantity = Convert.ToInt32(qty),
                        .SubTotal = Convert.ToDecimal(qty * Price.Text)
                    }

                selectedLists.Add(newItem)

                productDataGrid.ItemsSource = selectedLists
                SumSubtotalColumn()
                disable()
                Clear()
            Else
                MessageBox.Show("Please put a quantity first before adding it to the selected items list", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)

                Return
            End If

        End If

    End Sub

    Private Sub productDataGrid_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub SumSubtotalColumn()
        Dim total As Decimal = 0D

        For Each item As SelectedItem In selectedLists
            total += item.SubTotal
            item.GrandTotal = total
        Next

        Total_label.Content = total.ToString("C")
    End Sub

    Private Sub Remove_Click(sender As Object, e As RoutedEventArgs)
        Dim deleteButton As Button = CType(sender, Button)

        Dim selectedProduct As SelectedItem = CType(deleteButton.DataContext, SelectedItem)

        Dim productID As String = selectedProduct.ProductID


        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this item in the list?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then

            selectedLists.Remove(selectedProduct)
            SumSubtotalColumn()
            disable()
        End If
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim bills As Decimal
        Dim productID As String
        Dim gtotal As Decimal
        Dim qty As Integer
        Dim currentStock As Integer
        Dim q As String = "SELECT Quantity FROM Inventory WHERE ProductID= @ProductID"
        Dim updateQuery As String = "UPDATE Inventory SET Quantity  = @Quantity WHERE ProductID = @ProductID"

        If Decimal.TryParse(Bill.Text, bills) Then
            If bills < Total_label.Content Then
                MessageBox.Show("Bill is not enough", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)
                Return
            End If

            bills -= Total_label.Content
            Changes.Content = bills

            For Each item As SelectedItem In selectedLists
                gtotal += item.SubTotal
                productID = item.ProductID
                qty = item.Quantity

                Using connection As New SqlConnection(con.connectionString)
                    connection.Open()

                    Using cmd As New SqlCommand(q, connection)
                        cmd.Parameters.AddWithValue("@ProductID", productID)
                        currentStock = CInt(cmd.ExecuteScalar())
                    End Using

                    If currentStock >= qty Then
                        Dim newStock As Integer = currentStock - qty

                        Using updateCmd As New SqlCommand(updateQuery, connection)
                            updateCmd.Parameters.AddWithValue("@Quantity", newStock)
                            updateCmd.Parameters.AddWithValue("@ProductID", productID)
                            updateCmd.ExecuteNonQuery()


                        End Using
                        MessageBox.Show($"Order placed successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information)

                        Dim query As String = "SELECT CashierID FROM Cashier WHERE Username = @Username"

                        Using cmd As New SqlCommand(query, connection)
                            cmd.Parameters.AddWithValue("@Username", _cashier)

                            Dim result = cmd.ExecuteScalar()
                            If result IsNot Nothing Then
                                cashierID = result.ToString()
                                InsertSale(cashierID, gtotal)
                            Else
                                MessageBox.Show("Username not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning)
                            End If
                        End Using


                    Else
                        MessageBox.Show($"Not enough stock for product {productID}. Available: {currentStock}, Needed: {qty}", "Stock Error", MessageBoxButton.OK, MessageBoxImage.Warning)
                    End If
                End Using
            Next
            selectedLists.Clear()

        Else
            MessageBox.Show("Oops the selected list of items or bill is empty!!", "Purchase Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
            End
        End If
    End Sub




    Private Function InsertSale(cashierID As String, totalAmount As Decimal) As Boolean
        Dim insertSaleQuery As String = "INSERT INTO Sales (CashierID, SaleDate, TotalAmount) VALUES (@CashierID, @SaleDate, @TotalAmount)"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(insertSaleQuery, connection)
                cmd.Parameters.AddWithValue("@CashierID", cashierID)
                cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now)
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)

                cmd.ExecuteNonQuery()



            End Using
        End Using
    End Function

    Private Sub disable()
        If String.IsNullOrEmpty(Total_label.Content?.ToString()) Or Total_label.Content = 0 Then
            Bill.IsReadOnly = True

            Return
        End If
        Bill.IsReadOnly = False

    End Sub

    Private Sub Quantity_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Space Then
            e.Handled = True
        End If
    End Sub

    Private Sub Quantity_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Not Char.IsDigit(CChar(e.Text)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Search_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Char.IsWhiteSpace(CChar(e.Text)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Search_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Space Then
            e.Handled = True
        End If
    End Sub

    Private Sub Bill_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Space Then
            e.Handled = True
        End If
    End Sub
End Class
