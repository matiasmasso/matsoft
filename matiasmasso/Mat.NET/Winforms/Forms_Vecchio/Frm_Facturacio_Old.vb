'Imports System.Xml


Public Class Frm_Facturacio_Old
    Private _ClientsFacturables As New List(Of DTOClientFacturable)
    Private _Invoices As List(Of DTOInvoice)
    Private _Deliveries As List(Of DTODelivery)

    'Private mClientsFacturables As New List(Of ClientFacturable)
    'Private mEmp As DTO.DTOEmp = BLL.BLLApp.Emp
    'Private mFras As List(Of Fra)
    'Private mDoc As XmlDocument
    'Private mAlbs As Albs

    Private mAllowEvents As Boolean
    Private mAllowEventsBadClis As Boolean
    Private mAllowEventsClis As Boolean
    Private mTbBadClis As DataTable
    Private mDsClis As DataSet
    Private _Cfps As List(Of KeyValuePair(Of Integer, String))

    Private Enum Tabs
        Inicial
        Check
        Distribucio
        Final
    End Enum

    Private Enum AlbImgs
        ClosedBlank
        ClosedStandard
        ClosedFreeOfCharge
        ClosedCash
        OpenBlank
        OpenStandard
        OpenFreeOfCharge
        OpenCash
        AlbStandard
        AlbFreeOfCharge
        AlbCash
        AlbFpg
    End Enum

    Private Enum FraNodeTypes
        NotSet
        NoFra
        Fra
        Alb
    End Enum

    Private Enum BadClisCols
        Ico
        Err
        Cli
        Clx
        Obs
    End Enum

    Private Enum CliCols
        idx
        fch
        eur
        Cli
        Clx
    End Enum

    Public Sub New(Optional oDeliveries As List(Of DTODelivery) = Nothing)
        MyBase.New
        InitializeComponent()
        _Deliveries = oDeliveries
    End Sub

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New
        InitializeComponent()
        _Deliveries = BLLDeliveries.pendentsDeFacturar(oCustomer)
    End Sub

    Public Sub New()
        MyBase.New
        InitializeComponent()
        _Deliveries = BLLDeliveries.pendentsDeFacturar()
    End Sub


    Private Sub Frm_Facturacio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _Invoices = New List(Of DTOInvoice)
        TreeViewFras.ContextMenu = ContextMenuTvFras
        Dim oLastInvoice = BLLInvoices.Last(BLLExercici.Current, DTOInvoice.Series.Standard)
        LabelLastFra.Text = String.Format("{0} del {1:dd/MM/yy}", oLastInvoice.Num, oLastInvoice.Fch)
        Dim DtFirstFch As Date = oLastInvoice.Fch
        DateTimePickerFirst.Value = DtFirstFch

        LoadCfpCods()
        If _Deliveries Is Nothing Then
            DateTimePickerLast.Value = SetLastFch()
            If DateTimePickerLast.Value.Month <> Today.Month Then
                CheckBoxFacturarTot.Checked = True
            End If
        Else
            DateTimePickerLast.Value = Today
            TabControl1.SelectedTab = TabPageCheck
            CheckBoxFacturarTot.Checked = True
        End If

        Wizard_AfterTabSelect()
    End Sub

    Private Function SetLastFch() As Date
        Dim DtFirstFch As Date = DateTimePickerFirst.Value
        Dim DtLastFch As Date = CDate("1/" & DtFirstFch.Month & "/" & DtFirstFch.Year).AddMonths(1).AddDays(-1)
        Return DtLastFch
    End Function

