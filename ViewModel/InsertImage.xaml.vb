Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Win32

Public Class InsertImage

    Dim imageform As ProductImage
    Public Sub New(image As ProductImage)

        InitializeComponent()
        FetchCategory()

        imageform = image

    End Sub
    Dim con As New ConnectionString
    Private Sub btnBrowseImage_Click(sender As Object, e As RoutedEventArgs)
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*"

        openFileDialog.Title = "Select an Image File"

        If openFileDialog.ShowDialog() = True Then
            Dim selectedFilePath As String = openFileDialog.FileName

            txtImagePath.Text = selectedFilePath

            Dim bitmap As New BitmapImage(New Uri(selectedFilePath))
            imgPreview.Source = bitmap
        End If
    End Sub

    Private Sub updatebtn_Click(sender As Object, e As RoutedEventArgs) Handles updatebtn.Click
        Dim selectedCategory = CType(ComboCat.SelectedItem, Product)
        Dim selectedCatID As Integer = selectedCategory.ProductID
        Dim path As String = txtImagePath.Text

        InsertProductImage(selectedCatID, path)
    End Sub

    Public Sub InsertProductImage(id As Integer, uploadedImage As String)
        Dim productImage As New ProductImages()
        Dim imageFolder As String = "D:\VB_THESIS_WPS\ProductImages\"

        If Not Directory.Exists(imageFolder) Then
            Directory.CreateDirectory(imageFolder)
        End If

        Dim fileName As String = Path.GetFileName(uploadedImage)
        Dim uniqueFileName As String = Guid.NewGuid().ToString() & "_" & fileName

        Dim imagePath As String = Path.Combine(imageFolder, uniqueFileName)

        File.Copy(uploadedImage, imagePath)


        Dim query As String = "INSERT INTO ProductImage (ProductID, ImageUrl) VALUES (@ProductID, @ImageUrl)"

        Using connection As New SqlConnection(con.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@ProductID", id)
                command.Parameters.AddWithValue("@ImageUrl", uploadedImage)

                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Image inserted and saved successfully!", MessageBoxImage.Information, MessageBoxButton.OK)
        imageform.FetchImages()
        Me.Close()

    End Sub
    Public Sub FetchCategory()
        Dim query As String = "SELECT ProductID, ProductName FROM Product"
        ComboCat.Items.Clear()

        Using cons As New SqlConnection(con.connectionString)
            Using cmd As New SqlCommand(query, cons)
                cons.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim category As New Product() With {
                        .ProductID = reader("ProductID"),
                        .ProductName = reader("ProductName").ToString()
                    }
                        ComboCat.Items.Add(category)
                    End While
                End Using
            End Using
        End Using
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class