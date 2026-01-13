Public Class Xl_PgcCtas

    Inherits DataGridView

    Private _Values As List(Of DTOPgcCta)
    Private _DefaultValue As DTOPgcCta
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPgcCta), Optional oDefaultValue As DTOPgcCta = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOPgcCta) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOPgcCta In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPgcCta)
        Dim retval As List(Of DTOPgcCta)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.Esp.ToLower.Contains(_Filter.ToLower) Or x.Nom.Cat.ToLower.Contains(_Filter.ToLower) Or x.Nom.Eng.ToLower.Contains(_Filter.ToLower) Or x.Id.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOPgcCta
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPgcCta = oControlItem.Source
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
        MyBase.AllowDrop = True
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Comptes"
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

    Private Function SelectedItems() As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then
            If CurrentControlItem() IsNot Nothing Then
                retval.Add(CurrentControlItem.Source)
            End If
        End If
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
            Dim oMenu_PgcCta As New Menu_PgcCta(SelectedItems.First)
            AddHandler oMenu_PgcCta.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PgcCta.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPgcCta = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_PgcCta(oSelectedValue)
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

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, New MatEventArgs(Me.Value))
    End Sub

    Private Sub Xl_PgcCtas_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If _SelectionMode = DTO.Defaults.SelectionModes.Browse Then
            If e.Button = MouseButtons.Left Then
                MyBase.DoDragDrop(SelectedItems, DragDropEffects.Move)
            End If
        End If
    End Sub

    Public Function WidthAdjustment() As Integer
        Dim oLang As DTOLang = Current.Session.Lang
        Dim oGraphics As Graphics = MyBase.CreateGraphics()
        Dim iMaxWidth As Integer
        For Each oItem As ControlItem In _ControlItems
            Dim iWidth As Integer = DataGridViewCell.MeasureTextWidth(oGraphics, oItem.Nom, MyBase.Font, MyBase.RowTemplate.Height, TextFormatFlags.Left)
            If iWidth > iMaxWidth Then iMaxWidth = iWidth
        Next

        Dim iOriginalColWidth As Integer = MyBase.Columns(Cols.Nom).Width
        Dim retval As Integer = iMaxWidth - iOriginalColWidth
        Return retval
    End Function

    Public Function AdjustedHeight() As Integer
        Dim MaxVisibleRows As Integer = 16
        Dim VisibleRows As Integer = 0
        If MyBase.DataSource.Count <= MaxVisibleRows Then
            VisibleRows = MyBase.DataSource.Count
        Else
            VisibleRows = MaxVisibleRows
        End If

        Dim retval As Integer = MyBase.RowTemplate.Height * VisibleRows + 3
        Return retval
    End Function

    Protected Class ControlItem
        Property Source As DTOPgcCta

        Property Nom As String

        Public Sub New(value As DTOPgcCta)
            MyBase.New()
            _Source = value
            Dim oLang As DTOLang = Current.Session.Lang
            With value
                _Nom = DTOPgcCta.FullNom(value, oLang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


