Public Class Xl_Raffles
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        FchFrom
        FchTo
        Titol
        Participants
        NewParticipants
        Share 'porcentatge de leads nous
        CostPrize
        CostPubli
        CPL
        Status
    End Enum

    Public Shadows Sub Load(values As List(Of DTORaffle))
        _ControlItems = New ControlItems
        For Each oItem As DTORaffle In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
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
            .Columns.Add(New DataGridViewImageColumn)
            With CType(.Columns(Cols.Status), DataGridViewImageColumn)
                .DataPropertyName = "Status"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
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
            Select Case oControlItem.Source.Codi
                Case DTORaffle.Codis.Contest
                    'Dim oMenu_Contest As New Menu_Contest(SelectedItems.First)
                    'AddHandler oMenu_Contest.AfterUpdate, AddressOf RefreshRequest
                    'oContextMenu.Items.AddRange(oMenu_Contest.Range)
                Case DTORaffle.Codis.Raffle
                    Dim oMenu_Raffle As New Menu_Raffle(SelectedItems.First)
                    AddHandler oMenu_Raffle.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Raffle.Range)
            End Select

            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add("afegir sorteig", Nothing, AddressOf Do_AddNewRaffle)
        oContextMenu.Items.Add("afegir concurs", Nothing, AddressOf Do_AddNewContest)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Excel()
        Dim oSheet As DTOExcelSheet = BLL.BLLRaffles.Excel(Me.Values)
        UIHelper.ShowExcel(oSheet)
    End Sub

    Private Sub Do_AddNewRaffle()
        Dim oRaffle As DTORaffle = BLL.BLLRaffle.NewRaffle
        Dim oFrm As New Frm_Raffle(oRaffle)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNewContest()
        'Dim oRaffle As DTOContest = BLL.BLLRaffle.NewRaffle
        'Dim oFrm As New Frm_Raffle(oRaffle)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTORaffle = CurrentControlItem.Source
        Select Case oSelectedValue.Codi
            Case DTORaffle.Codis.Raffle
                Dim oFrm As New Frm_Raffle(oSelectedValue)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            Case DTORaffle.Codis.Contest
                'Dim oRaffle As DTOContest = BLL.BLLRaffle.NewRaffle
                'Dim oFrm As New Frm_Raffle(oRaffle)
                'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                'oFrm.Show()
        End Select
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
        Property Title As String
        Property ParticipantsCount As Integer
        Property NewParticipantsCount As Integer
        Property Share As Decimal
        Property CostPrize As Decimal
        Property CostPubli As Decimal
        Property CPL As Decimal
        Property Status As Image


        Public Sub New(oRaffle As DTORaffle)
            'Dim oNewItems As List(Of DTORaffleParticipant) = .Participants.FindAll(Function(x) x.Usuari.FchCreated > x.Raffle.FchFrom)

            MyBase.New()
            _Source = oRaffle
            With oRaffle
                _FchFrom = .FchFrom
                _FchTo = .FchTo
                _Title = .Title
                Select Case .Codi
                    Case DTORaffle.Codis.Contest
                        'Dim oContest As DTOContest = CType(oRaffle, DTOContest)
                        '_ParticipantsCount = oContest.Participants.Count
                        '_NewParticipantsCount = oContest.Participants.FindAll(Function(x) x.User.FchCreated > x.Parent.FchFrom).Count
                    Case DTORaffle.Codis.Raffle
                        'Dim oRaffle As DTORaffle = CType(oRaffle, DTORaffle)
                        _ParticipantsCount = oRaffle.ParticipantsCount
                        _NewParticipantsCount = oRaffle.NewParticipantsCount
                End Select
                If _ParticipantsCount > 0 Then
                    _Share = 100 * _NewParticipantsCount / _ParticipantsCount
                End If

                If .CostPrize IsNot Nothing Then
                    _CostPrize = .CostPrize.Eur
                End If

                If .CostPubli IsNot Nothing Then
                    _CostPubli = .CostPubli.Eur
                End If

                If _NewParticipantsCount > 0 Then
                    _CPL = (_CostPrize + _CostPubli) / _NewParticipantsCount
                End If

                If oRaffle.Codi = DTORaffle.Codis.Raffle Then
                    If oRaffle.FchPicture > Nothing Then
                        _Status = My.Resources.vb
                    ElseIf oRaffle.FchDelivery > Nothing Then
                        _Status = My.Resources.star_blue
                    ElseIf oRaffle.FchDistributorReaction > Nothing Then
                        _Status = My.Resources.star
                    ElseIf oRaffle.FchWinnerReaction > Nothing Then
                        _Status = My.Resources.star_red
                    End If
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class

