Public Class Xl_ProductChannels

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductChannel)
    Private _Lang As DTOLang
    Private _Mode As Modes
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Modes
        ChannelsPerProduct
        ProductsPerChannel
    End Enum

    Private Enum Cols
        ico
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductChannel), Mode As Modes)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _Mode = Mode
        _Lang = Current.Session.Lang
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOProductChannel) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOProductChannel In oFilteredValues
            Dim oControlItem As New ControlItem(oItem, _Mode, _Lang)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOProductChannel
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductChannel = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Canals"
            .DataPropertyName = "Nom"
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

    Private Function SelectedItems() As List(Of DTOProductChannel)
        Dim retval As New List(Of DTOProductChannel)
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
            Dim oProductChannel As DTOProductChannel = oControlItem.Source
            Dim oMenu_ProductChannel As New Menu_ProductChannel(oProductChannel)
            AddHandler oMenu_ProductChannel.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ProductChannel.Range)
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
            Dim oProductChannel As DTOProductChannel = oCurrentControlItem.Source
            Dim oFrm As New Frm_ProductChannel(oProductChannel)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_ProductChannels_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim item As DTOProductChannel = oControlItem.Source
                If item.Cod = DTOProductChannel.Cods.Inclou Then
                    e.Value = My.Resources.vb
                Else
                    e.Value = My.Resources.aspa
                End If
        End Select
    End Sub

    Private Sub Xl_ProductChannels_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
        MyBase.ClearSelection()
    End Sub

    Private Sub Xl_ProductChannels_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim item As DTOProductChannel = oControlItem.Source
        If item.Inherited Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOProductChannel

        Property Cod As DTOProductChannel.Cods
        Property Nom As String

        Public Sub New(value As DTOProductChannel, oMode As Modes, oLang As DTOLang)
            MyBase.New()
            _Source = value
            With value
                Select Case oMode
                    Case Modes.ChannelsPerProduct
                        _Nom = .DistributionChannel.LangText.Tradueix(oLang)
                    Case Modes.ProductsPerChannel
                        _Nom = .Product.FullNom()
                End Select
                _Cod = .Cod
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


