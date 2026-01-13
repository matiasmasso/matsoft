Public Class Xl_CsbResults
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOCsbResult)
    Private _AllControlItems As ControlItems
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Private Enum Cols
        Vto
        Tot
        Pending
        Paid
        Claimed
        Unpaid
        Ratio
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCsbResult))
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
        _AllControlItems = New ControlItems
        Dim oControlYear As New ControlItem(New DTOYearMonth(0, 0))
        Dim oControlMonth As New ControlItem(New DTOYearMonth(0, 0))
        Dim oControlItem As New ControlItem(CDate(Nothing))
        For Each oCsbVto In _Values
            If oCsbVto.vto <> oControlItem.vto Then
                If oCsbVto.vto.Year <> oControlMonth.yearMonth.year Or oCsbVto.vto.Month <> oControlMonth.yearMonth.month Then
                    If oCsbVto.vto.Year <> oControlMonth.yearMonth.year Then
                        oControlYear = New ControlItem(New DTOYearMonth(oCsbVto.vto.Year, 0))
                        _AllControlItems.Add(oControlYear)
                    End If
                    oControlMonth = New ControlItem(New DTOYearMonth(oCsbVto.vto.Year, oCsbVto.vto.Month))
                    _AllControlItems.Add(oControlMonth)
                End If
                oControlItem = New ControlItem(oCsbVto.vto)
                _AllControlItems.Add(oControlItem)
            End If
            Select Case oCsbVto.result
                Case DTOCsb.Results.Pendent
                    oControlItem.pending = oCsbVto.eur
                    oControlMonth.pending += oCsbVto.eur
                    oControlYear.pending += oCsbVto.eur
                Case DTOCsb.Results.Vençut
                    oControlItem.paid = oCsbVto.eur
                    oControlMonth.paid += oCsbVto.eur
                    oControlYear.paid += oCsbVto.eur
                Case DTOCsb.Results.Reclamat
                    oControlItem.claimed = oCsbVto.eur
                    oControlMonth.claimed += oCsbVto.eur
                    oControlYear.claimed += oCsbVto.eur
                Case DTOCsb.Results.Impagat
                    oControlItem.unpaid = oCsbVto.eur
                    oControlMonth.unpaid += oCsbVto.eur
                    oControlYear.unpaid += oCsbVto.eur
            End Select
        Next

        _ControlItems = New ControlItems
        For Each oControlItem In _AllControlItems.Where(Function(x) x.visible = True)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems

        MyBase.ClearSelection()
        _AllowEvents = True
    End Sub


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowCsbResult.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.vto)
            .HeaderText = "Venciment"
            .DataPropertyName = "caption"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Tot)
            .HeaderText = "Total"
            .DataPropertyName = "Tot"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pending)
            .HeaderText = "En Circulació"
            .DataPropertyName = "Pending"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Paid)
            .HeaderText = "Vençut"
            .DataPropertyName = "Paid"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Claimed)
            .HeaderText = "Reclamat"
            .DataPropertyName = "Claimed"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Unpaid)
            .HeaderText = "Impagat"
            .DataPropertyName = "Unpaid"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ratio)
            .HeaderText = "Index impagats"
            .DataPropertyName = "Ratio"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "0.#\%;-0.#\%;#"
        End With
    End Sub

    Private Sub Xl_CsbResults_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oCurrentRow As DataGridViewRow = MyBase.CurrentRow
        If oCurrentRow IsNot Nothing Then
            Dim oCurrentControlItem As ControlItem = oCurrentRow.DataBoundItem
            Select Case oCurrentControlItem.linCod
                Case ControlItem.linCods.year, ControlItem.linCods.month
                    oCurrentControlItem.expanded = Not oCurrentControlItem.expanded
                    Dim oCurrentLinCod = oCurrentControlItem.linCod

                    Dim oChildren = Children(oCurrentControlItem)
                    For Each oControlItem In oChildren
                        Select Case oCurrentLinCod
                            Case ControlItem.linCods.year
                                Select Case oControlItem.linCod
                                    Case ControlItem.linCods.month
                                        oControlItem.visible = oCurrentControlItem.expanded
                                    Case ControlItem.linCods.vto
                                        If oCurrentControlItem.expanded Then
                                            oControlItem.visible = oControlItem.isCurrent()
                                        Else
                                            oControlItem.visible = False
                                        End If
                                End Select
                            Case ControlItem.linCods.month
                                Select Case oControlItem.linCod
                                    Case ControlItem.linCods.vto
                                        oControlItem.visible = oCurrentControlItem.expanded
                                End Select
                        End Select

                    Next

                    _ControlItems.Clear()
                    For Each oControlItem In _AllControlItems.Where(Function(x) x.visible = True)
                        _ControlItems.Add(oControlItem)
                    Next


            End Select

        End If

    End Sub

    Private Function ChildrenRows(oCurrentControlItem As ControlItem) As List(Of DataGridViewRow)
        Dim retval As New List(Of DataGridViewRow)
        Dim oLinCod = oCurrentControlItem.linCod
        Dim oYearMonth = oCurrentControlItem.yearMonth
        Dim oChildrenRows As New List(Of DataGridViewRow)
        Select Case oLinCod
            Case ControlItem.linCods.year
                retval = MyBase.Rows.Cast(Of DataGridViewRow)().
                Where(Function(x) DirectCast(x.DataBoundItem, ControlItem).linCod <> ControlItem.linCods.year And DirectCast(x.DataBoundItem, ControlItem).yearMonth.year = oYearMonth.year).
                ToList
            Case ControlItem.linCods.month
                retval = MyBase.Rows.Cast(Of DataGridViewRow)().
                Where(Function(x) DirectCast(x.DataBoundItem, ControlItem).linCod = ControlItem.linCods.vto And DirectCast(x.DataBoundItem, ControlItem).yearMonth.year = oYearMonth.year And DirectCast(x.DataBoundItem, ControlItem).yearMonth.month = oYearMonth.month).
                ToList
        End Select
        Return retval
    End Function

    Private Function Children(oCurrentControlItem As ControlItem) As List(Of ControlItem)
        Dim retval As New List(Of ControlItem)
        Dim oLinCod = oCurrentControlItem.linCod
        Dim oYearMonth = oCurrentControlItem.yearMonth
        Select Case oLinCod
            Case ControlItem.linCods.year
                retval = _AllControlItems.
                Where(Function(x) x.linCod <> ControlItem.linCods.year And x.yearMonth.year = oYearMonth.year).
                ToList
            Case ControlItem.linCods.month
                retval = _AllControlItems.
                Where(Function(x) x.linCod = ControlItem.linCods.vto And x.yearMonth.year = oYearMonth.year And x.yearMonth.month = oYearMonth.month).
                ToList
        End Select
        Return retval
    End Function

    Protected Class ControlItem
        Property Guid As Guid

        Property vto As Date
        Property yearMonth As DTOYearMonth
        Property pending As Decimal
        Property paid As Decimal
        Property claimed As Decimal
        Property unpaid As Decimal

        Property linCod As linCods
        Property expanded As Boolean
        Property visible As Boolean

        Public Enum linCods
            year
            month
            vto
        End Enum

        ReadOnly Property tot As Decimal
            Get
                Return (_pending + paid + claimed + unpaid)
            End Get
        End Property

        ReadOnly Property ratio As Decimal
            Get
                Dim retval As Decimal
                If (_paid + _unpaid) > 0 Then
                    retval = Math.Round(100 * (_unpaid / (_paid + _unpaid)), 2)
                End If
                Return retval
            End Get
        End Property

        ReadOnly Property caption As String
            Get
                Select Case _linCod
                    Case linCods.year
                        Dim icon = IIf(_expanded, "-", "+")
                        Return icon & " " & _yearMonth.year.ToString
                    Case linCods.month
                        Dim icon = IIf(_expanded, "-", "+")
                        Dim olang = Current.Session.Lang
                        Return icon & " " & _yearMonth.year.ToString & " " & olang.Mes(_yearMonth.MonthNum)
                    Case linCods.vto
                        Return "        " & String.Format(_vto, "dd/MM/yy")
                    Case Else
                        Return ""
                End Select
            End Get
        End Property

        Public Sub New(vto As Date)
            MyBase.New()
            _Guid = Guid.NewGuid
            _vto = vto
            _yearMonth = New DTOYearMonth(vto.Year, vto.Month)
            _linCod = linCods.vto
            _visible = isCurrent()
        End Sub

        Public Sub New(yearMonth As DTOYearMonth)
            MyBase.New()
            _Guid = Guid.NewGuid
            _yearMonth = yearMonth
            If yearMonth.month = 0 Then
                _linCod = linCods.year
                _expanded = isCurrent()
                _visible = True
            Else
                _linCod = linCods.month
                _expanded = isCurrent()
                _visible = (_yearMonth.year = Today.Year)
            End If
        End Sub

        Function isCurrent() As Boolean
            Dim retval As Boolean = False
            Select Case _linCod
                Case linCods.year
                    retval = (_yearMonth.year = Today.Year)
                Case linCods.month
                    retval = (_yearMonth.year = Today.Year And _yearMonth.month = Today.Month)
                Case linCods.vto
                    retval = (_vto.Year = Today.Year And _vto.Month = Today.Month)
            End Select
            Return retval
        End Function

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


