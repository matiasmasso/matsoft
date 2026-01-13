Public Class Xl_DeliveryTrps

    Inherits _Xl_ReadOnlyDatagridview

    Private _Delivery As DTODelivery
    Private _Values As List(Of DTOTrpZon)
    Private _ControlItems As ControlItems
    Private _PropertiesSet As Boolean
    Private _AllowEvents As Boolean

    Private Enum Cols
        Eur
        EurComp
        Kg
        TrpNom
        TrpZon
    End Enum

    Public Shadows Async Function Load(oDelivery As DTODelivery) As Task
        Dim exs As New List(Of Exception)
        _Delivery = oDelivery
        _Values = Await FEB2.TrpZons.Tarifas(exs, oDelivery.Address.Zip)
        If exs.Count = 0 Then
            If Not _PropertiesSet Then
                SetProperties()
                _PropertiesSet = True
            End If

            Refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOTrpZon In _Values
            Dim oControlItem As New ControlItem(oItem, _Delivery)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTrpZon.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Cost"
            .DataPropertyName = "Eur"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.EurComp)
            .HeaderText = "Compara"
            .DataPropertyName = "EurComp"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Kg)
            .HeaderText = "Kg cubic"
            .DataPropertyName = "KgCubicat"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.TrpNom)
            .HeaderText = "Transportista"
            .DataPropertyName = "TrpNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.TrpZon)
            .HeaderText = "Tarifa"
            .DataPropertyName = "TrpZon"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOTrpZon)
        Dim retval As New List(Of DTOTrpZon)
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
            Dim oMenu_TrpZon As New Menu_TrpZon(SelectedItems.First)
            AddHandler oMenu_TrpZon.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_TrpZon.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOTrpZon = CurrentControlItem.Source
            Dim oFrm As New Frm_TrpZon(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim BlObsoleto As Boolean = oControlItem.Obsoleto
        If BlObsoleto Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOTrpZon

        Property Eur As Decimal
        Property EurComp As Decimal
        Property KgCubicat As Integer
        Property TrpZon As String
        Property TrpNom As String
        Property Obsoleto As Boolean


        Public Sub New(value As DTOTrpZon, oDelivery As DTODelivery)
            MyBase.New()
            _Source = value

            Dim kg As Integer = DTODelivery.WeightKg(oDelivery.Items)
            Dim m3 As Integer = DTODelivery.VolumeM3(oDelivery.Items)
            Dim DcKgCubicat = DTOTrpZon.KgCubicats(value, m3, kg)
            Dim oTrpCost As DTOTrpCost = DTOTrpZon.Cost(value, DTODelivery.VolumeM3(oDelivery.Items), kg)
            With value
                _Eur = oTrpCost.Eur
                _EurComp = _Eur * 100 / (100 - .Transportista.CompensaPercent)
                _KgCubicat = DcKgCubicat
                _TrpZon = value.Nom
                _TrpNom = value.Transportista.Abr
                _Obsoleto = Not DTOTrpZon.ActivatOrDefault(value)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

