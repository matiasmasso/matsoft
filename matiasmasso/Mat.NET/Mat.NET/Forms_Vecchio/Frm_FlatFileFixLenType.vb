

Public Class Frm_FlatFileFixLenType
    Private mFlatFileFixLen As FlatFileFixLen = Nothing
    Private mAllowEvents As Boolean = False

    Public Event afterUpdate(sender As Object, e As System.EventArgs)

    Private Enum ColsRegs
        Id
        Nom
    End Enum

    Private Enum ColsFields
        Lin
        Nom
        Len
        PosFrom
        PosTo
        Opcional
        OpcDsc
        Format
        FormatDsc
        DefaultValue
        Obs
    End Enum

    Public Sub New(oFlatFileFixLen As FlatFileFixLen)
        MyBase.New()
        Me.InitializeComponent()
        mFlatFileFixLen = oFlatFileFixLen
        LoadIds()
        LoadGral()
        mAllowEvents = True
    End Sub

    Private Sub LoadGral()
        ComboBoxId.SelectedValue = mFlatFileFixLen.Id
        ComboBoxId.Enabled = False

        With mFlatFileFixLen
            TextBoxNom.Text = .Nom
            Xl_TextBoxNumRegIdLen.Value = .RegIdLen
            Me.Text = "diseny fitxer " & .Id.ToString
            Xl_DocFile1.Load(.DocFile)
        End With

        LoadRegs()

    End Sub

    Private Sub LoadIds()
        UIHelper.LoadComboFromEnum(ComboBoxId, GetType(DTO.DTOFlatFile.ids), "(seleccionar Id)")
    End Sub

    Private Sub LoadRegs()
        Dim iFileId As Integer = CurrentFlatFile.Id
        Dim SQL As String = "SELECT RegId,Nom FROM FlatFileFixLen_Regs WHERE FF=@FileId ORDER BY RegId"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@FileId", iFileId.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridViewRegs
            With .RowTemplate
                .Height = DataGridViewRegs.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsRegs.Id)
                .HeaderText = "Id"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsRegs.Nom)
                .HeaderText = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenuRegs()
    End Sub

    Private Sub LoadFields()
        Dim SQL As String = "SELECT Lin,Nom,Len,0 as PosFrom, 0 as PosTo,opcional,'' as opcDsc, format, '' as formatDsc,[Default],Obs " _
        & "FROM FlatFileFixLen_Fields WHERE " _
        & "FF=@FileId AND Reg=@RegId ORDER BY Lin"
        Dim oReg As FlatFileFixLen_Reg = CurrentReg()
        Dim iFlatFileId As Integer = CInt(oReg.File.Id)
        Dim sRegId As String = oReg.id
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@FileId", iFlatFileId, "@RegId", sRegId)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim iPos As Integer = 1
        Dim iLen As Integer
        Dim oOpc As FF.CampsOpcionals
        Dim oFormat As DTO.DTOFlatField.Formats
        For Each oRow As DataRow In oTb.Rows
            iLen = CInt(oRow(ColsFields.Len))
            oOpc = CType(oRow(ColsFields.Opcional), FF.CampsOpcionals)
            oFormat = CType(oRow(ColsFields.Format), DTO.DTOFlatField.Formats)

            oRow(ColsFields.PosFrom) = iPos
            oRow(ColsFields.PosTo) = iPos + iLen - 1
            oRow(ColsFields.OpcDsc) = oOpc.ToString
            oRow(ColsFields.FormatDsc) = oFormat.ToString
            iPos += iLen
        Next

        With DataGridViewFields
            With .RowTemplate
                .Height = DataGridViewFields.Font.Height * 1.3
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsFields.Lin)
                .HeaderText = "Id"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsRegs.Nom)
                .HeaderText = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(ColsFields.Len)
                .HeaderText = "len"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsFields.PosFrom)
                .HeaderText = "from"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsFields.PosTo)
                .HeaderText = "to"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsFields.Opcional)
                .Visible = False
            End With

            With .Columns(ColsFields.OpcDsc)
                .HeaderText = "opc"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsFields.Format)
                .Visible = False
            End With

            With .Columns(ColsFields.FormatDsc)
                .HeaderText = "format"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsFields.DefaultValue)
                .HeaderText = "default"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsFields.Obs)
                .HeaderText = "obs"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With
        SetContextMenuFields()
    End Sub

    Private Function CurrentField() As FlatFileFixLen_Field
        Dim retval As FlatFileFixLen_Field = Nothing
        Dim oRow As DataGridViewRow = DataGridViewFields.CurrentRow
        If oRow IsNot Nothing Then
            Dim iFieldId As String = CInt(oRow.Cells(ColsFields.Lin).Value)
            Dim oReg As FlatFileFixLen_Reg = CurrentReg()
            retval = New FlatFileFixLen_Field(oReg, iFieldId)
        End If
        Return retval
    End Function


    Private Function CurrentReg() As FlatFileFixLen_Reg
        Dim retval As FlatFileFixLen_Reg = Nothing
        Dim oRow As DataGridViewRow = DataGridViewRegs.CurrentRow
        If oRow IsNot Nothing Then
            Dim sRegId As String = oRow.Cells(ColsRegs.Id).Value
            Dim oFlatFile As FlatFileFixLen = CurrentFlatFile()
            retval = New FlatFileFixLen_Reg(oFlatFile, sRegId)
        End If
        Return retval
    End Function

    Private Function CurrentFlatFile() As FlatFileFixLen
        Dim retval As New FlatFileFixLen(CurrentId)
        Return retval
    End Function

    Private Function CurrentId() As DTO.DTOFlatFile.ids
        Dim retval As Integer = ComboBoxId.SelectedValue
        Return retval
    End Function

    Private Sub ControlChanged(sender As Object, e As System.EventArgs) Handles _
         TextBoxNom.TextChanged, _
          Xl_TextBoxNumRegIdLen.AfterUpdate

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        If mFlatFileFixLen Is Nothing Then
            mFlatFileFixLen = New FlatFileFixLen(CurrentId)
        End If

        With mFlatFileFixLen
            .Nom = TextBoxNom.Text
            .RegIdLen = Xl_TextBoxNumRegIdLen.Value
            .DocFile = Xl_DocFile1.Value
        End With

        Dim exs as New List(Of exception)
        If FlatFileFixLenLoader.Update(mFlatFileFixLen, exs) Then
            RaiseEvent afterUpdate(mFlatFileFixLen, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar el fitxer")
        End If
    End Sub


    Private Sub SetContextMenuRegs()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As FlatFileFixLen_Reg = CurrentReg()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf Do_ZoomReg)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("afegir nou camp", Nothing, AddressOf Do_AddNewField)
            oContextMenuStrip.Items.Add(oMenuItem)

            'oMenuItem = New ToolStripMenuItem("eliminar", Nothing, AddressOf Delete)
            oContextMenuStrip.Items.Add("-")
        End If

        oMenuItem = New ToolStripMenuItem("afegir...", Nothing, AddressOf Do_AddNewReg)
        oContextMenuStrip.Items.Add(oMenuItem)

        DataGridViewRegs.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub SetContextMenuFields()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As FlatFileFixLen_Field = CurrentField()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", Nothing, AddressOf Do_ZoomField)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("afegir clon...", Nothing, AddressOf Do_AddNewClonField)
            oContextMenuStrip.Items.Add(oMenuItem)

            'oMenuItem = New ToolStripMenuItem("eliminar", Nothing, AddressOf Delete)
            'oContextMenuStrip.Items.Add(oMenuItem)
        End If

        oMenuItem = New ToolStripMenuItem("afegir...", Nothing, AddressOf Do_AddNewField)
        oContextMenuStrip.Items.Add(oMenuItem)

        DataGridViewFields.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub Do_ZoomField()
        Dim oField As FlatFileFixLen_Field = CurrentField()
        Dim oFrm As New Frm_FlatFileFixLenField(oField)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestFields
        oFrm.Show()
    End Sub

    Private Sub Do_AddNewClonField()
        Dim oField As FlatFileFixLen_Field = CurrentField()
        Dim oClon As FlatFileFixLen_Field = oField.Clon
        Dim oFrm As New Frm_FlatFileFixLenField(oClon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestFields
        oFrm.Show()
    End Sub

    Private Sub Do_AddNewField()
        Dim oReg As FlatFileFixLen_Reg = CurrentReg()
        Dim oField As New FlatFileFixLen_Field(CurrentReg, oReg.Fields.Count + 1)
        Dim oFrm As New Frm_FlatFileFixLenField(oField)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestFields
        oFrm.Show()
    End Sub

    Private Sub Do_ZoomReg()
        Dim oReg As FlatFileFixLen_Reg = CurrentReg()
        Dim oFrm As New Frm_FlatFileFixLenReg(oReg)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestRegs
        oFrm.Show()
    End Sub

    Private Sub Do_AddNewReg()
        Dim iNextRegId As Integer = CurrentFlatFile.registres.Count + 1
        Dim oReg As New FlatFileFixLen_Reg(CurrentFlatFile, iNextRegId)
        Dim oFrm As New Frm_FlatFileFixLenReg(oReg)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestRegs
        oFrm.Show()
    End Sub


    Private Sub RefreshRequestRegs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsRegs.Nom
        Dim oGrid As DataGridView = DataGridViewRegs

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadRegs()

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub RefreshRequestFields(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsFields.Nom
        Dim oGrid As DataGridView = DataGridViewFields

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadFields()

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub ComboBoxId_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ComboBoxId.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            mFlatFileFixLen = New FlatFileFixLen(CurrentId)
            LoadGral()
            mAllowEvents = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridViewRegs_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridViewRegs.DoubleClick
        Do_ZoomReg()
    End Sub

    Private Sub DataGridViewRegs_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridViewRegs.SelectionChanged
        If mAllowEvents Then
            SetContextMenuRegs()
            loadFields()
        End If
    End Sub

    Private Sub DataGridViewFields_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridViewFields.DoubleClick
        Do_ZoomField()
    End Sub

    Private Sub Xl_DocFile1_AfterFileDropped(sender As Object, oArgs As MatEventArgs) Handles Xl_DocFile1.AfterFileDropped
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class