Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports iText.IO.Image
Imports iText.Kernel.Pdf
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports System.Globalization
Imports iText.Layout

Public Class MyInventory

    Dim con As New ConnectionString()

    Dim pi As ProductInventory

    Public Sub New()


        ' This call is required by the designer.
        InitializeComponent()
        FetchProductData()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FilterInventoryTable()
        Dim input As String = Search.Text.Trim()

        If String.IsNullOrEmpty(input) Then
            MessageBox.Show("Error", "Please provide a value", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        Dim inventoryItems As New List(Of ProductInventory)()

        ' Modified query to include ProductName filter
        Dim q As String = "SELECT P.InventoryID, P.ProductID, S.ProductName, P.Quantity, P.OriginalStock, P.LastUpdated " &
                      "FROM Inventory P INNER JOIN Product S ON P.ProductID = S.ProductID " &
                      "WHERE (@InventoryID = '' OR P.InventoryID = @InventoryID) " &
                      "OR (@ProductID = '' OR P.ProductID = @ProductID) " &
                      "OR (@Quantity = '' OR P.Quantity = @Quantity) " &
                      "OR (@OriginalStock = '' OR P.OriginalStock = @OriginalStock) " &
                      "OR (@LastUpdated = '' OR CONVERT(VARCHAR, P.LastUpdated, 120) LIKE '%' + @LastUpdated + '%') " &
                      "OR (@ProductName = '' OR S.ProductName LIKE '%' + @ProductName + '%')"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using cmd As New SqlCommand(q, connection)
                If IsNumeric(input) Then
                    cmd.Parameters.AddWithValue("@InventoryID", Convert.ToInt32(input))
                    cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(input))
                    cmd.Parameters.AddWithValue("@OriginalStock", Convert.ToInt32(input))
                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(input))

                Else
                    cmd.Parameters.AddWithValue("@InventoryID", DBNull.Value)
                    cmd.Parameters.AddWithValue("@ProductID", DBNull.Value)
                    cmd.Parameters.AddWithValue("@OriginalStock", DBNull.Value)
                    cmd.Parameters.AddWithValue("@Quantity", DBNull.Value)

                End If

                If DateTime.TryParse(input, Nothing) Then
                    cmd.Parameters.AddWithValue("@LastUpdated", input)
                Else
                    cmd.Parameters.AddWithValue("@LastUpdated", DBNull.Value)
                End If

                ' Add parameter for ProductName
                If Not IsNumeric(input) AndAlso Not DateTime.TryParse(input, Nothing) Then
                    cmd.Parameters.AddWithValue("@ProductName", input)
                Else
                    cmd.Parameters.AddWithValue("@ProductName", DBNull.Value)
                End If

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim inventoryItem As New ProductInventory() With {
                        .InventoryID = Convert.ToInt32(reader("InventoryID")),
                        .ProductID = Convert.ToInt32(reader("ProductID")),
                        .ProductName = reader("ProductName").ToString(),
                        .LastUpdated = Convert.ToDateTime(reader("LastUpdated")),
                        .CurrentStock = Convert.ToInt32(reader("Quantity")),
                        .OriginalStock = Convert.ToInt32(reader("OriginalStock"))
                    }

                        inventoryItems.Add(inventoryItem)
                    End While
                End Using
            End Using
        End Using

        productDataGrid.ItemsSource = inventoryItems
    End Sub




    Public Sub FetchProductData()

        Dim prod As New ObservableCollection(Of ProductInventory)()

        Dim query As String = "SELECT P.InventoryID, P.ProductID, S.ProductName, P.Quantity, P.OriginalStock, P.LastUpdated FROM Inventory P INNER JOIN Product S ON P.ProductID = S.ProductID"
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
                .ProductName = row("ProductName").ToString(),
                .CurrentStock = Convert.ToInt32(row("Quantity")),
                .OriginalStock = Convert.ToInt32(row("OriginalStock")),
                .LastUpdated = Convert.ToDateTime(row("LastUpdated"))
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

    Private Sub datePickerFilter_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim filterDate As DateTime = datePickerFilter.SelectedDate

        Dim collectionView As CollectionView = CType(CollectionViewSource.GetDefaultView(productDataGrid.ItemsSource), CollectionView)
        collectionView.Filter = New Predicate(Of Object)(Function(item) FilterByDate(item, filterDate))

    End Sub



    Private Function FilterByDate(item As Object, selected As DateTime?) As Boolean


        Dim row As ProductInventory = CType(item, ProductInventory)

        If selected.HasValue Then
            Return row.LastUpdated.Date = selected.Value.Date


        End If

        Return True
    End Function
    Private Function FilterSales(item As Object, input As String) As Boolean
        Dim row As ProductInventory = CType(item, ProductInventory)

        If row.InventoryID.ToString().Contains(input) OrElse
             row.ProductID.ToString().Contains(input) OrElse
             row.CurrentStock.ToString().Contains(input) OrElse
             row.OriginalStock.ToString().Contains(input) OrElse
             row.LastUpdated.ToString().Contains(input) Then

            Return True

        Else


            Return False

        End If

    End Function


    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        FilterInventoryTable()

    End Sub



    Private Sub PrintToPDF(dataGrid As DataGrid, filePath As String)
        Using pdfWriter As New PdfWriter(filePath)
            Using pdfDocument As New PdfDocument(pdfWriter)
                Dim document As New Document(pdfDocument)

                ' Add Logo
                Dim logoPath As String = "D:\VB_THESIS_WPS\Images\logo.png"
                Dim logo As Image = New Image(ImageDataFactory.Create(logoPath))
                logo.SetHorizontalAlignment(HorizontalAlignment.Center)
                logo.SetWidth(150)
                document.Add(logo)

                ' Address paragraph
                Dim address As String = "Pamela Mabulay Footwear Retail Store" & vbCrLf &
                                        "#725 Quezon Blvd, Zone 030 Brgy. 308 Quiapo" & vbCrLf &
                                        "Manila, Philippines"
                Dim addressParagraph As New Paragraph(address)
                addressParagraph.SetTextAlignment(TextAlignment.CENTER)
                addressParagraph.SetFontSize(12)
                document.Add(addressParagraph)
                document.Add(New Paragraph().SetHeight(20))



                'Printed date paragraph
                Dim printedDate As String = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                Dim concatDate As String = $"Date: {printedDate}"
                Dim printedDateParagraph As New Paragraph(concatDate)
                printedDateParagraph.SetTextAlignment(TextAlignment.LEFT)
                printedDateParagraph.SetFontSize(12)
                document.Add(printedDateParagraph)
                document.Add(New Paragraph().SetHeight(10))


                Dim table As New Table(dataGrid.Columns.Count - 1)
                table.SetWidth(UnitValue.CreatePercentValue(100))

                For Each column As DataGridColumn In dataGrid.Columns
                    If Not TypeOf column Is DataGridTemplateColumn Then
                        table.AddHeaderCell(New Cell().Add(New Paragraph(column.Header.ToString())))
                    End If
                Next


                For Each item In dataGrid.ItemsSource
                    Dim product As ProductInventory = CType(item, ProductInventory)
                    table.AddCell(New Cell().Add(New Paragraph(product.InventoryID.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.ProductName.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.CurrentStock.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.OriginalStock.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.LastUpdated.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))))
                Next

                document.Add(table)
                document.Close()
                MessageBox.Show("PDF created successfully: ", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            End Using
        End Using

        Process.Start(New ProcessStartInfo(filePath) With {
        .UseShellExecute = True
    })
    End Sub

    Private Sub Print_Click_1(sender As Object, e As RoutedEventArgs)
        Dim rand As New Random
        Dim randomNumber As Integer = rand.Next()
        Dim randomInRange As Integer = rand.Next(1, 1000)

        Dim filePath As String = $"D:\VB_THESIS_WPS\Prints\Inventory_{randomInRange}.pdf"


        If productDataGrid.ItemsSource Is Nothing OrElse productDataGrid.Items.Count = 0 OrElse productDataGrid.Columns.Count = 0 Then
            MessageBox.Show("No data available to print.", "Print Cancelled", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        PrintToPDF(productDataGrid, filePath)
    End Sub

    Private Sub editbtn_Click(sender As Object, e As RoutedEventArgs)
        Dim edit_Button As Button = CType(sender, Button)

        Dim selectedId As ProductInventory = CType(edit_Button.DataContext, ProductInventory)

        Dim passToEdit As New StockIn(
         selectedId.InventoryID,
         Me
     )

        passToEdit.Show()
    End Sub

    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

        If Search.Text.Length > 0 Then
            FilterInventoryTable()

        Else

            FetchProductData()
        End If
    End Sub
End Class
