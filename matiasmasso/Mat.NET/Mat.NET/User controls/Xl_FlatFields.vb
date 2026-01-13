Public Class Xl_FlatFields

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Value
    End Enum

    Public Shadows Sub Load(values As List(Of DTOFlatField))
        _ControlItems = New ControlItems
        For Each oItem As DTOFlatField In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOFlatField
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOFlatField = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .HeaderText = "Nom"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Value)
                .HeaderText = "Valor"
                .DataPropertyName = "Value"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOFlatField)
        Dim retval As New List(Of DTOFlatField)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_FlatField As New Menu_FlatField(SelectedItems.First)
            'AddHandler oMenu_FlatField.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_FlatField.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        'Dim oSelectedValue As DTOFlatField = CurrentControlItem.Source
        'Dim oFrm As New Frm_FlatField(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOFlatField

        Property Nom As String
        Property Value As String

        Public Sub New(oField As DTOFlatField)
            MyBase.New()
            _Source = oField
            With oField
                _Nom = .Nom
                Select Case oField.Format
                    Case DTOFlatField.Formats.AAAAMMDD, DTOFlatField.Formats.DDMMAA
                        _Value = CDate(.Value).ToShortDateString
                    Case DTOFlatField.Formats.Decimal2DsComa
                        _Value = Format(CDec(.Value), "#,##0.00")
                    Case DTOFlatField.Formats.Numeric
                        _Value = Format(CDec(.Value), "#,##0")
                    Case Else
                        _Value = .Value
                End Select
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

