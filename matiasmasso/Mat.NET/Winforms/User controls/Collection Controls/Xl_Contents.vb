Public Class Xl_Contents

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As IEnumerable(Of DTOContent)
    Private _Cod As DTOContent.Srcs
    Private _DefaultValue As DTOContent
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        FchTo
        Title
        visitCount
    End Enum

    Public Shadows Sub Load(values As IEnumerable(Of DTOContent), Optional oCod As DTOContent.Srcs = DTOContent.Srcs.News, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        _Cod = oCod
        _SelectionMode = oSelectionMode
        _ControlItems = New ControlItems
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Select Case _Cod
            Case DTOContent.Srcs.Eventos
                MyBase.Columns(Cols.Fch).HeaderText = "Des de"
                MyBase.Columns(Cols.FchTo).Visible = True
            Case Else
                MyBase.Columns(Cols.Fch).HeaderText = "Data"
                MyBase.Columns(Cols.FchTo).Visible = False
        End Select

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()

    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As IEnumerable(Of DTOContent) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOContent In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As IEnumerable(Of DTOContent)
        Dim retval As IEnumerable(Of DTOContent)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.ToList.FindAll(Function(x) x.Title.Contains(_Filter.ToLower)).ToList
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

    Public ReadOnly Property Value As DTOContent
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOContent = oControlItem.Source
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
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FchTo)
            .HeaderText = "Fins"
            .DataPropertyName = "FchTo"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Title)
            .HeaderText = "Titol"
            .DataPropertyName = "Title"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.visitCount)
            .HeaderText = "Visites"
            .DataPropertyName = "visitCount"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0;-#,###0;#"
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

    Private Function SelectedItems() As List(Of DTOContent)
        Dim retval As New List(Of DTOContent)
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
            Dim oContent = SelectedItems.First.toSpecificObject
            Select Case oContent.Src
                Case DTOContent.Srcs.Blog
                    Dim oMenu_Blog As New Menu_Blog2Post(oContent)
                    AddHandler oMenu_Blog.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Blog.Range)
                Case DTOContent.Srcs.Content
                    Dim oMenu_Content As New Menu_Content(oContent)
                    AddHandler oMenu_Content.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Content.Range)
                Case Else
                    Dim oMenu_Noticia As New Menu_Noticia(oContent)
                    AddHandler oMenu_Noticia.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Noticia.Range)
            End Select
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
            Dim oSelectedValue As DTOContent = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.browse
                    Select Case oSelectedValue.Src
                        Case DTOContent.Srcs.Blog
                            Dim oFrm As New Frm_BlogPost(oSelectedValue)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                        Case Else
                            Dim oFrm As New Frm_Noticia(oSelectedValue)
                            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            oFrm.Show()
                    End Select
                Case DTO.Defaults.SelectionModes.selection
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

    Private Sub Xl_Contents2_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem.Source.visible Then
            oRow.DefaultCellStyle.BackColor = Color.White
        Else
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOContent

        Property Fch As Date
        Property FchTo As Date
        Property Title As String
        Property VisitCount As Integer

        Public Sub New(oContent As DTOContent)
            MyBase.New()
            _Source = oContent
            With oContent
                If .src = DTOContent.Srcs.Eventos Then
                    _Fch = CType(oContent, DTOEvento).FchFrom
                    _FchTo = CType(oContent, DTOEvento).FchTo
                Else
                    _Fch = .Fch
                End If
                _Title = .Title.Tradueix(Current.Session.Lang)
                _VisitCount = .visitCount
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


