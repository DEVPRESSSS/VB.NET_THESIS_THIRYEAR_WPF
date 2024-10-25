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

        Dim images As New ObservableCollection(Of shoesimages)()
        Dim q As String = "SELECT ImageID, ProductID, ImageUrl, CreatedAt FROM ProductImage"

        Using connection As New SqlConnection(con.connectionString)

            Using cmd As New SqlCommand(q, connection)

                Using adapter As New SqlDataAdapter(cmd)

                    Dim datatable As New DataTable()

                    connection.Open()
                    adapter.Fill(datatable)


                    For Each row As DataRow In datatable.Rows

                        images.Add(New shoesimages With {
                              .ImageID = Convert.ToInt32(row("ImageID")),
                              .ProductID = Convert.ToInt32(row("ProductID")),
                              .ImageUrl = row("ImageUrl").ToString(),
                              .CreatedAt = row("CreatedAt")
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

    End Sub
    Private insert_image_window As InsertImage = Nothing

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        If insert_image_window Is Nothing OrElse Not insert_image_window.IsLoaded Then
            insert_image_window = New InsertImage()
            insert_image_window.Show()
        Else
            insert_image_window.Activate()
        End If

    End Sub




End Class
