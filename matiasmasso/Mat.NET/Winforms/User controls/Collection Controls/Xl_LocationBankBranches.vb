Public Class Xl_LocationBankBranches

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOBankBranch)
    Private _DefaultValue As DTOBankBranch
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Bank
        Branch
        Adr
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBankBranch), Optional oDefaultValue As DTOBankBranch = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOBankBranch) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOBankBranch In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOBankBranch)
        Dim retval As List(Of DTOBankBranch)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Address.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOBankBranch
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBankBranch = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowBankBranch.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Bank)
            .HeaderText = "Entitat"
            .DataPropertyName = "Bank"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Branch)
            .HeaderText = "Agencia"
            .DataPropertyName = "Branch"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Adr)
            .HeaderText = "Adreça"
            .DataPropertyName = "Adr"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOBankBranch)
        Dim retval As New List(Of DTOBankBranch)
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
            Dim oMenu_BankBranch As New Menu_BankBranch(SelectedItems.First)
            AddHandler oMenu_BankBranch.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_BankBranch.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oReLocateMenuItem As New ToolStripMenuItem("canviar de població", Nothing, AddressOf Do_reLocate)
        oReLocateMenuItem.Enabled = _Values.All(Function(x) x.location.Equals(_Values.First.location))
        oContextMenu.Items.Add(oReLocateMenuItem)

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_reLocate()
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectLocation)
        AddHandler oFrm.onItemSelected, AddressOf onReLocate
        oFrm.Show()
    End Sub

    Private Async Sub onReLocate(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oLocateTo As DTOLocation = e.Argument
        Dim iCount As Integer = Await FEB2.BankBranches.reLocate(exs, oLocateTo, SelectedItems)
        If exs.Count = 0 Then
            MsgBox(String.Format("reassignades {0} oficines bancàries a {1}", iCount, oLocateTo.FullNom(Current.Session.Lang)))
            MyBase.RefreshRequest(Me, e)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBankBranch = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_BankBranch(oSelectedValue)
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



    Protected Class ControlItem
        Property Source As DTOBankBranch

        Property Bank As String
        Property Branch As String
        Property Adr As String

        Public Sub New(value As DTOBankBranch)
            MyBase.New()
            _Source = value
            With value
                _Bank = DTOBank.NomComercialORaoSocial(.Bank)
                _Branch = .Id
                _Adr = .Address
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


