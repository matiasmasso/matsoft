

Public Class Frm_Rep_Transfers
    Private _Plan As DTOPgcPlan = BLL.BLLPgcPlan.Current
    Private _Lang As DTOLang = BLL.BLLSession.Current.Lang
    Private mBancTransfer As BancTransfer
    Private mNewTransferCod As NewTransferCods
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDs As DataSet
    Private mAllowEvents As Boolean
    Private mRowTot As DataRow
    Private mDirty As Boolean
    Private mDirtyCell As Boolean

    Private Enum Cols
        Contact
        Eur
        WarnSdo
        IcoWarnSdo
        Abr
        Plan
        CtaId
        CtaNom
        WarnCcc
        IcoWarnCcc
        Ccc
        Txt
    End Enum

    Public Enum NewTransferCods
        NotSet
        Reps
        Staff
    End Enum

    Public WriteOnly Property NewTransferCod() As NewTransferCods
        Set(ByVal value As NewTransferCods)
            mNewTransferCod = value
            LoadBancs()
            Dim DtFch As Date = Today
            Dim sConcepte As String = CurrentBanc.Nom & "-Transferències comisions a representants"
            mBancTransfer = New BancTransfer(BLLBanc.BancToReceiveTransfers, DtFch, sConcepte)
            DateTimePicker1.Value = DtFch
            LoadGrid()

            If mNewTransferCod = NewTransferCods.Reps Then
                TextBoxConcepte.Text = "Comisiones " & BLL.BLLApp.Lang.Mes(DtFch.AddMonths(-1).Month)
                Me.Text = "Transferències a Representants"
            ElseIf mNewTransferCod = NewTransferCods.Staff Then
                TextBoxConcepte.Text = "Nómines " & BLL.BLLApp.Lang.Mes(DtFch.AddMonths(-1).Month)
                Me.Text = "Transferències al Personal"
            End If

            SetButtons()
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property BancTransfer() As BancTransfer
        Set(ByVal value As BancTransfer)
            mBancTransfer = value
            LoadBancs()
            With mBancTransfer
                DateTimePicker1.Value = .Fch
                TextBoxConcepte.Text = .Concepte
                ComboBoxBanc.SelectedItem = .Banc
            End With
            LoadGrid()
            SetTotals()
            SetButtons()
            mAllowEvents = True
        End Set
    End Property


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Txt
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()
        SetTotals()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub LoadGrid()
        Dim SQL As String = ""

        If mBancTransfer.Exists Then
            SQL = "SELECT CCB.ContactGuid, " _
            & "CCB.EUR AS SDO, " _
            & "'' as WARNSDO, " _
            & "CLX.CLX, " _
            & "CCB.PGCPLAN, " _
            & "PgcCta.Id AS CTAID, " _
            & "PgcCta.Id AS CTANOM, " _
            & "'' as WARNCCC, " _
            & "TRANSFER.IBAN, " _
            & "TRANSFER.TXT " _
            & "FROM  CCB INNER JOIN " _
            & "PgcCta ON Ccb.CtaGuid = PgcCta.Guid INNER JOIN " _
            & "TRANSFER ON TRANSFER.emp = CCB.Emp AND TRANSFER.yea = CCB.yea AND TRANSFER.cca = CCB.Cca AND TRANSFER.cta = CCB.cta AND TRANSFER.cli = CCB.cli INNER JOIN " _
            & "CLX ON CCB.EMP=CLX.EMP AND CCB.CLI=CLX.CLI " _
            & "WHERE CCB.CcaGuid ='" & mBancTransfer.Cca.Guid.ToString & "' " _
            & "ORDER BY CCB.LIN"
        Else
            Select Case mNewTransferCod
                Case NewTransferCods.Reps
                    Dim oCta As DTOPgcCta = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.proveidorsEur)
                    SQL = "SELECT CCB.ContactGuid, " _
                     & "SUM(CASE WHEN dh = 2 THEN eur ELSE - eur END) AS SDO, " _
                     & "'' as WARNSDO, " _
                     & "CLIREP.abr, " _
                     & "3 as PGCPLAN, " _
                     & "'" & oCta.Id & "' AS CTAID, " _
                     & "'" & BLL.BLLPgcCta.FullNom(oCta, _Lang) & "' AS CTANOM, " _
                     & "'' as WARNCCC, " _
                     & "'' AS CCC, " _
                     & "'' AS CONCEPTE " _
                     & "FROM  CCB INNER JOIN " _
                     & "CliRep ON CCB.ContactGuid = (CASE WHEN CLIREP.CcxGuid IS NULL THEN CliRep.Guid ELSE CliRep.CcxGuid END) " _
                     & "WHERE CCB.EMP = 1 AND CCB.YEA = " & Today.Year & " AND CCB.CtaGuid = '" & oCta.Guid.ToString & "' " _
                     & "GROUP BY CCB.ContactGuid, CLIREP.abr " _
                     & "ORDER BY CLIREP.abr"
                Case NewTransferCods.Staff
                    Dim oCta As DTOPgcCta = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.PagasTreballadors)
                    SQL = "SELECT CCB.ContactGuid, " _
                    & "SUM(CASE WHEN dh = 2 THEN eur ELSE - eur END) AS SDO, " _
                    & "'' as WARNSDO, " _
                    & "CLISTAFF.abr, " _
                    & "CCB.PGCPLAN, " _
                    & "'" & oCta.Id & "' AS CTAID, " _
                    & "'" & BLL.BLLPgcCta.FullNom(oCta, _Lang) & "' AS CTANOM, " _
                    & "'' as WARNCCC, " _
                    & "'' AS CCC, " _
                    & "'' AS CONCEPTE " _
                    & "FROM  CCB INNER JOIN " _
                    & "CLISTAFF ON CCB.Emp = Clistaff.emp AND CCB.cli = CLISTAFF.cli " _
                    & "WHERE CCB.EMP = 1 AND CCB.YEA = " & Today.Year & " AND CCB.CtaGuid = '" & oCta.Guid.ToString & "' " _
                    & "GROUP BY CCB.ContactGuid, CLISTAFF.abr, CCB.PGCPLAN " _
                    & "ORDER BY CLISTAFF.abr"
            End Select

        End If

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oColSdo As DataColumn = oTb.Columns.Add("ICOWARNSDO", System.Type.GetType("System.Byte[]"))
        oColSdo.SetOrdinal(Cols.IcoWarnSdo)

        Dim oColCcc As DataColumn = oTb.Columns.Add("ICOWARNCCC", System.Type.GetType("System.Byte[]"))
        oColCcc.SetOrdinal(Cols.IcoWarnCcc)

        Dim oRow As DataRow
        For Each oRow In oTb.Rows

            If Not CheckCuadra(oRow) Then
                oRow(Cols.WarnSdo) = "1"
            End If

            If oRow(Cols.Ccc) = "" Then
                Dim oIban As DTOIban = GetIban(oRow)
                If oIban Is Nothing Then
                    oRow(Cols.WarnCcc) = "1"
                Else
                    oRow(Cols.Ccc) = oIban.Digits
                    If Not BLL.BLLIban.Validated(oIban.Digits) Then oRow(Cols.WarnCcc) = "1"
                End If
            End If

            oRow(Cols.Txt) = TextBoxConcepte.Text
        Next

        mRowTot = oTb.NewRow
        'oTb.Rows.InsertAt(mRowTot, 0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Rows(0)
                '.DefaultCellStyle.BackColor = Color.LightBlue
            End With

            With .Columns(Cols.Contact)
                .Visible = False
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.WarnSdo)
                .Visible = False
            End With
            With .Columns(Cols.Plan)
                .Visible = False
            End With
            With .Columns(Cols.IcoWarnSdo)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .ReadOnly = True
            End With
            With .Columns(Cols.Abr)
                .HeaderText = "representant"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(Cols.CtaId)
                .Visible = False
            End With
            With .Columns(Cols.CtaNom)
                .HeaderText = "compte"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.WarnCcc)
                .Visible = False
            End With
            With .Columns(Cols.IcoWarnCcc)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .ReadOnly = True
            End With
            With .Columns(Cols.Ccc)
                .HeaderText = "IBAN"
                .Width = 170
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With
    End Sub

    Private Function GetIban(ByVal oRow As DataRow) As DTOIban
        Dim oIban As DTOIban = Nothing
        Select Case mNewTransferCod
            Case NewTransferCods.Reps
                Dim oRep As New Rep(CType(oRow(Cols.Contact), Guid))
                If oRep.Ccx IsNot Nothing Then
                    If oRep.Ccx.Exists Then
                        oIban = oRep.Ccx.Proveidor.FormaDePago.Iban
                    Else
                        oIban = oRep.Iban
                    End If
                Else
                    oIban = oRep.Iban
                End If
            Case NewTransferCods.Staff
                Dim oStaff As New Staff(CType(oRow(Cols.Contact), Guid))
                oIban = oStaff.Iban
        End Select
        Return oIban
    End Function

    Private Function CheckCuadra(ByVal oRow As DataRow) As Boolean
        Dim retval As Boolean
        Dim oCcd As Ccd
        Select Case mNewTransferCod
            Case NewTransferCods.Reps
                Dim iYea As Integer = Today.Year
                Dim oCta430 As DTOPgcCta = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Clients)
                Dim oRep As New Rep(CType(oRow(Cols.Contact), Guid))
                retval = (oRep.LastLiqEur = oRow(Cols.Eur))
                oCcd = New Ccd(oRep, iYea, New PgcCta(oCta430.Guid))
                If oCcd.Saldo(Today).Eur <> 0 Then CheckCuadra = False
            Case NewTransferCods.Staff
                Dim oStaff As New Staff(CType(oRow(Cols.Contact), Guid))
                Dim oLiqStored As DTOAmt = oStaff.Liquid
                retval = (oLiqStored.Eur = CDbl(oRow(Cols.Eur)))
        End Select
        Return retval
    End Function

    Private Sub SetTotals()
        Dim DblTot As Decimal = 0
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            If Not IsDBNull(oRow.Cells(Cols.Eur).Value) Then
                DblTot += oRow.Cells(Cols.Eur).Value
            End If
        Next
        LabelTot.Text = "total: " & Format(DblTot, "#,##0.00 €")
    End Sub


    Private Sub LoadBancs()
        Dim SQL As String = "SELECT Guid,Abr FROM CliBnc WHERE EMP=@Emp AND ACTIU=1 ORDER BY ORD"

        Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@Emp", BLLApp.Emp.Id)
        Dim i As Integer = -1
        Dim SelIdx As Integer
        Dim oDefaultBanc As New Banc(New Guid(BLL.BLLDefault.EmpValue(DTODefault.Codis.BancNominaTransfers)))
        Dim oBancs As New Bancs

        Do While oDrd.Read
            Dim oBanc As New Banc(oDrd("Guid"))
            oBanc.Abr = oDrd("Abr").ToString
            oBancs.Add(oBanc)
            i += 1
            If oBanc.Equals(oDefaultBanc) Then SelIdx = i
        Loop
        oDrd.Close()

        With ComboBoxBanc
            .DataSource = oBancs
            .DisplayMember = "Abr"
            .SelectedIndex = SelIdx
        End With
    End Sub


    Private Function CurrentContact() As Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Contact).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Contact).Value
                oContact = New Contact(oGuid)
            End If
        End If
        Return oContact
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentContact()

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs as New List(Of exception)
        If Save( exs) Then
            SaveFile()
            Me.Close()
            mDirty = False
        Else
            MsgBox("error al desar la transferencia:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub

    Private Sub FitxerToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FitxerToolStripButton.Click
        SaveFile()
    End Sub

    Private Sub SaveFile()
        Dim exs as New List(Of exception)
        Dim oAeb341 As AEB341 = Nothing
        If BLL_BankTransfer.AEB341(mBancTransfer, oAeb341, exs) Then
            Dim oDlg As New System.Windows.Forms.SaveFileDialog
            With oDlg
                .FileName = BLL_BankTransfer.DefaultFilename(mBancTransfer)
                .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                .FilterIndex = 1
                If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    oAeb341.Save(.FileName)
                End If
            End With
        Else
            UIHelper.WarnError( exs, "els següents comptes no han passat la validació:")
        End If
    End Sub

    Private Function CurrentBanc() As Banc
        Dim retval As Banc = Nothing
        If ComboBoxBanc.SelectedIndex >= 0 Then
            retval = ComboBoxBanc.SelectedItem
        End If
        Return retval
    End Function

    Private Function Save(ByRef exs as list(Of Exception)) As Boolean
        Dim oRow As DataRow
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oContact As Contact
        Dim oCta As DTOPgcCta
        Dim oIban As DTOIban
        Dim oAmt As DTOAmt
        Dim sConcepte As String

        With mBancTransfer
            .Fch = DateTimePicker1.Value
            .Concepte = TextBoxConcepte.Text
            .Banc = CurrentBanc()

            For Each oRow In oTb.Rows
                If oRow(Cols.Eur) > 0 Then
                    Dim oContactGuid As Guid = oRow(Cols.Contact)
                    oContact = New Contact(oContactGuid)
                    oCta = BLL.BLLPgcCta.FromId(_Plan, oRow(Cols.CtaId))
                    oIban = BLL.BLLIban.FromDigits(oRow(Cols.Ccc).ToString)
                    oAmt = BLLApp.GetAmt(CDec(oRow(Cols.Eur)))
                    sConcepte = oRow(Cols.Txt)
                    Dim oCcb As New Ccb(New PgcCta(oCta.Guid), oContact, oAmt, DTOCcb.DhEnum.Debe)
                    Dim oItm As New BankTransferItm(oCcb, oIban.Digits)
                    .Itms.Add(oItm)
                End If
            Next

        End With

        Dim retval As Boolean = mBancTransfer.Update( exs)
        Return retval
    End Function

    Private Sub SetButtons()
        If mDirty Then
            FitxerToolStripButton.Enabled = False
            ButtonOk.Enabled = True
        Else
            FitxerToolStripButton.Enabled = (mBancTransfer.Exists)
            ButtonOk.Enabled = False
        End If

        'vigila que els IBAN estiguin bé
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If oRow.Cells(Cols.Eur).Value <> 0 Then
                If oRow.Cells(Cols.WarnCcc).Value = "1" Then
                    ButtonOk.Enabled = False
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBoxConcepte.TextChanged, _
      ComboBoxBanc.SelectedIndexChanged
        If mAllowEvents Then
            mDirty = True
            SetButtons()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoWarnSdo
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(Cols.WarnSdo).Value) Then
                    e.Value = My.Resources.empty
                Else
                    Select Case oRow.Cells(Cols.WarnSdo).Value
                        Case "1"
                            e.Value = My.Resources.clip
                        Case Else
                            e.Value = My.Resources.empty
                    End Select
                End If
            Case Cols.IcoWarnCcc
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(Cols.WarnSdo).Value) Then
                    e.Value = My.Resources.empty
                Else
                    Select Case oRow.Cells(Cols.WarnCcc).Value
                        Case "1"
                            e.Value = My.Resources.warn
                        Case Else
                            e.Value = My.Resources.empty
                    End Select
                End If
        End Select
    End Sub

    Private Sub DataGridView1_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        mDirtyCell = True
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        If mDirtyCell Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Select Case e.ColumnIndex
                Case Cols.CtaNom
                    Dim iYea As Integer = DateTimePicker1.Value.Year
                    Dim sCtaNom As String = oRow.Cells(Cols.CtaNom).Value
                    Dim sKey As String
                    Dim iBlank As Integer = sCtaNom.IndexOf(" ")
                    If iBlank >= 0 Then
                        sKey = sCtaNom.Substring(1, iBlank - 1)
                    Else
                        sKey = sCtaNom
                    End If
                    If sKey = oRow.Cells(Cols.CtaId).Value Then Exit Sub

                    Dim oCta As DTOPgcCta = Finder.FindCta(_Plan, sKey)
                    If oCta IsNot Nothing Then
                        oRow.Cells(Cols.CtaId).Value = oCta.Id
                        oRow.Cells(Cols.CtaNom).Value = BLL.BLLPgcCta.FullNom(oCta, _Lang)
                    End If

                Case Cols.Ccc
                    Dim sDigits As String = oRow.Cells(Cols.Ccc).Value
                    oRow.Cells(Cols.WarnCcc).Value = IIf(BLL.BLLIban.Validated(sDigits), "", "1")

                Case Cols.Eur
                    SetTotals()
            End Select
            mDirty = True
            SetButtons()
        End If

    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Eur
                SetTotals()
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Eur
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oFrm As New Frm_Contact(CurrentContact)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub


    Private Sub DataGridView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'ho crida DataGridView1_EditingControlShowing
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Eur
                If e.KeyChar = "." Then
                    e.KeyChar = ","
                End If
        End Select
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        'fa que funcioni KeyPress per DataGridViews
        If TypeOf e.Control Is TextBox Then
            Dim oControl As TextBox = CType(e.Control, TextBox)
            AddHandler oControl.KeyPress, AddressOf DataGridView1_KeyPress
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If mAllowEvents Then
            mDirty = True
            SetButtons()
            _Plan = BLL.BLLPgcPlan.FromYear(DateTimePicker1.Value.Year)
        End If
    End Sub
End Class