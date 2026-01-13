Public Class Xl_CsbMailingLogs

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of CsbLog)
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Private Enum Cols
        cli
        eur
        txt
    End Enum

    Public Shadows Sub Load(oCsbs As List(Of DTOCsb))
        _Values = New List(Of CsbLog)
        For Each oCsb In oCsbs
            For Each oLog In oCsb.mailingLogs
                Dim value As New CsbLog
                value.Csb = oCsb
                value.Log = oLog
                _Values.Add(value)
            Next
        Next

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of CsbLog) = FilteredValues()
        _ControlItems = New ControlItems

        Dim items = oFilteredValues.GroupBy(Function(g) New With {Key .fch = g.Log.roundedFch, Key g.Csb.vto}).
        Select(Function(group) New With {.fch = group.Key.fch, .vto = group.Key.vto, .count = group.Count})
        For Each oItem In items
            Dim oControlItem As New ControlItem(oItem.fch, oItem.vto, oItem.count)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub



    Private Function FilteredValues() As List(Of CsbLog)
        Dim retval As List(Of CsbLog)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Log.user.emailAddress.ToLower.Contains(_Filter.ToLower) Or x.Csb.contact.nom.ToLower.Contains(_Filter.ToLower))
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
        'MyBase.RowMailingLog.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.cli)
            .HeaderText = "Client"
            .DataPropertyName = "cli"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 120
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.txt)
            .HeaderText = "Concepte"
            .DataPropertyName = "Txt"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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
            'Dim oMenu_Csb As New Menu_Csb(SelectedItems.First)
            'AddHandler oMenu_MailingLog.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_MailingLog.Range)
            'oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            oControlItem.isExpanded = Not oControlItem.isExpanded
            Select Case oControlItem.linCod
                Case ControlItem.linCods.group
                    Dim fch = oControlItem.Fch
                    Select Case oControlItem.isExpanded
                        Case True
                            Dim idx = _ControlItems.IndexOf(oControlItem) + 1
                            Dim oCsbLogs = FilteredValues.Where(Function(x) x.Log.roundedFch = fch And x.Csb.vto = oControlItem.Vto).
                                GroupBy(Function(y) y.Csb.Guid).
                                Select(Function(z) z.First).
                                OrderBy(Function(r) r.Csb.contact.nom)
                            For Each oCsbLog In oCsbLogs
                                Dim pControlItem As New ControlItem(oCsbLog, ControlItem.linCods.csb)
                                _ControlItems.Insert(idx, pControlItem)
                            Next
                        Case False
                            For i = _ControlItems.Count - 1 To 0 Step -1
                                If MatchesGroup(oControlItem, _ControlItems(i)) Then
                                    _ControlItems.RemoveAt(i)
                                End If
                            Next
                    End Select
                Case ControlItem.linCods.csb
                    Select Case oControlItem.isExpanded
                        Case True
                            Dim idx = _ControlItems.IndexOf(oControlItem) + 1
                            Dim oCsbLogs = FilteredValues.Where(Function(x) x.Log.roundedFch = oControlItem.Fch And x.Csb.Equals(oControlItem.csb))
                            For Each oCsbLog In oCsbLogs
                                Dim pControlItem As New ControlItem(oCsbLog, ControlItem.linCods.log)
                                _ControlItems.Insert(idx, pControlItem)
                            Next
                        Case Else
                            For i = _ControlItems.Count - 1 To 0 Step -1
                                If MatchesGroup(oControlItem, _ControlItems(i)) Then
                                    _ControlItems.RemoveAt(i)
                                End If
                            Next
                    End Select
            End Select
        End If
    End Sub

    Private Shared Function MatchesGroup(oParent As ControlItem, oChild As ControlItem) As Boolean
        Dim retval As Boolean = False
        If oChild.linCod > oParent.linCod Then
            If oChild.Fch = oParent.Fch Then
                Select Case oParent.linCod
                    Case ControlItem.linCods.group
                        retval = (oChild.Vto = oParent.Vto)
                    Case ControlItem.linCods.csb
                        If oParent.csb IsNot Nothing And oChild.csb IsNot Nothing Then
                            retval = (oChild.csb.Equals(oParent.csb))
                        End If
                End Select
            End If
        End If
        Return retval
    End Function


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_CsbMailingLogs_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
        If e.RowIndex >= 0 Then
            Dim oControlItem = _ControlItems(e.RowIndex)
            Select Case oControlItem.lincod
                Case ControlItem.linCods.group
                    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None
                Case ControlItem.linCods.csb
                    If e.ColumnIndex = 0 Then
                        Dim sf As New StringFormat()
                        sf.Alignment = StringAlignment.Near
                        sf.LineAlignment = StringAlignment.Near
                        sf.Trimming = StringTrimming.EllipsisCharacter

                        Dim rect As New Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height - 1)
                        Dim textRect As New Rectangle(rect.X + 30, rect.Y + 1, rect.Width - 30, rect.Height - 2)


                        Dim state As DataGridViewElementStates = (e.State And DataGridViewElementStates.Selected)
                        If state = DataGridViewElementStates.Selected Then
                            e.Graphics.FillRectangle(New SolidBrush(e.CellStyle.SelectionBackColor), rect)
                        Else
                            e.Graphics.FillRectangle(New SolidBrush(e.CellStyle.BackColor), rect)
                        End If

                        e.Graphics.DrawImage(oControlItem.icon, New Point(rect.X + 20, rect.Y + 3))

                        Dim oFontColor = IIf(state = DataGridViewElementStates.Selected, e.CellStyle.SelectionForeColor, e.CellStyle.ForeColor)
                        e.Graphics.DrawString(oControlItem.Cli, e.CellStyle.Font, New SolidBrush(oFontColor), textRect, sf)
                        e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single
                        'e.PaintContent(e.CellBounds)
                        e.Handled = True
                    End If
            End Select
        End If
    End Sub

    Private Sub Xl_CsbMailingLogs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Dim oControlItem = _ControlItems(e.RowIndex)
        Select Case oControlItem.linCod
            Case ControlItem.linCods.group, ControlItem.linCods.log
                e.Value = ""
                e.FormattingApplied = True
        End Select
    End Sub

    Private Sub Xl_CsbMailingLogs_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles Me.RowPostPaint
        If MyBase.RowCount > 0 And e.RowIndex > -1 Then
            Dim oControlItem = _ControlItems(e.RowIndex)

            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Near
            sf.LineAlignment = StringAlignment.Near
            sf.Trimming = StringTrimming.EllipsisCharacter
            Dim state As DataGridViewElementStates = (e.State And DataGridViewElementStates.Selected)
            Dim oFontColor = IIf(state = DataGridViewElementStates.Selected, e.InheritedRowStyle.SelectionForeColor, e.InheritedRowStyle.ForeColor)

            Dim rect As Rectangle = MyBase.GetRowDisplayRectangle(e.RowIndex, True)
            Select Case oControlItem.linCod
                Case ControlItem.linCods.group
                    Dim textRect As New Rectangle(rect.X + 15, rect.Y + 1, rect.Width - 15, rect.Height - 1)
                    e.Graphics.DrawImage(oControlItem.icon, New Point(rect.X + 5, rect.Y + 3))
                    e.Graphics.DrawString(oControlItem.Txt, e.InheritedRowStyle.Font, New SolidBrush(oFontColor), textRect, sf)
                Case ControlItem.linCods.log
                    Dim textRect As New Rectangle(rect.X + 30, rect.Y + 1, rect.Width - 30, rect.Height - 1)
                    e.Graphics.DrawString(DTOUser.AddressAndNickname(oControlItem.mailingLog.user), e.InheritedRowStyle.Font, New SolidBrush(oFontColor), textRect, sf)
            End Select
        End If
    End Sub


    Protected Class CsbLog
        Property Log As DTOMailingLog
        Property Csb As DTOCsb
    End Class

    Protected Class ControlItem
        Property mailingLog As DTOMailingLog
        Property csb As DTOCsb

        Property Fch As Date
        Property Usr As String
        Property Vto As Date
        Property Cli As String
        Property Eur As Decimal
        Property Txt As String
        Property linCod As linCods
        Property isExpanded As Boolean = False

        Public Enum linCods
            group
            csb
            log
        End Enum

        Public Sub New(fch As Date, vto As Date, count As Integer)
            MyBase.New()
            _linCod = linCods.group
            _Fch = fch
            _Vto = vto
            _Txt = String.Format("{0:dd/MM/yy} {0:HH\:mm} vto.{1:dd/MM/yy} total {2} notificacions", fch, vto, count)
        End Sub

        Public Sub New(oCsbLog As CsbLog, oLinCod As linCods)
            MyBase.New()
            _linCod = oLinCod
            _Fch = oCsbLog.Log.roundedFch
            _csb = oCsbLog.Csb
            _mailingLog = oCsbLog.Log
            With _mailingLog
                _Usr = .user.emailAddress
            End With
            Select Case oLinCod
                Case linCods.csb
                    With _csb
                        _Eur = .amt.eur
                        _Cli = .contact.nom
                        _Txt = .txt
                        _Vto = .vto
                    End With
                Case linCods.log
                    _Cli = _mailingLog.user.emailAddress
            End Select
        End Sub


        Public Function icon() As Image
            Select Case _isExpanded
                Case True
                    Return My.Resources.minus
                Case Else
                    Return My.Resources.plus1
            End Select
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


