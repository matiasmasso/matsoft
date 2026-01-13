Public Class Xl_ProductPackItems

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductPackItem)
    Private _ControlItems As ControlItems
    Private _IsDirty As Boolean
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event afterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Qty
        Retail
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductPackItem))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOProductPackItem)
        Get
            Dim retval As New List(Of DTOProductPackItem)
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Retail As DTOAmt
        Get
            Dim retval = DTOAmt.Empty
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Source IsNot Nothing Then
                    Dim oAmt As DTOAmt = DTOAmt.Import(oControlItem.Qty, oControlItem.Source.RRPP, 0)
                    retval.Add(oAmt)
                End If
            Next
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False

        Dim oFilteredValues As List(Of DTOProductPackItem) = _Values

        _ControlItems = New ControlItems
        For Each oItem As DTOProductPackItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOProductPackItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductPackItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        'MyBase.DataSource = _ControlItems
        'MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False
        MyBase.AllowUserToAddRows = True
        MyBase.AllowUserToDeleteRows = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Unitats"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Retail)
            .HeaderText = "Pvp"
            .DataPropertyName = "Retail"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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

    Private Function SelectedItems() As List(Of DTOProductPackItem)
        Dim retval As New List(Of DTOProductPackItem)
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
            Dim oProductPackItem As DTOProductPackItem = oControlItem.Source
            If oProductPackItem IsNot Nothing Then
                Dim oMenu_Art As New Menu_ProductSku(oProductPackItem)
                AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Art.Range)
                'oContextMenu.Items.Add("-")
            End If
        End If
        'oContextMenu.Items.Add("refresca", Nothing, AddressOf RefreshRequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductPackItem = CurrentControlItem.Source
            'Dim oFrm As New Frm_ProductPackItem(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

#Region "Edition"
    'Obligatori Controlitem constructor sense parametres

    Private _LastSkuEntered As DTOProductSku

    Private Sub Xl_ImportPrevisio_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValidated
        Select Case e.ColumnIndex
            Case Cols.Qty
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.Source Is Nothing Then oControlItem.Source = New DTOProductPackItem
                Dim value As DTOProductPackItem = oControlItem.Source
                value.Qty = oControlItem.Qty

            Case Cols.Nom
                If _LastSkuEntered IsNot Nothing Then
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    If oControlItem IsNot Nothing Then
                        With oControlItem
                            .Nom = _LastSkuEntered.NomLlarg
                            If _LastSkuEntered.RRPP IsNot Nothing Then
                                .Retail = _LastSkuEntered.RRPP.Eur
                            End If
                        End With
                        If oControlItem.Source Is Nothing Then oControlItem.Source = New DTOProductPackItem
                        oControlItem.Source = New DTOProductPackItem(_LastSkuEntered.Guid)
                        BLLProductSku.Load(oControlItem.Source)
                    End If
                End If
        End Select
        _IsDirty = True
        RaiseEvent afterUpdate(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_ImportPrevisio_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles Me.CellValidating
        Dim oRow As DataGridViewRow = Me.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim procesa As Boolean = oControlItem Is Nothing And e.FormattedValue > ""
                If oControlItem IsNot Nothing Then
                    procesa = oControlItem.Nom <> e.FormattedValue
                End If
                If procesa Then
                    _LastSkuEntered = Finder.FindSku(Current.Session.Emp, e.FormattedValue, Current.Session.Emp.Mgz)
                    If _LastSkuEntered Is Nothing Then
                        e.Cancel = True
                    End If
                End If
        End Select
    End Sub

#End Region



    Protected Class ControlItem
        Property Source As DTOProductPackItem

        Property Nom As String
        Property Qty As Integer
        Property Retail As Decimal

        Public Sub New(value As DTOProductPackItem)
            MyBase.New()
            _Source = value
            With value
                _Nom = .NomLlarg
                _Qty = .Qty
                If .RRPP IsNot Nothing Then
                    _Retail = .RRPP.Eur
                End If
            End With
        End Sub

        Public Sub New() 'obligatori per editable grid
            MyBase.New
        End Sub
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


