﻿Imports System.Data
Imports System.Data.SqlClient

Public Class AddProduct

    Dim con As New ConnectionString
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

    Private Sub Size_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Size_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Brand_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Insert_Click(sender As Object, e As RoutedEventArgs)


        Dim pname As String = ProductName.Text
        Dim descrip As String = Description.Text
        Dim brands As String = Brand.Text
        Dim Sizes As String = Size.Text
        Dim Colors As String = Color.Text
        Dim prices As Decimal
        If Not Decimal.TryParse(Price.Text, prices) Then
            MessageBox.Show("Please enter a valid decimal value for the price.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(pname) OrElse String.IsNullOrWhiteSpace(descrip) _
               OrElse String.IsNullOrWhiteSpace(brands) OrElse String.IsNullOrWhiteSpace(Colors) _
               OrElse String.IsNullOrWhiteSpace(Sizes) Then
            MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning)
            Return
        End If



        InsertCashier(pname, descrip, brands, Sizes, Colors, prices)

    End Sub


    Public Function InsertCashier(pname As String, descrip As String, brands As String, Sizes As String, Colors As String, prices As Decimal)



        Dim query As String = "INSERT INTO [dbo].[Product] ([ProductName], [Description], [Category], [Brand], [Size], [Color], [Price]) " &
                          "VALUES (@ProductName, @Description, @Category, @Brand, @Size, @Color, @Price)"

        Using connection As New SqlConnection(con.connectionString)
            Using command As New SqlCommand(query, connection)
                ' Use SqlParameter with specific types and sizes
                command.Parameters.Add(New SqlParameter("@ProductName", SqlDbType.NVarChar, 150)).Value = pname
                command.Parameters.Add(New SqlParameter("@Description", SqlDbType.NVarChar, 500)).Value = descrip
                command.Parameters.Add(New SqlParameter("@Category", SqlDbType.NVarChar, 100)).Value = brands

                command.Parameters.Add(New SqlParameter("@Brand", SqlDbType.NVarChar, 100)).Value = brands
                command.Parameters.Add(New SqlParameter("@Size", SqlDbType.NVarChar, 50)).Value = Sizes
                command.Parameters.Add(New SqlParameter("@Color", SqlDbType.NVarChar, 50)).Value = Colors
                command.Parameters.Add(New SqlParameter("@Price", SqlDbType.Decimal)).Value = prices
                command.Parameters("@Price").Precision = 18
                command.Parameters("@Price").Scale = 2




                connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Product record inserted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
                    Return True

            End Using
        End Using


    End Function
    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Color_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If (e.Key < Key.A OrElse e.Key > Key.Z) AndAlso e.Key <> Key.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Color_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Hide()
    End Sub

    Private Sub Price_TextChanged(sender As Object, e As TextChangedEventArgs)

    End Sub

    Private Sub Price_PreviewKeyDown(sender As Object, e As KeyEventArgs)

    End Sub

    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then
            DragMove()
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub
End Class