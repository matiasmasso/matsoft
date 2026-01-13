Public Class Xl_Model349
    Inherits DataGridView

    Private _Values As List(Of DTOModel349)
    Private _DefaultValue As DTOModel349
    Private _SelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nif
        Nom
        Tot
        M1
        M2
        M3
        M4
        M5
        M6
        M7
        M8
        M9
        M10
        M11
        M12
    End Enum

    Public Shadows Sub Load(values As List(Of DTOModel349), Optional oDefaultValue As DTOModel349 = Nothing, Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
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
        _ControlItems = New ControlItems
        Dim oTotals As New ControlItem(Nothing)
        Dim oControlItem As New ControlItem(New DTOContact)
        For Each oItem As DTOModel349 In _Values
            If Not oItem.Contact.Equals(oControlItem.Source) Then
                oControlItem = New ControlItem(oItem.Contact)
                _ControlItems.Add(oControlItem)
            End If
            Select Case oItem.Month
                Case 1
                    oControlItem.M1 += oItem.Amt.Eur
                    oTotals.M1 += oItem.Amt.Eur
                Case 2
                    oControlItem.M2 += oItem.Amt.Eur
                    oTotals.M2 += oItem.Amt.Eur
                Case 3
                    oControlItem.M3 += oItem.Amt.Eur
                    oTotals.M3 += oItem.Amt.Eur
                Case 4
                    oControlItem.M4 += oItem.Amt.Eur
                    oTotals.M4 += oItem.Amt.Eur
                Case 5
                    oControlItem.M5 += oItem.Amt.Eur
                    oTotals.M5 += oItem.Amt.Eur
                Case 6
                    oControlItem.M6 += oItem.Amt.Eur
                    oTotals.M6 += oItem.Amt.Eur
                Case 7
                    oControlItem.M7 += oItem.Amt.Eur
                    oTotals.M7 += oItem.Amt.Eur
                Case 8
                    oControlItem.M8 += oItem.Amt.Eur
                    oTotals.M8 += oItem.Amt.Eur
                Case 9
                    oControlItem.M9 += oItem.Amt.Eur
                    oTotals.M9 += oItem.Amt.Eur
                Case 10
                    oControlItem.M10 += oItem.Amt.Eur
                    oTotals.M10 += oItem.Amt.Eur
                Case 11
                    oControlItem.M11 += oItem.Amt.Eur
                    oTotals.M11 += oItem.Amt.Eur
                Case 12
                    oControlItem.M12 += oItem.Amt.Eur
                    oTotals.M12 += oItem.Amt.Eur
            End Select
        Next

        _ControlItems.Add(oTotals)


        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        SetContextMenu()
        _AllowEvents = True
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
        With MyBase.Columns(Cols.Nif)
            .HeaderText = "Nif"
            .DataPropertyName = "Nif"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = 80
        End With



        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Tot)
            .HeaderText = "Totals"
            .DataPropertyName = "Tot"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M1)
            .HeaderText = "Gen"
            .DataPropertyName = "M1"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M2)
            .HeaderText = "Feb"
            .DataPropertyName = "M2"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M3)
            .HeaderText = "Mar"
            .DataPropertyName = "M3"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M4)
            .HeaderText = "Abr"
            .DataPropertyName = "M4"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M5)
            .HeaderText = "Mai"
            .DataPropertyName = "M5"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M6)
            .HeaderText = "Jun"
            .DataPropertyName = "M6"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M7)
            .HeaderText = "Jul"
            .DataPropertyName = "M7"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M8)
            .HeaderText = "Ago"
            .DataPropertyName = "M8"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M9)
            .HeaderText = "Sep"
            .DataPropertyName = "M9"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M10)
            .HeaderText = "Oct"
            .DataPropertyName = "M10"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M11)
            .HeaderText = "Nov"
            .DataPropertyName = "M11"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.M12)
            .HeaderText = "Des"
            .DataPropertyName = "M12"
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
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            'Dim oSelectedValue As DTOModel349 = CurrentControlItem.Source
            'Select Case _SelectionMode
            ' Case BLL.Defaults.SelectionModes.Browse
            ' Dim oFrm As New Frm_Model349(oSelectedValue)
            ' AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            ' oFrm.Show()
            ' Case BLL.Defaults.SelectionModes.Selection
            ' RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            ' End Select

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
        Property Nif As String
        Property M1 As Decimal
        Property M2 As Decimal
        Property M3 As Decimal
        Property M4 As Decimal
        Property M5 As Decimal
        Property M6 As Decimal
        Property M7 As Decimal
        Property M8 As Decimal
        Property M9 As Decimal
        Property M10 As Decimal
        Property M11 As Decimal
        Property M12 As Decimal

        Public Sub New(value As DTOContact)
            MyBase.New()
            If value IsNot Nothing Then
                _Source = value
                With value
                    _Nom = .Nom
                    _Nif = .Nif
                End With
            End If
        End Sub

        ReadOnly Property Tot As Decimal
            Get
                Return M1 + M2 + M3 + M4 + M5 + M6 + M7 + M8 + M9 + M10 + M11 + M12
            End Get
        End Property
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

