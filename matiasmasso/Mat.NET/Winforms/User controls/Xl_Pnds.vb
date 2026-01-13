Public Class Xl_Pnds
    Inherits DataGridView

    Private _ControlItems As ControlItems
    Private _Pnds As List(Of DTOPnd)
    Private _Impagats As List(Of DTOImpagat)
    Private _Filter As String
    Private _ShowDivisas As Boolean
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        IcoStatus
        Vto
        Div
        Eur
        Cta
        Fra
        FraFch
        Txt
    End Enum

    Public Shadows Sub Load(oPnds As List(Of DTOPnd), oImpagats As List(Of DTOImpagat), Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Pnds = oPnds
        _Impagats = oImpagats
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        If oPnds IsNot Nothing Then
            For Each oPnd As DTOPnd In oPnds
                If oPnd.Amt IsNot Nothing Then
                    If oPnd.Amt.Cur IsNot Nothing Then
                        If oPnd.Amt.Cur.UnEquals(DTOCur.Eur) Then
                            MyBase.Columns(Cols.Div).Visible = True
                            Exit For
                        End If
                    End If
                End If
            Next
            refresca()
        End If

    End Sub


    Public Property Filter As String
        Get
            Return _filter
        End Get
        Set(value As String)
            _Filter = value
            If _Pnds IsNot Nothing Then refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _filter > "" Then
            _filter = ""
            refresca()
        End If
    End Sub

    Private Sub refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        Dim oFilteredPnds As List(Of DTOPnd) = FilteredPnds()
        If oFilteredPnds IsNot Nothing Then
            For Each oItem As DTOPnd In oFilteredPnds
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next
        End If

        Dim oFilteredImpagats As List(Of DTOImpagat) = FilteredImpagats()
        If oFilteredImpagats IsNot Nothing Then
            For Each oImpagat As DTOImpagat In oFilteredImpagats
                Dim oControlItem As New ControlItem(oImpagat)
                _ControlItems.Add(oControlItem)
            Next
        End If

        If oFilteredPnds IsNot Nothing Then
            MyBase.Columns(Cols.Div).Visible = IsDivisaVisible(oFilteredPnds)
        End If

        MyBase.DataSource = _ControlItems
        MyBase.CurrentCell = MyBase.FirstDisplayedCell
        SetContextMenu()

        _AllowEvents = True
    End Sub

    Private Function IsDivisaVisible(oFilteredValues As List(Of DTOPnd))
        Dim retval As Boolean
        If oFilteredValues.Count > 0 Then
            If oFilteredValues.First.Amt IsNot Nothing Then
                Dim FirstCur As DTOCur = oFilteredValues.First.Amt.Cur
                Dim BlDistinctCurs As Boolean = oFilteredValues.Any(Function(x) x.Amt IsNot Nothing AndAlso x.Amt.Cur.UnEquals(FirstCur))
                retval = BlDistinctCurs Or (FirstCur.UnEquals(DTOCur.Eur))
            End If
        End If
        Return retval
    End Function

    Private Function FilteredPnds() As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
        If _Filter = "" Then
            retval = _Pnds
        Else
            Dim LCaseFilter As String = _Filter.ToLower
            Dim DcFilter As Decimal = CDec(_Filter)
            retval = _Pnds.FindAll(Function(x) x.FraNum.ToString.Contains(_Filter) Or x.Amt.Eur = DcFilter)
        End If
        Return retval
    End Function

    Private Function FilteredImpagats() As List(Of DTOImpagat)
        Dim retval As New List(Of DTOImpagat)
        If _Filter = "" Then
            retval = _Impagats
        Else
            Dim LCaseFilter As String = _Filter.ToLower
            Dim DcFilter As Decimal = CDec(_Filter)
            retval = _Impagats.FindAll(Function(x) x.Csb.FraNum.ToString.Contains(_Filter) Or x.Csb.Amt.Eur = DcFilter)
        End If
        Return retval
    End Function

    Public ReadOnly Property Value As DTOPnd
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPnd = oControlItem.Source
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

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem IsNot Nothing Then retval.Add(CurrentControlItem)
        End If
        Return retval
    End Function

    Private Function SelectedPnds() As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If TypeOf oControlItem.Source Is DTOPnd Then
                retval.Add(oControlItem.Source)
            End If
        Next

        If retval.Count = 0 Then
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem IsNot Nothing Then
                If TypeOf oCurrentControlItem.Source Is DTOPnd Then
                    retval.Add(oCurrentControlItem.Source)
                End If
            End If
        End If
        Return retval
    End Function

    Private Function SelectedImpagats() As List(Of DTOImpagat)
        Dim retval As New List(Of DTOImpagat)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If TypeOf oControlItem.Source Is DTOImpagat Then
                retval.Add(oControlItem.Source)
            End If
        Next

        If retval.Count = 0 Then
            Dim oCurrentControlItem As ControlItem = CurrentControlItem()
            If oCurrentControlItem IsNot Nothing Then
                If TypeOf oCurrentControlItem.Source Is DTOImpagat Then
                    retval.Add(oCurrentControlItem.Source)
                End If
            End If
        End If
        Return retval
    End Function

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

        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.IcoStatus), DataGridViewImageColumn)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Vto)
            .DataPropertyName = "vto"
            .HeaderText = "venciment"
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "dd/MM/yy"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Div)
            .DataPropertyName = "div"
            .HeaderText = "divisas"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .DataPropertyName = "eur"
            .HeaderText = "import"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cta)
            .DataPropertyName = "cta"
            .HeaderText = "compte"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fra)
            .DataPropertyName = "fra"
            .HeaderText = "factura"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.FraFch)
            .DataPropertyName = "fch"
            .HeaderText = "data"
            .Width = 65
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "dd/MM/yy"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Txt)
            .DataPropertyName = "obs"
            .HeaderText = "observacions"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oLang As DTOLang = Current.Session.User.Lang
        Dim sFilename As String = oLang.Tradueix("M+O partidas pendientes", "M+O partides pendents", "M+O Open accounts") & ".xlsx"
        Dim sTitle As String = oLang.Tradueix("M+O partidas pendientes", "M+O partides pendents", "M+O Open accounts")
        Dim oSheet = DTOPnd.Excel(_Pnds, sTitle, DTOPnd.Codis.Deutor, oLang)

        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Pnds_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoStatus
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                If TypeOf oControlItem.Source Is DTOPnd Then
                    Dim oPnd As DTOPnd = oControlItem.Source
                    Select Case oPnd.Status
                        Case DTOPnd.StatusCod.enCirculacio
                            e.Value = My.Resources.candau
                        Case DTOPnd.StatusCod.enCartera
                            e.Value = My.Resources.candau_edit
                    End Select
                ElseIf TypeOf oControlItem.Source Is DTOImpagat Then
                    e.Value = My.Resources.pirata
                End If
            Case Cols.Div
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oPnd As DTOPnd = oControlItem.Source
                e.Value = DTOAmt.CurFormatted(oPnd.Amt)
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oSelectedValue As DTOPnd = CurrentControlItem.Source
        Dim oFrm As New Frm_Contact_Pnd(oSelectedValue)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            'RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItems As ControlItems = SelectedControlItems()

        Dim oSelectedPnds As List(Of DTOPnd) = SelectedPnds()
        Dim oSelectedImpagats As List(Of DTOImpagat) = SelectedImpagats()

        If oSelectedImpagats.Count + oSelectedPnds.Count > 0 Then
            If oSelectedImpagats.Count = 0 Then
                Dim oMenu_Pnds As New Menu_Pnd(oSelectedPnds)
                AddHandler oMenu_Pnds.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Pnds.Range)
            ElseIf oSelectedPnds.Count = 0 Then
                Dim oMenu_Impagats As New Menu_Impagat(SelectedImpagats)
                AddHandler oMenu_Impagats.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu_Impagats.Range)
            End If
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add("Excel", My.Resources.Excel_16, AddressOf Do_Excel)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOBaseGuid

        Property IcoStatus As DTOPnd.StatusCod
        Property Vto As Date
        Property Div As Decimal
        Property Eur As Decimal
        Property Cta As String
        Property Fra As String
        Property Fch As Date
        Property Obs As String

        Public Sub New(value As DTOPnd)
            MyBase.New()
            _Source = value
            With value
                _IcoStatus = .Status
                _Vto = .Vto
                If .Amt IsNot Nothing Then
                    _Div = .Amt.Val
                    _Eur = .Amt.Eur
                End If
                _Cta = DTOPgcCta.FullNom(.Cta, Current.Session.User.Lang)
                _Fra = .FraNum
                _Fch = .Fch
                _Obs = .Fpg
            End With
        End Sub

        Public Sub New(value As DTOImpagat)
            MyBase.New()
            Dim exs As New List(Of Exception)
            _Source = value
            With value
                _IcoStatus = .Status
                _Vto = .Csb.Vto
                _Div = .Csb.Amt.Val
                _Eur = DTOImpagat.Pendent(value).Eur + value.Gastos.Eur
                _Cta = DTOPgcCta.FullNom(FEB2.PgcCta.FromCodSync(DTOPgcPlan.Ctas.impagats, Current.Session.Emp, exs), Current.Session.User.Lang)
                _Fra = .Csb.FraNum
                _Fch = .Csb.Vto
                _Obs = .Obs
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


