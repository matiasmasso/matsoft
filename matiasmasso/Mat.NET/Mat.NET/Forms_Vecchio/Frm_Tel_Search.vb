

Public Class Frm_Tel_Search
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet

    Private Enum Modes
        email
        tel
        adr
        nif
        SubContact
    End Enum

    Private Enum Cols
        Guid
        Tel
        Nom
        ContactGuid
    End Enum

    Private Sub ButtonSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSearch.Click
        LoadGrid()
    End Sub

    Private Function CurrentMode() As Modes
        Dim oMode As Modes = Nothing
        If RadioButtonMail.Checked Then
            oMode = Modes.email
        ElseIf RadioButtonTel.Checked Then
            oMode = Modes.tel
        ElseIf RadioButtonAdr.Checked Then
            oMode = Modes.adr
        ElseIf RadioButtonNIF.Checked Then
            oMode = Modes.nif
        ElseIf RadioButtonContacte.Checked Then
            oMode = Modes.SubContact
        End If
        Return oMode
    End Function

    Private Sub LoadGrid()
        Cursor = Cursors.WaitCursor
        Dim Str As String = TextBoxSearch.Text
        Dim SQL As String = ""

        Select Case CurrentMode()
            Case Modes.email
                If Str.IndexOf("mailto:") = 0 Then Str = Str.Replace("mailto:", "")
                SQL = "SELECT EMAIL.Guid, EMAIL.ADR, CLX.CLX, CLX.Guid FROM EMAIL INNER JOIN " _
                & "EMAIL_CLIS ON EMAIL.Guid=EMAIL_CLIS.EmailGuid INNER JOIN " _
                & "CLX ON EMAIL_CLIS.EMP=CLX.EMP AND EMAIL_CLIS.CLI=CLX.CLI " _
                & "WHERE " _
                & "EMAIL.EMP=@EMP AND " _
                & "EMAIL.ADR LIKE @CLAU " _
                & "ORDER BY CLX.CLX"
            Case Modes.tel
                SQL = "SELECT CLITEL.Guid, CLITEL.NUM, CLX.CLX, CLX.Guid FROM CLITEL INNER JOIN " _
                & "CLX ON CLITEL.EMP=CLX.EMP AND CLITEL.CLI=CLX.CLI " _
                & "WHERE " _
                & "CLITEL.EMP=@EMP AND " _
                & "CLITEL.NUM LIKE @CLAU " _
                & "ORDER BY CLITEL.NUM"
            Case Modes.adr
                SQL = "SELECT CLIADR.SrcGuid, CLIADR.ADR, CLX.CLX, CLX.Guid FROM CLIADR INNER JOIN " _
                & "CLX ON CLIADR.EMP=CLX.EMP AND CLIADR.CLI=CLX.CLI " _
                & "WHERE " _
                & "CLIADR.EMP=@EMP AND " _
                & "CLIADR.ADR LIKE @CLAU " _
                & "ORDER BY CLIADR.ADR"
            Case Modes.nif
                SQL = "SELECT CLIGRAL.Guid,CLIGRAL.NIF, CLX.CLX, CLX.Guid FROM CLIGRAL INNER JOIN " _
                & "CLX ON CLIGRAL.EMP=CLX.EMP AND CLIGRAL.CLI=CLX.CLI " _
                & "WHERE " _
                & "CLIGRAL.EMP=@EMP AND " _
                & "CLIGRAL.NIF LIKE @CLAU " _
                & "ORDER BY CLIGRAL.NIF"
            Case Modes.SubContact
                SQL = "SELECT CliGral.Guid, CLICONTACT.CONTACT, CLX.CLX, CLX.Guid FROM CLICONTACT INNER JOIN " _
                & "CLX ON CLICONTACT.EMP=CLX.EMP AND CLICONTACT.CLI=CLX.CLI INNER JOIN " _
                & "CliGral ON CliContact.Emp=CliGral.Emp AND CliContact.Cli=CliGral.Cli " _
                & "WHERE " _
                & "CLICONTACT.EMP=@EMP AND " _
                & "CLICONTACT.CONTACT LIKE @CLAU " _
                & "ORDER BY CLICONTACT.CONTACT"
        End Select

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@CLAU", "%" & Str & "%")
        Dim oTb As DataTable = mDs.Tables(0)
        If oTb.Rows.Count = 0 Then
            DataGridView1.Visible = False
            PictureBoxNotFound.Visible = True
        Else
            DataGridView1.Visible = True
            PictureBoxNotFound.Visible = False

            With DataGridView1
                With .RowTemplate
                    .Height = DataGridView1.Font.Height * 1.3
                End With
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = False
                .RowHeadersVisible = False
                .MultiSelect = False
                .AllowUserToResizeRows = False
                .AllowDrop = False

                With .Columns(Cols.Guid)
                    .Visible = False
                End With
                With .Columns(Cols.Tel)
                    .Width = 150
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                End With
                With .Columns(Cols.Nom)
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DividerWidth = 0
                End With
                With .Columns(Cols.ContactGuid)
                    .Visible = False
                End With
            End With
        End If
        Cursor = Cursors.Default
    End Sub

    Private Function CurrentContact() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.ContactGuid).Value
            oContact = New Contact(oGuid)
        End If
        Return oContact
    End Function

    Private Function CurrentEmail() As Email
        Dim oEmail As Email = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            oEmail = New Email(oGuid)
        End If
        Return oEmail
    End Function

    Private Sub TextBoxSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSearch.TextChanged
        PictureBoxNotFound.Visible = False
    End Sub

    Private Sub TextBoxSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxSearch.KeyPress
        If e.KeyChar = Chr(13) Then
            LoadGrid()
        End If
    End Sub

    Private Sub TextBoxSearch_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxSearch.Validated
        'LoadGrid()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case CurrentMode()
            Case Modes.email
                Dim oFrm As New Frm_Contact_Email(CurrentEmail)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case Modes.tel, Modes.adr, Modes.nif, Modes.SubContact
                Dim oFrm As New Frm_Contact(CurrentContact)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select

    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Tel

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        Select Case DataGridView1.Rows.Count
            Case 0
            Case Is < i
                DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Case Else
                DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        sETcONTEXTMENU()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Select Case CurrentMode()
            Case Modes.email
                Dim oEmail As Email = CurrentEmail
                If oEmail IsNot Nothing Then
                    Dim oMenu_Email As New Menu_Email(oEmail)
                    AddHandler oMenu_Email.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Email.Range)

                    Dim oContact As Contact = CurrentContact()
                    If oContact IsNot Nothing Then
                        oContextMenu.Items.Add("-")
                        Dim oMenuItem As New ToolStripMenuItem("contacte")
                        oContextMenu.Items.Add(oMenuItem)

                        Dim oMenu_Contact As New Menu_Contact(oContact)
                        AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
                        oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
                    End If

                End If
            Case Modes.tel, Modes.adr, Modes.nif, Modes.SubContact
                Dim oContact As Contact = CurrentContact()
                If oContact IsNot Nothing Then
                    Dim oMenu_Contact As New Menu_Contact(oContact)
                    AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Contact.Range)
                End If
        End Select

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

End Class