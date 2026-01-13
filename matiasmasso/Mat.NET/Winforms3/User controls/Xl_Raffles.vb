Public Class Xl_Raffles
    Private _values As List(Of DTORaffle)
    Private _ControlItems As ControlItems
    Private _Filter As DTOProductBrand
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        FchFrom
        FchTo
        Lang
        Titol
        Participants
        NewParticipants
        Share 'porcentatge de leads nous
        CostPrize
        CostPubli
        CPL
        Shares
        Status
    End Enum

    Public Shadows Sub Load(values As List(Of DTORaffle))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _values = values
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTORaffle) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTORaffle In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        DataGridView1.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            DataGridView1.CurrentCell = DataGridView1.FirstDisplayedCell
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTORaffle
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTORaffle = oControlItem.Source
            Return retval
        End Get
    End Property

    Public ReadOnly Property Values As List(Of DTORaffle)
        Get
            Dim retval As New List(Of DTORaffle)
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Private Function FilteredValues() As List(Of DTORaffle)
        Dim retval As List(Of DTORaffle)
        If _Filter Is Nothing Then
            retval = _values
        Else
            retval = _values.FindAll(Function(x) x.Brand IsNot Nothing AndAlso x.Brand.Equals(_Filter))
        End If
        Return retval
    End Function

    Public Property Filter As DTOProductBrand
        Get
            Return _Filter
        End Get
        Set(value As DTOProductBrand)
            _Filter = value
            If _values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter IsNot Nothing Then
            _Filter = Nothing
            Refresca()
        End If
    End Sub

    Private Sub SetProperties()
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
            With .Columns(Cols.FchFrom)
                .HeaderText = "Inici"
                .DataPropertyName = "FchFrom"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchTo)
                .HeaderText = "Final"
                .DataPropertyName = "FchTo"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Lang)
                .HeaderText = "Idioma"
                .DataPropertyName = "Lang"
                .Width = 45
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Titol)
                .HeaderText = "Titol"
                .DataPropertyName = "Title"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Participants)
                .HeaderText = "Leads"
                .DataPropertyName = "ParticipantsCount"
                .Width = 55
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
                .DefaultCellStyle.Format = "#,###"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.NewParticipants)
                .HeaderText = "Nous"
                .DataPropertyName = "NewParticipantsCount"
                .Width = 55
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
                .DefaultCellStyle.Format = "#,###"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Share)
                .HeaderText = ""
                .DataPropertyName = "Share"
                .Width = 55
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.CostPrize)
                .HeaderText = "Premi"
                .DataPropertyName = "CostPrize"
                .Width = 55
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
                .DefaultCellStyle.Format = "#,###.00 €;-#,###.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.CostPubli)
                .HeaderText = "Publicitat"
                .DataPropertyName = "CostPubli"
                .Width = 55
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
                .DefaultCellStyle.Format = "#,###.00 €;-#,###.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.CPL)
                .HeaderText = "CPL"
                .DataPropertyName = "CPL"
                .Width = 55
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Shares)
                .HeaderText = "Shares"
                .DataPropertyName = "Shares"
                .Width = 55
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
                .DefaultCellStyle.Format = "#,###"
            End With
            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Status), DataGridViewImageColumn)
                .DataPropertyName = "Status"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
        End With
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

    Private Function SelectedItems() As List(Of DTORaffle)
        Dim retval As New List(Of DTORaffle)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
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
            Dim oMenu_Raffle As New Menu_Raffle(SelectedItems.First)
            AddHandler oMenu_Raffle.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Raffle.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add("afegir sorteig", Nothing, AddressOf Do_AddNewRaffle)
        oContextMenu.Items.Add("afegir concurs", Nothing, AddressOf Do_AddNewContest)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        Dim oSheet = FEB.Raffles.Excel(Me.Values)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_AddNewRaffle()
        Dim exs As New List(Of Exception)
        Dim oLang = Current.Session.Lang
        Dim oRaffle = DTORaffle.Factory(oLang)
        If FEB.Country.Load(oRaffle.Country, exs) Then
            Dim oFrm As New Frm_Raffle(oRaffle)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_AddNewContest()
        'Dim oRaffle As DTOContest = DTORaffle.Factory
        'Dim oFrm As New Frm_Raffle(oRaffle)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTORaffle = CurrentControlItem.Source
        Dim oFrm As New Frm_Raffle(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub


    Protected Class ControlItem
        Property Source As DTORaffle

        Property FchFrom As Date
        Property FchTo As Date
        Property Lang As String
        Property Title As String
        Property ParticipantsCount As Integer
        Property NewParticipantsCount As Integer
        Property Share As Decimal
        Property CostPrize As Decimal
        Property CostPubli As Decimal
        Property CPL As Decimal
        Property Shares As Integer
        Property Status As Image


        Public Sub New(oRaffle As DTORaffle)
            'Dim oNewItems As List(Of DTORaffleParticipant) = .Participants.FindAll(Function(x) x.Usuari.FchCreated > x.Raffle.FchFrom)

            MyBase.New()
            _Source = oRaffle
            With oRaffle
                _FchFrom = .FchFrom
                _FchTo = .FchTo
                _Lang = .Lang.Tag
                _Title = .Title
                _ParticipantsCount = oRaffle.ParticipantsCount
                _NewParticipantsCount = oRaffle.NewParticipantsCount
                _Share = oRaffle.RateNewLeads()

                If .CostPrize IsNot Nothing Then
                    _CostPrize = .CostPrize.Eur
                End If

                If .CostPubli IsNot Nothing Then
                    _CostPubli = .CostPubli.Eur
                End If

                If _NewParticipantsCount > 0 Then
                    _CPL = (_CostPrize + _CostPubli) / _NewParticipantsCount
                End If

                _Shares = .Shares

                Select Case oRaffle.Status
                    Case DTORaffle.Statuses.WinnerPictureSubmitted
                        _Status = My.Resources.vb
                    Case DTORaffle.Statuses.Delivered
                        _Status = My.Resources.star_blue
                    Case DTORaffle.Statuses.DistributorReacted
                        _Status = My.Resources.star
                    Case DTORaffle.Statuses.WinnerReacted
                        _Status = My.Resources.star_red
                End Select
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

