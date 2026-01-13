Public Class Xl_Noticias
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
    Private _Cod As DTONoticiaBase.Srcs
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        Title
        visitCount
    End Enum

    Public Shadows Sub Load(value As List(Of DTONoticia), Optional oCod As DTONoticiaBase.Srcs = DTONoticiaBase.Srcs.News, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _Cod = oCod
        _SelectionMode = oSelectionMode
        _ControlItems = New ControlItems
        For Each oItem As DTONoticia In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(DataGridView1)
        LoadGrid()
        UIHelper.SetDataGridviewCurrentCell(DataGridView1, oCell)
        SetContextMenu()
        _AllowEvents = True

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
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Title)
                .HeaderText = "Titol"
                .DataPropertyName = "Title"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.visitCount)
                .HeaderText = "Visites"
                .DataPropertyName = "visitCount"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0;-#,###0;#"
            End With
        End With
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

    Private Function SelectedItems() As List(Of DTONoticia)
        Dim retval As New List(Of DTONoticia)
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
            Dim oMenu_Noticia As New Menu_Noticia(SelectedItems.First)
            AddHandler oMenu_Noticia.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Noticia.Range)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick

        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTONoticia = CurrentControlItem.Source
            Select Case _SelectionMode
                Case BLL.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Noticia(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case BLL.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(oSelectedValue))
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
        Dim oNoticia As DTONoticia = Nothing
        If _Cod = DTONoticiaBase.Srcs.Eventos Then
            oNoticia = New DTOEvento
        Else
            oNoticia = New DTONoticia
        End If
        oNoticia.Src = _Cod
        Dim oFrm As New Frm_Noticia(oNoticia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Public Property Source As DTONoticia

        Public Property Fch As Date
        Public Property Title As String
        Public Property VisitCount As Integer

        Public Sub New(oNoticia As DTONoticia)
            MyBase.New()
            _Source = oNoticia
            With oNoticia
                If .Src = DTONoticiaBase.Srcs.Eventos Then
                    _Fch = CType(oNoticia, DTOEvento).FchFrom
                Else
                    _Fch = .Fch
                End If
                _Title = .Title
                _VisitCount = .VisitCount
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

