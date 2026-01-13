Public Class Xl_PromosPerRep
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrder)
    Private _Reps As List(Of DTORep)
    Private _RepOrders As List(Of List(Of DTOPurchaseOrder))
    Private _PropertiesSet As Boolean

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Qty
        Eur
    End Enum

    Public Shadows Async Function Load(values As List(Of DTOPurchaseOrder)) As Task
        If Not _PropertiesSet Then
            SetProperties()
            _PropertiesSet = True
        End If

        _Values = values
        Await Refresca()
    End Function

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        _AllowEvents = False

        Dim oReps = Await FEB2.Reps.AllActive(Current.Session.User, exs)
        If exs.Count = 0 Then
            oReps = oReps.Where(Function(x) x.Rol.Id = DTORol.Ids.Comercial Or x.Rol.Id = DTORol.Ids.Rep).ToList

            _ControlItems = New ControlItems

            Dim oTotal As New DTORep(Guid.Empty)
            oTotal.NickName = "totals"
            Dim oControlItem As New ControlItem(oTotal, _Values)
            _ControlItems.Add(oControlItem)

            For Each oRep As DTORep In oReps
                oControlItem = New ControlItem(oRep, _Values)
                _ControlItems.Add(oControlItem)
            Next

            Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
            MyBase.DataSource = _ControlItems
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)

            SetContextMenu()
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function



    Public ReadOnly Property Value As DTORep
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTORep = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPurchaseOrder.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Comandes"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 70
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

    Private Function SelectedItems() As List(Of DTORep)
        Dim retval As New List(Of DTORep)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
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
            Dim oMenu_PurchaseOrder As New Menu_Rep(SelectedItems.First)
            AddHandler oMenu_PurchaseOrder.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PurchaseOrder.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oFrm As New Frm_Rep(Me.Value)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTORep

        Property Nom As String
        Property Qty As Integer
        Property Eur As Decimal

        Public Sub New(oRep As DTORep, values As List(Of DTOPurchaseOrder))
            MyBase.New()
            _Source = oRep

            Dim oOrders As List(Of DTOPurchaseOrder)

            If oRep.Guid.Equals(Guid.Empty) Then
                oOrders = values
            Else
                oOrders = (From order In values
                           Where order.Items.Any(Function(x) MatchesRep(x.RepCom, oRep))).ToList
            End If

            Dim DcEur As Decimal = oOrders.SelectMany(Function(y) y.Items).Sum(Function(x) DTOAmt.Import(Int(x.Qty), x.Price, CDec(x.Dto)).Eur)
            _Nom = oRep.NickName
            _Qty = oOrders.Count
            _Eur = DcEur
        End Sub

        Private Function MatchesRep(oRepCom As DTORepCom, oRep As DTORep) As Boolean
            Dim retval As Boolean
            If oRepCom IsNot Nothing Then
                If oRepCom.Rep.Equals(oRep) Then
                    retval = True
                End If
            End If
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


