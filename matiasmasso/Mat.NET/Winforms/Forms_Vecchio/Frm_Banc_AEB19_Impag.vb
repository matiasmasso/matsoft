

Public Class Frm_Banc_AEB19_Impag
    Private mBanc As MaxiSrvr.Banc
    Private mDsCsas As DataSet
    Private mDsCsbs As DataSet
    Private mAllowEvents As Boolean

    Private Enum ColsCsas
        yea
        Csa
        Vto
    End Enum

    Private Enum ColsCsbs
        Chk
        Doc
        Eur
        Cli
        Nom
        Impagat
        Reclamat
    End Enum

    Public WriteOnly Property Banc() As MaxiSrvr.Banc
        Set(ByVal Value As MaxiSrvr.Banc)
            mBanc = Value
            Me.Text = Me.Text & " " & BLL.BLLIban.BankNom(mBanc.Iban)
            LoadGridCsas()
            If mDsCsas.Tables(0).Rows.Count > 0 Then
                LoadGridCsbs(CurrentCsa)
            End If
            mAllowEvents = True
        End Set
    End Property

    Private Function CurrentCsa() As Csa
        Dim oCsa As Csa = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCsas.CurrentRow
        If oRow IsNot Nothing Then
            Dim IntYea As Integer = DataGridViewCsas.CurrentRow.Cells(ColsCsas.yea).Value
            Dim LngId As Integer = DataGridViewCsas.CurrentRow.Cells(ColsCsas.Csa).Value
            oCsa = MaxiSrvr.Csa.FromNum(BLL.BLLApp.Emp, IntYea, LngId)
        End If
        Return oCsa
    End Function

    Private Sub LoadGridCsas()
        Dim SQL As String = "SELECT yea, csb, fch " _
        & " FROM Csa " _
        & "WHERE  emp=" & mBanc.Emp.Id & " AND " _
        & "bnc=" & mBanc.Id & " AND " _
        & "descomptat= 0 " _
        & "ORDER BY yea DESC, csb DESC"
        mDsCsas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsCsas.Tables(0)
        With DataGridViewCsas
            With .RowTemplate
                .Height = DataGridViewCsas.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(ColsCsas.yea)
                .Visible = False
            End With
            With .Columns(ColsCsas.Csa)
                .HeaderText = "Remesa"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsCsas.Vto)
                .HeaderText = "Venciment"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With
    End Sub

    Private Sub LoadGridCsbs(ByVal oCsa As Csa)
        Dim SQL As String = "SELECT CAST (0 AS BIT) AS checked, Doc, eur, cli, nom, impagat, reclamat " _
        & "FROM Csb " _
        & "WHERE Emp =" & mBanc.Emp.Id & " AND " _
        & "yea =" & oCsa.yea & " AND " _
        & "Csb =" & oCsa.Id & " " _
        & "ORDER BY Doc"
        mDsCsbs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsCsbs.Tables(0)
        With DataGridViewCsbs
            With .RowTemplate
                .Height = DataGridViewCsbs.Font.Height * 1.35
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(ColsCsbs.Chk)
                .HeaderText = ""
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(ColsCsbs.Doc)
                .Visible = False
            End With
            With .Columns(ColsCsbs.Eur)
                .HeaderText = "Import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
            With .Columns(ColsCsbs.Cli)
                .Visible = False
            End With
            With .Columns(ColsCsbs.Nom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(ColsCsbs.Impagat)
                .Visible = False
            End With
            With .Columns(ColsCsbs.Reclamat)
                .Visible = False
            End With
        End With
    End Sub


    Private Function SelectedCsbs() As Csbs
        Dim IntDoc As Integer
        Dim oCsbs As New Csbs
        Dim oCsa As Csa = CurrentCsa()
        Dim oCsb As Csb
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridViewCsbs.Rows
            If oRow.Cells(ColsCsbs.Chk).Value Then
                IntDoc = oRow.Cells(ColsCsbs.Doc).Value
                oCsb = New Csb(oCsa, IntDoc)
                oCsbs.Add(oCsb)
            End If
        Next
        Return oCsbs
    End Function

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oCsbs As Csbs = SelectedCsbs()
        Dim DtFch As Date = DateTimePicker1.Value
        Dim oPlan As PgcPlan = PgcPlan.FromYear(DtFch.Year)
        Dim oCtaDespeses As PgcCta = oPlan.Cta(DTOPgcPlan.ctas.despesesImpago)
        Dim exs as New List(Of exception)
        If oCsbs.Registra_Impagat_AEB19(mBanc, DtFch, exs) Then
            MsgBox("Registrat en compte: " & oCtaDespeses.FullNom, MsgBoxStyle.Information, "M+O")
            Me.Close()
        Else
            MsgBox("error al desar els impagats" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub EnableButtons()
        ButtonOk.Enabled = (SelectedCsbs.Count > 0)
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


    Private Sub DataGridViewCsas_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewCsas.SelectionChanged
        If mAllowEvents Then
            LoadGridCsbs(CurrentCsa)
        End If
    End Sub



    Private Sub DataGridViewCsbs_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewCsbs.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridViewCsbs.Rows(e.RowIndex)
        Dim BlImpagat As Boolean = oRow.Cells(ColsCsbs.Impagat).Value
        If BlImpagat Then
            oRow.DefaultCellStyle.BackColor = Color.LightSalmon
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub DataGridViewCsbs_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewCsbs.CellValueChanged
        Select Case e.ColumnIndex
            Case ColsCsbs.Chk
                EnableButtons()
        End Select
    End Sub

    Private Sub DataGridViewCsbs_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewCsbs.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridViewCsbs.CurrentCell.ColumnIndex
            Case ColsCsbs.Chk
                DataGridViewCsbs.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub
End Class
