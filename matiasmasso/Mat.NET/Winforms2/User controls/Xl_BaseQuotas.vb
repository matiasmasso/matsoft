Public Class Xl_BaseQuotas
    Inherits _Xl_ReadOnlyDatagridview

    Private _IvaLiquidacio As DTOIVALiquidacio
    Private _AllCtas As List(Of DTOPgcCta)

    Private _QuotaDevengada As Decimal
    Private _QuotaSoportada As Decimal

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        ico
        txt
        base
        tipus
        quota
    End Enum

    Public Shadows Sub Load(oIvaLiquidacio As DTOIVALiquidacio, oAllCtas As List(Of DTOPgcCta))
        Dim exs As New List(Of Exception)
        _AllCtas = oAllCtas
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If
        _IvaLiquidacio = oIvaLiquidacio
        Refresca()

    End Sub


    Private Sub Refresca()
        _AllowEvents = False
        Dim DcQuotaDevengada As Decimal

        _ControlItems = New ControlItems
        Dim oControlItem As New ControlItem(ControlItem.linSrcs.devengat, "Iva devengat")
        _ControlItems.Add(oControlItem)


        For Each oItem In _IvaLiquidacio.Items.Where(Function(x) x.Cod = DTOIVALiquidacio.Item.Cods.Repercutit)
            oControlItem = New ControlItem(ControlItem.linSrcs.repercutit, "IVA repercutit", oItem.Quota.Eur, oItem.Tipus, oItem.Base.Eur, oItem.Saldo.Eur)
            _ControlItems.Add(oControlItem)
            DcQuotaDevengada += oItem.Quota.eur
        Next

        Dim oIntraComunitari = _IvaLiquidacio.Items.FirstOrDefault(Function(x) x.Cod = DTOIVALiquidacio.Item.Cods.IntraComunitari)
        If oIntraComunitari IsNot Nothing Then
            oControlItem = New ControlItem(ControlItem.linSrcs.intracomunitari, "Adquisicions intracomunitaries de bens i serveis", oIntraComunitari.Quota.Eur, 0, oIntraComunitari.Base.Eur, oIntraComunitari.Saldo.Eur)
            _ControlItems.Add(oControlItem)
            DcQuotaDevengada += oIntraComunitari.Quota.eur
        End If


        For Each oItem In _IvaLiquidacio.Items.Where(Function(x) x.Cod = DTOIVALiquidacio.Item.Cods.RecarrecEquivalencia)
            oControlItem = New ControlItem(ControlItem.linSrcs.recarrecEquivalencia, "Recàrrec d'equivaléncia", oItem.Quota.Eur, oItem.Tipus, oItem.Base.Eur, oItem.Saldo.Eur)
            _ControlItems.Add(oControlItem)
            DcQuotaDevengada += oItem.Quota.eur
        Next

        oControlItem = New ControlItem(ControlItem.linSrcs.totalDevengat, "Total quota devengada", DcQuotaDevengada)
        _ControlItems.Add(oControlItem)


        oControlItem = New ControlItem(ControlItem.linSrcs.blank)
        _ControlItems.Add(oControlItem)

        oControlItem = New ControlItem(ControlItem.linSrcs.deduible, "Iva deduïble")
        _ControlItems.Add(oControlItem)

        Dim DcQuotaDeduible As Decimal

        Dim oSoportat = _IvaLiquidacio.Items.FirstOrDefault(Function(x) x.Cod = DTOIVALiquidacio.Item.Cods.SoportatNacional)
        oControlItem = New ControlItem(ControlItem.linSrcs.soportatInterior, "Per quotes soportades en ops.interiors corrents", oSoportat.Quota.Eur, 0, oSoportat.Base.Eur, oSoportat.Saldo.Eur)
        _ControlItems.Add(oControlItem)
        DcQuotaDeduible += oSoportat.Quota.eur


        Dim oImportacions = _IvaLiquidacio.Items.FirstOrDefault(Function(x) x.Cod = DTOIVALiquidacio.Item.Cods.Importacions)
        oControlItem = New ControlItem(ControlItem.linSrcs.soportatImportacions, "Per quotes soportades en importacions de bens corrents", oImportacions.Quota.Eur, 0, oImportacions.Base.Eur, oImportacions.Saldo.Eur)
        _ControlItems.Add(oControlItem)
        DcQuotaDeduible += oImportacions.Quota.eur



        oControlItem = New ControlItem(ControlItem.linSrcs.soportatIntracomunitari, "Per quotes soportades en adquisicions intracomunitaries", oIntraComunitari.Quota.Eur, 0, oIntraComunitari.Base.Eur, oIntraComunitari.Saldo.Eur)
        _ControlItems.Add(oControlItem)
        DcQuotaDeduible += oIntraComunitari.Quota.eur


        oControlItem = New ControlItem(ControlItem.linSrcs.totalDeduible, "Total a deduïr", DcQuotaDeduible)
        _ControlItems.Add(oControlItem)

        oControlItem = New ControlItem(ControlItem.linSrcs.blank)
        _ControlItems.Add(oControlItem)

        oControlItem = New ControlItem(ControlItem.linSrcs.resultat, "Resultat", DcQuotaDevengada - DcQuotaDeduible)
        _ControlItems.Add(oControlItem)

        MyBase.DataSource = _ControlItems
        MyBase.CurrentCell = Nothing

        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.base)
            .HeaderText = "Base"
            .DataPropertyName = "Base"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.tipus)
            .HeaderText = "Tipus"
            .DataPropertyName = "Tipus"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.00\%;-#.00\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.quota)
            .HeaderText = "Quota"
            .DataPropertyName = "Quota"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function


    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Select Case oControlItem.linSrc
                Case ControlItem.linSrcs.repercutit
                    oContextMenu.Items.Add("llibre de factures emeses", My.Resources.Excel, AddressOf Do_LlibreFacturesEmesesNacional)
                    oContextMenu.Items.Add("extracte Iva repercutit", My.Resources.Excel, AddressOf Do_ExtracteRepercutit)
                Case ControlItem.linSrcs.recarrecEquivalencia
                    oContextMenu.Items.Add("llibre de factures emeses", My.Resources.Excel, AddressOf Do_LlibreFacturesEmesesNacional)
                    oContextMenu.Items.Add("extracte Recarrec Equivalencia", My.Resources.Excel, AddressOf Do_ExtracteReq)
                Case ControlItem.linSrcs.intracomunitari
                    oContextMenu.Items.Add("llibre de factures rebudes intracomunitaries", My.Resources.Excel, AddressOf Do_BookFrasSoportatIntraComunitari)
                    oContextMenu.Items.Add("llibre de factures rebudes", My.Resources.Excel, AddressOf Do_Bookfras)
                    oContextMenu.Items.Add("extracte Iva repercutit Intracomunitari", My.Resources.Excel, AddressOf Do_ExtracteRepercutitIntraComunitari)
                Case ControlItem.linSrcs.soportatInterior
                    oContextMenu.Items.Add("llibre de factures rebudes nacionals", My.Resources.Excel, AddressOf Do_BookFrasSoportatInterior)
                    oContextMenu.Items.Add("llibre de factures rebudes", My.Resources.Excel, AddressOf Do_Bookfras)
                    oContextMenu.Items.Add("extracte Iva soportat nacional", My.Resources.Excel, AddressOf Do_ExtracteSoportatInterior)
                Case ControlItem.linSrcs.soportatIntracomunitari
                    oContextMenu.Items.Add("llibre de factures rebudes IntraComunitaries", My.Resources.Excel, AddressOf Do_BookFrasSoportatIntraComunitari)
                    oContextMenu.Items.Add("llibre de factures rebudes", My.Resources.Excel, AddressOf Do_Bookfras)
                    oContextMenu.Items.Add("extracte Iva soportat IntraComunitari", My.Resources.Excel, AddressOf Do_ExtracteSoportatIntraComunitari)
                Case ControlItem.linSrcs.soportatImportacions
                    oContextMenu.Items.Add("llibre de factures rebudes ExtraComunitaries", My.Resources.Excel, AddressOf Do_BookFrasSoportatExtraComunitari)
                    oContextMenu.Items.Add("llibre de factures rebudes", My.Resources.Excel, AddressOf Do_Bookfras)
                    oContextMenu.Items.Add("extracte Iva soportat ExtraComunitari", My.Resources.Excel, AddressOf Do_ExtracteSoportatExtraComunitari)
            End Select
            'Dim oMenu_Template As New Menu_Template(SelectedItems.First)
            'AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Template.Range)
            'oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ExtracteRepercutit()
        Do_Extracte(DTOPgcPlan.Ctas.IvaRepercutitNacional)
    End Sub

    Private Sub Do_ExtracteReq()
        Do_Extracte(DTOPgcPlan.Ctas.IvaRecarrecEquivalencia)
    End Sub

    Private Sub Do_ExtracteRepercutitIntraComunitari()
        Do_Extracte(DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari)
    End Sub

    Private Sub Do_ExtracteSoportatInterior()
        Do_Extracte(DTOPgcPlan.Ctas.IvaSoportatNacional)
    End Sub

    Private Sub Do_ExtracteSoportatIntraComunitari()
        Do_Extracte(DTOPgcPlan.Ctas.IvaSoportatIntracomunitari)
    End Sub

    Private Sub Do_ExtracteSoportatExtraComunitari()
        Do_Extracte(DTOPgcPlan.Ctas.IvaSoportatImportacio)
    End Sub

    Private Async Sub Do_Extracte(oCtaCod As DTOPgcPlan.Ctas)
        Dim exs As New List(Of Exception)

        Dim oCcbs = Await FEB.Ccbs.All(exs, _IvaLiquidacio.Exercici, DTOPgcCta.FromCodi(_AllCtas, oCtaCod),, _IvaLiquidacio.YearMonth.LastFch)
        oCcbs = oCcbs.OrderBy(Function(x) x.cca.cdn).OrderBy(Function(y) y.cca.ccd).OrderBy(Function(z) z.cca.fch).ToList

        Dim oSheet = FEB.Extracte.Excel(oCcbs, Current.Session.Lang)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_LlibreFacturesEmesesNacional()
        Dim exs As New List(Of Exception)
        Dim oAllInvoices = Await FEB.Invoices.All(exs, _IvaLiquidacio.Exercici, _IvaLiquidacio.Month)
        If exs.Count = 0 Then
            Dim oInvoices = oAllInvoices.
            Where(Function(x) x.ivaBaseQuotas.Any(Function(y) y.Tax.Codi = DTOTax.Codis.Iva_Standard Or y.Tax.Codi = DTOTax.Codis.Iva_Reduit Or y.Tax.Codi = DTOTax.Codis.Iva_SuperReduit)).
            ToList

            Dim sFilename As String = "M+O"
            Do_LlibreFacturesEmeses(oInvoices, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_LlibreFacturesEmeses(oInvoices As List(Of DTOInvoice), sFilename As String)
        'Dim oSheet As New MatHelper.Excel.Sheet(sFilename)
        Dim oSheet As New MatHelper.Excel.Sheet(sFilename)
        FEB.Invoices.LoadExcel(oSheet, oInvoices)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_BookFrasSoportatInterior()
        Dim exs As New List(Of Exception)
        Dim oAllBookFras = Await FEB.Bookfras.All(exs, DTOBookFra.Modes.All, _IvaLiquidacio.Exercici, _IvaLiquidacio.Month)
        If exs.Count = 0 Then
            Dim oBookFras = oAllBookFras.
            Where(Function(x) x.ClaveExenta = "").
            ToList

            Dim sFilename As String = String.Format("M+O {0}.{1} factures rebudes nacional", _IvaLiquidacio.YearMonth.year, _IvaLiquidacio.YearMonth.month)
            Do_BookFras(oBookFras, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_BookFrasSoportatIntraComunitari()
        Dim exs As New List(Of Exception)
        Dim oAllBookFras = Await FEB.Bookfras.All(exs, DTOBookFra.Modes.All, _IvaLiquidacio.Exercici, _IvaLiquidacio.Month)
        If exs.Count = 0 Then
            Dim oBookFras = oAllBookFras.
            Where(Function(x) x.ClaveExenta = "E5").
            ToList

            Dim sFilename As String = String.Format("M+O {0}.{1} factures rebudes intracomunitari", _IvaLiquidacio.YearMonth.year, _IvaLiquidacio.YearMonth.month)
            Do_BookFras(oBookFras, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_BookFrasSoportatExtraComunitari()
        Dim exs As New List(Of Exception)
        Dim oAllBookFras = Await FEB.Bookfras.All(exs, DTOBookFra.Modes.All, _IvaLiquidacio.Exercici, _IvaLiquidacio.Month)
        If exs.Count = 0 Then
            Dim oBookFras = oAllBookFras.
            Where(Function(x) x.ClaveExenta = "E2").
            ToList

            Dim sFilename As String = String.Format("M+O {0}.{1} factures rebudes extracomunitari", _IvaLiquidacio.YearMonth.year, _IvaLiquidacio.YearMonth.month)
            Do_BookFras(oBookFras, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Bookfras()
        Dim exs As New List(Of Exception)
        Dim oAllBookFras = Await FEB.Bookfras.All(exs, DTOBookFra.Modes.All, _IvaLiquidacio.Exercici, _IvaLiquidacio.Month)
        If exs.Count = 0 Then
            Dim sFilename As String = String.Format("M+O {0}.{1} factures rebudes", _IvaLiquidacio.YearMonth.year, _IvaLiquidacio.YearMonth.month)
            Do_BookFras(oAllBookFras, sFilename)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_BookFras(oBookFras As List(Of DTOBookFra), sFilename As String)
        Dim oSheet As New MatHelper.Excel.Sheet(sFilename)
        FEB.Bookfras.LoadExcel(oSheet, oBookFras)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_BaseQuotas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Validation <> -1 Then
                    If oControlItem.Quota = oControlItem.Validation Then
                        e.Value = My.Resources.vb
                    Else
                        e.Value = My.Resources.warning
                    End If
                End If
            Case Cols.txt
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.linCod
                    Case ControlItem.linCods.title
                        e.CellStyle.Font = New Drawing.Font(e.CellStyle.Font, FontStyle.Bold)
                    Case ControlItem.linCods.item
                        e.CellStyle.Padding = New Padding(20, 0, 0, 0)
                    Case ControlItem.linCods.sum
                        e.CellStyle.Padding = New Padding(40, 0, 0, 0)
                        'e.CellStyle.Font = New Drawing.Font(e.CellStyle.Font, FontStyle.Bold)
                End Select
            Case Cols.quota
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Select Case oControlItem.linCod
                    Case ControlItem.linCods.sum
                        e.CellStyle.Font = New Drawing.Font(e.CellStyle.Font, FontStyle.Bold)
                End Select
        End Select
    End Sub



    Private Sub Xl_BaseQuotas_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.linCod
                Case ControlItem.linCods.item
                    If oControlItem.Validation <> -1 Then
                        If oControlItem.Quota = oControlItem.Validation Then
                            e.ToolTipText = "cuadra amb el saldo del compte"
                        Else
                            e.ToolTipText = String.Format("el saldo del compte es de {0:#,##0.00\€}", oControlItem.Validation)
                        End If
                    End If
            End Select
        End If
    End Sub

    Public Function ExcelSheet() As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet
        With retval
            .AddColumn("Concepte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Base", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Tipus", MatHelper.Excel.Cell.NumberFormats.Percent)
            .AddColumn("Quota", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With
        For Each oControlItem As ControlItem In _ControlItems
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            oRow.AddCell(oControlItem.Txt)
            oRow.AddCell(oControlItem.Base)
            oRow.AddCell(oControlItem.Tipus)
            oRow.AddCell(oControlItem.Quota)
        Next
        Return retval
    End Function

    Protected Class ControlItem

        Property Txt As String
        Property Base As Decimal
        Property Tipus As Decimal
        Property Quota As Decimal
        Property Validation As Decimal
        Property linCod As linCods
        Property linSrc As linSrcs

        Public Enum linCods
            item
            sum
            title
            blank
        End Enum

        Public Enum linSrcs
            blank
            devengat
            repercutit
            intracomunitari
            recarrecEquivalencia
            totalDevengat
            deduible
            soportatInterior
            soportatImportacions
            soportatIntracomunitari
            totalDeduible
            resultat
        End Enum

        Public Sub New(linsrc As linSrcs, Optional txt As String = "", Optional DcQuota As Decimal = 0, Optional DcTipus As Decimal = 0, Optional DcBase As Decimal = 0, Optional DcValidation As Decimal = -1)
            MyBase.New()
            _linSrc = linsrc
            _linCod = linCod
            _Txt = txt
            _Base = DcBase
            _Tipus = DcTipus
            _Quota = DcQuota
            _Validation = DcValidation

            Select Case linsrc
                Case linSrcs.blank
                    _linCod = linCods.blank
                Case linSrcs.devengat, linSrcs.deduible
                    _linCod = linCods.title
                Case linSrcs.totalDevengat, linSrcs.resultat
                    _linCod = linCods.sum
                Case Else
                    _linCod = linCods.item
            End Select
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
