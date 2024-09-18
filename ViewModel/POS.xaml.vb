Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.TeamFoundation.Build.WebApi
Imports Microsoft.TeamFoundation.Common

Public Class POS
    Dim con As New ConnectionString
    Private selectedLists As ObservableCollection(Of SelectedItem)

    Public Sub New()
        InitializeComponent()
        disable()
        selectedLists = New ObservableCollection(Of SelectedItem)()
    End Sub




    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Dim Response As MessageBoxResult

        Response = MessageBox.Show("Confirmation", "Are you sure you want to exit?", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If Response = MsgBoxResult.Yes Then
            Me.Close()

        End If
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As RoutedEventArgs)
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        Else
            WindowState = WindowState.Maximized
        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub

    Private Sub SearchBtn_Click(sender As Object, e As RoutedEventArgs)
        FilterProductTable()
    End Sub

    Private Sub FilterProductTable()
        Dim input As String = Search.Text.Trim()
        Dim Selected As New ObservableCollection(Of SelectedItem)()
        Dim q As String = "SELECT p.*, c.CategoryName FROM PRODUCT p " &
                      "INNER JOIN Category c ON p.CategoryID = c.CategoryID " &
                      "WHERE (p.ProductName LIKE '%' + @ProductName + '%' OR @ProductName = '') " &
                      "OR (p.ProductID = @ProductID OR @ProductID = '')"

        Using connection As New SqlConnection(con.connectionString)
            connection.Open()

            Using cmd As New SqlCommand(q, connection)
                cmd.Parameters.AddWithValue("@ProductName", If(String.IsNullOrEmpty(input), "", input))
                cmd.Parameters.AddWithValue("@ProductID", If(IsNumeric(input), input, DBNull.Value))

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    If reader.HasRows Then
                        While reader.Read()
                            ID.Content = reader("ProductID").ToString()
                            ProductName.Text = reader("ProductName").ToString()
                            Size.Text = reader("Size").ToString()
                            Price.Text = reader("Price").ToString()
                        End While
                    Else

                        MessageBox.Show("Productname or ProductID not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                        Clear()
                        Return
                    End If

                End Using
            End Using
        End Using

    End Sub

    Private Sub Clear()
        ID.Content = ""
        ProductName.Text = ""
        Size.Text = ""
        Price.Text = ""
        Quantity.Text = ""
        Search.Text = ""
    End Sub



    Private Sub AddItem_Click(sender As Object, e As RoutedEventArgs)

        Dim qty As Integer

        If String.IsNullOrEmpty(ProductName.Text) OrElse String.IsNullOrEmpty(Size.Text) OrElse String.IsNullOrEmpty(Price.Text) Then
            MessageBox.Show("Please fill in all fields before adding.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If
        If Integer.TryParse(Quantity.Text, qty) Then


            Dim newItem As New SelectedItem() With {
                    .ProductID = Convert.ToInt32(ID.Content),
                    .ProductName = ProductName.Text,
                    .Size = Size.Text,
                    .Price = Convert.ToDecimal(Price.Text),
                    .Quantity = Convert.ToInt32(qty),
                    .SubTotal = Convert.ToDecimal(qty * Price.Text)
                }

            selectedLists.Add(newItem)

            productDataGrid.ItemsSource = selectedLists
            SumSubtotalColumn()
            disable()
            Clear()
        Else
            MessageBox.Show("Please put a quantity first before adding it to the selected items list.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)

            Return
        End If
    End Sub

    Private Sub productDataGrid_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub SumSubtotalColumn()
        Dim total As Decimal = 0D

        For Each item As SelectedItem In selectedLists
            total += item.SubTotal
            item.GrandTotal = total
        Next

        Total_label.Content = total.ToString("C")
    End Sub

    Private Sub Remove_Click(sender As Object, e As RoutedEventArgs)
        Dim deleteButton As Button = CType(sender, Button)

        Dim selectedProduct As SelectedItem = CType(deleteButton.DataContext, SelectedItem)

        Dim productID As String = selectedProduct.ProductID


        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to delete this item in the list?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then

            selectedLists.Remove(selectedProduct)
            SumSubtotalColumn()
            disable()
        End If
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim bills As Decimal



        If Decimal.TryParse(Bill.Text, bills) Then

            If bills < Total_label.Content Then

                MessageBox.Show("Bill is not enough", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error)
                Return
            End If

            bills -= Total_label.Content
            Changes.Content = bills


            selectedLists.Clear()


        Else

            MessageBox.Show("Oops the selected list of item is empty!!", "Purchase Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

    End Sub

    Private Sub disable()
        If String.IsNullOrEmpty(Total_label.Content?.ToString()) Or Total_label.Content = 0 Then
            Bill.IsReadOnly = True

            Return
        End If
        Bill.IsReadOnly = False

    End Sub
End Class
