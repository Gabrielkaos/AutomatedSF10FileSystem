Imports System.Data.OleDb
Imports System.Drawing.Imaging

Public Class MainForm

    Dim EditMode As Boolean
    Dim reasonFail As String
    Dim AddMode As Boolean
    Dim selectionChangedByUser As Boolean

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        selectionChangedByUser = False
        RefreshTable()

        TrackComboBox.Items.Add("ICT")
        TrackComboBox.Items.Add("STEM")
        TrackComboBox.Items.Add("ABM")
        TrackComboBox.Items.Add("HUMSS")
        TrackComboBox.Items.Add("GA")

        TrackComboBox.SelectedItem = TrackComboBox.Items(0)

        SexBox.Items.Add("Male")
        SexBox.Items.Add("Female")
        SemBox.Items.Add("1st")
        SemBox.Items.Add("2nd")

        SexBox.SelectedItem = SexBox.Items(0)
        SemBox.SelectedItem = SemBox.Items(0)

        GradeComboBox.Items.Add("11")
        GradeComboBox.Items.Add("12")

        SYComboBox.Items.Add("23-24")
        SYComboBox.Items.Add("22-23")
        SYComboBox.Items.Add("21-22")
        SYComboBox.Items.Add("20-21")
        SYComboBox.Items.Add("19-20")
        SYComboBox.Items.Add("18-19")
        SYComboBox.Items.Add("17-18")

        SYSearchComboBox.Items.Add("None")
        SYSearchComboBox.Items.Add("23-24")
        SYSearchComboBox.Items.Add("22-23")
        SYSearchComboBox.Items.Add("21-22")
        SYSearchComboBox.Items.Add("20-21")
        SYSearchComboBox.Items.Add("19-20")
        SYSearchComboBox.Items.Add("18-19")
        SYSearchComboBox.Items.Add("17-18")

        SYSearchComboBox.SelectedItem = SYSearchComboBox.Items(0)

        GradeComboBox.SelectedItem = GradeComboBox.Items(0)
        SYComboBox.SelectedItem = SYComboBox.Items(0)


        DisableAll()
        EditMode = False
        EditButton.Enabled = False
        DeleteButton.Enabled = False
        SaveButton.Hide()
        reasonFail = ""
        AddMode = False
        SearchButton.Focus()
    End Sub

    Sub SearchFailedPrecautions()
        DeleteButton.Enabled = False
        EditButton.Enabled = False
        SaveButton.Hide()
        EditButton.BackColor = Color.White
    End Sub

    Sub SearchSucceededPrecautions()
        DeleteButton.Enabled = True
        EditButton.Enabled = True
        SaveButton.Hide()
    End Sub

    Private Sub RefreshTable()
        Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb")
        con.Open()
        Dim ds As New DataSet()
        Dim dt = New OleDbDataAdapter("SELECT * FROM Table1", con)
        dt.Fill(ds)
        SF10DataGrid.DataSource = ds.Tables(0)
        con.Close()
    End Sub

    Private Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
        If SearchTextBox.Text = "" And LastNameSearchText.Text = "" And FirstNameSearchText.Text = "" And SYSearchComboBox.SelectedItem.ToString() = "None" Then
            MessageBox.Show("Search Failed No Input", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SearchFailedPrecautions()
            Return
        End If

        Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb")
        con.Open()
        Dim ds As New DataSet()
        Dim dt = New OleDbDataAdapter("SELECT * FROM Table1 WHERE LRN = '" + SearchTextBox.Text + "'", con)
        Dim dt1 = New OleDbDataAdapter("SELECT * FROM Table1 WHERE FirstName = '" + FirstNameSearchText.Text + "'", con)
        Dim dt2 = New OleDbDataAdapter("SELECT * FROM Table1 WHERE LastName = '" + LastNameSearchText.Text + "'", con)
        Dim dt3 = New OleDbDataAdapter("SELECT * FROM Table1 WHERE SchoolYear = '" + SYSearchComboBox.SelectedItem.ToString() + "'", con)
        dt.Fill(ds)
        dt1.Fill(ds)
        dt2.Fill(ds)
        dt3.Fill(ds)
        SF10DataGrid.DataSource = ds.Tables(0)
        RemoveDuplicate()
        con.Close()
    End Sub

    Private Sub ClearAllButton_Click(sender As Object, e As EventArgs) Handles ClearAllButton.Click
        PushingClearAllButton()
    End Sub

    Function FindLRNOfSelectedData() As String
        Dim rowIndex As Integer = SF10DataGrid.CurrentCell.RowIndex
        Dim columnIndex As Integer = -1

        For Each column As DataGridViewColumn In SF10DataGrid.Columns
            If column.HeaderText = "LRN" Then
                columnIndex = column.Index
                Exit For
            End If
        Next

        If columnIndex >= 0 Then
            Dim cellValue As String = SF10DataGrid.Rows(rowIndex).Cells(columnIndex).Value.ToString()
            Return cellValue
        End If
        Return "None"
    End Function

    Sub PushingClearAllButton()
        SearchTextBox.Text = ""
        FirstNameSearchText.Text = ""
        LastNameSearchText.Text = ""
        SYSearchComboBox.SelectedItem = SYSearchComboBox.Items(0)
        EditButton.Enabled = False
        EditButton.BackColor = Color.White
        SaveButton.Hide()
        DeleteButton.Enabled = False
        DisableAll()
        ClearAll()
        RefreshTable()
    End Sub

    Public Sub EnableAll()

        Prog_1.Enabled = True
        Prog_2.Enabled = True

        PE_1.Enabled = True
        PE_2.Enabled = True

        EmpTech_1.Enabled = True
        EmpTech_2.Enabled = True

        SchoolBox.Enabled = True
        SchoolIDBox.Enabled = True
        GradeComboBox.Enabled = True
        SYComboBox.Enabled = True

        TrackComboBox.Enabled = True
        SectionBox.Enabled = True
        RemarksBox.Enabled = True

        SemBox.Enabled = True

        LastNameBox.Enabled = True
        FirstNameBox.Enabled = True
        MiddleNameBox.Enabled = True
        LRNBox.Enabled = True
        SexBox.Enabled = True

        HSCOMPLETERBOX.Enabled = True

        JHSBox.Enabled = True

        PEPTPASSER.Enabled = True

        ALSBox.Enabled = True

        GenAveBox.Enabled = True
        GenAveJHSBox.Enabled = True

        SchoolNameBox.Enabled = True
        SchoolAddressBox.Enabled = True

        PEPTRating.Enabled = True
        ALSRatingBox.Enabled = True
        OTHERSSpecifyBox.Enabled = True
    End Sub

    Public Sub DisableAll()

        Prog_1.Enabled = False
        Prog_2.Enabled = False

        PE_1.Enabled = False
        PE_2.Enabled = False

        EmpTech_1.Enabled = False
        EmpTech_2.Enabled = False

        SchoolBox.Enabled = False
        SchoolIDBox.Enabled = False
        GradeComboBox.Enabled = False
        SYComboBox.Enabled = False

        TrackComboBox.Enabled = False
        SectionBox.Enabled = False
        RemarksBox.Enabled = False

        SemBox.Enabled = False

        LastNameBox.Enabled = False
        FirstNameBox.Enabled = False
        MiddleNameBox.Enabled = False
        LRNBox.Enabled = False
        SexBox.Enabled = False

        HSCOMPLETERBOX.Enabled = False

        JHSBox.Enabled = False

        PEPTPASSER.Enabled = False

        ALSBox.Enabled = False

        GenAveBox.Enabled = False
        GenAveJHSBox.Enabled = False

        SchoolNameBox.Enabled = False
        SchoolAddressBox.Enabled = False

        PEPTRating.Enabled = False
        ALSRatingBox.Enabled = False
        OTHERSSpecifyBox.Enabled = False
    End Sub

    Public Sub ClearAll()

        Prog_1.Text = ""
        Prog_2.Text = ""

        PE_1.Text = ""
        PE_2.Text = ""

        EmpTech_1.Text = ""
        EmpTech_2.Text = ""

        Prog_Final.Text = ""
        PE_Final.Text = ""
        EmpTech_Final.Text = ""

        Prog_Action.Text = ""
        PE_Action.Text = ""
        EmpTech_Action.Text = ""

        Final_Grade.Text = ""
        Final_Action.Text = ""

        SchoolBox.Text = ""
        SchoolIDBox.Text = ""
        GradeComboBox.SelectedItem = GradeComboBox.Items(0)
        SYComboBox.SelectedItem = SYComboBox.Items(0)

        TrackComboBox.SelectedItem = TrackComboBox.Items(0)
        SectionBox.Text = ""
        RemarksBox.Text = ""

        SemBox.SelectedItem = SemBox.Items(0)

        LastNameBox.Text = ""
        FirstNameBox.Text = ""
        MiddleNameBox.Text = ""
        LRNBox.Text = ""
        SexBox.SelectedItem = SexBox.Items(0)

        HSCOMPLETERBOX.Checked = False

        JHSBox.Checked = False

        PEPTPASSER.Checked = False

        ALSBox.Checked = False

        GenAveBox.Text = ""
        GenAveJHSBox.Text = ""

        SchoolNameBox.Text = ""
        SchoolAddressBox.Text = ""

        PEPTRating.Text = ""
        ALSRatingBox.Text = ""
        OTHERSSpecifyBox.Text = ""
    End Sub

    Private Sub EditButton_Click(sender As Object, e As EventArgs) Handles EditButton.Click
        EditMode = EditMode Xor True

        If EditMode = True Then
            AddButton.Enabled = False
            selectionChangedByUser = False
            SaveButton.Text = "Save Edited Form"
            SaveButton.Show()
            PrintButton.Hide()

            EditButton.BackColor = Color.Green
            DeleteButton.Enabled = False
            EnableAll()
            MessageBox.Show("Edit Mode Enabled", "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            AddButton.Enabled = True
            selectionChangedByUser = True
            SaveButton.Hide()
            PrintButton.Show()
            DisableAll()
            DeleteButton.Enabled = True
            EditButton.BackColor = Color.White
        End If
    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        Dim conn As New OleDbConnection
        Dim cmd As OleDbCommand
        Dim conn1 As New OleDbConnection
        Dim cmd1 As OleDbCommand

        Dim LRNSelected = FindLRNOfSelectedData()

        Try
            conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb"
            conn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10GradeDatabase.mdb"

            conn1.Open()
            conn.Open()

            cmd1 = conn1.CreateCommand()
            cmd = conn.CreateCommand()

            cmd1.CommandType = CommandType.Text
            cmd.CommandType = CommandType.Text

            cmd.CommandText = "DELETE * FROM Table1 WHERE LRN = '" + LRNSelected + "'"
            cmd1.CommandText = "DELETE * FROM Table1 WHERE LRN = '" + LRNSelected + "'"

            cmd1.ExecuteNonQuery()
            cmd.ExecuteNonQuery()

            conn.Close()
            conn1.Close()
            PushingClearAllButton()
            MessageBox.Show("Record Deleted", "Delete from Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Delete from Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            conn.Close()
            conn1.Close()
        End Try
    End Sub

    Function UpdateEligibility() As Boolean
        If Not isComplete() Then
            MessageBox.Show("Record Update Failed " & vbCrLf & reasonFail & vbCrLf & "Please make sure you fill necessary fields correctly", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Dim con As New OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb"
            con.Open()
            Dim cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text

            cmd.CommandText = "UPDATE Table1 SET HIGHSCHOOLCOMPLETER = '" + HSCOMPLETERBOX.Checked.ToString() + "', GENERALAVERAGE = '" + GenAveBox.Text + "', JUNIORHIGHCOMPLETER = '" + JHSBox.Checked.ToString() + "', GENERALAVERAGEJUNIOR = '" + GenAveJHSBox.Text + "', DATEOFGRADUATION = '" + DateGradBox.Text + "', SCHOOLNAME = '" + SchoolNameBox.Text + "', SCHOOLADDRESS = '" + SchoolAddressBox.Text + "', PEPTPASSER = '" + PEPTPASSER.Checked.ToString() + "', PEPTRATING = '" + PEPTRating.Text + "', ALSPASSER = '" + ALSBox.Checked.ToString() + "', ALSRATING = '" + ALSRatingBox.Text + "', OTHERSPLSSPECIFY = '" + OTHERSSpecifyBox.Text + "' WHERE LRN = '" + FindLRNOfSelectedData() + "'"
            cmd.ExecuteNonQuery()

            con.Close()
            MessageBox.Show("Eligibility Form Successfully Updated", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Return False
        End Try
    End Function

    Private Function isComplete() As Boolean
        If LRNBox.Text = "" Then
            reasonFail = "LRN in Learner's Information is EMPTY"
            Return False
        End If
        If SchoolBox.Text = "" Then
            reasonFail = "School in Schoolastic Record is EMPTY"
            Return False
        End If
        If SchoolIDBox.Text = "" Then
            reasonFail = "School ID in Schoolastic Record is EMPTY"
            Return False
        End If

        If SectionBox.Text = "" Then
            reasonFail = "Section in Schoolastic Record is EMPTY"
            Return False
        End If
        If RemarksBox.Text = "" Then
            reasonFail = "Remarks in Schoolastic Record is EMPTY"
            Return False
        End If
        Try
            If Not SemBox.SelectedItem.ToString() = "1st" And Not SemBox.SelectedItem.ToString() = "2nd" Then
                reasonFail = "Semester in Schoolastic Record is Invalid"
                Return False
            End If
            If Not SexBox.SelectedItem.ToString() = "Male" And Not SexBox.SelectedItem.ToString() = "Female" Then
                reasonFail = "Sex in Learner's Information is Invalid"
                Return False
            End If
        Catch ex As NullReferenceException
            reasonFail = "Please make sure to select the proper value for Sex and Sem"
            Return False
        End Try

        If LastNameBox.Text = "" Then
            reasonFail = "Last Name in Learner's Information is EMPTY"
            Return False
        End If
        If FirstNameBox.Text = "" Then
            reasonFail = "First Name in Learner's Information is EMPTY"
            Return False
        End If
        If MiddleNameBox.Text = "" Then
            reasonFail = "Middle Name in Learner's Information is EMPTY"
            Return False
        End If

        Dim containsLetter As Boolean = False
        For Each c As Char In LRNBox.Text
            If Char.IsLetter(c) Then
                containsLetter = True
                Exit For
            End If
        Next
        If containsLetter Then
            reasonFail = "LRN in Learner's Information contains non numeric value"
            Return False
        End If

        If PE_1.Text = "" Or PE_2.Text = "" Or Prog_1.Text = "" Or Prog_2.Text = "" Or EmpTech_1.Text = "" Or EmpTech_2.Text = "" Then
            reasonFail = "Grade is Incomplete"
            Return False
        End If

        Return True
    End Function

    Function UpdateScholasticRecord() As Boolean
        If Not isComplete() Then
            MessageBox.Show("Record Update Failed " & vbCrLf & reasonFail & vbCrLf & "Please make sure you fill necessary fields correctly", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Dim con As New OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb"
            con.Open()
            Dim cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text

            cmd.CommandText = "UPDATE Table1 SET SCHOOL = '" + SchoolBox.Text + "', SCHOOLID = '" + SchoolIDBox.Text + "', GRADELEVEL = '" + GradeComboBox.SelectedItem.ToString() + "', SCHOOLYEAR = '" + SYComboBox.SelectedItem.ToString() + "', SEM = '" + SemBox.SelectedItem.ToString() + "', TRACKSTRAND = '" + TrackComboBox.SelectedItem.ToString() + "', SCHOOLSECTION = '" + SectionBox.Text + "', REMARKS = '" + RemarksBox.Text + "' WHERE LRN = '" + FindLRNOfSelectedData() + "'"
            cmd.ExecuteNonQuery()

            con.Close()
            MessageBox.Show("Scholastic Record Successfully Updated", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Return False
        End Try
    End Function

    Function UpdateLearnerInfo() As Boolean
        If Not isComplete() Then
            MessageBox.Show("Record Update Failed " & vbCrLf & reasonFail & vbCrLf & "Please make sure you fill necessary fields correctly", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Dim con As New OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb"
            con.Open()
            Dim cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text

            cmd.CommandText = "UPDATE Table1 SET LASTNAME = '" + LastNameBox.Text + "', FIRSTNAME = '" + FirstNameBox.Text + "', MIDDLENAME = '" + MiddleNameBox.Text + "', LRN = '" + LRNBox.Text + "', DATEOFBIRTH = '" + DateOfBirthBox.Text + "', SEX = '" + SexBox.SelectedItem.ToString() + "' WHERE LRN = '" + LRNBox.Text + "'"
            cmd.ExecuteNonQuery()

            con.Close()
            MessageBox.Show("Learner's Info Successfully Updated", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Return False
        End Try
    End Function

    Function UpdateGrades() As Boolean
        If Not isComplete() Then
            MessageBox.Show("Record Update Failed " & vbCrLf & reasonFail & vbCrLf & "Please make sure you fill necessary fields correctly", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Dim con As New OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10GradeDatabase.mdb"
            con.Open()
            Dim cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text

            cmd.CommandText = "UPDATE Table1 SET PE_1 = '" + PE_1.Text + "', PE_2 = '" + PE_2.Text + "', EmpTech_1 = '" + EmpTech_1.Text + "', EmpTech_2 = '" + EmpTech_2.Text + "', Prog_1 = '" + Prog_1.Text + "', Prog_2 = '" + Prog_2.Text + "' WHERE LRN = '" + LRNBox.Text + "'"
            cmd.ExecuteNonQuery()

            con.Close()
            MessageBox.Show("Learner's Grade Successfully Updated", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
            con.Close()
            Return False
        End Try
    End Function

    Private Sub SaveButton_Click(sender As Object, e As EventArgs) Handles SaveButton.Click

        If EditMode = True And AddMode = False Then
            If UpdateScholasticRecord() = False Then
                MessageBox.Show("Scholastic Record Update Failed", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If UpdateLearnerInfo() = False Then
                MessageBox.Show("Learner's Info Record Update Failed", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If UpdateEligibility() = False Then
                MessageBox.Show("Eligibility Record Update Failed", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If UpdateGrades() = False Then
                MessageBox.Show("Grades Record Update Failed", "Update to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            PushingClearAllButton()
            AddButton.Enabled = True
            selectionChangedByUser = True
            Return
        End If

        If EditMode = False And AddMode = True Then
            If Not isComplete() Then
                MessageBox.Show("Record Save Failed " & vbCrLf & reasonFail & vbCrLf & "Please make sure you fill necessary fields correctly", "Save to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim conn1 As New OleDbConnection
            Dim cmd1 As OleDbCommand
            Dim conn As New OleDbConnection
            Dim cmd As OleDbCommand

            Try
                conn1.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10GradeDatabase.mdb"
                conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb"

                conn.Open()
                cmd = conn.CreateCommand()
                cmd.CommandType = CommandType.Text

                conn1.Open()
                cmd1 = conn1.CreateCommand()
                cmd1.CommandType = CommandType.Text

                cmd1.CommandText = "INSERT into Table1(LRN,PE_1,PE_2,Prog_1,Prog_2,EmpTech_1,EmpTech_2)values('" + LRNBox.Text + "','" + PE_1.Text + "','" + PE_2.Text + "','" + Prog_1.Text + "','" + Prog_2.Text + "','" + EmpTech_1.Text + "','" + EmpTech_2.Text + "')"
                cmd1.ExecuteNonQuery()

                cmd.CommandText = "INSERT into Table1(LRN,LASTNAME,FIRSTNAME,MIDDLENAME,SEX,DATEOFBIRTH,HIGHSCHOOLCOMPLETER,GENERALAVERAGE,JUNIORHIGHCOMPLETER,GENERALAVERAGEJUNIOR,DATEOFGRADUATION,SCHOOLNAME,SCHOOLADDRESS,PEPTPASSER,PEPTRATING,ALSPASSER,ALSRATING,OTHERSPLSSPECIFY,SCHOOL,SCHOOLID,GRADELEVEL,SCHOOLYEAR,SEM,TRACKSTRAND,SCHOOLSECTION,REMARKS)values('" + LRNBox.Text + "','" + LastNameBox.Text + "','" + FirstNameBox.Text + "','" +
                    MiddleNameBox.Text + "','" + SexBox.SelectedItem.ToString() + "','" + DateOfBirthBox.Text + "','" + HSCOMPLETERBOX.Checked.ToString() + "','" + GenAveBox.Text + "','" + JHSBox.Checked.ToString() + "','" + GenAveJHSBox.Text + "','" + DateGradBox.Text + "','" + SchoolNameBox.Text + "','" + SchoolAddressBox.Text + "','" +
                    PEPTPASSER.Checked.ToString() + "','" + PEPTRating.Text + "','" + ALSBox.Checked.ToString() + "','" + ALSRatingBox.Text + "','" + OTHERSSpecifyBox.Text + "','" + SchoolBox.Text + "','" + SchoolIDBox.Text + "','" + GradeComboBox.SelectedItem.ToString() + "','" + SYComboBox.SelectedItem.ToString() + "','" + SemBox.SelectedItem.ToString() + "','" + TrackComboBox.SelectedItem.ToString() + "','" +
                    SectionBox.Text + "','" + RemarksBox.Text + "')"
                cmd.ExecuteNonQuery()

                conn.Close()
                conn1.Close()
                ClearAll()
                RefreshTable()
                MessageBox.Show("Record Saved", "Save to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Save to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                conn1.Close()
                conn.Close()
            End Try
        End If
        MessageBox.Show("If this shows up, something is f*cked", "Save to Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub SaveButton_MouseEnter(sender As Object, e As EventArgs) Handles SaveButton.MouseEnter
        SaveButton.ForeColor = Color.White
        If isComplete() Then
            SaveButton.BackColor = Color.Green
        Else
            SaveButton.BackColor = Color.Red
        End If
    End Sub

    Private Sub SaveButton_MouseLeave(sender As Object, e As EventArgs) Handles SaveButton.MouseLeave
        SaveButton.ForeColor = Color.Black
        SaveButton.BackColor = Color.White
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        AddMode = AddMode Xor True

        If AddMode = True Then
            selectionChangedByUser = False
            AddButton.BackColor = Color.Green
            SaveButton.Text = "Save Newly Created Form"
            EnableAll()
            ClearAll()
            DeleteButton.Hide()
            EditButton.Hide()
            SaveButton.Show()

            SearchTextBox.Hide()
            LastNameSearchText.Hide()
            FirstNameSearchText.Hide()
            SYSearchComboBox.Hide()
            Label13.Hide()
            Label33.Hide()
            Label34.Hide()
            Label35.Hide()
            PrintButton.Hide()
            SearchButton.Hide()
            ClearAllButton.Hide()

            MessageBox.Show("Create Mode Enabled", "Create Mode", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            selectionChangedByUser = True
            AddButton.BackColor = Color.White
            DisableAll()
            ClearAll()
            SaveButton.Text = "Save"
            DeleteButton.Show()
            EditButton.Show()
            SaveButton.Hide()

            Label33.Show()
            Label34.Show()
            Label35.Show()
            PrintButton.Show()

            SearchButton.Show()
            SearchTextBox.Show()
            LastNameSearchText.Show()
            FirstNameSearchText.Show()
            SYSearchComboBox.Show()
            Label13.Show()
            ClearAllButton.Show()

            MessageBox.Show("Create Mode Disabled", "Create Mode", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub HelpButton_Click(sender As Object, e As EventArgs) Handles HelpButton.Click
        MessageBox.Show("Search LRN Button - Just type the LRN in the textbox and press this button" & vbTab & vbCrLf & vbCrLf &
            "Clear All Button - Clears everything in the textboxes" & vbTab & vbCrLf & vbCrLf &
            "Create New Form Button - For creating new SF10 Records" & vbTab & vbCrLf & vbCrLf &
            "Delete Button - Only gets enabled if the Search Button is pressed and finds a record successfully. If pressed, deletes the current record searched" & vbTab & vbCrLf & vbCrLf &
            "Edit Button - Only gets enabled if the Search Button is pressed and finds a record successfully. If pressed goes to Edit Mode" & vbTab & vbCrLf & vbCrLf, "Helper Mode", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub QuitButton_Click(sender As Object, e As EventArgs) Handles QuitButton.Click
        Me.Close()
        LogInForm.Close()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles SF10DataGrid.SelectionChanged
        If selectionChangedByUser = True Then
            ClearAll()
            Dim rowIndex As Integer = SF10DataGrid.CurrentCell.RowIndex
            Dim columnIndex As Integer = -1

            For Each column As DataGridViewColumn In SF10DataGrid.Columns
                If column.HeaderText = "LRN" Then
                    columnIndex = column.Index
                    Exit For
                End If
            Next

            If columnIndex >= 0 Then
                Dim cellValue As String = SF10DataGrid.Rows(rowIndex).Cells(columnIndex).Value.ToString()


                Dim con As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10DATABASE.mdb")
                Dim commandText As String = "SELECT * FROM Table1 WHERE LRN = '" + cellValue + "'"

                Dim cmd As OleDbCommand = New OleDbCommand(commandText, con)
                con.Open()

                Dim sdr As OleDbDataReader = cmd.ExecuteReader()

                Try
                    If sdr.Read() Then
                        'SearchSucceededPrecautions()
                        SchoolBox.Text = sdr("SCHOOL").ToString()
                        SchoolIDBox.Text = sdr("SCHOOLID").ToString()
                        Try
                            GradeComboBox.SelectedItem = GradeComboBox.Items(GradeComboBox.Items.IndexOf(sdr("GRADELEVEL").ToString()))
                        Catch ex As Exception

                        End Try
                        Try
                            SYComboBox.SelectedItem = SYComboBox.Items(SYComboBox.Items.IndexOf(sdr("SCHOOLYEAR").ToString()))
                        Catch ex As Exception

                        End Try

                        Try
                            TrackComboBox.SelectedItem = TrackComboBox.Items(TrackComboBox.Items.IndexOf(sdr("TRACKSTRAND").ToString()))
                        Catch ex As Exception

                        End Try
                        SectionBox.Text = sdr("SCHOOLSECTION").ToString()
                        RemarksBox.Text = sdr("REMARKS").ToString()

                        Try
                            SemBox.SelectedItem = SemBox.Items(SemBox.Items.IndexOf(sdr("SEM").ToString()))
                        Catch ex As Exception

                        End Try

                        LastNameBox.Text = sdr("LASTNAME")
                        FirstNameBox.Text = sdr("FIRSTNAME")
                        MiddleNameBox.Text = sdr("MIDDLENAME")
                        LRNBox.Text = cellValue
                        DateOfBirthBox.Value = sdr("DATEOFBIRTH")
                        SexBox.SelectedItem = SexBox.Items(SexBox.Items.IndexOf(sdr("SEX")))

                        If sdr("HIGHSCHOOLCOMPLETER").ToString() = "True" Then
                            HSCOMPLETERBOX.Checked = True
                        Else
                            HSCOMPLETERBOX.Checked = False
                        End If

                        If sdr("JUNIORHIGHCOMPLETER").ToString() = "True" Then
                            JHSBox.Checked = True
                        Else
                            JHSBox.Checked = False
                        End If

                        If sdr("PEPTPASSER").ToString() = "True" Then
                            PEPTPASSER.Checked = True
                        Else
                            PEPTPASSER.Checked = False
                        End If

                        If sdr("ALSPASSER").ToString() = "True" Then
                            ALSBox.Checked = True
                        Else
                            ALSBox.Checked = False
                        End If

                        GenAveBox.Text = sdr("GENERALAVERAGE").ToString()
                        GenAveJHSBox.Text = sdr("GENERALAVERAGEJUNIOR").ToString()

                        Try
                            DateGradBox.Value = sdr("DATEOFGRADUATION").ToString()
                        Catch ex As Exception

                        End Try

                        SchoolNameBox.Text = sdr("SCHOOLNAME").ToString()
                        SchoolAddressBox.Text = sdr("SCHOOLADDRESS").ToString()

                        PEPTRating.Text = sdr("PEPTRATING").ToString()
                        ALSRatingBox.Text = sdr("ALSRATING").ToString()
                        OTHERSSpecifyBox.Text = sdr("OTHERSPLSSPECIFY").ToString()
                    End If
                Catch ex As Exception
                    con.Close()
                End Try


                con.Close()

                Dim con1 As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\LENOVO\Desktop\VBProjects\AutomatedSF10FileSystem\AutomatedSF10FileSystem\DATABASES\SF10GradeDatabase.mdb")
                Dim commandText1 As String = "SELECT * FROM Table1 WHERE LRN = '" + cellValue + "'"

                Dim cmd1 As OleDbCommand = New OleDbCommand(commandText1, con1)
                con1.Open()

                Dim sdr1 As OleDbDataReader = cmd1.ExecuteReader()
                Try
                    If sdr1.Read() Then
                        SearchSucceededPrecautions()
                        PE_1.Text = sdr1("PE_1").ToString()
                        PE_2.Text = sdr1("PE_2").ToString()

                        EmpTech_1.Text = sdr1("EmpTech_1").ToString()
                        EmpTech_2.Text = sdr1("EmpTech_2").ToString()

                        Prog_1.Text = sdr1("Prog_1").ToString()
                        Prog_2.Text = sdr1("Prog_2").ToString()
                        CalculateFinalGrades()
                    Else
                        SearchFailedPrecautions()
                    End If
                Catch ex As Exception
                    con1.Close()
                    SearchFailedPrecautions()
                End Try

                con1.Close()
            End If
        End If
    End Sub

    Private Sub CalculateFinalGrades()
        Try
            Prog_Final.Text = Math.Round((Val(Prog_1.Text) + Val(Prog_2.Text)) / 2)
            EmpTech_Final.Text = Math.Round((Val(EmpTech_1.Text) + Val(EmpTech_2.Text)) / 2)
            PE_Final.Text = Math.Round((Val(PE_1.Text) + Val(PE_2.Text)) / 2)

            If Val(Prog_Final.Text) >= 75 Then
                Prog_Action.Text = "PASSED"
            Else
                Prog_Action.Text = "FAILED"
            End If
            If Val(EmpTech_Final.Text) >= 75 Then
                EmpTech_Action.Text = "PASSED"
            Else
                EmpTech_Action.Text = "FAILED"
            End If
            If Val(PE_Final.Text) >= 75 Then
                PE_Action.Text = "PASSED"
            Else
                PE_Action.Text = "FAILED"
            End If

            Final_Grade.Text = Math.Round((Val(Prog_Final.Text) + Val(PE_Final.Text) + Val(EmpTech_Final.Text)) / 3)
            If Val(Final_Grade.Text) >= 75 Then
                Final_Action.Text = "PASSED"
            Else
                Final_Action.Text = "FAILED"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Grade Calculation", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Sub RemoveDuplicate()
        For i As Integer = SF10DataGrid.Rows.Count - 2 To 0 Step -1
            For j As Integer = SF10DataGrid.Rows.Count - 1 To i + 1 Step -1
                Dim row1 As DataGridViewRow = SF10DataGrid.Rows(i)
                Dim row2 As DataGridViewRow = SF10DataGrid.Rows(j)
                Dim isDuplicate As Boolean = True

                For Each cell1 As DataGridViewCell In row1.Cells
                    If SF10DataGrid.Columns(cell1.ColumnIndex).HeaderText = "LRN" Then
                        Dim cell2 As DataGridViewCell = row2.Cells(cell1.ColumnIndex)
                        If Not cell1.Value.Equals(cell2.Value) Then
                            isDuplicate = False
                            Exit For
                        End If
                    End If
                Next

                If isDuplicate Then
                    SF10DataGrid.Rows.RemoveAt(j)
                End If
            Next
        Next

    End Sub

    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles SF10DataGrid.CellMouseDown
        If AddMode = False And EditMode = False Then
            selectionChangedByUser = True
        End If
    End Sub

    Private Sub DataGridView1_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles SF10DataGrid.CellMouseUp
        If AddMode = False And EditMode = False Then
            selectionChangedByUser = False
        End If
    End Sub

    Private Sub RefreshButton_Click(sender As Object, e As EventArgs) Handles RefreshButton.Click
        RefreshTable()
    End Sub

    Private Sub PE_1_TextChanged(sender As Object, e As EventArgs) Handles PE_1.TextChanged
        CalculateFinalGrades()
    End Sub

    Private Sub PE_2_TextChanged(sender As Object, e As EventArgs) Handles PE_2.TextChanged
        CalculateFinalGrades()
    End Sub

    Private Sub EmpTech_1_TextChanged(sender As Object, e As EventArgs) Handles EmpTech_1.TextChanged
        CalculateFinalGrades()
    End Sub

    Private Sub EmpTech_2_TextChanged(sender As Object, e As EventArgs) Handles EmpTech_2.TextChanged
        CalculateFinalGrades()
    End Sub

    Private Sub Prog_1_TextChanged(sender As Object, e As EventArgs) Handles Prog_1.TextChanged
        CalculateFinalGrades()
    End Sub

    Private Sub Prog_2_TextChanged(sender As Object, e As EventArgs) Handles Prog_2.TextChanged
        CalculateFinalGrades()
    End Sub

    Public Sub CaptureScreen()
        Dim screenshot As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Using graphics As Graphics = Graphics.FromImage(screenshot)
            graphics.CopyFromScreen(0, 0, 0, 0, screenshot.Size)
        End Using

        Dim fileName As String = "screenshot.png"
        screenshot.Save(fileName, ImageFormat.Png)

        MessageBox.Show("Screenshot saved")
    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click
        CaptureScreen()
    End Sub

    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Me.Close()
        LogInForm.Show()
    End Sub

End Class
