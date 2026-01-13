Public Class Xl_RankingItems
    Inherits _Xl_ReadOnlyDatagridview

    Private _Ranking As DTORanking

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Order
        Pais
        Zona
        Location
        Customer
        Units
        Amt
        Share
        ShareAccumulated
    End Enum

    Public Shadows Sub Load(value As DTORanking)
        _Ranking = value

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTORankingItem) = FilteredValues()
        _ControlItems = New ControlItems
        Dim DcTot As Decimal = _Ranking.Items.Sum(Function(x) x.Amt.Eur)
        Dim DcAccumulated As Decimal = 0
        For Each oItem As DTORankingItem In oFilteredValues
            DcAccumulated += oItem.Amt.Eur
            Dim oControlItem As New ControlItem(oItem, DcAccumulated, DcTot)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTORankingItem)
        Dim retval As List(Of DTORankingItem)
        If _Filter = "" Then
            retval = _Ranking.Items
        Else
            retval = _Ranking.Items.FindAll(Function(x) x.Customer.FullNom.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Ranking IsNot Nothing AndAlso _Ranking.Items IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTORankingItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTORankingItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowRanking.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Order)
            .HeaderText = _Ranking.User.Lang.Tradueix("Orden", "Ordre", "Order")
            .DataPropertyName = "Order"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pais)
            .HeaderText = _Ranking.User.Lang.Tradueix("Pais", "Pais", "Country")
            .DataPropertyName = "Pais"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Zona)
            .HeaderText = _Ranking.User.Lang.Tradueix("Zona", "Zona", "Zone")
            .DataPropertyName = "Zona"
            .Width = 150
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Location)
            .HeaderText = _Ranking.User.Lang.Tradueix("Población", "Població", "Location")
            .DataPropertyName = "Location"
            .Width = 150
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Customer)
            .HeaderText = _Ranking.User.Lang.Tradueix("Cliente", "Client", "Customer")
            .DataPropertyName = "Customer"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Units)
            .HeaderText = _Ranking.User.Lang.Tradueix("Unidades", "Unitats", "Units")
            .DataPropertyName = "Units"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = _Ranking.User.Lang.Tradueix("Importe", "Import", "Amount")
            .DataPropertyName = "Amt"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Share)
            .HeaderText = _Ranking.User.Lang.Tradueix("Cuota", "Quota", "Share")
            .DataPropertyName = "Share"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.ShareAccumulated)
            .HeaderText = _Ranking.User.Lang.Tradueix("Acum.", "Acum.", "Accum.")
            .DataPropertyName = "ShareAccumulated"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
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

    Private Function SelectedItems() As List(Of DTORankingItem)
        Dim retval As New List(Of DTORankingItem)
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
            Dim oItem As DTORankingItem = oControlItem.Source
            Dim oMenu As New Menu_Contact(oItem.Customer)
            oContextMenu.Items.AddRange(oMenu.Range)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            'Dim oSelectedValue As DTORanking = CurrentControlItem.Source
            'Dim oFrm As New Frm_Ranking(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTORankingItem

        Property Order As Integer
        Property Pais As String
        Property Zona As String
        Property Location As String
        Property Customer As String
        Property Units As Integer
        Property Amt As Decimal
        Property Share As Decimal
        Property ShareAccumulated As Decimal


        Public Sub New(oRankingItem As DTORankingItem, DcAccumulated As Decimal, DcTot As Decimal)
            MyBase.New()
            _Source = oRankingItem
            With oRankingItem
                _Order = .Order
                _Pais = .Location.Zona.Country.ISO
                _Zona = .Location.Zona.Nom
                _Location = .Location.Nom
                _Customer = .Customer.Nom
                _Units = .Units
                _Amt = .Amt.Eur
                If DcTot <> 0 Then
                    _Share = 100 * _Amt / DcTot
                    ShareAccumulated = 100 * DcAccumulated / DcTot
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


