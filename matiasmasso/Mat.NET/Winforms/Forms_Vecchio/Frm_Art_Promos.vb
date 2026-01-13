

Public Class Frm_Art_Promos
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mMode As Modes
    Private mAllowEvents As Boolean = False

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        TpaNom
        Nom
        FchFrom
        FchTo
    End Enum

    Public Enum Modes
        Consulta
        Seleccio
    End Enum

    Public Sub New(ByVal oMode As Modes)
        MyBase.New()
        Me.InitializeComponent()
        mMode = oMode
        LoadGrid()
        mallowevents = True
    End Sub


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT P.GUID,T.DSC,P.NOM,P.FCHFROM,P.FCHTO " _
        & "FROM ARTPROMOHDR P INNER JOIN " _
        & "TPA T ON P.EMP=T.EMP AND P.TPA=T.TPA " _
        & "WHERE P.EMP=@EMP " _
        & "ORDER BY T.ORD, P.FCHFROM DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id)
        Dim oTb As DataTable = oDs.Tables(0)
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
            With .Columns(Cols.TpaNom)
                .Width = 50
                .HeaderText = "marca"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Nom)
                .Width = 50
                .HeaderText = "promocio"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.FchFrom)
                .Width = 70
                .HeaderText = "desde"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.FchTo)
                .Width = 70
                .HeaderText = "fins"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With

    End Sub

    Private Function CurrentPromo() As ArtPromo
        Dim oPromo As ArtPromo = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As System.Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
            oPromo = New ArtPromo(oGuid)
        End If
        Return oPromo
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mMode = Modes.Seleccio Then
            RaiseEvent AfterSelect(CurrentPromo, EventArgs.Empty)
            Me.Close()
        Else
            Zoom(sender, e)
        End If
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Art_Promo(CurrentPromo)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPromo As ArtPromo = CurrentPromo()
        Dim oMenuItem As ToolStripMenuItem

        If oPromo IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem
            With oMenuItem
                .Text = "zoom"
                .Image = My.Resources.prismatics
            End With
            AddHandler oMenuItem.Click, AddressOf Zoom
            oContextMenu.Items.Add(oMenuItem)

        End If

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "afegeix"
            .Image = My.Resources.clip
        End With
        AddHandler oMenuItem.Click, AddressOf AddNew
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "refresca"
            .Image = My.Resources.refresca
        End With
        AddHandler oMenuItem.Click, AddressOf RefreshRequest
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPromo As New ArtPromo(New Tpa(mEmp, 104))
        oPromo.FchFrom = Today
        oPromo.FchTo = New Date(Today.Year, 12, 31)
        oPromo.Nom = "(nova promoció)"
        Dim oFrm As New Frm_Art_Promo(oPromo)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        ofrm.show()
    End Sub
End Class