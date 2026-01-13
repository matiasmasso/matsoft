Public Class Xl_LangsToTranslate
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTOLangText
    Private _DefaultLang As DTOLang

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        nom
    End Enum

    Public Shadows Sub Load(value As DTOLangText, oDefaultLang As DTOLang)
        _Value = value
        _DefaultLang = oDefaultLang

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        _ControlItems.AddLang(_Value, DTOLang.ESP)
        _ControlItems.AddLang(_Value, DTOLang.CAT)
        _ControlItems.AddLang(_Value, DTOLang.ENG)
        _ControlItems.AddLang(_Value, DTOLang.POR)

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        If _DefaultLang Is Nothing Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        Else
            Dim oControlItem As ControlItem = _ControlItems.FirstOrDefault(Function(x) x.Source.Equals(_DefaultLang))
            Dim iRowIndex = _ControlItems.IndexOf(oControlItem)
            MyBase.CurrentCell = MyBase.Rows(iRowIndex).Cells(Cols.nom)
        End If

        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOLang
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOLang = oControlItem.Source
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

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
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


    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOLang
        Property Nom As String
        Property IsEmpty As Boolean

        Public Sub New(oLangText As DTOLangText, oLang As DTOLang)
            MyBase.New()
            _Source = oLang
            _Nom = oLang.Nom()
            _IsEmpty = (oLangText Is Nothing OrElse oLangText.Text(oLang) = "")
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)

        Public Sub AddLang(oLangText As DTOLangText, oLang As DTOLang)
            Dim oControlItem As New ControlItem(oLangText, oLang)
            MyBase.Add(oControlItem)
        End Sub
    End Class

End Class


