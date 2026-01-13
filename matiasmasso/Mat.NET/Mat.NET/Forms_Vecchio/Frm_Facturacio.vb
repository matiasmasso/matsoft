'Imports System.Xml


Public Class Frm_Facturacio
    Private mClientsFacturables As New ClientsFacturables
    Private mEmp As DTO.DTOEmp =BLL.BLLApp.Emp
    Private mFras As Fras
    'Private mDoc As XmlDocument
    Private mAlbs As Albs
    Private mAllowEvents As Boolean
    Private mAllowEventsBadClis As Boolean
    Private mAllowEventsClis As Boolean
    Private mTbBadClis As DataTable
    Private mDsClis As DataSet

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

    Public WriteOnly Property Albs() As Albs
        Set(ByVal Value As Albs)
            mAlbs = Value
        End Set
    End Property

    Private Sub Frm_Facturacio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TreeViewFras.ContextMenu = ContextMenuTvFras
        Dim oLastFra As Fra = MaxiSrvr.Fra.LastFra(mEmp)
        LabelLastFra.Text = oLastFra.Id & " del " & oLastFra.Fch
        Dim DtFirstFch As Date = oLastFra.Fch
        DateTimePickerFirst.Value = DtFirstFch

        LoadCfpCods()
        If mAlbs Is Nothing Then
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
        Dim oCodEpg As New CodEpg(10)
        Dim itm As Cod
        With ComboBoxCfp
            .ValueMember = "Id"
            .DisplayMember = "Nom"
            .Items.Add(New Cod(0, ""))
            For Each itm In oCodEpg.Cods
                .Items.Add(itm)
            Next
        End With
    End Sub



    Public Sub RedactaFactures(ByVal fromfch As Date, ByVal ToFch As Date, ByVal BlCredit As Boolean, ByVal BlCash As Boolean, ByVal BlExport As Boolean, ByVal BlNegatius As Boolean)
        Dim oAlbaransPerFacturar As Albs = Nothing
        If mAlbs Is Nothing Then
            oAlbaransPerFacturar = MaxiSrvr.Albs.PendentsDeFacturar(fromfch, ToFch, BlCredit, BlCash, BlExport)
        Else
            oAlbaransPerFacturar = mAlbs
        End If

        mClientsFacturables = New ClientsFacturables

        With ProgressBarDist
            .Minimum = 0
            .Maximum = oAlbaransPerFacturar.Count
            .Value = 0
            .Visible = True
            Application.DoEvents()
        End With

        Dim exs as new list(Of Exception)
        Dim oClientFacturable As New ClientFacturable
        Dim oLastFactura As Fra = Nothing
        For Each oAlb As Alb In oAlbaransPerFacturar
            ProgressBarDist.Increment(1)
            Application.DoEvents()
            If oAlb.Fch > ToFch Then
                If Not oClientFacturable.Client.Equals(oAlb.Client.CcxOrMe) Then
                    If oClientFacturable.Facturas.Count = 0 Then oClientFacturable.Facturable = False
                    oClientFacturable = mClientsFacturables.Add(oAlb.Client.CcxOrMe)
                End If
                oClientFacturable.AlbaransPerFacturar.Add(oAlb)
            Else
                If CanUseLastFactura(oLastFactura, oAlb) Then
                    oClientFacturable.addAlbToFactura(oLastFactura, oAlb, exs)
                Else
                    If Not oClientFacturable.Client.Equals(oAlb.Client.CcxOrMe) Then
                        If oClientFacturable.Facturas.Count = 0 Then oClientFacturable.Facturable = False
                        oClientFacturable = mClientsFacturables.Add(oAlb.Client.CcxOrMe)
                    End If
                    oLastFactura = oClientFacturable.addNewFactura(oAlb, fromfch, exs)
                End If
                SetFpg(oLastFactura, exs)
            End If
        Next

        If Not BlNegatius Then
            For i As Integer = mClientsFacturables.Count - 1 To 0 Step -1
                If mClientsFacturables(i).Total.Eur <= 0 Then mClientsFacturables(i).Facturable = False
            Next
        End If

        If Not CheckBoxPreVto.Checked Then 'treu la factura si el vto es igual al de la data següent a la maxima de facturacio
            For i As Integer = mClientsFacturables.Count - 1 To 0 Step -1
                Dim DtMaxFchFacturable As Date = DateTimePickerLast.Value
                If mClientsFacturables(i).Client.FormaDePago.Dias.Dias.Count > 0 Then
                    Dim oClient As Client = mClientsFacturables(i).Client
                    Dim DtVtoFromNextFch As Date = oClient.FormaDePago.Vto(DtMaxFchFacturable.AddDays(1))
                    Dim FlagExistFacturasFacturables As Boolean = False
                    For j As Integer = mClientsFacturables(i).Facturas.Count - 1 To 0 Step -1
                        Dim oFra As Fra = mClientsFacturables(i).Facturas(j)
                        If oFra.Vto = DtVtoFromNextFch Then
                            RemoveFraFromClient(oFra, mClientsFacturables(i))
                        Else
                            FlagExistFacturasFacturables = True
                        End If
                    Next

                    If Not FlagExistFacturasFacturables Then
                        mClientsFacturables(i).Facturable = False
                    End If
                End If
            Next
        End If

        If Not CheckBoxFraPerMes.Checked Then
            For i As Integer = mClientsFacturables.Count - 1 To 0 Step -1
                If mClientsFacturables(i).Client.CodAlbsXFra = Client.CodsAlbsXFra.UnaSolaFraMensual Then
                    mClientsFacturables(i).Facturable = False
                End If
            Next
        End If


        If Not CheckBoxSmallVolume.Checked Then
            For i As Integer = mClientsFacturables.Count - 1 To 0 Step -1
                If mClientsFacturables(i).Client.FormaDePago.Dias.Dias.Count = 0 Then 'exceptua els que tenen dia de pagament
                    If mClientsFacturables(i).Total.Eur < 100 Then mClientsFacturables(i).Facturable = False
                End If
            Next
        End If


        'treu factures de import zero
        For i As Integer = mClientsFacturables.Count - 1 To 0 Step -1
            Dim oCli As ClientFacturable = mClientsFacturables(i)
            For j As Integer = oCli.Facturas.Count - 1 To 0 Step -1
                Dim oFra As Fra = oCli.Facturas(j)

                'If oFra.Total.Eur = 0 Then
                If oFra.Albs.Sum.Eur = 0 Then
                    For k As Integer = oFra.Albs.Count - 1 To 0 Step -1
                        Dim oAlb As Alb = oFra.Albs(k)
                        RemoveAlbFromParent(oAlb, oFra, oCli)
                        InsertAlbOnPendents(oAlb, oCli)
                    Next
                End If
            Next
        Next

        ProgressBarDist.Visible = False

    End Sub

    Private Sub RemoveFraFromClient(ByVal oFra As Fra, ByVal oClient As ClientFacturable)
        Dim i As Integer

        For i = oFra.Albs.Count - 1 To 0 Step -1
            Dim oAlb As Alb = oFra.Albs(i)
            oFra.Albs.RemoveAt(i)
            InsertAlbOnPendents(oAlb, oClient)
        Next

        Dim oFras As Fras = oClient.Facturas
        For i = oFras.Count - 1 To 0 Step -1
            If oFras(i).Equals(oFra) Then
                oFras.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub

    Private Sub OldCalFacturar(ByVal oAlb As Alb)
        For i As Integer = mClientsFacturables.Count - 1 To 0 Step -1
            If mClientsFacturables(i).Client.Id = 5348 Then Stop
            Dim DtMaxFchFacturable As Date = DateTimePickerLast.Value
            If mClientsFacturables(i).Client.FormaDePago.Dias.Dias.Count > 0 Then
                Dim oClient As Client = mClientsFacturables(i).Client
                Dim DtMaxVtoFacturable As Date = oClient.FormaDePago.Vto(DtMaxFchFacturable)
                Dim FlagExistFacturasFacturables As Boolean = False
                For Each oFra As Fra In mClientsFacturables(i).Facturas
                    If oFra.Vto <= DtMaxVtoFacturable Then
                        FlagExistFacturasFacturables = True
                        Exit For
                    End If
                Next
                If Not FlagExistFacturasFacturables Then
                    mClientsFacturables(i).Facturable = False
                End If
            End If
        Next
    End Sub

    Private Function CanUseLastFactura(ByVal oLastFactura As Fra, ByVal oAlbPerFacturar As Alb) As Boolean
        Dim retVal As Boolean = True

        If oLastFactura Is Nothing Then
            retVal = False
        Else
            Dim oCcx As Client = oAlbPerFacturar.Client.CcxOrMe
            If oLastFactura.Client.Id <> oCcx.Id Then
                retVal = False
            Else
                Dim oFirstAlbLastFra As Alb = oLastFactura.Albs(0)

                Select Case oCcx.CodAlbsXFra
                    Case Client.CodsAlbsXFra.FraPerAlbara
                        retVal = False
                    Case Client.CodsAlbsXFra.JuntarAlbsPetits
                        If oAlbPerFacturar.Total.Eur > oCcx.SepararAlbsPorEncimaDe.Eur Then retVal = False
                    Case Client.CodsAlbsXFra.UnaSolaFraMensual
                End Select

                If oCcx.FrasIndependents Then
                    If oAlbPerFacturar.Client.Id <> oFirstAlbLastFra.Client.Id Then retVal = False
                End If

                If oAlbPerFacturar.CashCod <> DTO.DTOCustomer.CashCodes.credit Then retVal = False
                If oAlbPerFacturar.DtoPct <> oLastFactura.DtoPct Then retVal = False
                If oAlbPerFacturar.DppPct <> oLastFactura.DppPct Then retVal = False

                'separa albarans que canviin de venciment en aquells que tinguin dies de pagament
                Dim DtFchFacturacio As Date = DateTimePickerFirst.Value
                If oAlbPerFacturar.Fch > DtFchFacturacio Then DtFchFacturacio = oAlbPerFacturar.Fch
                Dim AlbVto As Date = oAlbPerFacturar.Vto(DtFchFacturacio)
                Dim BlDiasDePago As Boolean = (oCcx.FormaDePago.Dias.Dias.Count > 0)
                If BlDiasDePago Then
                    If AlbVto > oLastFactura.Vto Then retVal = True
                End If

            End If

        End If

        Return retVal
    End Function



    Private Sub LoadClis()

        With DataGridViewClis
            With .RowTemplate
                .Height = DataGridViewClis.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = GetCliDataSource(mClientsFacturables)
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

    Private Function GetCliDataSource(ByVal oClientsFacturables As ClientsFacturables) As DataView
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
        For Each oClientFacturable As ClientFacturable In oClientsFacturables
            If oClientFacturable.Facturable Then
                oRow = oTb.NewRow
                oRow("IDX") = idx
                oRow("FCH") = oClientFacturable.LastFch
                oRow("EUR") = oClientFacturable.Total.Eur
                oRow("CLI") = oClientFacturable.Client.Id
                oRow("CLX") = oClientFacturable.Client.Clx
                oTb.Rows.Add(oRow)
            End If
            idx += 1
        Next
        Return oTb.DefaultView
    End Function

    Private Sub DisplayFpg(ByVal oClientFacturable As ClientFacturable)
        Dim oCli As Client = oClientFacturable.Client
        LabelFpg.Text = oCli.FormaDePago.Text(oCli.Lang)
        Dim s As String = oCli.Client.TextJoinAlbs()
        Select Case oCli.Client.CodAlbsXFra
            Case Client.CodsAlbsXFra.FraPerAlbara
                s = s & "/una fra. per cada albará"
            Case Client.CodsAlbsXFra.JuntarAlbsPetits
                s = s & "/juntar només albs.petits"
            Case Client.CodsAlbsXFra.UnaSolaFraMensual
                s = s & "/una sola fra.mensual"
        End Select
        LabelJoinAlbs.Text = s
    End Sub


    Private Sub DisplayNodes(ByVal oClientFacturable As ClientFacturable, Optional ByVal iSelectedNode As Integer = -1)
        TreeViewFras.Nodes.Clear()

        If oClientFacturable IsNot Nothing Then
            Dim oNoFraNode As maxisrvr.TreeNodeObj = TreeNodeNoFra(oClientFacturable.AlbaransPerFacturar)
            TreeViewFras.Nodes.Add(oNoFraNode)

            Dim FirstNodeSelected As Boolean = iSelectedNode >= 0
            Dim oFraNode As maxisrvr.TreeNodeObj
            For Each oFra As Fra In oClientFacturable.Facturas
                oFraNode = TreeNodeFra(oFra)
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




    Private Function TreeNodeAlb(ByVal oAlb As Alb) As maxisrvr.TreeNodeObj
        Dim oNode As New maxisrvr.TreeNodeObj("", oAlb)
        Dim sTxt As String = NodeText(oNode)

        With oNode
            Select Case oAlb.Total.Eur
                Case Is < 0
                    .ImageIndex = AlbImgs.AlbFreeOfCharge
                Case 0
                    sTxt = sTxt & " s/carrec "
                    .ImageIndex = AlbImgs.AlbFreeOfCharge
                Case Else
                    Select Case oAlb.CashCod
                        Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa, DTO.DTOCustomer.CashCodes.Reembols, DTO.DTOCustomer.CashCodes.Diposit
                            .ImageIndex = AlbImgs.AlbCash
                        Case DTO.DTOCustomer.CashCodes.credit
                            .ImageIndex = AlbImgs.AlbStandard
                    End Select

                    If Not oAlb.Fpg.CliDefault Then
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

    Private Function TreeNodeNoFra(ByVal oAlbsPerFacturar As Albs) As maxisrvr.TreeNodeObj
        Dim oNode As New maxisrvr.TreeNodeObj("", oAlbsPerFacturar)

        With oNode
            .Text = NodeText(oNode)
            .ImageIndex = AlbImgs.ClosedBlank
            .SelectedImageIndex = AlbImgs.OpenBlank
            For Each oAlb As Alb In oAlbsPerFacturar
                .Nodes.Add(TreeNodeAlb(oAlb))
            Next
        End With

        Return oNode
    End Function

    Private Function TreeNodeFra(ByVal oFra As Fra) As maxisrvr.TreeNodeObj
        Dim oNode As New maxisrvr.TreeNodeObj("", oFra)

        With oNode
            .Text = NodeText(oNode)
            .ImageIndex = TreeNodeFraImageIndex(oFra)
            .SelectedImageIndex = TreeNodeFraImageIndex(oFra, True)
            For Each oAlb As Alb In oFra.Albs
                .Nodes.Add(TreeNodeAlb(oAlb))
            Next
        End With

        Return oNode
    End Function

    Private Function TreeNodeFraImageIndex(ByVal oFra As Fra, Optional ByVal BlSelectedImage As Boolean = False) As Integer
        Dim retval As Integer
        Select Case oFra.Total.Eur
            Case Is <= 0
                retval = IIf(BlSelectedImage, AlbImgs.OpenFreeOfCharge, AlbImgs.ClosedFreeOfCharge)
            Case Else
                Select Case oFra.Albs(0).CashCod
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
        DateTimePickerLast.Value = maxisrvr.GetEndMonth(DateTimePickerFirst.Value)
    End Sub


    Private Sub DateTimePickerVto_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerVto.ValueChanged
        If mAllowEvents Then
            Dim oFra As Fra = CType(CurrentFraNode.Obj, Fra)
            oFra.Vto = DateTimePickerVto.Value
            Dim exs as new list(Of Exception)
            SetFpg(oFra, exs)
            If exs.Count > 0 Then MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
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
        Dim oCli As Client = CurrentCliFacturable.Client
        root.ShowContact(oCli)
    End Sub


    Private Sub DataGridViewClis_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewClis.SelectionChanged
        If mAllowEventsClis Then
            ShowCli()
            SetContextMenuClis()
        End If

    End Sub



    Private Sub ShowCli()
        Dim oClientFacturable As ClientFacturable = CurrentCliFacturable()
        If Not oClientFacturable Is Nothing Then
            DisplayNodes(oClientFacturable)
            DisplayFpg(oClientFacturable)
        End If
    End Sub


    Private Sub DisplayFra()
        mAllowEvents = False
        Dim oCurrentFra As Fra = Nothing
        Dim oNode As maxisrvr.TreeNodeObj = CurrentNode()
        Select Case FraNodeType(oNode)
            Case FraNodeTypes.NoFra
            Case FraNodeTypes.Fra
                oCurrentFra = CType(oNode.Obj, Fra)
            Case FraNodeTypes.Alb
                Dim oNodeParent As maxisrvr.TreeNodeObj = CType(oNode.Parent, maxisrvr.TreeNodeObj)
                Select Case FraNodeType(oNodeParent)
                    Case FraNodeTypes.NoFra
                    Case FraNodeTypes.Fra
                        oCurrentFra = CType(oNodeParent.Obj, Fra)
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
                ComboBoxCfp.SelectedIndex = ComboBoxCfpIdx(.Cfp)
                DateTimePickerVto.Value = .Vto
                TextBoxFpg.Text = .Fpg
                TextBoxOb1.Text = .Ob1
                TextBoxOb2.Text = .Ob2
                TextBoxOb3.Text = .Ob3
                CheckBoxIva.Checked = .Client.CcxOrMe.IVA
                CheckBoxReq.Checked = .Client.CcxOrMe.REQ
            End With

        End If
        mAllowEvents = True
    End Sub

    Private Function ComboBoxCfpIdx(ByVal CodId As Integer) As Integer
        Dim itm As Cod
        Dim idx As Integer
        For Each itm In ComboBoxCfp.Items
            If itm.Id = CodId Then
                Exit For
            End If
            idx = idx + 1
        Next
        Return idx
    End Function



    Private Sub MenuItemTvFrasZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasZoom.Click
        Dim oNode As maxisrvr.TreeNodeObj = TreeViewFras.SelectedNode
        Select Case FraNodeType(oNode)
            Case FraNodeTypes.Alb
                Dim oAlb As Alb = CType(oNode.Obj, Alb)
                root.ShowAlb(oAlb)
            Case FraNodeTypes.Fra
                Dim oFra As Fra = CType(oNode.Obj, Fra)
                root.ShowFra(oFra)
        End Select
    End Sub

    Private Function FraNodeType(ByVal oNode As maxisrvr.TreeNodeObj) As FraNodeTypes
        Dim retVal As FraNodeTypes = FraNodeTypes.NotSet
        Dim oObj As Object = oNode.Obj
        If TypeOf (oObj) Is Albs Then
            retVal = FraNodeTypes.NoFra
        ElseIf TypeOf (oObj) Is Fra Then
            retVal = FraNodeTypes.Fra
        ElseIf TypeOf (oObj) Is Alb Then
            retVal = FraNodeTypes.Alb
        End If
        Return retVal
    End Function

    Private Sub ContextMenuTvFras_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenuTvFras.Popup
        Dim oNode As Windows.Forms.TreeNode = TreeViewFras.SelectedNode
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



    Private Function CurrentCliFacturable() As ClientFacturable
        Dim retVal As ClientFacturable = Nothing

        If DataGridViewClis.SelectedRows.Count = 1 Then
            Dim oRow As DataGridViewRow = DataGridViewClis.SelectedRows(0)
            Dim idx As Integer = CInt(oRow.Cells(CliCols.idx).Value)
            retVal = mClientsFacturables(idx)
        End If

        Return retVal
    End Function


    Private Function CurrentClisFacturables() As ClientsFacturables
        Dim oClis As New ClientsFacturables

        If DataGridViewClis.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridViewClis.SelectedRows
                Dim idx As Integer = CInt(oRow.Cells(CliCols.idx).Value)
                oClis.Add(mClientsFacturables(idx))
            Next
        Else
            Dim oClientFacturable As ClientFacturable = CurrentCliFacturable()
            If oClientFacturable IsNot Nothing Then
                oClis.Add(oClientFacturable)
            End If
        End If
        Return oClis
    End Function


    Private Sub SetContextMenuClis()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oClis As ClientsFacturables = CurrentClisFacturables()

        If oClis.Count >= 0 Then
            oMenuItem = New ToolStripMenuItem("retirar de facturació", My.Resources.del, AddressOf OnRemoveCli)
            oContextMenu.Items.Add(oMenuItem)
            If oClis.Count = 1 Then
                oContextMenu.Items.Add("-")
                Dim oMenu_Contact As New Menu_Contact(oClis(0).Client)
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
        mClientsFacturables(idx).Facturable = False
        DataGridViewClis.Rows.Remove(oRow)
    End Sub


    Private Sub MenuItemCliRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oTvFraNode As Windows.Forms.TreeNode
        Dim oTvAlbNode As Windows.Forms.TreeNode
        Dim oTvNoFraNode As Windows.Forms.TreeNode = TreeViewFras.Nodes(0)

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
        Dim oNode As Windows.Forms.TreeNode = TreeViewFras.SelectedNode
        Dim oNoFraNode As Windows.Forms.TreeNode = TreeViewFras.Nodes(0)

        Select Case FraNodeType(oNode)
            Case FraNodeTypes.Alb
                RemoveAlbNodeFromParent(oNode, oNode.Parent)
                InsertAlbOnPendents(oNode)
            Case FraNodeTypes.Fra
                RemoveFraNode(oNode)
        End Select

        TreeViewFras.ExpandAll()
    End Sub

    Private Sub RemoveFraNode(ByVal oNodeFra As maxisrvr.TreeNodeObj)
        Do While oNodeFra.Nodes.Count > 0
            Dim oNodeAlb As maxisrvr.TreeNodeObj = oNodeFra.Nodes(0)
            RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)
            InsertAlbOnPendents(oNodeAlb)
        Loop
        'For Each oNodeAlb As maxisrvr.TreeNodeObj In oNodeFra.Nodes
        'RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)
        'InsertAlbOnPendents(oNodeAlb)
        'Next

        Dim oFra As Fra = CType(oNodeFra.Obj, Fra)
        Dim oFras As Fras = CurrentCliFacturable.Facturas
        For i As Integer = 0 To oFras.Count - 1
            If oFras(i).Equals(oFra) Then
                oFras.RemoveAt(i)
            End If
        Next

        TreeViewFras.Nodes.Remove(oNodeFra)
    End Sub

    Private Sub InsertFraOnCli(ByVal oAlbNodeToInsert As MaxiSrvr.TreeNodeObj) 'As Integer
        Dim exs as new list(Of Exception)
        Dim oClientFacturable As ClientFacturable = CurrentCliFacturable()
        Dim oAlbToInsert As Alb = CType(oAlbNodeToInsert.Obj, Alb)
        Dim oNewFra As Fra = oClientFacturable.addNewFactura(oAlbToInsert, DateTimePickerFirst.Value, exs)

        Dim oOldParent As MaxiSrvr.TreeNodeObj = oAlbNodeToInsert.Parent
        If TypeOf (oOldParent.Obj) Is Albs Then
            oClientFacturable.AlbaransPerFacturar.Remove(oAlbToInsert)
        Else
            Dim oOldFra As Fra = CType(oOldParent.Obj, Fra)
            oOldFra.Albs.Remove(oAlbToInsert)
            If oOldFra.Albs.Count = 0 Then
                oOldParent.Remove()
            Else
                'oOldFra.Calc
            End If
        End If

        DisplayNodes(CurrentCliFacturable)
    End Sub

    Private Sub RemoveAlbNodeFromParent(ByVal oAlbNodeToRemove As maxisrvr.TreeNodeObj, ByVal oFromParentNode As maxisrvr.TreeNodeObj)
        Dim oAlbToRemove As Alb = CType(oAlbNodeToRemove.Obj, Alb)
        oFromParentNode.Nodes.Remove(oAlbNodeToRemove)
        RemoveAlbFromParent(oAlbToRemove, oFromParentNode.Obj, CurrentCliFacturable)
        FraCalc(oFromParentNode)

        If oFromParentNode.Nodes.Count = 0 Then
            If FraNodeType(oFromParentNode) = FraNodeTypes.Fra Then
                RemoveFraNode(oFromParentNode)
            End If
        End If

    End Sub

    Private Sub RemoveAlbFromParent(ByVal oAlb As Alb, ByVal oParent As Object, ByVal oCli As ClientFacturable)
        If TypeOf (oParent) Is Albs Then
            Dim oAlbs As Albs = CType(oParent, Albs)
            oAlbs.Remove(oAlb)
        ElseIf TypeOf (oParent) Is Fra Then
            Dim oFra As Fra = CType(oParent, Fra)
            oFra.Albs.Remove(oAlb)
            If oFra.Albs.Count = 0 Then
                Dim oFras As Fras = oCli.Facturas
                For i As Integer = oFras.Count - 1 To 0 Step -1
                    If oFras(i).Equals(oFra) Then
                        oFras.RemoveAt(i)
                        Exit For
                    End If
                Next
            End If
        End If

    End Sub

    Private Sub FraCalc(ByVal oNode As maxisrvr.TreeNodeObj)
        If FraNodeType(oNode) = FraNodeTypes.Fra Then

        End If
        oNode.Text = NodeText(oNode)
    End Sub

    Private Function NodeText(ByVal oNode As maxisrvr.TreeNodeObj) As String
        Dim s As String = ""

        Select Case FraNodeType(oNode)
            Case FraNodeTypes.NoFra
                s = "pendents de facturar"
                Dim oAmt As New maxisrvr.Amt
                Dim oAlbs As Albs = CType(oNode.Obj, Albs)
                If oAlbs.Count > 0 Then
                    For Each oAlb As Alb In oAlbs
                        oAmt.Add(oAlb.Total)
                    Next
                    s = s & " " & oAmt.CurFormat
                End If

            Case FraNodeTypes.Fra
                s = FraNodeText(CType(oNode.Obj, Fra))
            Case FraNodeTypes.Alb
                s = AlbNodeText(CType(oNode.Obj, Alb))
        End Select
        Return s
    End Function

    Private Function FraNodeText(ByVal oFra As Fra) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("fra.")
        If oFra.Id > 0 Then sb.Append(oFra.Id.ToString)
        sb.Append(" del " & FchLastAlb(oFra).ToShortDateString)
        sb.Append(" vto." & oFra.Vto.ToShortDateString)
        sb.Append(" per " & FraTotal(oFra).CurFormat)

        Select Case oFra.Cfp
            Case DTO.DTOCustomer.CashCodes.Reembols, DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa, DTO.DTOCustomer.CashCodes.Diposit
                sb.Append(" (Cash)")
        End Select

        Dim s As String = sb.ToString
        Return s
    End Function

    Private Function AlbNodeText(ByVal oAlb As Alb) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("alb." & oAlb.Id)
        sb.Append(" del " & oAlb.Fch.ToShortDateString)
        If oAlb.Client.Referencia > "" Then
            sb.Append(" (" & oAlb.Client.Referencia & ")")
        End If
        sb.Append(" per " & oAlb.Total.CurFormat)

        Select Case oAlb.CashCod
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

    Private Function FraTotal(ByVal oFra As Fra) As maxisrvr.Amt
        Dim oAmt As New maxisrvr.Amt
        For Each oAlb As Alb In oFra.Albs
            oAmt.Add(oAlb.Total)
        Next
        Return oAmt
    End Function

    Private Function FchLastAlb(ByVal oFra As Fra) As Date
        Dim DtFch As Date = DateTimePickerFirst.Value
        For Each oAlb As Alb In oFra.Albs
            If oAlb.Fch > DtFch Then DtFch = oAlb.Fch
        Next
        Return DtFch
    End Function

    Private Sub MenuItemTvFrasNewFra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasNewFra.Click
        Dim oNodeAlb As maxisrvr.TreeNodeObj = CType(TreeViewFras.SelectedNode, maxisrvr.TreeNodeObj)

        RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)

        Dim oAlb As Alb = CType(oNodeAlb.Obj, Alb)
        Dim exs as new list(Of Exception)
        Dim oFra As Fra = CurrentCliFacturable.addNewFactura(oAlb, DateTimePickerFirst.Value, exs)
        Dim oNodeFra As maxisrvr.TreeNodeObj = TreeNodeFra(oFra)

        InsertFraNode(oNodeFra)
    End Sub

    Private Sub FacturarEn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenu As Windows.Forms.MenuItem = CType(sender, Windows.Forms.MenuItem)
        FacturarEn(TreeViewFras.SelectedNode, oMenu.Index)
    End Sub

    Private Sub FacturarEn(ByVal oTvNode As maxisrvr.TreeNodeObj, ByVal NewFraIdx As Integer)
        Dim oNodeAlb As maxisrvr.TreeNodeObj = CType(TreeViewFras.SelectedNode, maxisrvr.TreeNodeObj)
        Dim oNodeFra As maxisrvr.TreeNodeObj = CType(TreeViewFras.Nodes(NewFraIdx), maxisrvr.TreeNodeObj)

        RemoveAlbNodeFromParent(oNodeAlb, oNodeAlb.Parent)
        InsertAlbOnFra(oNodeAlb, oNodeFra)
    End Sub

    Private Sub InsertFraNode(ByVal oFraNode As maxisrvr.TreeNodeObj)
        Dim oFra As Fra = CType(oFraNode.Obj, Fra)
        Dim FlagInserted As Boolean = False
        For i As Integer = 1 To TreeViewFras.Nodes.Count - 1
            Dim oNode As maxisrvr.TreeNodeObj = TreeViewFras.Nodes(i)
            Dim oFraItm As Fra = CType(oNode.Obj, Fra)
            If oFraItm.Fch > oFra.Fch Then
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

    Private Sub InsertAlbOnFra(ByVal oAlbNodeToInsert As maxisrvr.TreeNodeObj, ByVal oFraNode As maxisrvr.TreeNodeObj)
        Dim oAlbToInsert As Alb = CType(oAlbNodeToInsert.Obj, Alb)
        Dim oFra As Fra = CType(oFraNode.Obj, Fra)
        Dim BlInserted As Boolean = False

        For i As Integer = 0 To oFraNode.Children.Count - 1
            Dim oNodeAlb As maxisrvr.TreeNodeObj = CType(oFraNode.Children(i), maxisrvr.TreeNodeObj)
            Dim oAlb As Alb = CType(oNodeAlb.Obj, Alb)
            If oAlbToInsert.Id > oAlb.Id Then
                oFraNode.Nodes.Insert(i, oAlbNodeToInsert)
                oFra.Albs.Insert(i, oAlbToInsert)
                FraCalc(oFraNode)
                BlInserted = True
                Exit For
            End If
        Next

        If Not BlInserted Then
            oFraNode.Nodes.Add(oAlbNodeToInsert)
            oFra.Albs.Add(oAlbToInsert)
            FraCalc(oFraNode)
            oFraNode.ExpandAll()
        End If

    End Sub


    Private Sub InsertAlbOnPendents(ByVal oAlbNodeToInsert As maxisrvr.TreeNodeObj)
        Dim oAlbToInsert As Alb = CType(oAlbNodeToInsert.Obj, Alb)
        Dim BlInserted As Boolean = False

        Dim oNoFraNode As maxisrvr.TreeNodeObj = TreeViewFras.Nodes(0)
        For i As Integer = 0 To oNoFraNode.Children.Count - 1
            Dim oNodeAlb As maxisrvr.TreeNodeObj = CType(oNoFraNode.Children(i), maxisrvr.TreeNodeObj)
            Dim oAlb As Alb = CType(oNodeAlb.Obj, Alb)
            If oAlbToInsert.Id > oAlb.Id Then
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

    Private Sub InsertAlbOnPendents(ByVal oAlbToInsert As Alb, ByVal oCli As ClientFacturable)
        Dim BlInserted As Boolean = False

        Dim oAlbs As Albs = oCli.AlbaransPerFacturar
        For i As Integer = 0 To oAlbs.Count - 1
            Dim oAlb As Alb = oAlbs(i)
            If oAlbToInsert.Id > oAlb.Id Then
                oAlbs.Insert(i, oAlbToInsert)
                'FraCalc(oNoFraNode)
                BlInserted = True
                Exit For
            End If
        Next

        If Not BlInserted Then
            oAlbs.Add(oAlbToInsert)
            'FraCalc(oNoFraNode)
        End If

    End Sub




    Private Sub MenuItemTvFrasFacturarEn_Select(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasFacturarEn.Select
        Dim OldFraIdx As Integer = TreeViewFras.SelectedNode.Parent.Index
        Dim oMnuItm As Windows.Forms.MenuItem

        MenuItemTvFrasFacturarEn.MenuItems.Clear()
        MenuItemTvFrasFacturarEn.MenuItems.Add(MenuItemTvFrasNewFra)

        For i As Integer = 1 To TreeViewFras.Nodes.Count - 1
            Dim oNode As maxisrvr.TreeNodeObj = TreeViewFras.Nodes(i)
            oMnuItm = MenuItemTvFrasFacturarEn.MenuItems.Add(oNode.Text, New EventHandler(AddressOf FacturarEn_Click))
            If i = OldFraIdx Then oMnuItm.Enabled = False
        Next

    End Sub


    Private Function CurrentFraNode() As maxisrvr.TreeNodeObj
        Dim oNode As maxisrvr.TreeNodeObj = Nothing

        Select Case FraNodeType(CurrentNode)
            Case FraNodeTypes.Alb
                oNode = CType(CurrentNode.Parent, maxisrvr.TreeNodeObj)
            Case FraNodeTypes.Fra
                oNode = CurrentNode()
        End Select

        Return oNode
    End Function

    Private Function CurrentNode() As maxisrvr.TreeNodeObj
        Dim oNode As maxisrvr.TreeNodeObj = TreeViewFras.SelectedNode
        Return oNode
    End Function

    Private Sub TreeViewFras_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewFras.AfterSelect
        Dim oNode As maxisrvr.TreeNodeObj = CurrentNode()

        DisplayFra()
    End Sub



    Private Sub ComboBoxCfp_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxCfp.SelectedValueChanged
        If mAllowEvents Then
            If ComboBoxCfp.SelectedIndex >= 0 Then
                Dim oFra As Fra = CurrentFraNode.Obj
                oFra.Cfp = CurrentCfp()

                Dim exs as new list(Of Exception)
                SetFpg(oFra, exs)
                If exs.Count > 0 Then MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
                DisplayFra()
            End If
        End If
    End Sub

    Private Sub SetFpg(ByVal oFra As Fra, ByRef exs as List(Of exception))
        Dim oFirstAlb As Alb = oFra.Albs(0)
        Dim oLang As DTOLang = DTOLang.FromTag(oFra.Client.Lang.Id.ToString)

        Select Case oFra.Cfp
            Case DTOCustomer.FormasDePagament.Comptat
                Select Case oFirstAlb.CashCod
                    Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia
                        oFra.Fpg = oLang.Tradueix("transferencia previa", "transferència prèvia", "bank transfer")
                    Case DTO.DTOCustomer.CashCodes.Visa
                        oFra.Fpg = oLang.Tradueix("tarjeta de crédito", "tarja de crèdit", "credit card")
                    Case DTO.DTOCustomer.CashCodes.Reembols
                        oFra.Fpg = oLang.Tradueix("contra reembolso", "contra reemborsament", "cash against goods")
                    Case DTO.DTOCustomer.CashCodes.Diposit
                        oFra.Fpg = oLang.Tradueix("a deducir de depósito a su favor", "a deduir de diposit al seu favor", "to deduct from existing diposit")
                    Case DTO.DTOCustomer.CashCodes.credit
                        exs.Add(New Exception("alb." & oFirstAlb.Id.ToString & " a credit facturat com cobrat"))
                End Select
            Case Else
                Dim oFormadepago As FormaDePago = oFra.Client.FormaDePago
                oFormadepago.Cod = oFra.Cfp
                oFra.Fpg = oFormadepago.Text(oFra.Client.Lang, oFra.Vto.ToShortDateString)
                Select Case oFra.Cfp
                    Case DTOCustomer.FormasDePagament.DomiciliacioBancaria, DTOCustomer.FormasDePagament.EfteAndorra
                        Dim oIban As DTOIban = oFra.Client.FormaDePago.Iban
                        If oIban Is Nothing Then
                            exs.Add(New Exception("falta Iban de " & oFra.Client.Clx))
                        Else
                            Dim oBank As DTOBank = BLL.BLLIban.Bank(oIban)
                            If oBank Is Nothing Then
                                exs.Add(New Exception("Iban mal entrat o desconegut de " & oFra.Client.Clx))
                            Else
                                oFra.Ob1 = oBank.RaoSocial
                                If oIban.BankBranch IsNot Nothing Then
                                    oFra.Ob2 = BLL.BLLIban.BranchLocationAndAdr(oIban)
                                End If
                                Dim sDigits As String = oIban.Digits
                                If sDigits.Length > 4 Then sDigits = "..." & sDigits.Substring(sDigits.Length - 4)
                                oFra.Ob3 = BLL.BLLIban.LastDigits(oIban, oLang)
                            End If
                        End If
                End Select
                If oFirstAlb.CashCod <> DTO.DTOCustomer.CashCodes.credit Then
                    exs.Add(New Exception("alb." & oFirstAlb.Id.ToString & " cobrat i facturat a credit"))
                End If
        End Select

    End Sub



    Private Function CurrentCfp() As DTOCustomer.FormasDePagament
        Dim oCod As MaxiSrvr.Cod = CType(ComboBoxCfp.SelectedItem, MaxiSrvr.Cod)
        Dim oCfp As DTOCustomer.FormasDePagament = oCod.Id
        Return oCfp
    End Function

    Private Sub TextBoxOb1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxOb1.TextChanged
        If mAllowEvents Then
            Dim oFra As Fra = CType(CurrentFraNode.Obj, Fra)
            oFra.Ob1 = TextBoxOb1.Text
        End If
    End Sub

    Private Sub TextBoxOb2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxOb2.TextChanged
        If mAllowEvents Then
            Dim oFra As Fra = CType(CurrentFraNode.Obj, Fra)
            oFra.Ob2 = TextBoxOb2.Text
        End If
    End Sub

    Private Sub TextBoxOb3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxOb3.TextChanged
        If mAllowEvents Then
            Dim oFra As Fra = CType(CurrentFraNode.Obj, Fra)
            oFra.Ob3 = TextBoxOb3.Text
        End If
    End Sub

    Private Sub TextBoxFpg_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxFpg.TextChanged
        If mAllowEvents Then
            Dim oFra As Fra = CType(CurrentFraNode.Obj, Fra)
            oFra.Fpg = TextBoxFpg.Text
        End If
    End Sub

    Private Sub MenuItemTvFrasSetNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTvFrasSetNum.Click
        Dim oNode As maxisrvr.TreeNodeObj = CurrentFraNode()
        Dim oFra As Fra = CType(oNode.Obj, Fra)
        Dim oFrm As New Frm_FraNumFch(oFra)
        With oFrm
            .ShowDialog()
            If Not .Cancel Then
                'If .FraNum > 0 Then
                oFra.SetId(.FraNum)
                'End If
                If .FraFch > Date.MinValue Then
                    oFra.Fch = .FraFch
                    DateTimePickerFirst.Value = .FraFch
                Else
                    oFra.Fch = Date.MinValue
                End If
                DisplayNodes(CurrentCliFacturable)
            End If
        End With

    End Sub


#End Region

#Region "Finalitzar"

    Private Function SortFacturas() As Fras
        Dim oFras As New Fras

        For Each oClientFacturable As ClientFacturable In mClientsFacturables
            If oClientFacturable.Facturable Then
                For Each oFra As Fra In oClientFacturable.Facturas
                    AddFraToSortedCollection(oFras, oFra)
                Next
            End If
        Next

        Return oFras
    End Function

    Private Sub AddFraToSortedCollection(ByRef oSortedCollection As Fras, ByVal oFraToAdd As Fra)
        Dim BlInserted As Boolean = False
        For i As Integer = 0 To oSortedCollection.Count - 1
            Dim oFra As Fra = oSortedCollection(i)
            Select Case oFra.Fch
                Case Is > oFraToAdd.Fch
                    oSortedCollection.Insert(i, oFraToAdd)
                    BlInserted = True
                    Exit For
                Case Is = oFraToAdd.Fch
                    Select Case oFra.Client.Clx
                        Case Is > oFraToAdd.Client.Clx
                            oSortedCollection.Insert(i, oFraToAdd)
                            BlInserted = True
                            Exit For
                        Case Is = oFraToAdd.Client.Clx
                            If oFra.Albs(0).Id > oFraToAdd.Albs(0).Id Then
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

    Private Function Save() As Fras
        LabelStatusSave.Text = "redactant factures..."
        Application.DoEvents()

        Dim oFras As MaxiSrvr.Fras = SortFacturas()

        With ProgressBarSave
            .Minimum = 0
            .Maximum = oFras.Count
            .Visible = True
        End With

        'If NextAlbForNextMes() Then SetIncentius()
        Dim oFra As MaxiSrvr.Fra
        For Each oFra In oFras
            LabelStatusSave.Text = oFra.Fch & " " & oFra.Client.Clx
            Dim exs as New List(Of exception)
            If oFra.Update( exs, root.Usuari) Then
                'root.RedactaFactura(oFra)
                ProgressBarSave.Increment(1)
            Else
                MsgBox("error al facturar factura " & oFra.Id & " de " & oFra.Client.Clx & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        Next
        LabelStatusSave.Text = "(finalitzat)"

        Return oFras
    End Function


    Private Function IsLastFraDelMes(ByVal LngCli As Long, ByVal LastFraFch As Date) As Boolean
        If LastFraFch = Date.MinValue Then
            Return True
        Else
            Dim SQL As String = "SELECT MAX(fch) AS LASTFRAFCH " _
            & "FROM Fra " _
            & "WHERE Emp =" & mEmp.Id & " AND " _
            & "yea =" & LastFraFch.Year & " AND " _
            & "cli =" & LngCli & " AND " _
            & "cfp <>3 AND " _
            & "MONTH(fch)=" & LastFraFch.Month & " AND " _
            & "fch > '" & Format(LastFraFch, "yyyyMMdd") & "'"

            Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
            oDrd.Read()
            IsLastFraDelMes = IsDBNull(oDrd("LASTFRAFCH"))
            oDrd.Close()
        End If
    End Function

    Private Sub InsertFraInSortedFras(ByRef oFras As MaxiSrvr.Fras, ByVal oFra As MaxiSrvr.Fra)
        Dim i As Integer
        Dim DtFch As Date = oFra.Fch
        For i = 0 To oFras.Count - 1
            'si encuentra alguna mayor, insertala en su sitio
            If oFras(i).Fch > DtFch Then
                oFras.Insert(i, oFra)
                Exit Sub
            End If
        Next
        'si es la mayor, añadela al final
        oFras.Add(oFra)
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
        Dim oFras As Fras = Save()
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
            oCli = MaxiSrvr.Client.FromNum(mEmp, iCli)
        End If
        Return oCli
    End Function


    Private Sub SetContextMenuBadClis()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentBadCli()

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            'AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        DataGridViewWarn.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridViewWarn_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewWarn.DoubleClick
        Dim oContact As Contact = CurrentBadCli()
        root.ShowContact(oContact)
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


