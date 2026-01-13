

Public Class Frm_Banc_Vtos

    Private mBanc As Banc
    Private mYea As Integer
    Private mDs As DataSet
    Private mAllowEvents As Boolean

    Private Enum Cols
        Warn
        Ico
        Eur
        Vto
        Txt
        Nom
        Yea
        Csa
        Id
        Impagat
        Reclamat
        CcaVtoCca
    End Enum

    Public WriteOnly Property Banc() As Banc
        Set(ByVal value As Banc)
            mBanc = value
            mYea = GetLastYea()
            LoadGrid()
            SetContextMenu()
            mAllowEvents = True
        End Set
    End Property


    Private Sub LoadGrid()
        Dim SQL As New System.Text.StringBuilder
        SQL.Append("SELECT (CASE WHEN IMPAGAT = 1 THEN 2 WHEN RECLAMAT = 1 THEN 3 WHEN CCAVTOCCA > 0 THEN 1 ELSE 0 END) AS WARN, ")
        SQL.Append("CSB.eur, CSB.vto, CSB.txt, CSB.nom, CSB.yea, CSB.csb, Csb.doc ")
        SQL.Append("FROM CSB INNER JOIN ")
        SQL.Append("CSA ON CSB.yea = CSA.yea AND CSB.Csb = CSA.csb ")
        SQL.Append("WHERE CSA.EMP=" & mBanc.Emp.Id & " AND Csa.bnc =" & mBanc.Id & " AND year(Csb.vto)=" & mYea & " ")
        SQL.Append("ORDER BY VTO DESC, FRA")
        mDs = maxisrvr.GetDataset(SQL.ToString, maxisrvr.Databases.Maxi)

        Dim oTb As DataTable = mDs.Tables(0)
        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

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
            .AllowUserToResizeColumns = True
            With .Columns(Cols.Warn)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 20
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Txt)
                .HeaderText = "concepte"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "lliurat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Csa)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .Visible = False
            End With
            'With .Columns(Cols.Impagat)
            '.Visible = False
            'End With
            'With .Columns(Cols.Reclamat)
            '.Visible = False
            'End With
            'With .Columns(Cols.CcaVtoCca)
            '.Visible = False
            'End With
        End With
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case oRow.Cells(Cols.Warn).Value
                    Case 0
                        e.Value = My.Resources.bell
                    Case 1
                        e.Value = My.Resources.Ok
                    Case 2
                        e.Value = My.Resources.pirata
                    Case 3
                        e.Value = My.Resources.NoPark
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom(sender, e)
    End Sub

    Private Sub Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCsb As Csb = CurrentCsb()
        If oCsb IsNot Nothing Then
            If oCsb.Impagat Then
                Dim oFrm As New Frm_Impagat
                'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest2
                With oFrm
                    .Impagat = New Impagat(oCsb)
                    .Show()
                End With
            Else
                Dim oFrm As New Frm_Csb
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                With oFrm
                    .Csb = oCsb
                    .Show()
                End With
            End If
        End If
    End Sub


    Private Function GetLastYea() As Integer
        Dim SQL As String = "SELECT TOP 1 YEA FROM CSA WHERE EMP=" & mBanc.Emp.Id & " AND BNC=" & mBanc.Id & " GROUP BY YEA ORDER BY YEA DESC"
        Dim iYea As Integer
        Dim oConn As SqlClient.SqlConnection = maxisrvr.GetSQLConnection(maxisrvr.Databases.Maxi)
        Dim SQLCommand As New SqlClient.SqlCommand(SQL, oConn)
        Try
            oConn.open()
            iYea = CInt(SQLCommand.ExecuteScalar())
        Catch ex As Exception

            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "MAT.NET")
        Finally
            oConn.Close()
        End Try
        Return iYea
    End Function

    Private Sub NavigatePreviousYear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonPreviousYear.Click
        mYea = mYea - 1
        LoadGrid()
    End Sub

    Private Sub NavigateNextYear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonNextYear.Click
        mYea = mYea + 1
        LoadGrid()
    End Sub

    Private Sub NavigateYear(ByVal IntYea As Integer)
        LoadGrid()
    End Sub

    Private Sub ShowBancImpagats(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Banc_Impago
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Impagats = GetImpagatsFromCsbs(CurrentCsbs)
            .Show()
        End With

    End Sub

    Private Sub Do_CopyTpv()
        Dim oCsb As Csb = CurrentCsb()
        Dim oImpagat As New Impagat(oCsb)
        Dim url As String = BLL_Tpv.url(oImpagat)
        Clipboard.SetDataObject(url, True)
    End Sub

    Private Sub MailImpagat(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oCsb As Csb = CurrentCsb()
       BLL.BLLIban.Load(oCsb.Iban)
        Dim oImpagat As New Impagat(oCsb)
        Dim oMsg As New ImpagatMsg(oImpagat)
        If oMsg.MailTo > "" Then
            Dim exs as New List(Of exception)
            If Not MatOutlook.NewMessage(oMsg.MailTo, oMsg.MailCc, "", oMsg.Subject, , oMsg.Body, , exs) Then
                UIHelper.WarnError( exs, "error al redactar missatge")
            End If
        Else
            MsgBox("aquest client no te cap adreça de email registrada" & vbCrLf & "cal imprimir el missatge i enviar-lo per fax", MsgBoxStyle.Exclamation, "MAT.NET")
            Dim sMailTo As String = "cuentas@matiasmasso.es"
            Dim exs as New List(Of exception)
            If Not MatOutlook.NewMessage(sMailTo, oMsg.MailCc, "", oMsg.Subject, , oMsg.Body, , exs) Then
                UIHelper.WarnError( exs, "error al redactar missatge")
            End If
        End If
    End Sub

    Private Function CurrentCsb() As Csb
        Dim oCsb As Csb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iYea As Integer = oRow.Cells(Cols.Yea).Value
            Dim iCsa As Integer = oRow.Cells(Cols.Csa).Value
            oCsb = New Csb(MaxiSrvr.Csa.FromNum(mBanc.Emp, iYea, iCsa), oRow.Cells(Cols.Id).Value)
        End If
        Return oCsb
    End Function

    Private Function GetImpagatsFromCsbs(ByVal oCsbs As Csbs) As Impagats
        Dim oImpagats As New Impagats
        For Each oCsb As Csb In oCsbs
            oImpagats.Add(New Impagat(oCsb))
        Next
        Return oImpagats
    End Function

    Private Function CurrentCsbs() As Csbs

        Dim oCsbs As New Csbs
        Dim oCsb As Csb
        Dim oTb As DataTable = mDs.Tables(0)

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim oRow As DataGridViewRow

            For Each oRow In DataGridView1.SelectedRows
                Dim iYea As Integer = oRow.Cells(Cols.Yea).Value
                Dim iCsa As Integer = oRow.Cells(Cols.Csa).Value
                oCsb = New Csb(MaxiSrvr.Csa.FromNum(mBanc.Emp, iYea, iCsa), oRow.Cells(Cols.Id).Value)
                oCsbs.Add(oCsb)
            Next
        Else
            oCsb = CurrentCsb()
            If oCsb IsNot Nothing Then
                oCsbs.Add(oCsb)
            End If
        End If
        Return oCsbs
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCsb As Csb = CurrentCsb()

        If oCsb IsNot Nothing Then
            Dim oMenu_Csb As New Menu_Csb(oCsb)
            AddHandler oMenu_Csb.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Csb.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom

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


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then SetContextMenu()
    End Sub
End Class