Public Class Xl_RaffleParticipants
    Private _Raffle As DTORaffle
    Private _values As List(Of DTORaffleParticipant)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _Mode As Modes
    Private _Label As String

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Enum Modes
        FromRaffle
        FromUsuari
    End Enum

    Private Enum Cols
        Num
        Fch
        text
    End Enum

    Public Shadows Sub Load(oRaffle As DTORaffle, values As List(Of DTORaffleParticipant), oMode As Modes, Optional Label As String = "")
        _Raffle = oRaffle
        _values = values
        _ControlItems = New ControlItems
        _Label = Label
        LoadGrid()
    End Sub

    Public ReadOnly Property Value As DTORaffleParticipant
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTORaffleParticipant = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        Dim search As String = TextBoxSearch.Text
        _ControlItems = New ControlItems
        For Each oItem As DTORaffleParticipant In _values
            If oItem.User.EmailAddress.ToString.Contains(search) Then
                Dim oControlItem As New ControlItem(oItem, _Mode)
                _ControlItems.Add(oControlItem)
            End If
        Next

        LabelCount.Text = _ControlItems.Count & IIf(_Label = "", " participants", _Label)

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
            With .Columns(Cols.Num)
                .HeaderText = "Numero"
                .DataPropertyName = "Num"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 110
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.text)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DataPropertyName = "text"
                Select Case _Mode
                    Case Modes.FromRaffle
                        .HeaderText = "email"
                    Case Modes.FromUsuari
                        .HeaderText = "sorteig"
                End Select
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

    Private Function SelectedItems() As List(Of DTORaffleParticipant)
        Dim retval As New List(Of DTORaffleParticipant)
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
            Dim oMenu_RaffleParticipant As New Menu_RaffleParticipant(SelectedItems)
            AddHandler oMenu_RaffleParticipant.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_RaffleParticipant.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("eliminar tots els participants", Nothing, AddressOf Do_Reset)
        oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentParticipant() As DTORaffleParticipant
        Dim retval As DTORaffleParticipant = SelectedItems.First
        retval.Raffle = _Raffle
        Return retval
    End Function

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue = CurrentParticipant()
        Dim oFrm As New Frm_RaffleParticipant(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem.Source.User.FchCreated > _Raffle.FchFrom Then
            'nou lead
            oRow.DefaultCellStyle.BackColor = Color.White
        Else
            'repetidor
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If

    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Do_Reset()
        MsgBox("no implamentat encara")
    End Sub
    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTORaffleParticipant

        Property Num As String
        Property fch As Date
        Property text As String

        Public Sub New(oRaffleParticipant As DTORaffleParticipant, oMode As Modes)
            MyBase.New()
            _Source = oRaffleParticipant
            With oRaffleParticipant
                _Num = Format(.Num, "0000")
                _fch = .Fch
                Select Case oMode
                    Case Modes.FromRaffle
                        _text = .User.EmailAddress
                    Case Modes.FromUsuari
                        _text = .Raffle.Title
                End Select
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        LoadGrid()
    End Sub


End Class
