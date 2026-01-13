Public Class Xl_LangTexts

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOLangText)
    Private _DefaultValue As DTOLangText
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        cod
        nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOLangText), Optional oDefaultValue As DTOLangText = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOLangText) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOLangText In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOLangText
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOLangText = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowLangText.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.cod)
            .HeaderText = "Codi"
            .DataPropertyName = "Cod"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
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

    Private Function SelectedItems() As List(Of DTOLangText)
        Dim retval As New List(Of DTOLangText)
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
            Dim oLangText As DTOLangText = oControlItem.Source
            '            Select Case oLangText.SrcType
            '            Case DTOLangText.Srcs.WebMenuGroup
            '            Dim oSrc As DTOWebMenuGroup = oLangText.Src
            '            Dim oMenu As New Menu_WebMenuGroup(oSrc)
            '            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            '            oContextMenu.Items.AddRange(oMenu.Range)
            '            Case DTOLangText.Srcs.WebMenuItem
            '            Dim oSrc As DTOWebMenuItem = oLangText.Src
            '            Dim oMenu As New Menu_WebMenuItem(oSrc)
            '            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            '            oContextMenu.Items.AddRange(oMenu.Range)
            '            Case DTOLangText.Srcs.WinMenuItem
            '            Dim oSrc As DTOWinMenuItem = oLangText.Src
            '            Dim oMenu As New Menu_WinMenuItem(oSrc)
            '            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            '            oContextMenu.Items.AddRange(oMenu.Range)
            '            Case DTOLangText.Srcs.Category
            '            Dim oSrc As DTOProductCategory = oLangText.Src
            '            Dim oMenu As New Menu_ProductCategory(oSrc)
            '            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            '            oContextMenu.Items.AddRange(oMenu.Range)
            '            Case DTOLangText.Srcs.Sku
            '            Dim oSrc As DTOProductSku = oLangText.Src
            '            Dim oMenu As New Menu_ProductSku(oSrc)
            '            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            '            oContextMenu.Items.AddRange(oMenu.Range)
            '            Case DTOLangText.Srcs.Noticia
            '            Dim oSrc As DTONoticia = oLangText.Src
            '            Dim oMenu As New Menu_Noticia(oSrc)
            '            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            '            oContextMenu.Items.AddRange(oMenu.Range)
            '            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOLangText = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Translate(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOLangText

        Property Cod As String
        Property Nom As String

        Public Sub New(value As DTOLangText)
            MyBase.New()
            _Source = value
            With value
                '_Cod = _Source.SrcType.ToString
                '_Nom = DTOLangText.nom(_Source)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


