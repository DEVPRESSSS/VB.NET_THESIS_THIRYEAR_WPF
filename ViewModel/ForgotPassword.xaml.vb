Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Text.RegularExpressions

Public Class ForgotPassword

    Public Sub New()


        InitializeComponent()

        '  Code.IsReadOnly = True
    End Sub
    Private Sub Verify_Click(sender As Object, e As RoutedEventArgs)
        Dim emailInput As String = Email.Text

        If String.IsNullOrWhiteSpace(emailInput) Then
            MessageBox.Show("Please enter an email address.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        Dim connections As New ConnectionString()
        Dim query As String = "SELECT Email FROM Admin WHERE Email = @Email UNION SELECT Email FROM Cashier WHERE Email = @Email"
        Dim foundEmail As String = String.Empty

        Using connection As New SqlConnection(connections.connectionString)
            Using command As New SqlCommand(query, connection)
                command.Parameters.AddWithValue("@Email", emailInput)
                connection.Open()

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        foundEmail = reader("Email").ToString()
                    End If
                End Using
            End Using
        End Using

        If Not String.IsNullOrEmpty(foundEmail) Then
            Dim otp As String = GenerateOTP()

            SendOTPEmail(foundEmail, otp)

            Application.Current.Properties("OTP") = otp
            Application.Current.Properties("Email") = foundEmail
            Code.IsReadOnly = False

        Else
            MessageBox.Show("Email not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Email.Text = ""

        End If
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub btnMinimize_Click_1(sender As Object, e As RoutedEventArgs)

    End Sub
    Public Function GenerateOTP() As String
        Dim random As New Random()
        Dim otp As String = random.Next(100000, 999999).ToString()
        Return otp
    End Function
    Public Sub SendOTPEmail(emails As String, otp As String)
        Dim mail As New MailMessage()
        Dim smtpServer As New SmtpClient("smtp.gmail.com")

        mail.From = New MailAddress("xmontemorjerald@gmail.com")
        mail.To.Add(emails)
        mail.Subject = "Your OTP Code"
        mail.Body = "Your OTP is: " & otp

        smtpServer.Port = 587
        smtpServer.Credentials = New Net.NetworkCredential("xmontemorjerald@gmail.com", "wkwp iyfm umur lhse")
        smtpServer.EnableSsl = True

        Try
            smtpServer.Send(mail)
            MessageBox.Show("OTP sent successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
        Catch ex As Exception
            MessageBox.Show("Error sending OTP: " & ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)

        Dim back As MessageBoxResult = MessageBox.Show("Are you sure you want to go back to Login?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question)

        If back = MessageBoxResult.Yes Then

            Dim login As New LoginView()
            login.Show()

            Me.Close()
        End If
    End Sub

    Private Sub OTP_Click(sender As Object, e As RoutedEventArgs)
        Dim enteredOtp As String = Code.Text
        Dim storedOtp As String = TryCast(Application.Current.Properties("OTP"), String)

        If String.IsNullOrEmpty(storedOtp) Then
            MessageBox.Show("No OTP generated. Please request a new one.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If

        If enteredOtp = storedOtp Then
            MessageBox.Show("OTP verified successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information)
            Dim emails As String = Email.Text
            Dim change As New ChangePassword(emails)
            change.Show()
            Me.Close()
            Email.Text = ""

            Clear()

        Else
            MessageBox.Show("Invalid OTP. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            Code.Text = ""
        End If
    End Sub

    Private Sub Clear()

        Email.Text = ""
        Code.Text = ""
    End Sub

    Private Sub Email_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        Dim currentText As String = CType(sender, TextBox).Text
        If e.Key = Key.Space Then
            e.Handled = True
        End If
        ' Check if the key pressed is a dot (".") or "@" key
        If e.Key = Key.OemPeriod Then ' Dot (.)
            ' Allow only one dot (".")
            If currentText.Contains(".") Then
                e.Handled = True ' Block additional dot
            End If
        ElseIf e.Key = Key.D2 AndAlso Keyboard.Modifiers = ModifierKeys.Shift Then ' "@" symbol (Shift + 2)
            ' Allow only one "@"
            If currentText.Contains("@") Then
                e.Handled = True ' Block additional "@"
            End If
        ElseIf Char.IsWhiteSpace(CChar(e.Key.ToString())) OrElse
               (e.Key >= Key.D0 AndAlso e.Key <= Key.D9) OrElse
               (e.Key >= Key.NumPad0 AndAlso e.Key <= Key.NumPad9) Then
            ' Block whitespace and numbers
            e.Handled = True
        End If
    End Sub

    Private Sub Code_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        If Not Char.IsDigit(CChar(e.Text)) OrElse Char.IsWhiteSpace(CChar(e.Text)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Email_LostFocus(sender As Object, e As RoutedEventArgs)
        Dim emailPattern As String = "^[a-zA-Z0-9._%+-]+@gmail\.com$"
        Dim emailInput As String = Email.Text.Trim()

        If Not Regex.IsMatch(emailInput, emailPattern) AndAlso Not String.IsNullOrEmpty(emailInput) Then
            MessageBox.Show("Invalid email address format. Email should end with @gmail.com.", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub
End Class

