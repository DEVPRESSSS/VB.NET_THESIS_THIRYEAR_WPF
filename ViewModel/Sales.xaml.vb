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
        Dim sales As New ObservableCollection(Of Sale)()



        Dim query As String = "SELECT * FROM Sales"
        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                sales.Add(New Sale With {
                .SaleID = Convert.ToInt32(row("SalesID")),
                .CashierID = Convert.ToInt32(row("CashierID").ToString()),
                .SaleDate = row("SaleDate"),
                .TotalAmount = Convert.ToDouble(row("TotalAmount"))
            })
            Next

            salesDataGrid.ItemsSource = sales
        End Using
    End Sub
    Private Sub SearchSales()
        Dim input As String = Search.Text.Trim().ToLower()

        Dim collectionView As CollectionView = CType(CollectionViewSource.GetDefaultView(salesDataGrid.ItemsSource), CollectionView)
        collectionView.Filter = New Predicate(Of Object)(Function(item) FilterSales(item, input))
    End Sub

    Private Function FilterSales(item As Object, input As String) As Boolean
        Dim row As Sale = CType(item, Sale)

        Return row.SaleID.ToString().Contains(input) OrElse
               row.CashierID.ToString().Contains(input) OrElse
               row.TotalAmount.ToString().Contains(input)
    End Function
    Private Sub datePickerFilter_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs)


        'Variable to hold the datetime entered by the user
        Dim filterDate As DateTime = datePickerFilter.SelectedDate

        Dim collectionView As CollectionView = CType(CollectionViewSource.GetDefaultView(salesDataGrid.ItemsSource), CollectionView)
        collectionView.Filter = New Predicate(Of Object)(Function(item) FilterByDate(item, filterDate))

    End Sub

    Private Function FilterByDate(item As Object, selected As DateTime?) As Boolean


        Dim row As Sale = CType(item, Sale)

        If selected.HasValue Then
            Return row.SaleDate.Date = selected.Value.Date


        End If

        Return True

    End Function
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)



    End Sub

    Private Sub SearchSale_Click(sender As Object, e As RoutedEventArgs)
        SearchSales()
    End Sub

    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)
        SearchSales()
    End Sub

    Private Sub print_Click(sender As Object, e As RoutedEventArgs)

        Dim rand As New Random
        Dim randomNumber As Integer = rand.Next()
        Dim randomInRange As Integer = rand.Next(1, 1000)

        Dim filePath As String = $"D:\VB_THESIS_WPS\Prints\Invoice_{randomInRange}.pdf"

        PrintToPDF(salesDataGrid, filePath)
    End Sub
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

                ' Create a table with the number of columns in the DataGrid
                Dim table As New Table(dataGrid.Columns.Count - 1)
                table.SetWidth(UnitValue.CreatePercentValue(100))
                ' Add headers
                For Each column As DataGridColumn In dataGrid.Columns
                    If Not TypeOf column Is DataGridTemplateColumn Then ' Check if it's not an Action column
                        table.AddHeaderCell(New Cell().Add(New Paragraph(column.Header.ToString())))
                    End If
                Next


                ' Add rows
                For Each item In dataGrid.ItemsSource
                    Dim sale As Sale = CType(item, Sale)
                    table.AddCell(New Cell().Add(New Paragraph(sale.SaleID.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(sale.CashierID.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(sale.SaleDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))))
                    table.AddCell(New Cell().Add(New Paragraph(sale.TotalAmount.ToString("C", CultureInfo.CurrentCulture))))
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


End Class
