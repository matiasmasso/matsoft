Public Class Xl_PropertyGrid
    Inherits DataGridView

    Private _PropertyGridItems As List(Of DTOPropertyGridItemBase)
    Private _BgColor As Color = Color.FromArgb(240, 240, 240)

    'Private _Lang As DTO.DTOLang

    Private Enum Cols
        Ico
        Label
        Value
    End Enum

    Protected Overrides Sub OnCreateControl()
        MyBase.AutoGenerateColumns = False
        MyBase.AllowUserToAddRows = False
        MyBase.AllowUserToDeleteRows = False


        MyBase.Columns.Clear()
        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False
        MyBase.GridColor = _BgColor

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With MyBase.Columns(Cols.Ico)
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.NullValue = Nothing
            .DefaultCellStyle.BackColor = _BgColor
            .Width = 14
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Label)
            '.DataPropertyName = "LabelEsp"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Value)
            '.DataPropertyName = "Value"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        With MyBase.RowTemplate
            '.Height = 50 ' MyBase.Font.Height * 5
        End With


        _PropertyGridItems = New List(Of DTOPropertyGridItemBase)
    End Sub


    Public Function PropertyValue(sPropertyNom As String) As Object
        Dim oItems As List(Of DTOPropertyGridItem) = Items()
        Dim retval As Object = oItems.Find(Function(x) CType(x, DTOPropertyGridItem).PropertyNom = sPropertyNom).Value()
        Return retval
    End Function

    Public Sub AddGroup(sLabel As String, Optional Collapsed As Boolean = False)
        Dim oGroup As New DTOPropertyGridGroup
        With oGroup
            .Label = sLabel
            .Collapsed = Collapsed
        End With
        _PropertyGridItems.Add(oGroup)
        MyBase.Rows.Add(GroupRow(oGroup))
    End Sub


    Public Sub AddItem(sLabel As String, sPropertyNom As String, Optional oEditor As DTOPropertyGridItem.Editors = DTOPropertyGridItem.Editors.Default, Optional value As Object = Nothing, Optional oOptions As Array = Nothing)
        Dim Item As New DTOPropertyGridItem
        With Item
            .Label = sLabel
            .PropertyNom = sPropertyNom
            .Editor = oEditor
            .Value = value
            .Options = oOptions
        End With
        _PropertyGridItems.Add(Item)
        MyBase.Rows.Add(ItemRow(Item))
    End Sub


    Private Function GroupRow(oGroup As DTOPropertyGridGroup) As DataGridViewRow
        Dim retval As New DataGridViewRow

        Dim oCellIco As New DataGridViewImageCell()
        oCellIco.Value = IIf(oGroup.Collapsed, My.Resources.plus, My.Resources.minus)
        retval.Cells.Add(oCellIco)

        Dim oCellLabel As New DataGridViewTextBoxCell
        oCellLabel.Value = oGroup.Label
        oCellLabel.Style.Font = New Font(MyBase.Font, FontStyle.Bold)
        retval.Cells.Add(oCellLabel)

        Return retval
    End Function

    Private Function ItemRow(oItem As DTOPropertyGridItem) As DataGridViewRow
        Dim retval As New DataGridViewRow

        Dim oCellIco As New DataGridViewImageCell()
        retval.Cells.Add(oCellIco)

        Dim oCellLabel As New DataGridViewTextBoxCell
        oCellLabel.Value = oItem.Label
        retval.Cells.Add(oCellLabel)

        Dim oCellValue As DataGridViewCell = Nothing
        Select Case oItem.Editor
            Case DTOPropertyGridItem.Editors.Default
                oCellValue = New DataGridViewTextBoxCell
                oCellValue.Value = oItem.Value
            Case DTOPropertyGridItem.Editors.TextBox
                oCellValue = New DataGridViewTextBoxCell
                oCellValue.Value = oItem.Value
            Case DTOPropertyGridItem.Editors.CheckBox
                oCellValue = New DataGridViewCheckBoxCell
                oCellValue.Value = oItem.Value
            Case DTOPropertyGridItem.Editors.ShortDate
                oCellValue = New DataGridViewCalendarCell
                oCellValue.Value = oItem.Value
            Case DTOPropertyGridItem.Editors.FullDate
                oCellValue = New DataGridViewCalendarCell
                oCellValue.Value = oItem.Value
            Case DTOPropertyGridItem.Editors.Combobox
                'oCellValue = New DataGridViewComboBoxCell
                'With CType(oCellValue, DataGridViewComboBoxCell)
                ' .DataSource = oItem.Options
                ' .Value = oItem.Value
                'End With
            Case DTOPropertyGridItem.Editors.Lang
                oCellValue = New DataGridViewLangCell
                CType(oCellValue, DataGridViewLangCell).Value = oItem.Value
            Case Else
                oCellValue = New DataGridViewTextBoxCell
                oCellValue.Value = oItem.Value
        End Select

        retval.Cells.Add(oCellValue)
        Return retval
    End Function

    Public Function Items() As List(Of DTOPropertyGridItem)
        Dim retval As New List(Of DTOPropertyGridItem)
        For idx As Integer = 0 To _PropertyGridItems.Count - 1
            If TypeOf _PropertyGridItems(idx) Is DTOPropertyGridItem Then
                Dim item As DTOPropertyGridItem = _PropertyGridItems(idx)
                Dim oCell As DataGridViewCell = MyBase.Rows(idx).Cells(Cols.Value)
                Select Case item.Editor
                    Case DTOPropertyGridItem.Editors.Lang
                        item.Value = CType(oCell, DataGridViewLangCell).Value
                    Case Else
                        item.Value = oCell.Value
                End Select
                retval.Add(item)
            End If
        Next
        Return retval
    End Function

    Private Sub Xl_PropertyGrid_CellStateChanged(sender As Object, e As DataGridViewCellStateChangedEventArgs) Handles Me.CellStateChanged
        If e.Cell Is Nothing And e.StateChanged <> DataGridViewElementStates.Selected Then
        Else
            Select Case e.Cell.ColumnIndex
                Case Cols.Ico, Cols.Label
                    e.Cell.Selected = False
            End Select
        End If
    End Sub

    Private Sub Xl_PropertyGrid_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles Me.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Ico
                If TypeOf _PropertyGridItems(e.RowIndex) Is DTOPropertyGridGroup Then
                    Dim oGroup As DTOPropertyGridGroup = _PropertyGridItems(e.RowIndex)
                    oGroup.Collapsed = Not oGroup.Collapsed
                    If oGroup.Collapsed Then
                        Collapse(e.RowIndex)
                    Else
                        Expand(e.RowIndex)
                    End If
                End If
        End Select
    End Sub

    Private Sub Collapse(fromRowIndex As Integer)
        For i As Integer = fromRowIndex + 1 To MyBase.Rows.Count - 1
            If TypeOf MyBase.Rows(i).DataBoundItem Is DTOPropertyGridGroup Then Exit For
            MyBase.Rows(i).Visible = False
        Next
    End Sub

    Private Sub Expand(fromRowIndex As Integer)
        For i As Integer = fromRowIndex + 1 To MyBase.Rows.Count - 1
            If TypeOf MyBase.Rows(i).DataBoundItem Is DTOPropertyGridGroup Then Exit For
            MyBase.Rows(i).Visible = True
        Next
    End Sub

    Public Class DTOPropertyGridItemBase
        Property Label As String
    End Class

    Public Class DTOPropertyGridGroup
        Inherits DTOPropertyGridItemBase

        Property Collapsed As Boolean
    End Class

    Public Class DTOPropertyGridItem
        Inherits DTOPropertyGridItemBase

        Property PropertyNom As String
        Property Editor As Editors
        Property Value As Object
        Property Options As Array

        Public Enum Editors
            [Default]
            TextBox
            CheckBox
            ShortDate
            FullDate
            Combobox
            Lang
            Lookup
        End Enum
    End Class
End Class
