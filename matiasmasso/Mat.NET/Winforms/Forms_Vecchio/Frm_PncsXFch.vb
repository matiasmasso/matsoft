

Public Class Frm_PncsXFch
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Private Enum Cols
        Yea
        Pdc
        Fch
        CliNum
        CliNom
        Qty
        ArtGuid
        ArtNom
    End Enum

    Private Sub Frm_PncsXFch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT PDC.yea, PDC.pdc, PDC.fch, PDC.Cli, CLX.clx, PNC.Pn2, Art.Guid as ArtGuid, ART.myD " _
        & "FROM PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "CLX ON PDC.CliGuid = CLX.Guid " _
        & "WHERE PDC.EMP=@EMP AND " _
        & "PDC.cod =@COD AND " _
        & "PNC.Pn2 > 0 " _
        & "ORDER BY PDC.fch, PNC.lin"

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", BLLApp.Emp.Id, "@COD", DTOPurchaseOrder.Codis.Client)
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
            .AllowDrop = False
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .HeaderText = "comanda"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.CliNum)
                .Visible = False
            End With
            With .Columns(Cols.CliNom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "quant"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            With .Columns(Cols.ArtGuid)
                .Visible = False
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oPdc = Pdc.FromNum(mEmp, oRow.Cells(Cols.Yea).Value, oRow.Cells(Cols.Pdc).Value)
        End If
        Return oPdc
    End Function

    Private Function CurrentContact() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oContact = MaxiSrvr.Contact.FromNum(mEmp, CInt(oRow.Cells(Cols.CliNum).Value))
        End If
        Return oContact
    End Function

    Private Function CurrentSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = New DTOProductSku(CType(oRow.Cells(Cols.ArtGuid).Value, Guid))
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        If CurrentPdc() IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("comanda...")
            oContextMenu.Items.Add(oMenuItem)
            Dim oMenu_Pdc As New Menu_Pdc(CurrentPdc)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.Image = My.Resources.prismatics
            oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)

            Dim oContact As Contact = CurrentContact()
            If oContact IsNot Nothing Then
                oMenuItem = New ToolStripMenuItem("client...")
                oContextMenu.Items.Add(oMenuItem)
                Dim oMenu_Contact As New Menu_Contact(CurrentContact)
                AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.Image = My.Resources.People_Blue
                oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
            End If

            oMenuItem = New ToolStripMenuItem("article...")
            oContextMenu.Items.Add(oMenuItem)
            Dim oMenu_Art As New Menu_ProductSku(CurrentSku)
            AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Art.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellChanged
        If mAllowEvents Then SetContextMenu()
    End Sub

    Private Sub RefreshRequest()
        mAllowEvents = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()
        LoadGrid()
        DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
        mAllowEvents = True

        If i > DataGridView1.Rows.Count - 1 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
        Else
            DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
        End If
    End Sub


End Class