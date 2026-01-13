Public Class Xl_SumasYSaldos

    Inherits DataGridView

    Private _Value As DTOSumasYSaldos
    Private _DefaultValue As DTOVisaEmisor
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleProgressBar(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Concepte
        SdoInicial
        Deb
        Hab
        SdoFinal
    End Enum

    Public Shadows Sub Load(value As DTOSumasYSaldos, Optional oDefaultValue As DTOSumasYSaldosItem = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Value = value
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOSumasYSaldosItem) = FilteredValues()

        MyBase.SuspendLayout()
        MyBase.Enabled = False
        _ControlItems = New ControlItems
        MyBase.DataSource = _ControlItems
        Dim oCta As New DTOPgcCta

        _ControlItems.Add(ControlItem.Totals(oFilteredValues))
        For Each oItem As DTOSumasYSaldosItem In oFilteredValues
            Dim oControlItem As ControlItem = Nothing
            If oItem.Equals(oCta) Then
                oControlItem = New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Else
                oCta = oItem
                Dim oSubComptes As List(Of DTOSumasYSaldosItem) = SubComptes(oCta, oFilteredValues)
                If oItem.Contact Is Nothing Then
                    If oSubComptes.Count = 1 Then
                        oControlItem = New ControlItem(oItem, ControlItem.Levels.Cta)
                        _ControlItems.Add(oControlItem)
                    Else
                        Dim oSummary As New ControlItem(SummaryItem(oSubComptes), ControlItem.Levels.Cta, ControlItem.Status.Colapsed)
                        _ControlItems.Add(oSummary)
                        oControlItem = New ControlItem(oItem, ControlItem.Levels.SubCta)
                        _ControlItems.Add(oControlItem)
                    End If
                Else
                    Dim oSummary As New ControlItem(SummaryItem(oSubComptes), ControlItem.Levels.Cta, ControlItem.Status.Colapsed)
                    _ControlItems.Add(oSummary)
                    oControlItem = New ControlItem(oItem, ControlItem.Levels.SubCta)
                    _ControlItems.Add(oControlItem)
                End If
            End If
        Next

        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
            For Each oRow As DataGridViewRow In MyBase.Rows
                Dim oItem As ControlItem = oRow.DataBoundItem
                oRow.Visible = oItem.Visible
            Next

        End If
        MyBase.Enabled = True
        MyBase.ResumeLayout()

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Concepte)
            End If
        End If

        SetContextMenu()
        MyBase.ClearSelection()
        _AllowEvents = True
    End Sub

    Private Function SubComptes(oCta As DTOPgcCta, oFilteredValues As List(Of DTOSumasYSaldosItem)) As List(Of DTOSumasYSaldosItem)
        Dim retval As List(Of DTOSumasYSaldosItem) = oFilteredValues.FindAll(Function(x) x.Equals(oCta)).ToList
        Return retval
    End Function

    Private Function SummaryItem(oValues As List(Of DTOSumasYSaldosItem)) As DTOSumasYSaldosItem
        Dim retval As New DTOSumasYSaldosItem(oValues.First.Guid)
        With retval
            .Id = oValues.First.Id
            .Nom = oValues.First.Nom
            .Act = oValues.First.Act
            .SdoInicial = oValues.Sum(Function(x) x.SdoInicial)
            .Debe = oValues.Sum(Function(x) x.Debe)
            .Haber = oValues.Sum(Function(x) x.Haber)
            .SdoFinal = oValues.Sum(Function(x) x.SdoFinal)
        End With
        Return retval
    End Function

    Private Function FilteredValues() As List(Of DTOSumasYSaldosItem)
        Dim retval As List(Of DTOSumasYSaldosItem)
        If _Filter = "" Then
            retval = _Value.items
        Else
            Dim tmp As String = _Filter.ToLower
            retval = _Value.items.FindAll(Function(x) x.Nom.Esp.ToLower.Contains(tmp) Or x.Nom.Cat.ToLower.Contains(tmp) Or x.Nom.Eng.ToLower.Contains(tmp))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Value IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOSumasYSaldosItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOSumasYSaldosItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

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
        With MyBase.Columns(Cols.concepte)
            .HeaderText = "Compte"
            .DataPropertyName = "Concepte"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SdoInicial)
            .HeaderText = "Sdo.Inicial"
            .DataPropertyName = "SdoInicial"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Deb)
            .HeaderText = "Deure"
            .DataPropertyName = "Deb"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Hab)
            .HeaderText = "Haver"
            .DataPropertyName = "Hab"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SdoFinal)
            .HeaderText = "Sdo.Final"
            .DataPropertyName = "SdoFinal"
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

    Private Function SelectedItems() As List(Of DTOSumasYSaldosItem)
        Dim retval As New List(Of DTOSumasYSaldosItem)
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
            Dim oExercici As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, _Value.Fch.Year)
            Select Case oControlItem.Level
                Case ControlItem.Levels.Cta
                    Dim oCce As New DTOCce(oExercici, oControlItem.Source)
                    Dim oMenu_Cce As New Menu_Cce(oCce)
                    AddHandler oMenu_Cce.RequestToToggleProgressBar, AddressOf ToggleProgressBarRequest
                    AddHandler oMenu_Cce.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Cce.Range)
                Case ControlItem.Levels.SubCta
                    Dim oCcd = DTOCcd.Factory(oExercici, oControlItem.Source, oControlItem.Source.Contact)
                    Dim oMenu_Ccd As New Menu_Ccd(oCcd)
                    AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Ccd.Range)
            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ToggleProgressBarRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToToggleProgressBar(Me, e)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oNode As ControlItem = CurrentControlItem()

        If oNode.HasChildren Then
            'Dim iParentIndex As Integer = oNode.Source.Index
            'If iParentIndex > 0 Then
            If oNode.Stat = ControlItem.Status.Expanded Then
                CollapseNode(oNode)
            Else
                ExpandNode(oNode)
            End If

            SwitchExpandNodeSign(oNode)

            'End If
        End If
    End Sub

    Private Sub SwitchExpandNodeSign(ByRef oNode As ControlItem)
        Dim sConcept As String = oNode.Concepte
        Dim sNewSign As String = IIf(oNode.IsExpanded, "-", "+")
        Dim sOldSign As String = IIf(oNode.IsExpanded, "+", "-")
        Dim iPos As Integer = sConcept.IndexOf(sOldSign)
        Dim sb As New System.Text.StringBuilder
        If iPos > 0 Then sb.Append(sConcept.Substring(0, iPos))
        sb.Append(sNewSign)
        sb.Append(sConcept.Substring(iPos + 1))
        oNode.Concepte = sb.ToString
    End Sub

    Private Sub ExpandNode(oNode As ControlItem)
        For iRowIndex As Integer = MyBase.CurrentRow.Index + 1 To MyBase.Rows.Count - 1
            Dim oRow As DataGridViewRow = MyBase.Rows(iRowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Level = oNode.Level + 1 Then
                If oControlItem.Source.UnEquals(oNode.Source) Then Exit For
                oRow.Visible = True
            End If
        Next
        oNode.Stat = ControlItem.Status.Expanded
    End Sub

    Private Sub CollapseNode(oNode As ControlItem)
        For iRowIndex As Integer = MyBase.CurrentRow.Index + 1 To MyBase.Rows.Count - 1
            Dim oRow As DataGridViewRow = MyBase.Rows(iRowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Level <= oNode.Level Then Exit For
            oControlItem.Stat = ControlItem.Status.Colapsed
            oRow.Visible = False
        Next
        oNode.Stat = ControlItem.Status.Colapsed
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

    Private Sub Xl_SumasYSaldos_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        If oRow IsNot Nothing Then
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.Level
                Case ControlItem.Levels.Totals
                    Dim success As Boolean = oControlItem.Deb = oControlItem.Hab
                    If oControlItem.SdoInicial <> 0 Then success = False
                    If oControlItem.SdoFinal <> 0 Then success = False
                    If success Then
                        oRow.DefaultCellStyle.BackColor = Color.LightBlue
                    Else
                        oRow.DefaultCellStyle.BackColor = Color.LightSalmon
                    End If
            End Select
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOSumasYSaldosItem
        Property Stat As Status
        Property Level As Levels
        Property Visible As Boolean

        Property Concepte As String
        Property SdoInicial As Decimal
        Property Deb As Decimal
        Property Hab As Decimal
        Property SdoFinal As Decimal

        Property Cta As DTOPgcCta

        Public Enum Levels
            Totals
            Cta
            SubCta
        End Enum

        Public Enum Status
            Expanded
            Colapsed
            NoChildren
        End Enum

        Public Sub New()
            MyBase.New
        End Sub

        Public Sub New(value As DTOSumasYSaldosItem, Optional olevel As Levels = Levels.SubCta, Optional oStat As Status = Status.NoChildren)
            MyBase.New()
            _Source = value
            _Stat = oStat
            _Level = olevel
            _Visible = (_Level <= Levels.Cta)


            With value
                Dim sb As New System.Text.StringBuilder
                sb.Append(New String(" ", 4 * olevel))
                Select Case oStat
                    Case Status.Expanded
                        sb.Append("- ")
                    Case Status.Colapsed
                        sb.Append("+ ")
                    Case Status.NoChildren
                        sb.Append("   ")
                End Select

                sb.Append(DTOPgcCta.FullNom(value, Current.Session.User.Lang))

                _Cta = value
                _Concepte = sb.ToString
                If .Contact IsNot Nothing Then
                    _Concepte = _Concepte & " " & .Contact.FullNom
                End If
                _SdoInicial = .SdoInicial
                _Deb = .Debe
                _Hab = .Haber
                _SdoFinal = .SdoFinal
            End With
        End Sub

        Shared Function Totals(items As List(Of DTOSumasYSaldosItem))
            Dim retval As New ControlItem()
            With retval
                .Stat = Status.NoChildren
                .Level = Levels.Totals
                .Visible = True
                .Concepte = "totals"
                .SdoInicial = items.Sum(Function(x) x.SdoInicial)
                .Deb = items.Sum(Function(x) x.Debe)
                .Hab = items.Sum(Function(x) x.Haber)
                .SdoFinal = items.Sum(Function(x) x.SdoFinal)
            End With
            Return retval
        End Function

        Public Function IsExpanded() As Boolean
            Return _Stat = Status.Expanded
        End Function

        Public Function IsColapsed() As Boolean
            Return _Stat = Status.Colapsed
        End Function

        Public Function HasChildren() As Boolean
            Return _Stat <> Status.NoChildren
        End Function

        Public Function HasNoChildren() As Boolean
            Return _Stat = Status.NoChildren
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

