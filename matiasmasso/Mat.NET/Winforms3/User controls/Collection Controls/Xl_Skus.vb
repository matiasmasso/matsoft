Public Class Xl_Skus
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductSku)
    Private _DefaultValue As DTOProductSku
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        Num
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductSku), Optional oDefaultValue As DTOProductSku = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values
        _SelectionMode = oSelectionMode
        _DefaultValue = oDefaultValue

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOProductSku) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOProductSku In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.nom.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOProductSku
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductSku = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowProductSku.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
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
            .Visible = _Values.Any(Function(x) x.Obsoleto)
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Num)
            .HeaderText = "Numero"
            .DataPropertyName = "Num"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
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

    Private Function SelectedItems() As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
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
            Dim oMenu_ProductSku As New Menu_ProductSku(SelectedItems.First)
            AddHandler oMenu_ProductSku.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ProductSku.Range)
            If _SelectionMode = DTO.Defaults.SelectionModes.Browse Then
                oContextMenu.Items.Add("-")
            End If
        End If

        If _SelectionMode = DTO.Defaults.SelectionModes.Browse Then
            oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        sortida()
    End Sub

    Private Sub sortida()
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductSku = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_ProductSku(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Sortida()
                e.Handled = True
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oProductSku As DTOProductSku = oControlItem.Source
        'oRow.DefaultCellStyle.BackColor = LegacyHelper.ImageHelper.Converter(DTOProductSku.BackColor(oProductSku))
    End Sub
    Private Sub Xl_Skus_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.ico
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oProductSku As DTOProductSku = oControlItem.Source
                If oProductSku.Obsoleto Then
                    e.Value = My.Resources.aspa
                End If
        End Select
    End Sub

#Region "Sizes"

    Public Function WidthAdjustment() As Integer
        Dim oGraphics As Graphics = Me.CreateGraphics()
        Dim iMaxWidth As Integer
        For Each oItem As ControlItem In _ControlItems
            Dim iWidth As Integer = DataGridViewCell.MeasureTextWidth(oGraphics, oItem.Nom, Me.Font, Me.RowTemplate.Height, TextFormatFlags.Left)
            If iWidth > iMaxWidth Then iMaxWidth = iWidth
        Next

        Dim iOriginalColWidth As Integer = Me.Columns(Cols.Nom).Width
        Dim retval As Integer = iMaxWidth - iOriginalColWidth
        Return retval
    End Function

    Public Function AdjustedHeight() As Integer
        Dim MaxVisibleRows As Integer = 16
        Dim VisibleRows As Integer = 0
        If _ControlItems.Count <= MaxVisibleRows Then
            VisibleRows = _ControlItems.Count
        Else
            VisibleRows = MaxVisibleRows
        End If

        Dim retval As Integer = Me.RowTemplate.Height * VisibleRows + 3
        Return retval
    End Function


#End Region


    Protected Class ControlItem
        Property Source As DTOProductSku

        Property Num As Integer
        Property Nom As String

        Public Sub New(value As DTOProductSku)
            MyBase.New()
            _Source = value
            With value
                _Num = .Id
                _Nom = .RefYNomLlarg.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

