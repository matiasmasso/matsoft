Public Class Xl_SupplierPriceListCompare
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of ComparedValue)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        ref
        nom
        vigent
        candidate
        diff
    End Enum

    Public Shadows Sub Load(oVigent As List(Of DTOPriceListItem_Supplier), oCandidate As DTOPriceListSupplier)
        Dim exs As New List(Of Exception)
        If FEB.PriceListSupplier.Load(exs, oCandidate) Then
            _Values = Merge(oVigent, oCandidate)

            Static PropertiesSet As Boolean
            If Not PropertiesSet Then
                SetProperties()
                PropertiesSet = True
            End If

            Refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Function Merge(oVigent As List(Of DTOPriceListItem_Supplier), oCandidate As DTOPriceListSupplier) As List(Of ComparedValue)
        Dim retval As New List(Of ComparedValue)
        Dim oVigentItems As List(Of DTOPriceListItem_Supplier) = oVigent.Where(Function(x) x.Ref.isNotEmpty()).OrderBy(Function(y) y.Ref).ToList
        Dim oCandidateItems As List(Of DTOPriceListItem_Supplier) = oCandidate.Items.Where(Function(x) x.Ref.isNotEmpty()).OrderBy(Function(y) y.Ref).ToList
        Dim idxVigent, idxCandidate As Integer
        Do
            Dim sVigentRef As String = ""
            Dim sCandidateRef As String = ""
            Dim sVigentNom As String = ""
            Dim sCandidateNom As String = ""


            If oVigentItems.Count > idxVigent Or oCandidateItems.Count > idxCandidate Then
                If oVigentItems.Count > idxVigent Then
                    sVigentRef = oVigentItems(idxVigent).Ref
                    sVigentNom = oVigentItems(idxVigent).Description
                End If
                If oCandidateItems.Count > idxCandidate Then
                    sCandidateRef = oCandidateItems(idxCandidate).Ref
                    sCandidateNom = oCandidateItems(idxCandidate).Description
                End If

                Dim item As New ComparedValue

                If (sVigentRef = "" Or (sCandidateRef > "" AndAlso sCandidateRef < sVigentRef)) Then
                    item.Ref = sCandidateRef
                    item.Nom = sCandidateNom
                    item.Candidate = oCandidateItems(idxCandidate)
                    idxCandidate += 1
                ElseIf (sCandidateRef = "" Or (sVigentRef > "" AndAlso sVigentRef < sCandidateRef)) Then
                    item.Ref = sVigentRef
                    item.Nom = sVigentNom
                    item.Vigent = oVigentItems(idxVigent)
                    idxVigent += 1
                Else
                    item.Ref = sVigentRef
                    item.Nom = sVigentNom
                    item.Vigent = oVigentItems(idxVigent)
                    item.Candidate = oCandidateItems(idxCandidate)
                    idxCandidate += 1
                    idxVigent += 1
                End If
                retval.Add(item)
            Else
                Exit Do
            End If

        Loop
        Return retval
    End Function

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of ComparedValue) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As ComparedValue In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of ComparedValue)
        Dim retval As List(Of ComparedValue)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Ref.ToLower.Contains(_Filter.ToLower) Or x.Nom.ToLower.Contains(_Filter.ToLower))
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
        With MyBase.Columns(Cols.ref)
            .HeaderText = "Ref"
            .DataPropertyName = "Ref"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.vigent)
            .HeaderText = "Vigent"
            .DataPropertyName = "Vigent"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.candidate)
            .HeaderText = "Candidat"
            .DataPropertyName = "candidate"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.diff)
            .HeaderText = "diff"
            .DataPropertyName = "diff"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "0.0\%;-0.0\%;#"
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

    Private Function SelectedItems() As List(Of ComparedValue)
        Dim retval As New List(Of ComparedValue)
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
            'Dim oMenu_Template As New Menu_Template(SelectedItems.First)
            'AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Template.Range)
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
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As ComparedValue = CurrentControlItem.Source
            'Dim oFrm As New Frm_Template(oSelectedValue)
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


    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.diff
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem.diff > 0 Then
                    e.CellStyle.BackColor = Color.LightSalmon
                ElseIf oControlItem.diff < 0 Then
                    e.CellStyle.BackColor = Color.LightGreen
                End If
        End Select
    End Sub

    Protected Class ComparedValue
        Property Vigent As DTOPriceListItem_Supplier
        Property Candidate As DTOPriceListItem_Supplier
        Property Ref As String
        Property Nom As String
    End Class

    Protected Class ControlItem
        Property Source As ComparedValue

        Property ref As String
        Property Nom As String
        Property vigent As Decimal
        Property candidate As Decimal
        Property diff As Decimal


        Public Sub New(oValue As ComparedValue)
            MyBase.New()
            _Source = oValue

            With _Source
                _Nom = .Nom
                _ref = .Ref
                If .Vigent IsNot Nothing Then
                    _vigent = .Vigent.CostNet
                End If
                If .Candidate IsNot Nothing Then
                    _candidate = .Candidate.CostNet
                End If
                If _vigent <> 0 And _candidate <> 0 Then
                    _diff = 100 * (_candidate / _vigent) - 100
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class


End Class
