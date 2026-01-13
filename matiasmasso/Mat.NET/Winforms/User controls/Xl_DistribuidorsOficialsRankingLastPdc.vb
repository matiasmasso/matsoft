Public Class Xl_DistribuidorsOficialsRankingLastPdc
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOCliProductBlocked)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Rank
        Fch
        Cli
        Product
        Cod
        Obs
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCliProductBlocked))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Dim oFilteredValues As List(Of DTOCliProductBlocked) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOCliProductBlocked In oFilteredValues
            Dim oControlItem As New ControlItem(oItem, _ControlItems.Count)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOCliProductBlocked)
        Dim retval As List(Of DTOCliProductBlocked)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Contact.FullNom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOCliProductBlocked
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCliProductBlocked = oControlItem.Source
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
        With MyBase.Columns(Cols.Rank)
            .HeaderText = "Ranking"
            .DataPropertyName = "Rank"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "000"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cli)
            .HeaderText = "Client"
            .DataPropertyName = "Cli"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Product)
            .HeaderText = "Producte"
            .DataPropertyName = "Product"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cod)
            .HeaderText = "Cod"
            .DataPropertyName = "Cod"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Obs)
            .HeaderText = "Observacions"
            .DataPropertyName = "Obs"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
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

    Private Function SelectedItems() As List(Of DTOCliProductBlocked)
        Dim retval As New List(Of DTOCliProductBlocked)
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
            Dim oMenu_CliProductBlocked As New Menu_CliProductBlocked(SelectedItems)
            AddHandler oMenu_CliProductBlocked.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_CliProductBlocked.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf Do_Refresca)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Refresca()
        RefreshRequest(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Dim oSheet = FEB2.CliProductsBlocked.ExcelRankingLastPdc(_Values)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
        Cursor = Cursors.Default
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOCliProductBlocked = CurrentControlItem.Source
            Dim oFrm As New Frm_CliProductBlocked(oSelectedValue)
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



    Protected Class ControlItem
        Property Source As DTOCliProductBlocked

        Property Rank As Integer
        Property Fch As Date
        Property Cli As String
        Property Product As String
        Property Cod As String
        Property Obs As String

        Public Sub New(value As DTOCliProductBlocked, iRank As Integer)
            MyBase.New()
            _Source = value
            With value
                _Rank = iRank
                _Fch = .LastFch
                _Cli = .Contact.FullNom
                _Product = CType(.product, DTOProduct).nom.Tradueix(Current.Session.Lang)
                _Cod = .Cod.ToString
                _Obs = .Obs
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

