

Public Class Frm_Art_Pncs
    Private mCod As DTOPurchaseOrder.Codis
    Private mArt As Art
    Private mAllowEvents As Boolean
    Private mShowEta As Boolean

    Private Enum Cols
        Guid
        Id
        Qty
        Fch
        Clx
        Pvp
        Cur
        Dto
        Pdf
        PdfIco
        Pot
        Carrec
        Extra
        Eta
    End Enum

    Public WriteOnly Property Cod() As DTOPurchaseOrder.Codis
        Set(ByVal value As DTOPurchaseOrder.Codis)
            mCod = value
            Select Case mCod
                Case DTOPurchaseOrder.Codis.proveidor
                    Me.Text = "PENDENTS DE ENTREGA DE PROVEIDOR"
                    mShowEta = True
                Case DTOPurchaseOrder.Codis.client
                    Me.Text = "PENDENTS DE ENTREGA A CLIENT"
            End Select
        End Set
    End Property

    Public WriteOnly Property Art() As Art
        Set(ByVal Value As Art)
            mArt = Value
            RefrescaArt()
            LoadGrid()
        End Set
    End Property

    Private Sub RefrescaArt()
        mArt.SetItm()
        PictureBoxTpaLogo.Image = mArt.Stp.Tpa.Image
        LabelTpa.Text = mArt.Stp.Tpa.Nom
        LabelStp.Text = mArt.Stp.Nom
        LabelNomCurt.Text = mArt.NomCurt
        PictureBoxArt.Image = mArt.Image

        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu_Art As New Menu_Art(mArt)
        AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequestArt
        oContextMenu.Items.AddRange(oMenu_Art.Range)
        PictureBoxArt.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        Cursor = Cursors.WaitCursor
        Dim SQL As String = ""
        Select Case mCod
            Case DTOPurchaseOrder.Codis.proveidor
                SQL = "SELECT Pnc.PdcGuid, Pdc.PDC, PNC.pn2, PDC.FCH, CLX.CLX, PNC.EUR, PDC.CUR, PNC.DTO, " _
                & "(CASE WHEN PDF IS NULL THEN 0 ELSE 1 END) AS PDF, " _
                & "PNC.PN3 AS POT, CARREC, PDC.EXTRA, PDCCONFIRM.ETA " _
                & "FROM PNC INNER JOIN " _
                & "PDC ON PNC.PdcGuid=PDC.Guid INNER JOIN " _
                & "CLX ON PDC.CliGuid=CLX.Guid LEFT OUTER JOIN " _
                & "PDCCONFIRM ON PNC.PDCCONFIRM LIKE PDCCONFIRM.GUID " _
                & "WHERE PNC.ArtGuid=@ArtGuid AND " _
                & "PNC.PN2<>0 AND " _
                & "PNC.COD=" & CInt(mCod).ToString & " " _
                & "ORDER BY PDC.FCH, PDC.PDC"
            Case Else
                SQL = "SELECT Pnc.PdcGuid, Pdc.PDC, " _
                & "SUM(PNC.Pn2), " _
                & "PDC.FCH, CLX.CLX, PNC.EUR, PDC.CUR, PNC.DTO, " _
                & "(CASE WHEN PDF IS NULL THEN 0 ELSE 1 END) AS PDF, " _
                & "PNC.PN3 AS POT, CARREC, PDC.EXTRA, '' as ETA " _
                & "FROM PNC INNER JOIN " _
                & "PDC ON PNC.PdcGuid=PDC.Guid INNER JOIN " _
                & "CLX ON PDC.CliGuid=CLX.Guid INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid " _
                & "WHERE PNC.ArtGuid=@ArtGuid AND " _
                & "PNC.PN2<>0 AND " _
                & "PNC.COD=" & CInt(mCod).ToString & " " _
                & "GROUP by Pnc.PdcGuid, Pdc.PDC, PNC.pn2, PDC.FCH, CLX.CLX, PNC.EUR, PDC.CUR, PNC.DTO,(CASE WHEN PDF IS NULL THEN 0 ELSE 1 END),PNC.PN3 , CARREC, PDC.EXTRA " _
                & "ORDER BY PDC.FCH, Pdc.PDC"

        End Select
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@ArtGuid", mArt.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix icono PDF
        Dim oColF As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oColF.SetOrdinal(Cols.PdfIco)

        If mShowEta Then
            If mCod = DTOPurchaseOrder.Codis.client Then
                Dim oRow As DataRow = Nothing
                Dim oPdc As Pdc = Nothing
                Dim DtEta As Date
                For Each oRow In oTb.Rows
                    Dim oPdcGuid As Guid = oRow("PdcGuid")
                    oPdc = New Pdc(oPdcGuid)
                    DtEta = LineItmPnc.Eta(oPdc, mArt)
                    Select Case DtEta
                        Case Date.MinValue
                            oRow(Cols.Eta) = "inmediat"
                        Case Date.MaxValue
                            oRow(Cols.Eta) = "no confirmat"
                        Case Else
                            oRow(Cols.Eta) = Format(DtEta, "dd/MM/yy")
                    End Select
                Next
            End If
        End If

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

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                If mCod = DTOPurchaseOrder.Codis.client Then
                    .Visible = False
                Else
                    .HeaderText = "Comanda"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 45
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "Quant"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Pvp)
                .HeaderText = "Preu"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                '.DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Cur)
                .Visible = False
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "Dte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 35
                '.DefaultCellStyle.Format = "0%;-0%;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Pdf)
                .Visible = False
            End With
            With .Columns(Cols.PdfIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Pot)
                .Visible = False
            End With
            With .Columns(Cols.Carrec)
                .Visible = False
            End With
            With .Columns(Cols.Extra)
                .Visible = False
            End With
            With .Columns(Cols.Eta)
                If mShowEta Then
                    .HeaderText = "ETA"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 65
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Visible = True
                Else
                    .Visible = False
                End If
            End With
        End With
        Cursor = Cursors.Default
    End Sub

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            oPdc = New Pdc(oGuid)
        End If
        Return oPdc
    End Function

    Private Function CurrentPdcs() As Pdcs
        Dim oPdcs As New Pdcs

        If DataGridView1.SelectedRows.Count > 0 Then

            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oPdc As New Pdc(oGuid)
                oPdcs.Add(oPdc)
            Next
            oPdcs.Sort()
        Else
            Dim oPdc As Pdc = CurrentPdc()
            If oPdc IsNot Nothing Then
                oPdcs.Add(CurrentPdc)
            End If
        End If
        Return oPdcs
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem
        Dim oPdcs As Pdcs = CurrentPdcs()

        If oPdcs.Count > 0 Then
            Dim oMenu_Pdc As New Menu_Pdc(oPdcs)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pdc.Range)

            Select Case BLL.BLLSession.Current.User.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                    oMenuItem = New ToolStripMenuItem("calcular ETA", My.Resources.REDO, AddressOf Do_CalcularEta)
                    oContextMenu.Items.Add(oMenuItem)
            End Select

            Dim iPn2 As Integer
            For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
                iPn2 += CInt(oRow.Cells(Cols.Qty).Value)
            Next

            oMenuItem = New ToolStripMenuItem("total seleccionats " & Format(iPn2, "#,##0"))
            oContextMenu.Items.Add(oMenuItem)
        End If

        oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_CalcularEta(ByVal sender As Object, ByVal e As System.EventArgs)
        mShowEta = True
        Refresca()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim oGrid As DataGridView = DataGridView1
        Dim iFirstVisibleCell As Integer = oGrid.CurrentCell.ColumnIndex

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

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

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Dto
                If e.Value = 0 Then
                    e.Value = ""
                Else
                    e.Value = e.Value & "%"
                End If
            Case Cols.Pvp
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim sCur As String = oRow.Cells(Cols.Cur).Value
                Select Case sCur
                    Case "EUR"
                        e.Value = Format(CDbl(e.Value), "#,##0.00") & " €"
                    Case "GBP"
                        e.Value = Format(CDbl(e.Value), "#,##0.00") & " £"
                    Case "USD"
                        e.Value = Format(CDbl(e.Value), "#,##0.00") & " $"
                    Case Else
                        e.Value = Format(CDbl(e.Value), "#,##0.00") & " " & sCur
                End Select
            Case Cols.PdfIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Pdf).Value = 1 Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.Eta
                If mCod = DTOPurchaseOrder.Codis.client Then
                    If IsDBNull(e.Value) Then
                    Else
                        If e.Value = "inmediat" Then
                            e.CellStyle.BackColor = Color.LightGreen
                        ElseIf e.Value = "no confirmat" Then
                            e.CellStyle.BackColor = Color.LightSalmon
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowPdc()
    End Sub

    Private Sub ShowPdc()
        Dim oPdc As Pdc = CurrentPdc()
        Select Case oPdc.Cod
            Case DTOPurchaseOrder.Codis.Proveidor
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(oPdc.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_Pdc_Proveidor(oPdc)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                End If

            Case DTOPurchaseOrder.Codis.Client
                Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oPdc.Guid)
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                    oFrm.Show()
                End If
            Case Else
                MsgBox("visor no implementat per aquest tipus de comanda")
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If oRow.Cells(Cols.Extra).Value = True Then
            PaintGradientRowBackGround(e, Color.MediumSpringGreen)
        ElseIf oRow.Cells(Cols.Pot).Value = True Then
            PaintGradientRowBackGround(e, Color.Yellow)
        Else
            If oRow.Cells(Cols.Carrec).Value = False Then
                PaintGradientRowBackGround(e, Color.LightSalmon)
            Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

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
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub

    Private Sub PictureBoxArt_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxArt.DoubleClick
        Dim oFrm As New Frm_Art(mArt)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestArt
        oFrm.show
    End Sub

    Private Sub RefreshRequestArt(ByVal sender As Object, ByVal e As System.EventArgs)
        RefrescaArt()
    End Sub



End Class