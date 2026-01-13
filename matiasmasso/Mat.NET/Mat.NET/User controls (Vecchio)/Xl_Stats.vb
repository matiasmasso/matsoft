Public Class Xl_Stats
    Private _Stat As DTOStat
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        concept
        tot
        share
        M1
        M2
        M3
        M4
        M5
        M6
        M7
        M8
        M9
        M10
        M11
        M12
    End Enum

    Public Shadows Sub Load(value As DTOStat)
        If value IsNot Nothing Then
            _Stat = value
            _ControlItems = New ControlItems
            If _Stat.Items IsNot Nothing Then
                Dim oControlItem As ControlItem = Nothing

                Dim dcTotals As Decimal = _Stat.Tot.Tot()

                If _Stat.ConceptType <> DTOStat.ConceptTypes.Yeas Then
                    oControlItem = New ControlItem(_Stat.Tot, dcTotals)
                    _ControlItems.Add(oControlItem)
                End If

                For Each oItem As DTOStatItem In _Stat.Items
                    If oItem.HasChildren Then oItem.IsExpanded = (oItem.Level < _Stat.ExpandToLevel)
                    oControlItem = New ControlItem(oItem, dcTotals)
                    _ControlItems.Add(oControlItem)
                Next
                LoadGrid()
            End If
        End If
    End Sub

    Public ReadOnly Property Value As DTOStatItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOStatItem = oControlItem.Source
            Return retval
        End Get
    End Property


    Private Sub LoadGrid()
        Dim sFormat As String = ""
        Select Case _Stat.Format
            Case DTOStat.Formats.Amounts
                sFormat = "#,##0.00;-#,##0.00;#"
            Case DTOStat.Formats.Units
                sFormat = "#,###"
        End Select

        With DataGridView1
            .SuspendLayout()
            .Enabled = False

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

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.concept)
                .HeaderText = BLL.BLLSession.Current.Lang.Tradueix("concepto", "concepte", "concept")
                .DataPropertyName = "concept"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .MinimumWidth = IIf(_Stat.ConceptType = DTOStat.ConceptTypes.Yeas, 60, 120)
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.tot)
                .HeaderText = "total"
                .DataPropertyName = "tot"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = sFormat
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.share)
                .HeaderText = BLL.BLLSession.Current.Lang.Tradueix("cuota", "quota", "share")
                .DataPropertyName = "share"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0%;-0%;#"
            End With
            For iMes As Integer = 1 To 12
                .Columns.Add(New DataGridViewTextBoxColumn)
                With .Columns(Cols.share + iMes)
                    .HeaderText = BLL.BLLSession.Current.Lang.MesAbr(iMes)
                    .DataPropertyName = "M" & Format(iMes, "00")
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = sFormat
                End With

            Next

            For Each oRow As DataGridViewRow In .Rows
                Dim oItem As ControlItem = oRow.DataBoundItem
                oRow.Visible = oItem.Visible
            Next

            .Enabled = True
            .ResumeLayout()
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

    Private Function SelectedItems() As List(Of DTOStatItem)
        Dim retval As New List(Of DTOStatItem)
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

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_StatItem As New Menu_StatItem(SelectedItems.First)
            'AddHandler oMenu_StatItem.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_StatItem.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub



    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        Dim oNode As ControlItem = CurrentControlItem()

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
        Dim sConcept As String = oNode.concept
        Dim sOldSign As String = IIf(oNode.IsExpanded, "-", "+")
        Dim sNewSign As String = IIf(oNode.IsExpanded, "+", "-")
        Dim iPos As Integer = sConcept.IndexOf(sOldSign)
        Dim sb As New System.Text.StringBuilder
        If iPos > 0 Then sb.Append(sConcept.Substring(0, iPos))
        sb.Append(sNewSign)
        sb.Append(sConcept.Substring(iPos + 1))
        oNode.concept = sb.ToString
    End Sub

    Private Sub ExpandNode(oNode As ControlItem)
        For iRowIndex As Integer = DataGridView1.CurrentRow.Index + 1 To DataGridView1.Rows.Count - 1
            Dim oRow As DataGridViewRow = DataGridView1.Rows(iRowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Source.Level = oNode.Source.Level + 1 Then
                If oControlItem.Source.ParentIndex <> oNode.Source.Index Then Exit For
                oRow.Visible = True
            End If
        Next
    End Sub

    Private Sub CollapseNode(oNode As ControlItem)
        For iRowIndex As Integer = DataGridView1.CurrentRow.Index + 1 To DataGridView1.Rows.Count - 1
            Dim oRow As DataGridViewRow = DataGridView1.Rows(iRowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem.Source.Level <= oNode.Source.Level Then Exit For
            oControlItem.IsExpanded = False
            oRow.Visible = False
        Next
    End Sub


    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.Source.Level
            Case 1
                oRow.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 255)
            Case 2
                oRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 220)
            Case 3
                oRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 255)
        End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub RefreshRequest()

    End Sub

    Protected Class ControlItem
        Property Source As DTOStatItem
        Property Parent As Guid
        Property HasChildren As Boolean
        Property Key As Guid

        Property IsExpanded As Boolean
        Property Visible As Boolean

        Property concept As String
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

        Public Sub New(oStatItem As DTOStatItem, DcTotals As Decimal)
            MyBase.New()
            _Source = oStatItem

            With oStatItem
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

                _concept = sb.ToString
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
        Inherits System.ComponentModel.BindingList(Of ControlItem)
    End Class

End Class
