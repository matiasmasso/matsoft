Public Class Xl_Ibans_Old
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ico
        FchFrom
        Titular
        Iban
        FchDownloaded
        FchUploaded
        fchApproved
    End Enum

    Public Shadows Sub Load(oValues As List(Of DTOIban))
        Dim EmptyGrid As Boolean = _ControlItems Is Nothing
        _ControlItems = New ControlItems
        For Each oItem As DTOIban In oValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        If EmptyGrid Then
            LoadGrid()
            SetContextMenu()
            _AllowEvents = True
        Else
            DataGridView1.DataSource = _ControlItems
        End If
    End Sub

    Public ReadOnly Property Value As DTOIban
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOIban = oControlItem.Source
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


            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchFrom)
                .HeaderText = "Des de"
                .DataPropertyName = "FchFrom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Titular)
                .HeaderText = "Titular"
                .DataPropertyName = "Titular"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Iban)
                .HeaderText = "Iban"
                .DataPropertyName = "Iban"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchDownloaded)
                .HeaderText = "Baixat"
                .DataPropertyName = "FchDownloaded"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.FchUploaded)
                .HeaderText = "Pujat"
                .DataPropertyName = "FchUploaded"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.fchApproved)
                .HeaderText = "aprovat"
                .DataPropertyName = "fchApproved"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
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

    Private Function SelectedItems() As List(Of DTOIban)
        Dim retval As New List(Of DTOIban)
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
            Dim oMenu_Iban As New Menu_Iban(SelectedItems.First)
            AddHandler oMenu_Iban.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Iban.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oSelectedValue As DTOIban = CurrentControlItem.Source
        Dim oFrm As New Frm_Iban(oSelectedValue)
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
        Property Source As DTOIban

        Property FchFrom As Nullable(Of Date)
        Property Titular As String
        Property Iban As String
        Property FchDownloaded As Nullable(Of Date)
        Property FchUploaded As Nullable(Of Date)
        Property FchApproved As Nullable(Of Date)



        Public Sub New(oIban As DTOIban)
            MyBase.New()
            _Source = oIban
            With oIban
                If .FchFrom > Date.MinValue Then
                    _FchFrom = .FchFrom
                End If
                _Titular = .Titular.FullNom
                _Iban = .Digits
                If .FchDownloaded > Date.MinValue Then
                    _FchDownloaded = .FchDownloaded
                End If
                If .FchUploaded > Date.MinValue Then
                    _FchUploaded = .FchUploaded
                End If
                If .FchApproved > Date.MinValue Then
                    _FchApproved = .FchApproved
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

