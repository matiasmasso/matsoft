

Public Class Xl_NUK_Invoic
    Private mAllowEvents As Boolean
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mProgressBar As ToolStripProgressBar = Nothing

    Public Event SelectionChange(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Warn
        Ico
        File
        Fra
        Fch
        Clx
        Bas
        Vat
        Liq
    End Enum

    Public Sub New(ByRef oProgressBar As ToolStripProgressBar)
        MyBase.new()
        InitializeComponent()
        mProgressBar = oProgressBar
        LoadGrid()

        Dim oFra As FacturaDeProveidor = CurrentFra()
        If oFra IsNot Nothing Then
            SetContextMenu()
            RaiseEvent SelectionChange(oFra, EventArgs.Empty)
        End If

        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim BlOldEvents As Boolean = mAllowEvents
        mAllowEvents = False
        Dim oTb As New DataTable
        Dim oRow As DataRow = Nothing
        With oTb.Columns
            .Add("WARN", System.Type.GetType("System.Int32"))
            .Add("ICO", System.Type.GetType("System.Byte[]"))
            .Add("FILE", System.Type.GetType("System.String"))
            .Add("FRA", System.Type.GetType("System.Int32"))
            .Add("FCH", System.Type.GetType("System.DateTime"))
            .Add("CLX", System.Type.GetType("System.String"))
            .Add("BAS", System.Type.GetType("System.Decimal"))
            .Add("VAT", System.Type.GetType("System.Decimal"))
            .Add("LIQ", System.Type.GetType("System.Decimal"))
        End With

        Dim oDirectoryInfo As New IO.DirectoryInfo(eDiversa.FolderPath(FolderPaths.inbox))
        Dim oFileInfos As IO.FileInfo() = oDirectoryInfo.GetFiles()
        Dim oFileInfo As IO.FileInfo = Nothing

        With mProgressBar
            .Visible = True
            .Minimum = 0
            .Maximum = oFileInfos.Length
            .Value = .Minimum
        End With

        Dim oFra As FacturaDeProveidor = Nothing
        For Each oFileInfo In oFileInfos
            Try
                mProgressBar.Increment(1)
                Dim oFF As New FF_EDIVERSA_INVOIC(oFileInfo.FullName())
                If oFF.TypeMatch() Then
                    oRow = oTb.NewRow
                    oRow(Cols.Ico) = maxisrvr.GetByteArrayFromImg(My.Resources.warn)
                    oRow(Cols.File) = oFileInfo.Name
                    oTb.Rows.Add(oRow)
                    Try
                        ' oFra = New FacturaDeProveidor(oFF)
                        'With oFra
                        'oRow(Cols.Fra) = .Num
                        'oRow(Cols.Fch) = .Fch
                        'oRow(Cols.Clx) = .Proveidor.Nom
                        'oRow(Cols.Bas) = .Bas.Eur
                        'oRow(Cols.Vat) = .Iva.Eur
                        'oRow(Cols.Liq) = .Liq.Eur

                        'If .ValidationErrors.Count = 0 Then
                        ' oRow(Cols.Ico) = maxisrvr.GetByteArrayFromImg(My.Resources.empty)
                        ' End If

                        'End With
                    Catch ex As Exception

                    End Try
                Else
                    'Stop
                End If

            Catch ex As Exception
                'Stop
            End Try
        Next

        mProgressBar.Visible = False

        With DataGridView1
            .DataSource = oTb
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True

            With .Columns(Cols.Warn)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.File)
                .HeaderText = "fitxer"
                .Width = 100
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "proveidor"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Bas)
                .HeaderText = "Base"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Vat)
                .HeaderText = "impostos"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Liq)
                .HeaderText = "total"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mAllowEvents = BlOldEvents

    End Sub

    Private Function CurrentRow() As DataGridViewRow
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Return oRow
    End Function

    Private Function CurrentFra() As FacturaDeProveidor
        Dim oFra As FacturaDeProveidor = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim sFilename As String = System.IO.Path.Combine(eDiversa.FolderPath(FolderPaths.inbox), oRow.Cells(Cols.File).Value)
            Dim oFF As New FF_EDIVERSA_INVOIC(sFilename)
            'oFra = New FacturaDeProveidor(oFF)
        End If
        Return oFra
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        'oMenuItem = New ToolStripMenuItem("factura", Nothing, AddressOf ShowFra)
        'oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("fitxer", Nothing, AddressOf ShowFile)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("refresca", Nothing, AddressOf LoadGrid)
        oContextMenu.Items.Add(oMenuItem)

        oMenuItem = New ToolStripMenuItem("validar", Nothing, AddressOf Validar)
        oContextMenu.Items.Add(oMenuItem)

        If BLL.BLLSession.Current.User.Rol.id = Rol.Ids.SuperUser Then
            'oMenuItem = New ToolStripMenuItem("importa albará", My.Resources.candau, AddressOf ImportaAlb)
            'oContextMenu.Items.Add(oMenuItem)

            'oMenuItem = New ToolStripMenuItem("importa tots els validats", My.Resources.candau, AddressOf ImportaTotsAlbs)
            'oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("eliminar fitxer", My.Resources.candau, AddressOf DelInvoic)
            oContextMenu.Items.Add(oMenuItem)
        End If


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub




    Private Sub ShowFile(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then
            Dim sFilename As String = System.IO.Path.Combine(eDiversa.FolderPath(FolderPaths.inbox), oRow.Cells(Cols.File).Value)
            Process.Start("notepad.exe", sFilename)
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            Dim oFra As FacturaDeProveidor = CurrentFra
            If oFra IsNot Nothing Then
                SetContextMenu()
                RaiseEvent SelectionChange(oFra, EventArgs.Empty)
            End If
        End If
    End Sub


    Private Sub DelInvoic(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then
            Dim sFilename As String = oRow.Cells(Cols.File).Value
            Dim rc As MsgBoxResult = MsgBox("eliminem fitxer " & sFilename & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim sFullPath As String = System.IO.Path.Combine(eDiversa.FolderPath(FolderPaths.inbox), sFilename)
                Dim oFile As New System.IO.FileInfo(sFullPath)
                oFile.Delete()
                RefreshRequest(sender, e)
            End If
        End If
    End Sub

    Private Sub ArxivaDesadv(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then
            Dim sFile As String = oRow.Cells(Cols.File).Value
            Dim sFullFileName As String = eDiversa.FolderPath(FolderPaths.inbox) & sFile
            Dim FF As New FF_EDIVERSA_DESADV(sFullFileName)
            Dim oPdc As Pdc = Pdc.FromPdcRefOrLastPdc(FF.NumeroDeComanda, New Pdc(BLL.BLLApp.Emp, DTOPurchaseOrder.Codis.client))
            Dim sql As String = "SELECT Arc.AlbGuid, ARC.YE1, ARC.ALB, ARC.YE2, ARC.PDC FROM ARC INNER JOIN " _
            & "PNC ON ARC.PdcGuid=PNC.PdcGuid AND ARC.LN2=PNC.LIN INNER JOIN " _
            & "ALB ON ARC.AlbGuid=ALB.Guid WHERE " _
            & "PNC.PdcGuid=@PdcGuid AND DELEGATNUM like '" & FF.NumeroDeAlbara & "' " _
            & "GROUP BY Arc.AlbGuid, ARC.YE1, ARC.ALB, ARC.YE2, ARC.PDC"
            Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(sql, MaxiSrvr.Databases.Maxi, "@PdcGuid", oPdc.Guid.ToString)
            Dim oAlb As Alb = Nothing
            If oDrd.Read Then
                Dim oGuid As Guid = oDrd("AlbGuid")
                oAlb = New Alb(oGuid)
                eDiversa.ArxivaDesadv(oDrd("YE1").ToString, oDrd("ALB").ToString, FF.NumeroDeAlbara & "." & oPdc.Id, sFullFileName)
            End If


            If oAlb IsNot Nothing Then
                MsgBox("Ok")
            Else
                MsgBox("KO", MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub


    Private Sub ImportaAlb(ByVal sender As System.Object, ByVal e As System.EventArgs) ' Handles 'ToolStripMenuItemNewAlb.Click
        'Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        'Dim oRow As DataGridViewRow = CurrentRow()
        'If oRow IsNot Nothing Then
        ' Dim sFile As String = oRow.Cells(Cols.File).Value
        ' Dim oFF As New FF_EDIVERSA_DESADV(eDiversa.FolderPath(FolderPaths.inbox) & sFile)
        ' Dim oAlb As Alb = eDiversa.ImportaDesadv(oFF, oFF.validationerrors)
        ' If oFF.validationerrors.Count = 0 Then
        ' If oAlb IsNot Nothing Then
        ' MsgBox("albará " & oAlb.Id & " registrat correctament", MsgBoxStyle.Information, "MAT.NET")
        ' LoadGrid()
        ' Else
        ' MsgBox("Operació no efectuada" & vbCrLf & oFF.validationerrors.ToMultilineString, MsgBoxStyle.Exclamation, "MAT.NET")
        ' End If
        ' Else
        ' MsgBox("hi han errors de validacio" & vbCrLf & oFF.validationerrors.ToMultilineString, MsgBoxStyle.Exclamation, "MAT.NET")
        ' End If
        ' End If
    End Sub

    Private Sub Validar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oRow As DataGridViewRow = CurrentRow()
        If oRow IsNot Nothing Then
            Dim sFile As String = oRow.Cells(Cols.File).Value
            Dim oFF As New FF_EDIVERSA_INVOIC(eDiversa.FolderPath(FolderPaths.inbox) & sFile)
            'Dim oFra As New FacturaDeProveidor(oFF)
            'Dim oFrm As New Frm_FraProveidor(oFra)
            'AddHandler oFrm.afterupdate, AddressOf Refreshrequest
            'ofrm.show()

        End If

    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.File
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If

            Dim oFra As FacturaDeProveidor = CurrentFra()
            If oFra IsNot Nothing Then
                SetContextMenu()
                RaiseEvent SelectionChange(oFra, EventArgs.Empty)
            End If


        End If


    End Sub
End Class
