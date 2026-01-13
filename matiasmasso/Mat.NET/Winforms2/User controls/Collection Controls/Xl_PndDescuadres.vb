Public Class Xl_PndDescuadres
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPgcSaldo)
    Private _DefaultValue As DTOPgcSaldo
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Cli
        Cta
        Sdo
        Pnd
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPgcSaldo), Optional oDefaultValue As DTOPgcSaldo = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values
        _SelectionMode = oSelectionMode
        _DefaultValue = oDefaultValue

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOPgcSaldo) = FilteredValues()
        Dim oLang = Current.Session.Lang
        _ControlItems = New ControlItems
        For Each oItem As DTOPgcSaldo In oFilteredValues
            Dim oControlItem As New ControlItem(oItem, oLang)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Cli)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPgcSaldo)
        Dim retval As List(Of DTOPgcSaldo)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Contact.Nom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOPgcSaldo
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPgcSaldo = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowCcd.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Cli)
            .HeaderText = "Nom"
            .DataPropertyName = "Cli"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cta)
            .HeaderText = "Compte"
            .DataPropertyName = "Cta"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sdo)
            .HeaderText = "Saldo"
            .DataPropertyName = "Sdo"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pnd)
            .HeaderText = "Pendent"
            .DataPropertyName = "Pnd"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As List(Of DTOPgcSaldo)
        Dim retval As New List(Of DTOPgcSaldo)
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

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oContactMenu = Await FEB.ContactMenu.Find(exs, SelectedItems.First.Contact)
            Dim oMenu_Contact As New Menu_Contact(SelectedItems.First.Contact, oContactMenu)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
            'oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Function


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPgcSaldo = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    'Dim oFrm As New Frm_Ctas(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            Await SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOPgcSaldo

        Property Cli As String
        Property Cta As String
        Property Sdo As Decimal
        Property Pnd As Decimal

        Public Sub New(value As DTOPgcSaldo, oLang As DTOLang)
            MyBase.New()
            _Source = value
            With value
                If .Contact IsNot Nothing Then
                    _Cli = .Contact.FullNom
                End If
                If .Epg.Nom IsNot Nothing Then
                    _Cta = .Epg.Nom.Tradueix(oLang)
                End If
                If .SdoCreditor IsNot Nothing Then
                    _Sdo = .SdoCreditor.Eur
                End If
                If .Pendent IsNot Nothing Then
                    _Pnd = .Pendent.Eur
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

