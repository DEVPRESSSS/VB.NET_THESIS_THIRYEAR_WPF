Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports iText.Commons.Actions
Imports iText.IO.Image
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports Microsoft.IdentityModel.Tokens


Public Class POS
    'Connection string
    Dim con As New ConnectionString

    'Collection of products in the cart list
    Private selectedLists As ObservableCollection(Of SelectedItem)
    'cashier value
    Private _cashier As String
    'cashier ID
    Dim cashierID As String = String.Empty

    Public Sub New(cashier As String)
        InitializeComponent()
        selectedLists = New ObservableCollection(Of SelectedItem)()

        Dim products As List(Of Product) = GetProducts()
        GetProducts()
        ProductList.ItemsSource = products
        _cashier = cashier
    End Sub



    'Retrieve casher username logic
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



    'btn close button
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Dim Response As MessageBoxResult

        Response = MessageBox.Show("Confirmation", "Are you sure you want to exit?", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If Response = MsgBoxResult.Yes Then




            Application.Current.Shutdown()

        End If
    End Sub

    'Maximize code
    Private Sub btnMaximize_Click(sender As Object, e As RoutedEventArgs)
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
            ' ProductList.Width = 621
            sellbtn.Width = 445

            shoeswrapper.Width = New GridLength(621, GridUnitType.Pixel)
        Else
            WindowState = WindowState.Maximized
            'ProductList.Width = Double.NaN
            sellbtn.Width = 510
            shoeswrapper.Width = New GridLength(750, GridUnitType.Pixel)
        End If
    End Sub

    'Minimize code
    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub


    'Get specific product
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
                            .Price = Convert.ToDecimal(reader("Price").ToString()),
                            .Size = reader("Size").ToString()
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

    'Search any product
    Private Sub SearchBtn_Click(sender As Object, e As RoutedEventArgs)
        Dim products As List(Of Product) = GetSpecificProduct()

        If products.Any() Then
            ProductList.ItemsSource = products

        Else
            MessageBox.Show("No products found.", "Not found", MessageBoxImage.Error, MessageBoxButton.OK)


        End If
    End Sub


    'Fetch all the products
    Public Function GetProducts() As List(Of Product)
        Dim products As New List(Of Product)

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()
            Dim query As String = "SELECT p.ProductID, p.ProductName, p.Size, p.Price, i.Quantity, pi.ImageUrl " &
                              "FROM Product p " &
                              "INNER JOIN Inventory i ON p.ProductID = i.ProductID " &
                              "LEFT JOIN ProductImage pi ON p.ProductID = pi.ProductID " &
                              "WHERE i.Quantity >= 1"

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim product As New Product()
                        product.ProductID = reader("ProductID")
                        product.ProductName = reader("ProductName").ToString()
                        product.Size = reader("Size").ToString()
                        product.Price = Convert.ToDecimal(reader("Price"))
                        product.Stock = Convert.ToInt32(reader("Quantity"))
                        product.ImageUrl = reader("ImageUrl").ToString()

                        products.Add(product)
                    End While
                End Using
            End Using
        End Using

        Return products
    End Function



    Private Sub StackPanel_Scroll(sender As Object, e As Primitives.ScrollEventArgs)

    End Sub


    'Search product code
    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

        If Search.Text.Length = 0 Then
            Dim products As List(Of Product) = GetProducts()

            ProductList.ItemsSource = products


        End If

    End Sub

    'Add item logic
    Private Sub AddItem_Click(sender As Object, e As RoutedEventArgs)
        Dim button As Button = CType(sender, Button)
        Dim selectedProduct As Product = CType(button.DataContext, Product)

        Dim existingItem As SelectedItem = selectedLists.FirstOrDefault(Function(item) item.ProductID = selectedProduct.ProductID)



        If existingItem IsNot Nothing Then
            existingItem.Quantity += 1


            If existingItem.Quantity > selectedProduct.Stock Then

                MessageBox.Show("Oops the quantity is greater than stock", MessageBoxImage.Error, MessageBoxButton.OK)
                existingItem.Quantity -= 1

                Return

            End If

        Else

                existingItem = New SelectedItem With {
            .ProductID = selectedProduct.ProductID,
            .ProductName = selectedProduct.ProductName,
            .Price = selectedProduct.Price,
            .Quantity = 1
             }
            selectedLists.Add(existingItem)


        End If

        existingItem.SubTotal = existingItem.Quantity * existingItem.Price
        items.ItemsSource = Nothing
        items.ItemsSource = selectedLists
        SumSubtotalColumn()

    End Sub

    'Minus quantity logic
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim minusbtn As Button = CType(sender, Button)
        Dim selectedProduct As SelectedItem = CType(minusbtn.DataContext, SelectedItem)
        Dim total As Decimal = 0D


        If selectedProduct.Quantity > 1 Then

            selectedProduct.Quantity -= 1

            items.ItemsSource = selectedLists

            SumSubtotalColumn()

        Else

            selectedLists.Remove(selectedProduct)

            SumSubtotalColumn()

        End If


    End Sub

    'Remove item logic
    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)



        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this item??", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then


            Dim deleteButton As Button = CType(sender, Button)
            Dim selectedProduct As SelectedItem = CType(deleteButton.DataContext, SelectedItem)
            selectedLists.Remove(selectedProduct)

            items.ItemsSource = Nothing
            items.ItemsSource = selectedLists
            Bill.Text = ""
            Subtotalcount.Content = ""

            SumSubtotalColumn()


        End If
    End Sub

    'Windows mousedown logic
    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()

        End If
    End Sub

    'Subtotal column logic

    Private Sub SumSubtotalColumn()
        Dim total As Decimal = 0D
        For Each item As SelectedItem In selectedLists
            total += item.SubTotal
            item.GrandTotal = total
        Next
        Subtotalcount.Content = total.ToString("C")
    End Sub

    'Insert sales into database
    Private Sub InsertSale(cashierID As String, totalAmount As Decimal)
        Dim salesID As Integer
        Dim insertSaleQuery As String = "INSERT INTO Sales (CashierID, SaleDate, TotalAmount) OUTPUT INSERTED.SalesID VALUES (@CashierID, @SaleDate, @TotalAmount)"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(insertSaleQuery, connection)
                cmd.Parameters.AddWithValue("@CashierID", cashierID)
                cmd.Parameters.AddWithValue("@SaleDate", DateTime.Now)
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount)

                salesID = Convert.ToInt32(cmd.ExecuteScalar())
                InsertSalesDetails(salesID)

            End Using
        End Using

    End Sub


    'INSERT INTO SALES DETAILS

    Private Sub InsertSalesDetails(salesID As Integer)
        Dim insertSaleDetailsQuery As String = "INSERT INTO SalesDetails (SalesID, ProductName, Quantity, UnitPrice) " &
                                           "VALUES (@SalesID, @ProductName, @Quantity, @UnitPrice)"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(insertSaleDetailsQuery, connection)

                ' Iterate through the selected products and add details
                For Each items As SelectedItem In selectedLists
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@SalesID", salesID)
                    cmd.Parameters.AddWithValue("@ProductName", items.ProductName)
                    cmd.Parameters.AddWithValue("@Quantity", items.Quantity)
                    cmd.Parameters.AddWithValue("@UnitPrice", items.Price)

                    cmd.ExecuteNonQuery()
                Next

            End Using
        End Using
    End Sub


    'Sell button logic
    Private Sub sellbtn_Click(sender As Object, e As RoutedEventArgs)

        If Subtotalcount.Content = "₱0.00" Then

            MessageBox.Show("The cart items are empty", "Purchase Error", MessageBoxButton.OK, MessageBoxImage.Error)

            Bill.Text = ""
            ChangeCount.Content = ""

            Return
        End If


        Dim billAmount As Decimal
        If String.IsNullOrEmpty(Bill.Text) OrElse Not Decimal.TryParse(Bill.Text, billAmount) Then
            MessageBox.Show("The cart items are empty or the bill amount is invalid.", "Purchase Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        Dim productID As String
        Dim qty, currentStock As Integer
        Dim totalAmount As Decimal = 0D
        Dim q As String = "SELECT Quantity FROM Inventory WHERE ProductID = @ProductID"
        Dim updateQuery As String = "UPDATE Inventory SET Quantity = @Quantity WHERE ProductID = @ProductID"
        Dim cashierID As String = String.Empty
        Dim cashierQuery As String = "SELECT CashierID FROM Cashier WHERE Username = @Username"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using transaction = connection.BeginTransaction()
                Try
                    Using cmd As New SqlCommand(cashierQuery, connection, transaction)
                        cmd.Parameters.AddWithValue("@Username", _cashier)
                        Dim result = cmd.ExecuteScalar()
                        If result Is Nothing Then
                            MessageBox.Show("Cashier not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                            transaction.Rollback()
                            Return
                        End If
                        cashierID = result.ToString()
                    End Using

                    For Each item As SelectedItem In selectedLists
                        productID = item.ProductID
                        qty = item.Quantity

                        Using cmd As New SqlCommand(q, connection, transaction)
                            cmd.Parameters.AddWithValue("@ProductID", productID)
                            Dim stockResult = cmd.ExecuteScalar()
                            If stockResult Is Nothing Then
                                MessageBox.Show($"Product ID {productID} not found in inventory.", "Stock Error", MessageBoxButton.OK, MessageBoxImage.Error)
                                transaction.Rollback()
                                Return
                            End If
                            currentStock = CInt(stockResult)
                        End Using

                        ' Check if stock is sufficient
                        If currentStock >= qty Then
                            Dim newStock As Integer = currentStock - qty

                            ' Update the stock in the Inventory table
                            Using updateCmd As New SqlCommand(updateQuery, connection, transaction)
                                updateCmd.Parameters.AddWithValue("@Quantity", newStock)
                                updateCmd.Parameters.AddWithValue("@ProductID", productID)
                                updateCmd.ExecuteNonQuery()
                            End Using

                            ' Accumulate the total amount for the sale
                            totalAmount += item.Quantity * item.Price

                        Else
                            MessageBox.Show($"Not enough stock for product: {productID}", "Stock Error", MessageBoxButton.OK, MessageBoxImage.Error)
                            transaction.Rollback()
                            Return ' Stop the process if any product is out of stock
                        End If
                    Next

                    If billAmount < totalAmount Then
                        MessageBox.Show($"The bill amount ({billAmount:C}) is less than the total purchase amount ({totalAmount:C}).", "Payment Error", MessageBoxButton.OK, MessageBoxImage.Error)
                        transaction.Rollback()
                        Return
                    End If

                    ' Insert the sale
                    InsertSale(cashierID, totalAmount)

                    ' Commit the transaction
                    transaction.Commit()

                    ' Show success message after completing the process
                    MessageBox.Show($"Order placed successfully. Total: {totalAmount:C}", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                    PrintReceipt()
                    selectedLists.Clear()

                    Bill.Text = ""

                    Subtotalcount.Content = ""


                    Dim updateProducts = GetProducts()
                    UpdateProductUI(updateProducts)

                Catch ex As Exception
                    ' In case of any error, roll back the transaction
                    transaction.Rollback()
                    MessageBox.Show($"An error occurred: {ex.Message}", "Transaction Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End Using
        End Using


    End Sub


    'Update the Product in realtime
    Private Sub UpdateProductUI(products As List(Of Product))
        ProductList.ItemsSource = products
    End Sub

    'Disable the bill textbox
    Private Sub Disable()
        If String.IsNullOrEmpty(Subtotalcount.Content?.ToString()) Or Subtotalcount.Content = 0 Then
            Bill.IsReadOnly = True

            Return
        End If
        Bill.IsReadOnly = False
    End Sub


    'Generate receipt
    Private Sub PrintReceipt()


        Dim rand As New Random
        Dim randomNumber As Integer = rand.Next()
        Dim randomInRange As Integer = rand.Next(1, 1000)
        Dim filePath As String = $"D:\VB_THESIS_WPS\Prints\Receipt_{randomInRange}.pdf"



        Using pdfWriter As New PdfWriter(filePath)
            Using pdfDocument As New PdfDocument(pdfWriter)
                Dim document As New Document(pdfDocument)

                ' Add Logo
                Dim logoPath As String = "D:\VB_THESIS_WPS\Images\logo.png" ' 
                Dim logo As Image = New Image(ImageDataFactory.Create(logoPath))
                logo.SetHorizontalAlignment(HorizontalAlignment.Center)
                logo.SetWidth(150)
                document.Add(logo)

                ' Add Company Address
                Dim address As String = "Pamela Mabulay Footwear Retail Store" & vbCrLf &
                                        "#725 Quezon Blvd, Zone 030 Brgy. 308 Quiapo" & vbCrLf &
                                        "Manila, Philippines" & vbCrLf & "09947294323" & vbCrLf & "pamelamabulayfootwear@gmail.com"
                Dim addressParagraph As New Paragraph(address)
                addressParagraph.SetTextAlignment(TextAlignment.CENTER)
                addressParagraph.SetFontSize(12)
                document.Add(addressParagraph)
                document.Add(New Paragraph().SetHeight(20))

                'Printed date paragraph
                Dim printedDate As String = DateTime.Now.ToString("yyyy-MM-dd   hh:mm tt", CultureInfo.InvariantCulture)
                Dim concatDate As String = $"Date: {printedDate}"
                Dim printedDateParagraph As New Paragraph(concatDate)
                printedDateParagraph.SetTextAlignment(TextAlignment.RIGHT)
                printedDateParagraph.SetFontSize(12)
                document.Add(printedDateParagraph)
                document.Add(New Paragraph().SetHeight(10))

                'Printed Cashier
                Dim printedCashier As String = $"Cashier Name: {_cashier}"
                Dim paragraph_cashier As New Paragraph(printedCashier)
                paragraph_cashier.SetTextAlignment(TextAlignment.LEFT)
                paragraph_cashier.SetFontSize(12)
                document.Add(paragraph_cashier)
                document.Add(New Paragraph().SetHeight(10))


                ' Title Section
                Dim title As String = "RECEIPT"
                Dim formatTitle As New Paragraph(title)
                formatTitle.SetTextAlignment(TextAlignment.CENTER)
                document.Add(formatTitle)

                ' Header Section for the table
                Dim table As New Table(4) ' Table with 4 columns (ProductName, Price, Quantity, Subtotal)
                table.SetWidth(UnitValue.CreatePercentValue(100))

                table.AddCell(New Cell().Add(New Paragraph("Product Name").SetBold().SetTextAlignment(TextAlignment.CENTER)))
                table.AddCell(New Cell().Add(New Paragraph("Price").SetBold().SetTextAlignment(TextAlignment.CENTER)))
                table.AddCell(New Cell().Add(New Paragraph("Quantity").SetBold().SetTextAlignment(TextAlignment.CENTER)))
                table.AddCell(New Cell().Add(New Paragraph("Subtotal").SetBold().SetTextAlignment(TextAlignment.CENTER)))

                ' Add items dynamically
                Dim total As Decimal = 0

                For Each item As SelectedItem In selectedLists
                    table.AddCell(New Cell().Add(New Paragraph(item.ProductName).SetTextAlignment(TextAlignment.CENTER)))
                    table.AddCell(New Cell().Add(New Paragraph(item.Price.ToString("C2")).SetTextAlignment(TextAlignment.CENTER)))
                    table.AddCell(New Cell().Add(New Paragraph(item.Quantity.ToString()).SetTextAlignment(TextAlignment.CENTER)))
                    table.AddCell(New Cell().Add(New Paragraph(item.SubTotal.ToString("C2")).SetTextAlignment(TextAlignment.CENTER)))

                    total = item.GrandTotal ' 
                Next

                document.Add(table)


                ' Grand total and Change section
                Dim gtotal As String = $"Bill:{Bill.Text}                 Change: {ChangeCount.Content}         Grand Total: {total.ToString("C2")}"
                Dim gtotal_format As New Paragraph(gtotal)
                gtotal_format.SetTextAlignment(TextAlignment.RIGHT)
                gtotal_format.SetBold()
                document.Add(gtotal_format)




                MessageBox.Show("Receipt generated  successfully: ", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            End Using
        End Using

        Process.Start(New ProcessStartInfo(filePath) With {
        .UseShellExecute = True
    })
    End Sub


    'Log out code
    Private Sub LogoutBtn_Click(sender As Object, e As RoutedEventArgs)

        Dim confirmation As MessageBoxResult = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If confirmation = MessageBoxResult.Yes Then
            Dim logout As New LoginView()
            logout.Show()
            Me.Hide()

        End If

    End Sub

    Private Sub Bill_TextChanged(sender As Object, e As TextChangedEventArgs)

        If Bill.Text.Length > 0 Then

            Dim change As Decimal = 0D
            change = Bill.Text - Subtotalcount.Content
            ChangeCount.Content = $"{change}"
        Else
            ChangeCount.Content = $"0"

        End If

    End Sub

    Private Sub Bill_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Back OrElse e.Key = Key.Delete OrElse e.Key = Key.Left OrElse e.Key = Key.Right Then
            Return
        End If

        If Not ((e.Key >= Key.D0 And e.Key <= Key.D9) OrElse
                (e.Key >= Key.NumPad0 And e.Key <= Key.NumPad9) OrElse
                (e.Key = Key.Decimal OrElse e.Key = Key.OemPeriod)) Then
            e.Handled = True
        End If
    End Sub
End Class
