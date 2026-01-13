

Public Class Frm_Art_Arc
    Private mDsMgzs As DataSet
    Private mDs As DataSet
    Private mArt As Art
    Private mMgz As DTOMgz
    Private mEmp as DTOEmp
    Private mAllowEvents As Boolean
    Private mAllowCosts As Boolean

    Private Enum Cols
        Ye2
        Pdc
        Ye1
        Transm
        Alb
        Yef
        Fra
        Fch
        Clx
        Input
        Output
        Stk
        Pvp
        Dto
        Cur
        TransmPending
    End Enum

    Public Sub New(oArt As Art, oMgz As DTOMgz)
        MyBase.New()
        Me.InitializeComponent()
        mArt = oArt
        mMgz = oMgz
        mEmp = mArt.Emp
        Me.Text = "HISTORIAL DE " & mArt.Id & ": " & mArt.Nom_ESP
        mAllowCosts = App.Current.emp.WinUsr.AllowCosts
        PictureBoxArt.Image = mArt.Image
        LoadMgzs()
        refresca()
        mAllowEvents = True
    End Sub


    Private Sub refresca()
        Cursor = Cursors.WaitCursor
        LoadGrid()
        SetContextMenu()
        Cursor = Cursors.Default
    End Sub


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ARC.YE2, ARC.PDC, Alb.Yea, ALB.Transm, ARC.alb, ALB.YEF, ALB.ALB, ARC.fch, CLX.clx, " _
            & "(CASE WHEN ARC.COD<50 THEN QTY ELSE 0 END) AS INPUT, " _
            & "(CASE WHEN ARC.COD>49 THEN QTY ELSE 0 END) AS OUTPUT, " _
            & "ARC.STK, ARC.EUR, ARC.DTO, ARC.CUR, (case when ALB.transm=0 then 1 else 0 end) as TRANSMPENDING " _
            & "FROM ARC INNER JOIN " _
            & "ALB ON ARC.AlbGuid=Alb.Guid LEFT OUTER JOIN " _
            & "CLX ON ALB.CliGuid = CLX.Guid " _
            & "WHERE Arc.ArtGuid=@ArtGuid AND ARC.MgzGuid=@MgzGuid "

        If Not CheckBoxEntrades.Checked Then
            SQL = SQL & "AND (NOT ARC.COD<50) "
        End If

        If Not CheckBoxSortides.Checked Then
            SQL = SQL & "AND (NOT ARC.COD>=50) "
        End If

        If CheckBoxClient.Checked Then
            SQL = SQL & "AND ALB.CliGuid='" & Xl_Contact1.Contact.Guid.ToString & "' "
        End If

        SQL = SQL & "ORDER BY ARC.fch DESC, ARC.ALB DESC"

        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@ArtGuid", mArt.Guid.ToString, "@MgzGuid", CurrentMgz.Guid.ToString)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim iStk As Integer
        For i As Integer = oTb.Rows.Count - 1 To 0 Step -1
            oRow = oTb.Rows(i)
            iStk += oRow(Cols.Input) - oRow(Cols.Output)
            oRow(Cols.Stk) = iStk
            If Not mAllowCosts Then
                If oRow(Cols.Input) <> 0 Then
                    oRow(Cols.Pvp) = 0
                    oRow(Cols.Dto) = 0
                End If
            End If
        Next
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

            With .Columns(Cols.Ye2)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .Visible = False
            End With
            With .Columns(Cols.Ye1)
                .Visible = False
            End With
            With .Columns(Cols.Transm)
                .HeaderText = "transm"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Alb)
                .HeaderText = "albará"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Yef)
                .Visible = False
            End With
            With .Columns(Cols.Fra)
                .Visible = False
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Input)
                If CheckBoxEntrades.Checked Then
                    .HeaderText = "entrades"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 55
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Output)
                If CheckBoxSortides.Checked Then
                    .HeaderText = "sortides"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 55
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Stk)
                If (CheckBoxEntrades.Checked And CheckBoxSortides.Checked And Not CheckBoxClient.Checked) Then
                    .HeaderText = "stock"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .Width = 55
                    .DefaultCellStyle.Format = "#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Else
                    .Visible = False
                End If
            End With
            With .Columns(Cols.Pvp)
                .HeaderText = "Preu"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                '.DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "Dte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 35
                '.DefaultCellStyle.Format = "0%;-0%;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Cur)
                .Visible = False
            End With
            With .Columns(Cols.TransmPending)
                .Visible = False
            End With
        End With
    End Sub

    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Ye1).Value
            Dim LngId As Long = DataGridView1.CurrentRow.Cells(Cols.Alb).Value
            oAlb = MaxiSrvr.Alb.FromNum(mArt.Emp, iYea, LngId)
        End If
        Return oAlb
    End Function

    Private Function CurrentAlbs() As Albs
        Dim oAlbs As New Albs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim IYea As Integer
            Dim LngId As Integer
            Dim oAlb As Alb
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                IYea = oRow.Cells(Cols.Ye1).Value
                LngId = oRow.Cells(Cols.Alb).Value
                oAlb = MaxiSrvr.Alb.FromNum(mArt.Emp, IYea, LngId)
                oAlbs.Add(oAlb)
            Next
            oAlbs.Sort()
        Else
            Dim oAlb As Alb = CurrentAlb()
            If oAlb IsNot Nothing Then
                oAlbs.Add(CurrentAlb)
            End If
        End If
        Return oAlbs
    End Function


    Private Sub CheckBoxClient_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxClient.CheckedChanged
        Xl_Contact1.Visible = CheckBoxClient.Checked
        If CheckBoxClient.Checked Then
            Xl_Contact1.Contact = Nothing
        Else
            refresca()
        End If
    End Sub

    Private Sub Xl_Contact1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        refresca()
    End Sub


    Private Sub CheckBoxIO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxEntrades.CheckedChanged, CheckBoxSortides.CheckedChanged
        If mAllowEvents Then
            refresca()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlbs As Albs = CurrentAlbs()

        If oAlbs.Count > 0 Then
            Dim oMenu_alb As New Menu_Alb(oAlbs)
            AddHandler oMenu_alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_alb.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False

        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer

        Refresca()

        If DataGridView1.CurrentRow IsNot Nothing Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
            mAllowEvents = True
            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
            End If
        Else
            mAllowEvents = True
        End If

    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub


    Private Sub LoadMgzs()
        Dim SQL As String = "SELECT CLX.Guid as MgzGuid, CLX.Clx as Nom " _
        & "FROM  ARC INNER JOIN " _
        & "CLX ON ARC.MgzGuid = CLX.Guid " _
        & "WHERE ARC.ArtGuid=@ArtGuid " _
        & "GROUP BY CLX.Guid, CLX.Clx " _
        & "ORDER BY Clx.clx"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@ArtGuid", mArt.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With ToolStripComboBoxMgz.ComboBox
            .BindingContext = Me.BindingContext
            .DisplayMember = "Nom"
            .ValueMember = "MgzGuid"
            .DataSource = oTb.DefaultView
            .SelectedValue = mMgz.Guid
            AddHandler .SelectedIndexChanged, AddressOf onMgzChanged
        End With

    End Sub

    Private Sub onMgzChanged(sender As Object, e As System.EventArgs)
        If mAllowEvents Then
            refresca()
        End If
    End Sub

    Private Function CurrentMgz() As Mgz
        Dim oGuid As Guid = ToolStripComboBoxMgz.ComboBox.SelectedValue
        Dim retval As Mgz = New Mgz(oGuid)
        Return retval
    End Function

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Transm
                If e.Value = 0 Then e.Value = ""
            Case Cols.Dto
                If e.Value = 0 Then
                    e.Value = ""
                Else
                    e.Value = e.Value & "%"
                End If
            Case Cols.Pvp
                If e.Value = 0 Then
                    e.Value = ""
                Else
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim sCur As String = oRow.Cells(Cols.Cur).Value
                    Select Case sCur
                        Case "EUR"
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " €"
                        Case "GBP"
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " £"
                        Case "USD"
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " $"
                        Case Else
                            e.Value = Format(CDbl(e.Value), "#,##0.00") & " " & sCur
                    End Select
                End If
        End Select
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        If oRow.Cells(Cols.TransmPending).Value <> 0 Then
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

    Private Sub RefrescaArt()
        mArt.SetItm()
        PictureBoxArt.Image = mArt.Image

        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu_Art As New Menu_Art(mArt)
        AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequestArt
        oContextMenu.Items.AddRange(oMenu_Art.Range)
        PictureBoxArt.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub PictureBoxArt_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxArt.DoubleClick
        Dim oFrm As New Frm_Art(mArt)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestArt
        oFrm.Show()
    End Sub

    Private Sub RefreshRequestArt(ByVal sender As Object, ByVal e As System.EventArgs)
        RefrescaArt()
    End Sub



End Class