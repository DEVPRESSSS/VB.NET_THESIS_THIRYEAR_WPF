Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class POS
    Dim con As New ConnectionString
    Public Sub New()
        InitializeComponent()
        FetchProductData()

    End Sub
    Public Sub FetchProductData()

        Dim prod As New ObservableCollection(Of Product)()

        Dim query As String = "SELECT p.ProductID, p.ProductName, p.Price, p.Description, p.CategoryID, c.CategoryName, p.Brand, p.Size, p.Color, p.CreatedAt " &
                          "FROM Product p JOIN Category c ON p.CategoryID = c.CategoryID"
        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                prod.Add(New Product With {
                .ProductID = Convert.ToInt32(row("ProductID")),
                .ProductName = row("ProductName").ToString(),
                .Price = Convert.ToDecimal(row("Price")),
                .Description = row("Description"),
                .CategoryID = Convert.ToInt32(row("CategoryID")),
                 .CategoryName = row("CategoryName").ToString(),
                .Brand = row("Brand").ToString(),
                .Size = row("Size").ToString(),
                .Color = row("Color").ToString(),
                .CreatedAt = row("CreatedAt")
            })
            Next

            productDataGrid.ItemsSource = prod
        End Using
    End Sub
    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)

        Dim result As MsgBoxResult = MessageBox.Show("Confirmation", "Are you sure you want to exit?", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MsgBoxResult.Yes Then
            Me.Close()

        Else

            MessageBox.Show("I miss you")
            Return
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
End Class
