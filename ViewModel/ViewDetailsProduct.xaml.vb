Imports System.Data
Imports System.Data.SqlClient

Public Class ViewDetailsProduct


    Dim cons As New ConnectionString
    Dim currentColor As String
    Dim editID As Integer


    Dim colorHexArray(,) As String = {
    {"Red", "#FF5252"},
    {"Blue", "#1E88E5"},
    {"Green", "#0CA678"},
    {"Yellow", "#FFC107"},
    {"Orange", "#FF8F00"},
    {"Purple", "#8E24AA"},
    {"Teal", "#00796B"},
    {"Brown", "#6D4C41"},
    {"Gray", "#9E9E9E"},
    {"Black", "#000000"},
    {"White", "#FFFFFF"},
    {"Pink", "#C2185B"}
}

    Public Sub New(Id As Integer, pname As String, prices As Double, descrip As String, catname As String, brands As String, sizes As String, colors As String)


        InitializeComponent()

        productname.Text = pname
        Price.Text = prices
        description.Text = descrip
        category.Text = catname
        brand.Text = brands
        size.Text = sizes
        editID = Id
        currentColor = colors



        Dim hexCode As String = GetHexCodeByColor(currentColor)
        If hexCode IsNot Nothing Then

            Dim colorBrush As SolidColorBrush = CType((New BrushConverter().ConvertFromString(hexCode)), SolidColorBrush)
            ColorBorder.Background = colorBrush
            BorderColorOutline.BorderBrush = colorBrush
            '  productname.Foreground = colorBrush
            ' size.Foreground = colorBrush
            ' description.Foreground = colorBrush
            ' Price.Foreground = colorBrush
            ' brand.Foreground = colorBrush
            ' category.Foreground = colorBrush
            ' Price_Copy.Foreground = colorBrush
        Else
            MessageBox.Show("Color not found in the array.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
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


    Private Function GetHexCodeByColor(colorName As String) As String
        For i As Integer = 0 To colorHexArray.GetLength(0) - 1
            If colorHexArray(i, 0).Equals(colorName, StringComparison.OrdinalIgnoreCase) Then
                Return colorHexArray(i, 1)
            End If
        Next
        Return Nothing
    End Function


End Class
