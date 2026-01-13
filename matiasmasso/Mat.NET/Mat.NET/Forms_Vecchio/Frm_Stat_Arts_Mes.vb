

Public Class Frm_Stat_Arts_Mes
    Private mEmp as DTOEmp
    Private mTpa As Tpa
    Private mStp As Stp
    Private mClient As Client
    Private mYea As Integer = Today.Year

    Private mDs As DataSet
    Private mMode As Modes
    Private mAllowEvents As Boolean

    Private Enum Cols
        ArtId
        StpNom
        ArtNom
        Tot
        Eur
        M1
        F1
    End Enum

    Private Enum Modes
        None
        Tpa
        Stp
        Cat
    End Enum

    Public WriteOnly Property Tpa() As Tpa
        Set(ByVal value As Tpa)
            mTpa = value
            mEmp = mTpa.emp
            mMode = Modes.Tpa
            Me.Text = "VENDES " & mTpa.Nom
            LoadYeas()
            If ComboBoxYeas.Items.Count > 0 Then
                LoadGrid()
            End If
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property Stp() As Stp
        Set(ByVal value As Stp)
            mStp = value
            mEmp = mStp.Tpa.emp
            mMode = Modes.Stp
            Me.Text = "VENDES " & mStp.Tpa.Nom & "/" & mStp.Nom
            LoadYeas()
            If ComboBoxYeas.Items.Count > 0 Then
                LoadGrid()
            End If
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property Client() As Client
        Set(ByVal value As Client)
            mClient = value
            If mClient IsNot Nothing Then
                mEmp = mClient.Emp
            End If
        End Set
    End Property

    Public WriteOnly Property Yea() As Integer
        Set(ByVal value As Integer)
            mYea = value
        End Set
    End Property

    Private Sub LoadYeas()
        Dim SQL As String = "SELECT Pdc.Yea FROM PNC INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid " _
        & "INNER JOIN Stp ON Art.Category = Stp.Guid " _
        & "INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid " _
        & "WHERE ART.EMP=" & mEmp.Id & " AND " _
        & "PNC.COD=2 "

        Select Case mMode
            Case Modes.Tpa
                SQL = SQL & "AND Stp.Brand='" & mTpa.Guid.ToString & "' "
            Case Modes.Stp
                SQL = SQL & "AND ART.Category='" & mStp.Guid.ToString & "' "
 
        End Select

        If mClient IsNot Nothing Then
            SQL = SQL & "AND PDC.CLI=" & mClient.Id & " "
        End If

        SQL = SQL & "GROUP BY Pdc.YEA " _
        & "ORDER BY Pdc.YEA DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        If oTb.Rows.Count > 0 Then
            With ComboBoxYeas
                .DisplayMember = "YEA"
                .ValueMember = "YEA"
                .DataSource = oDs.Tables(0)

                If mYea > 0 Then
                    .SelectedValue = mYea
                End If
                If .SelectedIndex < 0 Then
                    .SelectedIndex = 0
                End If
            End With
        End If

    End Sub

    Private Sub LoadGrid()
        Cursor = Cursors.WaitCursor
        Dim SQL As String = "SELECT ART.art, STP.dsc, ART.ord " _
        & ",SUM(PNC.qty) AS TOT, SUM(PNC.QTY*PNC.EUR*(100-PNC.DTO)/100) AS AMT "

        For i As Integer = 1 To 12
            SQL += ", SUM(CASE WHEN MONTH(PDC.FCH) =" & i.ToString & " THEN PNC.QTY ELSE 0 END) AS M" & i.ToString & " "
        Next

        SQL += "FROM  PNC INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "STP ON ART.Category = STP.Guid INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid " _
        & "WHERE ART.emp =" & mEmp.Id & " AND " _
        & "Pdc.yea =" & CurrentYea() & " AND " _
        & "PNC.Cod =" & 2 & " "

        Select Case mMode
            Case Modes.Tpa
                SQL = SQL & "AND Stp.Brand='" & mTpa.Guid.ToString & "' "
            Case Modes.Stp
                SQL = SQL & "AND Stp.Guid='" & mStp.Guid.ToString & "' "

        End Select

        If mClient IsNot Nothing Then
            SQL = SQL & "AND PDC.CLI=" & mClient.Id & " "
        End If

        SQL = SQL & " GROUP BY STP.ord, ART.art, STP.dsc, ART.ord " _
        & "ORDER BY STP.ord, ART.ord"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim iCol As Integer
        Dim oSumRow As DataRow = oTb.NewRow
        For iCol = Cols.Tot To oTb.Columns.Count - 1
            oSumRow(iCol) = 0
        Next

        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            For iCol = Cols.Tot To oTb.Columns.Count - 1
                oSumRow(iCol) += oRow(iCol)
            Next
        Next
        oSumRow(Cols.ArtNom) = "totals"
        oTb.Rows.InsertAt(oSumRow, 0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .BackgroundColor = Color.White

            With .Columns(Cols.ArtId)
                .Visible = False
            End With
            With .Columns(Cols.StpNom)
                .HeaderText = "categoría"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Tot)
                .HeaderText = "total"
                .Width = 50
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 90
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €"
            End With

            Dim oLang As DTOLang = BLL.BLLSession.Current.Lang
            For i As Integer = Cols.M1 To Cols.M1 + 11
                With .Columns(i)
                    .HeaderText = oLang.MesAbr(i - Cols.M1 + 1)
                    .Width = 35
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = "#,###"
                End With
            Next
        End With
        Cursor = Cursors.Default

    End Sub

    Private Sub LoadGridNew()
        Cursor = Cursors.WaitCursor
        Dim SQL As String = "SELECT ART.art, STP.dsc, ART.ord " _
        & ",SUM(PNC.qty) AS TOT, SUM(PNC.QTY*PNC.EUR*(100-PNC.DTO)/100) AS AMT "

        For i As Integer = 1 To 12
            SQL += ", SUM(CASE WHEN MONTH(PDC.FCH) =" & i.ToString & " THEN PNC.QTY ELSE 0 END) AS M" & i.ToString & " "
            SQL += ", MAX(CASE WHEN F.MES=" & i.ToString & " THEN F.QTY ELSE 0 END) AS F" & i.ToString & " "
        Next

        SQL += "FROM  PNC INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "STP ON ART.Category = STP.Guid INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid LEFT OUTER JOIN " _
        & "QFORECAST AS F ON ART.emp = F.emp AND ART.art = F.art AND Pdc.yea = F.yea " _
        & "WHERE ART.emp =" & mEmp.Id & " AND " _
        & "Pdc.yea =" & CurrentYea() & " AND " _
        & "PNC.Cod =" & 2 & " "

        Select Case mMode
            Case Modes.Tpa
                SQL = SQL & "AND Stp.Brand='" & mTpa.Guid.ToString & "' "
            Case Modes.Stp
                SQL = SQL & "AND Stp.Guid='" & mStp.Guid.ToString & "' "
 
        End Select

        If mClient IsNot Nothing Then
            SQL = SQL & "AND PDC.CLI=" & mClient.Id & " "
        End If

        SQL = SQL & " GROUP BY STP.ord, ART.art, STP.dsc, ART.ord " _
        & "ORDER BY STP.ord, ART.ord"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

        Dim iCol As Integer
        Dim oSumRow As DataRow = oTb.NewRow
        For iCol = Cols.Tot To oTb.Columns.Count - 1
            oSumRow(iCol) = 0
        Next

        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            For iCol = Cols.Tot To oTb.Columns.Count - 1
                oSumRow(iCol) += oRow(iCol)
            Next
        Next
        oSumRow(Cols.ArtNom) = "totals"
        oTb.Rows.InsertAt(oSumRow, 0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .BackgroundColor = Color.White

            With .Columns(Cols.ArtId)
                .Visible = False
            End With
            With .Columns(Cols.StpNom)
                .HeaderText = "categoría"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Tot)
                .HeaderText = "total"
                .Width = 50
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 90
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €"
            End With

            Dim oLang As DTOLang = BLL.BLLSession.Current.Lang
            For i As Integer = Cols.M1 To Cols.M1 + 23 Step 2
                With .Columns(i)
                    .HeaderText = oLang.MesAbr(i - Cols.M1 + 1)
                    .Width = 35
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = "#,###"
                End With
                With .Columns(i + 1)
                    .Visible = False
                    '.HeaderText = "f." & oLang.MesAbr(i - Cols.M1 + 1)
                    '.Width = 35
                    '.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    '.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    '.DefaultCellStyle.Format = "#,###"
                End With
            Next
        End With
        Cursor = Cursors.Default

    End Sub

    Private Function CurrentYea()
        Return ComboBoxYeas.SelectedValue
    End Function

    Private Sub ComboBoxYeas_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYeas.SelectedValueChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.ArtId).Value) Then
                Dim ArtId As Integer = oRow.Cells(Cols.ArtId).Value
                oArt = MaxiSrvr.Art.FromNum(mEmp, ArtId)
            End If
        End If
        Return oArt
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oArt As Art = CurrentArt()

        If oArt IsNot Nothing Then
            Dim oMenu_Art As New Menu_Art(oArt)
            'AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub
End Class