#Region "Funcions Auxiliars"

    Private Sub LoadCfpCods()
        Dim oCods = BLLPaymentTerms.Cods(BLLSession.Current.Lang)
        If ComboBoxCfp.Items.Count = 0 Then
            With ComboBoxCfp
                .DataSource = oCods
                .ValueMember = "Value"
                .DisplayMember = "Nom"
                .SelectedIndex = 0
            End With
        End If
    End Sub



    Public Sub RedactaFactures(ByVal fromfch As Date, ByVal ToFch As Date, ByVal BlCredit As Boolean, ByVal BlCash As Boolean, ByVal BlExport As Boolean, ByVal BlNegatius As Boolean)
        Dim oAlbaransPerFacturar As List(Of DTODelivery) = _Deliveries.Where(Function(x) x.Fch >= fromfch).ToList
        If Not BlCredit Then oAlbaransPerFacturar.RemoveAll(Function(x) x.CashCod = DTOCustomer.CashCodes.credit)
        If Not BlCash Then oAlbaransPerFacturar.RemoveAll(Function(x) x.CashCod <> DTOCustomer.CashCodes.credit)
        If BlExport Then oAlbaransPerFacturar.RemoveAll(Function(x) x.ExportCod = DTOInvoice.ExportCods.Nacional)

        _ClientsFacturables = New List(Of DTOClientFacturable)

        With ProgressBarDist
            .Minimum = 0
            .Maximum = oAlbaransPerFacturar.Count
            .Value = 0
            .Visible = True
            Application.DoEvents()
        End With

        Dim exs As New List(Of Exception)
        Dim oClientFacturable As New DTOClientFacturable
        Dim oLastInvoice As DTOInvoice = Nothing
        For Each oDelivery In oAlbaransPerFacturar
            ProgressBarDist.Increment(1)
            Application.DoEvents()
            If oDelivery.Fch > ToFch Then
                If Not BLLCustomer.CcxOrMe(oDelivery.Customer).Equals(oClientFacturable.Customer) Then
                    If oClientFacturable.Invoices.Count = 0 Then oClientFacturable.Facturable = False
                    oClientFacturable = New DTOClientFacturable(BLLCustomer.CcxOrMe(oDelivery.Customer))
                    _ClientsFacturables.Add(oClientFacturable)
                End If
                oClientFacturable.AlbaransPerFacturar.Add(oDelivery)
            Else
                If Not BLLCustomer.CcxOrMe(oDelivery.Customer).Equals(oClientFacturable.Customer) Then
                    If oClientFacturable.Invoices.Count = 0 Then oClientFacturable.Facturable = False
                    oClientFacturable = New DTOClientFacturable(BLLCustomer.CcxOrMe(oDelivery.Customer))
                    _ClientsFacturables.Add(oClientFacturable)
                End If
                oLastInvoice = BLLClientFacturable.addNewFactura(oClientFacturable, oDelivery, fromfch, _Invoices, exs)
                SetFpg(oLastInvoice, exs)
            End If
        Next

        If Not BlNegatius Then
            For i As Integer = _ClientsFacturables.Count - 1 To 0 Step -1
                If _ClientsFacturables(i).Total.Eur <= 0 Then _ClientsFacturables(i).Facturable = False
            Next
        End If

        If Not CheckBoxPreVto.Checked Then 'treu la factura si el vto es igual al de la data següent a la maxima de facturacio
            For i As Integer = _ClientsFacturables.Count - 1 To 0 Step -1
                Dim DtMaxFchFacturable As Date = DateTimePickerLast.Value
                Dim oPaymentTerms = _ClientsFacturables(i).Customer.PaymentTerms
                If oPaymentTerms.PaymentDays.Count > 0 Then
                    Dim DtVtoFromNextFch As Date = BLLPaymentTerms.Vto(oPaymentTerms, DtMaxFchFacturable.AddDays(1))
                    Dim FlagExistFacturasFacturables As Boolean = False
                    For j As Integer = _ClientsFacturables(i).Invoices.Count - 1 To 0 Step -1
                        Dim oInvoice = _ClientsFacturables(i).Invoices(j)
                        If oInvoice.Vto = DtVtoFromNextFch Then
                            RemoveFraFromClient(oInvoice, _ClientsFacturables(i))
                        Else
                            FlagExistFacturasFacturables = True
                        End If
                    Next

                    If Not FlagExistFacturasFacturables Then
                        _ClientsFacturables(i).Facturable = False
                    End If
                End If
            Next
        End If

        'If Not CheckBoxFraPerMes.Checked Then
        'For i As Integer = _ClientsFacturables.Count - 1 To 0 Step -1
        'If _ClientsFacturables(i).Client.CodAlbsXFra = Client.CodsAlbsXFra.UnaSolaFraMensual Then
        '_ClientsFacturables(i).Facturable = False
        'End If
        'Next
        'End If


        'If Not CheckBoxSmallVolume.Checked Then
        'For i As Integer = mClientsFacturables.Count - 1 To 0 Step -1
        ' If mClientsFacturables(i).Client.PaymentTerms.PaymentDays.Count = 0 Then 'exceptua els que tenen dia de pagament
        'If mClientsFacturables(i).Total.Eur < 100 Then mClientsFacturables(i).Facturable = False
        'End If
        'Next
        ' End If


        'treu factures de import zero
        For i As Integer = _ClientsFacturables.Count - 1 To 0 Step -1
            oClientFacturable = _ClientsFacturables(i)
            For j As Integer = oClientFacturable.Invoices.Count - 1 To 0 Step -1
                Dim oInvoice = oClientFacturable.Invoices(j)

                'If oFra.Total.Eur = 0 Then
                If oInvoice.Deliveries.Sum(Function(x) x.Import.Eur) = 0 Then
                    For k As Integer = oInvoice.Deliveries.Count - 1 To 0 Step -1
                        Dim oDelivery = oInvoice.Deliveries(k)
                        RemoveAlbFromParent(oDelivery, oInvoice, oClientFacturable)
                        InsertAlbOnPendents(oDelivery, oClientFacturable)
                    Next
                End If
            Next
        Next

        ProgressBarDist.Visible = False

    End Sub

    Private Sub RemoveFraFromClient(ByVal oInvoice As DTOInvoice, ByVal oClientFacturable As DTOClientFacturable)
        Dim i As Integer

        For i = oInvoice.Deliveries.Count - 1 To 0 Step -1
            Dim oDelivery = oInvoice.Deliveries(i)
            oInvoice.Deliveries.RemoveAt(i)
            InsertAlbOnPendents(oDelivery, oClientFacturable)
        Next

        Dim oInvoices As List(Of DTOInvoice) = oClientFacturable.Invoices
        For i = oInvoices.Count - 1 To 0 Step -1
            If oInvoices(i).Equals(oInvoice) Then
                oInvoices.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub


    Private Sub LoadClis()

        With DataGridViewClis
            With .RowTemplate
                .Height = DataGridViewClis.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = GetCliDataSource(_ClientsFacturables)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(CliCols.idx)
                .Visible = False
            End With
            With .Columns(CliCols.Cli)
                .Visible = False
            End With
            With .Columns(CliCols.eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(CliCols.fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(CliCols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            If .Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(CliCols.Clx)
                ShowCli()
            End If
        End With
        'End If
    End Sub

    Private Function GetCliDataSource(ByVal oClientsFacturables As List(Of DTOClientFacturable)) As DataView
        Dim oTb As New DataTable
        With oTb
            .Columns.Add("IDX", System.Type.GetType("System.Int32"))
            .Columns.Add("FCH", System.Type.GetType("System.DateTime"))
            .Columns.Add("EUR", System.Type.GetType("System.Decimal"))
            .Columns.Add("CLI", System.Type.GetType("System.Int32"))
            .Columns.Add("CLX", System.Type.GetType("System.String"))
        End With

        Dim idx As Integer = 0
        Dim oRow As DataRow = Nothing
        For Each oClientFacturable In oClientsFacturables
            If oClientFacturable.Facturable Then
                oRow = oTb.NewRow
                oRow("IDX") = idx
                oRow("FCH") = BLLClientFacturable.LastFch(oClientFacturable)
                oRow("EUR") = oClientFacturable.Total.Eur
                oRow("CLI") = oClientFacturable.Customer.Id
                oRow("CLX") = oClientFacturable.Customer.FullNom
                oTb.Rows.Add(oRow)
            End If
            idx += 1
        Next
        Return oTb.DefaultView
    End Function

    Private Sub DisplayFpg(ByVal oClientFacturable As DTOClientFacturable)
        Dim oCustomer = oClientFacturable.Customer
        LabelFpg.Text = BLLPaymentTerms.Text(oCustomer.PaymentTerms, oCustomer.Lang)
    End Sub


    Private Sub DisplayNodes(ByVal oClientFacturable As DTOClientFacturable, Optional ByVal iSelectedNode As Integer = -1)
        TreeViewFras.Nodes.Clear()

        If oClientFacturable IsNot Nothing Then
            Dim oNoFraNode As TreeNodeObj = TreeNodeNoFra(oClientFacturable.AlbaransPerFacturar)
            TreeViewFras.Nodes.Add(oNoFraNode)

            Dim FirstNodeSelected As Boolean = iSelectedNode >= 0
            Dim oFraNode As TreeNodeObj
            For Each oInvoice In oClientFacturable.Invoices
                oFraNode = TreeNodeFra(oInvoice)
                TreeViewFras.Nodes.Add(oFraNode)
                If Not FirstNodeSelected Then
                    TreeViewFras.SelectedNode = oFraNode
                    FirstNodeSelected = True
                End If
            Next

            TreeViewFras.ExpandAll()
            If iSelectedNode >= 0 Then
                If TreeViewFras.Nodes.Count > iSelectedNode Then
                    TreeViewFras.SelectedNode = TreeViewFras.Nodes(iSelectedNode)
                End If
            Else
                If TreeViewFras.SelectedNode Is Nothing Then
                    TreeViewFras.SelectedNode = oNoFraNode
                End If
            End If

            TreeViewFras.SelectedNode.EnsureVisible()
        End If
    End Sub




    Private Function TreeNodeAlb(ByVal oDelivery As DTODelivery) As TreeNodeObj
        Dim oNode As New TreeNodeObj("", oDelivery)
        Dim sTxt As String = NodeText(oNode)

        With oNode
            Select Case oDelivery.Import.Eur
                Case Is < 0
                    .ImageIndex = AlbImgs.AlbFreeOfCharge
                Case 0
                    sTxt = sTxt & " s/carrec "
                    .ImageIndex = AlbImgs.AlbFreeOfCharge
                Case Else
                    Select Case oDelivery.CashCod
                        Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa, DTO.DTOCustomer.CashCodes.Reembols, DTO.DTOCustomer.CashCodes.Diposit
                            .ImageIndex = AlbImgs.AlbCash
                        Case DTO.DTOCustomer.CashCodes.credit
                            .ImageIndex = AlbImgs.AlbStandard
                    End Select

                    If oDelivery.Fpg > "" Then
                        .BackColor = Color.Yellow
                        .ImageIndex = AlbImgs.AlbFpg
                        sTxt = sTxt & " (condicions especials)"
                    End If
            End Select
            .SelectedImageIndex = .ImageIndex
            .Text = sTxt
        End With

        Return oNode
    End Function

    Private Function TreeNodeNoFra(ByVal oAlbsPerFacturar As List(Of DTODelivery)) As TreeNodeObj
        Dim oNode As New TreeNodeObj("", oAlbsPerFacturar)

        With oNode
            .Text = NodeText(oNode)
            .ImageIndex = AlbImgs.ClosedBlank
            .SelectedImageIndex = AlbImgs.OpenBlank
            For Each oDelivery In oAlbsPerFacturar
                .Nodes.Add(TreeNodeAlb(oDelivery))
            Next
        End With

        Return oNode
    End Function

    Private Function TreeNodeFra(ByVal oInvoice As DTOInvoice) As TreeNodeObj
        Dim oNode As New TreeNodeObj("", oInvoice)

        With oNode
            .Text = NodeText(oNode)
            .ImageIndex = TreeNodeFraImageIndex(oInvoice)
            .SelectedImageIndex = TreeNodeFraImageIndex(oInvoice, True)
            For Each oDelivery In oInvoice.Deliveries
                .Nodes.Add(TreeNodeAlb(oDelivery))
            Next
        End With

        Return oNode
    End Function

    Private Function TreeNodeFraImageIndex(ByVal oInvoice As DTOInvoice, Optional ByVal BlSelectedImage As Boolean = False) As Integer
        Dim retval As Integer
        Select Case oInvoice.Total.Eur
            Case Is <= 0
                retval = IIf(BlSelectedImage, AlbImgs.OpenFreeOfCharge, AlbImgs.ClosedFreeOfCharge)
            Case Else
                Select Case oInvoice.Deliveries.First.CashCod
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Reembols, DTO.DTOCustomer.CashCodes.Diposit
                        retval = IIf(BlSelectedImage, AlbImgs.OpenCash, AlbImgs.ClosedCash)
                    Case DTO.DTOCustomer.CashCodes.credit
                        retval = IIf(BlSelectedImage, AlbImgs.OpenStandard, AlbImgs.ClosedStandard)
                End Select
        End Select
        Return retval
    End Function

#End Region

#Region "Portada"

    Private Sub DateTimePickerFirst_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DateTimePickerLast.Value = MaxiSrvr.GetEndMonth(DateTimePickerFirst.Value)
    End Sub


    Private Sub DateTimePickerVto_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerVto.ValueChanged
        If mAllowEvents Then
            Dim oInvoice As DTOInvoice = CurrentFraNode.Obj
            oInvoice.Vto = DateTimePickerVto.Value
            Dim exs As New List(Of Exception)
            SetFpg(oInvoice, exs)
            If exs.Count > 0 Then MsgBox(BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            DisplayNodes(CurrentCliFacturable, TreeViewFras.SelectedNode.Index)
            DisplayFra()
        End If
    End Sub

#End Region

#Region "Check Errors"

    Private Sub AddBadCli(ByVal oCli As Client, ByVal sWarnText As String, ByVal CodErr As Integer)
        Dim oRow As DataRow = mTbBadClis.NewRow
        oRow(BadClisCols.Cli) = oCli.Id
        oRow(BadClisCols.Clx) = oCli.Clx
        oRow(BadClisCols.Err) = CodErr
        oRow(BadClisCols.Obs) = sWarnText
        mTbBadClis.Rows.Add(oRow)
    End Sub

    Private Sub CreateTableBadClis()
        mTbBadClis = New DataTable("BADCLIS")
        mTbBadClis.Columns.Add("ERR", System.Type.GetType("System.Int32"))
        mTbBadClis.Columns.Add("CLI", System.Type.GetType("System.Int32"))
        mTbBadClis.Columns.Add("CLX", System.Type.GetType("System.String"))
        mTbBadClis.Columns.Add("OBS", System.Type.GetType("System.String"))
    End Sub




#End Region

#Region "Seleccio Factures"




    Private Sub DataGridViewClis_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewClis.DoubleClick
        Dim oCustomer = CurrentCliFacturable.Customer
        Dim oFrm As New Frm_Contact(oCustomer)
        oFrm.Show()
    End Sub


    Private Sub DataGridViewClis_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewClis.SelectionChanged
        If mAllowEventsClis Then
            ShowCli()
            SetContextMenuClis()
        End If

    End Sub



    Private Sub ShowCli()
        Dim oClientFacturable As DTOClientFacturable = CurrentCliFacturable()
        If Not oClientFacturable Is Nothing Then
            DisplayNodes(oClientFacturable)
            DisplayFpg(oClientFacturable)
        End If
    End Sub


    Private Sub DisplayFra()
        mAllowEvents = False
        Dim oCurrentFra As DTOInvoice = Nothing
        Dim oNode As TreeNodeObj = CurrentNode()
        Select Case FraNodeType(oNode)
            Case FraNodeTypes.NoFra
            Case FraNodeTypes.Fra
                oCurrentFra = oNode.Obj
            Case FraNodeTypes.Alb
                Dim oNodeParent As TreeNodeObj = CType(oNode.Parent, TreeNodeObj)
                Select Case FraNodeType(oNodeParent)
                    Case FraNodeTypes.NoFra
                    Case FraNodeTypes.Fra
                        oCurrentFra = oNodeParent.Obj
                End Select
        End Select

        If oCurrentFra Is Nothing Then
            ComboBoxCfp.Visible = False
            DateTimePickerVto.Visible = False
            TextBoxFpg.Visible = False
            TextBoxOb1.Visible = False
            TextBoxOb2.Visible = False
            TextBoxOb3.Visible = False
            CheckBoxIva.Visible = False
            CheckBoxReq.Visible = False
        Else
            ComboBoxCfp.Visible = True
            DateTimePickerVto.Visible = True
            TextBoxFpg.Visible = True
            TextBoxOb1.Visible = True
            TextBoxOb2.Visible = True
            TextBoxOb3.Visible = True
            CheckBoxIva.Visible = True
            CheckBoxReq.Visible = True
            With oCurrentFra
                For Each item As DTOValueNom In ComboBoxCfp.Items
                    If item.Value = .Cfp Then
                        ComboBoxCfp.SelectedItem = item
                        Exit For
                    End If
                Next
                DateTimePickerVto.Value = .Vto
                TextBoxFpg.Text = .Fpg
                TextBoxOb1.Text = .Ob1
                TextBoxOb2.Text = .Ob2
                TextBoxOb3.Text = .Ob3
                CheckBoxIva.Checked = .Customer.IVA
                CheckBoxReq.Checked = .Customer.Req
            End With

        End If
        mAllowEvents = True
    End Sub



    Private Sub MenuItemTvFrasZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasZoom.Click
        Dim oNode As TreeNodeObj = TreeViewFras.SelectedNode
        Select Case FraNodeType(oNode)
            Case FraNodeTypes.Alb
                Dim oAlb As Alb = CType(oNode.Obj, Alb)
                Dim oDelivery As New DTODelivery(oAlb.Guid)
                Dim oFrm As New Frm_AlbNew2(oDelivery)
                oFrm.Show()
            Case FraNodeTypes.Fra
                MsgBox("No implementat", MsgBoxStyle.Exclamation)
        End Select
    End Sub

    Private Function FraNodeType(ByVal oNode As TreeNodeObj) As FraNodeTypes
        Dim retVal As FraNodeTypes = FraNodeTypes.NotSet
        Dim oObj As Object = oNode.Obj
        If TypeOf (oObj) Is List(Of DTODelivery) Then
            retVal = FraNodeTypes.NoFra
        ElseIf TypeOf (oObj) Is DTOInvoice Then
            retVal = FraNodeTypes.Fra
        ElseIf TypeOf (oObj) Is DTODelivery Then
            retVal = FraNodeTypes.Alb
        End If
        Return retVal
    End Function

    Private Sub ContextMenuTvFras_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuTvFras.Popup
        Dim oNode As System.Windows.Forms.TreeNode = TreeViewFras.SelectedNode
        Select Case FraNodeType(oNode)
            Case FraNodeTypes.Alb
                MenuItemTvFrasZoom.Enabled = True
                MenuItemTvFrasFacturarEn.Visible = True
                Select Case FraNodeType(oNode.Parent)
                    Case FraNodeTypes.Fra
                        MenuItemTvFrasRemove.Enabled = True
                    Case Else
                        MenuItemTvFrasRemove.Enabled = False
                End Select
            Case FraNodeTypes.Fra
                MenuItemTvFrasZoom.Enabled = False
                MenuItemTvFrasRemove.Enabled = True
                MenuItemTvFrasFacturarEn.Visible = False
            Case Else
                MenuItemTvFrasZoom.Enabled = False
                MenuItemTvFrasRemove.Enabled = False
                MenuItemTvFrasFacturarEn.Visible = False

        End Select
    End Sub



    Private Function CurrentCliFacturable() As DTOClientFacturable
        Dim retVal As DTOClientFacturable = Nothing

        If DataGridViewClis.SelectedRows.Count = 1 Then
            Dim oRow As DataGridViewRow = DataGridViewClis.SelectedRows(0)
            Dim idx As Integer = CInt(oRow.Cells(CliCols.idx).Value)
            retVal = _ClientsFacturables(idx)
        End If

        Return retVal
    End Function


    Private Function CurrentClisFacturables() As List(Of DTOClientFacturable)
        Dim oClis As New List(Of DTOClientFacturable)

        If DataGridViewClis.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridViewClis.SelectedRows
                Dim idx As Integer = CInt(oRow.Cells(CliCols.idx).Value)
                oClis.Add(_ClientsFacturables(idx))
            Next
        Else
            Dim oClientFacturable As DTOClientFacturable = CurrentCliFacturable()
            If oClientFacturable IsNot Nothing Then
                oClis.Add(oClientFacturable)
            End If
        End If
        Return oClis
    End Function


    Private Sub SetContextMenuClis()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oClientsFacturables As List(Of DTOClientFacturable) = CurrentClisFacturables()

        If oClientsFacturables.Count >= 0 Then
            oMenuItem = New ToolStripMenuItem("retirar de facturació", My.Resources.del, AddressOf OnRemoveCli)
            oContextMenu.Items.Add(oMenuItem)
            If oClientsFacturables.Count = 1 Then
                oContextMenu.Items.Add("-")
                Dim oMenu_Contact As New Menu_Contact(oClientsFacturables(0).Customer)
                oContextMenu.Items.AddRange(oMenu_Contact.Range)
            End If
        End If


        DataGridViewClis.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub OnRemoveCli(ByVal sender As Object, ByVal e As System.EventArgs)
        If DataGridViewClis.SelectedRows.Count > 0 Then
            For Each oRow As DataGridViewRow In DataGridViewClis.SelectedRows
                RemoveCliRow(oRow)
            Next
        Else
            Dim oRow As DataGridViewRow = DataGridViewClis.CurrentRow
            If oRow IsNot Nothing Then RemoveCliRow(oRow)
        End If
    End Sub

    Private Sub RemoveCliRow(ByVal oRow As DataGridViewRow)
        Dim idx As Integer = oRow.Cells(CliCols.idx).Value
        _ClientsFacturables(idx).Facturable = False
        DataGridViewClis.Rows.Remove(oRow)
    End Sub


    Private Sub MenuItemCliRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oTvFraNode As System.Windows.Forms.TreeNode
        Dim oTvAlbNode As System.Windows.Forms.TreeNode
        Dim oTvNoFraNode As System.Windows.Forms.TreeNode = TreeViewFras.Nodes(0)

        Dim i As Integer
        Dim j As Integer
        For i = 1 To TreeViewFras.Nodes.Count - 1
            oTvFraNode = TreeViewFras.Nodes(i)
            For j = oTvFraNode.Nodes.Count - 1 To 0 Step -1
                oTvAlbNode = oTvFraNode.Nodes(j)
                oTvFraNode.Nodes.Remove(oTvAlbNode)
                oTvNoFraNode.Nodes.Add(oTvAlbNode)
            Next
            oTvFraNode.Remove()
        Next

        oTvNoFraNode.Expand()
    End Sub

    Private Sub MenuItemTvFrasRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasRemove.Click
        Dim oNode As System.Windows.Forms.TreeNode = TreeViewFras.SelectedNode
        Dim oNoFraNode As System.Windows.Forms.TreeNode = TreeViewFras.Nodes(0)

        Select Case FraNodeType(oNode)
            Case FraNodeTypes.Alb
                RemoveAlbNodeFromParent(oNode, oNode.Parent)
                InsertAlbOnPendents(oNode)
            Case FraNodeTypes.Fra
                RemoveFraNode(oNode)
        End Select

        TreeViewFras.ExpandAll()
    End Sub

    Private Sub RemoveFraNode(ByVal oNodeFra As TreeNodeObj)
        Do While oNodeFra.Nodes.Count > 0
            Dim oNodeAlb As TreeNodeObj = oNodeFra.Nodes(0)
            RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)
            InsertAlbOnPendents(oNodeAlb)
        Loop
        'For Each oNodeAlb As TreeNodeObj In oNodeFra.Nodes
        'RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)
        'InsertAlbOnPendents(oNodeAlb)
        'Next

        Dim oInvoice As DTOInvoice = oNodeFra.Obj
        Dim oInvoices = CurrentCliFacturable.Invoices
        For i As Integer = 0 To oInvoices.Count - 1
            If oInvoices(i).Equals(oInvoice) Then
                oInvoices.RemoveAt(i)
            End If
        Next

        TreeViewFras.Nodes.Remove(oNodeFra)
    End Sub


    Private Sub RemoveAlbNodeFromParent(ByVal oAlbNodeToRemove As TreeNodeObj, ByVal oFromParentNode As TreeNodeObj)
        Dim oAlbToRemove As DTODelivery = oAlbNodeToRemove.Obj
        oFromParentNode.Nodes.Remove(oAlbNodeToRemove)
        RemoveAlbFromParent(oAlbToRemove, oFromParentNode.Obj, CurrentCliFacturable)
        FraCalc(oFromParentNode)

        If oFromParentNode.Nodes.Count = 0 Then
            If FraNodeType(oFromParentNode) = FraNodeTypes.Fra Then
                RemoveFraNode(oFromParentNode)
            End If
        End If

    End Sub

    Private Sub RemoveAlbFromParent(ByVal oDelivery As DTODelivery, ByVal oParent As Object, ByVal oClientFacturable As DTOClientFacturable)
        If TypeOf (oParent) Is List(Of DTODelivery) Then
            Dim oDeliveries As List(Of DTODelivery) = oParent
            oDeliveries.Remove(oDelivery)
        ElseIf TypeOf (oParent) Is DTOInvoice Then
            Dim oInvoice As DTOInvoice = oParent
            oInvoice.Deliveries.Remove(oDelivery)
            If oInvoice.Deliveries.Count = 0 Then
                Dim oinvoices = oClientFacturable.Invoices
                For i As Integer = oinvoices.Count - 1 To 0 Step -1
                    If oinvoices(i).Equals(oInvoice) Then
                        oinvoices.RemoveAt(i)
                        Exit For
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub FraCalc(ByVal oNode As TreeNodeObj)
        If FraNodeType(oNode) = FraNodeTypes.Fra Then

        End If
        oNode.Text = NodeText(oNode)
    End Sub

    Private Function NodeText(ByVal oNode As TreeNodeObj) As String
        Dim s As String = ""

        Select Case FraNodeType(oNode)
            Case FraNodeTypes.NoFra
                s = "pendents de facturar"
                Dim oAmt As DTOAmt = BLLApp.EmptyAmt
                Dim oAlbs As List(Of DTODelivery) = oNode.Obj
                If oAlbs.Count > 0 Then
                    For Each oAlb In oAlbs
                        oAmt.Add(oAlb.Import)
                    Next
                    s = s & " " & oAmt.CurFormatted
                End If

            Case FraNodeTypes.Fra
                s = FraNodeText(oNode.Obj)
            Case FraNodeTypes.Alb
                s = AlbNodeText(oNode.Obj)
        End Select
        Return s
    End Function

    Private Function FraNodeText(ByVal oInvoice As DTOInvoice) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("fra.")
        If oInvoice.Num > 0 Then sb.Append(oInvoice.Num.ToString)
        sb.Append(" del " & FchLastAlb(oInvoice).ToShortDateString)
        sb.Append(" vto." & oInvoice.Vto.ToShortDateString)
        sb.Append(" per " & FraTotal(oInvoice).CurFormatted)

        Select Case oInvoice.Cfp
            Case DTO.DTOCustomer.CashCodes.Reembols, DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa, DTO.DTOCustomer.CashCodes.Diposit
                sb.Append(" (Cash)")
        End Select

        Dim s As String = sb.ToString
        Return s
    End Function

    Private Function AlbNodeText(ByVal oDelivery As DTODelivery) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("alb." & oDelivery.Id)
        sb.Append(" del " & oDelivery.Fch.ToShortDateString)
        If oDelivery.Customer.Ref > "" Then
            sb.Append(" (" & oDelivery.Customer.Ref & ")")
        End If
        sb.Append(" per " & oDelivery.Import.CurFormatted)

        Select Case oDelivery.CashCod
            Case DTO.DTOCustomer.CashCodes.Reembols
                sb.Append(" (reembols)")
            Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                sb.Append(" (transf)")
            Case DTO.DTOCustomer.CashCodes.Visa
                sb.Append(" (Visa)")
            Case DTO.DTOCustomer.CashCodes.Diposit
                sb.Append(" (diposit)")
        End Select

        Dim s As String = sb.ToString
        Return s
    End Function

    Private Function FraTotal(ByVal oInvoice As DTOInvoice) As DTOAmt
        Dim oAmt As DTOAmt = BLLApp.EmptyAmt
        For Each oAlb In oInvoice.Deliveries
            oAmt.Add(oAlb.Import)
        Next
        Return oAmt
    End Function

    Private Function FchLastAlb(ByVal oInvoice As DTOInvoice) As Date
        Dim DtFch As Date = DateTimePickerFirst.Value
        For Each oAlb In oInvoice.Deliveries
            If oAlb.Fch > DtFch Then DtFch = oAlb.Fch
        Next
        Return DtFch
    End Function

    Private Sub MenuItemTvFrasNewFra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasNewFra.Click
        Dim oNodeAlb As TreeNodeObj = CType(TreeViewFras.SelectedNode, TreeNodeObj)

        RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)

        Dim oDelivery = oNodeAlb.Obj
        Dim exs As New List(Of Exception)
        Dim oInvoice = BLLClientFacturable.addNewFactura(CurrentCliFacturable, oDelivery, DateTimePickerFirst.Value, _Invoices, exs)
        Dim oNodeFra As TreeNodeObj = TreeNodeFra(oInvoice)

        InsertFraNode(oNodeFra)
    End Sub

    Private Sub FacturarEn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenu As System.Windows.Forms.MenuItem = CType(sender, System.Windows.Forms.MenuItem)
        FacturarEn(TreeViewFras.SelectedNode, oMenu.Index)
    End Sub

    Private Sub FacturarEn(ByVal oTvNode As TreeNodeObj, ByVal NewFraIdx As Integer)
        Dim oNodeAlb As TreeNodeObj = CType(TreeViewFras.SelectedNode, TreeNodeObj)
        Dim oNodeFra As TreeNodeObj = CType(TreeViewFras.Nodes(NewFraIdx), TreeNodeObj)

        RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)
        InsertAlbOnFra(oNodeAlb, oNodeFra)
    End Sub

    Private Sub InsertFraNode(ByVal oFraNode As TreeNodeObj)
        Dim oInvoice As DTOInvoice = oFraNode.Obj
        Dim FlagInserted As Boolean = False
        For i As Integer = 1 To TreeViewFras.Nodes.Count - 1
            Dim oNode As TreeNodeObj = TreeViewFras.Nodes(i)
            Dim oFraItm As DTOInvoice = oNode.Obj
            If oFraItm.Fch > oInvoice.Fch Then
                TreeViewFras.Nodes.Insert(i, oFraNode)
                FlagInserted = True
                Exit For
            End If
        Next
        If Not FlagInserted Then
            TreeViewFras.Nodes.Add(oFraNode)
            oFraNode.ExpandAll()
        End If
    End Sub

    Private Sub InsertAlbOnFra(ByVal oAlbNodeToInsert As TreeNodeObj, ByVal oFraNode As TreeNodeObj)
        Dim oAlbToInsert As DTODelivery = oAlbNodeToInsert.Obj
        Dim oInvoice As DTOInvoice = oFraNode.Obj
        Dim BlInserted As Boolean = False

        For i As Integer = 0 To oFraNode.Children.Count - 1
            Dim oNodeAlb As TreeNodeObj = CType(oFraNode.Children(i), TreeNodeObj)
            Dim oAlb As DTODelivery = oNodeAlb.Obj
            If oAlbToInsert.Id > oAlb.Id Then
                oFraNode.Nodes.Insert(i, oAlbNodeToInsert)
                oInvoice.Deliveries.Insert(i, oAlbToInsert)
                FraCalc(oFraNode)
                BlInserted = True
                Exit For
            End If
        Next

        If Not BlInserted Then
            oFraNode.Nodes.Add(oAlbNodeToInsert)
            oInvoice.Deliveries.Add(oAlbToInsert)
            FraCalc(oFraNode)
            oFraNode.ExpandAll()
        End If

    End Sub


    Private Sub InsertAlbOnPendents(ByVal oAlbNodeToInsert As TreeNodeObj)
        Dim oAlbToInsert As DTODelivery = oAlbNodeToInsert.Obj
        Dim BlInserted As Boolean = False

        Dim oNoFraNode As TreeNodeObj = TreeViewFras.Nodes(0)
        For i As Integer = 0 To oNoFraNode.Children.Count - 1
            Dim oNodeAlb As TreeNodeObj = CType(oNoFraNode.Children(i), TreeNodeObj)
            Dim oDelivery = oNodeAlb.Obj
            If oAlbToInsert.Id > oDelivery.Id Then
                oNoFraNode.Nodes.Insert(i, oAlbNodeToInsert)
                CurrentCliFacturable.AlbaransPerFacturar.Insert(i, oAlbToInsert)
                FraCalc(oNoFraNode)
                BlInserted = True
                Exit For
            End If
        Next

        If Not BlInserted Then
            oNoFraNode.Nodes.Add(oAlbNodeToInsert)
            CurrentCliFacturable.AlbaransPerFacturar.Add(oAlbToInsert)
            FraCalc(oNoFraNode)
        End If

        oNoFraNode.Text = NodeText(oNoFraNode)
    End Sub

    Private Sub InsertAlbOnPendents(ByVal oAlbToInsert As DTODelivery, ByVal oClientFacturable As DTOClientFacturable)
        Dim BlInserted As Boolean = False

        Dim oDeliveries = oClientFacturable.AlbaransPerFacturar
        For i As Integer = 0 To oDeliveries.Count - 1
            Dim oDelivery = oDeliveries(i)
            If oAlbToInsert.Id > oDelivery.Id Then
                oDeliveries.Insert(i, oAlbToInsert)
                'FraCalc(oNoFraNode)
                BlInserted = True
                Exit For
            End If
        Next

        If Not BlInserted Then
            oDeliveries.Add(oAlbToInsert)
            'FraCalc(oNoFraNode)
        End If

    End Sub




    Private Sub MenuItemTvFrasFacturarEn_Select(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasFacturarEn.Select
        Dim OldFraIdx As Integer = TreeViewFras.SelectedNode.Parent.Index
        Dim oMnuItm As System.Windows.Forms.MenuItem

        MenuItemTvFrasFacturarEn.MenuItems.Clear()
        MenuItemTvFrasFacturarEn.MenuItems.Add(MenuItemTvFrasNewFra)

        For i As Integer = 1 To TreeViewFras.Nodes.Count - 1
            Dim oNode As TreeNodeObj = TreeViewFras.Nodes(i)
            oMnuItm = MenuItemTvFrasFacturarEn.MenuItems.Add(oNode.Text, New EventHandler(AddressOf FacturarEn_Click))
            If i = OldFraIdx Then oMnuItm.Enabled = False
        Next

    End Sub


    Private Function CurrentFraNode() As TreeNodeObj
        Dim oNode As TreeNodeObj = Nothing

        Select Case FraNodeType(CurrentNode)
            Case FraNodeTypes.Alb
                oNode = CType(CurrentNode.Parent, TreeNodeObj)
            Case FraNodeTypes.Fra
                oNode = CurrentNode()
        End Select

        Return oNode
    End Function

    Private Function CurrentNode() As TreeNodeObj
        Dim oNode As TreeNodeObj = TreeViewFras.SelectedNode
        Return oNode
    End Function

    Private Sub TreeViewFras_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewFras.AfterSelect
        Dim oNode As TreeNodeObj = CurrentNode()

        DisplayFra()
    End Sub



    Private Sub ComboBoxCfp_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxCfp.SelectedValueChanged
        If mAllowEvents Then
            If ComboBoxCfp.SelectedIndex >= 0 Then
                Dim oInvoice As DTOInvoice = CurrentFraNode.Obj
                oInvoice.Cfp = CurrentCfp()

                Dim exs As New List(Of Exception)
                SetFpg(oInvoice, exs)
                If exs.Count > 0 Then MsgBox(BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
                DisplayFra()
            End If
        End If
    End Sub

    Private Sub SetFpg(ByVal oInvoice As DTOInvoice, ByRef exs As List(Of Exception))
        Dim oFirstDelivery As DTODelivery = oInvoice.Deliveries.First
        Dim oLang As DTOLang = oInvoice.Customer.Lang

        Select Case oInvoice.Cfp
            Case DTOPaymentTerms.CodsFormaDePago.Comptat
                Select Case oFirstDelivery.CashCod
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                        oInvoice.Fpg = oLang.Tradueix("transferencia previa", "transferència prèvia", "bank transfer")

                    Case DTO.DTOCustomer.CashCodes.Visa
                        oInvoice.Fpg = oLang.Tradueix("tarjeta de crédito", "tarja de crèdit", "credit card")
                    Case DTO.DTOCustomer.CashCodes.Reembols
                        oInvoice.Fpg = oLang.Tradueix("contra reembolso", "contra reemborsament", "cash against goods")
                    Case DTO.DTOCustomer.CashCodes.Diposit
                        oInvoice.Fpg = oLang.Tradueix("a deducir de depósito a su favor", "a deduir de diposit al seu favor", "to deduct from existing diposit")
                    Case DTO.DTOCustomer.CashCodes.credit
                        exs.Add(New Exception("alb." & oFirstDelivery.Id.ToString & " a credit facturat com cobrat"))
                End Select
            Case Else
                Dim oPaymentTerms = oInvoice.Customer.PaymentTerms
                oPaymentTerms.Cod = oInvoice.Cfp
                oInvoice.Fpg = BLLPaymentTerms.Text(oPaymentTerms, oLang, oInvoice.Vto.ToShortDateString)
                Select Case oInvoice.Cfp
                    Case DTOPaymentTerms.CodsFormaDePago.Transferencia
                        Dim oNBanc As DTOBanc = oPaymentTerms.NBanc
                        If oNBanc IsNot Nothing Then
                            BLL.BLLBanc.Load(oNBanc)
                            oInvoice.Ob1 = BLL.BLLIban.BankNom(oNBanc.Iban)
                            oInvoice.Ob2 = "Cuenta " & BLL.BLLIban.Formated(oNBanc.Iban)
                        End If
                    Case DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria, DTOPaymentTerms.CodsFormaDePago.EfteAndorra
                        Dim oIban As DTOIban = oPaymentTerms.Iban
                        If oIban Is Nothing Then
                            exs.Add(New Exception("falta Iban de " & oInvoice.Customer.FullNom))
                        Else
                            Dim oBank As DTOBank = BLL.BLLIban.Bank(oIban)
                            If oBank Is Nothing Then
                                exs.Add(New Exception("Iban mal entrat o desconegut de " & oInvoice.Customer.FullNom))
                            Else
                                oInvoice.Ob1 = oBank.RaoSocial
                                If oIban.BankBranch IsNot Nothing Then
                                    oInvoice.Ob2 = BLL.BLLIban.BranchLocationAndAdr(oIban)
                                End If
                                Dim sDigits As String = oIban.Digits
                                If sDigits.Length > 4 Then sDigits = "..." & sDigits.Substring(sDigits.Length - 4)
                                oInvoice.Ob3 = BLL.BLLIban.LastDigits(oIban, oLang)
                            End If
                        End If
                End Select
                If oFirstDelivery.CashCod <> DTO.DTOCustomer.CashCodes.credit Then
                    exs.Add(New Exception("alb." & oFirstDelivery.Id.ToString & " cobrat i facturat a credit"))
                End If
        End Select

    End Sub



    Private Function CurrentCfp() As DTOPaymentTerms.CodsFormaDePago
        Dim item As KeyValuePair(Of Integer, String) = ComboBoxCfp.SelectedItem
        Dim retval As DTOPaymentTerms.CodsFormaDePago = item.Key
        Return retval
    End Function

    Private Sub TextBoxOb1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxOb1.TextChanged
        If mAllowEvents Then
            Dim oInvoice As DTOInvoice = CurrentFraNode.Obj
            oInvoice.Ob1 = TextBoxOb1.Text
        End If
    End Sub

    Private Sub TextBoxOb2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxOb2.TextChanged
        If mAllowEvents Then
            Dim oInvoice As DTOInvoice = CurrentFraNode.Obj
            oInvoice.Ob2 = TextBoxOb2.Text
        End If
    End Sub

    Private Sub TextBoxOb3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxOb3.TextChanged
        If mAllowEvents Then
            Dim oInvoice As DTOInvoice = CurrentFraNode.Obj
            oInvoice.Ob3 = TextBoxOb3.Text
        End If
    End Sub

    Private Sub TextBoxFpg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxFpg.TextChanged
        If mAllowEvents Then
            Dim oInvoice As DTOInvoice = CurrentFraNode.Obj
            oInvoice.Fpg = TextBoxFpg.Text
        End If
    End Sub

    Private Sub MenuItemTvFrasSetNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasSetNum.Click
        Dim oNode As TreeNodeObj = CurrentFraNode()
        Dim oInvoice As DTOInvoice = oNode.Obj
        Dim oFrm As New Frm_FraNumFch(oInvoice)
        With oFrm
            .ShowDialog()
            If Not .Cancel Then
                'If .FraNum > 0 Then
                oInvoice.Num = .FraNum
                'End If
                If .FraFch > Date.MinValue Then
                    oInvoice.Fch = .FraFch
                    DateTimePickerFirst.Value = .FraFch
                Else
                    oInvoice.Fch = Date.MinValue
                End If
                DisplayNodes(CurrentCliFacturable)
            End If
        End With

    End Sub


#End Region

#Region "Finalitzar"

    Private Function SortFacturas() As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)

        For Each oClientFacturable As DTOClientFacturable In _ClientsFacturables
            If oClientFacturable.Facturable Then
                For Each oInvoice In oClientFacturable.Invoices
                    AddFraToSortedCollection(retval, oInvoice)
                Next
            End If
        Next

        Return retval
    End Function

    Private Sub AddFraToSortedCollection(ByRef oSortedCollection As List(Of DTOInvoice), ByVal oFraToAdd As DTOInvoice)
        Dim BlInserted As Boolean = False
        For i As Integer = 0 To oSortedCollection.Count - 1
            Dim oInvoice = oSortedCollection(i)
            Select Case oInvoice.Fch
                Case Is > oFraToAdd.Fch
                    oSortedCollection.Insert(i, oFraToAdd)
                    BlInserted = True
                    Exit For
                Case Is = oFraToAdd.Fch
                    Select Case oInvoice.Customer.FullNom
                        Case Is > oFraToAdd.Customer.FullNom
                            oSortedCollection.Insert(i, oFraToAdd)
                            BlInserted = True
                            Exit For
                        Case Is = oFraToAdd.Customer.FullNom
                            If oInvoice.Deliveries.First.Id > oFraToAdd.Deliveries.First.Id Then
                                oSortedCollection.Insert(i, oFraToAdd)
                                BlInserted = True
                                Exit For
                            End If
                    End Select
            End Select
        Next

        If Not BlInserted Then
            oSortedCollection.Add(oFraToAdd)
        End If

    End Sub

    Private Function Save() As Boolean
        Dim retval As Boolean
        LabelStatusSave.Text = "redactant factures..."
        Application.DoEvents()

        ProgressBarSave.Visible = True
        Dim oInvoices As List(Of DTOInvoice) = SortFacturas()

        Dim oExercici As DTOExercici = BLLExercici.FromYear(oInvoices.First.Fch.Year)

        Dim oLastInvoice As DTOInvoice = BLLInvoices.Last(oExercici, DTOInvoice.Series.Standard)
        Dim LastInvoiceNum As Integer
        If oLastInvoice IsNot Nothing Then LastInvoiceNum = oLastInvoice.Num
        Dim oLastRectificativa As DTOInvoice = BLLInvoices.Last(oExercici, DTOInvoice.Series.Rectificativa)
        Dim LastRectificativaNum As Integer
        If oLastRectificativa IsNot Nothing Then LastRectificativaNum = oLastRectificativa.Num

        'Dim oTaxes As List(Of DTOTax) = BLLTaxes.Closest(oFras(0).Fch)
        For Each oInvoice In oInvoices
            oInvoice.IsNew = True
            If oInvoice.Num = 0 Then
                Dim oTotal As DTOAmt = BLLInvoice.Total(oInvoice)
                If oTotal.IsNegative Then
                    LastRectificativaNum += 1
                    oInvoice.Serie = DTOInvoice.Series.Rectificativa
                    oInvoice.Num = LastRectificativaNum
                    oLastRectificativa = oInvoice
                Else
                    LastInvoiceNum += 1
                    oInvoice.Num = LastInvoiceNum
                    oLastInvoice = oInvoice
                End If
            End If
            oInvoices.Add(oInvoice)
        Next

        Dim sLabelFin As String = String.Format("(Finalitzat.{0} factures redactades satisfactoriament)", oInvoices.Count)
        Dim exs As New List(Of Exception)
        If BLLInvoices.Update(oInvoices, BLLSession.Current.User, AddressOf ShowProgress, exs) Then
            ShowProgress(0, oInvoices.Count, oInvoices.Count, sLabelFin, CancelRequest:=False)
            retval = True
        Else
            UIHelper.WarnError(exs)
        End If

        LabelStatusSave.Text = sLabelFin

        Return retval
    End Function

    Private Sub ShowProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        With ProgressBarSave
            .Minimum = min
            .Maximum = max
            .Value = value
        End With
        Application.DoEvents()
    End Sub



    Private Sub InsertFraInSortedFras(ByRef oInvoices As List(Of DTOInvoice), ByVal oInvoice As DTOInvoice)
        Dim i As Integer
        Dim DtFch As Date = oInvoice.Fch
        For i = 0 To oInvoices.Count - 1
            'si encuentra alguna mayor, insertala en su sitio
            If oInvoices(i).Fch > DtFch Then
                oInvoices.Insert(i, oInvoice)
                Exit Sub
            End If
        Next
        'si es la mayor, añadela al final
        oInvoices.Add(oInvoice)
    End Sub

    Private Function NextAlbForNextMes() As Boolean
        Static pNextAlbForNextMes As Boolean
        Static BlDone As Boolean
        If Not BlDone Then
            BlDone = True

            'si ja hem passat el final de mes
            Dim sMesCurrent As String = Format(Today, "yyyyMM")
            Dim sMesFrx As String = Format(DateTimePickerFirst.Value, "yyyyMM")
            If sMesCurrent > sMesFrx Then pNextAlbForNextMes = True

            'si, no havent-ho passat, ja fem albarans del següent mes
            Dim DtNextAlb As Date = BLL.BLLDefault.EmpValue(DTODefault.Codis.MinAlbDate)
            Dim sMesNextAlb As String = Format(DtNextAlb, "yyyyMM")
            If sMesNextAlb > sMesFrx Then pNextAlbForNextMes = True
        End If

        Return pNextAlbForNextMes
    End Function

#End Region

#Region "Wizard Common Events"

    Private Sub Wizard_AfterTabSelect()
        EnableNavButtons()
        Dim oTab As TabPage = TabControl1.SelectedTab
        Select Case oTab.Text
            Case TabPageCheck.Text
                RedactaFactures(DateTimePickerFirst.Value, DateTimePickerLast.Value, CheckBoxCredit.Checked, CheckBoxCash.Checked, CheckBoxExport.Checked, CheckBoxNegatives.Checked)
                'mAllowEventsBadClis = True
                'If mTbBadClis.Rows.Count = 0 Then
                TabControl1.SelectedTab = TabPageDist
                Wizard_AfterTabSelect()
                'End If
            Case TabPageDist.Text
                LoadClis()
                mAllowEventsClis = True
        End Select
    End Sub

    Private Sub Wizard_NavigateNext(ByVal oPageSource As TabPage, Optional ByRef oPageTarget As TabPage = Nothing)
        Select Case oPageSource.Text
            Case TabPageFch.Text
                'If RadioButtonFrxRecover.Checked Then
                'If TextBoxXMLpath.Text > "" Then
                'oPageTarget = TabPageDist
                'End If
                'End If
        End Select
    End Sub

    Private Sub Wizard_NavigatePrevious(ByVal oPageSource As TabPage, Optional ByRef oPageTarget As TabPage = Nothing)
        Select Case oPageSource.Text
            Case TabPageDist.Text
                'If RadioButtonFrxRecover.Checked Then
                'If TextBoxXMLpath.Text > "" Then
                'oPageTarget = TabPageFch
                'End If
                'End If
        End Select
    End Sub

    Private Sub Wizard_NavigateEnd()
        If Save() Then
        Else
            TabControl1.SelectedTab = TabPageDist
            EnableNavButtons()
            Wizard_AfterTabSelect()
        End If
    End Sub
#End Region

#Region "Wizard Common Code"
    'Codi comú a totes les wizards
    'es recomana no modificar
    'aquest codi fa crides a la regió Wizard Common Events,
    'on hi va el codi propietari

    Private Sub TabControl1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPrevious.Click
        If TabControl1.SelectedTab.Text = TabPageDist.Text Then
            'passa enrera factura a factura
            If SelectPreviousFra() Then Exit Sub
        End If

        'canvia de tab
        Dim oPageTarget As TabPage
        'oPageTarget = TabControl1.TabPages(TabControl1.SelectedIndex - 1)
        oPageTarget = TabControl1.TabPages(0)

        Wizard_NavigateNext(TabControl1.SelectedTab, oPageTarget)
        TabControl1.SelectedTab = oPageTarget
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNext.Click
        If TabControl1.SelectedTab.Text = TabPageDist.Text Then
            'passa factura a factura
            If SelectNextFra() Then Exit Sub
        End If

        'canvia de tab
        Dim oPageTarget As TabPage = TabControl1.TabPages(TabControl1.SelectedIndex + 1)
        Wizard_NavigateNext(TabControl1.SelectedTab, oPageTarget)
        TabControl1.SelectedTab = oPageTarget
        Wizard_AfterTabSelect()
    End Sub

    Private Sub ButtonEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonEnd.Click
        Static BlDone As Boolean

        If BlDone Then
            Me.Close()
        Else
            BlDone = True
            Wizard_NavigateEnd()
            ButtonEnd.Text = "SORTIDA"
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Function SelectPreviousFra() As Boolean
        Dim retVal As Boolean = False
        Dim iNodes As Integer = TreeViewFras.GetNodeCount(False)
        If iNodes > 0 Then
            Dim iNode As Integer = TreeViewFras.SelectedNode.Index
            If iNode > 1 Then
                iNode -= 1
                TreeViewFras.SelectedNode = TreeViewFras.Nodes(iNode)
                retVal = True
            Else
                retVal = SelectPreviousCli()
            End If
        End If
        Return retVal
    End Function

    Private Function SelectNextFra() As Boolean
        Dim iNodes As Integer = TreeViewFras.GetNodeCount(False)
        Dim iNode As Integer = TreeViewFras.SelectedNode.Index
        If iNode < (iNodes - 1) Then
            iNode += 1
            TreeViewFras.SelectedNode = TreeViewFras.Nodes(iNode)
        Else
            Return SelectNextCli()
        End If
        Return True
    End Function

    Private Function SelectPreviousCli() As Boolean
        Dim retVal As Boolean = False
        Dim oRow As DataGridViewRow = DataGridViewClis.CurrentRow
        If oRow IsNot Nothing Then
            Dim iRow As Integer = oRow.Index
            If iRow > 0 Then
                'DataGridViewClis.Rows(iRow + 1).Selected = True
                DataGridViewClis.CurrentCell = DataGridViewClis.Rows(iRow - 1).Cells(CliCols.Clx)
                retVal = True
            End If
        End If
        Return retVal
    End Function

    Private Function SelectNextCli() As Boolean
        Dim retval As Boolean
        Dim iRow As Integer = DataGridViewClis.CurrentRow.Index
        If iRow < (DataGridViewClis.Rows.Count - 1) Then
            'DataGridViewClis.Rows(iRow + 1).Selected = True
            DataGridViewClis.CurrentCell = DataGridViewClis.Rows(iRow + 1).Cells(CliCols.Clx)
            retval = True
        Else
        End If
        Return retval
    End Function

    Private Sub EnableNavButtons()
        Dim Min As Integer = 0
        Dim Max As Integer = TabControl1.TabPages.Count - 1
        Dim Idx As Integer = TabControl1.SelectedIndex

        ButtonPrevious.Enabled = (Idx > Min)
        ButtonNext.Enabled = (Idx < Max)
        ButtonEnd.Enabled = (Idx = Max)
    End Sub

#End Region


    Private Sub DateTimePickerFirst_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerFirst.ValueChanged
        DateTimePickerLast.Value = SetLastFch()
    End Sub




    Private Sub DataGridViewWarn_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewWarn.CellFormatting
        Select Case e.ColumnIndex
            Case BadClisCols.Ico
                Dim oRow As DataGridViewRow = DataGridViewWarn.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(BadClisCols.Err).Value) Then
                    e.Value = My.Resources.empty
                Else
                    Dim iCod As Integer = CInt(oRow.Cells(BadClisCols.Err).Value)
                    Select Case iCod
                        Case 0
                            e.Value = My.Resources.empty
                        Case Else
                            e.Value = My.Resources.wrong
                    End Select
                End If
        End Select
    End Sub

    Private Function CurrentBadCli() As Client
        Dim oCli As Client = Nothing
        Dim oRow As DataGridViewRow = DataGridViewWarn.CurrentRow
        If oRow IsNot Nothing Then
            Dim iCli As Integer = oRow.Cells(BadClisCols.Cli).Value
            oCli = MaxiSrvr.Client.FromNum(iCli)
        End If
        Return oCli
    End Function


    Private Sub SetContextMenuBadClis()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentBadCli()

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact.ToDTO)
            'AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        DataGridViewWarn.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridViewWarn_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewWarn.DoubleClick
        Dim oContact As DTOContact = CurrentBadCli().ToDTO
        Dim oFrm As New Frm_Contact(oContact)
        oFrm.Show()
    End Sub

    Private Sub DataGridViewWarn_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewWarn.SelectionChanged
        If mAllowEventsBadClis Then
            SetContextMenuBadClis()
        End If
    End Sub

    Private Sub CheckBoxFacturarTot_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxFacturarTot.CheckedChanged
        If CheckBoxFacturarTot.Checked Then
            CheckBoxCash.Checked = True
            CheckBoxCredit.Checked = True
            CheckBoxExport.Checked = True
            CheckBoxNegatives.Checked = True
            CheckBoxPreVto.Checked = True
            CheckBoxFraPerMes.Checked = True
            CheckBoxSmallVolume.Checked = True
        End If
    End Sub

    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxCash.CheckedChanged, CheckBoxCredit.CheckedChanged, CheckBoxExport.CheckedChanged, _
        CheckBoxNegatives.CheckedChanged, CheckBoxPreVto.CheckedChanged, CheckBoxFraPerMes.CheckedChanged, _
        CheckBoxSmallVolume.CheckedChanged

        Dim oCheckbox As CheckBox = sender
        If Not oCheckbox.Checked Then
            CheckBoxFacturarTot.Checked = False
        End If

    End Sub


End Class


