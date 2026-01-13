

Public Class Frm_Client_Historial
    Private mClient As Client
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mGrouped As Boolean
    Private mTpas As Tpas

    Private _Customer As DTOCustomer

    Private Enum Cols
        PncGuid
        PdcGuid
        Pdc
        Lin
        Fch
        Qty
        Art
        Pn2
        Dto
        Eur
    End Enum

    Public WriteOnly Property Client() As Client
        Set(ByVal Value As Client)
            mClient = Value
            _Customer = New DTOCustomer(mClient.Guid)
            Me.Text = "HISTORIAL DE " & mClient.Clx
            LoadGrid()
        End Set
    End Property


    Private Sub LoadGrid()
        Dim SQL As String = ""
        Dim SQLSELECT As String = ""
        Dim SQLWHERE As String = ""
        Dim SQLGROUP As String = ""
        Dim SQLORDER As String = ""

        If mGrouped Then
            SQLSELECT = "SELECT NULL AS PncGuid, PDC.Guid, PDC.pdc, 0 AS LIN, PDC.fch, SUM(PNC.qty) AS QTY, TPA.DSC + ' / ' + STP.DSC AS DSC, SUM(PNC.Pn2) AS PN2, 0 AS dto, 0 AS VAL " _
            & "FROM PDC INNER JOIN " _
            & "PNC ON PDC.Guid = PNC.PdcGuid INNER JOIN " _
            & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
            & "STP ON ART.Category=STP.Guid INNER JOIN " _
            & "TPA ON TPA.Guid=STP.Brand "
            SQLWHERE = "WHERE PDC.CliGuid='" & mClient.Guid.ToString & "' "
            SQLGROUP = "GROUP BY PDC.Guid, Pdc.Yea, PDC.pdc, PDC.fch, STP.DSC "
            SQLORDER = "ORDER BY PDC.Yea DESC, PDC.PDC DESC"
        Else
            SQLSELECT = "SELECT Pnc.Guid AS PncGuid, PDC.Guid, PDC.pdc, PNC.LIN, PDC.fch, PNC.qty, ART.myD, PNC.Pn2, PNC.dto, (CASE WHEN CARREC=1 THEN PNC.eur ELSE 0 END) AS VAL " _
            & "FROM PDC INNER JOIN " _
            & "PNC ON PDC.Guid = PNC.PdcGuid INNER JOIN " _
            & "ART ON PNC.ArtGuid= ART.Guid " _
            & "INNER JOIN Stp ON Art.Category=Stp.Guid " _
            & "INNER JOIN Tpa ON Stp.Brand = Tpa.Guid "
            SQLWHERE = "WHERE PDC.CliGuid='" & mClient.Guid.ToString & "' "
            SQLORDER = "ORDER BY PDC.YEA DESC, PDC.PDC DESC, PNC.LIN"
        End If


        If CheckBoxFiltre.Checked Then

            If CurrentBrand() IsNot Nothing Then
                SQLSELECT = SQLSELECT & " AND Stp.Brand ='" & CurrentBrand.Guid.ToString & "' "
                If CurrentCategory() IsNot Nothing Then
                    SQLSELECT = SQLSELECT & " AND Art.Category ='" & CurrentCategory.Guid.ToString & "' "
                    If CurrentSku() IsNot Nothing Then
                        SQLSELECT = SQLSELECT & " AND Art.Guid='" & CurrentSku.Guid.ToString & "' "
                    End If
                End If
            End If
        End If

        SQL = SQLSELECT & SQLWHERE & SQLGROUP & SQLORDER
        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.PncGuid)
                .Visible = False
            End With
            With .Columns(Cols.PdcGuid)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .HeaderText = "Comanda"
                .Width = 60
            End With
            With .Columns(Cols.Lin)
                .Visible = False
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Qty)
                .Width = 40
                .HeaderText = "Quant"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Art)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Pn2)
                .HeaderText = "Pendent"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "Dto"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Preu"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Function CurrentBrand() As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing
        If ComboBoxTpa.SelectedIndex > 0 Then
            retval = ComboBoxTpa.SelectedItem
        End If
        Return retval
    End Function

    Private Function CurrentCategory() As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing
        If ComboBoxStp.SelectedIndex > 0 Then
            retval = ComboBoxStp.SelectedItem
        End If
        Return retval
    End Function

    Private Function CurrentSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        If ComboBoxArt.SelectedIndex > 0 Then
            retval = ComboBoxArt.SelectedItem
        End If
        Return retval
    End Function

    Private Function CurrentLin() As LineItmPnc
        Dim oLin As LineItmPnc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.PncGuid).Value
            oLin = New LineItmPnc(oGuid)
            oLin.SetItm()
        End If
        Return oLin
    End Function

    Private Sub MenuItmRefresca_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadGrid()
    End Sub

    Private Sub MenuItmPdc_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPdc As Pdc = CurrentLin.Pdc

        If oPdc IsNot Nothing Then
            Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oPdc.Guid)
            Dim exs As New List(Of Exception)
            If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, _Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                UIHelper.WarnError(exs)
            Else
                Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            End If
        End If


    End Sub

    Private Sub MenuItmArt_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Art(CurrentLin.Art)
        AddHandler oFrm.AfterUpdate, AddressOf refreshrequest
        oFrm.show()
    End Sub

    Private Sub MenuItmArc_OnClick(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowPncSortides(CurrentLin)
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "refresca"
            .Image = My.Resources.refresca
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItmRefresca_OnClick
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "comanda"
            '.Image = My.Resources.NewDoc
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItmPdc_OnClick
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "article"
            '.Image = My.Resources.NewDoc
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItmArt_OnClick
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "sortides"
            '.Image = My.Resources.NewDoc
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItmArc_OnClick
        oContextMenu.Items.Add(oMenuItem)


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub LoadTpas()
        Dim oCustomer As New DTOCustomer(mClient.Guid)
        Dim oBrands As List(Of DTOProductBrand) = BLL.BLLProductBrands.FromCustomerOrders(oCustomer)
        Dim oNoBrand As New DTOProductBrand(System.Guid.Empty)
        oNoBrand.Nom = "(seleccionar una marca)"
        oBrands.Insert(0, oNoBrand)
        With ComboBoxTpa
            .DisplayMember = "Nom"
            .DataSource = oBrands
            .SelectedIndex = 0
        End With

        ComboBoxStp.Visible = False
    End Sub

    Private Sub LoadStps()
        Dim oCustomer As New DTOCustomer(mClient.Guid)
        Dim oBrand As DTOProductBrand = CurrentBrand()
        If oBrand Is Nothing Then
            ComboBoxStp.Visible = False
        Else
            Dim oCategories As List(Of DTOProductCategory) = BLL.BLLProductCategories.FromCustomerOrders(oCustomer, stockOnly:=False, oBrand:=oBrand)
            Dim oNoCategory As New DTOProductCategory(System.Guid.Empty)
            oNoCategory.Nom = "(totes les categories)"
            oCategories.Insert(0, oNoCategory)
            With ComboBoxStp
                .DisplayMember = "Nom"
                .DataSource = oCategories
                .SelectedIndex = 0
            End With
        End If
        ComboBoxArt.Visible = False
    End Sub

    Private Sub LoadArts()
        Dim oCustomer As New DTOCustomer(mClient.Guid)
        Dim oCategory As DTOProductCategory = CurrentCategory()
        If oCategory Is Nothing Then
            ComboBoxArt.Visible = False
        Else
            Dim oSkus As List(Of DTOProductSku) = BLL.BLLProductSkus.FromCustomerOrders(oCustomer, oCategory)
            Dim oNoSku As New DTOProductSku(System.Guid.Empty)
            oNoSku.NomCurt = "(tots els articles)"
            oSkus.Insert(0, oNoSku)
            With ComboBoxArt
                .DisplayMember = "NomCurt"
                .DataSource = oSkus
                .SelectedIndex = 0
            End With
        End If
    End Sub

    Private Sub ButtonRefreshFiltre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonRefreshFiltre.Click
        LoadGrid()
        SetDirty(False)
    End Sub

    Private Sub ComboBoxTpa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxTpa.SelectedIndexChanged
        If ComboBoxStp.Items.Count > 0 Then ComboBoxStp.SelectedIndex = 0
        If ComboBoxArt.Items.Count > 0 Then ComboBoxArt.SelectedIndex = 0
        LoadStps()
        ComboBoxStp.Visible = True
        SetDirty(True)
    End Sub

    Private Sub ComboBoxStp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxStp.SelectedIndexChanged
        If ComboBoxStp.SelectedIndex = 0 Then
            If ComboBoxArt.Items.Count > 0 Then ComboBoxArt.SelectedIndex = 0
            ComboBoxArt.Visible = False
        Else
            LoadArts()
            ComboBoxArt.Visible = True
        End If
        SetDirty(True)
    End Sub


    Private Sub ComboBoxArt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxArt.SelectedIndexChanged
        SetDirty(True)
    End Sub

    Private Sub CheckBoxFiltre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxFiltre.CheckedChanged
        Static TpaLoaded As Boolean
        If Not TpaLoaded Then
            LoadTpas()
            TpaLoaded = True
        End If
        ComboBoxTpa.Visible = CheckBoxFiltre.Checked
        ComboBoxStp.Visible = False
        ComboBoxArt.Visible = False
        SetDirty(True)
    End Sub

    Private Sub SetDirty(ByVal blDirty As Boolean)
        Static DefaultBackGroundColor As Color
        If blDirty Then
            DefaultBackGroundColor = DataGridView1.BackgroundColor
            DataGridView1.BackgroundColor = Me.BackColor
        Else
            If Not IsDBNull(DefaultBackGroundColor) Then
                DataGridView1.BackgroundColor = DefaultBackGroundColor
            End If
        End If
        ButtonRefreshFiltre.Enabled = blDirty
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Qty

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


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim IntPn2 As Integer = oRow.Cells(Cols.Pn2).Value
        If IntPn2 > 0 Then
            PaintGradientRowBackGround(e, Color.GreenYellow)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            Me.DataGridView1.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            Me.DataGridView1.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub
End Class