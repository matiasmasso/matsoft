
Imports System.Drawing

Public Class Frm_Pdcs
    Private mDs As DataSet
    Private mContact As Contact
    Private mEmp as DTOEmp
    Private mAllowEvents As Boolean
    Private mCod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.NotSet

    Private Enum Cols
        Guid
        Id
        Pdf
        IcoPdf
        Fch
        Eur
        Pdd
        IcoSrc
        Src
        Usr
        Pn2
    End Enum

    Public WriteOnly Property Contact() As Contact
        Set(ByVal Value As Contact)
            mContact = Value
            mEmp = mContact.Emp
            Select Case mCod
                Case DTOPurchaseOrder.Codis.client
                    Me.Text = "COMANDES DE " & mContact.Clx
                Case DTOPurchaseOrder.Codis.proveidor
                    Me.Text = "COMANDES A " & mContact.Clx
            End Select
            Refresca()
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property Cod() As DTOPurchaseOrder.Codis
        Set(ByVal value As DTOPurchaseOrder.Codis)
            mCod = value
        End Set
    End Property

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT PDC.Guid,PDC.PDC, " _
        & "(CASE WHEN PDC.Hash IS NULL THEN 0 ELSE 1 END) AS PDF," _
        & "PDC.FCH,PDC.EUR,PDC.PDD,PDC.SRC, " _
        & "(CASE WHEN USR.LOGIN IS NULL THEN CAST(PDC.USRCREATED AS VARCHAR) ELSE USR.LOGIN END) AS USR, " _
        & "(CASE WHEN MAX(PNC.Pn2) > 0 THEN 1 ELSE 0 END) AS PN2 " _
        & "FROM PDC LEFT OUTER JOIN " _
        & "PNC ON PDC.Guid = PNC.PdcGuid LEFT OUTER JOIN " _
        & "EMPUSR ON Pdc.UsrCreatedGuid=EMPUSR.ContactGuid LEFT OUTER JOIN " _
        & "USR ON EmpUsr.UsrGuid = Usr.Guid " _
        & "WHERE PDC.CliGuid=@CliGuid AND " _
        & "PDC.COD=@COD " _
        & "GROUP BY Pdc.Guid,PDC.YEA,PDC.PDC,(CASE WHEN PDC.Hash IS NULL THEN 0 ELSE 1 END),PDC.FCH,PDC.EUR,PDC.PDD,PDC.SRC,PDC.USRCREATED,USR.LOGIN " _
        & "ORDER BY PDC.YEA DESC, PDC.PDC DESC"

        mDs = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@CliGuid", mContact.Guid.ToString, "@COD", CInt(mCod))
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oColPdf As DataColumn = oTb.Columns.Add("ICOPDF", System.Type.GetType("System.Byte[]"))
        oColPdf.SetOrdinal(Cols.IcoPdf)

        Dim oColSrc As DataColumn = oTb.Columns.Add("ICOSRC", System.Type.GetType("System.Byte[]"))
        oColSrc.SetOrdinal(Cols.IcoSrc)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowDrop = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Comanda"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Pdf)
                .Visible = False
            End With
            With .Columns(Cols.IcoPdf)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Pdd)
                .HeaderText = "Concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Src)
                .Visible = False
            End With
            With .Columns(Cols.IcoSrc)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Usr)
                .HeaderText = "Usuari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Pn2)
                .Visible = False
            End With
        End With
    End Sub

    Private Function CurrentPdc() As Pdc
        Dim oPdc As Pdc = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
            oPdc = New Pdc(oGuid)
        End If
        Return oPdc
    End Function

    Private Function CurrentPdcs() As Pdcs
        Dim oPdcs As New Pdcs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oPdc As New Pdc(oGuid)
                oPdcs.Add(oPdc)
            Next
            oPdcs.Sort()
        Else
            Dim oPdc As Pdc = CurrentPdc()
            If oPdc IsNot Nothing Then
                oPdcs.Add(CurrentPdc)
            End If
        End If
        Return oPdcs
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPdcs As Pdcs = CurrentPdcs()

        If oPdcs.Count > 0 Then
            Dim oMenu_Pdc As New Menu_Pdc(oPdcs)
            AddHandler oMenu_Pdc.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pdc.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()
        Refresca()

        If DataGridView1.Rows.Count > 0 Then

            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
            End If
        End If
        SetContextMenu()
        mAllowEvents = True
    End Sub



    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        'Exit Sub
        Select Case e.ColumnIndex
            Case Cols.IcoPdf
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Pdf).Value = 1 Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = My.Resources.empty
                End If
            Case Cols.IcoSrc
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As DTOPurchaseOrder.Codis = CType(oRow.Cells(Cols.Src).Value, DTOPurchaseOrder.Codis)
                e.Value = BLL.BLLPurchaseOrder.SrcIcon(oCod)
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowPdc()
    End Sub

    Private Sub ShowPdc()
        Dim oPdc As Pdc = CurrentPdc()
        Select Case oPdc.Cod
            Case DTOPurchaseOrder.Codis.client
                If oPdc IsNot Nothing Then
                    Dim oPurchaseOrder As DTOPurchaseOrder = BLL.BLLPurchaseOrder.Find(oPdc.Guid)
                    Dim exs As New List(Of Exception)
                    If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        UIHelper.WarnError(exs)
                    Else
                        Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    End If
                End If
            Case DTOPurchaseOrder.Codis.Proveidor
                Dim exs As New List(Of Exception)
                If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(oPdc.Client.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
                    UIHelper.WarnError(exs)
                Else
                    Dim oFrm As New Frm_Pdc_Proveidor(oPdc)
                    oFrm.Show()
                End If

            Case Else
                MsgBox("nomes implementades les comandes de client o proveidor", MsgBoxStyle.Exclamation)
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If oRow.Cells(Cols.Pn2).Value = 1 Then
            PaintGradientRowBackGround(e, Color.Yellow)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

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
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Refresca()
    End Sub


#Region "DragDrop"

    Private mLastMouseDownRectangle As System.Drawing.Rectangle

    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            '    or this tells us if it is an Outlook attachment drop
            'PictureBox1.BackColor = Color.SeaShell
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            'PictureBox1.BackColor = Color.LemonChiffon
            '    or none of the above
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub DataGridView1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragOver
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridView1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        Dim oTargetCell As DataGridViewCell = Nothing
        Dim exs as New List(Of exception)
        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            If oTargetCell IsNot Nothing Then
                Dim oRow As DataGridViewRow = DataGridView1.Rows(oTargetCell.RowIndex)
                Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
                Dim oPdc As New Pdc(oGuid)
                ImportPdf(oPdc, oDocFiles.First)
                RefreshRequest(sender, e)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DataGridView1.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    DataGridView1.CurrentCell = sender.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
                    Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
                    Dim oPdc As Pdc = CurrentPdc()
                    sender.DoDragDrop(oPdc, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

    Private Sub ImportPdf(ByVal oPdc As Pdc, ByVal oDocFile As DTODocFile)
        oPdc.DocFile = oDocFile
        Dim exs as New List(Of exception)
        If oPdc.Update( exs) Then
            RefreshRequest(oPdc, New System.EventArgs)
        Else
            UIHelper.WarnError( exs, "error al desar la comanda")
        End If
    End Sub

#End Region

End Class