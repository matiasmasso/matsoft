

Public Class Frm_Transmisio_New
    Private mTransmisio As Transmisio
    Private mMgz As DTOMgz
    Private mEmp as DTOEmp
    Private mAlbs As Albs
    Private _Deliveries As List(Of DTODelivery)
    Private mDs As DataSet
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Chk
        Alb
        Fch
        Eur
        Clx
        Trp
        Idx
    End Enum

    Public Sub New(ByVal oTransmisio As Transmisio)
        MyBase.New()
        Me.InitializeComponent()
        mTransmisio = oTransmisio
        Me.Text = "TRANSMISIO " & mTransmisio.Id.ToString
        mMgz = mTransmisio.Mgz
        mAlbs = mTransmisio.Albs

        refresca()
    End Sub

    Public Sub New(ByVal oMgz As DTOMgz)
        MyBase.New()
        Me.InitializeComponent()
        mMgz = oMgz
        Dim iEciCount As Integer
        _Deliveries = BLL.BLLDeliveries.PendentsDeTransmetre(BLLApp.Mgz)
        mAlbs = AlbsLoader.PendentsDeTransmetre(oMgz, iEciCount)
        refresca()
    End Sub

    Private Sub refresca()
        If mMgz Is Nothing Then
            mEmp =BLL.BLLApp.Emp
        Else
            mEmp = mMgz.Emp
        End If
        TextBoxMailTo.Text = BLL.BLLDefault.EmpValue(DTODefault.Codis.EmailTransmisioVivace)
        CreateDatatable()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub CreateDatatable()
        Dim oTb As New DataTable("PNDS")
        oTb.Columns.Add("CHK", System.Type.GetType("System.Boolean"))
        oTb.Columns.Add("ALB", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("FCH", System.Type.GetType("System.DateTime"))
        oTb.Columns.Add("EUR", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("CLX", System.Type.GetType("System.String"))
        oTb.Columns.Add("TRP", System.Type.GetType("System.String"))
        oTb.Columns.Add("IDX", System.Type.GetType("System.Int32"))
        mDs = New DataSet
        mDs.Tables.Add(oTb)
    End Sub

    Private Sub LoadGrid()
        'Dim BlTransmitEciAlbs As Boolean = ECI.AllowedTransmision

        Dim oRow As DataRow
        Dim oTb As DataTable = mDs.Tables(0)
        Dim idx As Integer
        'Dim BlCheck As Boolean
        'For Each oDelivery As DTODelivery In _Deliveries
        For Each oAlb As Alb In mAlbs
            oRow = oTb.NewRow()

            'If ECI.Belongs(oAlb.Client) Then
            'BlCheck = BlTransmitEciAlbs
            'Else
            'BlCheck = True
            'End If

            oRow(Cols.Chk) = True 'BlCheck
            oRow(Cols.Alb) = oAlb.Id
            oRow(Cols.Fch) = oAlb.Fch
            oRow(Cols.Eur) = oAlb.Total.Eur
            oRow(Cols.Clx) = oAlb.Nom & " (" & oAlb.Zip.Location.Nom & ")"
            If oAlb.Transportista IsNot Nothing Then
                oRow(Cols.Trp) = oAlb.Transportista.Abr
            End If
            oRow(Cols.Idx) = idx
            oTb.Rows.Add(oRow)
            idx += 1
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.Alb)
                .HeaderText = "Albará"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ReadOnly = True
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(Cols.Trp)
                .HeaderText = "Transport"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 100
                .ReadOnly = True
            End With
            With .Columns(Cols.Idx)
                .Visible = False
            End With
        End With
        SetTotals()
    End Sub

    Private Sub SetTotals()
        Dim oSum As DTOAmt = BLLApp.EmptyAmt
        Dim iCount As Integer
        Dim Idx As Integer
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(Cols.Chk).Value Then
                Idx = DataGridView1.Rows(i).Cells(Cols.Idx).Value
                oSum.Add(mAlbs(Idx).Total)
                iCount = iCount + 1
            End If
        Next
        LabelTot.Text = iCount & " albs. " & oSum.CurFormatted
        ButtonOk.Enabled = (iCount > 0)
    End Sub


    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim Idx As Integer = oRow.Cells(Cols.Idx).Value
            oAlb = mAlbs(Idx)
        End If
        Return oAlb
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mTransmisio Is Nothing Then
            mTransmisio = New Transmisio(mMgz, Now)
        End If
        Cursor = Cursors.WaitCursor
        mTransmisio.Albs = GetAlbsFromGrid()

        Dim exs as New List(Of exception)
        If TransmisioLoader.Update(mTransmisio, exs) Then
            If CheckBoxMailTo.Checked Then
                'Dim oTransmisio As New DTOTransmisio(mTransmisio.Guid)
                If mTransmisio.Send(TextBoxMailTo.Text, exs) Then
                    MsgBox("transmisió enviada correctament a:" & vbCrLf & TextBoxMailTo.Text, MsgBoxStyle.Information, "MAT.NET")
                    RaiseEvent AfterUpdate(sender, e)
                    Me.Close()
                Else
                    UIHelper.WarnError(exs, "error en la transmisió:")
                End If
            Else
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            End If
        Else
            UIHelper.WarnError( exs, "error al desar la transmisio")
        End If

        Cursor = Cursors.Default
    End Sub

    Public Function GetAlbsFromGrid() As Albs
        Dim oAlbs As New Albs
        Dim Idx As Integer
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells(Cols.Chk).Value Then
                Idx = DataGridView1.Rows(i).Cells(Cols.Idx).Value
                oAlbs.Add(mAlbs(Idx))
            End If
        Next
        Return oAlbs
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
            Case Else
                Dim oFrm As New Frm_AlbNew2(CurrentAlb)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                With oFrm
                    .Show()
                End With
        End Select
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlb As Alb = CurrentAlb()

        If oAlb IsNot Nothing Then
            Dim tmp As ToolStripMenuItem = oContextMenu.Items.Add("albará")
            Dim oMenu_Alb As New Menu_Alb(oAlb)
            AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            tmp.DropDownItems.AddRange(oMenu_Alb.Range)
        End If
        oContextMenu.Items.Add("marcar tots", Nothing, AddressOf Do_SelectAll)
        oContextMenu.Items.Add("desmarcar tots", Nothing, AddressOf Do_DeselectAll)


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_SelectAll()
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = True
        Next
    End Sub

    Private Sub Do_DeselectAll()
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = False
        Next
        If DataGridView1.SelectedRows.Count > 0 Then
            For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
                oRow.Cells(Cols.Chk).Value = False
            Next
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Alb

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If mDs.Tables(0).Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub CheckBoxMailTo_ChkChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxMailTo.CheckedChanged
        TextBoxMailTo.Enabled = CheckBoxMailTo.Checked
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Chk
                SetTotals()
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub ToolStripButtonPdfAlbs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonPdfAlbs.Click
        Dim oAlbs As Albs = ECI.SortAlbs(GetAlbsFromGrid)
        root.ShowPdf(oAlbs.PdfStream())
    End Sub

    Private Sub ToolStripButtonCheckNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonCheckNone.Click
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = False
        Next
    End Sub

    Private Sub ToolStripButtonCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonCheckAll.Click
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = True
        Next
    End Sub
End Class