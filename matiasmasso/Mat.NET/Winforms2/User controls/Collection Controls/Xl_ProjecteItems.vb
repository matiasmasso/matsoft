Public Class Xl_ProjecteItems

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOCcb)
    Private _DefaultValue As DTOCcb
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Eurx
        Nom
        Amt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCcb), Optional oDefaultValue As DTOCcb = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOCcb) = FilteredValues()
        _ControlItems = New ControlItems

        Dim oContact As New DTOContact
        Dim oContactControlItem As New ControlItem(ControlItem.LinCods.Resum, Nothing, "", 0)
        For Each oValue As DTOCcb In oFilteredValues
            If oValue.Contact.UnEquals(oContact) Then
                oContact = oValue.Contact
                oContactControlItem = New ControlItem(ControlItem.LinCods.Resum, oContact, oContact.FullNom, 0)
                _ControlItems.Add(oContactControlItem)
            End If
            Dim sNom As String = String.Format("{0:dd/MM/yy} - {1}", oValue.Cca.Fch, oValue.Cca.Concept)
            Dim oControlItem As New ControlItem(ControlItem.LinCods.Detall, oValue, sNom, IIf(oValue.Dh = DTOCcb.DhEnum.Debe, oValue.Amt.Eur, -oValue.Amt.Eur))
            oContactControlItem.Eurx += oControlItem.Eur
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems

        For i As Integer = 0 To MyBase.Rows.Count - 1
            'MyBase.Rows(i).Visible = _ControlItems(i).LinCod = ControlItem.LinCods.Resum
        Next

        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Contact.FullNom.ToLower.Contains(_Filter.ToLower))
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


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowCcb.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Eurx)
            .HeaderText = "Total"
            .DataPropertyName = "Eurx"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Parcial"
            .DataPropertyName = "Eur"
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

    Private Function SelectedItems() As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        'For Each oRow As DataGridViewRow In MyBase.SelectedRows
        ' Dim oControlItem As ControlItem = oRow.DataBoundItem
        ' retval.Add(oControlItem.Source)
        ' Next
        '
        '        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
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
            'Dim oMenu_Ccb As New Menu_Ccb(SelectedItems.First)
            'AddHandler oMenu_Ccb.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Ccb.Range)
            'oContextMenu.Items.Add("-")
        End If
        'oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()

        'If oCurrentControlItem IsNot Nothing Then
        ' Dim oSelectedValue As DTOCcb = CurrentControlItem.Source
        ' Select Case _SelectionMode
        ' Case DTO.Defaults.SelectionModes.Browse
        ' Dim oFrm As New Frm_Ccb(oSelectedValue)
        ' AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        ' oFrm.Show()
        ' Case DTO.Defaults.SelectionModes.Selection
        ' RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
        ' End Select

        'End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_ProjecteItems_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.Resum
            Case ControlItem.LinCods.Detall
                e.CellStyle.Padding = New System.Windows.Forms.Padding(20, 0, 0, 0)
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOBaseGuid

        Property Eurx As Decimal
        Property Nom As String
        Property Eur As Decimal
        Property LinCod As LinCods

        Public Enum LinCods
            Resum
            Detall
        End Enum

        Public Sub New(oLinCod As LinCods, value As DTOBaseGuid, sNom As String, DcEur As Decimal)
            MyBase.New()
            _Source = value
            With value
                _LinCod = oLinCod
                _Nom = sNom
                If oLinCod = LinCods.Resum Then
                    _Eurx = DcEur
                Else
                    _Eur = DcEur
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


