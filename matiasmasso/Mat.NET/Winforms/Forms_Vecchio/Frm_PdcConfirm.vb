

Public Class Frm_PdcConfirm
    Private mPdcConfirm As PdcConfirm = Nothing
    Private mAllowEvents As Boolean = False
    Private mPncs As LineItmPncs
    Private mDirtyPncs As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        LinCod
        PncGuid
        PdcGuid
        Pdc
        Lin
        Qty
        Art
        Nom
        Pvp
        Dto
        Amt
        Fch
    End Enum

    Private Enum LinCods
        Pdc
        Pnc
    End Enum

    Public Sub New(ByVal oPdcConfirm As PdcConfirm)
        MyBase.new()
        Me.InitializeComponent()
        mPdcConfirm = oPdcConfirm
        refresca()
    End Sub

    Private Sub Refresca()
        With mPdcConfirm
            Xl_DocFile1.Load(.DocFile)
            DateTimePickerFch.Value = .Fch
            TextBoxRef.Text = .Ref
            PictureBoxLogo.Image = .Contact.Img48
            Xl_Contact1.Contact = .Contact
            If .ETD <> Nothing Then
                DateTimePickerETD.Value = .ETD
            End If
            If .ETA <> Nothing Then
                DateTimePickerETA.Value = .ETA
            End If
            TextBoxObs.Text = .Obs
            mPncs = .Pncs
            LoadGrid()
            ButtonDel.Enabled = .Exists
            mAllowEvents = True
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 _
    DateTimePickerFch.ValueChanged,
    TextBoxRef.TextChanged,
    Xl_Contact1.AfterUpdate,
    DateTimePickerETD.ValueChanged,
    DateTimePickerETA.ValueChanged,
    TextBoxObs.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mPdcConfirm
            .DocFile = Xl_DocFile1.Value
            .Fch = DateTimePickerFch.Value
            .Ref = TextBoxRef.Text
            .Contact = Xl_Contact1.Contact
            .ETD = DateTimePickerETD.Value
            .ETA = DateTimePickerETA.Value
            .Obs = TextBoxObs.Text
            If mDirtyPncs Then
                .Pncs = GetPncsFromGrid
            End If
            .Update()
        End With
        RaiseEvent AfterUpdate(mPdcConfirm, System.EventArgs.Empty)
        Me.Close()
    End Sub


    Private Sub LoadGrid()
        Dim oEmp as DTOEmp = mPdcConfirm.Contact.Emp
        Dim oTb As DataTable = CreateTable()
        Dim oRow As DataRow = Nothing
        Dim oPdc As New Pdc(oEmp, DTOPurchaseOrder.Codis.proveidor)
        For Each oItm As LineItmPnc In mPncs
            If Not (oItm.Pdc.Yea = oPdc.Yea And oItm.Pdc.Id = oPdc.Id) Then
                oPdc = oItm.Pdc
                oRow = oTb.NewRow
                oRow(Cols.LinCod) = LinCods.Pdc
                oRow(Cols.PdcGuid) = oPdc.Guid
                oRow(Cols.Pdc) = oPdc.Id
                oRow(Cols.Lin) = 0
                oRow(Cols.Qty) = 0
                oRow(Cols.Art) = 0
                oRow(Cols.Nom) = oPdc.FullConcepte
                oRow(Cols.Pvp) = 0
                oRow(Cols.Dto) = 0
                oRow(Cols.Amt) = 0
                oTb.Rows.Add(oRow)
            End If

            oRow = oTb.NewRow
            With oItm
                oRow(Cols.LinCod) = LinCods.Pnc
                oRow(Cols.PncGuid) = .Guid
                oRow(Cols.PdcGuid) = .Pdc.Guid
                oRow(Cols.Pdc) = .Pdc.Id
                oRow(Cols.Lin) = .Lin
                oRow(Cols.Qty) = .Pendent
                oRow(Cols.Art) = .Art.Id
                oRow(Cols.Nom) = .Art.NomPrvOrMyd
                oRow(Cols.Pvp) = .Preu.Formatted
                oRow(Cols.Dto) = .Dto
                oRow(Cols.Amt) = .Amt.Formatted
                If .FchConfirm <> Nothing Then
                    oRow(Cols.Fch) = .FchConfirm
                End If
            End With
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.LinCod)
                .Visible = False
            End With
            With .Columns(Cols.PncGuid)
                .Visible = False
            End With
            With .Columns(Cols.PdcGuid)
                .Visible = False
            End With
            With .Columns(Cols.Pdc)
                .Visible = False
            End With
            With .Columns(Cols.Lin)
                .Visible = False
            End With
            With .Columns(Cols.Art)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.qty)
                .HeaderText = "cant"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.pvp)
                .HeaderText = "preu"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.dto)
                .HeaderText = "Dto"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "0\%;-0\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "import"
                .Width = 60
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "entrega"
                .Width = 60
                .DefaultCellStyle.Format = "dd/MM/yy"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With

        End With
    End Sub


    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim BlPdcHeader As Boolean = CType(oRow.Cells(Cols.LinCod).Value, LinCods) = LinCods.Pdc

        If BlPdcHeader Then
            e.CellStyle.BackColor = System.Drawing.Color.SkyBlue
        End If


    End Sub

    Private Function CreateTable() As DataTable
        Dim oTb As New DataTable()
        oTb.Columns.Add("LINCOD", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("PncGuid", System.Type.GetType("System.Guid"))
        oTb.Columns.Add("PdcGuid", System.Type.GetType("System.Guid"))
        oTb.Columns.Add("PDC", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("LIN", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("QTY", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("ART", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("MYD", System.Type.GetType("System.String"))
        oTb.Columns.Add("PVP", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("DTO", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("AMT", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("FCH", System.Type.GetType("System.DateTime"))
        Return oTb
    End Function

    Private Function GetPncsFromGrid() As LineItmPncs
        Dim oEmp as DTOEmp = mPdcConfirm.Contact.Emp
        Dim oPncs As New LineItmPncs
        Dim oPdc As Pdc = Nothing
        Dim oPnc As LineItmPnc = Nothing
        Dim oRow As DataGridViewRow = Nothing
        For Each oRow In DataGridView1.Rows
            Select Case CType(oRow.Cells(Cols.LinCod).Value, LinCods)
                Case LinCods.Pdc
                    Dim oGuid As Guid = oRow.Cells(Cols.PdcGuid).Value
                    oPdc = New Pdc(oGuid)
                Case LinCods.Pnc
                    oPnc = New LineItmPnc(CType(oRow.Cells(Cols.PncGuid).Value, Guid))
                    oPnc.SetItm()
                    oPncs.Add(oPnc)
            End Select
        Next
        Return oPncs
    End Function


    Private Sub DataGridView1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        e.Effect = DragDropEffects.Copy
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
        Dim oPoint = DataGridView1.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = DataGridView1.HitTest(oPoint.X, oPoint.Y)
        If hit.Type = DataGridViewHitTestType.Cell Then
            Dim oclickedCell As DataGridViewCell = DataGridView1.Rows(hit.RowIndex).Cells(hit.ColumnIndex)
            DataGridView1.CurrentCell = oclickedCell
            Dim oPncs As LineItmPncs = e.Data.GetData(GetType(LineItmPncs))
            For Each oitm As LineItmPnc In oPncs
                mPncs.Add(oitm)
            Next
            mDirtyPncs = True
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta confirmacio de comanda?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            mPdcConfirm.DELETE()
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
            Me.Close()
        End If
    End Sub
End Class