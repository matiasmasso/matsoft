

Public Class Frm_Nuk_Desadvs
    Private mRoche As Contact = Roche.Contact
    Private mAllowEvents As Boolean = False

    Private Enum Cols
        Desadv
        alb
        fch
        fra
        eur
        nom
        guid
    End Enum

    Private Sub Frm_Nuk_Desadvs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ALB.DelegatNum, ALB.alb, ALB.fch, ALB.fra, ALB.eur, ALB.nom, ALB.Guid " _
        & "FROM            ALB INNER JOIN " _
        & "CIT ON ALB.CitNum = CIT.Id " _
        & "WHERE ALB.DelegatContact = @ROCHE AND ALB.Cod = 2 " _
        & "ORDER BY ALB.DelegatNum DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mRoche.Emp.Id, "@ROCHE", mRoche.Id)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.guid)
                .Visible = False
            End With
            With .Columns(Cols.Desadv)
                .HeaderText = "alb ROCHE"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.alb)
                .HeaderText = "alb M+O"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.fra)
                .HeaderText = "factura"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.eur)
                .HeaderText = "Import"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.nom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With

        SetContextMenu()
        mAllowEvents = True
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow

        If oRow IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("copiar enllaç factura", Nothing, AddressOf CopyFraLink)
            oMenuItem.Enabled = (oRow.Cells(Cols.fra).Value > 0)
            oContextMenu.Items.Add(oMenuItem)

            Dim DtFch As Date = oRow.Cells(Cols.fch).Value
            Dim iYea As Integer = DtFch.Year
            Dim iAlb As Integer = oRow.Cells(Cols.alb).Value
            Dim oAlb As Alb = MaxiSrvr.Alb.FromNum(mRoche.Emp, iYea, iAlb)
            oMenuItem = New ToolStripMenuItem("albará...")
            oContextMenu.Items.Add(oMenuItem)

            Dim oMenu_Alb As New Menu_Alb(oAlb)
            'AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu_Alb.Range)

        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mallowevents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub CopyFraLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim DtFch As Date = oRow.Cells(Cols.fch).Value
        Dim iYea As Integer = DtFch.Year
        Dim iFra As Integer = oRow.Cells(Cols.fra).Value
        Dim oFra As Fra = Fra.FromNum(mRoche.Emp, iYea, iFra)
        Clipboard.SetDataObject(BLL.BLLDocFile.DownloadUrl(oFra.Cca.DocFile, True), True)
        MsgBox("copiat enllaç a factura " & oFra.Id)
    End Sub
End Class