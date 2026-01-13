Public Class Xl_BundleSkus
    Inherits TabStopDataGridView

    Private _Values As List(Of DTOSkuBundle)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Qty
        Pvp
        Kg
        m3
    End Enum


    Public Shadows Sub Load(Values As List(Of DTOSkuBundle))
        _Values = Values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then SetProperties()

        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOSkuBundle)
        Get
            Dim retval = _ControlItems.Select(Function(x) x.Source).ToList
            Return retval
        End Get
    End Property

    Public ReadOnly Property Value As DTOSkuBundle
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOSkuBundle = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOSkuBundle In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oBindingSource As New BindingSource
        oBindingSource.AllowNew = True
        oBindingSource.DataSource = _ControlItems
        MyBase.DataSource = oBindingSource

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPrvCliNum.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Unitats"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0;-#,###0;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pvp)
            .HeaderText = "Pvp"
            .DataPropertyName = "Pvp"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Kg)
            .HeaderText = "Pes/ud"
            .DataPropertyName = "Kg"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###0.0 \K\g;-#,###0.0 \K\g;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.m3)
            .HeaderText = "Vol/ud"
            .DataPropertyName = "m3"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,###0.0000 \m3;-#,###0.0000 \m3;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
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

    Private Function SelectedItems() As List(Of DTOSkuBundle)
        Dim retval As New List(Of DTOSkuBundle)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                retval.Add(oControlItem.Source)
            End If
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

        If oControlItem IsNot Nothing AndAlso oControlItem.Source IsNot Nothing Then
            Dim oMenu_Client As New Menu_ProductSku(SelectedItems.First.Sku)
            oContextMenu.Items.AddRange(oMenu_Client.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("retirar de la llista", Nothing, AddressOf Do_Remove)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Remove()
        Dim oControlItem = CurrentControlItem()
        _ControlItems.Remove(oControlItem)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOSkuBundle = CurrentControlItem.Source
            Dim oFrm As New Frm_Art(oSelectedValue.Sku)
            oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Async Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Nom
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim sKey As String = oRow.Cells(Cols.Nom).Value 'e.FormattedValue
                    Dim oSku = Await Finder.FindSku(exs, Current.Session.Emp, sKey)
                    If exs.Count = 0 Then
                        If oSku IsNot Nothing Then
                            Dim oBundle As New DTOSkuBundle
                            oBundle.Sku = oSku
                            _ControlItems(oRow.Index) = New ControlItem(oBundle)
                            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                        End If
                    Else
                            UIHelper.WarnError(exs)
                    End If
                Case Cols.Qty
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oSkuBundle As DTOSkuBundle = oControlItem.source
                    oSkuBundle.Qty = oControlItem.Qty
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            End Select
        End If


    End Sub



    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            'Dim oSku As DTOProductSku = DirectCast(oControlItem.Source, DTOSkuBundle).Sku
            'If oSku IsNot Nothing Then
            ' If oSku.Obsoleto Then
            ' oRow.DefaultCellStyle.BackColor = Color.LightGray
            ' Else
            '     oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            ' End If
            ' End If

        End If
    End Sub

    Private Sub Xl_BundleSkus_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                Do_Remove()
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOSkuBundle

        Property Nom As String
        Property Qty As String
        Property Pvp As Decimal
        Property Kg As Decimal
        Property m3 As Decimal

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(value As DTOSkuBundle)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Qty = .Qty
                If .Sku.rrpp IsNot Nothing Then
                    _Pvp = .Sku.rrpp.eur
                End If
                _Kg = .Sku.WeightKgOrInherited
                _m3 = .Sku.VolumeM3OrInherited
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

