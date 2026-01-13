Public Class Xl_NominasNew
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterItemCheckedChange(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Check
        Ico
        Nom
        Devengat
        Dietas
        SegSoc
        Irpf
        Deutes
        Liquid
    End Enum

    Public Shadows Sub Load(value As List(Of DTONomina))
        _ControlItems = New ControlItems
        For Each oItem As DTONomina In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public WriteOnly Property Fch As Date
        Set(value As Date)
            For Each oItem As ControlItem In _ControlItems
                Dim oNomina As DTONomina = oItem.Source
                oNomina.Cca.Fch = value
            Next
        End Set
    End Property

    Public ReadOnly Property SelectedValues As List(Of DTONomina)
        Get
            Dim retval As New List(Of DTONomina)
            For Each oItem As ControlItem In _ControlItems
                If oItem.Checked Then
                    retval.Add(oItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Check), DataGridViewImageColumn)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Ico), DataGridViewImageColumn)
                .DataPropertyName = "Ico"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 16
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Nom)
                .DataPropertyName = "Nom"
                .HeaderText = "treballador"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Devengat)
                .DataPropertyName = "Devengat"
                .HeaderText = "devengat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Dietas)
                .DataPropertyName = "Dietas"
                .HeaderText = "dietes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.SegSoc)
                .DataPropertyName = "SegSoc"
                .HeaderText = "Seg.Social"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Irpf)
                .DataPropertyName = "Irpf"
                .HeaderText = "Irpf"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Deutes)
                .DataPropertyName = "Deutes"
                .HeaderText = "Deutes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Liquid)
                .DataPropertyName = "Liquid"
                .HeaderText = "liquid"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTONomina)
        Dim retval As New List(Of DTONomina)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oNomina As DTONomina = oControlItem.Source
            Dim oContactMenu = Await FEB.ContactMenu.Find(exs, oNomina.Staff)
            Dim oMenuContact As New Menu_Contact(oNomina.Staff, oContactMenu)
            oContextMenu.Items.AddRange(oMenuContact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Function

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oControlItem As ControlItem = DataGridView1.Rows(e.RowIndex).DataBoundItem
                If oControlItem.Checked = True Then
                    e.Value = My.Resources.Checked13
                Else
                    e.Value = My.Resources.UnChecked13
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Check
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                oControlItem.Checked = Not oControlItem.Checked
                DataGridView1.Refresh()
                RaiseEvent AfterItemCheckedChange(Me, New MatEventArgs(oControlItem.Source))
        End Select
    End Sub

    Private Async Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Await SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As DTONomina

        Public Property Checked As Boolean
        Public Property Ico As Image = Nothing
        Public Property Nom As String
        Public Property Devengat As Decimal
        Public Property Dietas As Decimal
        Public Property SegSoc As Decimal
        Public Property Irpf As Decimal
        Public Property Deutes As Decimal
        Public Property Liquid As Decimal

        Public Sub New(oNomina As DTONomina)
            MyBase.New()
            _Source = oNomina
            _Checked = True

            With oNomina
                _Nom = DTOStaff.AliasOrNom(.Staff)
                If .Devengat IsNot Nothing Then _Devengat = .Devengat.Eur
                If .Dietes IsNot Nothing Then _Dietas = .Dietes.Eur
                If .SegSocial IsNot Nothing Then _SegSoc = .SegSocial.Eur
                If .Irpf IsNot Nothing Then _Irpf = .Irpf.Eur
                If .Embargos IsNot Nothing Then _Deutes = .Embargos.Eur
                If .Deutes IsNot Nothing Then _Deutes += .Deutes.Eur
                If .Anticips IsNot Nothing Then _Deutes += .Anticips.Eur
                If .Liquid IsNot Nothing Then _Liquid = .Liquid.Eur

                If (_Devengat) <> (_SegSoc + _Irpf + _Deutes + _Liquid) Then
                    _Ico = My.Resources.warn
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class