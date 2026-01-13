
Imports System.Data
Imports System.Data.SqlClient

Public Class Frm_Geo_Pdcs
    Private mAllowEvents As Boolean

    Private Enum Cols
        Cli
        Pais
        Zona
        Cit
        Nom
        Qty
        Eur
    End Enum


    Private Enum GeoLevels
        pais
        zona
        cit
    End Enum

    Private Sub Frm_Geo_Pdcs_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        NumericUpDownYea.Value = Today.Year
        LoadTree(GeoLevels.cit, "Barcelona")
        LoadTpas()
        LoadStps()
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Cli_Geo3.Cli, Country.Nom_ESP as Pais, Cli_Geo3.Zona, Cli_Geo3.Cit, Cli_Geo3.NOM, SUM(PNC.QTY) AS QTY, ROUND(SUM((PNC.qty * PNC.eur) * (100 - PNC.dto) / 100), 2) AS EUR " _
        & "FROM            PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "Cli_Geo3 ON PDC.Emp = Cli_Geo3.emp AND PDC.cli = Cli_Geo3.Cli INNER JOIN " _
        & "CliClient on CliClient.emp=PDC.EMP AND CliClient.Cli=PDC.CLI INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "Country ON Cli_Geo3.ISOpais = Country.ISO " _
        & "WHERE Pdc.Emp = @EMP AND PNC.COD=2 AND Pdc.yea = @YEA and CLICLIENT.NOREP=0 "

        Dim sCurrentPais As String = CurrentPais()
        If sCurrentPais > "" Then
            SQL = SQL & " AND (Country.Nom_Esp LIKE '" & sCurrentPais & "') "
        End If

        Dim sCurrentZona As String = CurrentZona()
        If sCurrentZona > "" Then
            SQL = SQL & " AND (Cli_Geo3.Zona LIKE '" & sCurrentZona & "') "
        End If

        Dim sCurrentCit As String = CurrentCit()
        If sCurrentCit > "" Then
            SQL = SQL & " AND (Cli_Geo3.Cit LIKE @CIT) "
        End If

        Dim oTpa As Tpa = CurrentTpa()
        If oTpa IsNot Nothing Then
            SQL = SQL & " AND (ART.TPA = " & oTpa.Id & ") "

            Dim oStp As Stp = CurrentStp()
            If oStp IsNot Nothing Then
                SQL = SQL & " AND (ART.STP= " & oStp.Id & ") "
            End If
        End If

        SQL = SQL & "GROUP BY Country.Nom_Esp, Cli_Geo3.Zona, Cli_Geo3.Cit, Cli_Geo3.NOM, Cli_Geo3.Cli "
        SQL = SQL & "ORDER BY Country.Nom_Esp, Cli_Geo3.Zona, Cli_Geo3.Cit, Cli_Geo3.NOM "

        Cursor = Cursors.WaitCursor

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@YEA", CurrentYea, "@CIT", sCurrentCit)
        Dim oTb As DataTable = oDs.Tables(0)
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

            With .Columns(Cols.Cli)
                .Visible = False
            End With

            With .Columns(Cols.Pais)
                .Visible = False
            End With

            With .Columns(Cols.Zona)
                If CurrentLevel() < GeoLevels.zona Then
                    .HeaderText = "zona"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .Visible = True
                Else
                    .Visible = False
                End If
            End With

            With .Columns(Cols.Cit)
                If CurrentLevel() < GeoLevels.cit Then
                    .HeaderText = "població"
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .Visible = True
                Else
                    .Visible = False
                End If
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.Qty)
                .HeaderText = "unitats"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
                .DefaultCellStyle.Format = "#"
            End With

            With .Columns(Cols.Eur)
                .HeaderText = "volum"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

        SetContextMenu()

        Cursor = Cursors.Default
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As Contact = CurrentContact

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub LoadTree(oSelectedLevel As GeoLevels, sSelectedText As String)
        Dim oPaisNode As New maxisrvr.TreeNodeObj
        Dim oZonaNode As New maxisrvr.TreeNodeObj
        Dim oCitNode As New maxisrvr.TreeNodeObj

        Dim SQL As String = "SELECT Country.Nom_Esp as Pais, Cli_Geo3.Zona, Cli_Geo3.Cit " _
        & "FROM            PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "Cli_Geo3 ON PDC.Emp = Cli_Geo3.emp AND PDC.cli = Cli_Geo3.Cli INNER JOIN " _
        & "CliClient on CliClient.emp=PDC.EMP AND CliClient.Cli=PDC.CLI INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "Country ON Cli_Geo3.ISOpais = Country.ISO " _
        & "WHERE Pdc.Emp =@EMP AND Pdc.yea = @YEA AND PNC.COD=2 AND CLICLIENT.NOREP=0 " _
        & "GROUP BY Country.Nom_Esp, Cli_Geo3.Zona, Cli_Geo3.Cit " _
        & "ORDER BY Country.Nom_Esp, Cli_Geo3.Zona, Cli_Geo3.Cit"

        Try
            Cursor = Cursors.WaitCursor
            TreeView1.Nodes.Clear()
            Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@YEA", CurrentYea)
            Do While oDrd.Read
                If oDrd("Pais") <> oPaisNode.Text Then
                    oPaisNode = New maxisrvr.TreeNodeObj(oDrd("Pais").ToString)
                    TreeView1.Nodes.Add(oPaisNode)
                    CheckNodeSelection(oPaisNode, oSelectedLevel, sSelectedText)
                End If
                If oDrd("Zona") <> oZonaNode.Text Then
                    oZonaNode = New maxisrvr.TreeNodeObj(oDrd("Zona").ToString)
                    oPaisNode.Nodes.Add(oZonaNode)
                    CheckNodeSelection(oZonaNode, oSelectedLevel, sSelectedText)
                End If
                If oDrd("Cit") <> oCitNode.Text Then
                    oCitNode = New maxisrvr.TreeNodeObj(oDrd("Cit").ToString)
                    oZonaNode.Nodes.Add(oCitNode)
                    CheckNodeSelection(oCitNode, oSelectedLevel, sSelectedText)
                End If
            Loop

            oDrd.Close()
            Cursor = Cursors.Default

        Catch ex As Exception
            Stop
        End Try
    End Sub

    Private Sub CheckNodeSelection(oTestNode As TreeNode, oSelectedLevel As GeoLevels, sSelectedText As String)
        If oTestNode.Level = oSelectedLevel Then
            If oTestNode.Text = sSelectedText Then
                TreeView1.SelectedNode = oTestNode
                oTestNode.EnsureVisible()
            End If
        End If
    End Sub

    Private Function CurrentYea() As Integer
        Dim retval As Integer = NumericUpDownYea.Value
        Return retval
    End Function

    Private Function CurrentTpa() As Tpa
        Dim oTpa As Tpa = Nothing
        Dim oItem As maxisrvr.ListItem = ComboBoxTpa.SelectedItem
        If oItem IsNot Nothing Then
            If oItem.Value > 0 Then
                oTpa = New Tpa(BLL.BLLApp.Emp, oItem.Value)
            End If
        End If
        Return oTpa
    End Function

    Private Function CurrentStp() As Stp
        Dim oStp As Stp = Nothing
        Dim oTpa As Tpa = CurrentTpa()
        If oTpa IsNot Nothing Then
            Dim oItem As maxisrvr.ListItem = ComboBoxStp.SelectedItem
            If oItem IsNot Nothing Then
                If oItem.Value > 0 Then
                    oStp = New Stp(CurrentTpa, oItem.Value)
                End If
            End If
        End If
        Return oStp
    End Function

    Private Function CurrentLevel() As GeoLevels
        If TreeView1.SelectedNode Is Nothing Then TreeView1.SelectedNode = TreeView1.Nodes(0)
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        Dim retVal As Integer = oNode.Level
        Return retVal
    End Function

    Private Function CurrentPais() As String
        Dim retval As String = ""
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        If oNode IsNot Nothing Then
            Select Case oNode.Level
                Case 0
                    retval = oNode.Text
                Case 1
                    retval = oNode.Parent.Text
                Case 2
                    retval = oNode.Parent.Parent.Text
            End Select
        End If
        Return retval
    End Function

    Private Function CurrentZona() As String
        Dim retval As String = ""
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        If oNode IsNot Nothing Then
            Select Case oNode.Level
                Case 1
                    retval = oNode.Text
                Case 2
                    retval = oNode.Parent.Text
            End Select
        End If
        Return retval
    End Function

    Private Function CurrentCit() As String
        Dim retval As String = ""
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        If oNode IsNot Nothing Then
            Select Case oNode.Level
                Case 2
                    retval = oNode.Text
            End Select
        End If
        Return retval
    End Function

    Private Function CurrentContact() As Contact
        Dim retval As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iCli As Integer = oRow.Cells(Cols.Cli).Value
            retval = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, iCli)
        End If
        Return retval
    End Function

    Private Sub LoadTpas(Optional iSelectedTpa As Integer = 0)
        Dim SQL As String = "SELECT TPA.TPA, TPA.DSC " _
        & "FROM            PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "Cli_Geo3 ON PDC.Emp = Cli_Geo3.emp AND PDC.cli = Cli_Geo3.Cli INNER JOIN " _
        & "CliClient on CliClient.emp=PDC.EMP AND CliClient.Cli=PDC.CLI INNER JOIN " _
        & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
        & "Country ON Cli_Geo3.ISOpais = Country.ISO INNER JOIN " _
        & "TPA ON ART.emp = TPA.EMP AND ART.tpa = TPA.TPA " _
        & "WHERE        Pdc.Emp =@EMP AND Pdc.yea = @YEA AND PNC.COD=2 AND CLICLIENT.NOREP=0 "
        SQL = SQL & "GROUP BY TPA.TPA, TPA.DSC, TPA.ORD " _
        & "ORDER BY TPA.ORD"
        ComboBoxTpa.Items.Clear()
        AddComboBoxItem(ComboBoxTpa, "(totes les marques)", 0, iSelectedTpa)

        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@YEA", CurrentYea)
        Do While oDrd.Read
            AddComboBoxItem(ComboBoxTpa, oDrd("DSC").ToString, CInt(oDrd("TPA")), iSelectedTpa)
        Loop
        oDrd.Close()
    End Sub

    Private Sub LoadStps(Optional iSelectedStp As Integer = 0)
        ComboBoxStp.Items.Clear()
        AddComboBoxItem(ComboBoxStp, "(totes les categories)", 0, iSelectedStp)

        Dim oCurrentTpa As Tpa = CurrentTpa()
        If oCurrentTpa IsNot Nothing Then
            Dim SQL As String = "SELECT STP.STP, STP.DSC " _
            & "FROM            PNC INNER JOIN " _
            & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
            & "Cli_Geo3 ON PDC.Emp = Cli_Geo3.emp AND PDC.cli = Cli_Geo3.Cli INNER JOIN " _
            & "CliClient on CliClient.emp=PDC.EMP AND CliClient.Cli=PDC.CLI INNER JOIN " _
            & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
            & "Country ON Cli_Geo3.ISOpais = Country.ISO INNER JOIN " _
            & "Stp ON ART.Category = Stp.Guid INNER JOIN " _
            & "Tpa ON Stp.Brand = Tpa.Guid " _
            & "WHERE        Pdc.Emp =@EMP AND Pdc.yea = @YEA AND Tpa.Guid = '" & CurrentTpa.Guid.ToString & "' AND PNC.COD=2 AND CLICLIENT.NOREP=0 "
            SQL = SQL & "GROUP BY  STP.STP, STP.DSC, TPA.ORD " _
            & "ORDER BY TPA.ORD"

            Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", App.Current.Emp.Id, "@YEA", CurrentYea)
            Do While oDrd.Read
                AddComboBoxItem(ComboBoxStp, oDrd("DSC").ToString, CInt(oDrd("STP")), iSelectedStp)
            Loop
            oDrd.Close()
        End If

    End Sub

    Private Sub AddComboBoxItem(oComboBox As ComboBox, sText As String, iValue As Integer, iSelectedValue As Integer)
        Dim oItem As New maxisrvr.ListItem(iValue, sText)
        oComboBox.Items.Add(oItem)
        If iSelectedValue = iValue Then oComboBox.SelectedItem = oItem
    End Sub


    Private Sub ComboBoxTpa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ComboBoxTpa.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            LoadStps()
            LoadGrid()
            mAllowEvents = True
        End If
    End Sub

    Private Sub ComboBoxStp_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ComboBoxStp.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            LoadGrid()
            mAllowEvents = True
        End If
    End Sub

    Private Sub NumericUpDownYea_ValueChanged(sender As Object, e As System.EventArgs) Handles NumericUpDownYea.ValueChanged
        If mAllowEvents Then
            mAllowEvents = False
            Dim oSelectedNode As TreeNode = TreeView1.SelectedNode
            LoadTree(oSelectedNode.Level, oSelectedNode.Text)
            LoadTpas()
            LoadStps()
            LoadGrid()
            mAllowEvents = True
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If mAllowEvents Then
            mAllowEvents = False
            LoadTpas()
            LoadStps()
            LoadGrid()
            mAllowEvents = True
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = DataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

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

    Private Sub ButtonExcel_Click(sender As Object, e As System.EventArgs) Handles ButtonExcel.Click
        MatExcel.GetExcelFromDataGridView(DataGridView1).Visible = True
    End Sub
End Class