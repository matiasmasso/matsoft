Public Class Xl_SalesReportItems
    Inherits _Xl_ReadOnlyDatagridview

    Private _SalesReport As DTOSalesReport
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        txt
        tot
        M01
    End Enum


    Public Shadows Sub Load(oSalesReport As DTOSalesReport)
        _SalesReport = oSalesReport

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Select Case _SalesReport.SelectedFormat
            Case DTOSalesReport.Formats.Qty
                For i = 0 To 12
                    MyBase.Columns(Cols.tot + i).DefaultCellStyle.Format = "#,###"
                Next
            Case DTOSalesReport.Formats.Eur
                For i = 0 To 12
                    MyBase.Columns(Cols.tot + i).DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                Next
        End Select

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        Dim oRows = _SalesReport.Rows()
        Dim oTotal = DTOSalesReport.Total(oRows)
        Dim oControlItem As New ControlItem(oTotal)
        _ControlItems.Add(oControlItem)
        For Each oItem As DTOSalesReport.Row In oRows
            oControlItem = New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOSalesReport.Row
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOSalesReport.Row = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = 120
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.tot)
            .HeaderText = "Total"
            .DataPropertyName = "Tot"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        For i = 1 To 12
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.tot + i)
                .HeaderText = Current.Session.Lang.MesAbr(i)
                .DataPropertyName = String.Format("M{0:00}", i)
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00;-#,###0.00;#"
            End With
        Next

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

    Private Function SelectedItems() As List(Of DTOSalesReport.Row)
        Dim retval As New List(Of DTOSalesReport.Row)
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
            Select Case MyBase.CurrentCell.ColumnIndex
                Case Cols.txt
                    Select Case _SalesReport.SelectedConcept
                        Case DTOSalesReport.Concepts.Centros
                            oContextMenu.Items.Add("desglosar per producte", Nothing, AddressOf SplitPerProduct)
                    End Select
            End Select
            'Dim oMenu_Template As New Menu_Template(SelectedItems.First)
            'AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Template.Range)
            'oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SplitPerProduct()
        Dim oSalesReport = _SalesReport.Clon
        With oSalesReport
            .SelectedConcept = DTOSalesReport.Concepts.Categories
            .SelectedCentro = CurrentControlItem.Source.Tag
        End With
        Dim oFrm As New Frm_SalesReport(oSalesReport)
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue = CurrentControlItem.Source
            'Dim oFrm As New Frm_Template(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
            'RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1__DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
        MyBase.Rows(0).Frozen = True
    End Sub

    Protected Class ControlItem
        Property Source As DTOSalesReport.Row

        Property Txt As String
        Property Tot As Decimal
        Property M01 As Decimal
        Property M02 As Decimal
        Property M03 As Decimal
        Property M04 As Decimal
        Property M05 As Decimal
        Property M06 As Decimal
        Property M07 As Decimal
        Property M08 As Decimal
        Property M09 As Decimal
        Property M10 As Decimal
        Property M11 As Decimal
        Property M12 As Decimal

        Public Sub New(value As DTOSalesReport.Row)
            MyBase.New()
            _Source = value
            With value
                _Txt = .Txt
                _Tot = .Months.Sum(Function(x) x)
                _M01 = .Months(0)
                _M02 = .Months(1)
                _M03 = .Months(2)
                _M04 = .Months(3)
                _M05 = .Months(4)
                _M06 = .Months(5)
                _M07 = .Months(6)
                _M08 = .Months(7)
                _M09 = .Months(8)
                _M10 = .Months(9)
                _M11 = .Months(10)
                _M12 = .Months(11)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


