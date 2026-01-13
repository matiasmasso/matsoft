

Public Class Frm_Impagats2
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.impagats)
    Private mAllowEvents As Boolean

    Private Enum Cols1
        LastVto
        Ico
        NoAsnef
        IcoCod
        Cli
        Clx
        Sdo
        Nominal
        Despeses
        aCompte
        Pendent
        LastMem
    End Enum

    Private Enum Cols2
        Guid
        vto
        Nominal
        Despeses
        txt
    End Enum

    Private Enum Icons
        Empty
        NoCuadra
        NoAsnef
    End Enum

    Private Sub Frm_Impagats2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid1()
        If DataGridView1.Rows.Count > 0 Then DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(Cols1.Clx)
        SetContextMenu1()
        LoadGrid2()
        SetContextMenu2()
        LoadMems()
    End Sub

    Private Sub LoadGrid1()
        Dim oDs As DataSet = Asnef.GetDataSource()
        Dim oTb As DataTable = oDs.Tables(0)

        Dim DcTot As Decimal = 0
        For Each oRow As DataRow In oTb.Rows
            'oRow("PENDENT") = oRow("NOMINAL") + oRow("DESPESES") - oRow("ACOMPTE")
            If oRow("SALDO") = oRow("NOMINAL") Then
                If CBool(oRow("NOASNEF")) Then
                    oRow("ICOCOD") = Icons.NoAsnef
                End If
            Else
                oRow("ICOCOD") = Icons.NoCuadra
            End If
            DcTot += oRow("SALDO")
        Next

        Dim sTitle As String = oTb.Rows.Count & " IMPAGATS " & Format(DcTot, "#,##0.00 €")
        Me.Text = sTitle

        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols1.Ico)

        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols1.LastVto)
                .HeaderText = "ult.vto"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols1.Ico)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols1.NoAsnef)
                .Visible = False
            End With
            With .Columns(Cols1.IcoCod)
                .Visible = False
            End With
            With .Columns(Cols1.Cli)
                .Visible = False
            End With
            With .Columns(Cols1.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols1.Sdo)
                .HeaderText = "saldo"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.Nominal)
                .Visible = False
            End With
            With .Columns(Cols1.Despeses)
                .HeaderText = "despeses"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.aCompte)
                .HeaderText = "a compte"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.Pendent)
                .HeaderText = "pendent"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols1.LastMem)
                .Visible = False
            End With
        End With
        mAllowEvents = True
    End Sub

    Public Sub LoadGrid2()

        Dim oCli As Contact = CurrentCli()
        If oCli IsNot Nothing Then
            Dim SQL As String = "SELECT IMPAGATS.Guid, CSB.vto, CSB.eur, IMPAGATS.Gastos, CSB.txt " _
            & "FROM  IMPAGATS INNER JOIN " _
            & "CSB ON IMPAGATS.Emp = CSB.Emp AND IMPAGATS.Yea = CSB.yea AND IMPAGATS.Csa = CSB.Csb AND IMPAGATS.Csb = CSB.Doc " _
            & "WHERE CSB.Emp =@EMP AND CSB.cli =@CLI AND IMPAGATS.Status <> 3 " _
            & "ORDER BY CSB.VTO"
            Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@EMP", mEmp.Id, "@CLI", oCli.Id)
            Dim oTb As DataTable = oDs.Tables(0)

            Dim xEur As Decimal
            Dim xGts As Decimal
            Dim oRow As DataRow = Nothing
            For Each oRow In oTb.Rows
                xEur += oRow("EUR")
                xGts += oRow("Gastos")
            Next
            oRow = oTb.NewRow
            oRow("EUR") = xEur
            oRow("Gastos") = xGts
            oTb.Rows.Add(oRow)

            mAllowEvents = False
            With DataGridView2
                .RowTemplate.Height = .Font.Height * 1.3
                .DataSource = oTb
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .ColumnHeadersVisible = True
                .RowHeadersVisible = False
                .MultiSelect = True
                .AllowUserToResizeRows = False
                With .Columns(Cols2.Guid)
                    .Visible = False
                End With
                With .Columns(Cols2.vto)
                    .HeaderText = "venciment"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 65
                    .DefaultCellStyle.Format = "dd/MM/yy"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                End With
                With .Columns(Cols2.Nominal)
                    .HeaderText = "nominal"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols2.Despeses)
                    .HeaderText = "despeses"
                    .Width = 70
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                With .Columns(Cols2.txt)
                    .HeaderText = "concepte"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End With
                .Rows(.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightGray
            End With

        End If
        mAllowEvents = True

    End Sub

    Private Sub LoadMems()
        Dim oMems As New List(Of DTOMem)
        Dim oCli As Contact = CurrentCli()
        If oCli IsNot Nothing Then
            Dim oContact As New DTOContact(CurrentCli.Guid)
            oMems = BLL.BLLMems.All(oContact, DTOMem.Cods.Impagos)
        End If
        Xl_Mems1.Load(oMems)
    End Sub

    Private Function CurrentCli() As Contact
        Dim RetVal As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            RetVal = MaxiSrvr.Contact.FromNum(mEmp, oRow.Cells(Cols1.Cli).Value)
        End If
        Return RetVal
    End Function


    Private Function CurrentImpagat() As Impagat
        Dim RetVal As Impagat = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            RetVal = New Impagat(New Guid(oRow.Cells(Cols2.Guid).Value.ToString))
        End If
        Return RetVal
    End Function

    Private Sub SetContextMenu1()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequest1)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("excel", My.Resources.Excel, AddressOf Do_Excel)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("contacte")
        oContextMenu.Items.Add(oMenuItem)

        Dim oContact As Contact = CurrentCli()

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest1
            oMenuItem.DropDownItems.AddRange(oMenu_Contact.Range)
        End If

        oMenuItem = New ToolStripMenuItem("asnef")
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("exportar a disc", My.Resources.save_16, AddressOf AsnefToFile))
        oMenuItem.DropDownItems.Add(New ToolStripMenuItem("enviar per FTP", My.Resources.save_16, AddressOf AsnefToFtp))


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SetContextMenu2()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As New ToolStripMenuItem("zoom", Nothing, AddressOf Do_ZoomImpagat)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView2.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols1.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols1.IcoCod).Value, Icons)
                    Case Icons.Empty
                        e.Value = My.Resources.empty
                    Case Icons.NoCuadra
                        e.Value = My.Resources.warn
                    Case Icons.NoAsnef
                        e.Value = My.Resources.NoPark
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim DcSdo As Decimal = oRow.Cells(Cols1.Sdo).Value
        If DcSdo <> 0 Then
            Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.impagats)
            Dim oContact As Contact = CurrentImpagat.Csb.Client
            Dim oExercici As New Exercici(oContact.Emp, Today.Year)
            Dim oFrm As New Frm_CliCtas(oContact, oCta, oExercici)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest1
            oFrm.Show()
        Else
            Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.impagats)
            Dim oContact As Contact = CurrentCli()
            Dim oExercici As New Exercici(oContact.Emp, Today.Year)
            Dim oFrm As New Frm_CliCtas(oContact, oCta, oExercici)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest1
            oFrm.Show()
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu1()
            LoadGrid2()
            SetContextMenu2()
            LoadMems()
        End If
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        MatExcel.GetExcelFromDataset(Asnef.GetDataSource()).Visible = True
    End Sub

    Private Sub RefreshRequest1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols1.Clx

        If DataGridView1.CurrentRow IsNot Nothing Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid1()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
        SetContextMenu1()
        LoadGrid2()
        SetContextMenu2()
        LoadMems()
    End Sub


    Private Sub RefreshRequest2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols2.Nominal

        If DataGridView2.CurrentRow IsNot Nothing Then
            i = DataGridView2.CurrentRow.Index
            j = DataGridView2.CurrentCell.ColumnIndex
            iFirstRow = DataGridView2.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid2()

        If DataGridView2.Rows.Count = 0 Then
        Else
            DataGridView2.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView2.Rows.Count - 1 Then
                DataGridView2.CurrentCell = DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(j)
            Else
                DataGridView2.CurrentCell = DataGridView2.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
        SetContextMenu2()
    End Sub

    Private Sub Do_ZoomImpagat(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Impagat
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest2
        With oFrm
            .Impagat = CurrentImpagat()
            .Show()
        End With
    End Sub

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Do_ZoomImpagat(sender, e)
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowEvents Then
            SetContextMenu2()
        End If
    End Sub

    Public Sub AsnefToFtp()
        Dim exs as new list(Of Exception)
        Dim RetVal As EventLogEntryType = Asnef.SendFile( exs)
        MsgBox( BLL.Defaults.ExsToMultiline(exs), IIf(RetVal = EventLogEntryType.Information, MsgBoxStyle.Information, MsgBoxStyle.Exclamation), "MAT.NET")
    End Sub

    Public Sub AsnefToFile()
        Dim oFileAsnef As Maxisrvr.MatFileASNEF = Asnef.GetFile

        Dim oDlg As New SaveFileDialog
        With oDlg
            .Title = "EXPORTAR FITXER ASNEF"
            .DefaultExt = ".zip"
            .Filter = "fitxers comprimits (*.zip)|*.zip|tots els fitxers(*.*)|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .FileName = oFileAsnef.DefaultZipFileName
            If .ShowDialog = DialogResult.OK Then
                oFileAsnef.SaveAsZip(.FileName, oFileAsnef.DefaultTxtFileName)
            Else
                Exit Sub
            End If
        End With
    End Sub



    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If IsDBNull(oRow.Cells(Cols1.LastVto).Value) Then
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        Else
            Dim BlShowUp As Boolean = False
            Dim DtLastVto As Date = oRow.Cells(Cols1.LastVto).Value
            If IsDBNull(oRow.Cells(Cols1.LastMem).Value) Then
                BlShowUp = True
            Else
                Dim DtLastMem As Date = oRow.Cells(Cols1.LastMem).Value
                BlShowUp = (DtLastVto > DtLastMem)
            End If

            If BlShowUp Then
                PaintGradientRowBackGround(e, Color.Yellow)
            Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            End If
        End If

    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            Me.DataGridView1.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            Me.DataGridView1.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub Xl_Mems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToAddNew
        Dim oMem As New DTOMem()
        With oMem
            .Contact = BLL.BLLContact.Find(CurrentCli.Guid)
            .Fch = Now
            .Cod = DTOMem.Cods.Impagos
            .Usr = BLL.BLLSession.Current.User
        End With

        Dim oFrm As New Frm_Mem(oMem)
        AddHandler oFrm.AfterUpdate, AddressOf LoadMems
        oFrm.Show()
    End Sub


    Private Sub Xl_Mems1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Mems1.RequestToRefresh
        LoadMems()
    End Sub


End Class