Public Class Xl_EdiversaFiles

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOEdiversaFile)
    Private _DefaultValue As DTOEdiversaFile
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _IOCod As DTOEdiversaFile.IOcods

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        filename
        rebut
        data
        amt
        remite
        docnum
    End Enum


    Public Shadows Sub Load(values As List(Of DTOEdiversaFile), Optional oDefaultValue As DTOEdiversaFile = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values
        _SelectionMode = oSelectionMode
        If _Values.Count > 0 Then _IOCod = _Values.First.IOCod

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If


        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOEdiversaFile) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOEdiversaFile In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Function FilteredValues() As List(Of DTOEdiversaFile)
        Dim retval As List(Of DTOEdiversaFile)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Docnum.ToLower.Contains(_Filter.ToLower) Or x.FileName.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOEdiversaFile
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOEdiversaFile = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowEdiversaFile.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.filename)
            .HeaderText = "Fitxer"
            .DataPropertyName = "filename"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.rebut)
            .HeaderText = "Rebut"
            .DataPropertyName = "Rebut"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.data)
            .HeaderText = "Data"
            .DataPropertyName = "Data"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.remite)
            Select Case _IOCod
                Case DTOEdiversaFile.IOcods.Inbox
                    .HeaderText = "Remitent"
                Case DTOEdiversaFile.IOcods.Outbox
                    .HeaderText = "Destinatari"
            End Select
            .DataPropertyName = "remite"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.docnum)
            .HeaderText = "Document"
            .DataPropertyName = "docnum"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
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

    Private Function SelectedItems() As List(Of DTOEdiversaFile)
        Dim retval As New List(Of DTOEdiversaFile)
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
            Dim oMenu_EdiversaFile As New Menu_EdiversaFile(SelectedItems)
            AddHandler oMenu_EdiversaFile.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu_EdiversaFile.RequestToToggleProgressBar, AddressOf ToggleProgressBarRequest
            oContextMenu.Items.AddRange(oMenu_EdiversaFile.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add("Afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim items As List(Of DTOEdiversaFile) = FilteredValues()
        If SelectedItems.Count > 1 Then
            items = SelectedItems()
        End If
        Dim oSheet = DTOEdiversaFile.Excel(items)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOEdiversaFile = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_EdiversaFile(oSelectedValue)
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


    Private Sub Xl_EdiversaFiles_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim oFile As DTOEdiversaFile = oControlItem.Source

        If oFile.Result = DTOEdiversaFile.Results.Pending Then
            oRow.DefaultCellStyle.BackColor = Color.White
        Else
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOEdiversaFile

        Property filename As String
        Property rebut As DateTime
        Property data As Date
        Property amt As Decimal
        Property remite As String
        Property docnum As String


        Public Sub New(value As DTOEdiversaFile)
            MyBase.New()
            _Source = value
            With value
                _filename = .FileName
                _rebut = .FchCreated
                _data = .Fch

                If .Amount IsNot Nothing Then
                    _amt = .Amount.Eur
                End If

                Select Case value.IOCod
                    Case DTOEdiversaFile.IOcods.Inbox
                        If .Sender IsNot Nothing Then
                            If .Sender.Contact Is Nothing Then
                                _remite = .Sender.Nom
                            Else
                                _remite = .Sender.Contact.Nom
                            End If
                        End If
                    Case DTOEdiversaFile.IOcods.Outbox
                        If .Receiver IsNot Nothing Then
                            If .Receiver.Contact IsNot Nothing Then
                                _remite = .Receiver.Contact.Nom
                            End If
                        End If
                End Select

                _docnum = .Docnum
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


