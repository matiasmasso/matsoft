Public Class Xl_ProductHighResImages

    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        thumbnail
        Txt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOHighResImage))
        _ControlItems = New ControlItems
        For Each oItem As DTOHighResImage In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOHighResImage
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOHighResImage = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        MyBase.RowTemplate.Height = 50 ' MyBase.Font.Height * 3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With CType(MyBase.Columns(Cols.thumbnail), DataGridViewImageColumn)
            .DataPropertyName = "Thumbnail"
            .ImageLayout = DataGridViewImageCellLayout.Zoom
            '.HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 50
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Txt"
            '.DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        SetContextMenu()
        _AllowEvents = True
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

    Private Function SelectedItems() As List(Of DTOHighResImage)
        Dim retval As New List(Of DTOHighResImage)
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
            'Dim oMenu_HighResImage As New Menu_HighResImage(SelectedItems.First)
            'AddHandler oMenu_HighResImage.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_HighResImage.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTOHighResImage = CurrentControlItem.Source
        Dim oFrm As New Frm_HighResImage(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOHighResImage

        Property Thumbnail As Image
        Property Txt As String

        Public Sub New(value As DTOHighResImage)
            MyBase.New()
            _Source = value
            With value
                _Thumbnail = .Thumbnail
                _Txt = "Hola" ' BLL.BLLHighResImage.MultilineFeatures(value)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


