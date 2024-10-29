Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Layout.Properties
Imports iText.IO.Image
Imports System.Globalization

Public Class Sales
    Dim con As New ConnectionString


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        FetchSale()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FetchSale()
        Dim salesDetailsCollection As New ObservableCollection(Of SaleDetails)()

        Dim query As String = "SELECT s.[SalesID], s.[CashierID], s.[SaleDate], s.[TotalAmount], " &
                       "sd.[SalesDetailID], sd.[ProductName], sd.[Quantity], sd.[UnitPrice], sd.[TotalPrice] " &
                       "FROM [Pamela].[dbo].[Sales] s " &
                       "INNER JOIN [Pamela].[dbo].[SalesDetails] sd " &
                       "ON s.[SalesID] = sd.[SalesID] " &
                       "ORDER BY s.[SalesID];"

        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                Dim saleDetails As New SaleDetails With {
                .SalesDetailsID = Convert.ToInt32(row("SalesDetailID")),
                .SaleID = Convert.ToInt32(row("SalesID")),
                .CashierID = Convert.ToInt32(row("CashierID")),
                .SaleDate = If(IsDBNull(row("SaleDate")), String.Empty, Convert.ToDateTime(row("SaleDate")).ToString("yyyy-MM-dd HH:mm")),
                .TotalAmount = Convert.ToDecimal(row("TotalAmount")),
                .ProductName = row("ProductName").ToString(),
                .Quantity = Convert.ToInt32(row("Quantity")),
                .UnitPrice = Convert.ToDecimal(row("UnitPrice")),
                .TotalPrice = Convert.ToDecimal(row("TotalPrice"))
            }

                salesDetailsCollection.Add(saleDetails)
            Next

            salesDataGrid.ItemsSource = salesDetailsCollection
        End Using
    End Sub








    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)



    End Sub
    'Search Sale
    Private Sub SearchSale_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

        If Search.Text.Length > 0 Then
            SearchSales()
        Else
            FetchSale()

        End If
    End Sub

    'Print click button
    Private Sub print_Click(sender As Object, e As RoutedEventArgs)

        Dim rand As New Random
        Dim randomNumber As Integer = rand.Next()
        Dim randomInRange As Integer = rand.Next(1, 1000)

        Dim filePath As String = $"D:\VB_THESIS_WPS\Prints\Invoice_{randomInRange}.pdf"

        PrintToPDF(salesDataGrid, filePath)
    End Sub
    'Pdf printer
    Private Sub PrintToPDF(dataGrid As DataGrid, filePath As String)
        Using pdfWriter As New PdfWriter(filePath)
            Using pdfDocument As New PdfDocument(pdfWriter)
                Dim document As New Document(pdfDocument)

                ' Add Logo
                Dim logoPath As String = "D:\VB_THESIS_WPS\Images\logo.png" ' Change to your logo path
                Dim logo As Image = New Image(ImageDataFactory.Create(logoPath))
                logo.SetHorizontalAlignment(HorizontalAlignment.Center)
                logo.SetWidth(150)
                document.Add(logo)

                ' Add Company Address
                Dim address As String = "Pamela Mabulay Footwear Retail Store" & vbCrLf &
                                        "#725 Quezon Blvd, Zone 030 Brgy. 308 Quiapo" & vbCrLf &
                                        "Manila, Philippines"
                Dim addressParagraph As New Paragraph(address)
                addressParagraph.SetTextAlignment(TextAlignment.CENTER)
                addressParagraph.SetFontSize(12)
                document.Add(addressParagraph)

                document.Add(New Paragraph().SetHeight(20))

                ' Printed date paragraph
                Dim printedDate As String = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                Dim concatDate As String = $"Date: {printedDate}"
                Dim printedDateParagraph As New Paragraph(concatDate)
                printedDateParagraph.SetTextAlignment(TextAlignment.LEFT)
                printedDateParagraph.SetFontSize(12)
                document.Add(printedDateParagraph)
                document.Add(New Paragraph().SetHeight(10))

                ' Create a table with the number of columns in the DataGrid
                Dim table As New Table(dataGrid.Columns.Count)
                table.SetWidth(UnitValue.CreatePercentValue(100))

                ' Add headers
                For Each column As DataGridColumn In dataGrid.Columns
                    If Not TypeOf column Is DataGridTemplateColumn Then ' Check if it's not an Action column
                        table.AddHeaderCell(New Cell().Add(New Paragraph(column.Header.ToString())))
                    End If
                Next

                ' Add rows
                For Each item In dataGrid.ItemsSource
                    Dim saleDetails As SaleDetails = CType(item, SaleDetails)

                    ' Add SaleDetails columns to the table
                    table.AddCell(New Cell().Add(New Paragraph(saleDetails.SaleID.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(saleDetails.CashierID.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(saleDetails.ProductName)))
                    table.AddCell(New Cell().Add(New Paragraph(saleDetails.UnitPrice.ToString("C", CultureInfo.CurrentCulture))))
                    table.AddCell(New Cell().Add(New Paragraph(saleDetails.Quantity.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(saleDetails.TotalPrice.ToString("C", CultureInfo.CurrentCulture))))
                    table.AddCell(New Cell().Add(New Paragraph(String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", saleDetails.SaleDate))))
                    table.AddCell(New Cell().Add(New Paragraph(saleDetails.TotalAmount.ToString("C", CultureInfo.CurrentCulture))))
                Next

                document.Add(table)
                document.Close()
                MessageBox.Show("Receipt created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            End Using
        End Using

        Process.Start(New ProcessStartInfo(filePath) With {
          .UseShellExecute = True
        })
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub SearchSales()


        Dim searchTerm As String = Search.Text.Trim()
        Dim salesDetailsCollection As New ObservableCollection(Of SaleDetails)()

        ' Adjust the SQL query to filter based on the search term
        Dim query As String = "SELECT s.[SalesID], s.[CashierID], s.[SaleDate], s.[TotalAmount], " &
                      "sd.[SalesDetailID], sd.[ProductName], sd.[Quantity], sd.[UnitPrice], sd.[TotalPrice] " &
                      "FROM [Pamela].[dbo].[Sales] s " &
                      "INNER JOIN [Pamela].[dbo].[SalesDetails] sd " &
                      "ON s.[SalesID] = sd.[SalesID] " &
                      "WHERE sd.[ProductName] LIKE @searchTerm OR " &
                      "s.[SalesID] LIKE @searchTerm OR " &
                      "s.[CashierID] LIKE @searchTerm OR " &
                      "s.[SaleDate] LIKE @searchTerm OR " &
                      "s.[TotalAmount] LIKE @searchTerm OR " &
                      "sd.[Quantity] LIKE @searchTerm OR " &
                      "sd.[UnitPrice] LIKE @searchTerm OR " &
                      "sd.[TotalPrice] LIKE @searchTerm " &
                      "ORDER BY s.[SalesID];"


        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")

            ' Dim parsedDate As DateTime
            '  command.Parameters.Add("@SaleDate", SqlDbType.Date).Value = If(DateTime.TryParse(searchTerm, parsedDate), parsedDate, DBNull.Value)

            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                Dim saleDetails As New SaleDetails With {
                .SalesDetailsID = Convert.ToInt32(row("SalesDetailID")),
                .SaleID = Convert.ToInt32(row("SalesID")),
                .CashierID = Convert.ToInt32(row("CashierID")),
                 .SaleDate = If(IsDBNull(row("SaleDate")), String.Empty, Convert.ToDateTime(row("SaleDate")).ToString("yyyy-MM-dd HH:mm")),
                .TotalAmount = Convert.ToDecimal(row("TotalAmount")),
                .ProductName = row("ProductName").ToString(),
                .Quantity = Convert.ToInt32(row("Quantity")),
                .UnitPrice = Convert.ToDecimal(row("UnitPrice")),
                .TotalPrice = Convert.ToDecimal(row("TotalPrice"))
            }

                salesDetailsCollection.Add(saleDetails)
            Next



            salesDataGrid.ItemsSource = salesDetailsCollection
        End Using
    End Sub

End Class
