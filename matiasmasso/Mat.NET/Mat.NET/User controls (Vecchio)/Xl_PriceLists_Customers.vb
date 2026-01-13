Public Class Xl_PriceLists_Customers

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _SelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        Concepte
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPricelistCustomer), SelectionMode As BLL.Defaults.SelectionModes)
        _SelectionMode = SelectionMode
        _ControlItems = New ControlItems
        For Each oItem As DTOPricelistCustomer In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
        _AllowEvents = True
    End Sub


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Concepte)
                .HeaderText = "concepte"
                .DataPropertyName = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
    End Sub

    Private Function CurrentItems() As List(Of DTOPricelistCustomer)
        Dim retval As New List(Of DTOPricelistCustomer)

        Dim oRow As DataGridViewRow = Nothing
        If DataGridView1.SelectedRows.Count = 0 Then
            oRow = DataGridView1.CurrentRow
            If oRow IsNot Nothing Then
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOPricelistCustomer = oControlItem.Source
                retval.Add(oItem)
            End If
        Else
            For Each oRow In DataGridView1.SelectedRows
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOPricelistCustomer = oControlItem.Source
                retval.Add(oItem)
            Next
        End If

        Return retval
    End Function

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPriceLists As List(Of DTOPricelistCustomer) = CurrentItems()

        If oPriceLists.Count > 0 Then
            Dim oMenu_PriceList As New Menu_PriceList_Customer(oPriceLists)
            AddHandler oMenu_PriceList.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PriceList.Range)
        End If

        Dim oMenuItem_Add As New ToolStripMenuItem("afegir nova", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add(oMenuItem_Add)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DoZoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceList_Customer(CurrentItems(0))
        AddHandler oFrm.afterupdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                DoZoom(sender, e)
            Case bll.dEFAULTS.SelectionModes.Selection
                Dim oSelectedItem AS DTOPricelistCustomer = CurrentItems(0)
                RaiseEvent OnItemSelected(Me, New MatEventArgs(oSelectedItem))
        End Select
    End Sub

    Private Sub DataGridView1_DragDrop(sender As Object, e As DragEventArgs) Handles DataGridView1.DragDrop
        Dim oItems As List(Of DTOPriceListItem_Supplier) = Nothing
        If e.Data.GetDataPresent(GetType(DTOPriceList_Supplier)) Then
            Dim oParent As DTOPriceList_Supplier = e.Data.GetData(GetType(DTOPriceList_Supplier))
            oItems = oParent.Items
        ElseIf e.Data.GetDataPresent(GetType(List(Of DTOPriceListItem_Supplier))) Then
            oItems = e.Data.GetData(GetType(List(Of DTOPriceListItem_Supplier)))
        End If

        If oItems IsNot Nothing Then
            UpdatePriceList_Customers(oItems)
        End If
    End Sub

    Private Sub UpdatePriceList_Customers(oPriceListItems_Supplier As List(Of DTOPriceListItem_Supplier))
        Dim iAfegits As Integer
        Dim iModificats As Integer
        Dim iNotFound As Integer
        Dim oPriceList_Customer AS DTOPricelistCustomer = CurrentItems(0)
        If oPriceList_Customer IsNot Nothing Then
            Dim rc = MsgBox("actualitzem la tarifa " & oPriceList_Customer.Concepte & " amb aquests " & oPriceListItems_Supplier.Count & " productes?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = vbOK Then
                Dim oCommercialMargin As DTOCommercialMargin = BLL.BLLProveidor.CommercialMargin

                With oPriceList_Customer
                    For Each oSupplierItem As DTOPriceListItem_Supplier In oPriceListItems_Supplier
                        Dim oCustomerItem As DTOPricelistItemCustomer = BLL.BLLPriceList_Supplier.GetSalePrice(oSupplierItem, oPriceList_Customer, oCommercialMargin, 0)
                        If oCustomerItem Is Nothing Then
                            iNotFound += 1
                        Else
                            Dim oExistingItem As DTOPricelistItemCustomer = .Items.Find(Function(x) x.Sku.RefProveidor = oSupplierItem.Ref)
                            If oExistingItem Is Nothing Then
                                .Items.Add(oCustomerItem)
                                iAfegits += 1
                            Else

                                With oExistingItem
                                    If Not .TarifaA.Equals(oCustomerItem.TarifaA) _
                                        Or Not .TarifaB.Equals(oCustomerItem.TarifaB) _
                                        Or Not .Retail.Equals(oCustomerItem.Retail) Then
                                        iModificats += 1
                                        .TarifaA = oCustomerItem.TarifaA
                                        .TarifaB = oCustomerItem.TarifaB
                                        .Retail = oCustomerItem.Retail
                                    End If
                                End With
                            End If
                        End If
                    Next
                End With

                Dim exs As New List(Of Exception)
                If BLL.BLLPricelistCustomer.Update(oPriceList_Customer, exs) Then
                    MsgBox("tarifa actualitzada" & vbCrLf & "fora de cataleg: " & iNotFound.ToString & vbCrLf & "afegits:" & iAfegits.ToString & vbCrLf & "actualitzats: " & iModificats.ToString, MsgBoxStyle.Information, "MAT.NET")
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_DragEnter(sender As Object, e As DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(GetType(DTOPriceList_Supplier))) Then
            e.Effect = DragDropEffects.Copy
        ElseIf e.Data.GetDataPresent(GetType(List(Of DTOPriceListItem_Supplier))) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOPricelistCustomer

        Property Fch As Date
        Property Concepte As String

        Public Sub New(value As DTOPricelistCustomer)
            MyBase.New()
            _Source = value
            With value
                _Fch = .Fch
                _Concepte = .Concepte
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class