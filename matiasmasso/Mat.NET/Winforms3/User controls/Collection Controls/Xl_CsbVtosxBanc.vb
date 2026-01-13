Public Class Xl_CsbVtosxBanc
    Inherits DataGridView

    Private _Values As List(Of DTOCsb)
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _MenuItemImpagats As ToolStripMenuItem
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ico
        Amt
        Vto
        Txt
        Nom
    End Enum

    Public Shadows Sub Load(oCsbs As List(Of DTOCsb))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _MenuItemImpagats = New ToolStripMenuItem("mostrar nomes els impagats", Nothing, AddressOf Refresca)
        _MenuItemImpagats.CheckOnClick = True

        _Values = oCsbs
        Refresca()
    End Sub

    Private Sub Refresca()

        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOCsb) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOCsb In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            If Not (_MenuItemImpagats.Checked And Not oControlItem.Source.Result = DTOCsb.Results.Impagat) Then
                _ControlItems.Add(oControlItem)
            End If
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Function FilteredValues() As List(Of DTOCsb)
        Dim retval As List(Of DTOCsb)
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
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.Ico), DataGridViewImageColumn)
            .DataPropertyName = "Ico"
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Amt"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Vto)
            .HeaderText = "Venciment"
            .DataPropertyName = "Vto"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Lliurat"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

    End Sub

    Private Function SelectedItems() As List(Of DTOCsb)
        Dim retval As New List(Of DTOCsb)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
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
            Dim oMenu_Template As New Menu_Csb2(SelectedItems)
            AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Template.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add(_MenuItemImpagats)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Xl_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlitem As ControlItem = oRow.DataBoundItem
                Select Case oControlitem.Result
                    Case DTOCsb.Results.Impagat
                        e.Value = My.Resources.aspa
                    Case DTOCsb.Results.Vençut
                        e.Value = My.Resources.vb
                    Case DTOCsb.Results.Pendent
                        e.Value = My.Resources.empty
                    Case DTOCsb.Results.Reclamat
                        e.Value = My.Resources.del
                End Select
        End Select

    End Sub

    Private Sub DataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
        End If
    End Sub

    Private Sub Xl_CsbVtos_CellToolTipTextNeeded(sender As Object, e As DataGridViewCellToolTipTextNeededEventArgs) Handles Me.CellToolTipTextNeeded
        If e.RowIndex >= 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            Dim oCsbs As DTOCsb = oControlitem.Source
            'e.ToolTipText = sb.ToString
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOCsb
        Property Amt As Decimal
        Property Vto As Date
        Property Txt As String
        Property Nom As String
        Property Result As DTOCsb.Results

        Public Sub New(value As DTOCsb)
            MyBase.New()
            _Source = value
            If value IsNot Nothing Then
                With value
                    _Amt = .Amt.Eur
                    _Vto = .Vto
                    _Txt = .Txt
                    _Nom = .Contact.Nom
                    _Result = .Result
                End With
            End If
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


