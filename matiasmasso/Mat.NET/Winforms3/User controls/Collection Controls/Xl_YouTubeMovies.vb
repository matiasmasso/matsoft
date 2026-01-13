Public Class Xl_YouTubeMovies
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOYouTubeMovie)
    Private _DefaultValue As DTOYouTubeMovie
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        id
        fch
        nom
        lang
        esp
        cat
        eng
        por
        imgIco
        duration
    End Enum

    Public Shadows Sub Load(values As List(Of DTOYouTubeMovie), Optional oDefaultValue As DTOYouTubeMovie = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOYouTubeMovie) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOYouTubeMovie In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOYouTubeMovie)
        Dim retval As List(Of DTOYouTubeMovie)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.Contains(_Filter) Or x.YoutubeId.Contains(_Filter))
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

    Public ReadOnly Property Value As DTOYouTubeMovie
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOYouTubeMovie = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowYouTube.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.id)
            .HeaderText = "Youtube Id"
            .DataPropertyName = "Id"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
        End With

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
        With MyBase.Columns(Cols.nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.lang)
            .HeaderText = "Idioma"
            .DataPropertyName = "Lang"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 45
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.esp)
            .HeaderText = "es"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 19
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.cat)
            .HeaderText = "ca"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 19
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.eng)
            .HeaderText = "en"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 19
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.por)
            .HeaderText = "pt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 19
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ImgIco), DataGridViewImageColumn)
            .HeaderText = ""
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.duration)
            .HeaderText = "Duració"
            .DataPropertyName = "Duration"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 50
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
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

    Private Function SelectedItems() As List(Of DTOYouTubeMovie)
        Dim retval As New List(Of DTOYouTubeMovie)
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
            Dim oMenu_YouTubeMovie As New Menu_YouTubeMovie(SelectedItems.First)
            AddHandler oMenu_YouTubeMovie.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_YouTubeMovie.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Shadows Sub RefreshRequest(sender As Object, e As MatEventArgs)
        MyBase.RefreshRequest(sender, e)
        MyBase.ClearSelection()
        'MyBase.Refresh()
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOYouTubeMovie = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Youtube(oSelectedValue)
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

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlitem As ControlItem = oRow.DataBoundItem
        Dim oYoutubeMovie As DTOYouTubeMovie = oControlitem.Source
        If oYoutubeMovie.Obsoleto Then
            oRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray
        End If
    End Sub

    Private Sub Xl_ProductSkusExtended_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.imgIco

                Dim oRow As DataGridViewRow = Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOYouTubeMovie = oControlItem.Source

                If oItem.HasThumbnail() Then
                    e.Value = My.Resources.img_16
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.esp
                Dim oRow As DataGridViewRow = Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOYouTubeMovie = oControlItem.Source
                If oItem.LangSet.HasLang(DTOLang.ESP) Then
                    e.CellStyle.BackColor = Color.LightGreen
                End If
            Case Cols.cat
                Dim oRow As DataGridViewRow = Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOYouTubeMovie = oControlItem.Source
                If oItem.LangSet.HasLang(DTOLang.CAT) Then
                    e.CellStyle.BackColor = Color.LightGreen
                End If
            Case Cols.eng
                Dim oRow As DataGridViewRow = Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOYouTubeMovie = oControlItem.Source
                If oItem.LangSet.HasLang(DTOLang.ENG) Then
                    e.CellStyle.BackColor = Color.LightGreen
                End If
            Case Cols.por
                Dim oRow As DataGridViewRow = Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem As DTOYouTubeMovie = oControlItem.Source
                If oItem.LangSet.HasLang(DTOLang.POR) Then
                    e.CellStyle.BackColor = Color.LightGreen
                End If
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOYouTubeMovie

        Property Id As String
        Property Fch As Date
        Property Nom As String
        Property Lang As String
        Property Duration As String

        Public Sub New(value As DTOYouTubeMovie)
            MyBase.New()
            _Source = value
            With value
                _Id = .YoutubeId
                _Fch = .FchCreated
                _Nom = .Nom.Tradueix(Current.Session.Lang)
                If String.IsNullOrEmpty(_Nom) AndAlso .Lang IsNot Nothing Then
                    _Nom = .Nom.Text(.Lang)
                End If
                If .Lang IsNot Nothing Then
                    _Lang = .Lang.Tag
                End If
                If .Duration IsNot Nothing Then
                    Dim dtDuration As TimeSpan = CType(.Duration, TimeSpan)
                    _Duration = New DateTime(dtDuration.Ticks).ToString("HH:mm:ss")
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


