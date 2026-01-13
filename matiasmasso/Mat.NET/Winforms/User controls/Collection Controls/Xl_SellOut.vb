Public Class Xl_SellOut

    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTOSellOut
    Private _Items As List(Of DTOSelloutItem)
    Private _DefaultValue As DTOSelloutItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Tot
    End Enum

    Public Shadows Sub Load(Value As DTOSellOut)
        _Value = Value
        _Items = _Value.Items

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        PropertiesAmmend

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        Dim oControlTot As New ControlItem()
        Select Case _Value.ConceptType
            Case DTOSellOut.ConceptTypes.Yeas
            Case Else
                _ControlItems.Add(oControlTot)
        End Select

        For Each oItem As DTOSelloutItem In _Items
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)

            With oControlTot
                .Tot += oControlItem.Tot
                .M01 += oControlItem.M01
                .M02 += oControlItem.M02
                .M03 += oControlItem.M03
                .M04 += oControlItem.M04
                .M05 += oControlItem.M05
                .M06 += oControlItem.M06
                .M07 += oControlItem.M07
                .M08 += oControlItem.M08
                .M09 += oControlItem.M09
                .M10 += oControlItem.M10
                .M11 += oControlItem.M11
                .M12 += oControlItem.M12
            End With
        Next

        'Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        'UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        MyBase.ClearSelection()
        'SetContextMenu()
        _AllowEvents = True
    End Sub


    Public ReadOnly Property Value As DTOSelloutItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOSelloutItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowSelloutItem.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .MinimumWidth = 100
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Tot)
            .HeaderText = "Totals"
            .DataPropertyName = "Tot"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With

        Dim monthIdx As Integer = 0
        For Each oYearMonth In _Value.YearMonths ' monthIdx As Integer = 1 To 12
            monthIdx += 1
            MyBase.Columns.Add(New DataGridViewTextBoxColumn)
            With MyBase.Columns(Cols.Tot + monthIdx)
                .HeaderText = DTOYearMonth.Formatted(oYearMonth, _Value.Lang)
                .DataPropertyName = String.Format("M{0:00}", monthIdx)
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Next
    End Sub

    Private Sub PropertiesAmmend()
        Dim ColMesosWidth As Integer = 70
        Dim ColTotWidth As Integer = 70
        If _Value.Items.Count > 0 Then
            Dim maxTot As Decimal = _Value.Items.Max(Function(x) x.Tot)
            ColTotWidth = ColWidth(maxTot)
            Dim maxMesos As Decimal = _Value.Items.SelectMany(Function(x) x.Values).Max
            ColMesosWidth = ColWidth(maxMesos)
        End If

        With MyBase.Columns(Cols.Tot)
            .Width = ColTotWidth
            Select Case _Value.Format
                Case DTOSellOut.Formats.Units
                    .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                Case DTOSellOut.Formats.Amounts
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            End Select
        End With

        Dim monthIdx As Integer = 0
        For Each oYearMonth In _Value.YearMonths ' monthIdx As Integer = 1 To 12
            monthIdx += 1
            With MyBase.Columns(Cols.Tot + monthIdx)
                .Width = ColMesosWidth
                Select Case _Value.Format
                    Case DTOSellOut.Formats.Units
                        .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                    Case DTOSellOut.Formats.Amounts
                        .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                End Select
            End With
        Next
    End Sub

    Private Function ColWidth(iMaxNum) As Integer
        Dim retval As Integer
        Select Case iMaxNum
            Case > 999999
                retval = 82
            Case > 99999
                retval = 76
            Case Else
                retval = 60
        End Select
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

    Private Function SelectedItems() As List(Of DTOSelloutItem)
        Dim retval As New List(Of DTOSelloutItem)
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

        If oControlItem IsNot Nothing AndAlso oControlItem.Source IsNot Nothing Then
            Try
                Dim oSrc As DTOBaseGuid = oControlItem.Source.Tag
                If TypeOf oSrc Is DTOCnap Then
                    Dim oMenuCnap As New Menu_Cnap(oSrc)
                    AddHandler oMenuCnap.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenuCnap.Range)
                ElseIf TypeOf oSrc Is DTODistributionChannel Then
                    Dim oMenuChannel As New Menu_DistributionChannel(oSrc)
                    AddHandler oMenuChannel.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenuChannel.Range)
                ElseIf TypeOf oSrc Is DTOProduct Then
                    Dim oMenuProduct As New Menu_Product(oSrc)
                    AddHandler oMenuProduct.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenuProduct.Range)
                ElseIf TypeOf oSrc Is DTOArea Then
                    Dim oMenuArea As New Menu_Area(oSrc)
                    AddHandler oMenuArea.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenuArea.Range)
                ElseIf TypeOf oSrc Is DTORep Then
                    Dim oMenuRep As New Menu_Rep(oSrc)
                    AddHandler oMenuRep.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenuRep.Range)
                End If

            Catch ex As Exception

            End Try
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            If oControlItem.IsExpanded Then
                CollapseItem(oControlItem)
            ElseIf oControlItem.HasChildren Then
                ExpandItem(oControlItem)
            End If
            ' Dim oFrm As New Frm_SelloutItem(oSelectedValue)
            ' AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            ' oFrm.Show()
        End If
    End Sub

    Private Sub ExpandItem(oParentControlItem As ControlItem)
        Dim idx As Integer = _ControlItems.IndexOf(oParentControlItem)
        Dim oValue As DTOSelloutItem = oParentControlItem.Source
        For Each item As DTOSelloutItem In oValue.Items
            idx += 1
            Dim oControlItem As New ControlItem(item)
            _ControlItems.Insert(idx, oControlItem)
        Next
        oParentControlItem.IsExpanded = True
    End Sub

    Private Sub CollapseItem(oControlItem As ControlItem)
        Dim oTarget As Guid = oControlItem.Source.Key
        For i As Integer = _ControlItems.Count - 1 To 0 Step -1
            If _ControlItems(i).Source IsNot Nothing AndAlso _ControlItems(i).Source.Parent.Equals(oTarget) Then
                CollapseItem(_ControlItems(i))
                _ControlItems.RemoveAt(i)
            End If
        Next
        oControlItem.IsExpanded = False
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Nom
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If oControlItem IsNot Nothing Then
                    Dim oSelloutItem As DTOSelloutItem = oControlItem.Source
                    If oSelloutItem IsNot Nothing Then
                        Dim iLeftPadding As Integer = 20 * oSelloutItem.Level
                        e.CellStyle.Padding = New Padding(iLeftPadding, 0, 0, 0)
                    End If
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CellPainting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) _
Handles Me.CellPainting
        If e.RowIndex > 0 AndAlso e.ColumnIndex = Cols.Nom Then
            Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                Dim oSelloutItem As DTOSelloutItem = oControlItem.Source
                If oSelloutItem IsNot Nothing Then
                    If oControlItem.HasChildren Then
                        Dim iLeftPadding As Integer = 20 * oSelloutItem.Level
                        DataGridView_CellPainting(sender, e, iLeftPadding, oControlItem.IsExpanded)  '6 is column index of “Change”
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub DataGridView_CellPainting(ByVal sender As System.Object, ByVal e As DataGridViewCellPaintingEventArgs, leftPadding As Integer, isExpanded As Boolean)

        Dim oBrush As Brush
        Dim icon As Image
        If e.RowIndex = MyBase.CurrentCell.RowIndex Then
            oBrush = Brushes.White
            icon = IIf(isExpanded, My.Resources.collapse9inverse, My.Resources.expand9inverse)
        Else
            oBrush = Brushes.Black
            icon = IIf(isExpanded, My.Resources.collapse9, My.Resources.expand9)
        End If

        Dim format As New StringFormat()

        e.PaintBackground(e.ClipBounds, True) 'transparent in cursor

        'draw icon image
        format.LineAlignment = StringAlignment.Center
        format.Alignment = StringAlignment.Near
        Dim iconMargin As Integer = 3
        Dim x As Integer = e.CellBounds.Left + leftPadding + iconMargin
        Dim y As Integer = e.CellBounds.Top + (e.CellBounds.Height - icon.Height) / 2
        Dim width As Integer = icon.Width
        Dim height As Integer = icon.Height
        Dim iconRect = New Rectangle(x, y, width, height)
        e.Graphics.DrawImage(icon, iconRect)

        'draw text
        format.LineAlignment = StringAlignment.Center
        format.Alignment = StringAlignment.Near
        format.Trimming = StringTrimming.EllipsisCharacter
        format.FormatFlags = StringFormatFlags.NoWrap

        x = e.CellBounds.Left + leftPadding + iconMargin + icon.Width + iconMargin
        y = e.CellBounds.Top
        width = e.CellBounds.Width - leftPadding - iconMargin - icon.Width - iconMargin
        height = e.CellBounds.Height
        Dim textRect = New Rectangle(x, y, width, height)



        e.Graphics.DrawString(e.Value, e.CellStyle.Font, oBrush, textRect, format)

        e.CellStyle.SelectionBackColor = Color.Transparent
        e.Handled = True
    End Sub


    Protected Class ControlItem
        Property Source As DTOSelloutItem
        Property IsExpanded As Boolean
        Property Nom As String


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

        Public Sub New(value As DTOSelloutItem)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Concept
                _Tot = .Tot
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

        Public Sub New()
            MyBase.New()
            _Nom = "Totals"
        End Sub

        Public Function HasChildren() As Boolean
            Dim retval As Boolean = _Source.Items.Count > 0
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

