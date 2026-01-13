

Public Class Frm_Contracts_Old
    Private mEmp As DTOEmp = BLL.BLLApp.Emp
    Private mSelectionMode As BLL.Defaults.SelectionModes
    Private mDefaultContract As DTOContract = Nothing
    Private mAllowevents As Boolean = False

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols1
        Guid
        Nom
    End Enum

    Private Enum Cols2
        Guid
        Hash
        Ico
        CliNom
        Text
        Num
        fchFrom
        FchTo
        Caducat
    End Enum

    Public Sub New(Optional ByVal oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse, Optional ByVal oDefaultContract As DTOContract = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mSelectionMode = oSelectionMode
        mDefaultContract = oDefaultContract
        CheckBoxInclouPrivats.Visible = BLL.BLLSession.Current.User.Rol.IsAdmin
        LoadCodis()
        SetContextMenu1()
        LoadGridContracts()
        SetContextMenu2()
        mAllowevents = True
    End Sub



    Private Sub LoadCodis()
        Dim SQL As String = "SELECT Guid,NOM FROM CONTRACT_CODIS ORDER BY NOM"
        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols1.Guid)
                .Visible = False
            End With
            With .Columns(Cols1.Nom)
                .HeaderText = "codi"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Function CurrentCodi() As DTOContractCodi
        Dim oRetVal As DTOContractCodi = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow Is Nothing Then oRow = DataGridView1.Rows(0)
        If oRow IsNot Nothing Then
            oRetVal = New DTOContractCodi(oRow.Cells(Cols1.Guid).Value)
            oRetVal.Nom = oRow.Cells(Cols1.Nom).Value
        End If
        Return oRetVal
    End Function

    Private Sub LoadGridContracts()
        Dim SQL As String = "SELECT CONTRACT.GUID, CONTRACT.HASH, " _
        & "CLX.CLX, CONTRACT.NOM, CONTRACT.NUM, CONTRACT.FCHFROM, CONTRACT.FCHTO, " _
        & "(CASE WHEN CONTRACT.FCHTO < GETDATE() THEN 1 ELSE 0 END) AS CADUCAT " _
        & "FROM CONTRACT INNER JOIN " _
        & "CLX ON CONTRACT.ContactGuid=CLX.Guid " _
        & "WHERE CONTRACT.EMP=@EMP AND CONTRACT.CODIGuid='" & CurrentCodi.Guid.ToString & "' "

        If Not CheckBoxInclouPrivats.Checked Then
            SQL = SQL & " AND CONTRACT.PRIVAT=0 "
        End If
        If CheckBoxFilter.Checked Then
            SQL = SQL & " AND @FCH BETWEEN CONTRACT.FCHFROM AND CONTRACT.FCHTO "
        End If


        SQL = SQL & "ORDER BY CADUCAT, CONTRACT.FCHFROM DESC, CONTRACT.FCHTO DESC"


        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@EMP", mEmp.Id, "@FCH", DateTimePicker1.Value)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix icono PDF
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols2.Ico)

        mAllowevents = False
        With DataGridView2
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols2.Guid)
                .Visible = False
            End With
            With .Columns(Cols2.Hash)
                .Visible = False
            End With
            With .Columns(Cols2.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols2.CliNom)
                .HeaderText = "contractant"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols2.Text)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols2.Num)
                .HeaderText = "contracte"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols2.fchFrom)
                .HeaderText = "inici"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols2.FchTo)
                .HeaderText = "caducitat"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols2.Caducat)
                .Visible = False
            End With
        End With
        mAllowevents = True
    End Sub

    Private Function CurrentContract() As DTOContract
        Dim oRetVal As DTOContract = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            oRetVal = New DTOContract(oRow.Cells(Cols2.Guid).Value)
            If Not IsDBNull(oRow.Cells(Cols2.Hash).Value) Then
                Dim sHash As String = oRow.Cells(Cols2.Hash).Value
                If sHash > "" Then
                    oRetVal.DocFile = New DTODocFile(sHash)
                End If
            End If
        End If
        Return oRetVal
    End Function

    Private Sub DataGridView2_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        Select Case e.ColumnIndex
            Case Cols2.Ico
                Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(Cols2.Hash).Value) Then
                    e.Value = My.Resources.empty
                Else
                    e.Value = My.Resources.pdf
                End If
        End Select
    End Sub

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Zoom_Contract(sender, e)
    End Sub

    Private Sub DataGridView2_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView2.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
        If (oRow.Cells(Cols2.Caducat).Value = 1) Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom_Codi(sender, e)
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowevents Then
            SetContextMenu1()
            LoadGridContracts()
            SetContextMenu2()
        End If
    End Sub

    Private Sub SetContextMenu1()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom_Codi)
        oMenuItem.Enabled = (CurrentCodi() IsNot Nothing)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("afegir nou codi", Nothing, AddressOf AddNew_Codi)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("afegir nou contracte", Nothing, AddressOf AddNew_Contract)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowevents Then
            SetContextMenu2()
        End If
    End Sub

    Private Sub SetContextMenu2()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing
        Dim BlExists As Boolean = (CurrentContract() IsNot Nothing)
        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf Zoom_Contract)
        oMenuItem.Enabled = (BlExists)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("pdf", Nothing, AddressOf Pdf_Contract)
        oMenuItem.Enabled = (BlExists)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("copiar enllaç", Nothing, AddressOf CopyLink_Contract)
        oMenuItem.Enabled = (BlExists)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("afegir", Nothing, AddressOf AddNew_Contract)
        oContextMenu.Items.Add(oMenuItem)


        DataGridView2.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Zoom_Codi(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim oCodi As DTOContractCodi = CurrentCodi()
        If oCodi IsNot Nothing Then

            If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
                RaiseEvent AfterSelect(oCodi, EventArgs.Empty)
                Me.Close()
            Else
                Dim oFrm As New Frm_ContractCodi(oCodi)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest_Codis
                oFrm.Show()
            End If

        End If
    End Sub

    Private Sub AddNew_Codi(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCodi As New DTOContractCodi()
        Dim oFrm As New Frm_ContractCodi(oCodi)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest_Codis
        oFrm.Show()
    End Sub

    Private Sub Pdf_Contract(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContract As DTOContract = CurrentContract()
        'UIHelper.show
        UIHelper.ShowStream(oContract.DocFile)
    End Sub

    Private Sub CopyLink_Contract(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            If IsDBNull(oRow.Cells(Cols2.Hash).Value) Then
                MsgBox("no hi ha entrat cap document en aquest contracte", MsgBoxStyle.Information, "MAT.NET")
            Else
                Dim sHash As String = oRow.Cells(Cols2.Hash).Value
                Dim oDocFile As New DTODocFile(sHash)
                UIHelper.CopyLink(oDocFile)
            End If
        End If
    End Sub

    Private Sub Zoom_Contract(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContract As DTOContract = CurrentContract()
        If oContract IsNot Nothing Then


            If mSelectionMode = BLL.Defaults.SelectionModes.Selection Then
                RaiseEvent AfterSelect(oContract, EventArgs.Empty)
                Me.Close()
            Else
                Dim oFrm As New Frm_Contract(oContract)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest_Contracts
                oFrm.Show()
            End If

        End If
    End Sub

    Private Sub AddNew_Contract(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oContract As New DTOContract
        oContract.Codi = CurrentCodi()
        Dim oFrm As New Frm_Contract(oContract)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest_Contracts
        oFrm.Show()
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSheet As DTOExcelSheet = UIHelper.GetExcelFromDataGridView(DataGridView2)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub RefreshRequest_Codis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols1.Nom
        Dim oGrid As DataGridView = DataGridView1
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadCodis()

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

    Private Sub RefreshRequest_Contracts()
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols2.Text
        Dim oGrid As DataGridView = DataGridView2
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridContracts()

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

    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxFilter.CheckedChanged,
         CheckBoxInclouPrivats.CheckedChanged

        If mAllowevents Then
            LoadGridContracts()
        End If
    End Sub

End Class