

Public Class Frm_Contact_Correspondencia
    Private _Contact As DTOContact

    Private mEmp as DTOEmp
    Private mContact As Contact
    Private mDsYeas As DataSet
    Private mDirtyBigFile As maxisrvr.BigFileNew
    Private mAllowEvents As Boolean

    Private Enum ColsMails
        Guid
        Id
        Fch
        Mime
        Ico
        RT
        Subject
        Usr
    End Enum


    Public WriteOnly Property Contact() As Contact
        Set(ByVal value As Contact)
            _Contact = BLL.BLLContact.Find(value.Guid)
            mContact = value
            mEmp = mContact.Emp
            Me.Text = "CORRESPONDENCIA I MEMOS DE " & mContact.Clx
            RefrescaMails()
            RefrescaMemos()
            mAllowEvents = True
        End Set
    End Property

    Private Sub RefrescaMails()
        LoadGridMails()
        SetContextMenuMails()

        SplitContainer2.Panel2Collapsed = True
        'Xl_Contact_Correspondencies1.Visible = False
        'RefrescacCorrespondencia()
    End Sub

    Private Sub RefrescacCorrespondencia()
        Dim oCorrespondencies As List(Of DTO.DTOCorrespondencia) = BLL_Correspondencies.FromContact(_Contact)
        Xl_Contact_Correspondencies1.Load(oCorrespondencies)
    End Sub

    Private Sub RefrescaMemos()
        Dim oMems As List(Of DTOMem) = BLL.BLLMems.All(New DTOContact(mContact.Guid))
        Xl_Mems1.Load(oMems)
    End Sub

    Private Sub LoadGridMails()
        Dim SQL As String = "SELECT CRR.Guid, CRR.CRR, CRR.FCH, " _
        & "Bigfile.MIME, " _
        & "CRR.RT, CRR.DSC " _
        & "FROM CRRCLI LEFT OUTER JOIN " _
        & "CRR ON CRRCLI.MAILGUID=CRR.GUID " _
        & "LEFT OUTER JOIN Bigfile ON CRR.Hash COLLATE SQL_Latin1_General_CP1_CI_AS = Bigfile.Hash " _
        & "WHERE CRRCLI.CLIGUID='" & mContact.Guid.ToString & "' " _
        & "ORDER BY (CASE WHEN CRR.CRR IS NULL THEN 0 ELSE 1 END), CRR.YEA DESC, CRR.CRR DESC"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        'afageix icono Pdf
        Dim oCol As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColsMails.Ico)

        With DataGridViewMails
            With .RowTemplate
                .Height = DataGridViewMails.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True

            With .Columns(ColsMails.Guid)
                .Visible = False
            End With
            With .Columns(ColsMails.Id)
                .HeaderText = "registre"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsMails.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsMails.Mime)
                .Visible = False
            End With
            With .Columns(ColsMails.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsMails.RT)
                .Visible = False
            End With
            With .Columns(ColsMails.Subject)
                .HeaderText = "assumpte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub



    Private Function CurrentMail() As Mail
        Dim oMail As Mail = Nothing
        Dim oRow As DataGridViewRow = DataGridViewMails.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridViewMails.CurrentRow.Cells(ColsMails.Guid).Value
            oMail = New Mail(oGuid)
        End If
        Return oMail
    End Function


    Private Sub SetContextMenuMails()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMail As Mail = CurrentMail()

        If oMail IsNot Nothing Then
            Dim oMenu_Mail As New Menu_Mail(oMail)
            AddHandler oMenu_Mail.AfterUpdate, AddressOf RefreshRequestMails
            oContextMenu.Items.AddRange(oMenu_Mail.Range)
        End If

        DataGridViewMails.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequestMails(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oGrid As DataGridView = DataGridViewMails

        mAllowEvents = False
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsMails.Fch

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        RefrescaMails()

        mAllowEvents = True
        If oGrid.Rows.Count > 0 Then
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
        SetContextMenuMails()
    End Sub

    Private Sub DataGridViewMails_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewMails.CellFormatting
        Select Case e.ColumnIndex
            Case ColsMails.Ico
                Dim oRow As DataGridViewRow = DataGridViewMails.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(ColsMails.Mime).Value) Then
                    e.Value = My.Resources.empty
                Else
                    Dim oMimecod As DTOEnums.MimeCods = oRow.Cells(ColsMails.Mime).Value
                    e.Value = root.GetIcoFromMime(oMimecod)
                End If
        End Select
    End Sub

    Private Sub DataGridViewMails_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewMails.DoubleClick
        ShowMail(CurrentMail)
    End Sub

    Private Sub DataGridViewMails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridViewMails.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowMail(CurrentMail)
            e.Handled = True
        End If
    End Sub

    Private Sub ShowMail(ByVal oMail As Mail)
        Dim oFrm As New Frm_Contact_Mail(oMail)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestMails
        oFrm.show()
    End Sub

    Private Sub DataGridViewMails_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewMails.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridViewMails.Rows(e.RowIndex)
        Dim oCod As DTO.DTOCorrespondencia.Cods = CType(oRow.Cells(ColsMails.RT).Value, DTO.DTOCorrespondencia.Cods)
        Select Case oCod
            Case DTO.DTOCorrespondencia.Cods.Enviat
                PaintGradientRowBackGround(DataGridViewMails, e, Color.LightBlue)
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal oDataGridView As DataGridView, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus

        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            oDataGridView.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            oDataGridView.HorizontalScrollingOffset + 1, _
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

    Private Sub DataGridViewMails_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewMails.SelectionChanged
        SetContextMenuMails()
    End Sub


    Private Sub ToolStripButtonRefrescaMails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefrescaMails.Click
        RefrescaMails()
    End Sub

    Private Sub DataGridViewMails_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridViewMails.DragEnter
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

    Private Sub DataGridViewMails_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridViewMails.DragOver
        Dim oPoint = DataGridViewMails.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridViewMails.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridViewMails.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridViewMails.CurrentCell = oclickedCell
        End If
    End Sub

    Private Sub DataGridViewMails_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridViewMails.DragDrop
        Dim exs As New List(Of Exception)
        Dim oTargetCell As DataGridViewCell = Nothing
        Dim oDocFiles As List(Of DTODocFile) = Nothing
        If DragDropHelper.GetDatagridDropDocFiles(sender, e, oDocFiles, oTargetCell, exs) Then
            If oTargetCell Is Nothing Then
                Dim oMail As New Mail(BLL.BLLApp.Emp, Today)
                With oMail
                    .DocFile = oDocFiles.First
                    .Cod = DTO.DTOCorrespondencia.Cods.Rebut
                    .Contacts.Add(mContact)
                End With
                Dim oFrm As New Frm_Contact_Mail(oMail)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestMails
                oFrm.Show()

            Else
                Dim oMail As Mail = CurrentMail()
                oMail.DocFile = oDocFiles.First
                If oMail.Update(exs) Then
                    RefreshRequestMails(oMail, New System.EventArgs)
                Else
                    MsgBox("error al desar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If

        Else
            MsgBox("error al arrossegar el document" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If

    End Sub




    Private Sub ToolStripButtonRefrescaMemos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefrescaMemos.Click
        RefrescaMemos()
    End Sub

    Private Sub ToolStripButtonNewMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonNewMail.Click
        Dim oMail As New Mail(mEmp, Today)
        With oMail
            .Contacts.Add(mContact)
            .Cod = DTO.DTOCorrespondencia.Cods.Rebut
        End With
        ShowMail(oMail)
    End Sub

    Private Sub ToolStripButtonNewMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonNewMemo.Click
        Dim oMem As New DTOMem()
        With oMem
            .Contact = New DTOContact(mContact.Guid)
            .Fch = Today
            .Cod = DTOMem.Cods.Despaitx
            .Usr = BLL.BLLSession.Current.User
        End With

        Dim oFrm As New Frm_Mem(oMem)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaMemos
        oFrm.Show()
    End Sub

    Private Sub Xl_Contact_Correspondencies1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contact_Correspondencies1.RequestToAddNew
        Dim oContact As New DTOContact(mContact.Guid)
        oContact.FullNom = mContact.Clx
        oContact.Emp = New DTOEmp
        oContact.Emp.Id = BLL_App.Current.Emp.Id
        Dim oCorrespondencia As DTO.DTOCorrespondencia = BLL_Correspondencia.NewFromContact(oContact, DTO.DTOCorrespondencia.Cods.Rebut, BLL.BLLSession.Current.User)
        Dim oFrm As New Frm_Correspondencia(oCorrespondencia)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescacCorrespondencia
        oFrm.Show()
    End Sub


    Private Sub Xl_Contact_Correspondencies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Correspondencies1.RequestToRefresh
        RefrescacCorrespondencia()
    End Sub

    Private Sub Xl_Mems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToAddNew
        Dim oMem As New DTOMem
        With oMem
            .Usr = BLL.BLLSession.Current.User
            .Fch = Today
            .FchCreated = Now
            .Contact = _Contact
        End With
        Dim oFrm As New Frm_Mem(oMem)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaMemos
        oFrm.Show()
    End Sub

    Private Sub Xl_Mems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToRefresh
        RefrescaMemos()
    End Sub
End Class


