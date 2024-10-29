Public Class Product
    Public Property ProductID As Integer

    Public Property ProductName As String
    Public Overrides Function ToString() As String
        Return ProductName
    End Function


    Public Property Price As Double
    Public Property Description As String
    Public Property CategoryID As Integer
    Public Property CategoryName As String
    Public Property Brand As String
    Public Property Size As String
    Public Property Color As String

    Public Property CreatedAt As String
    Public Property Stock As Integer
    Public Property ImageUrl As String



End Class
