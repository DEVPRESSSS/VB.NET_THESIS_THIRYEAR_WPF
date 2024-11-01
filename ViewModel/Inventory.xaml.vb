Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports iText.IO.Image
Imports iText.Kernel.Pdf
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports System.Globalization
Imports Microsoft.TeamFoundation.Common
Imports iText.Layout
Imports System.Text

Public Class Inventory
    Dim con As New ConnectionString
    Private add_window As AddProduct = Nothing

    Private Sub AddNewProduct_Click(sender As Object, e As RoutedEventArgs)
        If add_window Is Nothing OrElse Not add_window.IsLoaded Then
            add_window = New AddProduct(Me)
            add_window.Show()
        Else
            add_window.Activate()
        End If

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
                .CreatedAt = CDate(row("CreatedAt")).ToString("yyyy-MM-dd hh:mm")
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

        If String.IsNullOrEmpty(input) Then
            MessageBox.Show("Please provide a value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        Dim products As New List(Of Product)()
        Dim isNumericInput As Boolean = Decimal.TryParse(input, New Decimal())
        Dim isIntegerInput As Boolean = Integer.TryParse(input, New Integer())
        Dim isDateInput As Boolean = DateTime.TryParse(input, Nothing) ' Check if input can be parsed as Date

        ' Prepare SQL query with conditional parameters based on input type
        Dim query As New StringBuilder()
        query.Append("SELECT p.*, c.CategoryName FROM PRODUCT p ")
        query.Append("INNER JOIN Category c ON p.CategoryID = c.CategoryID ")
        query.Append("WHERE 1=1 ") ' Always true, simplifies adding conditions

        ' Conditions based on user input
        If Not String.IsNullOrEmpty(input) Then
            query.Append("AND (p.ProductName LIKE '%' + @ProductName + '%' ")
            query.Append("OR p.Description LIKE '%' + @Description + '%' ")
            query.Append("OR p.Brand LIKE '%' + @Brand + '%' ")
            query.Append("OR p.Size LIKE '%' + @Size + '%' ")
            query.Append("OR p.Color LIKE '%' + @Color + '%' ")

            If isNumericInput Then
                query.Append("OR p.Price = @Price ")
            End If

            If isIntegerInput Then
                query.Append("OR p.CategoryID = @CategoryID ")
                query.Append("OR p.ProductID = @ProductID ")
            End If

            If isDateInput Then
                query.Append("OR CONVERT(DATE, p.CreatedAt) = @CreatedAt ")
            End If

            query.Append("OR c.CategoryName LIKE '%' + @CategoryName + '%' )")
        End If

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using cmd As New SqlCommand(query.ToString(), connection)
                cmd.Parameters.AddWithValue("@ProductName", input)
                cmd.Parameters.AddWithValue("@Description", input)
                cmd.Parameters.AddWithValue("@Brand", input)
                cmd.Parameters.AddWithValue("@Size", input)
                cmd.Parameters.AddWithValue("@Color", input)
                cmd.Parameters.AddWithValue("@CategoryName", input)

                If isNumericInput Then
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(input))
                End If
                If isIntegerInput Then
                    cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(input))
                    cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(input))
                End If

                If isDateInput Then
                    Dim parsedDate As DateTime
                    parsedDate = DateTime.Parse(input) ' Parsing the input date
                    cmd.Parameters.AddWithValue("@CreatedAt", parsedDate)
                Else
                    cmd.Parameters.Add("@CreatedAt", SqlDbType.Date).Value = DBNull.Value
                End If

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
                        .CreatedAt = Convert.ToDateTime(reader("CreatedAt")).ToString("yyyy-MM-dd hh:mm")
                    }

                        products.Add(filter)
                    End While
                End Using
            End Using
        End Using

        productDataGrid.ItemsSource = products
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
                    Dim product As Product = CType(item, Product)
                    table.AddCell(New Cell().Add(New Paragraph(product.ProductID.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.ProductName.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.Description.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.CategoryName.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.Brand.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.Size.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.Color.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(product.Price.ToString())))

                    table.AddCell(New Cell().Add(New Paragraph(String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", product.CreatedAt))))
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
    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

        Dim input As String = Search.Text

        If input.Length = 0 Then

            FetchProductData()


        Else
            FilterProductTable()

        End If
    End Sub

    Private Sub Print_Click(sender As Object, e As RoutedEventArgs)
        Dim rand As New Random
        Dim randomNumber As Integer = rand.Next()
        Dim randomInRange As Integer = rand.Next(1, 1000)

        Dim filePath As String = $"D:\VB_THESIS_WPS\Prints\Cashierlist_{randomInRange}.pdf"


        If productDataGrid.ItemsSource Is Nothing OrElse productDataGrid.Items.Count = 0 OrElse productDataGrid.Columns.Count = 0 Then
            MessageBox.Show("No data available to print.", "Print Cancelled", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        PrintToPDF(productDataGrid, filePath)
    End Sub



    Private Sub Images_Click(sender As Object, e As RoutedEventArgs)

        Dim parentWindow As Window = Window.GetWindow(Me)

        If TypeOf parentWindow Is MainWindow Then
            Dim mainWindow As MainWindow = CType(parentWindow, MainWindow)
            mainWindow.MainContentArea.Content = New ProductImage()
        End If
    End Sub

    Private ViewDetails As ViewDetailsProduct = Nothing

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)



        If ViewDetails Is Nothing OrElse Not ViewDetails.IsLoaded Then
            Dim viewbtn As Button = CType(sender, Button)

            Dim selectedItem As Product = CType(viewbtn.DataContext, Product)

            ViewDetails = New ViewDetailsProduct(
            Integer.Parse(selectedItem.ProductID),
            selectedItem.ProductName,
            selectedItem.Price,
            selectedItem.Description,
            selectedItem.CategoryName,
            selectedItem.Brand,
            selectedItem.Size,
            selectedItem.Color
        )

            ViewDetails.Show()
        Else
            ViewDetails.Activate()
        End If
    End Sub






    'Private Sub datePickerFilter_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs)
    '  If datePickerFilter.SelectedDate.HasValue Then
    ' Dim selectedDate As Date = datePickerFilter.SelectedDate.Value
    '    FilterDataByDate(selectedDate)
    ' End If
    'End Sub
    Private Sub FilterDataByDate(selectedDate As Date)
        Dim query As String = "SELECT p.ProductID, p.ProductName, p.Description, p.Price, p.Brand, p.Size, p.Color, p.CreatedAt, " &
                              "p.CategoryID, c.CategoryName " &
                              "FROM Product p " &
                              "JOIN Category c ON p.CategoryID = c.CategoryID " &
                              "WHERE CONVERT(DATE, p.CreatedAt) = @SelectedDate"

        Dim filteredProducts As New List(Of Product)()

        Using connection As New SqlConnection(con.connectionString)
            Using command As New SqlCommand(query, connection)
                ' Set the parameter for the selected date
                command.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = selectedDate

                connection.Open()
                Using reader As SqlDataReader = command.ExecuteReader()
                    ' Read each record and map it to the Product object
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
                            .CreatedAt = Convert.ToDateTime(reader("CreatedAt")).ToString("yyyy-MM-dd HH:mm")
                        }
                        filteredProducts.Add(filter)
                    End While
                End Using
            End Using
        End Using

        ' Bind the filtered list to your DataGrid or other UI element
        productDataGrid.ItemsSource = filteredProducts
    End Sub


End Class
