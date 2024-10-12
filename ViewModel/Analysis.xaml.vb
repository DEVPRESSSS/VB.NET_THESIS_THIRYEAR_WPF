Imports System.Data
Imports System.Data.SqlClient
Imports LiveCharts
Imports LiveCharts.Wpf

Public Class Analysis

    Private con As New ConnectionString
    Public Sub New()


        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FetchMonthlySalesData()
    End Sub
    Private Sub FetchMonthlySalesData()
        Dim monthlySales As New Dictionary(Of String, Decimal)

        Dim query As String = "SELECT YEAR(SaleDate) AS Year, MONTH(SaleDate) AS Month, SUM(TotalAmount) AS MonthlyTotal " &
                          "FROM [Pamela].[dbo].[Sales] " &
                          "GROUP BY YEAR(SaleDate), MONTH(SaleDate) " &
                          "ORDER BY YEAR(SaleDate), MONTH(SaleDate);"

        Using connection As New SqlConnection(con.connectionString)
            Dim command As New SqlCommand(query, connection)
            Dim adapter As New SqlDataAdapter(command)
            Dim dataTable As New DataTable()

            connection.Open()
            adapter.Fill(dataTable)

            For Each row As DataRow In dataTable.Rows
                Dim year As Integer = Convert.ToInt32(row("Year"))
                Dim month As Integer = Convert.ToInt32(row("Month"))
                Dim totalAmount As Decimal = Convert.ToDecimal(row("MonthlyTotal"))

                Dim monthYear As String = $"{year}-{month:D2}" ' Format as Year-Month (e.g., "2024-01")
                monthlySales(monthYear) = totalAmount
            Next
        End Using

        ' Now that we have the monthly sales data, let's populate the chart
        PopulateChart(monthlySales)
    End Sub

    Private Sub PopulateChart(monthlySales As Dictionary(Of String, Decimal))
        ' Create a list to hold the values for the chart
        Dim salesValues2024 As New ChartValues(Of Decimal)
        Dim salesValues2023 As New ChartValues(Of Decimal)
        Dim labels As New List(Of String)

        For Each monthYear In monthlySales.Keys
            If monthYear.StartsWith("2024") Then
                salesValues2024.Add(monthlySales(monthYear))
            ElseIf monthYear.StartsWith("2023") Then
                salesValues2023.Add(monthlySales(monthYear))
            End If

            labels.Add(monthYear.Substring(5)) ' Extract the month part for the labels
        Next

        ' Assuming you have a reference to the chart
        Dim cartesianChart = CType(FindName("test"), CartesianChart) ' Change "yourChartName" to the actual name of your chart

        ' Update the series with new data
        cartesianChart.Series.Add(New ColumnSeries With {
            .Title = "2024",
            .Values = salesValues2024,
            .Fill = Brushes.Blue ' Add color for the series
        })
        cartesianChart.Series.Add(New ColumnSeries With {
        .Title = "2023",
        .Values = salesValues2023,
        .Fill = Brushes.Red ' Add color for the series
    })


        ' Update the X-axis labels
        cartesianChart.AxisX(0).Labels = labels
    End Sub


End Class
