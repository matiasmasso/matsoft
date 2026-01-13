Public Class Xl_Spvs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOSpv)
    Private _DefaultValue As DTOSpv
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _Extended As Boolean
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        id
        Fch
        Llegit
        Arribat
        Sortit
        Customer
        Product
    End Enum

    Public Shadows Sub Load(values As List(Of DTOSpv),
                            Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse,
                            Optional Extended As Boolean = False)
        _Values = values
        _SelectionMode = oSelectionMode
        _Extended = Extended

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOSpv) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOSpv In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Function FilteredValues() As List(Of DTOSpv)
        Dim retval As List(Of DTOSpv)
        If _Filter = "" Then
            retval = _Values
        Else
            Dim sFilter = _Filter.ToLower
            retval = _Values.FindAll(Function(x) Matching(x, sFilter))
        End If
        Return retval
    End Function

    Private Function Matching(oSpv As DTOSpv, candidate As String) As Boolean
        Dim retval As Boolean
        If IsNumeric(candidate) Then
            retval = (oSpv.Id = CInt(candidate))
        Else
            If oSpv.Customer.FullNom.ToLower.Contains(candidate) Then
                retval = True
            ElseIf oSpv.Product IsNot Nothing Then
                retval = oSpv.Product.Nom.ToLower.Contains(candidate)
            End If
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

    Public ReadOnly Property Value As DTOSpv
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOSpv = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowSpv.DefaultCellStyle.BackColor = Color.Transparent

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
            .HeaderText = "Numero"
            .DataPropertyName = "Id"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Avís"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Llegit), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = "Llegit"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 35
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
            .Visible = _Extended
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Arribat), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = "Arribat"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 35
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
            .Visible = _Extended
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Sortit), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = "Sortit"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 35
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
            .Visible = _Extended
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Customer)
            .HeaderText = "Client"
            .DataPropertyName = "Customer"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Product)
            .HeaderText = "Producte"
            .DataPropertyName = "Product"
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

    Private Function SelectedItems() As List(Of DTOSpv)
        Dim retval As New List(Of DTOSpv)
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
            Dim oMenu_Spv As New Menu_Spv(SelectedItems.First)
            AddHandler oMenu_Spv.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Spv.Range)
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
            Dim oSelectedValue As DTOSpv = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Spv(oSelectedValue)
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

    Private Sub Xl_Spvs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        If _Extended Then
            Dim oRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem = oRow.DataBoundItem
            Dim oSpv As DTOSpv = oControlItem.source
            Select Case e.ColumnIndex
                Case Cols.Llegit
                    If oSpv.FchRead <> Nothing Then e.Value = My.Resources.vb
                Case Cols.Arribat
                    If oSpv.SpvIn Is Nothing Then
                        If oSpv.UsrOutOfSpvIn IsNot Nothing Then
                            e.Value = My.Resources.aspa
                        End If
                    Else
                        e.Value = My.Resources.vb
                    End If
                Case Cols.Sortit
                    If oSpv.Delivery Is Nothing Then
                        If oSpv.UsrOutOfSpvOut IsNot Nothing Then
                            e.Value = My.Resources.aspa
                        End If
                    Else
                        e.Value = My.Resources.vb
                    End If
            End Select
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOSpv

        Property id As Integer
        Property Fch As Date
        Property Customer As String
        Property Product As String


        Public Sub New(value As DTOSpv)
            MyBase.New()
            _Source = value
            With value
                _id = .Id
                _Fch = .FchAvis
                _Customer = .Customer.FullNom

                If .Product IsNot Nothing Then
                    _Product = DirectCast(.Product, DTOProduct).FullNom(Current.Session.Lang)
                End If

            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


