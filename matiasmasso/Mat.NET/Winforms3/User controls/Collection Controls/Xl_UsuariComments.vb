Public Class Xl_UsuariComments
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPostComment)
    Private _DefaultValue As DTOPostComment
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse
    Private _BlogPosts As List(Of DTOBlogPost)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        fch
        lang
        usr
        post
        comment
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPostComment), oBlogPosts As List(Of DTOBlogPost), Optional oDefaultValue As DTOPostComment = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _BlogPosts = oBlogPosts
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oLang As DTOLang = Current.Session.Lang
        _ControlItems = New ControlItems
        For Each oItem As DTOPostComment In FilteredValues()
            Dim oControlItem As New ControlItem(oItem, oLang, _BlogPosts)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPostComment)
        Dim retval As List(Of DTOPostComment)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.User.NickName.ToLower.Contains(_Filter.ToLower) Or x.User.EmailAddress.ToLower.Contains(_Filter.ToLower) Or x.Text.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOPostComment
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPostComment = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPostComment.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.lang)
            .HeaderText = "Idioma"
            .DataPropertyName = "Lang"
            .Width = 45
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.usr)
            .HeaderText = "Usuari"
            .DataPropertyName = "Usr"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.post)
            .HeaderText = "Post"
            .DataPropertyName = "Post"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.comment)
            .HeaderText = "Comentari"
            .DataPropertyName = "Comment"
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

    Private Function SelectedItems() As List(Of DTOPostComment)
        Dim retval As New List(Of DTOPostComment)
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
            If (SelectedItems.First.ParentSource = DTOPostComment.ParentSources.Shop4moms) Then
                oContextMenu.Items.Add("Zoom", Nothing, AddressOf ZoomShop4momsComment)
            Else
                Dim oMenu_PostComment As New Menu_PostComment(SelectedItems.First)
                AddHandler oMenu_PostComment.AfterUpdate, AddressOf RefreshRequest
                AddHandler oMenu_PostComment.RequestToToggleProgressBar, AddressOf ToggleProgressBarRequest
                oContextMenu.Items.AddRange(oMenu_PostComment.Range)
                oContextMenu.Items.Add("-")
            End If
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ZoomShop4momsComment()
        Dim oPostComment = SelectedItems.First
        Dim url = "https://www.4moms.es/backoffice/msg/" & oPostComment.Guid.ToString()
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPostComment = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.browse
                    Dim oFrm As New Frm_PostComment(oSelectedValue)
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


    Protected Class ControlItem
        Public Property Source As DTOPostComment

        Public Property Fch As Date
        Public Property Lang As String
        Public Property Usr As String
        Public Property Post As String
        Public Property Comment As String

        Public Sub New(oPostComment As DTOPostComment, oLang As DTOLang, oBlogPosts As List(Of DTOBlogPost))
            MyBase.New()
            _Source = oPostComment
            With oPostComment
                _Fch = .Fch
                _Lang = .Lang.Tag
                _Usr = DTOUser.NicknameOrElse(oPostComment.User)
                If oPostComment.Parent.Equals(DTOContent.Wellknown(DTOContent.Wellknowns.consultasBlog).Guid) Then
                    _Post = Current.Session.Tradueix("Consultas del Blog", "Consultes del Blog", "Blog queries")
                Else
                    _Post = oPostComment.ParentTitle.Tradueix(oLang)
                End If
                If String.IsNullOrEmpty(_Post) Then
                    If oBlogPosts.Any(Function(x) x.Guid.Equals(.Parent)) Then
                        _Post = oBlogPosts.FirstOrDefault(Function(x) x.Guid.Equals(.Parent)).Title.Tradueix(Current.Session.Lang())
                    Else
                        _Post = Source.ParentSource.ToString
                    End If
                End If
                _Comment = oPostComment.Text
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class
