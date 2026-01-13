Public Class Xl_Rep_RepComPncCheck
    Private _LiniesDeComandaPendentsDeLiquidar As List(Of DTOPurchaseOrderItem)
    Private _RepProducts As List(Of DTORepProduct)
    Private _RepCliComs As List(Of DTORepCliCom)

    Private _ControlItems As ControlItems
    Private _CancelRequest As Boolean
    Private _AllowEvents As Boolean

    Private Enum Cols
        Pdc
        Lin
        Fch
        ArtNom
        CliNom
        RepNom1
        Com1
        RepNom2
        Com2
    End Enum

    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        _LiniesDeComandaPendentsDeLiquidar = BLL.BLLPurchaseOrderItems.PendentsDeLiquidacioRep(BLL.BLLApp.Emp)
        _RepProducts = BLL.BLLRepProducts.All(BLL.BLLApp.Emp, , True)
        _RepCliComs = BLL.BLLRepCliComs.All(BLL.BLLApp.Emp)

        _ControlItems = New ControlItems
        _CancelRequest = False
        PanelBottomBar.Visible = True
        ButtonStart.Enabled = False

        LoadConflictes()
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add("Comanda", "Comanda")
            .Columns.Add("Linia", "Linia")
            .Columns.Add("Data", "Data")
            .Columns.Add("Producte", "Producte")
            .Columns.Add("Client", "Client")
            .Columns.Add("RepNom1", "RepNom1")
            .Columns.Add("Com1", "Com1")
            .Columns.Add("RepNom2", "RepNom2")
            .Columns.Add("Com2", "Com2")

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            With .Columns(Cols.Pdc)
                .HeaderText = "Comanda"
                .DataPropertyName = "Comanda"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Lin)
                .HeaderText = "Linia"
                .DataPropertyName = "Linia"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "Producte"
                .DataPropertyName = "Producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.CliNom)
                .HeaderText = "Client"
                .DataPropertyName = "Client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.RepNom1)
                .HeaderText = "assignat"
                .DataPropertyName = "RepOriginal"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Com1)
                .HeaderText = "comisió"
                .DataPropertyName = "ComOriginal"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.00 %;-0.00 %;#"
            End With
            With .Columns(Cols.RepNom2)
                .HeaderText = "suggerit"
                .DataPropertyName = "RepSuggerit"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Com2)
                .HeaderText = "comisió"
                .DataPropertyName = "ComSuggerida"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.00 %;-0.00 %;#"
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem)
        Return retval
    End Function

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentItem()

        If oControlItem IsNot Nothing Then
            If SelectedItems.Count = 1 Then
                Dim oMenuItem As ToolStripMenuItem = oContextMenu.Items.Add("Client")
                Dim oContact As New Contact(oControlItem.LineItmPnc.PurchaseOrder.Customer.Guid)
                Dim oMenu_Contact As New Menu_Contact(oContact)
                oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
            End If
            oContextMenu.Items.Add("Acceptar canvis", Nothing, AddressOf Do_SubmitChanges)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_SubmitChanges(sender As Object, e As EventArgs)
        Dim oItems As ControlItems = SelectedItems()
        Dim exs As New List(Of exception)
        If oItems.SubmitChanges(exs) Then
            _ControlItems.Remove(oItems)
            LoadGrid()
        Else
            MsgBox("error al actualitzar la comisió de representant" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub LoadConflictes()
        Dim retval As New LineItmPncs
        Dim oSuggestedRepCom As DTORepCom = Nothing

        PanelBottomBar.Visible = True
        With ProgressBar1
            .Maximum = _LiniesDeComandaPendentsDeLiquidar.Count - 1
            .Value = 0
        End With

        Dim exs As New List(Of Exception)
        For Each oItem As DTOPurchaseOrderItem In _LiniesDeComandaPendentsDeLiquidar
            'If oItem.PurchaseOrder.Id = 16678 And oItem.Lin = 2 Then Stop

            If BLL.BLLPurchaseOrderItem.RepComConflicte(oItem, _RepProducts, _RepCliComs, oSuggestedRepCom, exs) Then
                Dim oControlItem As New ControlItem(oItem, oSuggestedRepCom)
                _ControlItems.Add(oControlItem)
                LabelCount.Text = _ControlItems.Count
            End If
            ProgressBar1.Increment(1)
            Application.DoEvents()
            If _CancelRequest Then Exit For
        Next

        If exs.Count > 0 Then
            UIHelper.WarnError(exs, "relació de errors trobats:")
        End If

        PanelBottomBar.Visible = False
        ButtonStart.Enabled = True
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As ControlItem = oRow.DataBoundItem
        Select Case oItem.Updated
            Case True
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _CancelRequest = True
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Protected Class ControlItem
        Public Property LineItmPnc As DTOPurchaseOrderItem
        Public Property SuggestedRepCom As DTORepCom
        Public Property Updated As Boolean

        Public Property Comanda As Integer = 0
        Public Property Linia As Integer = 0
        Public Property Fch As Date = Today
        Public Property Producte As String = ""
        Public Property Client As String = ""
        Public Property RepOriginal As String = ""
        Public Property ComOriginal As Decimal = 0
        Public Property RepSuggerit As String = ""
        Public Property ComSuggerida As Decimal = 0

        Public Sub New(oSrc As DTOPurchaseOrderItem, ByRef oSuggestedRepCom As DTORepCom)
            MyBase.New()
            _LineItmPnc = oSrc
            _SuggestedRepCom = oSuggestedRepCom

            _Comanda = _LineItmPnc.PurchaseOrder.Id
            _Linia = _LineItmPnc.Lin
            _Fch = _LineItmPnc.PurchaseOrder.Fch
            _Producte = BLL.BLLProduct.FullNom(_LineItmPnc.Sku)
            _Client = _LineItmPnc.PurchaseOrder.Customer.FullNom

            If _LineItmPnc.RepCom IsNot Nothing Then
                _RepOriginal = _LineItmPnc.RepCom.Rep.Nom
                _ComOriginal = _LineItmPnc.RepCom.Com / 100
            End If

            If oSuggestedRepCom IsNot Nothing Then
                _RepSuggerit = oSuggestedRepCom.Rep.Nom
                _ComSuggerida = oSuggestedRepCom.Com / 100
            End If

        End Sub

        Public Function SubmitChanges(ByRef exs As List(Of Exception)) As Boolean
            _LineItmPnc.RepCom = _SuggestedRepCom
            Dim retval As Boolean = BLL.BLLPurchaseOrderItem.UpdateRepCom(_LineItmPnc, exs)

            If _LineItmPnc.RepCom Is Nothing Then
                _RepOriginal = ""
                _ComOriginal = 0
            Else
                _RepOriginal = _LineItmPnc.RepCom.Rep.Nom
                _ComOriginal = _LineItmPnc.RepCom.Com / 100
            End If

            _Updated = retval
            Return retval
        End Function
    End Class

    Protected Class ControlItems

        Inherits System.Collections.CollectionBase

        Public Sub Add(ByVal NewObjMember As ControlItem)
            List.Add(NewObjMember)
        End Sub

        Public Sub Remove(ByVal oObjectsToRemove As ControlItems)
            'For Each oItem As ControlItem In List
            'List.Remove(oItem)
            'Next
        End Sub

        Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As ControlItem
            Get
                Item = List.Item(vntIndexKey)
            End Get
        End Property

        Public Function SubmitChanges(ByRef exs As List(Of Exception)) As Boolean
            Dim retval As Boolean = True
            For Each oItem As ControlItem In List
                If Not oItem.SubmitChanges(exs) Then
                    retval = False
                End If
            Next
            Return retval
        End Function
    End Class

    Private Sub ButtonUpdateArc_Click(sender As Object, e As EventArgs) Handles ButtonUpdateArc.Click
        Dim iCount As Integer
        Dim exs As New List(Of exception)
        If RepComLoader.SyncArcWithPnc(iCount, exs) Then
            If iCount = 0 Then
                MsgBox("no s'ha trobat cap inconsistencia des de primers de 2013", MsgBoxStyle.Information)
            Else
                MsgBox("actualitzades " & iCount & " linies d'albará", MsgBoxStyle.Information)
            End If
        Else
            UIHelper.WarnError(exs, "error al actualitzar les linies d'albará")
        End If
    End Sub

    Private Sub ButtonSyncRps_Click(sender As Object, e As EventArgs) Handles ButtonSyncRps.Click
        Dim i As Integer = RepComLoader.SyncRpsWithArc
        If i = 0 Then
            MsgBox("no s'ha trobat cap inconsistencia des de primers de 2013", MsgBoxStyle.Information)
        Else
            MsgBox("actualitzades " & i & " factures pendents de liquidar", MsgBoxStyle.Information)
        End If
    End Sub
End Class




