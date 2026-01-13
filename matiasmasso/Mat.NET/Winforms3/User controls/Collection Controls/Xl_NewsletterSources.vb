Public Class Xl_NewsletterSources

    Inherits DataGridView

    Private _Values As List(Of DTONewsletterSource)
    Private _DefaultValue As DTOVisaEmisor
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Cod
        Title
    End Enum

    Public Shadows Sub Load(values As List(Of DTONewsletterSource), Optional oDefaultValue As DTONewsletterSource = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTONewsletterSource)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTONewsletterSource) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTONewsletterSource In FilteredValues()
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
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Title)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTONewsletterSource)
        Dim retval As List(Of DTONewsletterSource)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Title.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTONewsletterSource
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTONewsletterSource = oControlItem.Source
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
        With MyBase.Columns(Cols.Cod)
            .HeaderText = "Font"
            .DataPropertyName = "Cod"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Title)
            .HeaderText = "Titol"
            .DataPropertyName = "Title"
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

    Private Function SelectedItems() As List(Of DTONewsletterSource)
        Dim retval As New List(Of DTONewsletterSource)
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
        Dim oMenu As Menu_Base = Nothing

        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            Dim oNewsLetterSource As DTONewsletterSource = oControlItem.Source

            Select Case oNewsLetterSource.Cod
                Case DTONewsletterSource.Cods.News
                    Dim oNoticia As DTONoticia = Nothing
                    If TypeOf oNewsLetterSource.Tag Is Newtonsoft.Json.Linq.JObject Then
                        oNoticia = oNewsLetterSource.Tag.toObject(Of DTONoticia)
                    Else
                        oNoticia = oNewsLetterSource.Tag
                    End If
                    oMenu = New Menu_Noticia(oNoticia)
                Case DTONewsletterSource.Cods.Blog
                    Dim oBlogPost As DTOBlogPost = Nothing
                    If TypeOf oNewsLetterSource.Tag Is Newtonsoft.Json.Linq.JObject Then
                        oBlogPost = oNewsLetterSource.Tag.toObject(Of DTOBlogPost)
                    Else
                        oBlogPost = oNewsLetterSource.Tag
                    End If
                    oMenu = New Menu_BlogPost(oBlogPost)
                Case DTONewsletterSource.Cods.Events
                    Dim oEvent As DTOEvento = Nothing
                    If TypeOf oNewsLetterSource.Tag Is Newtonsoft.Json.Linq.JObject Then
                        oEvent = oNewsLetterSource.Tag.toObject(Of DTOEvento)
                    Else
                        oEvent = oNewsLetterSource.Tag
                    End If
                    oMenu = New Menu_Noticia(oEvent)
                Case DTONewsletterSource.Cods.Promo
                    Dim oIncentiu As DTOIncentiu = Nothing
                    If TypeOf oNewsLetterSource.Tag Is Newtonsoft.Json.Linq.JObject Then
                        oIncentiu = oNewsLetterSource.Tag.toObject(Of DTOIncentiu)
                    Else
                        oIncentiu = oNewsLetterSource.Tag
                    End If
                    oMenu = New Menu_Incentiu(oIncentiu)
            End Select

            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)
            oContextMenu.Items.Add("-")
        End If
        Dim oAddMenuItem As New ToolStripMenuItem("afegir")
        oContextMenu.Items.Add(oAddMenuItem)

        With oAddMenuItem.DropDownItems
            .Add(New ToolStripMenuItem("noticia", Nothing, AddressOf Do_AddNews))
            .Add(New ToolStripMenuItem("post del blog", Nothing, AddressOf Do_AddBlogPost))
            .Add(New ToolStripMenuItem("evento", Nothing, AddressOf Do_AddEvent))
            .Add(New ToolStripMenuItem("promo", Nothing, AddressOf Do_AddPromo))
        End With

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNews()
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(DTONewsletterSource.Cods.News))
    End Sub

    Private Sub Do_AddBlogPost()
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(DTONewsletterSource.Cods.Blog))
    End Sub

    Private Sub Do_AddEvent()
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(DTONewsletterSource.Cods.Events))
    End Sub

    Private Sub Do_AddPromo()
        RaiseEvent RequestToAddNew(Me, New MatEventArgs(DTONewsletterSource.Cods.Promo))
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTONewsletterSource = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Select Case oSelectedValue.Cod
                        Case DTONewsletterSource.Cods.News
                            'Dim oFrm As New Frm_NewsletterSource(oSelectedValue)
                            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                            'oFrm.Show()
                    End Select
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
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTONewsletterSource

        Property Cod As String
        Property Title As String

        Public Sub New(value As DTONewsletterSource)
            MyBase.New()
            _Source = value
            With value
                _Cod = .Cod.ToString
                _Title = .Title.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


