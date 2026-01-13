Public Class Xl_TrpCosts

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOTrpCost)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Property IsDirty As Boolean

    Private Enum Cols
        Kg
        Cost
        Fixe
        Minim
        Suplidos
        Cod
    End Enum

    Public ReadOnly Property Values As List(Of DTOTrpCost)
        Get
            Return _Values
        End Get
    End Property

    Public Shadows Sub Load(values As List(Of DTOTrpCost))
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        Dim oControlItem As ControlItem = Nothing
        For Each oItem As DTOTrpCost In _Values
            oControlItem = New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOTrpCost
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOTrpCost = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        With MyBase.RowTemplate
            .Height = MyBase.Font.Height * 1.3
            '.DefaultCellStyle.BackColor = Color.Transparent (es transparenten els tabs de sota)
        End With

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.BackgroundColor = Color.White
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = True
        MyBase.RowHeadersWidth = 25
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False
        MyBase.AllowUserToAddRows = True
        MyBase.AllowUserToDeleteRows = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Kg)
            .DataPropertyName = "Kg"
            .HeaderText = "Kg"
            .Width = 60
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.0 Kg"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cost)
            .DataPropertyName = "Cost"
            .HeaderText = "Cost"
            .Width = 60
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.0000 €;-#,###0.0000 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fixe)
            .DataPropertyName = "Fixe"
            .HeaderText = "Fixe"
            .Width = 60
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.0000 €;-#,###0.0000 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Minim)
            .DataPropertyName = "Minim"
            .HeaderText = "Minim"
            .Width = 60
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.0000 €;-#,###0.0000 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Suplidos)
            .DataPropertyName = "Suplidos"
            .HeaderText = "Suplidos"
            .Width = 60
            .DefaultCellStyle.BackColor = Color.AliceBlue
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.0 Kg"
        End With
        MyBase.Columns.Add(New DataGridViewComboBoxColumn)
        With DirectCast(MyBase.Columns(Cols.Cod), DataGridViewComboBoxColumn)
            .HeaderText = "codi costos"
            .DataPropertyName = "Cod"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MaxDropDownItems = 2
            .DataSource = GetCodiCostos()
            .ValueMember = "key"
            .DisplayMember = "value"
        End With

        MyBase.CurrentCell = Me(0, 0)
        MyBase.BeginEdit(True)
    End Sub

    Private Function GetCodiCostos() As List(Of ListItem2)
        Dim retval As New List(Of ListItem2)
        Dim item1 As New ListItem2(DTOTrpCost.Codis.CostFinsAKg, "cost fins a Kgs")
        Dim item2 As New ListItem2(DTOTrpCost.Codis.CostPerKg, "cost per Kg")
        retval.Add(item1)
        retval.Add(item2)
        Return retval
    End Function



    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOTrpCost)
        Dim retval As New List(Of DTOTrpCost)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                retval.Add(oControlItem.Source)
            End If
        Next

        If retval.Count = 0 Then
            retval.Add(CurrentControlItem.Source)
        End If
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

        'oContextMenu.Items.Add("refresca", Nothing, AddressOf Refreshrequest)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub Do_DeleteLine()
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        Dim oControlitem As ControlItem = oRow.DataBoundItem
        _ControlItems.Remove(oControlitem)

    End Sub


    Private Shadows Sub Refreshrequest(sender As Object, e As System.EventArgs)
        MyBase.RefreshRequest(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_TrpCosts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        'ho crida DataGridView1_EditingControlShowing
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Cost, Cols.Fixe, Cols.Minim, Cols.Suplidos
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub

    Private Sub Xl_TrpCosts_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Me.EditingControlShowing
        'fa que funcioni KeyPress per DataGridViews
        If TypeOf e.Control Is TextBox Then
            Dim oControl As TextBox = DirectCast(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf Xl_TrpCosts_KeyPress
        End If
    End Sub

    Protected Class ControlItem
        Property Source As Object

        Property Kg As Integer
        Property Cost As Decimal
        Property Fixe As Decimal
        Property Minim As Decimal
        Property Suplidos As Decimal
        Property Cod As Integer


        Public Sub New(value As DTOTrpCost)
            MyBase.New()
            _Source = value
            With value
                _Kg = .HastaKg
                _Cost = .Eur
                _Fixe = .FixEur
                _Minim = .MinEur
                _Suplidos = .Suplidos
                _Cod = .Cod
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


