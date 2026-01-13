Public Class Xl_Reps
    Private _ControlItems As ControlItems
    Private _Mode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Async Sub Load(values As List(Of DTORep), Optional oDefaultRep As DTORep = Nothing, Optional oMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        _Mode = oMode
        _ControlItems = New ControlItems
        For Each oItem As DTORep In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        Await LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTORep
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTORep = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Async Function LoadGrid() As Task
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
        End With
        Await SetContextMenu()
        _AllowEvents = True
    End Function

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTORep)
        Dim retval As New List(Of DTORep)
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

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oContactMenu = Await FEB.ContactMenu.Find(exs, SelectedItems.First)
            Dim oMenu_Rep As New Menu_Rep(SelectedItems.First, oContactMenu)
            AddHandler oMenu_Rep.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Rep.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Function

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Select Case _Mode
            Case DTO.Defaults.SelectionModes.Selection
                Dim oControlItem As ControlItem = CurrentControlItem()
                If oControlItem IsNot Nothing Then
                    Dim oRep As DTORep = oControlItem.Source
                    RaiseEvent onItemSelected(Me, New MatEventArgs(oRep))
                End If
            Case Else
                'Dim oSelectedValue As DTORep = CurrentControlItem.Source
                'Dim oFrm As New Frm_Rep(oSelectedValue)
                'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                'oFrm.Show()
        End Select
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Select Case _Mode
                Case DTO.Defaults.SelectionModes.Selection
                    Dim oControlItem As ControlItem = CurrentControlItem()
                    If oControlItem IsNot Nothing Then
                        Dim oRep As DTORep = oControlItem.Source
                        RaiseEvent onItemSelected(Me, New MatEventArgs(oRep))
                    End If
                Case Else
                    'Dim oSelectedValue As DTORep = CurrentControlItem.Source
                    'Dim oFrm As New Frm_Rep(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
            End Select
            e.Handled = True
        End If
    End Sub


    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            Await SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTORep

        Property Nom As String

        Public Sub New(oRep As DTORep)
            MyBase.New()
            _Source = oRep
            With oRep
                _Nom = .NickName
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class
