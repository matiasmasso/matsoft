Public Class Xl_Bloggers
    Private _Values As List(Of DTOBlogger)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _SelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        title
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBlogger), oMode As bll.dEFAULTS.SelectionModes)
        _values = values
        _SelectionMode = oMode
        If _Values.Count > 0 Then
            _ControlItems = New ControlItems
            LoadGrid()
        Else
            SetContextMenu()
        End If
    End Sub

    Public ReadOnly Property Value As DTOBlogger
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBlogger = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        Dim search As String = TextBoxSearch.Text
        _ControlItems = New ControlItems
        For Each oItem As DTOBlogger In _Values
            If oItem.Title.Contains(search) Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

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
            With .Columns(Cols.title)
                .HeaderText = "Titol"
                .DataPropertyName = "Title"
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

    Private Function SelectedItems() As List(Of DTOBlogger)
        Dim retval As New List(Of DTOBlogger)
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
            Dim oMenu_Blogger As New Menu_Blogger(SelectedItems.First)
            AddHandler oMenu_Blogger.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Blogger.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        Dim oBlogger As New DTOBlogger
        Dim oFrm As New Frm_Blogger(oBlogger)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOBlogger = CurrentControlItem.Source

        Select Case _SelectionMode
            Case BLL.Defaults.SelectionModes.Browse
                Dim oFrm As New Frm_Blogger(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case bll.dEFAULTS.SelectionModes.Selection
                Dim oEventArgs As New MatEventArgs(oSelectedValue)
                RaiseEvent onItemSelected(Me, oEventArgs)
        End Select

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
        Property Source As DTOBlogger

        Property Title As String

        Public Sub New(oBlogger As DTOBlogger)
            MyBase.New()
            _Source = oBlogger
            With oBlogger
                _Title = .Title
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

