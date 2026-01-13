Public Class Xl_SurveyParticipants

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOSurveyParticipant)
    Private _MaxSurveyScores As Integer
    Private _DefaultValue As DTOSurveyParticipant
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fch
        Nom
        Value
        Ico
    End Enum

    Public Shadows Sub Load(values As List(Of DTOSurveyParticipant), Optional oDefaultValue As DTOSurveyParticipant = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        If _Values.Count > 0 Then _MaxSurveyScores = BLLSurvey.MaxScores(_Values.First.Survey)
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOSurveyParticipant) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOSurveyParticipant In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOSurveyParticipant)
        Dim retval As List(Of DTOSurveyParticipant)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.User.EmailAddress.ToLower.Contains(_Filter.ToLower) Or x.User.NickName.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOSurveyParticipant
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOSurveyParticipant = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowSurveyParticipant.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm:ss"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Participant"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Value)
            .HeaderText = "Valoració"
            .DataPropertyName = "Valoracio"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With CType(MyBase.Columns(Cols.ico), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
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

    Private Function SelectedItems() As List(Of DTOSurveyParticipant)
        Dim retval As New List(Of DTOSurveyParticipant)
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
            Dim oMenu_SurveyParticipant As New Menu_SurveyParticipant(SelectedItems.First)
            AddHandler oMenu_SurveyParticipant.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_SurveyParticipant.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("refresca", Nothing, AddressOf Do_Refresh)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Refresh()
        MyBase.RefreshRequest(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOSurveyParticipant = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_SurveyParticipant(oSelectedValue)
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

    Private Sub Xl_SurveyParticipants_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Value
                If _MaxSurveyScores > 0 Then
                    Dim oRow As DataGridViewRow = CType(sender, DataGridView).Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Select Case oControlItem.Valoracio
                        Case 0
                            e.CellStyle.BackColor = Color.White
                        Case >= 76
                            e.CellStyle.BackColor = Color.FromArgb(204, 255, 153)
                        Case >= 51
                            e.CellStyle.BackColor = Color.FromArgb(255, 255, 153)
                        Case >= 26
                            e.CellStyle.BackColor = Color.FromArgb(255, 204, 153)
                        Case Else
                            e.CellStyle.BackColor = Color.FromArgb(255, 153, 153)
                    End Select
                End If
            Case Cols.Ico
                Dim oRow As DataGridViewRow = CType(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Info Then
                    e.Value = My.Resources.info_16
                End If
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOSurveyParticipant

        Property Nom As String
        Property Fch As Nullable(Of DateTime)
        Property Valoracio As Integer
        Property Info As Boolean

        Public Sub New(value As DTOSurveyParticipant)
            MyBase.New()
            _Source = value
            With value
                If .Fch <> Date.MinValue Then
                    _Fch = .Fch
                End If
                _Nom = DTOUser.NicknameOrElse(.User)
                _Valoracio = BLLSurveyParticipant.Valoracio(value)
                _Info = .Obs > ""
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

