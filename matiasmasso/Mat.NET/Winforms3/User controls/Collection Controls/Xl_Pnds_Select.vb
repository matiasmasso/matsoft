Public Class Xl_Pnds_Select
    Private _ControlItems As New ControlItems
    Protected Shared _CtaImpagats As DTOPgcCta
    Private _Codi As DTOPnd.Codis = DTOPnd.Codis.NotSet
    Private _Sum As DTOAmt

    'Private _ContainsDivises As Boolean
    Private _AllowEvents As Boolean

    Public Event ItemCheckedChange(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Checked
        Vto
        Amt
        Eur
        Fra
        Fch
        Cta
        Obs
    End Enum

    Public Enum Totals
        All
        AllChecked
        CheckedPendents
        CheckedImpagats
    End Enum

    Public Shadows Sub Load(value As List(Of DTOPnd), oCodi As DTOPnd.Codis)
        _Codi = oCodi
        For Each oItem As DTOPnd In value
            'If oItem.Amt.Cur.Tag <> DTOCur.Ids.EUR.ToString Then _ContainsDivises = True
            Dim oControlItem As New ControlItem(oItem, _Codi)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public Shadows Sub Load(value As List(Of DTOImpagat))
        Dim exs As New List(Of Exception)
        _CtaImpagats = FEB.PgcCta.FromCodSync(DTOPgcPlan.Ctas.impagats, Current.Session.Emp, exs)
        For Each oItem As DTOImpagat In value
            'If oItem.Csb.Amt.Cur.Tag <> DTOCur.Ids.EUR.ToString Then _ContainsDivises = True
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public Property Codi() As DTOPnd.Codis
        Get
            Return _Codi
        End Get
        Set(ByVal Value As DTOPnd.Codis)
            _Codi = Value
        End Set
    End Property

    Public ReadOnly Property Pnds As List(Of DTOPnd)
        Get
            Dim retval As New List(Of DTOPnd)
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked And oControlItem.SrcCod = ControlItem.SrcCods.pendent Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Impagats As List(Of DTOImpagat)
        Get
            Dim retval As New List(Of DTOImpagat)
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked And oControlItem.SrcCod = ControlItem.SrcCods.impagat Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property SelectedAmt As DTOAmt
        Get
            Dim retval = DTOAmt.Empty
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked Then
                    Select Case oControlItem.SrcCod
                        Case ControlItem.SrcCods.pendent
                            Dim oPnd As DTOPnd = oControlItem.Source
                            If oPnd.Cod = _Codi Then
                                retval.Add(oPnd.Amt)
                            Else
                                retval.Substract(oPnd.Amt)
                            End If
                        Case ControlItem.SrcCods.impagat
                            Dim oImpagat As DTOImpagat = oControlItem.Source
                            retval.Add(oImpagat.Nominal)
                    End Select
                End If
            Next
            Return retval
        End Get
    End Property


    Public Sub SetCheckedItems(oPndsToCheck As List(Of DTOPnd))
        For Each oControlItem As ControlItem In _ControlItems
            Dim pPnd As DTOPnd = oControlItem.Source
            'oControlItem.Checked = oPndsToCheck.Exists(Function(x) x.Equals(pPnd))
            oControlItem.Checked = oPndsToCheck.Exists(Function(x) x.Guid.Equals(pPnd.Guid))
        Next
        TextBoxSel.Text = DTOAmt.CurFormatted(Me.SelectedAmt)
    End Sub

    Private Sub SetTotals()
        Dim oSum As DTOAmt = GetTotal(Totals.AllChecked)

        Dim s As String = ""
        If oSum.Cur IsNot Nothing Then
            s = DTOAmt.CurFormatted(oSum)
            If oSum.Cur.Tag <> "EUR" Then
                s = s & " (" & Format(oSum.Eur, "#,###.00 EUR") & ")"
            End If

        End If
        TextBoxSel.Text = s
        _Sum = oSum
    End Sub

    Private Sub LoadGrid()
        _AllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()
            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewImageColumn)
            With DirectCast(.Columns(Cols.Checked), DataGridViewImageColumn)
                .DataPropertyName = "Checked"
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Vto)
                .HeaderText = "Venciment"
                .DataPropertyName = "Vto"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .DataPropertyName = "Amt"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Eur)
                If IsTotEur() Then
                    .Visible = False
                Else
                    .Visible = True
                    .HeaderText = "Eur"
                    .DataPropertyName = "Eur"
                    .Width = 65
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
                End If
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fra)
                .HeaderText = "Factura"
                .DataPropertyName = "Fra"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "dd/MM/yy"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Cta)
                .HeaderText = "Compte"
                .DataPropertyName = "Cta"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Obs)
                .HeaderText = "Observacions"
                .DataPropertyName = "Obs"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With


        Dim oAmt As DTOAmt = GetTotal(Totals.All)
        TextBoxTot.Text = DTOAmt.CurFormatted(oAmt)
        TextBoxSel.Text = ""

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Function GetTotal(oTotalMode As Totals) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oControlItem As ControlItem In _ControlItems

            Dim BlProceed As Boolean = False
            Select Case oTotalMode
                Case Totals.All
                    BlProceed = True
                Case Totals.AllChecked
                    BlProceed = oControlItem.Checked
                Case Totals.CheckedPendents
                    BlProceed = oControlItem.Checked And oControlItem.SrcCod = ControlItem.SrcCods.pendent
                Case Totals.CheckedImpagats
                    BlProceed = oControlItem.Checked And oControlItem.SrcCod = ControlItem.SrcCods.impagat
            End Select

            If BlProceed Then
                Select Case oControlItem.SrcCod
                    Case ControlItem.SrcCods.pendent
                        Dim oPnd As DTOPnd = oControlItem.Source
                        If oPnd.Cod = _Codi Then
                            retval.Add(oPnd.Amt)
                        Else
                            retval.Substract(oPnd.Amt)
                        End If
                    Case ControlItem.SrcCods.impagat
                        Dim oImpagat As DTOImpagat = oControlItem.Source
                        retval.Add(oImpagat.Nominal)
                End Select
            End If
        Next
        Return retval
    End Function

    Private Function IsTotEur() As Boolean
        Dim retval As Boolean = True
        For Each oControlItem As ControlItem In _ControlItems
            Select Case oControlItem.SrcCod
                Case ControlItem.SrcCods.pendent
                    Dim oPnd As DTOPnd = oControlItem.Source
                    If Not oPnd.amt.Cur.Tag = DTOCur.Eur.Tag Then
                        retval = False
                        Exit For
                    End If
                Case ControlItem.SrcCods.impagat
                    Dim oImpagat As DTOImpagat = oControlItem.Source
                    If Not oImpagat.Nominal.Cur.Tag = DTOCur.Eur.Tag Then
                        retval = False
                        Exit For
                    End If
            End Select
        Next
        Return retval
    End Function

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of DTOPnd)
        Dim retval As New List(Of DTOPnd)
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

    Private Sub SetContextMenu(Optional oRow As DataGridViewRow = Nothing)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem
        If oRow Is Nothing Then
            oControlItem = CurrentControlItem()
        Else
            oControlItem = oRow.DataBoundItem
        End If

        If oControlItem IsNot Nothing Then
            Select Case oControlItem.SrcCod
                Case ControlItem.SrcCods.pendent
                    Dim oPnd As DTOPnd = CurrentControlItem.Source

                    If oPnd IsNot Nothing Then
                        Dim oMenu_Pnd As New Menu_Pnd(SelectedItems.First)
                        AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
                        oContextMenu.Items.AddRange(oMenu_Pnd.Range)
                    End If
                Case ControlItem.SrcCods.impagat
                    Dim oImpagat As DTOImpagat = CurrentControlItem.Source

                    If oImpagat IsNot Nothing Then
                        Dim oMenu_Impagat As New Menu_Impagat(oImpagat)
                        AddHandler oMenu_Impagat.AfterUpdate, AddressOf RefreshRequest
                        oContextMenu.Items.AddRange(oMenu_Impagat.Range)
                    End If
            End Select
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add(MenuItem_SelectAll)
        oContextMenu.Items.Add(MenuItem_SelectNone)

        DataGridView1.ContextMenuStrip = oContextMenu

    End Sub

    Private Function MenuItem_SelectAll() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Sel.leccionar"
        oMenuItem.Enabled = False
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                If oControlItem.Checked = False Then
                    oMenuItem.Enabled = True
                    Exit For
                End If
            End If
        Next
        AddHandler oMenuItem.Click, AddressOf Do_SelectAll
        Return oMenuItem
    End Function

    Private Function MenuItem_SelectNone() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desel.leccionar"
        oMenuItem.Enabled = False
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            If oControlItem IsNot Nothing Then
                If oControlItem.Checked = True Then
                    oMenuItem.Enabled = True
                    Exit For
                End If
            End If
        Next
        AddHandler oMenuItem.Click, AddressOf Do_SelectNone
        Return oMenuItem
    End Function

    Private Sub Do_SelectAll()
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            oControlItem.Checked = True
        Next
        DataGridView1.Refresh()
        TextBoxSel.Text = DTOAmt.CurFormatted(Me.SelectedAmt)
        RaiseEvent ItemCheckedChange(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_SelectNone()
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            oControlItem.Checked = False
        Next
        DataGridView1.Refresh()
        TextBoxSel.Text = DTOAmt.CurFormatted(Me.SelectedAmt)
        RaiseEvent ItemCheckedChange(Me, MatEventArgs.Empty)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Checked

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Checked
                Dim oControlItem As ControlItem = DataGridView1.Rows(e.RowIndex).DataBoundItem
                If oControlItem.Checked = True Then
                    e.Value = My.Resources.Checked13
                Else
                    e.Value = My.Resources.UnChecked13
                End If
            Case Cols.Amt
                Try
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem
                    Dim oCur As DTOCur
                    If oControlItem.SrcCod = ControlItem.SrcCods.pendent Then
                        oCur = oControlItem.Source.Amt.Cur
                    Else
                        oCur = oControlItem.Source.pendent.cur
                    End If
                    Dim sFormat As String = ""
                    Select Case oCur.Tag
                        Case "EUR"
                            sFormat = "#,###0.00 €"
                        Case "GBP"
                            sFormat = "£ #,###0.00"
                        Case "USD"
                            sFormat = "$ #,###0.00"
                        Case Else
                            sFormat = "#,###0.00"
                    End Select
                    e.Value = Format(CDbl(e.Value), sFormat)

                Catch ex As Exception

                End Try
        End Select
    End Sub


    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Select Case e.ColumnIndex
            Case Cols.Checked
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                oControlItem.Checked = Not oControlItem.Checked
                DataGridView1.Refresh()
                TextBoxSel.Text = DTOAmt.CurFormatted(Me.SelectedAmt)
                RaiseEvent ItemCheckedChange(Me, New MatEventArgs(oControlItem.Source))
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        'If Not IsDBNull(oRow.Cells(Cols.Xec).Value) Then
        'If CInt(oRow.Cells(Cols.Xec).Value) > 0 Then
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        'End If
        'End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                RaiseEvent ItemCheckedChange(Me, New MatEventArgs(CurrentControlItem.Source))
                SetContextMenu()
            End If
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As Object
        Public Property SrcCod As SrcCods

        Public Property Checked As Boolean
        Public Property Vto As Date
        Public Property Amt As Decimal
        Public Property Eur As Decimal
        Public Property Fra As String
        Public Property Fch As Date
        Public Property Cta As String
        Public Property Obs As String

        Public Enum SrcCods
            pendent
            impagat
        End Enum

        Public Sub New(oPnd As DTOPnd, oCodi As DTOPnd.Codis)
            MyBase.New()
            _Source = oPnd
            _SrcCod = SrcCods.pendent

            With oPnd
                _Checked = False
                _Vto = .Vto
                _Amt = .Amt.Val
                _Eur = .Amt.Eur
                _Fra = .FraNum
                _Fch = .Fch
                _Cta = DTOPgcCta.FullNom(.Cta, Current.Session.Lang)
                _Obs = .Fpg
            End With

            If oPnd.Cod <> oCodi Then
                _Amt = -_Amt
            End If

        End Sub

        Public Sub New(oImpagat As DTOImpagat)
            MyBase.New()
            _Source = oImpagat
            _SrcCod = SrcCods.impagat

            With oImpagat
                _Checked = False
                _Vto = .Csb.Vto
                _Amt = .Nominal.Val
                _Eur = .Nominal.Eur
                '_Fra = .
                _Fch = IIf(.FchSdo = Nothing, .Csb.Csa.Fch, .FchSdo)
                _Cta = DTOPgcCta.FullNom(_CtaImpagats, Current.Session.Lang)
                _Obs = .Obs
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)

    End Class

End Class

