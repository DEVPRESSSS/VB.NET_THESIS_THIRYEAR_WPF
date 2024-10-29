Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports iText.IO.Image
Imports iText.Kernel.Pdf
Imports iText.Layout
Imports iText.Layout.Element
Imports iText.Layout.Properties


Public Class Employee

    Public Sub New()
        InitializeComponent()
        FetchCashierData()

    End Sub



    Dim con As New ConnectionString
    Private Sub EditBtn(sender As Object, e As RoutedEventArgs)

        Dim edit_Button As Button = CType(sender, Button)

        Dim selectedCashier As Cashier = CType(edit_Button.DataContext, Cashier)

        Dim passToEdit As New EditEmployee(
         Integer.Parse(selectedCashier.CashierID),
         selectedCashier.Username,
         selectedCashier.FirstName,
         selectedCashier.LastName,
         selectedCashier.Email,
         Me
     )

        passToEdit.Show()





    End Sub
    Private insert_image_window As AddEmployee = Nothing

    Private Sub AddbtnClick(sender As Object, e As RoutedEventArgs)
        If insert_image_window Is Nothing OrElse Not insert_image_window.IsLoaded Then
            insert_image_window = New AddEmployee()
            insert_image_window.Show()
        Else
            insert_image_window.Activate()
        End If





    End Sub
    Dim converter As New BrushConverter()
    Dim myArray As String() = {"#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#6741D9", "#0CA678"}
    Dim fletter As String
    Dim brush As Brush
    Public Sub FetchCashierData()


        Dim cashiers As New ObservableCollection(Of Cashier)()

        Dim query As String = "SELECT * FROM Cashier"

        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For i As Integer = 0 To dataTable.Rows.Count - 1
                Dim row As DataRow = dataTable.Rows(i)

                Dim colorString As String = myArray(i Mod myArray.Length)

                brush = CType(converter.ConvertFromString(colorString), Brush)

                Dim fname As String = row("FirstName").ToString()
                fletter = If(fname.Length > 0, fname.Substring(0, 1), String.Empty)

                cashiers.Add(New Cashier With {
                    .Character = fletter,
                    .BgColor = brush,
                    .CashierID = row("CashierID").ToString(),
                    .Username = row("Username").ToString(),
                    .FirstName = row("FirstName").ToString(),
                    .LastName = row("LastName").ToString(),
                    .Email = row("Email").ToString(),
                    .CreatedAt = CDate(row("CreatedAt")).ToString("yyyy-MM-dd hh:mm")
                })
            Next

            cashierDataGrid.ItemsSource = cashiers
        End Using
    End Sub

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub



    Private Sub Delete_Click(sender As Object, e As RoutedEventArgs)
        Dim deleteButton As Button = CType(sender, Button)

        Dim selectedCashier As Cashier = CType(deleteButton.DataContext, Cashier)

        Dim cashierId As String = selectedCashier.CashierID


        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this cashier?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then
            Dim query As String = "DELETE FROM Cashier WHERE CashierID = @CashierID"

            Using connection As New SqlConnection(con.connectionString)
                Try
                    connection.Open()
                    Dim cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@CashierID", cashierId)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Cashier deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)

                    FetchCashierData()

                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End Using
        End If

    End Sub

    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)

        If Search.Text.Length > 0 Then
            FilterProductTable()

        Else
            FetchCashierData()

        End If

    End Sub

    Private Sub Search_Click(sender As Object, e As RoutedEventArgs)
        FilterProductTable()
    End Sub


    Private Sub FilterProductTable()

        Dim input As String = Search.Text.Trim()
        Dim products As New List(Of Cashier)()
        Dim q As String = "SELECT * FROM Cashier WHERE " &
                  "(UserName LIKE '%' + @UserName + '%' OR @UserName = '') " &
                  "OR (FirstName LIKE '%' + @FirstName + '%' OR @FirstName = '') " &
                  "OR (LastName LIKE '%' + @LastName + '%' OR @LastName = '') " &
                  "OR (Email LIKE '%' + @Email + '%' OR @Email = '') " &
                  "OR (ISNUMERIC(@CashierID) = 1 AND CashierID = @CashierID) " &
                  "OR (CONVERT(DATE, CreatedAt) = @CreatedAt)"


        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using cmd As New SqlCommand(q, connection)
                cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = input
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = input
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = input
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = input
                cmd.Parameters.Add("@CashierID", SqlDbType.Int).Value = If(IsNumeric(input), CInt(input), DBNull.Value)

                Dim parsedDate As DateTime
                cmd.Parameters.Add("@CreatedAt", SqlDbType.Date).Value = If(DateTime.TryParse(input, parsedDate), parsedDate, DBNull.Value)


                Using reader As SqlDataReader = cmd.ExecuteReader()
                    Dim i As Integer = 0
                    While reader.Read()
                        Dim colorString As String = myArray(i Mod myArray.Length)
                        brush = CType(converter.ConvertFromString(colorString), Brush)

                        Dim fname As String = reader("FirstName").ToString()
                        fletter = If(fname.Length > 0, fname.Substring(0, 1), String.Empty)

                        Dim filter As New Cashier() With {
                            .CashierID = Convert.ToInt32(reader("CashierID")),
                            .Username = reader("UserName").ToString(),
                            .FirstName = reader("FirstName").ToString(),
                            .Email = reader("Email").ToString(),
                            .LastName = reader("LastName").ToString(),
                            .Character = fletter,
                            .BgColor = brush,
                            .CreatedAt = CDate(reader("CreatedAt")).ToString("yyyy-MM-dd hh:mm")
                        }

                        products.Add(filter)
                        i += 1
                    End While
                End Using
            End Using
        End Using

        cashierDataGrid.ItemsSource = products
    End Sub


    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub print_Click(sender As Object, e As RoutedEventArgs)
        Dim rand As New Random
        Dim randomNumber As Integer = rand.Next()
        Dim randomInRange As Integer = rand.Next(1, 1000)

        Dim filePath As String = $"D:\VB_THESIS_WPS\Prints\Productlist_{randomInRange}.pdf"


        If cashierDataGrid.ItemsSource Is Nothing OrElse cashierDataGrid.Items.Count = 0 OrElse cashierDataGrid.Columns.Count = 0 Then
            MessageBox.Show("No data available to print.", "Print Cancelled", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        PrintToPDF(cashierDataGrid, filePath)
    End Sub
    Private Sub PrintToPDF(dataGrid As DataGrid, filePath As String)
        Using pdfWriter As New PdfWriter(filePath)
            Using pdfDocument As New PdfDocument(pdfWriter)
                Dim document As New Document(pdfDocument)

                ' Logo data picture
                Dim logoPath As String = "D:\VB_THESIS_WPS\Images\logo.png"
                Dim logo As Image = New Image(ImageDataFactory.Create(logoPath))
                logo.SetHorizontalAlignment(HorizontalAlignment.Center)
                logo.SetWidth(150)
                document.Add(logo)

                'Address data paragraph
                Dim address As String = "Pamela Mabulay Footwear Retail Store" & vbCrLf &
                                        "#725 Quezon Blvd, Zone 030 Brgy. 308 Quiapo" & vbCrLf &
                                        "Manila, Philippines"
                Dim addressParagraph As New Paragraph(address)
                addressParagraph.SetTextAlignment(TextAlignment.CENTER)
                addressParagraph.SetFontSize(12)
                document.Add(addressParagraph)
                document.Add(New Paragraph().SetHeight(20))


                'Printed data paragraph

                Dim printedDate As String = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                Dim concatDate As String = $"Date: {printedDate}"
                Dim printedDateParagraph As New Paragraph(concatDate)
                printedDateParagraph.SetTextAlignment(TextAlignment.LEFT)
                printedDateParagraph.SetFontSize(12)
                document.Add(printedDateParagraph)
                document.Add(New Paragraph().SetHeight(10))





                Dim table As New Table(dataGrid.Columns.Count - 2)
                table.SetWidth(UnitValue.CreatePercentValue(100))

                For Each column As DataGridColumn In dataGrid.Columns
                    If Not TypeOf column Is DataGridTemplateColumn Then
                        table.AddHeaderCell(New Cell().Add(New Paragraph(column.Header.ToString())))
                    End If
                Next


                For Each item In dataGrid.ItemsSource
                    Dim sale As Cashier = CType(item, Cashier)
                    table.AddCell(New Cell().Add(New Paragraph(sale.CashierID.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(sale.Username.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(sale.FirstName.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(sale.LastName.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(sale.Email.ToString())))
                    table.AddCell(New Cell().Add(New Paragraph(String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd}", sale.CreatedAt))))
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
