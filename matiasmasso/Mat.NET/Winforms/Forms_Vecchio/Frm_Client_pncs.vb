

Public Class Frm_Client_pncs
    Private mClient As Client
    Private mProveidor As Proveidor
    Private mContact As Contact
    Private mCod As DTOPurchaseOrder.Codis
    Private mDs As DataSet

    Private Enum Cols
        PncGuid
        PdcGuid
        pdc
        art
        nom
        qty
        pvp
        cur
        dto
        carrec
        pot
        lin
        venuts
        LinCod
    End Enum

    Private Enum LinCods
        Blank
        Pdc
        Itm
    End Enum

    Public WriteOnly Property Proveidor() As Proveidor
        Set(ByVal value As Proveidor)
            mProveidor = value
            mContact = mProveidor
            mCod = DTOPurchaseOrder.Codis.proveidor
            Me.Text = Me.Text & " " & mContact.Clx
            LoadGrid()
        End Set
    End Property

    Public WriteOnly Property Client() As Client
        Set(ByVal Value As Client)
            mClient = Value
            mContact = mClient
            mCod = DTOPurchaseOrder.Codis.client
            Me.Text = Me.Text & " " & mContact.Clx
            LoadGrid()
        End Set
    End Property

    Private Function CreateTable() As DataTable
        Dim oTb As New DataTable()
        oTb.Columns.Add("PncGuid", System.Type.GetType("System.Guid"))
        oTb.Columns.Add("PdcGuid", System.Type.GetType("System.Guid"))
        oTb.Columns.Add("PDC", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("ART", System.Type.GetType("System.Guid"))
        oTb.Columns.Add("MYD", System.Type.GetType("System.String"))
        oTb.Columns.Add("PN2", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("PTS", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("CUR", System.Type.GetType("System.String"))
        oTb.Columns.Add("DTO", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("CARREC", System.Type.GetType("System.Boolean"))
        oTb.Columns.Add("POT", System.Type.GetType("System.Boolean"))
        oTb.Columns.Add("LIN", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("PDCCONFIRM", System.Type.GetType("System.String"))
        oTb.Columns.Add("ETA", System.Type.GetType("System.DateTime"))
        oTb.Columns.Add("VENUT", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("LINCOD", System.Type.GetType("System.Int32"))
        Return oTb
    End Function

    Private Sub LoadGrid()
        Dim SQL As String = ""

        Select Case mCod
            Case DTOPurchaseOrder.Codis.Proveidor
                SQL = "SELECT Pnc.Guid AS PncGuid, PNC.PdcGuid, Pdc.PDC, PNC.ArtGuid, " _
                    & "(case when art.REFprv = '' then  ART.myD else (CASE WHEN CHARINDEX(ART.REF,ART.REFPRV)=1 THEN art.REFprv ELSE ART.REF+' '+ART.REFPRV END) end) as MYD, PNC.Pn2, PNC.pts, PNC.Cur, PNC.dto, PNC.carrec, PDC.pot, PNC.LIN, " _
                & "(CASE WHEN ArtPn2NoPn3.Qty > ArtStock.Stock THEN (ArtPn2NoPn3.Qty - ArtStock.Stock) ELSE 0 END) AS VENUT, " & LinCods.Itm & " " _
               & "FROM PDC INNER JOIN " _
               & "PNC ON PDC.Guid = PNC.PdcGuid INNER JOIN " _
               & "ART ON PNC.ArtGuid = ART.Guid LEFT OUTER JOIN " _
               & "ArtStock ON ART.Guid=ArtStock.ArtGuid AND ArtStock.MGZGuid=@MGZGuid LEFT OUTER JOIN " _
               & "ArtPn2NoPn3 on ART.Guid=ArtPn2NoPn3.ArtGuid " _
               & "WHERE Pdc.CliGuid =@CliGuid AND " _
               & "PNC.Pn2 <> 0 AND " _
               & "PNC.Cod = 1 " _
               & "ORDER BY PDC.YEA, PDC.PDC, pnc.lin"
            Case DTOPurchaseOrder.Codis.Client
                SQL = "SELECT Pnc.Guid AS PncGuid, PNC.PdcGuid, Pdc.Pdc, PNC.ArtGuid, ART.myD, PNC.Pn2, PNC.pts, PNC.Cur, PNC.dto, PNC.carrec, PDC.pot, PNC.LIN, '',NULL, " _
                & "0 AS VENUT, " & LinCods.Itm & " " _
                & "FROM PDC INNER JOIN " _
                & "PNC ON PDC.Guid = PNC.PdcGuid INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid " _
                & "WHERE Pdc.CliGuid =@CliGuid AND " _
                & "PNC.Pn2<>0 AND " _
                & "PNC.Cod = 2 " _
                & "ORDER BY PDC.YEA, PDC.PDC, pnc.lin"
        End Select

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@CliGuid", mContact.Guid.ToString, "@MGZGuid", BLL.BLLApp.Mgz.Guid.ToString)

        'afageix capçaleres comanda
        Dim oLastPdc As New Pdc(mContact.Emp, mCod)
        Dim oRow As DataRow
        Dim oTb As DataTable = oDs.Tables(0)
        Dim FirstLine As Boolean = True

        mDs = New DataSet
        mDs.Tables.Add(CreateTable)
        For Each oRow In oTb.Rows
            Dim oPdcGuid As Guid = oRow(Cols.PdcGuid)
            If Not (oLastPdc.Guid.Equals(oPdcGuid)) Then
                If FirstLine Then
                    FirstLine = False
                Else
                    'deixa linia en blanc si no es al principi
                    mDs.Tables(0).Rows.Add(RowBlank)
                End If
                oLastPdc = New Pdc(oPdcGuid)
                mDs.Tables(0).Rows.Add(RowPdc(oLastPdc))
            End If
            mDs.Tables(0).Rows.Add(RowItm(oRow))
        Next



        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0)
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
            With .Columns(Cols.pdc)
                .Visible = False
            End With
            With .Columns(Cols.art)
                .Visible = False
            End With
            With .Columns(Cols.carrec)
                .Visible = False
            End With
            With .Columns(Cols.pot)
                .Visible = False
            End With
            With .Columns(Cols.cur)
                .Visible = False
            End With
            With .Columns(Cols.nom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.qty)
                .HeaderText = "cant"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.pvp)
                .HeaderText = "preu"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.dto)
                .HeaderText = "Dto"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "0\%;-0\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.lin)
                .Visible = False
            End With
            With .Columns(Cols.venuts)
                If mCod = DTOPurchaseOrder.Codis.Client Then
                    .Visible = False
                Else
                    .HeaderText = "venut"
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End With
            With .Columns(Cols.LinCod)
                .Visible = False
            End With
        End With
    End Sub

    Private Function RowItm(ByVal oSourceRow As DataRow) As DataRow
        Dim oDestRow As DataRow = mDs.Tables(0).NewRow
        Dim iCol As Integer
        For iCol = 0 To mDs.Tables(0).Columns.Count - 1
            oDestRow(iCol) = oSourceRow(iCol)
        Next
        Return oDestRow
    End Function

    Private Function RowPdc(ByVal oPdc As Pdc) As DataRow
        Dim sConcepte As String = oPdc.FullConcepte
        If oPdc.FchMin <> Nothing Then
            sConcepte = sConcepte & "   (servei " & oPdc.FchMin.ToShortDateString & ")"
        End If
        Dim oRow As DataRow = RowBlank()
        oRow(Cols.PncGuid) = System.Guid.Empty
        oRow(Cols.PdcGuid) = oPdc.Guid
        oRow(Cols.pdc) = oPdc.Id
        oRow(Cols.nom) = oPdc.FullConcepte ' mContact.Lang.Tradueix("pedido", "comanda", "order") & IIf(oPdc.Text > "", " " & oPdc.Text, "") & " " & mContact.Lang.Tradueix("del", "del", "from") & " " & oPdc.Fch.ToShortDateString
        oRow(Cols.lin) = 0
        oRow(Cols.LinCod) = LinCods.Pdc
        Return oRow
    End Function

    Private Function RowBlank() As DataRow
        Dim oRow As DataRow = mDs.Tables(0).NewRow
        oRow(Cols.PncGuid) = System.Guid.Empty
        oRow(Cols.PdcGuid) = System.Guid.Empty
        oRow(Cols.pdc) = 0
        oRow(Cols.art) = System.Guid.Empty
        oRow(Cols.nom) = ""
        oRow(Cols.qty) = False
        oRow(Cols.pvp) = 0
        oRow(Cols.dto) = 0
        oRow(Cols.pot) = False
        oRow(Cols.carrec) = False
        oRow(Cols.cur) = "EUR"
        oRow(Cols.lin) = 0
        oRow(Cols.LinCod) = LinCods.Blank
        Return oRow
    End Function



    Private Function CurrentSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.art).Value
            If Not oGuid.Equals(System.Guid.Empty) Then
                retval = New DTOProductSku(oGuid)
            End If
        End If
        Return retval
    End Function

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Try
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            If oRow IsNot Nothing Then
                Dim oGuid As Guid = oRow.Cells(Cols.PdcGuid).Value
                oPdc = New Pdc(oGuid)
            End If
        Catch ex As Exception
            Return Nothing
        End Try
        Return oPdc
    End Function

    Private Function CurrentPncs() As LineItmPncs
        Dim oGrid As DataGridView = DataGridView1
        Dim oEmp as DTOEmp = mContact.Emp
        Dim oPncs As New LineItmPncs
        Dim oPdc As Pdc = Nothing
        Dim oPnc As LineItmPnc = Nothing
        Dim oRow As DataGridViewRow = Nothing
        If oGrid.SelectedRows.Count > 0 Then
            For Each oRow In DataGridView1.SelectedRows
                If CType(oRow.Cells(Cols.LinCod).Value, LinCods) = LinCods.Itm Then
                    oPnc = New LineItmPnc(CType(oRow.Cells(Cols.PncGuid).Value, Guid))
                    oPnc.SetItm()
                    oPncs.Add(oPnc)
                End If
            Next
        Else
            oRow = oGrid.CurrentRow
            If oRow IsNot Nothing Then
                If CType(oRow.Cells(Cols.LinCod).Value, LinCods) = LinCods.Itm Then
                    oPnc = New LineItmPnc(CType(oRow.Cells(Cols.PncGuid).Value, Guid))
                    oPnc.SetItm()
                    oPncs.Add(oPnc)
                End If
            End If
        End If
        Return oPncs
    End Function


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oLinCod As LinCods = CType(oRow.Cells(Cols.LinCod).Value, LinCods)

        Select Case oLinCod
            Case LinCods.Itm
                Select Case e.ColumnIndex
                    Case Cols.qty
                        Dim BlPot As Boolean = oRow.Cells(Cols.pot).Value
                        If BlPot Then
                            e.CellStyle.BackColor = System.Drawing.Color.Yellow
                        End If
                    Case Cols.pvp
                        Dim BlCarrec As Boolean = oRow.Cells(Cols.carrec).Value
                        If Not BlCarrec Then
                            e.CellStyle.BackColor = System.Drawing.Color.LightSalmon
                        End If
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oLinCod As LinCods = CType(oRow.Cells(Cols.LinCod).Value, LinCods)

        Select Case oLinCod
            Case LinCods.Pdc
                oRow.DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue
        End Select

    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oSku As DTOProductSku = CurrentSku()
        Dim oPdc As Pdc = CurrentPdc()
        Dim oMenuItem As ToolStripMenuItem

        If oSku IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem
            oMenuItem.Text = "Article..."
            Dim oMenu_Art As New Menu_ProductSku(oSku)
            AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Art.Range)

            oContextMenu.Items.Add(oMenuItem)

            If oPdc IsNot Nothing Then
                oMenuItem = New ToolStripMenuItem
                oMenuItem.Text = "Comanda..."
                Dim oMenu_Pdc As New Menu_Pdc(oPdc)
                AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)

            End If

            oContextMenu.Items.Add(oMenuItem)
        Else
            If oPdc IsNot Nothing Then
                Dim oMenu_Pdc As New Menu_Pdc(oPdc)
                AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Pdc.Range)
            End If
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oSku As DTOProductSku = CurrentSku()

        If oSku IsNot Nothing Then
            Dim oFrm As New Frm_Art(oSku)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            Dim oItm As Pdc = CurrentPdc()
            If oItm IsNot Nothing Then
                Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oItm.Guid)
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim oDragDropReult As DragDropEffects
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim oPncs As LineItmPncs = CurrentPncs()
            oDragDropReult = DataGridView1.DoDragDrop(oPncs, DragDropEffects.Copy)
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        Dim oSheet As DTOExcelSheet = BLLExcel.Sheet(mDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class
