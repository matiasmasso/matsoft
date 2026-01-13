Public Class Xl_Alb_lineItems

    Private _Delivery As DTODelivery
    Private mAlb As Alb = Nothing

    Private mFch As Date = Date.MinValue

    Private BCOLORBLANK As Color = Color.FromArgb(240, 240, 240)
    Private BCOLORPDC As Color = Color.FromArgb(153, 255, 255)
    Private BCOLORSPV As Color = Color.FromArgb(224, 255, 255)
    Private BCOLOROBS As Color = Color.FromArgb(255, 255, 153)
    Private BCOLORITM As Color = Color.FromArgb(250, 250, 250)

    Private mTb As DataTable = Nothing
    Private mSuma As DTOAmt
    Private mTot As DTOAmt
    Private mMenuItemEditPreus As New ToolStripMenuItem("editar preus i descomptes", Nothing, AddressOf Do_EditPreus)

    Private mLineasDeAlbaran As LineasDeAlbaran
    Private _LineasDeAlbaran As List(Of DTODeliveryItem)

    'Private mFlagTabThrough As Boolean = True
    Private mDirtyCell As Boolean = False
    Private mDirtyQty As Boolean = False
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
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

    Public Shadows Sub Load(oDelivery As DTODelivery)
        _Delivery = oDelivery
        mFch = _Delivery.Fch

        setDataSource()
        SetGridColumns()
        LoadGrid()
        SetTotals() 'ToDTO
        mAllowEvents = True

    End Sub


    Public WriteOnly Property Alb() As Alb
        Set(ByVal value As Alb)
            mAlb = value
            mFch = mAlb.Fch

            _Delivery = mAlb.ToDTO

            setDataSource()
            SetGridColumnsOld()
            LoadGridOld()
            SetTotalsOld()
            mAllowEvents = True
        End Set
    End Property


    Public ReadOnly Property Items() As List(Of DTODeliveryItem)
        Get
            Dim retval As New List(Of DTODeliveryItem)
            For Each mItem As LineItmArc In mLineasDeAlbaran.NonEmptyLineItmArcs
                Dim item As DTODeliveryItem = mItem.ToDTO
                retval.Add(item)
            Next

            Return retval
        End Get
    End Property

    Public ReadOnly Property OldItems() As LineItmArcs
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
        SetTotalsOld()
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
        Dim oCustomer = _Delivery.Contact
        Dim oLang As DTOLang = oCustomer.Lang

        Dim oTotal As DTOAmt = New DTOAmt(_LineasDeAlbaran.Sum(Function(x) x.Import.Eur))
        Dim sb As New System.Text.StringBuilder
        sb.Append(oTotal.CurFormatted)

        If BLLDelivery.ExportCod(_Delivery) <> DTOInvoice.ExportCods.Nacional Or _Delivery.IvaExempt Then
            sb.Append(oLang.Tradueix(" (exento de IVA)", " (exempt de IVA)", " (VAT n/a)"))
        Else
            Dim oBaseImponible As DTOAmt = oTotal.Clone
            Dim DcIvaTipus As Decimal = BLLDelivery.IvaTipus(_Delivery)
            If DcIvaTipus <> 0 Then
                Dim oIvaAmt = BLLDelivery.IvaAmt(_Delivery)
                sb.Append(" +IVA " & DcIvaTipus.ToString & "% " & oIvaAmt.Formatted)
                oTotal.Add(oIvaAmt)

                Dim DcIvaReq As Decimal = BLLDelivery.ReqTipus(_Delivery)
                If DcIvaReq <> 0 Then
                    Dim oReqAmt = BLLDelivery.ReqAmt(_Delivery)
                    sb.Append(" +Rec.equival. " & DcIvaReq.ToString & "% " & oReqAmt.Formatted)
                    oTotal.Add(oReqAmt)
                End If
            End If

            sb.Append(" total " & oTotal.CurFormatted)
        End If

        mTot = oTotal

        LabelTotals.Text = sb.ToString
    End Sub


    Private Sub SetTotalsOld()
        Dim oClient As Client = mAlb.Client
        Dim oLang As DTOLang = oClient.Lang

        Dim oTotal As DTOAmt = mLineasDeAlbaran.SumaDeImports
        Dim sb As New System.Text.StringBuilder
        sb.Append(oTotal.CurFormatted)

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
            Dim oBaseImponible As DTOAmt = oTotal.Clone

            If oClient.IVA Then

                Dim oItms As LineItmArcs = mLineasDeAlbaran.NonEmptyLineItmArcs
                Dim oIvaBaseQuotas = oItms.IvaBaseQuotas(Not oClient.IVA, oClient.REQ)
                For Each oIvaBaseQuota In oIvaBaseQuotas
                    Select Case oIvaBaseQuota.Tax.Codi
                        Case DTOTax.Codis.Iva_Standard, DTOTax.Codis.Iva_Reduit, DTOTax.Codis.Iva_SuperReduit
                            sb.Append(" +")
                            sb.Append(oIvaBaseQuota.Tax.Tipus.ToString & "% ")
                            sb.Append(BLLTax.Nom(oIvaBaseQuota.Tax.Codi, oLang))
                            sb.Append(" ")

                            If Not oIvaBaseQuota.Base.Equals(oBaseImponible) Then
                                sb.Append("s/" & oIvaBaseQuota.Base.Formatted)
                            End If

                            oTotal.Add(BLLTaxBaseQuota.Quota(oIvaBaseQuota))
                    End Select
                Next

                For Each oIvaBaseQuota In oIvaBaseQuotas
                    Select Case oIvaBaseQuota.Tax.Codi
                        Case DTOTax.Codis.Recarrec_Equivalencia_Standard, DTOTax.Codis.Recarrec_Equivalencia_Reduit, DTOTax.Codis.Recarrec_Equivalencia_SuperReduit
                            sb.Append(" +")
                            sb.Append(oIvaBaseQuota.Tax.Tipus.ToString & "% ")
                            sb.Append(BLLTax.Nom(oIvaBaseQuota.Tax.Codi, oLang))
                            sb.Append(" ")

                            If Not oIvaBaseQuota.Base.Equals(oBaseImponible) Then
                                sb.Append("s/" & oIvaBaseQuota.Base.Formatted)
                            End If

                            oTotal.Add(BLLTaxBaseQuota.Quota(oIvaBaseQuota))
                    End Select
                Next

            End If

            sb.Append(" total " & oTotal.CurFormatted)
        End If

        mTot = oTotal

        LabelTotals.Text = sb.ToString
    End Sub



    Private Sub setDataSource()
        mTb = New DataTable
        With mTb.Columns
            .Add(New DataColumn("Guid", Type.GetType("System.Guid")))
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
        Dim oLastPdc As New DTOPurchaseOrder
        Dim oLastSpv As New DTOSpv
        Dim FirstRow As Boolean = True

        If _Delivery.IsNew Then
            _LineasDeAlbaran = BLLDeliveryItems.Factory(_Delivery.Contact) '  LineasDeAlbaran.FromClient(mAlb.Client, mAlb.Mgz, mAlb.Cod)
            If _Delivery.Cod = DTOPurchaseOrder.Codis.Client Then
                BLLDeliveryItems.setIncentius(_LineasDeAlbaran)
            End If
        Else
            mLineasDeAlbaran = LineasDeAlbaran.FromAlb(mAlb)
            LabelUser.Text = mAlb.UserText
        End If

        For idx As Integer = 0 To _LineasDeAlbaran.Count - 1

            Dim oItem As DTODeliveryItem = _LineasDeAlbaran(idx)

            Select Case _Delivery.Cod
                Case DTOPurchaseOrder.Codis.Reparacio
                    Dim oSpv As DTOSpv = SpvFromIdx(idx)
                    If oSpv IsNot Nothing Then
                        If Not oSpv.Guid.Equals(oLastSpv.Guid) Then
                            oLastSpv = oSpv
                            LoadRowSpv(idx)
                        End If
                    End If

                Case Else
                    If Not oLastPdc.Equals(oItem.PurchaseOrderItem.PurchaseOrder) Then
                        oLastPdc = oItem.PurchaseOrderItem.PurchaseOrder
                        If Not FirstRow Then LoadRow(idx, LinCods.Blank)
                        FirstRow = False
                        LoadRowPdc(idx)
                    End If
            End Select

            LoadRowItm(idx)
            FirstRow = False
        Next


    End Sub

    Private Sub LoadGridOld()
        Dim oLastPdc As New Pdc(Guid.NewGuid)
        Dim oLastSpv As New DTOSpv(Guid.NewGuid)
        Dim FirstRow As Boolean = True

        If mAlb.IsNew Then
            mLineasDeAlbaran = LineasDeAlbaran.FromClient(mAlb.Client, mAlb.Mgz, mAlb.Cod)
            If mAlb.Cod = DTOPurchaseOrder.Codis.Client Then
                mLineasDeAlbaran.SetIncentius()
            End If
            LabelUser.Text = mAlb.UserText
        Else
            mLineasDeAlbaran = LineasDeAlbaran.FromAlb(mAlb)
            LabelUser.Text = mAlb.UserText
        End If

        For idx As Integer = 0 To mLineasDeAlbaran.Count - 1

            Dim oItem As LineaDeAlbaran = mLineasDeAlbaran(idx)

            Select Case mAlb.Cod
                Case DTOPurchaseOrder.Codis.Reparacio
                    Dim oSpv As DTOSpv = SpvFromIdx(idx)
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

            LoadRowItmOld(idx)
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
            With .Columns(Cols.Guid)
                .Visible = False
            End With
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
                If _Delivery.Cod = DTOPurchaseOrder.Codis.Proveidor Then
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
                If _Delivery.IsNew Then
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
                If _Delivery.IsNew Then
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
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
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
                If _Delivery.IsNew Then
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
                If _Delivery.IsNew Then
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

    Private Sub SetGridColumnsOld() 'To Deprecate

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
            With .Columns(Cols.Guid)
                .Visible = False
            End With
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
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
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
        Dim oitm = _LineasDeAlbaran(idx)
        Dim oRow As DataGridViewRow = LoadRow(idx, LinCods.Itm, oitm.Sku.NomLlarg)
        With oRow
            .Cells(Cols.artId).Value = oitm.Sku.Id
            If oitm.PurchaseOrderItem IsNot Nothing Then
                .Cells(Cols.pendent).Value = oitm.PurchaseOrderItem.Pending
            End If
            .Cells(Cols.concepte).Value = oitm.Sku.NomLlarg
            ' .Cells(Cols.icono).Value = oitm.Icono
            .Cells(Cols.sortida).Value = oitm.Qty
            .Cells(Cols.import).Value = oitm.Import.Eur
            .Cells(Cols.stock).Value = oitm.Sku.Stock
            .Cells(Cols.clients).Value = oitm.Sku.Clients
            .ReadOnly = False
            .Cells(Cols.preu).Value = oitm.Price.Eur
            .Cells(Cols.discount).Value = oitm.Dto
        End With
    End Sub
    Private Sub LoadRowItmOld(idx As Integer)
        Dim oitm As LineaDeAlbaran = mLineasDeAlbaran(idx)
        Dim oRow As DataGridViewRow = LoadRow(idx, LinCods.Itm, oitm.Sku.NomLlarg)
        With oRow
            .Cells(Cols.artId).Value = oitm.Sku.Id
            If oitm.Pnc IsNot Nothing Then
                .Cells(Cols.Guid).Value = oitm.Pnc.Guid
                .Cells(Cols.pendent).Value = oitm.Pnc.Pendent
            End If
            .Cells(Cols.concepte).Value = oitm.Sku.NomLlarg
            .Cells(Cols.icono).Value = oitm.Icono
            .Cells(Cols.sortida).Value = oitm.Qty
            .Cells(Cols.preu).Value = oitm.Preu
            .Cells(Cols.discount).Value = oitm.Dto
            .Cells(Cols.import).Value = oitm.Amt
            .Cells(Cols.stock).Value = oitm.Stock
            .Cells(Cols.clients).Value = oitm.Clients
            .ReadOnly = False
        End With
    End Sub

    Private Function OldSuggestedQty(item As LineaDeAlbaran) As Integer
        Dim retval As Integer = 0
        If mAlb.IsNew Then
            If mAlb.Cod = DTOPurchaseOrder.Codis.Client Then
                Dim DtFchMin As Date = item.Pnc.Pdc.FchMin
                If DtFchMin = Nothing Then
                    retval = item.Qty
                Else
                    Dim timeGap As Integer = (DtFchMin - Now).Days
                    If timeGap < 7 Then
                        retval = item.Qty
                    End If
                End If
            Else
                retval = item.Qty
            End If
        Else
            retval = item.Qty
        End If

        Return retval
    End Function


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

    Private Function SpvFromIdx(idx As Integer) As DTOSpv
        Dim oLineaDeAlbaran As LineaDeAlbaran = LineaDeAlbaranFromIdx(idx)
        Dim retval As DTOSpv = oLineaDeAlbaran.Spv
        Return retval
    End Function


    Private Sub LoadRowPdc(idx As Integer)
        Dim oPdc As Pdc = PdcFromIdx(idx)
        Dim oRow As DataGridViewRow = LoadRow(idx, LinCods.Pdc, oPdc.FullConcepte)
        If oPdc.Obs > "" Then LoadRowObs(idx, oPdc.Obs)

        If mAlb.IsNew Then
            If mAlb.Cod = DTOPurchaseOrder.Codis.Client Then
                If oPdc.FchMin > Today Then
                    LoadRowObs(idx, "ATENCIO: No servir abans de " & oPdc.FchMin.ToShortDateString)
                End If
            End If

            Dim oPaymentTerms As DTOPaymentTerms
            Select Case _Delivery.Cod
                Case DTOPurchaseOrder.Codis.Proveidor
                    oPaymentTerms = _Delivery.Proveidor.PaymentTerms
                Case Else
                    oPaymentTerms = _Delivery.Customer.PaymentTerms
            End Select

            If Not BLLPaymentTerms.Match(oPaymentTerms, oPdc.Fpg) Then
                LoadRowObs(idx, "Condicions sespecials de pagament")
            End If

        End If

    End Sub

    Private Sub LoadRowObs(idx As Integer, ByVal sObs As String)
        Dim oRow As DataGridViewRow = LoadRow(idx, LinCods.Obs, sObs)
    End Sub

    Private Sub LoadRowSpv(idx As Integer)
        Dim oRow As DataGridViewRow = Nothing
        Dim oSpv As DTOSpv = SpvFromIdx(idx)
        Dim oLang As DTOLang = mAlb.Client.Lang
        Dim oSpvTextArray = BLLSpv.Lines(oSpv, oLang)
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
                        If mAlb.IsNew And mAlb.Cod = DTOPurchaseOrder.Codis.Client Then
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
                                    e.CellStyle.BackColor = DTOProductSku.COLOR_STOCK
                                ElseIf iStk > 0 Then
                                    e.CellStyle.BackColor = DTOProductSku.COLOR_RESERVED
                                Else
                                    e.CellStyle.BackColor = DTOProductSku.COLOR_NOSTOCK
                                End If
                            End If

                        End If
                End Select
            Case Cols.icono
                If mAlb.IsNew And mAlb.Cod = DTOPurchaseOrder.Codis.Client Then
                    Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                    e.Value = GetWarnIcon(oRow)
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.concepte
                If mAlb.Cod = DTOPurchaseOrder.Codis.Reparacio Then
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

            Case Cols.preu, Cols.import
                Dim oRow As DataGridViewRow = MatDataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols.linCod).Value, LinCods)
                    Case LinCods.Itm
                        Dim oAmt As DTOAmt = e.Value
                        e.Value = oAmt.CurFormatted
                End Select
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
                        If TypeOf oRow.Cells(e.ColumnIndex).Value Is System.String Then
                            Dim sVal As String = oRow.Cells(e.ColumnIndex).Value
                            Dim oAmt As DTOAmt = Nothing
                            If IsNumeric(sVal) Then

                                Dim DcVal As Decimal = Decimal.Parse(sVal, Globalization.NumberStyles.Currency)
                                oAmt = BLLApp.GetAmt(DcVal)
                            Else
                                oAmt = BLLApp.EmptyAmt
                            End If
                            oRow.Cells(e.ColumnIndex).Value = oAmt
                            CalculaImports(oRow)
                            RaiseEvent AfterUpdate(mTot, EventArgs.Empty)
                        End If
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
            oRow.Cells(Cols.import).Value = .Amt ' .Amt.Val
        End With

        If mAlb.IsNew Then
            If mAlb.Client.IncentiusEnabled And mAlb.Cod = DTOPurchaseOrder.Codis.Client Then
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

        SetTotalsOld()
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
            If oLineaDeAlbaran.Sku IsNot Nothing Then
                Dim oMenu_Sku As New Menu_ProductSku(oLineaDeAlbaran.Sku)
                oMenuItem = New ToolStripMenuItem("article...")
                oMenuItem.DropDownItems.AddRange(oMenu_Sku.Range)
                oContextMenu.Items.Add(oMenuItem)
            End If

            Dim BlSpv As Boolean = False
            Dim oSpv As DTOSpv = oLineaDeAlbaran.Spv
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

            oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel)
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
        Dim oSheet As DTOExcelSheet = BLLDelivery.Excel(_Delivery)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
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
            Dim iQty As Integer = oLineaDeAlbaran.Qty
            If BLLProductSku.IsAllowedOrderQty(oLineaDeAlbaran.Sku, iQty, BLLSession.Current.User) Then
                Dim oPdc As Pdc = GetPdc(oRow)
                If oPdc.FchMin > Today Then
                    oImage = My.Resources.Outlook_16
                Else
                    If oPdc.TotJunt Then oImage = My.Resources.clip
                End If
            Else
                oImage = My.Resources.warn
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
