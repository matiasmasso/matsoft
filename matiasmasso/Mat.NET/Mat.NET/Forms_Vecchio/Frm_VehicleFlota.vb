

Public Class Frm_VehicleFlota
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Matricula
        Model
        Fch
        Conductor
        IcoPrivat
        Obsoleto
        Privat
    End Enum

    Private Sub Frm_VehicleFlota_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT F.GUID, F.MATRICULA, (MC.NOM+' '+ML.NOM) AS MARCAIMODEL, F.ALTA, CLX, (CASE WHEN BAIXA IS NULL THEN 0 ELSE 1 END) AS OBSOLETO, PRIVAT " _
        & "FROM VEHICLE_FLOTA F INNER JOIN " _
        & "VEHICLE_MODELS ML ON ML.ID=F.MODEL INNER JOIN " _
        & "VEHICLE_MARCAS MC ON MC.ID=ML.MARCA LEFT OUTER JOIN " _
        & "CLX ON CLX.EMP=F.EMP AND CLX.CLI=F.CONDUCTOR " _
        & "WHERE F.EMP=@EMP "

        If Not BLL.BLLSession.Current.User.Rol.IsAdmin Then
            SQL = SQL & "AND F.PRIVAT=0 "
        End If
        SQL = SQL & "ORDER BY F.BAIXA, F.ALTA DESC, F.ID"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("IcoPrivat", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.IcoPrivat)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Matricula)
                .HeaderText = "matricula"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With
            With .Columns(Cols.Model)
                .HeaderText = "model"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
            End With
            With .Columns(Cols.Conductor)
                .HeaderText = "conductor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.IcoPrivat)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Obsoleto)
                .Visible = False
            End With
            With .Columns(Cols.Privat)
                .Visible = False
            End With
        End With
    End Sub

    Private Function CurrentItm() As Vehicle
        Dim oItm As Vehicle = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As System.Guid = oRow.Cells(Cols.Guid).Value
            oItm = New Vehicle(oGuid)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oItm As Vehicle = CurrentItm()
        If oItm IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom))
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

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoPrivat
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If CBool(oRow.Cells(Cols.Privat).Value) Then
                    e.Value = My.Resources.candau
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oVehicle As New Vehicle
        oVehicle.Alta = Today
        Dim oFrm As New Frm_Vehicle(oVehicle)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_Vehicle(CurrentItm())
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Matricula
        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
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

    Private Sub DataGridViewTpas_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim BlObsoleto As Boolean = CType(oRow.Cells(Cols.Obsoleto).Value, Boolean)
        Select Case BlObsoleto
            Case True
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            Case False
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub
End Class