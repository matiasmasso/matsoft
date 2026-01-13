Public Class Xl_AreaContacts

    Inherits DataGridView

    Private _Values As List(Of DTOContact)
    Private _DefaultValue As DTOVisaEmisor
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Adr
        ZipyCit
    End Enum

    Public Shadows Sub Load(values As List(Of DTOContact), Optional oDefaultValue As DTOContact = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOContact) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOContact In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOContact)
        Dim retval As List(Of DTOContact)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOContact
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOContact = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = 50
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Adr)
            .HeaderText = "Adreça"
            .DataPropertyName = "Adr"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.ZipyCit)
            .HeaderText = "Localitat"
            .DataPropertyName = "ZipyCit"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
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

    Private Function SelectedItems() As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
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
            Dim oMenu_Contact As New Menu_Contact(SelectedItems.First)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oReZipMenuItem As New ToolStripMenuItem("canviar de codi postal", Nothing, AddressOf Do_reZip)
        oReZipMenuItem.Enabled = _Values.All(Function(x) x.address.Zip.Equals(_Values.First.address.Zip))
        oContextMenu.Items.Add(oReZipMenuItem)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOContact = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Contact(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub Do_reZip()
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZip)
        AddHandler oFrm.onItemSelected, AddressOf onReZip
        oFrm.Show()
    End Sub

    Private Async Sub onReZip(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oZipTo As DTOZip = e.Argument
        Dim iCount As Integer = Await FEB2.Contacts.reZip(exs, oZipTo, SelectedItems)
        If exs.Count = 0 Then
            MsgBox(String.Format("reassignats {0} contactes a {1}", iCount, oZipTo.FullNom(Current.Session.Lang)))
            RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOContact

        Property Nom As String
        Property Adr As String
        Property ZipyCit As String

        Public Sub New(value As DTOContact)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
                If .NomComercial > "" Then
                    _Nom = _Nom & " '" & .NomComercial & "'"
                End If
                _Adr = .Address.Text
                _ZipyCit = DTOAddress.ZipyCit(.Address)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


