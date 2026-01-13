Public Class Xl_RankingItems

    Private _Ranking As Ranking
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

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

    Public Shadows Sub Load(value As Ranking)
        If value IsNot Nothing Then
            _Ranking = value
            _ControlItems = New ControlItems
            Dim DcTot As Decimal = _Ranking.Items.Sum(Function(x) x.Amt.Eur)
            Dim DcAccumulated As Decimal = 0
            For Each oItem As RankingItem In _Ranking.Items
                DcAccumulated += oItem.Amt.Eur
                Dim oControlItem As New ControlItem(oItem, DcAccumulated, DcTot)
                _ControlItems.Add(oControlItem)
            Next
            LoadGrid()
        End If
    End Sub

    Public ReadOnly Property Value As RankingItem
        Get
            Dim retval As RankingItem = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property


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
            With .Columns(Cols.Order)
                .HeaderText = _Ranking.User.Lang.Tradueix("Orden", "Ordre", "Order")
                .DataPropertyName = "Order"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Pais)
                .HeaderText = _Ranking.User.Lang.Tradueix("Pais", "Pais", "Country")
                .DataPropertyName = "Pais"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Zona)
                .HeaderText = _Ranking.User.Lang.Tradueix("Zona", "Zona", "Zone")
                .DataPropertyName = "Zona"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Location)
                .HeaderText = _Ranking.User.Lang.Tradueix("Población", "Població", "Location")
                .DataPropertyName = "Location"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Customer)
                .HeaderText = _Ranking.User.Lang.Tradueix("Cliente", "Client", "Customer")
                .DataPropertyName = "Customer"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Units)
                .HeaderText = _Ranking.User.Lang.Tradueix("Unidades", "Unitats", "Units")
                .DataPropertyName = "Units"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Amt)
                .HeaderText = _Ranking.User.Lang.Tradueix("Importe", "Import", "Amount")
                .DataPropertyName = "Amt"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Share)
                .HeaderText = _Ranking.User.Lang.Tradueix("Cuota", "Quota", "Share")
                .DataPropertyName = "Share"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.ShareAccumulated)
                .HeaderText = _Ranking.User.Lang.Tradueix("Acum.", "Acum.", "Accum.")
                .DataPropertyName = "ShareAccumulated"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
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
            Dim oItem As RankingItem = oControlItem.Source
            Dim oContact As New Contact(oItem.Customer.Guid)
            Dim oMenu As New Menu_Contact(oContact)

            'Dim oMenu_RankingItem As New Menu_RankingItem(SelectedItems.First)
            'AddHandler oMenu_RankingItem.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        'Dim oSelectedValue As RankingItem = CurrentControlItem.Source
        'Dim oFrm As New Frm_RankingItem(oSelectedValue)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        ' oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Property Source As RankingItem

        Property Order As Integer
        Property Pais As String
        Property Zona As String
        Property Location As String
        Property Customer As String
        Property Units As Integer
        Property Amt As Decimal
        Property Share As Decimal
        Property ShareAccumulated As Decimal


        Public Sub New(oRankingItem As RankingItem, DcAccumulated As Decimal, DcTot As Decimal)
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
                _Share = 100 * _Amt / DcTot
                ShareAccumulated = 100 * DcAccumulated / DcTot
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

