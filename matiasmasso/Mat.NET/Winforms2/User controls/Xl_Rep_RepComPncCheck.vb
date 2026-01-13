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

    Private Async Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        Dim exs As New List(Of Exception)
        PanelBottomBar.Visible = True
        _RepProducts = Await FEB.RepProducts.All(exs, Current.Session.Emp, , True)
        If exs.Count = 0 Then
            _LiniesDeComandaPendentsDeLiquidar = Await FEB.PurchaseOrderItems.PendentsDeLiquidacioRep(exs, Current.Session.Emp)
            If exs.Count = 0 Then
                PanelBottomBar.Visible = False
                _RepCliComs = Await FEB.RepCliComs.All(exs, Current.Session.Emp)
                If exs.Count = 0 Then
                    _ControlItems = New ControlItems
                    _CancelRequest = False
                    ProgressBar1.Style = ProgressBarStyle.Blocks
                    PanelBottomBar.Visible = True
                    ButtonStart.Enabled = False

                    Await LoadConflictes()
                    LoadGrid()
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

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

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentItem()

        If oControlItem IsNot Nothing Then
            If SelectedItems.Count = 1 Then
                Dim oMenuItem As ToolStripMenuItem = oContextMenu.Items.Add("Client")
                Dim oContact As DTOContact = oControlItem.LineItmPnc.PurchaseOrder.contact
                Dim oContactMenu = Await FEB.ContactMenu.Find(exs, oContact)
                Dim oMenu_Contact As New Menu_Contact(oContact, oContactMenu)
                oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
            End If
            oContextMenu.Items.Add("Acceptar canvis", Nothing, AddressOf Do_SubmitChanges)
            oContextMenu.Items.Add("Confirmar com està", Nothing, AddressOf Do_Confirm)
            oContextMenu.Items.Add("Descartar", Nothing, AddressOf Do_Discard)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Function

    Private Async Sub Do_Discard(sender As Object, e As EventArgs)
        Dim oItems As ControlItems = SelectedItems()
        Dim exs As New List(Of Exception)
        If Await oItems.Discard(exs) Then
            _ControlItems.Remove(oItems)
            DataGridView1.Refresh()
        Else
            MsgBox("error al actualitzar la comisió de representant" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Async Sub Do_Confirm(sender As Object, e As EventArgs)
        Dim oItems As ControlItems = SelectedItems()
        Dim exs As New List(Of Exception)
        If Await oItems.Confirm(exs) Then
            _ControlItems.Remove(oItems)
            DataGridView1.Refresh()
        Else
            MsgBox("error al actualitzar la comisió de representant" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Async Sub Do_SubmitChanges(sender As Object, e As EventArgs)
        Dim oItems As ControlItems = SelectedItems()
        Dim exs As New List(Of Exception)
        If Await oItems.SubmitChanges(exs) Then
            _ControlItems.Remove(oItems)
            DataGridView1.Refresh()
        Else
            UIHelper.WarnError(exs, "error al actualitzar la comisió de representant")
        End If
    End Sub

    Private Async Function LoadConflictes() As Task
        PanelBottomBar.Visible = True
        With ProgressBar1
            .Maximum = _LiniesDeComandaPendentsDeLiquidar.Count - 1
            .Value = 0
        End With

        Dim exs As New List(Of Exception)
        For Each oItem As DTOPurchaseOrderItem In _LiniesDeComandaPendentsDeLiquidar
            Dim oSuggestedRepCom = Await FEB.PurchaseOrderItem.SuggestedRepCom(exs, GlobalVariables.Emp, oItem, _RepProducts, _RepCliComs)
            If oSuggestedRepCom IsNot Nothing Then
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
    End Function

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

    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Await SetContextMenu()
        End If
    End Sub

    Private Async Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _CancelRequest = True
        Await SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Async Sub ButtonSincronitza_Click(sender As Object, e As EventArgs) Handles ButtonSincronitza.Click
        Dim exs As New List(Of Exception)
        ButtonSincronitza.Enabled = False

        ProgressBar1.Style = ProgressBarStyle.Marquee
        ProgressBar1.Visible = True
        Await FEB.RepComLiquidables.sincronitza(exs)
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            MsgBox("Sortides sincronitzades correctament", MsgBoxStyle.Information)
            ButtonSincronitza.Enabled = True
        Else
            UIHelper.WarnError(exs)
            ButtonSincronitza.Enabled = True
        End If
    End Sub


    Protected Class ControlItem
        Public Property LineItmPnc As DTOPurchaseOrderItem
        Public Property SuggestedRepCom As DTORepCom
        Public Property Updated As Boolean

        Public Property Comanda As Integer = 0
        Public Property Linia As Integer = 0
        Public Property Fch As Date = DTO.GlobalVariables.Today()
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

            _Comanda = _LineItmPnc.PurchaseOrder.Num
            _Linia = _LineItmPnc.Lin
            _Fch = _LineItmPnc.PurchaseOrder.Fch
            _Producte = DTOProductSku.FullNom(_LineItmPnc.Sku)
            _Client = _LineItmPnc.PurchaseOrder.Contact.FullNom

            If _LineItmPnc.RepCom IsNot Nothing Then
                _RepOriginal = _LineItmPnc.RepCom.Rep.Nom
                _ComOriginal = _LineItmPnc.RepCom.Com / 100
            End If

            If oSuggestedRepCom IsNot Nothing Then
                _RepSuggerit = oSuggestedRepCom.Rep.Nom
                _ComSuggerida = oSuggestedRepCom.Com / 100
            End If

        End Sub

        Public Async Function SubmitChanges(exs As List(Of Exception)) As Task(Of Boolean)
            _LineItmPnc.RepCom = _SuggestedRepCom
            Dim retval = Await FEB.PurchaseOrderItem.UpdateRepCom(exs, _LineItmPnc)

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

        Public Async Function Confirm(exs As List(Of Exception)) As Task(Of Boolean)

            If _LineItmPnc.RepCom Is Nothing Then
                _LineItmPnc.RepCom = New DTORepCom
                _RepOriginal = ""
                _ComOriginal = 0
            Else
                _RepOriginal = _LineItmPnc.RepCom.Rep.Nom
                _ComOriginal = _LineItmPnc.RepCom.Com / 100
            End If
            _LineItmPnc.RepCom.RepCustom = True
            Dim retval = Await FEB.PurchaseOrderItem.UpdateRepCom(exs, _LineItmPnc)

            _Updated = retval
            Return retval
        End Function

        Public Async Function Discard(exs As List(Of Exception)) As Task(Of Boolean)
            _LineItmPnc.RepCom = New DTORepCom
            _LineItmPnc.RepCom.RepCustom = True
            Dim retval = Await FEB.PurchaseOrderItem.UpdateRepCom(exs, _LineItmPnc)
            _RepOriginal = ""
            _ComOriginal = 0

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

        Public Async Function Discard(exs As List(Of Exception)) As Task(Of Boolean)
            Dim retval As Boolean = True
            For Each oItem As ControlItem In List
                If Not Await oItem.Discard(exs) Then
                    retval = False
                End If
            Next
            Return retval
        End Function

        Public Async Function Confirm(exs As List(Of Exception)) As Task(Of Boolean)
            Dim retval As Boolean = True
            For Each oItem As ControlItem In List
                If Not Await oItem.Confirm(exs) Then
                    retval = False
                End If
            Next
            Return retval
        End Function

        Public Async Function SubmitChanges(exs As List(Of Exception)) As Task(Of Boolean)
            Dim retval As Boolean = True
            For Each oItem As ControlItem In List
                If Not Await oItem.SubmitChanges(exs) Then
                    retval = False
                End If
            Next
            Return retval
        End Function
    End Class

End Class




