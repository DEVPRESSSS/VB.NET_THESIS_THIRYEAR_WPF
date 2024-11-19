Imports System.Data.SqlClient

Class Application

    Dim con As New ConnectionString()

    Public Sub New()


    End Sub



    Private Sub InventoryCount()
        Dim query As String = "SELECT COUNT(*) FROM Inventory WHERE Quantity = 0"
        Using connection As New SqlConnection(con.connectionString)
            connection.Open()
            Using cmd As New SqlCommand(query, connection)

                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())



            End Using

        End Using

    End Sub


End Class
