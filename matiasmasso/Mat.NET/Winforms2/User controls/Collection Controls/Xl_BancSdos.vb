Public Class Xl_BancSdos

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOBancSdo)
    Private _ControlItems As ControlItems
    Private _ReloadPending As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Banc
        Fch
        Sdo
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBancSdo))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOBancSdo)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As DTOBancSdo In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOBancSdo
        Get
            Dim retval As DTOBancSdo = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowBancSdo.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = False
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Banc)
            .HeaderText = "Entitat"
            .DataPropertyName = "Banc"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm"
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sdo)
            .HeaderText = "Saldo"
            .DataPropertyName = "Sdo"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_Banc As New Menu_Banc(Value.Banc)
            AddHandler oMenu_Banc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Banc.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub Do_Excel()
        Dim oSheet As MatHelper.Excel.Sheet = DTOBancSdo.Excel(_Values)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBanc = CurrentControlItem.Source.Banc
            Dim oFrm As New Frm_Contact(oSelectedValue)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            If _ReloadPending Then
                _ReloadPending = False
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                SetContextMenu()
            End If
        End If
    End Sub


    Private Sub Xl_BancSdos_RowValidated(sender As Object, e As DataGridViewCellEventArgs) Handles Me.RowValidated

    End Sub

    Private Async Sub Xl_BancSdos_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValueChanged
        If _AllowEvents Then

            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                Dim oPreviousBancSdo As DTOBancSdo = oControlItem.Source
                Dim DcEur As Decimal = oRow.Cells(Cols.Sdo).Value
                Dim BlUpdate As Boolean = oPreviousBancSdo Is Nothing And DcEur <> 0
                If oPreviousBancSdo.Saldo Is Nothing Then
                    BlUpdate = (DcEur <> 0)
                Else
                    BlUpdate = (DcEur <> oPreviousBancSdo.Saldo.Eur)
                End If

                If BlUpdate Then
                    Dim oBancSdo As New DTOBancSdo
                    With oBancSdo
                        .Banc = oPreviousBancSdo.Banc
                        .Fch = DTO.GlobalVariables.Now()
                        .Saldo = DTOAmt.Factory(DcEur)
                    End With

                    Dim exs As New List(Of Exception)
                    If Await FEB.BancSdo.Update(oBancSdo, exs) Then
                        _ReloadPending = True
                    Else
                        oControlItem = New ControlItem(oPreviousBancSdo)
                        UIHelper.WarnError(exs)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOBancSdo

        Property Banc As String
        Property Fch As Date
        Property Sdo As Decimal

        Public Sub New(value As DTOBancSdo)
            MyBase.New()
            _Source = value
            With value
                _Banc = .Banc.Abr
                _Fch = .Fch
                If .Saldo IsNot Nothing Then
                    _Sdo = .Saldo.Eur
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


