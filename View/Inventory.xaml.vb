Imports System.Collections.ObjectModel

Public Class Inventory
    Private Sub AddNewProduct_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Public Sub New()
        InitializeComponent()

        Dim converter As New BrushConverter()
        Dim members As New ObservableCollection(Of Member)()

        members.Add(New Member With {
            .Number = "1",
            .Character = "J",
            .BgColor = CType(converter.ConvertFromString("#1098AD"), Brush),
            .Name = "John Doe",
            .Position = "Coach",
            .Email = "john.doe@gmail.com",
            .Phone = "415-954-1475"
        })
        members.Add(New Member With {
            .Number = "2",
            .Character = "R",
            .BgColor = CType(converter.ConvertFromString("#1E88E5"), Brush),
            .Name = "Reza Alavi",
            .Position = "Administrator",
            .Email = "reza110@hotmail.com",
            .Phone = "254-451-7893"
        })
        members.Add(New Member With {
            .Number = "3",
            .Character = "D",
            .BgColor = CType(converter.ConvertFromString("#FF8F00"), Brush),
            .Name = "Dennis Castillo",
            .Position = "Coach",
            .Email = "deny.cast@gmail.com",
            .Phone = "125-520-0141"
        })
        members.Add(New Member With {
            .Number = "4",
            .Character = "G",
            .BgColor = CType(converter.ConvertFromString("#FF5252"), Brush),
            .Name = "Gabriel Cox",
            .Position = "Coach",
            .Email = "coxcox@gmail.com",
            .Phone = "808-635-1221"
        })
        members.Add(New Member With {
            .Number = "5",
            .Character = "L",
            .BgColor = CType(converter.ConvertFromString("#0CA678"), Brush),
            .Name = "Lena Jones",
            .Position = "Manager",
            .Email = "lena.offi@hotmail.com",
            .Phone = "320-658-9174"
        })
        members.Add(New Member With {
            .Number = "6",
            .Character = "B",
            .BgColor = CType(converter.ConvertFromString("#6741D9"), Brush),
            .Name = "Benjamin Caliword",
            .Position = "Administrator",
            .Email = "beni12@hotmail.com",
            .Phone = "114-203-6258"
        })
        members.Add(New Member With {
            .Number = "7",
            .Character = "S",
            .BgColor = CType(converter.ConvertFromString("#FF6D00"), Brush),
            .Name = "Sophia Muris",
            .Position = "Coach",
            .Email = "sophi.muri@gmail.com",
            .Phone = "852-233-6854"
        })
        members.Add(New Member With {
            .Number = "8",
            .Character = "A",
            .BgColor = CType(converter.ConvertFromString("#FF5252"), Brush),
            .Name = "Ali Pormand",
            .Position = "Manager",
            .Email = "alipor@yahoo.com",
            .Phone = "968-378-4849"
        })
        members.Add(New Member With {
            .Number = "9",
            .Character = "F",
            .BgColor = CType(converter.ConvertFromString("#1E88E5"), Brush),
            .Name = "Frank Underwood",
            .Position = "Manager",
            .Email = "frank@yahoo.com",
            .Phone = "301-584-6966"
        })
        members.Add(New Member With {
            .Number = "10",
            .Character = "S",
            .BgColor = CType(converter.ConvertFromString("#0CA678"), Brush),
            .Name = "Saeed Dasman",
            .Position = "Coach",
            .Email = "saeed.dasi@hotmail.com",
            .Phone = "817-320-5052"
        })

        members.Add(New Member With {
            .Number = "11",
            .Character = "J",
            .BgColor = CType(converter.ConvertFromString("#1098AD"), Brush),
            .Name = "John Doe",
            .Position = "Coach",
            .Email = "john.doe@gmail.com",
            .Phone = "415-954-1475"
        })
        members.Add(New Member With {
            .Number = "12",
            .Character = "R",
            .BgColor = CType(converter.ConvertFromString("#1E88E5"), Brush),
            .Name = "Reza Alavi",
            .Position = "Administrator",
            .Email = "reza110@hotmail.com",
            .Phone = "254-451-7893"
        })
        members.Add(New Member With {
            .Number = "13",
            .Character = "D",
            .BgColor = CType(converter.ConvertFromString("#FF8F00"), Brush),
            .Name = "Dennis Castillo",
            .Position = "Coach",
            .Email = "deny.cast@gmail.com",
            .Phone = "125-520-0141"
        })
        members.Add(New Member With {
            .Number = "14",
            .Character = "G",
            .BgColor = CType(converter.ConvertFromString("#FF5252"), Brush),
            .Name = "Gabriel Cox",
            .Position = "Coach",
            .Email = "coxcox@gmail.com",
            .Phone = "808-635-1221"
        })
        members.Add(New Member With {
            .Number = "15",
            .Character = "L",
            .BgColor = CType(converter.ConvertFromString("#0CA678"), Brush),
            .Name = "Lena Jones",
            .Position = "Manager",
            .Email = "lena.offi@hotmail.com",
            .Phone = "320-658-9174"
        })
        members.Add(New Member With {
            .Number = "16",
            .Character = "B",
            .BgColor = CType(converter.ConvertFromString("#6741D9"), Brush),
            .Name = "Benjamin Caliword",
            .Position = "Administrator",
            .Email = "beni12@hotmail.com",
            .Phone = "114-203-6258"
        })
        members.Add(New Member With {
            .Number = "17",
            .Character = "S",
            .BgColor = CType(converter.ConvertFromString("#FF6D00"), Brush),
            .Name = "Sophia Muris",
            .Position = "Coach",
            .Email = "sophi.muri@gmail.com",
            .Phone = "852-233-6854"
        })
        members.Add(New Member With {
            .Number = "18",
            .Character = "A",
            .BgColor = CType(converter.ConvertFromString("#FF5252"), Brush),
            .Name = "Ali Pormand",
            .Position = "Manager",
            .Email = "alipor@yahoo.com",
            .Phone = "968-378-4849"
        })
        members.Add(New Member With {
            .Number = "19",
            .Character = "F",
            .BgColor = CType(converter.ConvertFromString("#1E88E5"), Brush),
            .Name = "Frank Underwood",
            .Position = "Manager",
            .Email = "frank@yahoo.com",
            .Phone = "301-584-6966"
        })
        members.Add(New Member With {
            .Number = "20",
            .Character = "S",
            .BgColor = CType(converter.ConvertFromString("#0CA678"), Brush),
            .Name = "Saeed Dasman",
            .Position = "Coach",
            .Email = "saeed.dasi@hotmail.com",
            .Phone = "817-320-5052"
        })

        members.Add(New Member With {
            .Number = "21",
            .Character = "J",
            .BgColor = CType(converter.ConvertFromString("#1098AD"), Brush),
            .Name = "John Doe",
            .Position = "Coach",
            .Email = "john.doe@gmail.com",
            .Phone = "415-954-1475"
        })
        members.Add(New Member With {
            .Number = "22",
            .Character = "R",
            .BgColor = CType(converter.ConvertFromString("#1E88E5"), Brush),
            .Name = "Reza Alavi",
            .Position = "Administrator",
            .Email = "reza110@hotmail.com",
            .Phone = "254-451-7893"
        })
        members.Add(New Member With {
            .Number = "23",
            .Character = "D",
            .BgColor = CType(converter.ConvertFromString("#FF8F00"), Brush),
            .Name = "Dennis Castillo",
            .Position = "Coach",
            .Email = "deny.cast@gmail.com",
            .Phone = "125-520-0141"
        })
        members.Add(New Member With {
            .Number = "24",
            .Character = "G",
            .BgColor = CType(converter.ConvertFromString("#FF5252"), Brush),
            .Name = "Gabriel Cox",
            .Position = "Coach",
            .Email = "coxcox@gmail.com",
            .Phone = "808-635-1221"
        })
        members.Add(New Member With {
            .Number = "25",
            .Character = "L",
            .BgColor = CType(converter.ConvertFromString("#0CA678"), Brush),
            .Name = "Lena Jones",
            .Position = "Manager",
            .Email = "lena.offi@hotmail.com",
            .Phone = "320-658-9174"
        })
        members.Add(New Member With {
            .Number = "26",
            .Character = "B",
            .BgColor = CType(converter.ConvertFromString("#6741D9"), Brush),
            .Name = "Benjamin Caliword",
            .Position = "Administrator",
            .Email = "beni12@hotmail.com",
            .Phone = "114-203-6258"
        })
        members.Add(New Member With {
            .Number = "27",
            .Character = "S",
            .BgColor = CType(converter.ConvertFromString("#FF6D00"), Brush),
            .Name = "Sophia Muris",
            .Position = "Coach",
            .Email = "sophi.muri@gmail.com",
            .Phone = "852-233-6854"
        })
        members.Add(New Member With {
            .Number = "28",
            .Character = "A",
            .BgColor = CType(converter.ConvertFromString("#FF5252"), Brush),
            .Name = "Ali Pormand",
            .Position = "Manager",
            .Email = "alipor@yahoo.com",
            .Phone = "968-378-4849"
        })
        members.Add(New Member With {
            .Number = "29",
            .Character = "F",
            .BgColor = CType(converter.ConvertFromString("#1E88E5"), Brush),
            .Name = "Frank Underwood",
            .Position = "Manager",
            .Email = "frank@yahoo.com",
            .Phone = "301-584-6966"
        })
        members.Add(New Member With {
            .Number = "30",
            .Character = "S",
            .BgColor = CType(converter.ConvertFromString("#0CA678"), Brush),
            .Name = "Saeed Dasman",
            .Position = "Coach",
            .Email = "saeed.dasi@hotmail.com",
            .Phone = "817-320-5052"
        })

        membersDataGrid.ItemsSource = members
    End Sub



    Public Class Member
        Public Property Character As String
        Public Property BgColor As Brush
        Public Property Number As String
        Public Property Name As String
        Public Property Position As String
        Public Property Email As String
        Public Property Phone As String
    End Class

    Private Sub EditBtn(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
