

Public Class Frm_Csa
    Private mCsa As Csa
    Private mDs As DataSet
    Private mTot As Decimal

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Doc
        Amt
        Vto
        Result
        Ico
        Nom
        Ccc
        Obs
    End Enum

    Private Enum Resultats
        pendent
        reclamat
        impagat
        vençut
    End Enum

    Public WriteOnly Property Csa() As Csa
        Set(ByVal Value As Csa)
            If Not Value Is Nothing Then
                mCsa = Value
                With mCsa
                    Me.Text = "REMESA DE EFECTES NUM." & .Id
                    LabelBanc.Text = .Banc.Abr
                    DateTimePicker1.Value = .fch
                    PictureBoxBancLogo.Image = .Banc.Img48
                    CheckBoxDescomptat.Checked = .descomptat
                End With
                refresca()
            End If
        End Set
    End Property

    Private Sub refresca()
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT DOC, EUR AS AMT, " _
        & "VTO, (CASE WHEN RECLAMAT=1 THEN " & Resultats.reclamat & " WHEN IMPAGAT=1 THEN " & Resultats.impagat & " WHEN CCAVTOCCA>0 THEN " & Resultats.vençut & " ELSE 0 END) AS RESULT, 0 as ICO, " _
        & "NOM, CCC, TXT AS OBS FROM CSB " _
        & "WHERE " _
        & "CSB.EMP=" & mCsa.emp.Id & " AND " _
        & "CSB.YEA=" & mCsa.yea & " AND " _
        & "CSB.CSB=" & mCsa.Id() & " " _
        & "ORDER BY Csb.DOC"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        'afegeix columna ico
        oTb.Columns.RemoveAt(Cols.Ico)
        Dim oCol As DataColumn = oTb.Columns.Add("RESULTICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)

        mTot = 0
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            mTot += CDbl(oRow(Cols.Amt))
        Next


        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.Doc)
                .HeaderText = "efecte"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "Import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "Venciment"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Result)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "lliurat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Ccc)
                .HeaderText = "compte"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Obs)
                .HeaderText = "concepte"
                .Width = 120
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
        End With

    End Sub


    Private Function CurrentCsb() As Csb
        Dim oCsb As Csb = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oCsb = New Csb(mCsa, oRow.Cells(Cols.Doc).Value)
        End If
        Return oCsb
    End Function


    Private Sub Banc_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles LabelBanc.DoubleClick, PictureBoxBancLogo.DoubleClick
        root.ShowContact(mCsa.Banc)
    End Sub


    Private Sub ToolStripButtonFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonFile.Click
        root.SaveCsaFile(mCsa)
    End Sub

    Private Sub ToolStripButtonPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonPrint.Click
        Dim oCsas As New Csas
        oCsas.Add(mCsa)
        root.PrintCsas(oCsas, maxisrvr.ReportDocument.PrintModes.Copia)
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Retrocedim la remesa?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mCsa.BackUp( exs) Then
                MsgBox("Remesa eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Doc

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If mDs.Tables(0).Rows.Count = 0 Then
            MsgBox("remesa buida!", MsgBoxStyle.Exclamation)
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


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

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Select Case CType(oRow.Cells(Cols.Result).Value, Resultats)
                    Case Resultats.vençut
                        e.Value = My.Resources.Ok
                    Case Resultats.reclamat
                        e.Value = My.Resources.REDO
                    Case Resultats.impagat
                        e.Value = My.Resources.pirata
                    Case Else
                        e.Value = My.Resources.empty
                End Select
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oCsb As Csb = CurrentCsb()
        If oCsb IsNot Nothing Then
            Dim oFrm As New Frm_Csb
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            With oFrm
                .Csb = oCsb
                .Show()
            End With
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mCsa
            .descomptat = CheckBoxDescomptat.CheckAlign
            .fch = DateTimePicker1.Value

            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxDescomptat.CheckedChanged, _
     DateTimePicker1.ValueChanged
        ButtonOk.Enabled = True
    End Sub

End Class