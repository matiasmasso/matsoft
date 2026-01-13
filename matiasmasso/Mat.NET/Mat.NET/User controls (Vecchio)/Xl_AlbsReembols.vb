

Public Class Xl_AlbsReembols
    Public Event AfterUpdate()

    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mTransportista As Transportista
    Private mSelAmt As maxisrvr.Amt = MaxiSrvr.DefaultAmt
    Private WithEvents mAlb As Alb
    Private mAllowEvents As Boolean

    Private Enum Cols
        Chk
        Yea
        Alb
        Fch
        Eur
        Nom
        Cit
    End Enum

    Public WriteOnly Property Transportista() As Transportista
        Set(ByVal value As Transportista)
            mTransportista = value
            LoadGrid()
            mAllowEvents = True
        End Set
    End Property

    Public ReadOnly Property Albs() As Albs
        Get
            Dim oTb As DataTable = mDs.Tables(0)
            Dim oRow As DataRow
            Dim oAlbs As New Albs
            Dim oAlb As Alb
            Dim iYea As Integer
            Dim LngAlb As Long
            Dim i As Integer
            For i = 0 To oTb.Rows.Count - 1
                oRow = oTb.Rows(i)
                If oRow(Cols.Chk) Then
                    iYea = oRow(Cols.Yea)
                    LngAlb = oRow(Cols.Alb)
                    oAlb = MaxiSrvr.Alb.FromNum(mEmp, iYea, LngAlb)
                    oAlbs.Add(oAlb)
                End If
            Next
            Return oAlbs
        End Get
    End Property

    Public ReadOnly Property Amt() As maxisrvr.Amt
        Get
            Return mSelAmt
        End Get
    End Property



    Private Sub LoadGrid()
        Dim SQL As String = "SELECT CAST(0 AS BIT) AS CHECKED, ALB.yea, ALB.alb, ALB.fch, (ALB.Pts + ALB.Pt2) AS Eur, ALB.nom, CIT.CIT " _
        & "FROM ALB left outer join " _
        & "CIT ON CIT.ID=ALB.CITNUM " _
        & "WHERE  ALB.Emp =@EMP AND " _
        & "(ALB.Pq_Trp =@TRP OR (ALB.Pq_Trp=0 AND ALB.MGZ=@TRP)) AND " _
        & "ALB.CashCod =@CASHCOD AND " _
        & "ALB.cobro IS NULL AND " _
        & "(ALB.Pts > 0) " _
        & "ORDER BY ALB.YEA, ALB.ALB"


        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@TRP", mTransportista.Id, "@CASHCOD", DTO.DTOCustomer.CashCodes.Reembols)
        Dim oEur As maxisrvr.Cur = MaxiSrvr.Cur.Eur
        Dim oSum As MaxiSrvr.Amt = New Amt
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            oSum.Add(New maxisrvr.Amt(oRow(Cols.Eur), oEur, oRow(Cols.Eur)))
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Alb)
                .HeaderText = "albará"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .ReadOnly = True
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ReadOnly = True
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "Destinatari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(Cols.Cit)
                .HeaderText = "Població"
                .Width = 200
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .ReadOnly = True
            End With
        End With

        TextBoxTot.Text = oSum.CurFormat
        TextBoxSel.Text = ""

    End Sub

    Private Sub SetTotals()
        Dim oRow As DataGridViewRow
        Dim DblSum As Decimal

        For Each oRow In DataGridView1.Rows
            If oRow.Cells(Cols.Chk).Value Then
                DblSum += oRow.Cells(Cols.Eur).Value
            End If
        Next

        mSelAmt = New maxisrvr.Amt(DblSum, MaxiSrvr.Cur.Eur, DblSum)
        TextBoxSel.Text = Format(DblSum, "#,##0.00 €;-#,##0.00 €;#")
    End Sub

    Private Function CurrentAlb() As Alb
        Dim oAlb As Alb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
            Dim LngId As Long = DataGridView1.CurrentRow.Cells(Cols.Alb).Value
            oAlb = MaxiSrvr.Alb.FromNum(BLL.BLLApp.Emp, iYea, LngId)
        End If
        Return oAlb
    End Function

    Private Function CurrentAlbs() As Albs
        Dim oAlbs As New Albs

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim iYea As Integer
            Dim LngId As Integer
            Dim oAlb As Alb
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                iYea = DataGridView1.CurrentRow.Cells(Cols.Yea).Value
                LngId = oRow.Cells(Cols.Alb).Value
                oAlb = MaxiSrvr.Alb.FromNum(mEmp, iYea, LngId)
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowAlb()
    End Sub

    Private Sub ShowAlb()
        Dim oFrm As New Frm_AlbNew2(CurrentAlb)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowAlb()
            e.Handled = True
        End If
    End Sub

    
    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oAlbs As Albs = CurrentAlbs()

        If oAlbs.Count > 0 Then
            Dim oMenu_Alb As New Menu_Alb(oAlbs)
            AddHandler oMenu_Alb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Alb.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
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

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Chk
                SetTotals()
                RaiseEvent AfterUpdate()
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

End Class
