Public Class Xl_IntrastatExceptions

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOIntrastatException)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        caption
    End Enum

    Public Shadows Sub Load(values As List(Of DTOIntrastatException))
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
        Dim oFilteredValues As List(Of DTOIntrastatException) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOIntrastatException In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOIntrastatException
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOIntrastatException = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowIntrastatException.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.caption)
            .DataPropertyName = "Caption"
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

    Private Function SelectedItems() As List(Of DTOIntrastatException)
        Dim retval As New List(Of DTOIntrastatException)
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
            Dim oException As DTOIntrastatException = SelectedItems.First
            Dim remesaId As Integer = DirectCast(oException.Tag, DTOImportacio).Id
            Dim oMenuItem As New ToolStripMenuItem("Editar la remesa " & remesaId, Nothing, AddressOf Do_EditImportacio)
            oMenuItem.Tag = oException.Tag
            oContextMenu.Items.Add(oMenuItem)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_EditImportacio(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        Dim oImportacio As DTOImportacio = oMenuItem.Tag
        Dim oFrm As New Frm_Importacio(oImportacio)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOIntrastatException = CurrentControlItem.Source
            Dim oImportacio As DTOImportacio = oSelectedValue.Tag
            Dim oFrm As New Frm_Importacio(oImportacio)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_IntrastatExceptions_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                e.Value = My.Resources.warn
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOIntrastatException

        Property Caption As String

        Public Sub New(value As DTOIntrastatException)
            MyBase.New()
            _Source = value

            Dim remesaId As Integer = DirectCast(value.Tag, DTOImportacio).Id
            Select Case value.Codi
                Case DTOIntrastatException.Codis.CodiMercancia
                    _Caption = "Falta codi mercancia a la remesa " & remesaId
                Case DTOIntrastatException.Codis.Weight
                    _Caption = "Falta el pes en Kg a la remesa " & remesaId
                Case DTOIntrastatException.Codis.Amount
                    _Caption = "Falta l'import de la remesa " & remesaId
            End Select

        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


