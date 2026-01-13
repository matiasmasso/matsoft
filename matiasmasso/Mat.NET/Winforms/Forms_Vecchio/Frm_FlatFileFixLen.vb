
Public Class Frm_FlatFileFixLen
    Private mFile As FlatFileFixLen
    Private mAllowEvents As Boolean = False

    Private Enum ColsReg
        Idx
        Ico
        Cod
        Txt
    End Enum

    Private Enum ColsField
        Idx
        Ico
        Nom
        Len
        Value
    End Enum

    Public Sub New(ByVal oFile As FlatFileFixLen)
        MyBase.New()
        Me.InitializeComponent()
        mFile = oFile
        Me.Text = "Fitxers plans: " & mFile.Id.ToString

        LoadRegs()
    End Sub

    Private Sub LoadRegs()
        Dim oTb As New DataTable
        oTb.Columns.Add("IDX", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oTb.Columns.Add("COD", System.Type.GetType("System.String"))
        oTb.Columns.Add("TXT", System.Type.GetType("System.String"))

        Dim oRow As DataRow = Nothing
        Dim idx As Integer
        For Each oReg As FlatFileFixLen_Reg In mFile.registres
            oRow = oTb.NewRow
            oRow(ColsReg.Idx) = idx
            oRow(ColsReg.Ico) = BLL.GetByteArrayFromImg(My.Resources.Ok)
            oRow(ColsReg.Cod) = oReg.id
            oRow(ColsReg.Txt) = oReg.Nom
            oTb.Rows.Add(oRow)
            idx += 1
        Next

        With DataGridViewRegs
            With .RowTemplate
                .Height = DataGridViewRegs.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsReg.Idx)
                .Visible = False
            End With
            With .Columns(ColsReg.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsReg.Cod)
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
            End With
            With .Columns(ColsReg.Txt)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        Cursor = Cursors.Default
        mAllowEvents = True

        If oTb.Rows.Count > 0 Then
            DataGridViewRegs.CurrentCell = DataGridViewRegs.Rows(0).Cells(ColsReg.Txt)
        End If
        LoadFields()
    End Sub

    Private Function CurrentReg() As FlatFileFixLen_Reg
        Dim retVal As FlatFileFixLen_Reg = Nothing
        Dim oRow As DataGridViewRow = DataGridViewRegs.CurrentRow
        If oRow IsNot Nothing Then
            Dim iIdx As Integer = oRow.Cells(ColsReg.Idx).Value
            retVal = mFile.registres(iIdx)
        End If
        Return retVal
    End Function

    Private Sub LoadFields()
        Dim oTb As New DataTable
        oTb.Columns.Add("IDX", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oTb.Columns.Add("NOM", System.Type.GetType("System.String"))
        oTb.Columns.Add("LEN", System.Type.GetType("System.String"))
        oTb.Columns.Add("VAL", System.Type.GetType("System.String"))

        Dim oRow As DataRow = Nothing
        Dim idx As Integer
        Dim oReg As FlatFileFixLen_Reg = CurrentReg()
        If oReg IsNot Nothing Then

            For Each oField As FlatFileFixLen_Field In oReg.Fields
                oRow = oTb.NewRow
                oRow(ColsField.Idx) = idx
                oRow(ColsField.Ico) = BLL.GetByteArrayFromImg(My.Resources.Ok)
                oRow(ColsField.Nom) = oField.Nom
                oRow(ColsField.Len) = oField.len
                oRow(ColsField.Value) = oField.Value
                oTb.Rows.Add(oRow)
                idx += 1
            Next

            With DataGridViewFields
                With .RowTemplate
                    .Height = DataGridViewRegs.Font.Height * 1.3
                    '.DefaultCellStyle.BackColor = Color.Transparent
                End With
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .MultiSelect = False
                .AllowUserToResizeRows = False

                With .Columns(ColsField.Idx)
                    .Visible = False
                End With
                With .Columns(ColsField.Ico)
                    .HeaderText = ""
                    .Width = 16
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .ReadOnly = True
                End With
                With .Columns(ColsField.Nom)
                    .HeaderText = "camp"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 150
                    .ReadOnly = True
                End With
                With .Columns(ColsField.Len)
                    .HeaderText = "len"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 30
                    .ReadOnly = True
                End With
                With .Columns(ColsField.Value)
                    .HeaderText = "valor"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
            End With
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub DataGridViewRegs_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewRegs.SelectionChanged
        If mAllowEvents Then
            LoadFields()
        End If
    End Sub

    Private Function CurrentField() As FlatFileFixLen_Field
        Dim oReg As FlatFileFixLen_Reg = CurrentReg()
        Dim retVal As FlatFileFixLen_Field = Nothing
        Dim oRow As DataGridViewRow = DataGridViewFields.CurrentRow
        If oRow IsNot Nothing Then
            Dim iIdx As Integer = oRow.Cells(ColsField.Idx).Value
            retVal = oReg.Fields(iIdx)
        End If
        Return retVal
    End Function

    Private Sub DataGridViewFields_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewFields.CellValidated
        Dim oRow As DataGridViewRow = DataGridViewFields.Rows(e.RowIndex)
        Dim sValue As String = oRow.Cells(ColsField.Value).Value
        CurrentField.Value = sValue
    End Sub

    Private Sub DataGridViewFields_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridViewFields.CellValidating
        Select Case e.ColumnIndex
            Case ColsField.Value
                Dim oField As FlatFileFixLen_Field = CurrentField()

                Dim iLen As Integer = oField.len ' DataGridViewFields.Rows(e.RowIndex).Cells(ColsField.Len).Value.ToString.Length
                Dim iEnteredValueLen As Integer = e.FormattedValue.ToString.Length
                If iLen <> iEnteredValueLen Then
                    DataGridViewFields.Rows(e.RowIndex).Cells(ColsField.Ico).Value = BLL.GetByteArrayFromImg(My.Resources.warn)
                Else
                    DataGridViewFields.Rows(e.RowIndex).Cells(ColsField.Ico).Value = BLL.GetByteArrayFromImg(My.Resources.Ok)
                    oField.Value = e.FormattedValue
                    ButtonOk.Enabled = True
                End If
        End Select
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oDlg As New SaveFileDialog
        With oDlg
            .FileName = mFile.FileName
            If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                mFile.Save(.FileName)
                Me.Close()
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class