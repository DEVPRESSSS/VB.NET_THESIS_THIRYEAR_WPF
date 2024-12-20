﻿Imports System.Data.SqlClient
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

        If selectedCategory Is Nothing OrElse selectedCategory.ProductID = 0 Then
            MessageBox.Show("Please select a valid category.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If



        Dim selectedCatID As Integer = selectedCategory.ProductID
        Dim path As String = txtImagePath.Text




        InsertOrUpdateProductImage(selectedCatID, path)
    End Sub

    Public Sub InsertOrUpdateProductImage(id As Integer, uploadedImage As String)
        If String.IsNullOrEmpty(txtImagePath.Text) Then
            MessageBox.Show("Please choose a file before inserting.", "Insert Failed", MessageBoxButton.OK, MessageBoxImage.Error)
        Else
            Dim imageFolder As String = "D:\VB_THESIS_WPS\ProductImages\"

            If Not Directory.Exists(imageFolder) Then
                Directory.CreateDirectory(imageFolder)
            End If

            Dim fileName As String = Path.GetFileName(uploadedImage)
            Dim uniqueFileName As String = Guid.NewGuid().ToString() & "_" & fileName
            Dim imagePath As String = Path.Combine(imageFolder, uniqueFileName)

            File.Copy(uploadedImage, imagePath, overwrite:=True)

            Dim query As String = "
            IF EXISTS (SELECT 1 FROM ProductImage WHERE ProductID = @ProductID)
                UPDATE ProductImage
                SET ImageUrl = @ImageUrl
                WHERE ProductID = @ProductID
            ELSE
                INSERT INTO ProductImage (ProductID, ImageUrl)
                VALUES (@ProductID, @ImageUrl)
        "

            Using connection As New SqlConnection(con.connectionString)
                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ProductID", id)
                    command.Parameters.AddWithValue("@ImageUrl", imagePath)

                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Image inserted or updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            Refresh()
            ComboCat.SelectedItem = Nothing
            txtImagePath.Text = ""
            imgPreview.Source = Nothing
        End If
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
    Private Sub Refresh()
        imageform.FetchImages()

    End Sub
End Class