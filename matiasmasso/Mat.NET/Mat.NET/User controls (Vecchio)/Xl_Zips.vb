Public Class Xl_Zips
    Private _ControlItems As ControlItems
    Private _DefaultControlItem As ControlItem
    Private _AllowEvents As Boolean
    Private _SelectionMode As bll.dEFAULTS.SelectionModes

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOZip), Optional oDefaultValue As DTOZip = Nothing, Optional oSelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _SelectionMode = oSelectionMode
        _ControlItems = New ControlItems
        For Each oItem As DTOZip In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        If oDefaultValue Is Nothing Then
            If _ControlItems.Count = 1 Then _DefaultControlItem = _ControlItems(0)
        Else
            _DefaultControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Guid.Equals(oDefaultValue.Guid))
        End If

        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTOZip
        Get
            Dim retval As DTOZip = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Public Sub Clear()
        _ControlItems = New ControlItems
        LoadGrid()
    End Sub


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
                .HeaderText = "Codi postal"
                .DataPropertyName = "Nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            If _DefaultControlItem IsNot Nothing Then
                Dim rowIdx As Integer = _ControlItems.IndexOf(_DefaultControlItem)
                If rowIdx >= 0 Then
                    .CurrentCell = .Rows(rowIdx).Cells(Cols.Nom)
                End If
            End If
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

    Private Function SelectedItems() As List(Of DTOZip)
        Dim retval As New List(Of DTOZip)
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
            Dim oMenu_Zip As New Menu_Zip(SelectedItems.First)
            AddHandler oMenu_Zip.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Zip.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOZip = CurrentControlItem.Source
            Select Case _SelectionMode
                Case BLL.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(oSelectedValue))
                Case Else
                    Dim oFrm As New Frm_Zip(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
            End Select
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent requestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOZip

        Property Nom As String

        Public Sub New(oZip As DTOZip)
            MyBase.New()
            _Source = oZip
            With oZip
                _Nom = .ZipCod
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class


End Class

