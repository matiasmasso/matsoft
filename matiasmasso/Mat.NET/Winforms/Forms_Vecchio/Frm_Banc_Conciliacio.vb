

Public Class Frm_Banc_Conciliacio
    Private mConciliacio As BancConciliacio
    Private mDirtyBigFile As maxisrvr.BigFileNew = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        Cca
        Fch
        Text
        Eur
    End Enum

    Public Sub New(ByVal oConciliacio As BancConciliacio)
        MyBase.New()
        Me.InitializeComponent()
        mConciliacio = oConciliacio
        With mConciliacio
            'Xl_IBAN1.IBAN = .Banc.Iban
            DateTimePicker1.Value = .Fch
            If .BigFile IsNot Nothing Then
                Xl_BigFile1.BigFile = .BigFile.BigFile
            End If
            Xl_AmtSSaldo.Amt = .Saldo
        End With
        Refresca()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mConciliacio
            Dim oNSdo As DTOAmt = .Ccd.GetSaldo(.Fch)
            Dim oAnteriors As DTOAmt = .SumaItemsAnteriors
            Dim oPosteriors As DTOAmt = .SumaItemsPosteriors
            TextBoxNSdo.Text = oNSdo.Formatted
            TextBoxCcaAnteriors.Text = oAnteriors.Formatted
            TextBoxCcaPosteriors.Text = oPosteriors.Formatted
            Dim oResult As DTOAmt = oNSdo.Clone
            oResult.Add(oAnteriors)
            oResult.Substract(oPosteriors)
            oResult.Substract(Xl_AmtSSaldo.Amt)
            TextBoxResultat.Text = oResult.Formatted

            If oResult.Eur = 0 Then
                PictureBoxResult.Image = My.Resources.Ok
            End If
        End With
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT D.CCAGUID, CCB.CCA, CCB.FCH, CCA.TXT, CCB.EUR " _
        & "FROM CCB INNER JOIN CCA ON Ccb.CcaGuid = Cca.Guid  AND CCB.CTA LIKE @CTA AND CCB.CLI LIKE @CLI INNER JOIN " _
        & "ConciliacionsBancsDetail D ON D.CCAGUID LIKE CCA.GUID " _
        & "WHERE D.HEADERGUID LIKE @GUID " _
        & "ORDER BY CCB.FCH"

        Dim oCcd As Ccd = mConciliacio.Ccd
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@GUID", mConciliacio.Guid.ToString, "@CTA", oCcd.Cta.Id, "@CLI", oCcd.Contact.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Cca)
                .HeaderText = "apunt"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Text)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Function CurrentItem() As Cca
        Dim oCca As Cca = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New System.Guid(oRow.Cells(Cols.Guid).Value.ToString)
            oCca = New Cca(oGuid)
        End If
        Return oCca
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As New ToolStripMenuItem("afegir", My.Resources.clip, AddressOf AddNew)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.clip, AddressOf RemoveItem)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("assentament")
        oContextMenu.Items.Add(oMenuItem)

        Dim oCca As Cca = CurrentItem()
        If oCca IsNot Nothing Then
            Dim oMenu_Pais As New Menu_Cca(oCca)
            AddHandler oMenu_Pais.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Pais.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RemoveItem(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim oGuid As New System.Guid(oRow.Cells(Cols.Guid).Value.ToString)
        For i As Integer = 0 To mConciliacio.Items.Count - 1
            If mConciliacio.Items(i).Equals(oGuid) Then
                mConciliacio.Items.RemoveAt(i)
                mConciliacio.Update()
                RefreshRequest(sender, e)
                RaiseEvent AfterUpdate(sender, e)
                Exit For
            End If
        Next
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CliCtasOld(mConciliacio.Ccd, Frm_CliCtasOld.Modes.ForSelection)
        AddHandler oFrm.AfterSelect, AddressOf OnAddNew
        oFrm.Show()
    End Sub

    Private Sub OnAddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCca As Cca = DirectCast(sender, Cca)
        mConciliacio.Items.Add(oCca.Guid)
        Save()
        Refresca()
    End Sub


    Public Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Text
        Dim oGrid As DataGridView = DataGridView1
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub



    Private Sub Save()
        With mConciliacio
            .Fch = DateTimePicker1.Value
            .Saldo = Xl_AmtSSaldo.Amt
            If mDirtyBigFile IsNot Nothing Then
                .BigFile = New BigFileSrc(DTODocFile.Cods.ExtracteBancari, .Guid)
                .BigFile.BigFile = mDirtyBigFile
            End If
            .Update()
        End With
        RaiseEvent AfterUpdate(mConciliacio, EventArgs.Empty)
    End Sub



    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_BigFile1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_BigFile1.AfterUpdate
        mDirtyBigFile = Xl_BigFile1.BigFile
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Save()
        RaiseEvent AfterUpdate(sender, e)
        Me.Close()
    End Sub

    Private Sub Xl_AmtSSaldo_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_AmtSSaldo.AfterUpdate
        If mAllowEvents Then
            ButtonOk.Enabled = True
            Refresca()
        End If
    End Sub
End Class