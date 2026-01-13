Public Class Xl_Alb_lineItems

    Private mAlb As Alb = Nothing
    Private mFch As Date = Date.MinValue

    Private BCOLORBLANK As Color = Color.FromArgb(240, 240, 240)
    Private BCOLORPDC As Color = Color.FromArgb(153, 255, 255)
    Private BCOLORSPV As Color = Color.FromArgb(224, 255, 255)
    Private BCOLOROBS As Color = Color.FromArgb(255, 255, 153)
    Private BCOLORITM As Color = Color.FromArgb(250, 250, 250)

    Private mTb As DataTable = Nothing
    Private mSuma As MaxiSrvr.Amt = Nothing
    Private mTot As MaxiSrvr.Amt = Nothing
    Private mMenuItemEditPreus As New ToolStripMenuItem("editar preus i descomptes", Nothing, AddressOf Do_EditPreus)

    Private mLineasDeAlbaran As LineasDeAlbaran

    Private mFlagTabThrough As Boolean = True
    Private mDirtyCell As Boolean = False
    Private mDirtyQty As Boolean = False
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        idx
        linCod
        artId
        pendent
        concepte
        icono
        sortida
        preu
        discount
        import
        stock
        clients
    End Enum

    Private Enum LinCods
        Itm
        Blank
        Pdc
        SpvFirstLine
        SpvOtherLines
        Obs
        Footer
    End Enum

    Private Enum ServirTotJuntConds
        NotSet
        SurtTot
        NoSurtRes
    End Enum

    Private Enum WarnIcons
        NotSet
        ServirTotJunt
        FchMin
        MinPack
        Bell
    End Enum

    Public WriteOnly Property Alb() As Alb
        Set(ByVal value As Alb)
            mAlb = value
            mFch = mAlb.Fch

            setDataSource()
            SetGridColumns()
            LoadGrid()
            SetTotals()
            mAllowEvents = True
        End Set
    End Property


    Public ReadOnly Property Items() As LineItmArcs
        Get
            Dim retval As LineItmArcs = mLineasDeAlbaran.NonEmptyLineItmArcs
            Return retval
        End Get
    End Property

    Public Sub SetIvaExempt(ByVal oIvaExempt As Boolean)
        mAlb.IvaExempt = oIvaExempt
        SetTotals()
    End Sub

    Public Sub SetFch(ByVal DtNewFch As Date)
        mFch = DtNewFch
        SetTotals()
    End Sub

    Public Function IsEmpty() As Boolean
        Dim RetVal As Boolean = True
        For Each oRow As DataGridViewRow In MatDataGridView1.Rows
            If getLincod(oRow) = LinCods.Itm Then
                If oRow.Cells(Cols.sortida).Value <> 0 Then
                    RetVal = False
                    Exit For
                End If
            End If
        Next
        Return RetVal
    End Function


    Private Sub SetTotals()
        Dim oClient As Client = mAlb.Client
        Dim oLang As DTOLang = oClient.Lang

        Dim oTotal As MaxiSrvr.Amt = mLineasDeAlbaran.SumaDeImports
        Dim sb As New System.Text.StringBuilder
        sb.Append(oTotal.CurFormat)

        Dim BlDtoExists As Boolean = False
        Dim DcDto As Decimal = mAlb.DtoPct
        If DcDto > 0 Then
            sb.Append(" -" & DcDto.ToString & "% ")
            oTotal.DeductPercent(DcDto)
            BlDtoExists = True
        End If

        Dim DcDpp As Decimal = mAlb.DppPct
        If DcDpp > 0 Then
            sb.Append(" -" & DcDpp.ToString & "% ")
            oTotal.DeductPercent(DcDpp)
            BlDtoExists = True
        End If

        If BlDtoExists Then
            sb.Append("total " & oTotal.Formatted & " ")
        End If

        If mAlb.Export Or mAlb.IvaExempt Then
            sb.Append(oLang.Tradueix("(exento de IVA)", "(exempt de IVA)", "(VAT n/a)"))
        Else
            Dim oBaseImponible As MaxiSrvr.Amt = oTotal.Clone

            If mAlb.Client.IVA Then

                Dim oItms As LineItmArcs = mLineasDeAlbaran.NonEmptyLineItmArcs
                For Each oBase As MaxiSrvr.IvaBaseQuota In oItms.IvaBaseQuotas
                    'Dim oIva As New maxisrvr.Iva(oBase.IvaCod, mFch)
                    Dim oIva As MaxiSrvr.Iva = MaxiSrvr.Iva.Current(oBase.Iva.Codi, mFch)
                    sb.Append(" +")
                    sb.Append(oIva.Tipus.ToString & "% ")
                    sb.Append(oIva.Text(oLang))
                    sb.Append(" ")

                    If Not oBase.Base.Equals(oBaseImponible) Then
                        sb.Append("s/" & oBase.Base.Formatted)
                    End If

                    oTotal.Add(oIva.Quota(oBase.Base))
                Next
                If mAlb.Client.CcxOrMe.REQ Then
                    For Each oBase As MaxiSrvr.IvaBaseQuota In oItms.IvaBaseQuotas
                        Dim oIva As MaxiSrvr.Iva = MaxiSrvr.Iva.Current(oBase.Iva.Codi, mFch)
                        Dim oReq As MaxiSrvr.Iva = oIva.RecarrecEquivalencia

                        sb.Append(" +")
                        sb.Append(oReq.Tipus.ToString & "% ")
                        sb.Append(oReq.Text)
                        sb.Append(" ")

                        If Not oBase.Base.Equals(oBaseImponible) Then
                            sb.Append("s/" & oBase.Base.Formatted)
                        End If

                        oTotal.Add(oReq.Quota(oBase.Base))
                    Next
                End If
            End If

            sb.Append(" total " & oTotal.CurFormat)
        End If

        mTot = oTotal

        LabelTotals.Text = sb.ToString
    End Sub



    Private Sub setDataSource()
        mTb = New DataTable
        With mTb.Columns
            .Add(New DataColumn("idx", Type.GetType("System.Int32")))
            .Add(New DataColumn("linCod", Type.GetType("System.Int32")))
            .Add(New DataColumn("artId", Type.GetType("System.Int32")))
            .Add(New DataColumn("pendent", Type.GetType("System.Int32")))
            .Add(New DataColumn("concepte", Type.GetType("System.String")))
            .Add(New DataColumn("icono", Type.GetType("System.Byte[]")))
            .Add(New DataColumn("sortida", Type.GetType("System.Int32")))
            .Add(New DataColumn("preu", Type.GetType("System.Int32")))
            .Add(New DataColumn("discount", Type.GetType("System.Int32")))
            .Add(New DataColumn("import", Type.GetType("System.Int32")))
            .Add(New DataColumn("stock", Type.GetType("System.Int32")))
            .Add(New DataColumn("clients", Type.GetType("System.Int32")))
        End With
    End Sub

    Private Sub LoadGrid()
        Dim oLastPdc As New Pdc(Guid.NewGuid)
        Dim oLastSpv As New Spv(Guid.NewGuid)
        Dim FirstRow As Boolean = True

        If mAlb.IsNew Then
            mLineasDeAlbaran = LineasDeAlbaran.FromClient(mAlb.Client, mAlb.Mgz, mAlb.Cod)
            If mAlb.Cod = DTOPurchaseOrder.Codis.client Then
                mLineasDeAlbaran.SetIncentius()
            End If
        Else
            mLineasDeAlbaran = LineasDeAlbaran.FromAlb(mAlb)
            LabelUser.Text = mAlb.UserText
        End If

        For idx As Integer = 0 To mLineasDeAlbaran.Count - 1

            Dim oItem As LineaDeAlbaran = mLineasDeAlbaran(idx)

            Select Case mAlb.Cod
                Case DTOPurchaseOrder.Codis.Reparacio
                    Dim oSpv As Spv = SpvFromIdx(idx)
                    If oSpv IsNot Nothing Then
                        If Not oSpv.Guid.Equals(oLastSpv.Guid) Then
                            oLastSpv = oSpv
                            LoadRowSpv(idx)
                        End If
                    End If

                Case Else
                    If Not oLastPdc.Guid.Equals(oItem.Pnc.Pdc.Guid) Then
                        oLastPdc = oItem.Pnc.Pdc
                        If Not FirstRow Then LoadRow(idx, LinCods.Blank)
                        FirstRow = False
                        LoadRowPdc(idx)
                    End If
            End Select

            LoadRowItm(idx)
            FirstRow = False
        Next


    End Sub

    Private Sub SetGridColumns()

        With MatDataGridView1
            .AutoGenerateColumns = False
            With .RowTemplate
                .Height = MatDataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .ReadOnly = False
            .Columns.Clear()

            .Columns.Add(New TabStopTextBoxColumn)
            With .Columns(Cols.idx)
                .Visible = False
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With .Columns(Cols.linCod)
                .Visible = False
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.artId), TabStopTextBoxColumn)
                If mAlb.Cod = DTOPurchaseOrder.Codis.Proveidor Then
                    .HeaderText = "ref"
                    .ReadOnly = True
                    .TabStop = False
                    .Width = 50
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                Else
                    .Visible = False
                End If
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.pendent), TabStopTextBoxColumn)
                If mAlb.IsNew Then
                    .HeaderText = "pendent"
                    .ReadOnly = True
                    .TabStop = False
                    .Width = 50
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,###"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.concepte), TabStopTextBoxColumn)
                .HeaderText = "concepte"
                .ReadOnly = True
                .TabStop = False
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With .Columns(Cols.icono)
                If mAlb.IsNew Then
                    .HeaderText = ""
                    .Width = 18 ' 16
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .ReadOnly = True
                Else
                    .Visible = False
                End If
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.sortida), TabStopTextBoxColumn)
                .HeaderText = "sortida"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = False
                .TabStop = True
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.preu), TabStopTextBoxColumn)
                .HeaderText = "preu"
                .ReadOnly = True
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                '.DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .TabStop = False
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.discount), TabStopTextBoxColumn)
                .HeaderText = "dte"
                .ReadOnly = True
                .Width = 35
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .TabStop = False
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.import), TabStopTextBoxColumn)
                .HeaderText = "import"
                .ReadOnly = True
                .TabStop = False
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = BCOLORBLANK
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.stock), TabStopTextBoxColumn)
                If mAlb.IsNew Then
                    .HeaderText = "stock"
                    .ReadOnly = True
                    .TabStop = False
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,###"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.BackColor = BCOLORBLANK
                Else
                    .Visible = False
                End If
            End With
            .Columns.Add(New TabStopTextBoxColumn)
            With CType(.Columns(Cols.clients), TabStopTextBoxColumn)
                If mAlb.IsNew Then
                    .HeaderText = "clients"
                    .ReadOnly = True
                    .TabStop = False
                    .Width = 40
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,###"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.BackColor = BCOLORBLANK
                Else
                    .Visible = False
                End If
            End With
        End With

    End Sub

    Private Function BlankRow() As DataRow
        Dim oRow As DataRow = mTb.NewRow
        oRow(Cols.linCod) = LinCods.Blank
        oRow(Cols.concepte) = ""
        Return oRow
    End Function

    Private Function PdcRow(idx As Integer) As DataRow
        Dim oPdc As Pdc = PdcFromIdx(idx)
        Dim oRow As DataRow = mTb.NewRow
        oRow(Cols.linCod) = LinCods.Pdc
        oRow(Cols.concepte) = oPdc.FullConcepte
        Return oRow
    End Function

    Private Function ObsRow(ByVal sObs As String) As DataRow
        Dim oRow As DataRow = mTb.NewRow
        oRow(Cols.linCod) = LinCods.Obs
        oRow(Cols.concepte) = sObs
        Return oRow
    End Function

    Private Function LoadRow(ByVal ItemIdx As Integer, ByVal oLinCod As LinCods, Optional ByVal sText As String = "")
        Dim iRow As Integer = MatDataGridView1.Rows.Add
        Dim oRow As DataGridViewRow = MatDataGridView1.Rows(iRow)
        With oRow
            .Cells(Cols.idx).Value = ItemIdx
            .Cells(Cols.linCod).Value = oLinCod
            .Cells(Cols.concepte).Value = sText
            .ReadOnly = True
        End With
        Return oRow
    End Function

    Private Sub LoadRowItm(idx As Integer)
        Dim oitm As LineaDeAlbaran = mLineasDeAlbaran(idx)
        Dim oRow As DataGridViewRow = LoadRow(idx, LinCods.Itm, oitm.Art.Nom_ESP)
        With oRow
            .Cells(Cols.artId).Value = oitm.Art.Id
            If oitm.Pnc IsNot Nothing Then
                .Cells(Cols.pendent).Value = oitm.Pnc.Pendent
            End If
            .Cells(Cols.concepte).Value = oitm.Art.Nom_ESP
            .Cells(Cols.icono).Value = oitm.Icono

            If Not (mAlb.IsNew And mAlb.Cod = DTOPurchaseOrder.Codis.Proveidor) Then
                .Cells(Cols.sortida).Value = oitm.Qty
            End If

            .Cells(Cols.preu).Value = oitm.Preu
            .Cells(Cols.discount).Value = oitm.Dto
            .Cells(Cols.import).Value = oitm.Amt
            .Cells(Cols.stock).Value = oitm.Stock
            .Cells(Cols.clients).Value = oitm.Clients
            .ReadOnly = False
        End With
    End Sub


    Private Function LineaDeAlbaranFromIdx(idx As Integer) As LineaDeAlbaran
        Dim retval As LineaDeAlbaran = mLineasDeAlbaran(idx)
        Return retval
    End Function

    Private Function LineItmPncFromIdx(idx As Integer) As LineItmPnc
        Dim oLineaDeAlbaran As LineaDeAlbaran = LineaDeAlbaranFromIdx(idx)
        Dim retval As LineItmPnc = oLineaDeAlbaran.Pnc
        Return retval
    End Function

    Private Function PdcFromIdx(idx As Integer) As Pdc
        Dim oLineItmPnc As LineItmPnc = LineItmPncFromIdx(idx)
        Dim retval As Pdc = oLineItmPnc.Pdc
        Return retval
    End Function

    Private Function SpvFromIdx(idx As Integer) As spv
        Dim oLineaDeAlbaran As LineaDeAlbaran = LineaDeAlbaranFromIdx(idx)
        Dim retval As spv = oLineaDeAlbaran.Spv
        Return retval
    End Function


    Private Sub LoadRowPdc(idx As Integer)
        Dim oPdc As Pdc = PdcFromIdx(idx)
        Dim oRow As DataGridViewRow = LoadRow(idx, LinCods.Pdc, oPdc.FullConcepte)
        If oPdc.Obs > "" Then LoadRowObs(idx, oPdc.Obs)

        If mAlb.IsNew Then
            If mAlb.Cod = DTOPurchaseOrder.Codis.client Then
                If oPdc.FchMin > Today Then
                    LoadRowObs(idx, "ATENCIO: No servir abans de " & oPdc.FchMin.ToShortDateString)
                End If
            End If

            If Not oPdc.Fpg.CliDefault Then
                LoadRowObs(idx, oPdc.Fpg.Text())
            End If
        End If

    End Sub

    Private Sub LoadRowObs(idx As Integer, ByVal sObs As String)
        Dim oRow As DataGridViewRow = LoadRow(idx, LinCods.Obs, sObs)
    End Sub

    Private Sub LoadRowSpv(idx As Integer)
        Dim oRow As DataGridViewRow = Nothing
        Dim oSpv As spv = SpvFromIdx(idx)
        Dim oSpvTextArray As ArrayList = oSpv.TextArray
        Dim oCod As LinCods = LinCods.SpvFirstLine
        For i As Integer = 0 To oSpvTextArray.Count - 1
            oRow = LoadRow(idx, oCod, oSpvTextArray(i).ToString.TrimEnd)
            oCod = LinCods.SpvOtherLines
        Next
    End Sub

    Private Sub MatDataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles MatDataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.sortida
                Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols.linCod).Value, LinCods)
                    Case LinCods.Itm
                        If mAlb.IsNew And mAlb.Cod = DTOPurchaseOrder.Codis.client Then
                            Dim estaEnElPot As Boolean '= oRow.Cells(Cols.Pot).Value
                            If estaEnElPot Then
                                e.CellStyle.BackColor = Color.FromArgb(255, 204, 255)
                            Else
                                Dim iPnc, iStk As Integer
                                If Not IsDBNull(oRow.Cells(Cols.clients).Value) Then
                                    iPnc = oRow.Cells(Cols.clients).Value
                                End If
                                If Not IsDBNull(oRow.Cells(Cols.stock).Value) Then
                                    iStk = oRow.Cells(Cols.stock).Value
                                End If
                                If iStk > iPnc Then
                                    e.CellStyle.BackColor = Art.COLOR_STOCK
                                ElseIf iStk > 0 Then
                                    e.CellStyle.BackColor = Art.COLOR_RESERVED
                                Else
                                    e.CellStyle.BackColor = Art.COLOR_NOSTOCK
                                End If
                            End If

                        End If
                End Select
            Case Cols.icono
                If mAlb.IsNew And mAlb.Cod = DTOPurchaseOrder.Codis.client Then
                    Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                    e.Value = GetWarnIcon(oRow)
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.concepte
                If mAlb.Cod = DTOPurchaseOrder.Codis.reparacio Then
                    Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                    Select Case CType(oRow.Cells(Cols.linCod).Value, LinCods)
                        Case LinCods.SpvOtherLines
                            e.CellStyle.Padding = New Padding(20, 0, 0, 0)
                            e.CellStyle.Font = New Font(e.CellStyle.Font, FontStyle.Italic)
                            e.CellStyle.BackColor = BCOLORSPV
                        Case LinCods.Itm
                            e.CellStyle.Padding = New Padding(40, 0, 0, 0)
                    End Select
                End If
            Case Cols.import
                'e.CellStyle.Format =
                'e.DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            Case Cols.discount
                If IsNumeric(e.Value) Then
                    If e.Value <> 0 Then
                        If CInt(e.Value) = e.Value Then
                            e.Value = Format(CDec(e.Value), "0") & "%"
                        Else
                            e.Value = CDec(e.Value) & "%"
                        End If
                    Else
                        e.Value = ""
                    End If
                Else
                    e.Value = ""
                End If
        End Select
    End Sub

    Private Sub MatDataGridView1_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles MatDataGridView1.RowPostPaint
        'merge datagridviewcells for epigrafs Cols.nom+cols.value
        Dim oBackgroundColor As System.Drawing.Color = Color.White

        Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
        Dim LinCod As LinCods = oRow.Cells(Cols.linCod).Value
        Select Case LinCod
            Case LinCods.Blank
                oBackgroundColor = BCOLORBLANK
            Case LinCods.Pdc, LinCods.SpvFirstLine
                oBackgroundColor = BCOLORPDC
            Case LinCods.Obs
                oBackgroundColor = BCOLOROBS
            Case LinCods.SpvOtherLines
                oBackgroundColor = BCOLORSPV
        End Select

        Select Case LinCod
            Case LinCods.Pdc, LinCods.Obs, LinCods.Blank, LinCods.SpvFirstLine ', LinCods.Spv

                Dim X As Integer = e.RowBounds.Left ' MatDataGridView1.GetColumnDisplayRectangle(Cols.concepte, True).X
                Dim Y As Integer = e.RowBounds.Y
                Dim Width As Integer = e.RowBounds.Right - X
                Dim Height As Integer = e.RowBounds.Bottom - Y
                Dim oBrush As New SolidBrush(oBackgroundColor)
                Dim oMergeRectangle As New Rectangle(X, Y, Width, Height - 1)
                e.Graphics.FillRectangle(oBrush, oMergeRectangle)

                Dim s As String = oRow.Cells(Cols.concepte).Value
                oBrush = New SolidBrush(Color.Black)
                Dim oRectangle As New Rectangle(oMergeRectangle.X, oMergeRectangle.Y + 1, oMergeRectangle.Width, oMergeRectangle.Height)
                e.Graphics.DrawString(s, MatDataGridView1.Font, oBrush, oRectangle)
        End Select

    End Sub


    Private Sub MatDataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles MatDataGridView1.CellValidating
        If mAllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.sortida, Cols.pendent
                    Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                    Dim sEditedValue As String = oRow.Cells(e.ColumnIndex).EditedFormattedValue
                    If IsNumeric(sEditedValue) Then
                        If oRow.Cells(e.ColumnIndex).Value Is Nothing Then
                            mDirtyCell = True
                            mDirtyQty = True
                        Else
                            If sEditedValue <> oRow.Cells(e.ColumnIndex).Value.ToString Then
                                mDirtyCell = True
                                mDirtyQty = True
                            End If
                        End If

                    End If
                Case Cols.sortida, Cols.pendent, Cols.preu, Cols.discount
                    Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                    Dim sEditedValue As String = oRow.Cells(e.ColumnIndex).EditedFormattedValue
                    If IsNumeric(sEditedValue) Then
                        If sEditedValue <> oRow.Cells(e.ColumnIndex).Value.ToString Then
                            mDirtyCell = True
                        End If
                    End If
            End Select
        End If

    End Sub

    Private Sub MatDataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MatDataGridView1.CellValidated
        If mAllowEvents Then
            If mDirtyCell Then
                Select Case e.ColumnIndex
                    Case Cols.sortida, Cols.pendent
                        Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                        CalculaImports(oRow)
                        RaiseEvent AfterUpdate(mTot, EventArgs.Empty)
                        mDirtyQty = False
                    Case Cols.preu
                        Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                        Dim DcVal As Decimal = oRow.Cells(e.ColumnIndex).Value
                        oRow.Cells(e.ColumnIndex).Value = New Amt(DcVal)
                        CalculaImports(oRow)
                        RaiseEvent AfterUpdate(mTot, EventArgs.Empty)
                    Case Cols.discount
                        Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                        CalculaImports(oRow)
                        RaiseEvent AfterUpdate(mTot, EventArgs.Empty)
                End Select
                mDirtyCell = False
            End If
        End If
    End Sub

    Private Sub CalculaImports(ByVal oRow As DataGridViewRow)
        Dim RowIdx As Integer = oRow.Cells(Cols.idx).Value
        Dim oLineaDeAlbaran As LineaDeAlbaran = mLineasDeAlbaran(RowIdx)
        With oLineaDeAlbaran
            .Qty = oRow.Cells(Cols.sortida).Value
            .Preu = oRow.Cells(Cols.preu).Value
            .Dto = oRow.Cells(Cols.discount).Value
            oRow.Cells(Cols.import).Value = .Amt.Val
        End With

        If mAlb.IsNew Then
            If mAlb.Client.IncentiusEnabled And mAlb.Cod = DTOPurchaseOrder.Codis.client Then
                'set incentius
                If mDirtyQty Then
                    mLineasDeAlbaran.SetIncentius()
                    For Each oRow In MatDataGridView1.Rows
                        If oRow.Cells(Cols.linCod).Value = LinCods.Itm Then
                            Dim Idx As Integer = oRow.Cells(Cols.idx).Value
                            If mLineasDeAlbaran(Idx).Dto < mLineasDeAlbaran(Idx).Pnc.dto Then
                                mLineasDeAlbaran(Idx).Dto = mLineasDeAlbaran(Idx).Pnc.dto
                            End If
                            oRow.Cells(Cols.discount).Value = mLineasDeAlbaran(Idx).Dto
                        End If
                    Next
                End If
            End If
        End If

        SetTotals()
    End Sub

    Private Sub MatDataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MatDataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu(MatDataGridView1.CurrentRow)
        End If
    End Sub

    Private Sub SetContextMenu(ByVal oRow As DataGridViewRow)
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        If getLincod(oRow) = LinCods.Itm Then
            Dim oLineaDeAlbaran As LineaDeAlbaran = GetLineaDeAlbaran(oRow)
            Dim oArt As Art = oLineaDeAlbaran.Art
            If oArt IsNot Nothing Then
                Dim oMenu_Art As New Menu_Art(oArt)
                oMenuItem = New ToolStripMenuItem("article...")
                oMenuItem.DropDownItems.AddRange(oMenu_Art.Range)
                oContextMenu.Items.Add(oMenuItem)
            End If

            Dim BlSpv As Boolean = False
            Dim oSpv As spv = oLineaDeAlbaran.Spv
            If oSpv IsNot Nothing Then
                BlSpv = oSpv.Id > 0
            End If

            If BlSpv Then
                Dim oMenu_Spv As New Menu_Spv(oSpv)
                oMenuItem = New ToolStripMenuItem("reparació...")
                oMenuItem.DropDownItems.AddRange(oMenu_Spv.Range)
                oContextMenu.Items.Add(oMenuItem)
            Else
                Dim oPdc As Pdc = GetPdc(oRow)
                Dim oMenu_Pdc As New Menu_Pdc(oPdc)
                oMenuItem = New ToolStripMenuItem("comanda...")
                oMenuItem.DropDownItems.AddRange(oMenu_Pdc.Range)
                oContextMenu.Items.Add(oMenuItem)

            End If

            oContextMenu.Items.Add(mMenuItemEditPreus)

            oMenuItem = New ToolStripMenuItem("excel")
            AddHandler oMenuItem.Click, AddressOf Do_Excel
            oContextMenu.Items.Add(oMenuItem)

        End If

        MatDataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_EditPreus(ByVal sender As Object, ByVal e As System.EventArgs)
        mMenuItemEditPreus.Checked = Not mMenuItemEditPreus.Checked
        With MatDataGridView1
            .Columns(Cols.preu).ReadOnly = Not mMenuItemEditPreus.Checked
            .Columns(Cols.discount).ReadOnly = Not mMenuItemEditPreus.Checked
        End With
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataGridView(MatDataGridView1).Visible = True
    End Sub

    Private Function getLincod(ByVal oRow As DataGridViewRow) As LinCods
        Dim RetVal As LinCods = CType(oRow.Cells(Cols.linCod).Value, LinCods)
        Return RetVal
    End Function

    Private Function GetPdc(ByVal oRow As DataGridViewRow) As Pdc
        Dim idx As Integer = CInt(oRow.Cells(Cols.idx).Value)
        Dim oItem As LineaDeAlbaran = mLineasDeAlbaran(idx)
        Dim RetVal As Pdc = oItem.Pnc.Pdc
        Return RetVal
    End Function

    Private Function GetPnc(ByVal oRow As DataGridViewRow) As LineItmPnc
        Dim idx As Integer = CInt(oRow.Cells(Cols.idx).Value)
        Dim oItem As LineaDeAlbaran = mLineasDeAlbaran(idx)
        Dim RetVal As LineItmPnc = oItem.Pnc
        Return RetVal
    End Function

    Private Function GetLineaDeAlbaran(oRow As DataGridViewRow) As LineaDeAlbaran
        Dim retval As LineaDeAlbaran = Nothing
        Dim idx As Integer = CInt(oRow.Cells(Cols.idx).Value)
        retval = mLineasDeAlbaran(idx)
        Return retval
    End Function

    Private Function GetWarnIcon(ByVal oRow As DataGridViewRow) As Image
        Dim oImage As Image = My.Resources.empty
        Dim oLinCod As LinCods = getLincod(oRow)
        If oLinCod = LinCods.Itm Then
            Dim oLineaDeAlbaran As LineaDeAlbaran = GetLineaDeAlbaran(oRow)
            Dim oArt As Art = oLineaDeAlbaran.Art
            Dim iQty As Integer = oLineaDeAlbaran.Qty
            If Not oArt.Dimensions.AllowedOrderQty(iQty) Then
                oImage = My.Resources.warn
            Else
                Dim oPdc As Pdc = GetPdc(oRow)
                If oPdc.FchMin > Today Then
                    oImage = My.Resources.Outlook_16
                Else
                    If oPdc.TotJunt Then oImage = My.Resources.clip
                End If
            End If
        End If
        Return oImage
    End Function



    Private Sub MatDataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MatDataGridView1.KeyPress
        'ho crida MatDataGridView1_EditingControlShowing
        Select Case MatDataGridView1.CurrentCell.ColumnIndex
            Case Cols.preu
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub

    Private Sub MatDataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles MatDataGridView1.EditingControlShowing
        'fa que funcioni KeyPress per DataGridViews
        If TypeOf e.Control Is TextBox Then
            Dim oControl As TextBox = CType(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf MatDataGridView1_KeyPress
        End If
    End Sub

End Class

Public Class LineaDeAlbaran
    Inherits LineItmArc
    Property Stock As Integer
    Property Clients As Integer

    Public Enum Iconos
        NotSet
        Warning
        Outlook
        ServirTotJunt
    End Enum

    Public Function Icono() As Iconos
        Dim retval As Iconos = Iconos.NotSet
        Dim iQty As Integer = MyBase.Qty
        If Not MyBase.Art.Dimensions.AllowedOrderQty(iQty) Then
            retval = Iconos.Warning
        Else
            If MyBase.Pnc IsNot Nothing Then
                Dim oPdc As Pdc = MyBase.Pnc.Pdc
                If oPdc.FchMin > Today Then
                    retval = Iconos.Outlook
                Else
                    If oPdc.TotJunt Then
                        retval = Iconos.ServirTotJunt
                    End If
                End If
            End If
            End If
            Return retval
    End Function

    Shared Function fromLineItmArc(oItm As LineItmArc) As LineaDeAlbaran
        Dim retval As New LineaDeAlbaran
        With retval
            .Pnc = oItm.Pnc
            .Qty = oItm.Qty
            .Preu = oItm.Preu
            .Dto = oItm.Dto
            .RepCom = oItm.RepCom
            .Art = oItm.Art
            .Spv = oItm.Spv
            .Lin = oItm.Lin
            .IvaCod = oItm.IvaCod
            .RepComLiquidable = oItm.RepComLiquidable
        End With

        Return retval
    End Function
End Class

Public Class LineasDeAlbaran
    Inherits System.Collections.CollectionBase

    Public Sub Add(ByVal NewObjMember As LineaDeAlbaran)
        List.Add(NewObjMember)
    End Sub

    Public Sub Remove(ByVal index As Integer)
        If index > Count - 1 Or index < 0 Then
            Exit Sub
        End If
        List.RemoveAt(index)
    End Sub

    Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As LineaDeAlbaran
        Get
            Item = List.Item(vntIndexKey)
        End Get
    End Property

    Public Sub SetIncentius()
        If List.Count > 0 Then
            Dim oFirstItem As LineaDeAlbaran = List(0)
            Dim oFirstPdc As Pdc = oFirstItem.Pnc.Pdc
            Dim oClient As Client = oFirstPdc.Client
            'If oFirstItem.Alb.Cod = DTOPurchaseOrder.Codis.client Then
            SetIncentius(oClient)
            'End If
        End If
    End Sub

    Public Function SumaDeImports() As Amt
        Dim retval As New Amt
        For Each oitm As LineItmArc In List
            retval.Add(oitm.Amt)
        Next
        Return retval
    End Function

    Shared Function FromAlb(oAlb As Alb) As LineasDeAlbaran
        Dim retval As New LineasDeAlbaran
        For Each oItem As LineItmArc In oAlb.Itms
            Dim oLineaDeAlbaran As LineaDeAlbaran = LineaDeAlbaran.fromLineItmArc(oItem)
            retval.Add(oLineaDeAlbaran)
        Next
        Return retval
    End Function

    Shared Function FromClient(oClient As Client, oMgz As Mgz, oCod As DTOPurchaseOrder.Codis) As LineasDeAlbaran
        Dim retval As New LineasDeAlbaran

        Dim SQL As String = "SELECT PNC.PdcGuid,Pdc.Pdc,PNC.ArtGuid, " _
        & "PNC.Lin,PNC.Pn2,PNC.PTS,PNC.DTO,PNC.CARREC, " _
        & "PNC.RepGuid,PNC.COM,PNC.DTO,PNC.PN3 AS POT,  " _
        & "ART.ART,ART.MYD,ART.IVACOD2, ART.VIRTUAL,ART.NOMGZ, " _
        & "ART.Category,ART.HeredaDimensions,ART.InnerPack,ART.ForzarInnerPack, " _
        & "STP.Stp,STP.Brand,STP.InnerPack AS StpInnerPack,STP.ForzarInnerPack AS StpForzarInnerPack, " _
        & "TPA.Tpa, " _
        & "ArtStock.Stock, " _
        & "ArtPn2NoPn3.Qty AS Clients " _
        & "FROM PNC INNER JOIN PDC ON PNC.PdcGuid=PDC.Guid INNER JOIN  " _
        & "ART ON PNC.ArtGuid=ART.Guid INNER JOIN  " _
        & "STP ON ART.Category=STP.Guid INNER JOIN  " _
        & "TPA ON STP.Brand=TPA.Guid LEFT OUTER JOIN  " _
        & "ArtStock ON ArtStock.ArtGuid=ART.Guid AND ArtStock.MgzGuid='" & oMgz.Guid.ToString & "' LEFT OUTER JOIN " _
        & "ArtPn2NoPn3 ON ArtPn2NoPn3.ArtGuid=ART.Guid " _
        & "WHERE Pdc.CliGuid = '" & oClient.Guid.ToString & "' And PNC.PN2 <> 0 And PNC.COD =" & CInt(oCod) & " " _
        & "AND (TPA.CODSTK=1 OR ArtStock.Stock<>0)  " _
        & "ORDER BY Pdc.YEA, Pdc.PDC, PNC.LIN"

        Dim oDrd As SqlClient.SqlDataReader = GetDataReader(SQL, Databases.Maxi)
        Dim oLastPdc As New Pdc(Guid.NewGuid) ', oClient.Emp, 0)
        oLastPdc.Client = oClient
        oLastPdc.Cod = oCod

        Do While oDrd.Read
            Dim oPdcGuid As Guid = oDrd("PdcGuid")
            If Not oPdcGuid.Equals(oLastPdc.Guid) Then
                oLastPdc = New Pdc(oPdcGuid) ', oClient.Emp, CInt(oDrd("Pdc")))
                oLastPdc.Client = oClient
                oLastPdc.Cod = oCod
            End If

            Dim oTpa As New Tpa(CType(oDrd("Brand"), Guid), oClient.Emp, CInt(oDrd("Tpa"))) 'while incentius no es gestiona per Guids

            Dim oStpDimensions As New ArtDimensions
            With oStpDimensions
                .InnerPack = CInt(oDrd("StpInnerPack"))
                .ForzarInnerPack = CInt(oDrd("StpForzarInnerPack"))
            End With

            Dim oStp As New Stp(CType(oDrd("Category"), Guid))
            With oStp
                .Id = CInt(oDrd("Stp")) 'while incentius no es gestiona per Guids
                .Tpa = oTpa
                .Dimensions = oStpDimensions
            End With

            Dim oArtDimensions As New ArtDimensions
            With oArtDimensions
                .Hereda = CInt(oDrd("HeredaDimensions"))
                .InnerPack = CInt(oDrd("InnerPack"))
                .ForzarInnerPack = CInt(oDrd("ForzarInnerPack"))
            End With

            Dim oArtGuid As Guid = oDrd("ArtGuid")
            Dim oArt As New Art(oArtGuid)
            With oArt
                .Stp = oStp
                .Emp = oClient.Emp
                .Id = CInt(oDrd("Art"))
                .Nom_ESP = oDrd("Myd")
                .IVAcod = oDrd("IvaCod2")
                .Virtual = oDrd("Virtual")
                .NoMgz = oDrd("NoMgz")
                .Dimensions = oArtDimensions
            End With

            Dim oLineItmPnc As New LineItmPnc(oLastPdc, oDrd("Lin"))
            With oLineItmPnc
                .Art = oArt
                .Pendent = CInt(oDrd("Pn2"))
                .Preu = New Amt(CDec(oDrd("Pts")))
                .dto = oDrd("Dto")
                If Not IsDBNull(oDrd("RepGuid")) Then
                    Dim oRepGuid As Guid = oDrd("RepGuid")
                    .RepCom = New RepCom(New Rep(oRepGuid), CDec(oDrd("Com")))
                End If
                .Lin = oDrd("Lin")
            End With

            Dim iStock As Integer
            If IsDBNull(oDrd("Stock")) Then
                iStock = 0
            Else
                iStock = CInt(oDrd("Stock"))
            End If

            Dim iClients As Integer
            If IsDBNull(oDrd("Clients")) Then
                iClients = 0
            Else
                iClients = CInt(oDrd("Clients"))
            End If

            Dim BlPot As MaxiSrvr.root.TriState = IIf(CBool(oDrd("Pot")), MaxiSrvr.root.TriState.Verdadero, MaxiSrvr.root.TriState.Falso)
            Dim oItem As New LineaDeAlbaran
            With oItem
                .Pnc = oLineItmPnc
                .RepCom = .Pnc.RepCom
                .Art = oArt
                If oCod = DTOPurchaseOrder.Codis.Client Then
                    .Qty = .SuggestedSortida(iStock, BlPot)
                End If
                .Preu = .Pnc.Preu
                .Dto = .Pnc.dto
                .Stock = iStock
                .Clients = iClients
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Public Function SetIncentius(ByVal oClient As Client) As Boolean
        Dim BlRetVal As Boolean = False

        Dim oItmIncentius As Incentius = Nothing
        If oClient.IncentiusEnabled Then
            Dim oAlbIncentius As New Incentius
            Dim oAlbIncentiu As Incentiu = Nothing
            Dim oItm As LineItmArc
            Dim DcDto As Decimal

            'assigna les quantitats de cada oferta
            For Each oItm In List
                oItm.Incentius = New Product(oItm.Art).Incentius(Incentiu.Cods.Dto, Today)
                If oItm.Incentius.Count > 0 Then
                    Dim BlFoundInAlbIncentius As Boolean = False
                    For Each oItmIncentiu As Incentiu In oItm.Incentius
                        BlFoundInAlbIncentius = False
                        For Each oAlbIncentiu In oAlbIncentius
                            If oItmIncentiu.Equals(oAlbIncentiu) Then
                                BlFoundInAlbIncentius = True
                                Exit For
                            End If
                        Next

                        If Not BlFoundInAlbIncentius Then
                            oAlbIncentiu = oItmIncentiu
                            oAlbIncentius.Add(oAlbIncentiu)
                        End If

                        oAlbIncentiu.Unitats += oItm.Qty
                    Next
                End If
            Next


            For Each oItm In List

                'assigna la quantitat mès alta de les ofertes en que participa cada linia
                Dim iQty As Integer = 0
                For Each oItmIncentiu As Incentiu In oItm.Incentius
                    For Each oAlbIncentiu In oAlbIncentius
                        If oAlbIncentiu.Equals(oItmIncentiu) Then
                            If oAlbIncentiu.Unitats > iQty Then
                                iQty = oAlbIncentiu.Unitats
                            End If
                            Exit For
                        End If
                    Next
                Next

                'assigna el descompte de la oferta mes favorable que li toca a cada linea
                Dim DcArcDto As Decimal = oItm.Pnc.dto
                Dim oAssignedIncentiu As Incentiu = Nothing
                For Each oItmIncentiu As Incentiu In oItm.Incentius
                    DcDto = oItmIncentiu.GetDto(iQty)
                    If DcDto > DcArcDto Then
                        DcArcDto = DcDto
                        oAssignedIncentiu = oItmIncentiu
                        BlRetVal = True
                    End If
                Next
                oItm.Dto = DcArcDto
                oItm.Incentiu = oAssignedIncentiu
            Next

        End If

        Return BlRetVal
    End Function

    Public Function NonEmptyLineItmArcs() As LineItmArcs
        Dim retval As New LineItmArcs
        For Each oLineaDeAlbaran As LineaDeAlbaran In List
            If oLineaDeAlbaran.Qty <> 0 Then
                retval.Add(oLineaDeAlbaran)
            End If
        Next
        Return retval
    End Function
End Class


