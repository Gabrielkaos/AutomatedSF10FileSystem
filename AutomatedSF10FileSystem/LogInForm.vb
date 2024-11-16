Imports System.Data.OleDb
Public Class LogInForm

    'creates and opens SF10 Database
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles LogInButton.Click
        Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\ADMINS.mdb")
        Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM loginict WHERE USERNAME = '" & UserBox.Text & "' and [PASSWORD]='" & PassBox.Text & "'", con)

        Dim user As String = ""
        Dim pass As String = ""

        con.Open() 'opens the connection


        Dim sdr As OleDbDataReader = cmd.ExecuteReader() 'creates Reader 

        'we try reading from the database
        'if reading fails we catch the error and shows the reason why
        Try
            sdr.Read()
            user = sdr("USERNAME")
            pass = sdr("PASSWORD")
        Catch ex As Exception
            MessageBox.Show("Log in Unsuccessful", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Return
        End Try


        'now if we have read something
        'we compare the data read from data and the texts from the TextBoxes
        'if match found we can login and show the next form
        'we open the GotoMenuForm and closes this form
        If user = UserBox.Text And pass = PassBox.Text Then
            MessageBox.Show("Logged In", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Me.Hide()
            UserBox.Text = ""
            PassBox.Text = ""
            MainForm.Show()
            'Dim gradeForm As New GradeForm()
            'GradeForm.Show()
            'if not match we show that it failed
        Else
            MessageBox.Show("Log in Unsuccessful", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        con.Close()
    End Sub


    'upon Form Load we initalize the TextBoxes to blank ""
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UserBox.Text = ""
        PassBox.Text = ""
    End Sub

    'upon closing we open the AdminguestForm again 
    Private Sub Form1_Close(sender As Object, e As EventArgs) Handles MyBase.Closed

    End Sub

    'handles the show password checkbox
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles ShowPasswordCheckBox.CheckedChanged
        If ShowPasswordCheckBox.Checked = False Then
            PassBox.PasswordChar = "*"
        Else
            PassBox.PasswordChar = ""
        End If
    End Sub

    'back button goes back to AdminGuestForm too
    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Me.Close()
    End Sub

End Class
