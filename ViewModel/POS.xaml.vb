Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient


Public Class POS
    Dim con As New ConnectionString
    Private selectedLists As ObservableCollection(Of SelectedItem)
    Private _cashier As String
    Dim cashierID As String = String.Empty

    Public Sub New(cashier As String)
        InitializeComponent()
        selectedLists = New ObservableCollection(Of SelectedItem)()
        Dim products As List(Of Product) = GetProducts()
        ProductList.ItemsSource = products
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
            ProductList.Width = 621
            'shoeswrapper.Width = New GridLength(621, GridUnitType.Pixel)
        Else
            WindowState = WindowState.Maximized
            ProductList.Width = Double.NaN
            ' shoeswrapper.Width = New GridLength(800, GridUnitType.Pixel)
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub



    Public Function GetSpecificProduct() As List(Of Product)
        Dim input As String = Search.Text.Trim()
        Dim products As New List(Of Product)

        If String.IsNullOrEmpty(input) Then
            MessageBox.Show("Please input a value", "Error", MessageBoxButton.OK, MessageBoxImage.Error)

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


                            Dim product As New Product() With {
                            .ProductName = reader("ProductName").ToString(),
                            .Price = Convert.ToDecimal(reader("Price").ToString())
                            }
                            products.Add(product)



                        End While
                    Else
                        MessageBox.Show("Product Name or Product ID does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    End If
                End Using
            End Using
        End Using

        Return products
    End Function

    Private Sub SearchBtn_Click(sender As Object, e As RoutedEventArgs)
        Dim products As List(Of Product) = GetSpecificProduct()

        If products.Any() Then
            ProductList.ItemsSource = products


        End If
    End Sub

    Public Function GetProducts() As List(Of Product)
        Dim products As New List(Of Product)

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()
            Dim query As String = "SELECT ProductID, ProductName, Price FROM Product"
            Dim command As New SqlCommand(query, connection)
            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                Dim product As New Product() With {
                    .ProductID = reader("ProductID"),
                    .ProductName = reader("ProductName").ToString(),
                    .Price = Decimal.Parse(reader("Price").ToString())
                }
                products.Add(product)
            End While
        End Using

        Return products
    End Function

    Private Sub StackPanel_Scroll(sender As Object, e As Primitives.ScrollEventArgs)

    End Sub

    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

        If Search.Text.Length = 0 Then
            Dim products As List(Of Product) = GetProducts()
            ProductList.ItemsSource = products


        End If

    End Sub

    Private Sub AddItem_Click(sender As Object, e As RoutedEventArgs)
        Dim button As Button = CType(sender, Button)
        Dim selectedProduct As Product = CType(button.DataContext, Product)



        Dim existingItem As SelectedItem = selectedLists.FirstOrDefault(Function(item) item.ProductID = selectedProduct.ProductID)



        If existingItem IsNot Nothing Then



            existingItem.Quantity += 1
            items.ItemsSource = Nothing
            items.ItemsSource = selectedLists

        Else

            selectedLists.Add(New SelectedItem With {
            .ProductID = selectedProduct.ProductID,
            .ProductName = selectedProduct.ProductName,
            .Price = selectedProduct.Price
        })

        End If


        items.ItemsSource = selectedLists


    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim minusbtn As Button = CType(sender, Button)
        Dim selectedProduct As SelectedItem = CType(minusbtn.DataContext, SelectedItem)


        If selectedProduct.Quantity > 1 Then

            selectedProduct.Quantity -= 1
            items.ItemsSource = Nothing
            items.ItemsSource = selectedLists
        Else

            selectedLists.Remove(selectedProduct)

        End If


    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)



        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this item??", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then


            Dim deleteButton As Button = CType(sender, Button)
            Dim selectedProduct As SelectedItem = CType(deleteButton.DataContext, SelectedItem)
            selectedLists.Remove(selectedProduct)

            items.ItemsSource = Nothing
            items.ItemsSource = selectedLists

        End If
    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()

        End If
    End Sub
End Class
