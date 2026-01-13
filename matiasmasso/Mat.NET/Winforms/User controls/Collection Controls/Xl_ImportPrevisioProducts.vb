Public Class Xl_ImportPrevisioProducts

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOImportPrevisio)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Type
        Sku
        Nom
        Qty
        Confirmat
    End Enum

    Public Shadows Sub Load(values As List(Of DTOImportPrevisio))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Dim oFilteredValues As List(Of DTOImportPrevisio) = _Values.
            OrderBy(Function(x) ControlItem.GetSkuId(x)).
            OrderBy(Function(x) ControlItem.GetSkuType(x)).
            ToList

        _ControlItems = New ControlItems
        For Each oItem As DTOImportPrevisio In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub
    Public ReadOnly Property Value As DTOImportPrevisio
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOImportPrevisio = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Type)
            .HeaderText = ""
            .DataPropertyName = "Type"
            .Width = 20
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Ref"
            .DataPropertyName = "SkuId"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Previsio"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Confirmat)
            .HeaderText = "Confirmat"
            .DataPropertyName = "Confirmat"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
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

    Private Function SelectedItems() As List(Of DTOImportPrevisio)
        Dim retval As New List(Of DTOImportPrevisio)
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
            Dim oMenu_ImportPrevisio As New Menu_ImportPrevisio(SelectedItems)
            AddHandler oMenu_ImportPrevisio.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ImportPrevisio.Range)
            'oContextMenu.Items.Add("-")
        End If
        'oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOImportPrevisio = CurrentControlItem.Source
                    'Dim oFrm As New Frm_ImportPrevisio(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOImportPrevisio

        Property Type As String
        Property SkuId As String
        Property Nom As String
        Property Qty As Integer
        Property Confirmat As Integer

        Public Sub New(value As DTOImportPrevisio)
            MyBase.New()
            _Source = value
            With value
                _Nom = GetSkuNom(value)
                _SkuId = GetSkuId(value)
                _Type = GetSkuType(value)
                _Qty = .Qty
            End With
        End Sub

        Shared Function GetSkuType(value As DTOImportPrevisio) As String
            Dim retval As String = "Z"
            If value.Sku IsNot Nothing Then
                retval = Chr(65 + CInt(value.Sku.Category.Codi))
            End If
            Return retval
        End Function

        Shared Function GetSkuId(value As DTOImportPrevisio) As Integer
            Dim retval As String = 0
            If value.Sku IsNot Nothing Then
                retval = value.Sku.Id
            End If
            Return retval
        End Function

        Shared Function GetSkuNom(value As DTOImportPrevisio) As String
            Dim retval As String = 0
            If value.Sku Is Nothing Then
                retval = value.SkuRef & " " & value.SkuNom
            Else
                retval = value.sku.nomLlarg.Tradueix(Current.Session.Lang)
            End If
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


