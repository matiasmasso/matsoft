Public Class Xl_Pnds_Select
    Private _ControlItems As New ControlItems
    Protected Shared _CtaImpagats As PgcCta
    Private _Codi As Pnd.Codis = Pnd.Codis.NotSet
    Private _Sum As Amt

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

    Public Shadows Sub Load(value As Pnds, oCodi As Pnd.Codis)
        _Codi = oCodi
        For Each oItem As Pnd In value
            Dim oControlItem As New ControlItem(oItem, _Codi)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public Shadows Sub Load(value As Impagats)
        _CtaImpagats = MaxiSrvr.Current.PgcPlan.Cta(DTOPgcPlan.ctas.impagats)
        For Each oItem As Impagat In value
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public Property Codi() As Pnd.Codis
        Get
            Return _Codi
        End Get
        Set(ByVal Value As Pnd.Codis)
            _Codi = Value
        End Set
    End Property

    Public ReadOnly Property Pnds As Pnds
        Get
            Dim retval As New Pnds
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked And oControlItem.SrcCod = ControlItem.SrcCods.pendent Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property Impagats As Impagats
        Get
            Dim retval As New Impagats
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked And oControlItem.SrcCod = ControlItem.SrcCods.impagat Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Get
    End Property

    Public ReadOnly Property SelectedAmt As Amt
        Get
            Dim retval As New Amt
            For Each oControlItem As ControlItem In _ControlItems
                If oControlItem.Checked Then
                    Select Case oControlItem.SrcCod
                        Case ControlItem.SrcCods.pendent
                            Dim oPnd As Pnd = oControlItem.Source
                            retval.Add(oPnd.Amt)
                        Case ControlItem.SrcCods.impagat
                            Dim oImpagat As Impagat = oControlItem.Source
                            retval.Add(oImpagat.Pendent)
                    End Select
                End If
            Next
            Return retval
        End Get
    End Property


    Public Sub SetCheckedItems(oPndsToCheck As Pnds)
        For Each oControlItem As ControlItem In _ControlItems
            For Each oPnd As Pnd In oPndsToCheck
                Dim pPnd As Pnd = oControlItem.Source
                If pPnd.Id = (oPnd.Id) Then
                    oControlItem.Checked = True
                    Exit For
                End If
            Next
        Next
        TextBoxSel.Text = Me.SelectedAmt.CurFormat
    End Sub

    Private Sub SetTotals()
        Dim oSum As maxisrvr.Amt = GetTotal(Totals.AllChecked)

        Dim s As String = ""
        If oSum.Cur IsNot Nothing Then
            s = oSum.CurFormat
            If oSum.Cur.Id <> "EUR" Then
                s = s & " (" & Format(oSum.Eur, "#,###.00 EUR") & ")"
            End If

        End If
        TextBoxSel.Text = s
        _Sum = oSum
    End Sub

    Private Sub LoadGrid()
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
            With CType(.Columns(Cols.Checked), DataGridViewImageColumn)
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

        TextBoxTot.Text = GetTotal(Totals.All).CurFormat
        TextBoxSel.Text = ""

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Function GetTotal(oTotalMode As Totals)
        Dim retval As New MaxiSrvr.Amt()
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
                        Dim oPnd As Pnd = oControlItem.Source
                        retval.Add(oPnd.Amt)
                    Case ControlItem.SrcCods.impagat
                        Dim oImpagat As Impagat = oControlItem.Source
                        retval.Add(oImpagat.Pendent)
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
                    Dim oPnd As Pnd = oControlItem.Source
                    If Not oPnd.Amt.Cur.Equals(Cur.Eur) Then
                        retval = False
                        Exit For
                    End If
                Case ControlItem.SrcCods.impagat
                    Dim oImpagat As Impagat = oControlItem.Source
                    If Not oImpagat.Pendent.Cur.Equals(Cur.Eur) Then
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

    Private Function SelectedItems() As Pnds
        Dim retval As New Pnds
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
                    Dim oPnd As Pnd = CurrentControlItem.Source

                    If oPnd IsNot Nothing Then
                        Dim oMenu_Pnd As New Menu_Pnd(SelectedItems.First)
                        AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
                        oContextMenu.Items.AddRange(oMenu_Pnd.Range)
                    End If
                Case ControlItem.SrcCods.impagat
                    Dim oImpagat As Impagat = CurrentControlItem.Source

                    If oImpagat IsNot Nothing Then
                        Dim oMenu_Impagat As New Menu_Impagat(oImpagat)
                        AddHandler oMenu_Impagat.AfterUpdate, AddressOf RefreshRequest
                        oContextMenu.Items.AddRange(oMenu_Impagat.Range)
                    End If
            End Select
        End If

        DataGridView1.ContextMenuStrip = oContextMenu

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
                    Dim oCur As MaxiSrvr.Cur
                    If oControlItem.SrcCod = ControlItem.SrcCods.pendent Then
                        oCur = oControlItem.Source.Amt.Cur
                    Else
                        oCur = oControlItem.Source.pendent.cur
                    End If
                    Dim sFormat As String = ""
                    Select Case oCur.Id
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
                TextBoxSel.Text = Me.SelectedAmt.CurFormat
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
            RaiseEvent ItemCheckedChange(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
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

        Public Sub New(oPnd As Pnd, oCodi As Pnd.Codis)
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
                _Cta = .Cta.FullNom
                _Obs = .Fpg
            End With

            If oPnd.Cod <> oCodi Then
                _Amt = -_Amt
            End If

        End Sub

        Public Sub New(oImpagat As Impagat)
            MyBase.New()
            _Source = oImpagat
            _SrcCod = SrcCods.impagat

            With oImpagat
                _Checked = False
                '_Vto = 
                _Amt = .Pendent.Val
                _Eur = .Pendent.Eur
                '_Fra = .
                '_Fch = .Fch
                _Cta = _CtaImpagats.Nom
                _Obs = .Obs
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits System.ComponentModel.BindingList(Of ControlItem)

    End Class

End Class

