

Public Class Frm_Trp_Tarifas_Old

    Private mTransportista As DTOTransportista
    Private mTrpZon As TrpZon
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Ico
        Nom
        Activat
    End Enum

    Public WriteOnly Property Transportista() As DTOTransportista
        Set(ByVal value As DTOTransportista)
            mTransportista = value
            refresca()
            SetContextMenu()
            mAllowEvents = True
        End Set
    End Property

    Private Sub refresca()
        Dim sTrpNom As String = mTransportista.Abr
        If sTrpNom = "" Then sTrpNom = BLLContact.NomComercialOrDefault(mTransportista)
        TextBoxNom.Text = sTrpNom
        PictureBoxLogo.Image = mTransportista.Logo
        Dim SQL As String = "SELECT TRPZON,NOM,ACTIVAT FROM TRPZONNOM WHERE " _
        & "TrpGuid='" & mTransportista.Guid.ToString & "' " _
        & "ORDER BY TRPZON"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        'With ListBox1
        '.DataSource = oDs.Tables(0)
        '.ValueMember = "TRPZON"
        '.DisplayMember = "NOM"
        'End With
        'SetButtons()

        Dim oTb As DataTable = oDs.Tables(0)
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

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

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Activat)
                .Visible = False
            End With
        End With
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Activat).Value Then
                    e.Value = My.Resources.empty
                Else
                    e.Value = My.Resources.del
                End If
        End Select
    End Sub

    Private Function CurrentItm() As TrpZon
        Dim oItm As TrpZon = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oItm = New TrpZon(mTransportista, CInt(oRow.Cells(Cols.Id).Value))
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oItm As TrpZon = CurrentItm()
        If oItm IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom))
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("eliminar", My.Resources.del, AddressOf DeleteTrpZon))
        End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DeleteTrpZon()
        Dim oTrpZon As TrpZon = CurrentItm()
        Dim rc As MsgBoxResult = MsgBox("eliminem tota la zona " & oTrpZon.Nom & vbCrLf & " de " & oTrpZon.Transportista.Nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        Select Case rc
            Case MsgBoxResult.Ok
                oTrpZon.Delete()
                refresca()
            Case Else
                MsgBox("Operació cancelada", MsgBoxStyle.Information, "MAT.NET")
        End Select
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oTrpZon As New TrpZon(mTransportista, 0)
        Dim oFrm As New Frm_Trp_Tarifa(oTrpZon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oTrpZon As TrpZon = CurrentItm()
        If oTrpZon Is Nothing Then
            oTrpZon = New TrpZon(mTransportista, 0)
        End If

        Dim oFrm As New Frm_Trp_Tarifa(oTrpZon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        refresca()

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


End Class