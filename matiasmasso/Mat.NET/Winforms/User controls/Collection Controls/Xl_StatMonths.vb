Imports MatHelperStd

Public Class Xl_StatMonths
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTOStat
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Private Enum Cols
        concept
        tot
        share
        Mes01
        Mes02
        Mes03
        Mes04
        Mes05
        Mes06
        Mes07
        Mes08
        Mes09
        Mes10
        Mes11
        Mes12
    End Enum

    Public Shadows Sub Load(value As DTOStat)
        _Value = value

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Dim sNumFormat As String = IIf(_Value.Format = DTOStat.Formats.Amounts, "#,###0.00 €;-#,###0.00 €;#", "#")
        MyBase.Columns(Cols.tot).DefaultCellStyle.Format = sNumFormat
        For i As Integer = 1 To 12
            MyBase.Columns(Cols.share + i).DefaultCellStyle.Format = sNumFormat
        Next

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False

        Dim dcTotals As Decimal = _Value.Tot.Tot()

        _ControlItems = New ControlItems

        Dim oControlItem As ControlItem = Nothing
        If _Value.ConceptType <> DTOStat.ConceptTypes.Yeas Then
            oControlItem = New ControlItem(_Value.Tot, dcTotals)
            _ControlItems.Add(oControlItem)
        End If

        For Each oItem As DTOStatItem In _Value.Items
            If oItem.HasChildren Then oItem.IsExpanded = (oItem.Level < _Value.ExpandToLevel)
            oControlItem = New ControlItem(oItem, dcTotals)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)

        MyBase.SuspendLayout()
        MyBase.Enabled = False
        MyBase.DataSource = _ControlItems

        For Each oRow As DataGridViewRow In MyBase.Rows
            Dim oItem As ControlItem = oRow.DataBoundItem
            oRow.Visible = oItem.Visible
        Next

        UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        MyBase.Enabled = True
        MyBase.ResumeLayout()

        _AllowEvents = True
    End Sub



    Public ReadOnly Property Value As DTOStatItem
        Get
            Dim retval As DTOStatItem = Nothing
            Dim oRow As DataGridViewRow = MyBase.CurrentRow
            If oRow IsNot Nothing Then
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        Dim oUnitFilter As DTOStatFilter = _Value.Filters.Find(Function(x) x.Cod = DTOStatFilter.Cods.UnitsOrAmounts)

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
        With MyBase.Columns(Cols.concept)
            .HeaderText = Current.Session.Lang.Tradueix("concepto", "concepte", "concept")
            .DataPropertyName = "concept"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = IIf(_Value.ConceptType = DTOStat.ConceptTypes.Yeas, 60, 120)
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.tot)
            .HeaderText = "total"
            .DataPropertyName = "tot"
            .Width = 85
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = IIf(_Value.Format = DTOStat.Formats.Amounts, "#,###0.00 €;-#,###0.00 €;#", "#")
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.share)
            .HeaderText = Current.Session.Lang.Tradueix("cuota", "quota", "share")
            .DataPropertyName = "share"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "0%;-0%;#"
        End With

        For i As Integer = 1 To 12
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.share + i)
                .HeaderText = _Value.Lang.MesAbr(i)
                .DataPropertyName = String.Format("M{0:00}", i)
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = IIf(_Value.Format = DTOStat.Formats.Amounts, "#,###0.00 €;-#,###0.00 €;#", "#")
            End With
        Next



    End Sub


    Private Sub DataGridView_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        Dim oNode As ControlItem = oRow.DataBoundItem

        If oNode.HasChildren Then
            Dim iParentIndex As Integer = oNode.Source.Index
            If iParentIndex > 0 Then
                If oNode.IsExpanded Then
                    CollapseNode(oNode)
                Else
                    ExpandNode(oNode)
                End If

                SwitchExpandNodeSign(oNode)
                oNode.IsExpanded = Not oNode.IsExpanded

            End If
        End If
    End Sub

    Private Sub SwitchExpandNodeSign(ByRef oNode As ControlItem)
        Dim sConcept As String = oNode.Concept
        Dim sOldSign As String = IIf(oNode.IsExpanded, "-", "+")
        Dim sNewSign As String = IIf(oNode.IsExpanded, "+", "-")
        Dim iPos As Integer = sConcept.IndexOf(sOldSign)
        Dim sb As New System.Text.StringBuilder
        If iPos > 0 Then sb.Append(sConcept.Substring(0, iPos))
        sb.Append(sNewSign)
        sb.Append(sConcept.Substring(iPos + 1))
        oNode.Concept = sb.ToString
    End Sub

    Private Sub ExpandNode(oNode As ControlItem)
        For iRowIndex As Integer = MyBase.CurrentRow.Index + 1 To MyBase.Rows.Count - 1
            Dim oRow As DataGridViewRow = MyBase.Rows(iRowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Source.Level = oNode.Source.Level + 1 Then
                If oControlItem.Source.ParentIndex <> oNode.Source.Index Then Exit For
                oRow.Visible = True
            End If
        Next
    End Sub

    Private Sub CollapseNode(oNode As ControlItem)
        For iRowIndex As Integer = MyBase.CurrentRow.Index + 1 To MyBase.Rows.Count - 1
            Dim oRow As DataGridViewRow = MyBase.Rows(iRowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Source.Level <= oNode.Source.Level Then Exit For
            oControlItem.IsExpanded = False
            oRow.Visible = False
        Next
    End Sub

    Public Function Excel(oLang As DTOLang) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet
        With retval
            .DisplayTotals = True
            .AddColumn("Concept")
            .AddColumn("Total")
            For mes As Integer = 1 To 12
                .AddColumn(oLang.MesAbr(mes))
            Next
            For Each oRow As DataGridViewRow In MyBase.Rows
                If oRow.Visible Then
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim eRow As ExcelHelper.Row = retval.AddRow()
                    eRow.AddCell(oControlItem.Concept)
                    eRow.AddFormula("SUM(RC[1]:RC[12])")
                    eRow.AddCell(oControlItem.M01)
                    eRow.AddCell(oControlItem.M02)
                    eRow.AddCell(oControlItem.M03)
                    eRow.AddCell(oControlItem.M04)
                    eRow.AddCell(oControlItem.M05)
                    eRow.AddCell(oControlItem.M06)
                    eRow.AddCell(oControlItem.M07)
                    eRow.AddCell(oControlItem.M08)
                    eRow.AddCell(oControlItem.M09)
                    eRow.AddCell(oControlItem.M10)
                    eRow.AddCell(oControlItem.M11)
                    eRow.AddCell(oControlItem.M12)
                End If
            Next
        End With
        Return retval
    End Function


    Protected Class ControlItem
        Property Source As DTOStatItem
        Property Parent As Guid
        Property HasChildren As Boolean
        Property Key As Guid
        Property IsExpanded As Boolean
        Property Visible As Boolean

        Property Concept As String
        Property tot As Decimal
        Property share As Decimal

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

        Public Sub New(value As DTOStatItem, DcTotals As Decimal)
            MyBase.New()
            _Source = value
            With value
                _Parent = .Parent
                _Key = .Key
                _Visible = (.Level <= .Stat.ExpandToLevel)
                _HasChildren = .HasChildren
                _IsExpanded = .IsExpanded

                Dim sb As New System.Text.StringBuilder
                sb.Append(New String(" ", 4 * .Level))
                If _HasChildren Then
                    If _IsExpanded Then
                        sb.Append("- ")
                    Else
                        sb.Append("+ ")
                    End If
                End If
                sb.Append(.Concept)

                _Concept = sb.ToString
                _tot = .Tot
                If DcTotals > 0 Then
                    _share = _tot / DcTotals
                End If

                _M01 = .Values(0)
                _M02 = .Values(1)
                _M03 = .Values(2)
                _M04 = .Values(3)
                _M05 = .Values(4)
                _M06 = .Values(5)
                _M07 = .Values(6)
                _M08 = .Values(7)
                _M09 = .Values(8)
                _M10 = .Values(9)
                _M11 = .Values(10)
                _M12 = .Values(11)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


