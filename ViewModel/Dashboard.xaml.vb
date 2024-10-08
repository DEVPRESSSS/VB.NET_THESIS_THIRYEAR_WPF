﻿Imports System.Data.SqlClient

Public Class Dashboard

    Dim cons As New ConnectionString
    Public Sub New()

        InitializeComponent()
        ProductCount()
        CashierCount()
        InventoryCount()
        SaleCount()
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

    Private Sub SaleCount()
        Dim query As String = "SELECT COUNT(*) FROM Sales"
        Using connection As New SqlConnection(cons.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(query, connection)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                Sale.Text = count
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

    Private Sub InventoryCount()
        Dim query As String = "SELECT COUNT(*) FROM Inventory WHERE Quantity= 0"
        Using connection As New SqlConnection(cons.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(query, connection)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                ZeroCounts.Text = count
            End Using

        End Using

    End Sub

End Class
