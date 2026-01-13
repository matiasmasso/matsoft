

Public Class Frm_Grup_Stat
    Private mClient As client
    Private mAllowEvents As Boolean
    Private mDs(2) As DataSet

    Private Enum Cols
        Guid
        Nom1
        Nom2
        Tot
        Mes
    End Enum

    Private Enum Tabs
        GridClis
        GridArts
    End Enum

    Public WriteOnly Property Client() As client
        Set(ByVal value As client)
            mClient = value
            LoadYeas()
            LoadGridClis()
            mAllowEvents = True
        End Set
    End Property

    Private Sub LoadGridClis()
        Dim SQL As String = ""
        Select Case RadioButtonArc.Checked
            Case True
                SQL = "SELECT  CliClient.Guid, CliClient.ref, '',SUM(ARC.qty * ARC.eur*(100-ARC.DTO)/100) AS TOT "

                For i As Integer = 1 To 12
                    SQL = SQL & ", SUM(CASE WHEN MONTH(ALB.FCH)=" & i & " THEN ARC.qty * ARC.eur*(100-ARC.DTO)/100 ELSE 0 END) AS M" & i & " "
                Next

                SQL = SQL & "FROM ARC INNER JOIN " _
                & "ALB ON ARC.AlbGuid = ALB.Guid INNER JOIN " _
                & "CliClient ON ALB.CliGuid = CliClient.Guid INNER JOIN " _
                & "ART ON ARC.ArtGuid = ART.Guid " _
                & "WHERE Alb.Yea =" & CurrentYea() & " AND " _
                & "(CliClient.Guid ='" & mClient.Guid.ToString & "' OR CliClient.CcxGuid ='" & mClient.Guid.ToString & "') " _
                & "GROUP BY CliClient.Guid, CliClient.ref " _
                & "ORDER BY CliClient.ref"
            Case False
                SQL = "SELECT  CliClient.Guid, CliClient.ref, '',SUM(PNC.qty * PNC.eur*(100-PNC.DTO)/100) AS TOT "

                For i As Integer = 1 To 12
                    SQL = SQL & ", SUM(CASE WHEN MONTH(PDC.FCH)=" & i & " THEN PNC.qty * PNC.eur*(100-PNC.DTO)/100 ELSE 0 END) AS M" & i & " "
                Next

                SQL = SQL & "FROM  PNC INNER JOIN " _
                & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
                & "CliClient ON PDC.CliGuid = CliClient.Guid INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid " _
                & "WHERE Pdc.Yea =" & CurrentYea() & " AND " _
                & "(CliClient.Guid ='" & mClient.Guid.ToString & "' OR CliClient.CcxGuid ='" & mClient.Guid.ToString & "') " _
                & "GROUP BY CliClient.Guid, CliClient.ref " _
                & "ORDER BY CliClient.ref"
        End Select

        mDs(Tabs.GridClis) =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs(Tabs.GridClis).Tables(0)

        With DataGridViewClis
            With .RowTemplate
                .Height = DataGridViewClis.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Nom1)
                .HeaderText = "centro"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Nom2)
                .Visible = False
            End With
            With .Columns(Cols.Tot)
                .HeaderText = "import"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            Dim mes As Integer
            For mes = 1 To 12
                With .Columns(Cols.Mes + mes - 1)
                    .HeaderText = maxisrvr.mes(mes, BLL.BLLApp.Lang)
                    .Width = 65
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            Next
        End With
    End Sub

    Private Sub LoadGridArts()
        Dim SQL As String = ""

        Select Case RadioButtonArc.Checked
            Case True
                SQL = "SELECT  STP.Guid, TPA.DSC,  STP.dsc AS STPDSC, SUM(ARC.qty) AS TOT "

                For i As Integer = 1 To 12
                    SQL = SQL & ", SUM(CASE WHEN MONTH(ARC.FCH)=" & i & " THEN QTY ELSE 0 END) AS M" & i & " "
                Next

                SQL = SQL & "FROM  ARC INNER JOIN " _
                & "ALB ON ARC.AlbGuid = ALB.Guid INNER JOIN " _
                & "CliClient ON ALB.CliGuid = CliClient.Guid INNER JOIN " _
                & "ART ON ARC.ArtGuid = ART.Guid INNER JOIN " _
                & "STP ON ART.Category = STP.Guid INNER JOIN " _
                & "TPA ON STP.Brand = TPA.Guid " _
                & "WHERE Alb.Yea =" & CurrentYea() & " AND " _
                & "(CliClient.Guid ='" & mClient.Guid.ToString & "' OR CliClient.ccxGuid ='" & mClient.Guid.ToString & "') " _
                & "GROUP BY TPA.ORD, Stp.Guid, TPA.DSC, STP.ord, STP.dsc, TPA.DSC " _
                & "ORDER BY TPA.ORD, STP.ord, STPDSC"
            Case False
                SQL = "SELECT  STP.Guid, TPA.DSC, STP.dsc AS STPDSC, SUM(PNC.qty) AS TOT "

                For i As Integer = 1 To 12
                    SQL = SQL & ", SUM(CASE WHEN MONTH(PDC.FCH)=" & i & " THEN QTY ELSE 0 END) AS M" & i & " "
                Next

                SQL = SQL & "FROM  PNC INNER JOIN " _
                & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
                & "CliClient ON PDC.CliGuid = CliClient.Guid INNER JOIN " _
                & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
                & "STP ON ART.Category = STP.Guid INNER JOIN " _
                & "TPA ON STP.Brand = TPA.Guid " _
                & "WHERE Pdc.Yea =" & CurrentYea() & " AND " _
                & "(CliClient.Guid ='" & mClient.Guid.ToString & "' OR CliClient.CcxGuid ='" & mClient.Guid.ToString & "') " _
                & "GROUP BY TPA.ORD, Stp.Guid, TPA.DSC, STP.ord, STP.dsc, TPA.DSC " _
                & "ORDER BY TPA.ORD, STP.ord, STPDSC"
        End Select

        mDs(Tabs.GridArts) =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs(Tabs.GridArts).Tables(0)

        With DataGridViewArts
            With .RowTemplate
                .Height = DataGridViewClis.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Nom1)
                .HeaderText = "marca"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Nom2)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Tot)
                .HeaderText = "totals"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With

            Dim mes As Integer
            For mes = 1 To 12
                With .Columns(Cols.Mes + mes - 1)
                    .HeaderText = maxisrvr.mes(mes, BLL.BLLApp.Lang)
                    .Width = 50
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            Next
        End With
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedTab.Text
            Case TabPageArts.Text
                'Static LoadedArts As Boolean
                'If Not LoadedArts Then
                LoadGridArts()
                'LoadedArts = True
                'End If
        End Select
    End Sub

    Private Sub LoadYeas()
        Dim iCli As Integer = mClient.Id
        Dim SQL As String = "SELECT PDC.YEA FROM PDC INNER JOIN " _
        & "CliClient ON PDC.CliGuid = CliClient.Guid " _
        & "WHERE (PDC.CliGuid =@Guid OR CliClient.CcxGuid = @Guid) " _
        & "GROUP BY PDC.YEA " _
        & "ORDER BY PDC.YEA DESC"

        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, New List(Of Exception), "@Guid", mClient.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        With ComboBoxYea
            .DataSource = oTb
            .DisplayMember = "YEA"
            .ValueMember = "YEA"
            .SelectedIndex = 0
        End With
    End Sub

    Private Function CurrentYea() As Integer
        Dim retval As Integer
        Try
            retval = ComboBoxYea.SelectedValue
        Catch ex As Exception
        End Try
        Return retval
    End Function


    Private Sub ComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYea.SelectedIndexChanged
        If mallowevents Then
            Select Case TabControl1.SelectedTab.Text
                Case TabPageClis.Text
                    LoadGridClis()
                Case TabPageArts.Text
                    LoadGridArts()
            End Select
        End If
    End Sub


    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim oSheet As DTOExcelSheet = BLLExcel.Sheet(mDs(TabControl1.SelectedIndex))
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Function CurrentContact() As Contact
        Dim retval As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridViewClis.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Guid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New Contact(oGuid)
            End If
        End If
        Return retval
    End Function

    Private Function CurrentStp() As Stp
        Dim retval As Stp = Nothing
        Dim oRow As DataGridViewRow = DataGridViewClis.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(Cols.Guid).Value) Then
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                retval = New Stp(oGuid)
            End If
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Select Case TabControl1.SelectedTab.Text
            Case TabPageArts.Text
                Dim oStp As Stp = CurrentStp()

                If oStp IsNot Nothing Then
                    Dim oMenu_Stp As New Menu_ProductCategory(oStp)
                    AddHandler oMenu_Stp.AfterUpdate, AddressOf RefreshRequestArts
                    oContextMenu.Items.AddRange(oMenu_Stp.Range)
                    DataGridViewArts.ContextMenuStrip = oContextMenu
                End If
            Case TabPageClis.Text
                Dim oContact As Contact = CurrentContact()

                If oContact IsNot Nothing Then
                    Dim oMenu_Contact As New Menu_Contact(oContact)
                    AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequestClis
                    oContextMenu.Items.AddRange(oMenu_Contact.Range)
                    DataGridViewClis.ContextMenuStrip = oContextMenu
                End If

        End Select

    End Sub


    Private Sub RefreshRequestClis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom1
        Dim oGrid As DataGridView = DataGridViewClis

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridClis()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub RefreshRequestArts(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom1
        Dim oGrid As DataGridView = DataGridViewArts

        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridArts()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        DataGridViewArts.SelectionChanged, _
         DataGridViewClis.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub RadioButtonArc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonArc.CheckedChanged
        If mAllowEvents Then
            Select Case TabControl1.SelectedIndex
                Case Tabs.GridClis
                    RefreshRequestClis(sender, e)
                Case Tabs.GridArts
                    RefreshRequestArts(sender, e)
            End Select
        End If
    End Sub
End Class