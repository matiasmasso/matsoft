Imports System.Data.SqlClient

Imports System.Xml

Public Class Frm_Mailing

    Private mDs As DataSet
    Private mFirstTime As Boolean
    Private mEmailAdrs As Boolean
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsTpas As DataSet
    Private mDsZons As DataSet
    Private mAllowEvents As Boolean

    Private Enum SelTarifa
        Totes
        ExclouTarifaB
        NomesTarifaB
    End Enum

    Private Enum ColsAdr
        Nom
        Adr
        ZipyCit
        Provincia
        Pais
        Zona
        Cit
        Id
    End Enum

    Private Enum ColsEMail
        Nom
        Adr
        Pais
        Zona
        Cit
        Id
    End Enum

    Private Sub MakeDataSet()
        mFirstTime = True
        If CheckBoxReps.Checked Then AddReps()
        If CheckBoxSalesmen.Checked Then AddSalesmen()
        If CheckBoxClis.Checked Then AddClis()
        If CheckBoxCliMasters.Checked Then AddMasters()
        If CheckBoxCliSalePoints.Checked Then AddCliSalePoints()
        'If CheckBoxReps.Checked Then AddReps()
        If CheckBoxOnLineShops.Checked Then AddOnLineShops()
        If CheckBoxPrv.Checked Then AddPrvs()
        Dim oTb As DataTable = mDs.Tables(0)
        ShowTable()
    End Sub

    Private Sub ShowTable()
        With DataGridView1
            '.DataSource = mDs.Tables(0).DefaultView
            '.DataMember = "CLI"
        End With
        If mEmailAdrs Then
            FormatGridEmails()
        Else
            FormatGridAdrs()
        End If
    End Sub

    Private Sub FormatGridEmails()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0).DefaultView
            '.DataMember = "CLI"
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColsEMail.Nom)
                .HeaderText = "destinatari"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsEMail.Adr)
                .HeaderText = "adreça"
                .Width = 200
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsEMail.Pais)
                .HeaderText = "pais"
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsEMail.Zona)
                .HeaderText = "zona"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsEMail.Cit)
                .HeaderText = "cit"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsEMail.Id)
                .HeaderText = "Id"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

        End With
    End Sub

    Private Sub FormatGridAdrs()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0).DefaultView
            .DataMember = "CLI"
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColsAdr.Nom)
                .HeaderText = "destinatari"
                .Width = 200
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsAdr.Adr)
                .HeaderText = "adreça"
                .Width = 200
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsAdr.ZipyCit)
                .HeaderText = "adreça"
                .Width = 200
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsAdr.Provincia)
                .HeaderText = "adreça"
                .Width = 200
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsAdr.Pais)
                .HeaderText = "pais"
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsAdr.Zona)
                .HeaderText = "zona"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsAdr.Cit)
                .HeaderText = "cit"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsAdr.Id)
                .HeaderText = "Id"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

        End With
    End Sub

    Private Sub AddReps()
        Dim SQL As String = "SELECT  CliRep.Emp, CliRep.Cli, CliRep.Abr as Clx " _
        & "FROM            RepProducts " _
        & "INNER JOIN ProductParent ON RepProducts.Product = ProductParent.ChildGuid " _
        & "INNER JOIN CliRep ON RepProducts.Rep = CliRep.Guid " _
        & "AND (RepProducts.FchFrom < GETDATE()) AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo > GETDATE()) " _
        & "AND CliRep.ComStd > 0 "

        If Not CheckBoxAllTpas.Checked Then
            Dim i As Integer
            Dim oTb As DataTable = mDsTpas.Tables(0)
            Dim oRow As DataRow
            Dim AnyChecked As Boolean
            For i = 0 To oTb.Rows.Count - 1
                If CheckedListBoxTpas.GetItemChecked(i) Then
                    If Not AnyChecked Then
                        SQL = SQL & " AND ("
                        AnyChecked = True
                    Else
                        SQL = SQL & " OR "
                    End If
                    oRow = oTb.Rows(i)
                    SQL = SQL & "ProductParent.ParentGuid ='" & oRow("Guid").ToString & "' "
                End If
            Next

            If AnyChecked Then
                SQL = SQL & ") "
                AnyChecked = True
            End If

        End If

        SQL = SQL & "GROUP BY CliRep.emp, CliRep.cli, CliRep.Abr"

        AddTable(SQL, "CLI", mDs)
    End Sub

    Private Sub AddSalesmen()
        Dim SQL As String = "SELECT EMP, CLI, ABR AS CLX " _
        & "FROM CliRep " _
        & "WHERE CLIREP.EMP=1 AND " _
        & "Hasta IS NULL AND " _
        & "ComStd = 0"
        AddTable(SQL, "CLI", mDs)
    End Sub

    Private Sub AddClis()
        Dim SQL As String = "SELECT CLX.EMP, CLX.CLI, CLX.CLX " _
        & "FROM PDC INNER JOIN " _
        & "CLX ON PDC.CLI=CLX.CLI AND PDC.EMP=CLX.EMP INNER JOIN " _
        & "CLIGRAL ON PDC.CLI=CLIGRAL.CLI AND PDC.EMP=CLIGRAL.EMP "

        Select Case ComboBoxTarifa.SelectedIndex
            Case SelTarifa.Totes
            Case SelTarifa.ExclouTarifaB
                SQL = SQL & " INNER JOIN CLICLIENT ON CLICLIENT.EMP=CLIGRAL.EMP AND CLICLIENT.CLI=CLIGRAL.CLI AND CLICLIENT.TARIFA<>" & Client.Tarifas.Virtual & " "
            Case SelTarifa.NomesTarifaB
                SQL = SQL & " INNER JOIN CLICLIENT ON CLICLIENT.EMP=CLIGRAL.EMP AND CLICLIENT.CLI=CLIGRAL.CLI AND CLICLIENT.TARIFA=" & Client.Tarifas.Virtual & " "
        End Select

        If Not CheckBoxAllTpas.Checked Then
            SQL = SQL & " INNER JOIN PNC ON PDC.Guid=Pnc.PdcGuid " _
                & "INNER JOIN ProductParent ON Pnc.ArtGuid = ProductParent.ChildGuid "
        End If


            SQL = SQL & "WHERE PDC.EMP=1 AND " _
            & "PDC.EUR>0 AND " _
            & "PDC.FCH>'" & Format(DateTimePicker1.Value, "yyyyMMdd") & "' AND " _
            & "PDC.COD=2 AND " _
            & "CLX.EX=0 AND " _
            & "CLX.NOMAIL=0 AND " _
            & "CLIGRAL.BOTIGA=1 "

            If Not CheckBoxAllTpas.Checked Then
                Dim i As Integer
                Dim oTb As DataTable = mDsTpas.Tables(0)
                Dim oRow As DataRow
                Dim AnyChecked As Boolean
                For i = 0 To oTb.Rows.Count - 1
                    If CheckedListBoxTpas.GetItemChecked(i) Then
                        If Not AnyChecked Then
                            SQL = SQL & " AND ("
                            AnyChecked = True
                        Else
                            SQL = SQL & " OR "
                        End If
                        oRow = oTb.Rows(i)
                    SQL = SQL & "ProductParent.ParentGuid ='" & oRow("Guid").ToString & "' "
                End If
                Next

                If AnyChecked Then
                    SQL = SQL & ") "
                    AnyChecked = True
                End If

            End If


            SQL = SQL & "GROUP BY CLX.EMP, CLX.CLI, CLX.CLX"
            AddTable(SQL, "CLI", mDs)
    End Sub

    Private Sub AddPrvs()
        Dim SQL As String = "SELECT CLX.EMP, CLX.cli, CLX.clx " _
        & "FROM CliPrv INNER JOIN " _
        & "CLX ON CliPrv.emp = CLX.Emp AND CliPrv.cli = CLX.cli " _
        & "WHERE CliPrv.emp = 1 AND CLX.EX = 0 " _
        & "GROUP BY CLX.EMP, CLX.CLI, CLX.CLX"
        AddTable(SQL, "CLI", mDs)
    End Sub

    Private Sub AddMasters()
        Dim SQL As String = "SELECT CLX.EMP, CLX.cli, CLX.clx " _
        & "FROM PDC INNER JOIN " _
        & "CLIGRAL ON PDC.CLI=CLIGRAL.CLI AND PDC.EMP=CLIGRAL.EMP INNER JOIN " _
        & "CliClient ON PDC.Emp = CliClient.Emp AND PDC.cli = CliClient.Cli INNER JOIN " _
        & "CLX ON CliClient.Emp = CLX.Emp AND CliClient.ccx = CLX.cli " _
        & "WHERE PDC.EMP=1 AND " _
        & "PDC.EUR>0 AND " _
        & "PDC.FCH>'" & Format(DateTimePicker1.Value, "yyyyMMdd") & "' AND " _
        & "PDC.COD=2 AND " _
        & "CLX.EX=0 AND " _
        & "CLX.NOMAIL=0 AND " _
        & "CLIGRAL.BOTIGA=1 "

        Select Case ComboBoxTarifa.SelectedIndex
            Case SelTarifa.Totes
            Case SelTarifa.ExclouTarifaB
                SQL = SQL & " AND CLICLIENT.TARIFA<>" & Client.Tarifas.Virtual & " "
            Case SelTarifa.NomesTarifaB
                SQL = SQL & " AND CLICLIENT.TARIFA=" & Client.Tarifas.Virtual & " "
        End Select

        SQL = SQL & "GROUP BY CLX.EMP, CLX.cli, CLX.clx"
        AddTable(SQL, "CLI", mDs)
    End Sub

    Private Sub AddCliSalePoints()
        Dim SQL As String = "SELECT CLX.EMP, CLX.cli, CLX.clx " _
        & "FROM PDC INNER JOIN " _
        & "CliClient ON PDC.Emp = CliClient.Emp AND PDC.cli = CliClient.Cli INNER JOIN " _
        & "CliClient CliClient_1 ON CliClient.Emp = CliClient_1.Emp AND CliClient.ccx = CliClient_1.ccx AND CliClient.Cli <> CliClient_1.Cli INNER JOIN " _
        & "CLX ON CliClient_1.Emp = CLX.Emp AND CliClient_1.Cli = CLX.cli "


        If Not CheckBoxAllTpas.Checked Then
            Dim i As Integer
            Dim oTb As DataTable = mDsTpas.Tables(0)
            Dim oRow As DataRow
            Dim AnyChecked As Boolean
            For i = 0 To oTb.Rows.Count - 1
                If CheckedListBoxTpas.GetItemChecked(i) Then
                    If Not AnyChecked Then
                        SQL = SQL & " INNER JOIN PNC ON PDC.Guid=PNC.PdcGuid " _
                        & "INNER JOIN ProductParent ON Pnc.ArtGuid = ProductParent.ChildGuid AND ("
                        AnyChecked = True
                    Else
                        SQL = SQL & " OR "
                    End If
                    oRow = oTb.Rows(i)
                    SQL = SQL & "ProductParent.ParentGuid ='" & oRow("Guid").ToString & "' "
                End If
            Next

            If AnyChecked Then
                SQL = SQL & ") "
                AnyChecked = True
            End If

        End If


        SQL = SQL & "WHERE PDC.EMP=1 AND " _
        & "PDC.EUR>0 AND " _
        & "PDC.FCH>'" & Format(DateTimePicker1.Value, "yyyyMMdd") & "' AND " _
        & "PDC.COD=2 AND " _
        & "CLX.EX=0 AND " _
        & "CLX.NOMAIL=0 AND " _
        & "CliClient.ccx > 0 "

        Select Case ComboBoxTarifa.SelectedIndex
            Case SelTarifa.Totes
            Case SelTarifa.ExclouTarifaB
                SQL = SQL & " AND CLICLIENT.TARIFA<>" & Client.Tarifas.Virtual & " "
            Case SelTarifa.NomesTarifaB
                SQL = SQL & " AND CLICLIENT.TARIFA=" & Client.Tarifas.Virtual & " "
        End Select

        SQL = SQL & "GROUP BY CLX.EMP,CLX.cli, CLX.clx"
        AddTable(SQL, "CLI", mDs)
    End Sub

    Private Sub AddOnLineShops()
        Dim SQL As String = "SELECT CLX.EMP, CLX.CLI, ESHOP.NOM " _
        & "FROM ESHOP INNER JOIN " _
        & "CLX ON ESHOP.emp = CLX.Emp AND ESHOP.CLI=CLX.CLI "

        Select Case ComboBoxTarifa.SelectedIndex
            Case SelTarifa.Totes
            Case SelTarifa.ExclouTarifaB
                SQL = SQL & " INNER JOIN CLICLIENT ON CLICLIENT.EMP=CLX.EMP AND CLICLIENT.CLI=CLX.CLI AND CLICLIENT.TARIFA<>" & Client.Tarifas.Virtual & " "
            Case SelTarifa.NomesTarifaB
                SQL = SQL & " INNER JOIN CLICLIENT ON CLICLIENT.EMP=CLX.EMP AND CLICLIENT.CLI=CLX.CLI AND CLICLIENT.TARIFA=" & Client.Tarifas.Virtual & " "
        End Select

        SQL = SQL & "WHERE CLX.EMP=1 AND " _
        & "CLX.EX = 0"
        AddTable(SQL, "CLI", mDs)
    End Sub

    Private Sub AddTable(ByVal sSQL As String, ByVal sTableName As String, ByRef oDataSet As DataSet)
        Dim SQL As String

        If mEmailAdrs Then
            SQL = "SELECT MIN(EMAIL.NOM) AS NOM, EMAIL.ADR, MIN(EMAIL.ISOPAIS) AS ISOPAIS, MIN(EMAIL.ZONA) AS ZONA, MIN(EMAIL.CIT) AS CIT, MIN(EMAIL.CLI) AS CLI " _
            & "FROM (" & SQLEMAIL() & ") EMAIL INNER JOIN " _
            & "(" & sSQL & ") SOURCE " _
            & "ON EMAIL.EMP=SOURCE.EMP AND EMAIL.CLI=SOURCE.CLI "

            SQL = SQL & "GROUP BY EMAIL.ADR " _
            & "ORDER BY ISOPAIS, ZONA, CIT"
        Else
            SQL = "SELECT GEO.NOM, GEO.ADR, GEO.POBLACIO, GEO.PROVINCIA, GEO.ISOPAIS, GEO.ZONA, GEO.CIT, GEO.CLI FROM (" & SQLGEO() & ") GEO INNER JOIN " _
            & "(" & sSQL & ") SOURCE " _
            & "ON GEO.EMP=SOURCE.EMP AND GEO.CLI=SOURCE.CLI " _
            & "GROUP BY GEO.NOM, GEO.ADR, GEO.POBLACIO, GEO.PROVINCIA, GEO.ISOPAIS, GEO.ZONA, GEO.CIT, GEO.CLI " _
            & "ORDER BY ISOPAIS, ZONA, CIT"
        End If


        Dim oConn As SqlConnection = Nothing

        Try
            If mFirstTime Then mDs = New DataSet

            oConn = maxisrvr.GetSQLConnection(maxisrvr.Databases.Maxi)
            Dim oDA As SqlDataAdapter = maxisrvr.GetSQLDataAdapter(SQL, oConn)
            oDA.Fill(oDataSet)

            If mFirstTime Then
                mFirstTime = False
                DefinePrivateKey()
            End If
        Catch ex As SqlException
            MsgBox(ex.Message)
        Finally
            oConn.Close()
            Dim i As Integer
            If mDs.Tables.Count > 0 Then
                i = mDs.Tables(0).Rows.Count
            End If
            GroupBoxPrint.Text = "Sortida (" & i & " adreçes)"
        End Try
    End Sub

    Private Sub EnableButtons()
        Dim BlRecordsExist As Boolean
        If Not mDs Is Nothing Then
            If mDs.Tables.Count > 0 Then
                If mDs.Tables(0).Rows.Count > 0 Then
                    BlRecordsExist = True
                End If
            End If
        End If

        ButtonEtqs.Enabled = BlRecordsExist
        ButtonExcel.Enabled = BlRecordsExist
        If BlRecordsExist Then
            ButtonEmails.Enabled = mEmailAdrs
            ButtonAdrs.Enabled = Not ButtonEmails.Enabled
        Else
            ButtonEmails.Enabled = CheckBoxReps.Checked _
            Or CheckBoxSalesmen.Checked _
            Or CheckBoxClis.Checked _
            Or CheckBoxCliMasters.Checked _
            Or CheckBoxCliSalePoints.Checked _
            Or CheckBoxOnLineShops.Checked
            ButtonAdrs.Enabled = ButtonEmails.Enabled
        End If
    End Sub

    Private Sub DefinePrivateKey()
        Dim oTb As DataTable = mDs.Tables(0)
        Dim Cols As Integer = oTb.Columns.Count
        Dim myKey(Cols) As DataColumn
        Dim i As Integer
        For i = 0 To Cols - 1
            myKey(i) = oTb.Columns(i)
        Next
        oTb.PrimaryKey = myKey
    End Sub

    Private Function SQLEMAIL() As String
        Dim SQL As String = "SELECT " _
        & "CliAdr.emp, CliAdr.cli, EMAIL.ADR, " _
        & "CLIGRAL.RAOSOCIAL+(CASE WHEN CLIGRAL.NOMCOM > '' THEN ' ''' + CLIGRAL.NOMCOM + '''' ELSE '' END) AS NOM, " _
        & "Country.ISO AS ISOPAIS, Zona.Nom AS ZONA, Location.Nom AS CIT " _
        & "FROM CLIGRAL INNER JOIN " _
        & "CLIADR ON CLIGRAL.EMP = CLIADR.EMP AND CLIGRAL.CLI = CLIADR.CLI INNER JOIN " _
        & "Zip ON CliAdr.Zip=Zip.Guid INNER JOIN " _
        & "Location ON Zip.Location=Location.Guid INNER JOIN " _
        & "Zona ON Location.Zona=Zona.Guid INNER JOIN " _
        & "Country ON Zona.Country=Country.Guid INNER JOIN " _
        & "EMAIL_CLIS ON CLIGRAL.Guid=EMAIL_CLIS.ContactGuid INNER JOIN " _
        & "EMAIL ON EMAIL.Guid=EMAIL_CLIS.EmailGuid AND EMAIL.OBSOLETO=0 "

        If Not CheckBoxAllZons.Checked Then
            SQL = SQL & "INNER JOIN (" & SQLCLIZONAS() & ") Z ON CliAdr.EMP=Z.EMP AND CliAdr.CLI=Z.CLI "
        End If


        SQL = SQL & "WHERE EMAIL.BadMail=0 AND EMAIL.OBSOLETO = 0 "

        If CheckBoxNoSpam.Checked Then
            SQL = SQL & "AND EMAIL.GUID NOT IN (SELECT SSCEMAIL.EMAIL FROM SSCEMAIL WHERE SSCEMAIL.SSC=" & CInt(DTOSubscription.Ids.Noticias).ToString & ") "
        End If

        Select Case ComboBoxLang.SelectedIndex
            Case 0
            Case 1
                SQL = SQL & "AND EMAIL.LANG LIKE 'ESP' "
            Case 2
                SQL = SQL & "AND EMAIL.LANG LIKE 'CAT' "
            Case 3
                SQL = SQL & "AND EMAIL.LANG LIKE 'ENG' "
        End Select

        SQL = SQL & "GROUP BY CliAdr.emp, CliAdr.cli, EMAIL.ADR, CLIGRAL.RAOSOCIAL, CLIGRAL.NOMCOM, Country.ISO, Zona.Nom, Location.Nom"

        Return SQL
    End Function

    Private Function SQLGEO() As String
        Dim SQL As String = "SELECT " _
        & "CliAdr.emp, CliAdr.cli, " _
        & "(CASE WHEN CLIGRAL.NOMCOM LIKE '' THEN CLIGRAL.RAOSOCIAL ELSE CLIGRAL.NOMCOM END) AS NOM, " _
        & "CliAdr.adr, " _
        & "Zip.ZipCod + (CASE WHEN Zip.ZipCod > '' THEN ' ' ELSE '' END) + Location.Nom AS POBLACIO, " _
        & "(CASE WHEN Country.ISO LIKE 'ES' THEN " _
        & "(CASE WHEN PROVINCIA.NOM IS NULL OR PROVINCIA.NOM = Location.Nom THEN '' ELSE '(' + PROVINCIA.NOM + ')' END) " _
        & "ELSE '(' + Country.NOM_ESP + ')' END)AS PROVINCIA, " _
        & "Country.ISO AS ISOPAIS, Zona.Nom AS Zona, Location.Nom AS CIT " _
        & "FROM CLIGRAL INNER JOIN " _
        & "CLIADR ON CLIGRAL.EMP = CLIADR.EMP AND CLIGRAL.CLI = CLIADR.CLI INNER JOIN " _
        & "(SELECT emp, cli, MAX(cod) AS cod " _
        & "FROM CliAdr " _
        & "WHERE CliAdr.EMP=1 AND " _
        & "(cod BETWEEN 1 AND 2) " _
        & "GROUP BY CliAdr.emp, CliAdr.cli) B " _
        & "ON CliAdr.emp = B.emp AND CliAdr.cli = B.cli AND CliAdr.cod = B.cod INNER JOIN " _
        & "Zip ON CliAdr.Zip=Zip.Guid INNER JOIN " _
        & "Location ON Zip.Location=Location.Guid INNER JOIN " _
        & "Zona ON Location.Zona=Zona.Guid INNER JOIN " _
        & "Country ON Zona.Country=Country.Guid LEFT OUTER JOIN " _
        & "Provincia ON Zona.Provincia=Provincia.Guid "

        If Not CheckBoxAllZons.Checked Then
            SQL = SQL & "INNER JOIN (" & SQLCLIZONAS() & ") Z ON B.EMP=Z.EMP AND B.CLI=Z.CLI "
        End If

        If Not CheckBoxAllTpas.Checked Then
            SQL = SQL & "INNER JOIN (" & SQLCLITPAS() & ") T ON B.EMP=T.EMP AND B.CLI=T.CLI "
        End If

        Return SQL

    End Function


    Private Function SQLCLIZONAS() As String
        Dim SQL As String = "SELECT EMP, CLI FROM CLIADR INNER JOIN " _
                            & "Zip ON CliAdr.Zip=Zip.Guid INNER JOIN " _
                            & "Location ON Zip.Location=Location.Guid WHERE "

        Dim i As Integer
        Dim oTb As DataTable = mDsZons.Tables(0)
        Dim oRow As DataRow
        Dim AnyChecked As Boolean
        For i = 0 To oTb.Rows.Count - 1
            If CheckedListBoxZons.GetItemChecked(i) Then
                If AnyChecked Then
                    SQL = SQL & " OR "
                Else
                    AnyChecked = True
                End If
                oRow = oTb.Rows(i)
                SQL = SQL & "(Location.Zona='" & oRow("Guid").ToString & "') "
            End If
        Next
        'SQL = SQL & ") "
        Return SQL
    End Function

    Private Function SQLCLITPAS() As String
        Dim SQL As String = "SELECT PDC.EMP, PDC.CLI FROM PDC INNER JOIN " _
        & "PNC ON PDC.Guid=PNC.PdcGuid INNER JOIN " _
        & "ART ON PNC.ArtGuid=ART.Guid WHERE " _
        & "PDC.COD=2 AND " _
        & "PNC.EUR>0 AND ("

        Dim i As Integer
        Dim oTb As DataTable = mDsTpas.Tables(0)
        Dim oRow As DataRow
        Dim AnyChecked As Boolean = False
        For i = 0 To oTb.Rows.Count - 1
            If CheckedListBoxTpas.GetItemChecked(i) Then
                If AnyChecked Then
                    SQL = SQL & " OR "
                Else
                    AnyChecked = True
                End If
                oRow = oTb.Rows(i)
                SQL = SQL & "ART.TPA=" & oRow("Guid")
            End If
        Next
        SQL = SQL & ")"
        Return SQL
    End Function

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Today.AddYears(-1)
        EnableButtons()
        ComboBoxTarifa.SelectedIndex = 0
        ComboBoxLang.SelectedIndex = 0
        mAllowEvents = True
    End Sub

    Private Sub ButtonAdrs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonAdrs.Click
        mEmailAdrs = False
        MakeDataSet()
        EnableButtons()
    End Sub

    Private Sub ButtonEmails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEmails.Click
        mEmailAdrs = True
        MakeDataSet()
        EnableButtons()
    End Sub

    Private Sub CheckBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxReps.Click, CheckBoxSalesmen.Click, CheckBoxClis.Click, CheckBoxCliMasters.Click, CheckBoxCliSalePoints.Click, CheckBoxOnLineShops.Click

        EnableButtons()
    End Sub

    Private Sub ButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub

    Private Sub ButtonEtqs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonEtqs.Click
        Dim oTb As DataTable = mDs.Tables(0)
        'Dim oEtq As maxisrvr.etq = MatFwkXl.GetEtqConfig()
        'root.PrintEtqs(oTb, oEtq)
    End Sub

    Private Sub CheckBoxAllTpas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAllTpas.CheckedChanged
        If mAllowEvents Then
            CheckedListBoxTpas.Visible = Not CheckBoxAllTpas.Checked
            If CheckedListBoxTpas.Visible Then LoadTpas()
        End If
    End Sub

    Private Sub CheckBoxAllZons_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAllZons.CheckedChanged
        If mAllowEvents Then
            CheckedListBoxZons.Visible = Not CheckBoxAllZons.Checked
            If CheckedListBoxZons.Visible Then LoadZonas()
        End If
    End Sub

    Private Sub LoadTpas()
        Static Loaded As Boolean
        If Loaded Then Return
        Loaded = True

        Dim SQL As String = "SELECT Guid, TPA,DSC FROM TPA WHERE " _
        & "tpa.EMP=" & mEmp.Id & " AND " _
        & "OBSOLETO=0 " _
        & "ORDER BY ORD, TPA"
        mDsTpas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)

        Dim oRow As DataRow
        For Each oRow In mDsTpas.Tables(0).Rows
            CheckedListBoxTpas.Items.Add(oRow("DSC"), True)
        Next
    End Sub

    Private Sub LoadZonas()
        Static Loaded As Boolean
        If Loaded Then Return
        Loaded = True

        Dim SQL As String = "SELECT Country.ISO,Zona.Guid,Zona.Nom FROM Zona INNER JOIN " _
                            & "Country ON Zona.Country=Country.Guid " _
        & "ORDER BY Country.ISO, Zona.Nom"
        mDsZons = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim s As String
        Dim BlChecked As Boolean
        Dim oRow As DataRow
        For Each oRow In mDsZons.Tables(0).Rows
            s = oRow("ISO") & " " & oRow("Nom")
            'BlChecked = oRow("ISOPAIS") = "ES"
            'If oRow("ISOPAIS") = "AD" Then BlChecked = True
            CheckedListBoxZons.Items.Add(s, BlChecked)
        Next
    End Sub


    Private Sub ButtonCircular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCircular.Click
        Dim oRow As DataRow
        Dim LngId As Long
        Dim oContact As Contact
        Dim sRecipient As String = ""
        Dim exs as new list(Of Exception)
        Dim BLSTART As Boolean

        For Each oRow In mDs.Tables(0).Rows

            BLSTART = False
            LngId = oRow(ColsEMail.Id)
            If LngId = 6777 Then
                BLSTART = True
                Stop
            End If
            'End If
            If BLSTART Then
                sRecipient = oRow(ColsEMail.Adr)
                oContact = MaxiSrvr.Contact.FromNum(mEmp, LngId)
                If BLLApp.Emp.EMail_Circular(oContact, TextBoxUrl.Text, TextBoxSubject.Text, sRecipient, exs) <> EventLogEntryType.Information Then
                    MsgBox("error:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "CIRCULARS")
                End If
            End If
        Next
    End Sub


End Class
