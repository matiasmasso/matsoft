Public Class Xl_Extracte
    Private _DataSource As Extracte
    Private _ControlItems As ControlItems
    Private _SelectionMode As bll.dEFAULTS.SelectionModes
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event requestToRefresh(sender As Object, e As EventArgs)

    Private Enum Cols
        Doc
        Id
        Fch
        Txt
        Deb
        Hab
        Sdo
    End Enum

    Public Shadows Sub Load(oDataSource As Extracte, Optional oSelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _DataSource = oDataSource
        _SelectionMode = oSelectionMode
        LoadGrid()
    End Sub

    Public Function SelectedItem() As Ccb
        Dim oControlItem As ControlItem = CurrentItem()
        Dim retval As Ccb = oControlItem.Source
        Return retval
    End Function

    Public Function SelectedItems() As Ccbs
        Dim retval As New Ccbs
        For Each oControlItem As ControlItem In SelectedControlItems()
            retval.Add(oControlItem.Source)
        Next
        Return retval
    End Function

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next
        Return retval
    End Function

    Private Function CurrentCcas() As Ccas
        Dim retval As New Ccas
        If DataGridView1.SelectedRows.Count > 0 Then
            For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                retval.Add(oControlItem.Source.Cca)
            Next
        Else
            Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source.Cca)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        If SelectedControlItems.Count > 1 Then
            Dim DcSuma As Decimal = SelectedControlItems.Suma
            oMenuItem = New ToolStripMenuItem("total " & Format(DcSuma, "#,###.00"))
            oContextMenu.Items.Add(oMenuItem)
        End If

        Try
            Dim oCcas As Ccas = CurrentCcas()

            If oCcas.Count > 0 Then
                Dim oMenu_Cca As New Menu_Cca(oCcas)
                AddHandler oMenu_Cca.AfterUpdate, AddressOf onAfterUpdate
                oContextMenu.Items.AddRange(oMenu_Cca.Range)
            End If

            oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf DoExcel)
            oContextMenu.Items.Add("Cuadra", Nothing, AddressOf DoCuadra)
            oContextMenu.Items.Add("Canvia el compte", Nothing, AddressOf DoSwitchContact)

            oMenuItem = New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequest)
            oContextMenu.Items.Add(oMenuItem)
        Catch ex As Exception
            BLL.MailHelper.MailErr("error al contextmenu de Xl_ccdextracte" & vbCrLf & ex.Message & vbCrLf & ex.StackTrace)
        End Try

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow Is Nothing Then
            If DataGridView1.Rows.Count > 0 Then
                oRow = DataGridView1.Rows(0)
                DataGridView1.CurrentCell = oRow.Cells(Cols.Id)
            End If
        Else
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs)
        If _AllowEvents Then
            Dim oControlItem As ControlItem = CurrentItem()
            Dim oCcb As Ccb = oControlItem.Source
            Dim oEventArgs As New MatEventArgs(oCcb)
            RaiseEvent onItemSelected(Me, oEventArgs)
            SetContextMenu()
        End If
    End Sub

    Private Sub LoadGrid()
        _AllowEvents = False

        _ControlItems = New ControlItems

        Dim DcSdo As Decimal
        For Each oCcb As Ccb In _DataSource.Ccbs

            Select Case oCcb.Cta.Act
                Case PgcCta.Acts.deutora
                    DcSdo += IIf(oCcb.Dh = DTOCcb.DhEnum.Debe, oCcb.Amt.Eur, -oCcb.Amt.Eur)
                Case PgcCta.Acts.creditora
                    DcSdo += IIf(oCcb.Dh = DTOCcb.DhEnum.Haber, oCcb.Amt.Eur, -oCcb.Amt.Eur)
            End Select

            Dim oControlItem As New ControlItem(oCcb, DcSdo)
            _ControlItems.Add(oControlItem)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With

            .ReadOnly = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .RowHeadersVisible = False
            .ColumnHeadersVisible = True
            .AutoGenerateColumns = False
            .Columns.Clear()
            .DataSource = _ControlItems

            .Columns.Add(New DataGridViewImageColumn)
            With .Columns(Cols.Doc)
                .DataPropertyName = "DocExists"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.NullValue = Nothing
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Id)
                .DataPropertyName = "Id"
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .DataPropertyName = "Fch"
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Txt)
                .DataPropertyName = "Txt"
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Deb)
                .DataPropertyName = "Deb"
                .HeaderText = "deure"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Hab)
                .DataPropertyName = "Hab"
                .HeaderText = "haver"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Sdo)
                .DataPropertyName = "Sdo"
                .HeaderText = "saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

        End With

        Dim iVisibleRowsCount As Integer = DataGridView1.DisplayedRowCount(False)
        If iVisibleRowsCount < _ControlItems.Count Then
            DataGridView1.FirstDisplayedScrollingRowIndex = _ControlItems.Count - iVisibleRowsCount
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Doc
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                e.Value = IIf(oControlItem.Doc, My.Resources.pdf, Nothing)
        End Select
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent requestToRefresh(Me, e)
    End Sub


    Private Sub onAfterUpdate(ByVal sender As Object, ByVal e As EventArgs)
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iSelectedRow As Integer = DataGridView1.CurrentRow.Index
            RefreshRequest(sender, e)
            LoadGrid()
            Select Case DataGridView1.Rows.Count
                Case Is > iSelectedRow
                    DataGridView1.CurrentCell = DataGridView1.Rows(iSelectedRow).Cells(Cols.Txt)
                Case 0
                Case Else
                    DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(Cols.Txt)
            End Select
        End If
    End Sub

    Private Sub DoCuadra(sender As Object, e As System.EventArgs)
        Dim oCcbs As Ccbs = _ControlItems.PendentDeCuadrar
        MatExcel.Extracte(oCcbs).Visible = True
    End Sub

    Private Sub DoExcel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oExcelSheet As DTOExcelSheet = _DataSource.ExcelSheet
        UIHelper.ShowExcel(oExcelSheet)
    End Sub


    Private Sub DoSwitchContact(sender As Object, e As System.EventArgs)
        Dim sSearchKey As String = InputBox("canvia els assentaments seleccionats de titular" & vbCrLf & "entrar el nou titular:")
        If sSearchKey > "" Then
            Dim oEmp as DTOEmp = _DataSource.Exercici.Emp
            Dim oNewContact As Contact = Finder.FindContact(oEmp, sSearchKey)
            If oNewContact Is Nothing Then
                MsgBox("no s'ha trobat cap contacte per '" & sSearchKey & "'", MsgBoxStyle.Exclamation)
            Else
                Dim iCount As Integer
                Dim oPreviousContact As Contact = _DataSource.Contact
                Dim oCcbs As Ccbs = SelectedItems()
                For Each oItem As Ccb In oCcbs
                    Dim oCca As Cca = oItem.Cca
                    For Each oCcb As Ccb In oCca.ccbs
                        If oPreviousContact.Equals(oCcb.Contact) Then
                            oCcb.Contact = oNewContact
                        End If
                    Next
                    Dim exs as New List(Of exception)
                    If oCca.Update( exs) Then
                        iCount += 1
                    Else
                        MsgBox("error al desar assentament " & oCca.Id & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
                Next
                MsgBox(iCount.ToString & " assentaments de" & vbCrLf & oPreviousContact.Clx & vbCrLf & "canviats a" & vbCrLf & oNewContact.Clx, MsgBoxStyle.Information)
                RaiseEvent requestToRefresh(Me, EventArgs.Empty)
            End If
        End If
    End Sub


    Protected Class ControlItem
        Public Property Source As Ccb
        Public Property PartidaQueSalda As ControlItem
        Public Property Doc As Boolean
        Public Property Id As Integer
        Public Property Fch As Date
        Public Property Txt As String
        Public Property Deb As Decimal
        Public Property Hab As Decimal
        Public Property Sdo As Decimal

        Public Sub New(oCcb As Ccb, DcSdo As Decimal)
            MyBase.New()
            _Source = oCcb
            _Doc = oCcb.Cca.DocExists
            _Id = oCcb.Cca.Id
            _Fch = oCcb.Cca.fch
            _Txt = oCcb.Cca.Txt
            _Deb = IIf(oCcb.Dh = DTOCcb.DhEnum.Debe, oCcb.Amt.Eur, 0)
            _Hab = IIf(oCcb.Dh = DTOCcb.DhEnum.Haber, oCcb.Amt.Eur, 0)
            _Sdo = DcSdo
        End Sub

        Public Function Import() As Decimal
            Dim retval As Decimal
            If _Source.Cta.Act = PgcCta.Acts.deutora Then
                retval += _Deb - _Hab
            Else
                retval += _Hab - _Deb
            End If
            Return retval
        End Function
    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)

        Public Function Suma() As Decimal
            Dim retval As Decimal = 0
            For Each oControlItem As ControlItem In Me.Items
                retval += oControlItem.Import
            Next
            Return retval
        End Function

        Public Function PendentDeCuadrar() As Ccbs
            Dim oSource As ControlItems = DesdeDarrerSaldoZero()
            For i As Integer = 0 To oSource.Items.Count - 1
                If oSource.Items(i).PartidaQueSalda Is Nothing Then
                    For j = i + 1 To oSource.Items.Count - 2
                        If oSource.Items(j).PartidaQueSalda Is Nothing Then
                            If oSource.Items(i).Import = -oSource.Items(j).Import Then
                                oSource.Items(i).PartidaQueSalda = oSource.Items(j)
                                oSource.Items(j).PartidaQueSalda = oSource.Items(i)
                            End If
                        End If
                    Next
                End If
            Next

            Dim retval As New Ccbs
            For Each oControlItem In oSource.Items
                If oControlItem.PartidaQueSalda Is Nothing Then
                    retval.Add(oControlItem.Source)
                End If
            Next
            Return retval
        End Function

        Public Function DesdeDarrerSaldoZero() As ControlItems
            'retorna l'extracte desde que es va saldar la darrera vegada. Ideal per cuadrar comptes
            Dim retval As New ControlItems
            Dim i, j As Integer
            For i = Me.Items.Count - 1 To 0 Step -1
                If Me.Items(i).Sdo = 0 Then Exit For
            Next

            For j = i + 1 To Me.Items.Count - 1
                retval.Add(Me.Items(j))
            Next
            Return retval
        End Function
    End Class

    Private Sub DataGridView1_CellMouseMove(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Dim oCcb As Ccb = oControlItem.Source
            DataGridView1.DoDragDrop(oCcb, DragDropEffects.Copy)
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        If _SelectionMode = BLL.Defaults.SelectionModes.Selection Then
            RaiseEvent onItemSelected(Me, New MatEventArgs(CurrentItem.Source))
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDown
        Dim hitRowIndex As Integer = DataGridView1.HitTest(e.X, e.Y).RowIndex
        If hitRowIndex >= 0 Then
            If ((Not DataGridView1.SelectedRows.Contains(DataGridView1.Rows(hitRowIndex))) Or ((ModifierKeys & Keys.Control) <> Keys.None)) Then
                Me.OnMouseDown(e)
            End If
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged1(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class
