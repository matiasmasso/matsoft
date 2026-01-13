

Public Class Frm_ECI_Pdcs

    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Pdc
        Fch
        Eci
        Ico
        Ref
        Platform
        Lins
        Pendents
        Servits
    End Enum

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadYeas()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT PDC.Guid, PDC.pdc, PDC.fch, SUBSTRING(PDC.pdd, 1, 8) AS ECInum, CliClient.ref, Pdc.Platform, " _
        & "COUNT(DISTINCT PNC.lin) AS lins, SUM(CASE WHEN PN2<>0 THEN 1 ELSE 0 END) AS pendents, SUM(CASE WHEN PN2=0 THEN 1 ELSE 0 END) AS servits " _
        & "FROM            PDC INNER JOIN " _
        & "CCX ON PDC.CliGuid = CCX.Guid LEFT OUTER JOIN " _
        & "PNC ON PDC.Guid = PNC.PdcGuid INNER JOIN " _
        & "CliClient ON PDC.CliGuid = CliClient.Guid " _
        & "WHERE CCX.EMP = @EMP AND CCX.CCX = @ECI AND PDC.YEA=@YEA AND (ISNUMERIC(SUBSTRING(PDC.pdd, 1, 8)) = 1) " _
        & "GROUP BY PDC.Guid, PDC.yea, PDC.pdc, PDC.fch, SUBSTRING(PDC.pdd, 1, 8), CliClient.ref, Pdc.Platform " _
        & "ORDER BY PDC.yea DESC, ECINUM DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@ECI", ElCorteIngles.ID, "@YEA", CurrentYea)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        With DataGridView1
            With .RowTemplate
                .Height = 17 ' DataGridView1.Font.Height * 1.3
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

            With .Columns(Cols.pdc)
                .HeaderText = "n/num"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
            End With

            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            With .Columns(Cols.Eci)
                .HeaderText = "comanda ECI"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 80
            End With

            With .Columns(Cols.Ref)
                .HeaderText = "centre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Platform)
                .Visible = False
            End With

            With .Columns(Cols.Lins)
                .Visible = False
            End With

            With .Columns(Cols.Pendents)
                .Visible = False
            End With

            With .Columns(Cols.Servits)
                .Visible = False
            End With

            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

        End With
        SetContextMenu()
    End Sub

    Private Function CurrentItm() As DTOPurchaseOrder
        Dim oItm As DTOPurchaseOrder = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oItm = New DTOPurchaseOrder(oRow.Cells(Cols.Guid).Value)
            oItm.Platform = New DTOCustomerPlatform(oRow.Cells(Cols.Platform).Value)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As DTOPurchaseOrder = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf Zoom)
            oContextMenuStrip.Items.Add(oMenuItem)
            oMenuItem = New ToolStripMenuItem("plataforma")
            oContextMenuStrip.Items.Add(oMenuItem)

            For Each oPlatf As Contact In ElCorteIngles.Plataformas
                Dim oDropDown As New ToolStripMenuItem(oPlatf.Clx)
                oDropDown.CheckOnClick = True
                oDropDown.Tag = oPlatf
                AddHandler oDropDown.CheckedChanged, AddressOf ChangePlatform

                If oItm.Platform IsNot Nothing Then
                    If oPlatf.Guid.Equals(oItm.Platform.Guid) Then
                        oDropDown.Checked = True
                    End If
                End If
                oMenuItem.DropDownItems.Add(oDropDown)
            Next
        End If

        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub ChangePlatform(sender As Object, e As EventArgs)
        If mAllowEvents Then
            Dim oItm As DTOPurchaseOrder = CurrentItm()
            Dim oMenu As ToolStripMenuItem = sender
            If oMenu.Checked Then
                Dim oPlatform As DTOCustomerPlatform = oItm.Platform
                Dim oSelected As Contact = oMenu.Tag
                If Not oSelected.Guid.Equals(oPlatform.Guid) Then
                    BLL.BLLPurchaseOrder.Load(oItm)
                    oItm.Platform = New DTOCustomerPlatform(oSelected.Guid)
                    Dim exs As New List(Of Exception)
                    If BLL.BLLPurchaseOrder.Update(oItm, exs) Then
                        SetContextMenu()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Zoom()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico

                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim iLins As Integer = oRow.Cells(Cols.Lins).Value
                Dim iPncs As Integer = oRow.Cells(Cols.Pendents).Value
                Dim iServits As Integer = oRow.Cells(Cols.Servits).Value

                If iPncs = 0 Then
                    If iServits = 0 Then
                        'buit
                        e.Value = My.Resources.NoPark
                    Else
                        'servit
                        e.Value = My.Resources.TruckGreen
                    End If
                ElseIf iPncs >= iLins Then
                    'per servir
                    e.Value = My.Resources.TruckWhite
                Else
                    'parcial
                    e.Value = My.Resources.TruckYellow
                End If

        End Select
    End Sub


    Private Sub Zoom()
        Dim oPurchaseOrder As DTOPurchaseOrder = CurrentItm()
        BLL.BLLPurchaseOrder.Load(oPurchaseOrder, BLL.BLLApp.Mgz)
        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If

    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Eci
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



    Private Sub LoadYeas()
        Dim SQL As String = "SELECT PDC.yea " _
        & "FROM            PDC INNER JOIN " _
        & "CCX ON PDC.CliGuid = CCX.Guid INNER JOIN " _
        & "CliClient ON PDC.CliGuid = CliClient.Guid " _
        & "WHERE CCX.EMP = @EMP AND CCX.CCX = @ECI " _
        & "GROUP BY PDC.yea " _
        & "ORDER BY PDC.yea DESC"

        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@ECI", ElCorteIngles.ID)
        Do While oDrd.Read
            ToolStripComboBoxYea.Items.Add(oDrd("YEA"))
        Loop
        ToolStripComboBoxYea.SelectedIndex = 0
        EnableYeaButtons()
    End Sub

    Private Function CurrentYea() As Integer
        Dim iYea As Integer = ToolStripComboBoxYea.SelectedItem
        Return iYea
    End Function

    Private Sub EnableYeaButtons()
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = ToolStripComboBoxYea.Items.Count
        AnyanteriorToolStripButton.Enabled = (Idx < iYeas - 1)
        AnysegüentToolStripButton.Enabled = (Idx > 0)
    End Sub

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx + 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub AnysegüentToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnysegüentToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx - 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            EnableYeaButtons()
            LoadGrid()
            mAllowEvents = True
        End If
    End Sub
End Class