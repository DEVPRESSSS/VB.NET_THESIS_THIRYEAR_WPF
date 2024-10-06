Imports System.Data.SqlClient

Public Class StockIn
    Dim con As New ConnectionString
    Dim editID As Integer
    Dim load As MyInventory

    Public Sub New(Id As Integer, myinventory As MyInventory)

        ' This call is required by the designer.
        InitializeComponent()

        editID = Id
        load = myinventory

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub RefreshForm()

        load.FetchProductData()
    End Sub
    Private Sub EditStock()
        Dim newStocks As Integer = Integer.Parse(newstock.Text)
        Dim currentQuantity As Integer

        Dim selectQuery As String = "SELECT Quantity FROM Inventory WHERE InventoryID = @InventoryID"

        Using mycon As New SqlConnection(con.connectionString)
            mycon.Open()

            Using selectCmd As New SqlCommand(selectQuery, mycon)
                selectCmd.Parameters.AddWithValue("@InventoryID", editID)

                Dim reader As SqlDataReader = selectCmd.ExecuteReader()

                If reader.Read() Then
                    currentQuantity = Convert.ToInt32(reader("Quantity"))
                Else
                    MessageBox.Show("No record found for the given InventoryID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
                    Exit Sub
                End If
                reader.Close()
            End Using

            Dim updateQuery As String = "UPDATE Inventory SET Quantity = Quantity + @Quantity, OriginalStock = @OriginalStock WHERE InventoryID = @InventoryID"

            Using updateCmd As New SqlCommand(updateQuery, mycon)
                updateCmd.Parameters.AddWithValue("@Quantity", newStocks)
                updateCmd.Parameters.AddWithValue("@OriginalStock", currentQuantity + newStocks)
                updateCmd.Parameters.AddWithValue("@InventoryID", editID)

                updateCmd.ExecuteNonQuery()
            End Using
        End Using

        MessageBox.Show("Updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
        RefreshForm()
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub updatebtn_Click(sender As Object, e As RoutedEventArgs)
        EditStock()
    End Sub

    Private Sub newstock_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Not IsNumeric(e.Text) Then
            e.Handled = True ' Reject the input
        End If
    End Sub

    Private Sub newstock_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Back OrElse e.Key = Key.Delete OrElse e.Key = Key.Tab OrElse e.Key = Key.Left OrElse e.Key = Key.Right Then
            e.Handled = False ' Allow these keys
        ElseIf Not (e.Key >= Key.D0 AndAlso e.Key <= Key.D9 OrElse e.Key >= Key.NumPad0 AndAlso e.Key <= Key.NumPad9) Then
            ' If the key is not a number, block it
            e.Handled = True
        End If
    End Sub
End Class
