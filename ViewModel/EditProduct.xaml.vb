

Imports System.Data.SqlClient

Public Class EditProduct
    Dim cons As New ConnectionString
    Public Sub New()

        InitializeComponent()

    End Sub

    Public Sub New(Id As Integer, pname As String, prices As Double, descrip As String, catname As String, brands As String, sizes As String, colors As String)

        InitializeComponent()

        ProductName.Text = pname
        Description.Text = descrip
        ComboCat.Items.Add(catname.ToString())
        Brand.Text = brands
        Size.Text = sizes
        Color.Text = colors
        Price.Text = prices

    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)

        WindowState = WindowState.Minimized
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Hide()
    End Sub

    Private Sub ProductName_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub ProductName_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Description_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Description_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Clear(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Size_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Size_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Price_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Price_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Color_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Color_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Brand_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub



    Private Sub ComboCat_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)


    End Sub
    Public Sub FetchCategory()


        Dim query As String = "SELECT CategoryName FROM Category"
        ComboCat.Items.Clear()

        Using con As New SqlConnection(cons.connectionString)

            Using cmd As New SqlCommand(query, con)

                con.Open()

                Using reader As SqlDataReader = cmd.ExecuteReader()


                    While reader.Read()

                        ComboCat.Items.Add(reader("CategoryName").ToString())
                    End While


                End Using



            End Using
        End Using
    End Sub

    Private Sub ComboCat_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)


        FetchCategory()

    End Sub

    Private Sub Update_Click(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
