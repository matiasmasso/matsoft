

Public Class Frm_CostDtos
    Private mTpa As Tpa
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Nom
        Dto
    End Enum

    Public Sub New(oTpa As Tpa)
        MyBase.New()
        Me.InitializeComponent()
        mTpa = oTpa
    End Sub

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CostDto.Guid, Product.Nom, CostDto.Dto/100 " _
                            & "FROM CostDto INNER JOIN " _
                            & "PRODUCT ON CostDto.Product = Product.Guid " _
                            & "WHERE Product.Emp=@Emp AND Product.TpaId=@Tpa " _
                            & "ORDER BY Product.Nom"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Emp", mTpa.emp.Id, "@Tpa", mTpa.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Dto)
                .HeaderText = "dte"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0%;-0%;"
            End With

        End With
    End Sub

    Private Function CurrentItm() As CostDto
        Dim oItm As CostDto = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = CType(oRow.Cells(Cols.Guid).Value, Guid)
            oItm = CostDtoLoader.FromGuid(oGuid)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As Object = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar", My.Resources.del, AddressOf Delete)
            oContextMenuStrip.Items.Add(oMenuItem)

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

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oItm As New CostDto()
        Dim oFrm As New Frm_CostDto(oItm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oItm As CostDto = CurrentItm()
        Dim oFrm As New Frm_CostDto(oItm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Delete()
        Dim oItm As CostDto = CurrentItm()
        Dim rc As MsgBoxResult = MsgBox("eliminem descompte del " & oItm.Dto & "% per " & oItm.Product.Nom(BLL.BLLApp.Lang) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim BlSuccess As Boolean = CostDtoLoader.delete(oItm)
            If BlSuccess Then
                RefreshRequest(Nothing, EventArgs.Empty)
            Else
                MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

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

End Class