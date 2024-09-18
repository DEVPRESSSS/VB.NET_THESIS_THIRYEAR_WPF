Class MainWindow
    Public Sub New()
        InitializeComponent()


        MainContentArea.Content = New Dashboard()
    End Sub



    Private Sub Window_MouseDown(sender As Object, e As MouseButtonEventArgs)
        If e.LeftButton = MouseButtonState.Pressed Then

            DragMove()

        End If
    End Sub



    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized

    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As RoutedEventArgs)
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        Else
            WindowState = WindowState.Maximized
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)

        Dim response As MessageBoxResult

        response = MessageBox.Show("Confirmation", "Are you sure you want to exit?", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If response = MessageBoxResult.Yes Then

            Dim login As New LoginView()

            login.Show()
            Me.Close()

        End If





    End Sub



    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        MainContentArea.Content = New Inventory()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        MainContentArea.Content = New Settings()

    End Sub

    Private Sub Sales_Click(sender As Object, e As RoutedEventArgs)

        MainContentArea.Content = New Sales()
    End Sub

    Private Sub Analysis_Click(sender As Object, e As RoutedEventArgs)
        MainContentArea.Content = New Analysis()

    End Sub

    Private Sub Logout(sender As Object, e As RoutedEventArgs)


        Dim result As MessageBoxResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If result = MessageBoxResult.Yes Then
            Dim login As New LoginView()
            login.Show()
            Me.Close()
        Else
            Return
        End If
    End Sub

    Private Sub Button_Click_2(sender As Object, e As RoutedEventArgs)
        MainContentArea.Content = New Dashboard()

    End Sub

    Private Sub EmployeeClick(sender As Object, e As RoutedEventArgs)
        MainContentArea.Content = New Employee()

    End Sub
End Class
