Public Class Xl_ProductPluginItems
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTOProductPlugin

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Noms
    End Enum

    Public Shadows Sub Load(value As DTOProductPlugin)
        _Value = value

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub


    Public Function Values() As List(Of DTOProductPlugin.Item)
        Dim retval As New List(Of DTOProductPlugin.Item)
        For Each oControlItem In _ControlItems
            retval.Add(oControlItem.Source)
        Next
        Return retval
    End Function

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOProductPlugin.Item) = _Value.items
        _ControlItems = New ControlItems
        For Each oItem As DTOProductPlugin.Item In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOProductPlugin.Item
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductPlugin.Item = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = 80
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = True
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Noms)
            .HeaderText = "Nom"
            .DataPropertyName = "Noms"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
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

    Private Function SelectedItems() As List(Of DTOProductPlugin.Item)
        Dim retval As New List(Of DTOProductPlugin.Item)
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
            Dim oMenu_Template As New Menu_ProductPluginItem(SelectedItems.First)
            AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Template.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_BudgetOrders_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                'Dim oSku = oControlItem.Source.product
                'Dim exs As New List(Of Exception)
                'Dim oThumbnail = FEB2.ProductSku.ThumbnailSync(exs, oSku, 70, 80)
                Dim oThumbnail = oControlItem.Source.thumbnail
                If oThumbnail IsNot Nothing Then
                    e.Value = LegacyHelper.ImageHelper.Converter(MatHelperStd.ImageHelper.GetThumbnailToFill(oThumbnail, 70, 80))
                End If
        End Select
    End Sub

#Region "DragDrop"

    Private fromIndex As Integer
    Private dragRect As Rectangle


    Private Sub Xl_ProductPluginItems_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
        fromIndex = MyBase.HitTest(e.X, e.Y).RowIndex
        If fromIndex > -1 Then
            Dim dragSize As Size = SystemInformation.DragSize
            dragRect = New Rectangle(New Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize)
        Else
            dragRect = Rectangle.Empty
        End If
    End Sub

    Private Sub Xl_ProductPluginItems_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
        If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
            If (dragRect <> Rectangle.Empty AndAlso Not dragRect.Contains(e.X, e.Y)) Then
                MyBase.DoDragDrop(MyBase.Rows(fromIndex), DragDropEffects.Move)
            End If
        End If
    End Sub

    Private Sub Xl_ProductPluginItems_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragOver
        e.Effect = DragDropEffects.Move

        'Hot-tracking:
        Dim p As Point = MyBase.PointToClient(New Point(e.X, e.Y))
        Dim dragIndex = MyBase.HitTest(p.X, p.Y).RowIndex
        If dragIndex >= 0 Then
            Dim oRow = MyBase.Rows(dragIndex)
            oRow.Selected = True
        End If
    End Sub


    Private Sub Xl_ProductPluginItems_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragDrop
        Dim p As Point = MyBase.PointToClient(New Point(e.X, e.Y))
        Dim toIndex = MyBase.HitTest(p.X, p.Y).RowIndex

        If e.Effect = DragDropEffects.Move Then
            Dim oControlItem = _ControlItems(fromIndex)
            _ControlItems.RemoveAt(fromIndex)
            _ControlItems.Insert(toIndex, oControlItem)
            MyBase.Refresh()

            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Values))
        End If

    End Sub

#End Region

    Protected Class ControlItem
        Property Source As DTOProductPlugin.Item

        Property Noms As String


        Public Sub New(value As DTOProductPlugin.Item)
            MyBase.New()
            _Source = value
            With value
                _Noms = .LangNom.ToMultiline()
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

