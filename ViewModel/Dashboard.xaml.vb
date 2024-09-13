Imports System.Data.SqlClient

Public Class Dashboard

    Dim cons As New ConnectionString
    Public Sub New()

        InitializeComponent()
        ProductCount()
        CashierCount()
    End Sub


    Private Sub ProductCount()
        Dim query As String = "SELECT COUNT(*) FROM Product"
        Using connection As New SqlConnection(cons.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(query, connection)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                ProductCounts.Text = count
            End Using

        End Using

    End Sub



    Private Sub CashierCount()
        Dim query As String = "SELECT COUNT(*) FROM Cashier"
        Using connection As New SqlConnection(cons.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(query, connection)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                CashierCounts.Text = count
            End Using

        End Using

    End Sub

    Private Sub SalesCount()
        Dim query As String = "SELECT COUNT(*) FROM Sale"
        Using connection As New SqlConnection(cons.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(query, connection)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                CashierCounts.Text = count
            End Using

        End Using

    End Sub

End Class
