Imports System.Data
Imports System.Data.SqlClient

Public Class ViewDetailsProduct


    Dim cons As New ConnectionString

    Dim editID As Integer
    Public Sub New(Id As Integer, pname As String, prices As Double, descrip As String, catname As String, brands As String, sizes As String, colors As String)


        InitializeComponent()

        productname.Text = pname
        Price.Text = prices
        description.Text = descrip
        category.Text = catname
        brand.Text = brands
        size.Text = sizes
        editID = Id


        FetchImagePath()

    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub


    Private Sub FetchImagePath()

        Dim query As String = "SELECT ImageUrl FROM ProductImage Where ProductID= @ProductID"
        Using connection As New SqlConnection(cons.connectionString)

            connection.Open()

            Using cmd As New SqlCommand(query, connection)


                cmd.Parameters.AddWithValue("@ProductID", editID)

                Using reader As SqlDataReader = cmd.ExecuteReader()

                    If reader.Read() Then
                        Dim imageUrl As String = reader("ImageUrl").ToString()

                        SetImageSource(imageUrl)

                    End If
                End Using



            End Using


        End Using
    End Sub


    Private Sub SetImageSource(imageUrl As String)
        Dim defaultImg As String = "D:\VB_THESIS_WPS\Images\logo.png"
        Dim bitmap As New BitmapImage()

        If Not String.IsNullOrEmpty(imageUrl) Then
            Try
                bitmap.BeginInit()
                bitmap.UriSource = New Uri(imageUrl, UriKind.RelativeOrAbsolute)
                bitmap.EndInit()
                Imagepreview.Source = bitmap
            Catch ex As Exception
                bitmap = New BitmapImage(New Uri(defaultImg, UriKind.RelativeOrAbsolute))
                Imagepreview.Source = bitmap
            End Try
        Else
            bitmap = New BitmapImage(New Uri(defaultImg, UriKind.RelativeOrAbsolute))
            Imagepreview.Source = bitmap
        End If
    End Sub

End Class
