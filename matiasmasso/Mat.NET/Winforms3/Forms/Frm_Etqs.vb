Public Class Frm_Etqs
    Private mDs As DataSet
    Private mPdf As LegacyHelper.PdfEtq
    Private mAllowEvents As Boolean

    Private mStatus As Status
    Private Enum Status
        AddNew
        Edit
    End Enum

    Private Enum Cols
        Warn
        Ico
        Adr1
        Adr2
        Adr3
        Adr4
    End Enum

    Private Sub CreateDataSource()
        mDs = New DataSet
        Dim oTb As New System.Data.DataTable
        With oTb
            .Columns.Add("WARN", System.Type.GetType("System.String"))
            .Columns.Add("ICO", System.Type.GetType("System.String"))
            .Columns.Add("ADR1", System.Type.GetType("System.String"))
            .Columns.Add("ADR2", System.Type.GetType("System.String"))
            .Columns.Add("ADR3", System.Type.GetType("System.String"))
            .Columns.Add("ADR4", System.Type.GetType("System.String"))
        End With
        mDs.Tables.Add(oTb)

        mPdf = New LegacyHelper.PdfEtq(mDs)
        NumericUpDown1.Maximum = mPdf.NumEtqsPerPagina

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Warn)
                .Width = 16
            End With
            With .Columns(Cols.Adr1)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Adr2)
                .Visible = False
            End With
            With .Columns(Cols.Adr3)
                .Visible = False
            End With
            With .Columns(Cols.Adr4)
                .Visible = False
            End With
        End With

    End Sub

    Private Sub ButtonAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAdd.Click
        Dim oTb As System.Data.DataTable = mDs.Tables(0)
        Dim oRow As DataRow = oTb.NewRow
        oRow("ADR1") = TextBoxAdr1.Text
        oRow("ADR2") = TextBoxAdr2.Text
        oRow("ADR3") = TextBoxAdr3.Text
        oRow("ADR4") = TextBoxAdr4.Text
        Dim BlFits As Boolean = CheckRowFits(oRow("ADR1"), oRow("ADR2"), oRow("ADR3"), oRow("ADR4"))
        oRow("WARN") = IIf(BlFits, "", "1")
        oTb.Rows.Add(oRow)
        Clear()
    End Sub

    Private Sub ButtonUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpdate.Click
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        oRow.Cells("ADR1").Value = TextBoxAdr1.Text
        oRow.Cells("ADR2").Value = TextBoxAdr2.Text
        oRow.Cells("ADR3").Value = TextBoxAdr3.Text
        oRow.Cells("ADR4").Value = TextBoxAdr4.Text
        Dim BlFits As Boolean = CheckRowFits(TextBoxAdr1.Text, TextBoxAdr2.Text, TextBoxAdr3.Text, TextBoxAdr4.Text)
        oRow.Cells("WARN").Value = IIf(BlFits, "", "1")
        Clear()
    End Sub

    Private Sub Clear()
        TextBoxAdr1.Clear()
        TextBoxAdr2.Clear()
        TextBoxAdr3.Clear()
        TextBoxAdr4.Clear()
        ButtonDel.Enabled = False
        ButtonUpdate.Enabled = False
        ButtonAdd.Enabled = False
        ButtonClear.Enabled = False
        mStatus = Status.AddNew
    End Sub

    Private Sub ButtonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        Clear()
    End Sub

    Private Sub Frm_Etqs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CreateDataSource()
        mAllowEvents = True
    End Sub


    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Dim oPdfEtq As New LegacyHelper.PdfEtq(mDs, NumericUpDown1.Value)
        UIHelper.ShowPdf(oPdfEtq.Stream)
    End Sub


    Private Sub ImportarToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportarToolStripButton.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "OBRIR FITXER ADREÇES ETIQUETES"
            .Filter = "fitxers excel (*.xls)|*.xls|All files (*.*)|*.*"
            .FilterIndex = 1
            If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                ImportAdrsFromExcel(.FileName)
            End If
        End With



    End Sub

    Private Sub ImportAdrsFromExcel(ByVal sFilename As String)
        Dim exs As New List(Of Exception)
        Dim oBook = MatHelper.Excel.ClosedXml.Read(exs, sFilename)
        If exs.Count = 0 Then
            Dim oSheet = oBook.Sheets.FirstOrDefault()
            Dim oTb As DataTable = mDs.Tables(0)
            For Each oRow As MatHelper.Excel.Row In oSheet.Rows
                Dim oDataRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oDataRow)
                oDataRow("ADR1") = oRow.Cells(0).Content
                oDataRow("ADR2") = oRow.Cells(1).Content
                oDataRow("ADR3") = oRow.Cells(2).Content
                oDataRow("ADR4") = oRow.Cells(3).Content
                Dim BlFits As Boolean = CheckRowFits(oDataRow("ADR4"), oDataRow("ADR4"), oDataRow("ADR4"), oDataRow("ADR4"))
                oDataRow("WARN") = IIf(BlFits, "", "1")
            Next
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Function CheckRowFits(Optional ByVal sAdr1 As String = "", Optional ByVal sAdr2 As String = "", Optional ByVal sAdr3 As String = "", Optional ByVal sAdr4 As String = "") As Boolean
        Dim BlFits As Boolean = True
        If Not mPdf.Fits(sAdr1) Then BlFits = False
        If Not mPdf.Fits(sAdr2) Then BlFits = False
        If Not mPdf.Fits(sAdr3) Then BlFits = False
        If Not mPdf.Fits(sAdr4) Then BlFits = False
        Return BlFits
    End Function


    Private Sub TextBoxAdr1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBoxAdr1.KeyDown
        If e.KeyCode = Keys.F1 Then
            Dim exs As New List(Of Exception)
            Dim oContact = Finder.FindContact(exs, Current.Session.User, TextBoxAdr1.Text)
            If exs.Count = 0 Then
                If Not oContact Is Nothing Then
                    Dim oAddress As DTOAddress = FEB.Contact.MailAddress(oContact)
                    Dim sNom As String = oContact.Nom
                    Dim sNomCom As String = oContact.NomComercial
                    TextBoxAdr1.Text = sNom
                    If sNom <> sNomCom And sNomCom > "" Then
                        TextBoxAdr2.Text = sNomCom
                        TextBoxAdr3.Text = oAddress.Text
                        TextBoxAdr4.Text = DTOZip.FullNom(oAddress.Zip)
                    Else
                        TextBoxAdr2.Text = oAddress.Text
                        TextBoxAdr4.Text = DTOZip.FullNom(oAddress.Zip)
                    End If
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub TextBoxAdr1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxAdr1.TextChanged
        Select Case mStatus
            Case Status.AddNew
                ButtonAdd.Enabled = True
            Case Status.Edit
                ButtonUpdate.Enabled = True
        End Select
        ButtonClear.Enabled = True
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim s As String = oRow.Cells(Cols.Warn).Value
                If s = "1" Then
                    e.Value = My.Resources.warn
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            TextBoxAdr1.Text = oRow.Cells("ADR1").Value
            TextBoxAdr2.Text = oRow.Cells("ADR2").Value
            TextBoxAdr3.Text = oRow.Cells("ADR3").Value
            If Not IsDBNull(oRow.Cells("ADR4").Value) Then
                TextBoxAdr4.Text = oRow.Cells("ADR4").Value
            End If
            mStatus = Status.Edit
            ButtonDel.Enabled = True
            ButtonClear.Enabled = True
        End If

    End Sub
End Class