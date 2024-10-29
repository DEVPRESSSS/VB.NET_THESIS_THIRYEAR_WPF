Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class ProductImage

    Dim con As New ConnectionString()
    Public Sub New()


        InitializeComponent()
        FetchImages()

    End Sub

    Public Sub FetchImages()

        Dim images As New ObservableCollection(Of ProductImages)()
        Dim q As String = "SELECT ImageID, ProductID, ImageUrl, CreatedAt FROM ProductImage"

        Using connection As New SqlConnection(con.connectionString)

            Using cmd As New SqlCommand(q, connection)

                Using adapter As New SqlDataAdapter(cmd)

                    Dim datatable As New DataTable()

                    connection.Open()
                    adapter.Fill(datatable)


                    For Each row As DataRow In datatable.Rows

                        images.Add(New ProductImages With {
                              .ImageID = Convert.ToInt32(row("ImageID")),
                              .ProductID = Convert.ToInt32(row("ProductID")),
                              .ImageUrl = row("ImageUrl").ToString(),
                              .CreatedAt = CDate(row("CreatedAt")).ToString("yyyy-MM-dd")
                        })


                    Next
                    IMAGEDataGrid.ItemsSource = images

                End Using


            End Using


        End Using

    End Sub
    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Dim deleteButton As Button = CType(sender, Button)

        Dim selectedimage As ProductImages = CType(deleteButton.DataContext, ProductImages)

        Dim productid As String = selectedimage.ProductID


        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this cashier?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then
            Dim query As String = "DELETE FROM ProductImage WHERE ProductID = @ProductID"

            Using connection As New SqlConnection(con.connectionString)
                Try
                    connection.Open()
                    Dim cmd As New SqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@ProductID", productid)

                    cmd.ExecuteNonQuery()

                    MessageBox.Show("Product image deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information)


                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                End Try
            End Using
        End If
    End Sub
    Private insert_image_window As InsertImage = Nothing

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        If insert_image_window Is Nothing OrElse Not insert_image_window.IsLoaded Then
            insert_image_window = New InsertImage(Me)
            insert_image_window.Show()
        Else
            insert_image_window.Activate()
        End If

    End Sub

    Private Sub Search_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Search.Text.Length > 0 Then
            SearhImage()
        Else
            FetchImages()

        End If
    End Sub

    Private Sub Button_Click_3(sender As Object, e As RoutedEventArgs)
        SearhImage()
    End Sub
    Private Sub SearhImage()
        Dim images As New List(Of ProductImages)()

        Dim input As String = Search.Text.Trim()

        Dim query As String = "SELECT ImageID, ProductID, ImageUrl, CreatedAt FROM ProductImage WHERE 1=1"

        If Not String.IsNullOrEmpty(input) Then
            query += " AND (ImageID LIKE '%' + @ImageID + '%' OR ProductID LIKE '%' + @ProductID + '%' OR ImageUrl LIKE '%' + @ImageUrl + '%' OR CONVERT(DATE, CreatedAt) = @CreatedAt)"
        End If

        Using connecton As New SqlConnection(con.connectionString)
            connecton.Open()
            Using cmd As New SqlCommand(query, connecton)
                cmd.Parameters.AddWithValue("@ImageID", If(IsNumeric(input), input, DBNull.Value))
                cmd.Parameters.AddWithValue("@ProductID", If(IsNumeric(input), input, DBNull.Value))
                cmd.Parameters.AddWithValue("@ImageUrl", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@CreatedAt", If(DateTime.TryParse(input, Nothing), DateTime.Parse(input), DBNull.Value))

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim filter As New ProductImages() With {
                            .ImageID = Convert.ToInt32(reader("ImageID")),
                            .ProductID = Convert.ToInt32(reader("ProductID")),
                            .ImageUrl = reader("ImageUrl").ToString(),
                            .CreatedAt = CDate(reader("CreatedAt")).ToString("yyyy-MM-dd")
                        }
                        images.Add(filter)
                    End While
                End Using

                IMAGEDataGrid.ItemsSource = images
            End Using
        End Using
    End Sub





End Class
