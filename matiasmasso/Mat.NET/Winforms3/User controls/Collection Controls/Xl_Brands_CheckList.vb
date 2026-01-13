Public Class Xl_Brands_CheckList

    Inherits DataGridView

    Private _allBrands As List(Of DTOProductBrand)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        checked
        Nom
    End Enum

    Public Shadows Sub Load(allBrands As List(Of DTOProductBrand), selectedBrands As List(Of DTOProductBrand))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _allBrands = allBrands
        Refresca(selectedBrands)
    End Sub

    Public Sub ClearChecks()
        If _ControlItems IsNot Nothing Then
            For Each oControlItem As ControlItem In _ControlItems
                oControlItem.Checked = False
            Next
        End If
    End Sub

    Private Sub Refresca(selectedBrands As List(Of DTOProductBrand))
        If selectedBrands IsNot Nothing Then

            _AllowEvents = False

            _ControlItems = New ControlItems
            For Each oItem As DTOProductBrand In _allBrands
                Dim oControlItem As New ControlItem(oItem)
                'oControlItem.Checked = selectedBrands.Exists(Function(x) x.Id = oItem.Id)
                oControlItem.Checked = selectedBrands.Exists(Function(x) x.Equals(oItem))
                _ControlItems.Add(oControlItem)
            Next

            MyBase.DataSource = _ControlItems
            MyBase.CurrentCell = Nothing
            'If _ControlItems.Count > 0 Then
            'MyBase.CurrentCell = MyBase.FirstDisplayedCell
            'End If

            'SetContextMenu()
            _AllowEvents = True
        End If
    End Sub

    Private Sub SetProperties()
        'MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.EnableHeadersVisualStyles = False
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewCheckBoxColumn)
        With DirectCast(MyBase.Columns(Cols.checked), DataGridViewCheckBoxColumn)
            .DataPropertyName = "Checked"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 20
            '.DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Marca comercial"
            .DataPropertyName = "Nom"
            .ReadOnly = True
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

    Public Function selectedBrands() As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        For Each oRow As DataGridViewRow In MyBase.Rows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Checked Then
                retval.Add(oControlItem.Source)
            End If
        Next
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
            Dim oMenu_Template As New Menu_ProductBrand(oControlItem.Source)
            AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Template.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("seleccionar", Nothing, AddressOf Do_SelectAll)
        oContextMenu.Items.Add("deseleccionar", Nothing, AddressOf Do_SelectNone)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_SelectAll()
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            DirectCast(oRow.Cells(Cols.checked), DataGridViewCheckBoxCell).Value = True
        Next
    End Sub

    Private Sub Do_SelectNone()
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            DirectCast(oRow.Cells(Cols.checked), DataGridViewCheckBoxCell).Value = False
        Next
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.checked
                If _AllowEvents Then
                    Dim oRol As DTOProductBrand = _allBrands(e.RowIndex)
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(selectedBrands))
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.checked
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_Brands_CheckList_EnabledChanged(sender As Object, e As EventArgs) Handles Me.EnabledChanged
        SetBackgroundColor()
    End Sub

    Private Sub SetBackgroundColor()
        MyBase.BackgroundColor = IIf(MyBase.Enabled, SystemColors.AppWorkspace, SystemColors.Control)
        MyBase.ColumnHeadersDefaultCellStyle.BackColor = IIf(MyBase.Enabled, Color.White, SystemColors.Control)
        MyBase.ColumnHeadersDefaultCellStyle.ForeColor = IIf(MyBase.Enabled, Color.Black, Color.FromArgb(150, 150, 150))
        For Each oRow As DataGridViewRow In MyBase.Rows
            oRow.DefaultCellStyle.BackColor = IIf(MyBase.Enabled, Color.White, SystemColors.Control)
        Next
        MyBase.ForeColor = IIf(MyBase.Enabled, Color.Black, Color.FromArgb(150, 150, 150))

        If Not MyBase.Enabled Then MyBase.CurrentCell = Nothing
    End Sub

    Private Sub Xl_Brands_CheckList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
        SetBackgroundColor()
    End Sub

    Protected Class ControlItem
        Property Source As DTOProductBrand
        Property Checked As Boolean
        Property Nom As String

        Public Sub New(value As DTOProductBrand)
            MyBase.New()
            _Source = value
            With value
                _Nom = .nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class



