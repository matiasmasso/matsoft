Public Class Xl_Transmisio_SelectAlbs
    Inherits DataGridView

    Private _Values As List(Of DTODelivery)
    Private _DefaultValue As DTODelivery
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event ItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs)

    Private Enum Cols
        Check
        Num
        Fch
        IcoEtq
        Eur
        Nom
        Trp
    End Enum

    Public Shadows Sub Load(values As List(Of DTODelivery), Optional oDefaultValue As DTODelivery = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTODelivery) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTODelivery In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        DisplayValues()
    End Sub

    Private Sub DisplayValues()
        _AllowEvents = False

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        If _DefaultValue Is Nothing Then
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        Else
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTODelivery
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTODelivery = oControlItem.Source
            Return retval
        End Get
    End Property

    Public Sub SelectAllItems()
        For Each oControlItem As ControlItem In _ControlItems
            oControlItem.Check = True
        Next
        MyBase.Refresh()
        DisplayValues()
    End Sub

    Public Sub SelectNoItems()
        For Each oControlItem As ControlItem In _ControlItems
            oControlItem.Check = False
        Next
        MyBase.Refresh()
        DisplayValues()
    End Sub

    Public Sub DeSelectRest()
        Dim oControlItem As ControlItem = Nothing
        For Each oRow As DataGridViewRow In MyBase.Rows
            oControlItem = oRow.DataBoundItem
            Dim BlCheckRow As Boolean = False
            For Each oCell As DataGridViewCell In oRow.Cells
                If oCell.Selected Then
                    BlCheckRow = True
                    Exit For
                End If
            Next
            oControlItem.Check = BlCheckRow
        Next
        MyBase.Refresh()

        'fake args
        Dim oCurrentCheckState As CheckState = oControlItem.Check
        Dim oNewCheckState As CheckState = IIf(oControlItem.Check = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
        Dim oArgs As New ItemCheckEventArgs(_ControlItems.IndexOf(oControlItem), oNewCheckState, oCurrentCheckState)
        RaiseEvent ItemCheck(Me, oArgs)
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Check), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Num)
            .HeaderText = "Numero"
            .DataPropertyName = "Num"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.IcoEtq), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 70
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
        With MyBase.Columns(Cols.Trp)
            .HeaderText = "Transport"
            .DataPropertyName = "Trp"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 70
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

    Public Function CheckedValues() As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        For Each oControlItem As ControlItem In _ControlItems
            If oControlItem.Check Then
                retval.Add(oControlItem.Source)
            End If
        Next
        Return retval
    End Function

    Private Function SelectedValues() As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        For Each oControlItem As ControlItem In SelectedControlItems()
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
            Dim oMenu_Delivery As New Menu_Delivery(SelectedValues)
            AddHandler oMenu_Delivery.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Delivery.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("deseleccionar la resta", Nothing, AddressOf DeSelectRest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub



    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

#Region "Check"
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles MyBase.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem

                Dim oCurrentCheckState As CheckState = oControlItem.Check
                Dim oNewCheckState As CheckState = IIf(oControlItem.Check = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
                Dim oArgs As New ItemCheckEventArgs(e.RowIndex, oNewCheckState, oCurrentCheckState)
                oControlItem.Check = Not oControlItem.Check
                RaiseEvent ItemCheck(Me, oArgs)
        End Select
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Select Case oControlItem.Check
                    Case CheckState.Checked
                        e.Value = My.Resources.Checked13
                    Case CheckState.Unchecked
                        e.Value = My.Resources.UnChecked13
                    Case CheckState.Indeterminate
                        e.Value = My.Resources.CheckedGrayed13
                End Select
            Case Cols.IcoEtq
                Dim oControlItem As ControlItem = MyBase.Rows(e.RowIndex).DataBoundItem
                Dim oDelivery As DTODelivery = oControlItem.Source
                If oDelivery.EtiquetesTransport IsNot Nothing Then
                    e.Value = My.Resources.label16
                End If
        End Select
    End Sub

    Private Sub Datagridview1_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Select Case e.ColumnIndex
                Case Cols.IcoEtq
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oDelivery As DTODelivery = oControlItem.Source
                    If oDelivery.EtiquetesTransport IsNot Nothing Then
                        e.ToolTipText = "albarà amb etiquetes de transport personalitzades"
                    End If
            End Select
        End If
    End Sub

#End Region



    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTODelivery

        Property Check As Boolean
        Property Num As Integer
        Property Fch As Date
        Property Eur As Decimal
        Property Nom As String
        Property Trp As String



        Public Sub New(value As DTODelivery)
            MyBase.New()
            _Source = value


            With value
                _Check = True
                _Num = .Id
                _Fch = .Fch
                _Eur = .Import.Eur
                'Dim sNom As String = .Nom
                'If .Address IsNot Nothing Then
                ' If .Address.Zip IsNot Nothing Then
                ' If .Address.Zip.Location IsNot Nothing Then
                ' sNom = sNom & " (" & .Address.Zip.Location.Nom & ")"
                ' End If
                ' End If
                ' End If
                _Nom = .Customer.FullNom
                _Trp = DTODelivery.DeliveryTerms(value)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class