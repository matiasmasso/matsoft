Imports Microsoft.Office.Interop

Imports System.Data.SqlClient

Public Class Frm_Auditoria
    Private mLang As DTOLang = BLL.BLLApp.Lang

    Private Enum Cols
        id
        nom
    End Enum

    Public Enum FileFormats
        NotSet
        Csv
        Pdf
    End Enum

    Private Enum tasks
        NotSet
        llibreDiariPdf
        llibreDiariExcel
        llibreMajorDeComptesPdf
        llibreMajorDeComptesExcel
        llibreMajorDeComptesExcelNextYear1T
        llibreFacturesEmeses
        llibreFacturesRebudes
        llibreFacturesEmesesNextYearFirstQ
        llibreFacturesRebudesNextYearFirstQ
        llibreFacturesIntracomunitaries
        balanç3digits
        balanç4digits
        balançFullDigits
        balanç3digitsNextYear1T
        balançFullDigitsNextYear1T
        sumesIsaldos3digits
        sumesIsaldos4digits
        sumesISaldosFullDigits
        sumesISaldos3digitsNextYear1T
        sumesISaldosFulldigitsNextYear1T
        llibreAlbarans
        geoCtas
        deutors
        creditors
        efectesEnCirculacio
        StaffCategoriaSex
        StaffAge
        LastRebutsPrestecsILeasing
        LlibreNomines
        Models145
        llibreContractes
        VendesUltims10Albarans
        VendesPrimers10AlbaransAnySeguent
        CompresUltims10Albarans
        CompresPrimers10AlbaransAnySeguent
    End Enum

    Private Sub Frm_Auditoria_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDefaultYear()
        LoadTasks()
    End Sub

    Private Sub LoadDefaultYear()
        With NumericUpDownYea
            .Minimum = 1985
            .Maximum = 2050
            .Value = Today.Year - 1
        End With
    End Sub

    Private Sub LoadTasks()
        AddTask(tasks.llibreDiariPdf, "llibre diari (pdf)")
        AddTask(tasks.llibreDiariExcel, "llibre diari (excel)")
        AddTask(tasks.llibreMajorDeComptesPdf, "llibre major de comptes (pdf)")
        AddTask(tasks.llibreMajorDeComptesExcel, "llibre major de comptes (excel)")
        AddTask(tasks.llibreMajorDeComptesExcelNextYear1T, "llibre major de comptes (excel) primer trimestre any següent")
        AddTask(tasks.llibreFacturesEmeses, "llibre factures emeses")
        AddTask(tasks.llibreFacturesRebudes, "llibre factures rebudes")
        AddTask(tasks.llibreFacturesEmesesNextYearFirstQ, "llibre factures emeses primer trimestre any següent")
        AddTask(tasks.llibreFacturesRebudesNextYearFirstQ, "llibre factures rebudes primer trimestre any següent")
        AddTask(tasks.llibreFacturesIntracomunitaries, "llibre factures intracomunitaries (excel)")
        AddTask(tasks.balanç3digits, "balanç a 3 digits")
        AddTask(tasks.balanç4digits, "balanç a 4 digits")
        AddTask(tasks.balançFullDigits, "balanç tots els digits")
        AddTask(tasks.balanç3digitsNextYear1T, "balanç a 3 digits 1er trimestre any següent")
        AddTask(tasks.balançFullDigitsNextYear1T, "balanç tots els digits 1er trimestre any següent")
        AddTask(tasks.sumesIsaldos3digits, "sumes i saldos 3 digits")
        AddTask(tasks.sumesIsaldos4digits, "sumes i saldos 4 digits")
        AddTask(tasks.sumesISaldosFullDigits, "sumes i saldos tots els digits")
        AddTask(tasks.sumesISaldos3digitsNextYear1T, "sumes i saldos 3 digits 1er trimestre any següent")
        AddTask(tasks.sumesISaldosFulldigitsNextYear1T, "sumes i saldos tots els digits 1er trimestre any següent")
        AddTask(tasks.llibreAlbarans, "llibre d'albarans")
        AddTask(tasks.deutors, "cartera de cobraments")
        AddTask(tasks.creditors, "cartera de pagaments")
        AddTask(tasks.geoCtas, "distribucio geografica comptes (CCAA,Espanya,CEE)")
        AddTask(tasks.efectesEnCirculacio, "efectes en circulació")
        AddTask(tasks.LlibreNomines, "llibre de nómines")
        AddTask(tasks.Models145, "models 145")
        AddTask(tasks.StaffCategoriaSex, "personal per categories i sexe")
        AddTask(tasks.StaffAge, "personal. Dates de neixament")
        AddTask(tasks.LastRebutsPrestecsILeasing, "ultims rebuts credits i leasing")
        AddTask(tasks.llibreContractes, "llibre de contractes")
        AddTask(tasks.VendesUltims10Albarans, "Vendes: ultims 10 albarans")
        AddTask(tasks.VendesPrimers10AlbaransAnySeguent, "Vendes: primers 10 albarans any següent")
        AddTask(tasks.CompresUltims10Albarans, "Compres: ultims 10 albarans")
        AddTask(tasks.CompresPrimers10AlbaransAnySeguent, "Compres: primers 10 albarans any següent")

    End Sub

    Private Sub DoTask()
        Dim oTask As tasks = CurrentTask()
        Dim oExercici As Exercici = CurrentExercici()
        Select Case oTask
            Case tasks.llibreDiariPdf
                Dim oLlibre As New Llibre_Diari(oExercici)
                SaveFile(oLlibre, FileFormats.Pdf)
            Case tasks.llibreDiariExcel
                Dim oLlibre As New Llibre_Diari(oExercici)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreMajorDeComptesPdf
                Dim oLlibre As New Llibre_Major(oExercici.Emp, oExercici.FchEnd)
                SaveFile(oLlibre, FileFormats.Pdf)
            Case tasks.llibreMajorDeComptesExcel
                Dim oLlibre As New Llibre_Major(oExercici.Emp, oExercici.FchEnd)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreMajorDeComptesExcelNextYear1T
                Dim oLlibre As New Llibre_Major(oExercici.Emp, oExercici.FchEnd1stQuarterNextYear)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreFacturesEmeses
                Dim oLlibre As New Llibre_IVAFacturesEmeses(oExercici)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreFacturesRebudes
                Dim oLlibre As New Llibre_IVAFacturesRebudes(oExercici)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreFacturesEmesesNextYearFirstQ
                Dim oNextExercici As New Exercici(oExercici.Emp, oExercici.Yea + 1)
                Dim oLlibre As New Llibre_IVAFacturesEmeses(oNextExercici)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreFacturesRebudesNextYearFirstQ
                Dim oNextExercici As New Exercici(oExercici.Emp, oExercici.Yea + 1)
                Dim oLlibre As New Llibre_IVAFacturesRebudes(oNextExercici)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreFacturesIntracomunitaries
                Dim SQL As String = "SELECT INTRASTAT.Guid, INTRASTAT.mes, CCA.Guid,CCA.auxCca, CCA.fch, CCA.txt AS Expr1, CCB.eur " _
                & "FROM IMPORTDTL INNER JOIN " _
                & "INTRASTAT ON IMPORTDTL.emp = INTRASTAT.emp AND IMPORTDTL.Intrastat = INTRASTAT.Guid INNER JOIN " _
                & "CCB INNER JOIN " _
                & "CCA ON Ccb.CcaGuid = Cca.Guid ON IMPORTDTL.Guid = CCA.Guid AND IMPORTDTL.emp = CCA.emp " _
                & "WHERE CCB.cta LIKE '6%' AND CCB.Emp = @EMP AND CCB.yea=@YEA " _
                & "ORDER BY INTRASTAT.mes, CCA.auxCca"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea)
                root.ShowCsvFromDataset(oDs)
            Case tasks.balanç3digits
                Dim oLlibre As New Llibre_Balance_Situacio(oExercici.Emp, mLang, oExercici.FchEnd, 3, False)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.balanç4digits
                Dim oLlibre As New Llibre_Balance_Situacio(oExercici.Emp, mLang, oExercici.FchEnd, 4, False)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.balançFullDigits
                Dim oLlibre As New Llibre_Balance_Situacio(oExercici.Emp, mLang, oExercici.FchEnd, 5, True)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.balanç3digitsNextYear1T
                Dim oLlibre As New Llibre_Balance_Situacio(oExercici.Emp, mLang, oExercici.FchEnd1stQuarterNextYear, 3, False)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.balançFullDigitsNextYear1T
                Dim oLlibre As New Llibre_Balance_Situacio(oExercici.Emp, mLang, oExercici.FchEnd1stQuarterNextYear, 5, True)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.sumesIsaldos3digits
                Dim oLlibre As New Llibre_Balance_SumasYSaldos(oExercici.Emp, mLang, oExercici.FchEnd, 3, False)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.sumesIsaldos4digits
                Dim oLlibre As New Llibre_Balance_SumasYSaldos(oExercici.Emp, mLang, oExercici.FchEnd, 4, False)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.sumesISaldosFullDigits
                Dim oLlibre As New Llibre_Balance_SumasYSaldos(oExercici.Emp, mLang, oExercici.FchEnd, 5, True)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.sumesISaldos3digitsNextYear1T
                Dim oLlibre As New Llibre_Balance_SumasYSaldos(oExercici.Emp, mLang, oExercici.FchEnd1stQuarterNextYear, 3, False)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.sumesISaldosFulldigitsNextYear1T
                Dim oLlibre As New Llibre_Balance_SumasYSaldos(oExercici.Emp, mLang, oExercici.FchEnd1stQuarterNextYear, 5, True)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.llibreAlbarans
                Dim oLlibre As New Llibre_Albarans(oExercici)
                SaveFile(oLlibre, FileFormats.Csv)
            Case tasks.deutors
                Deutors()
            Case tasks.creditors
                Creditors()
            Case tasks.geoCtas
                Dim oFrm As New Frm_PgcGeos
                oFrm.Show()

            Case tasks.efectesEnCirculacio
                Dim SQL As String = "SELECT CliBnc.Abr, " _
                & "CAST(CSB.yea AS VARCHAR) + '.' + CAST(CSB.Csb AS VARCHAR) + '.' + CAST(Csb.DOC AS VARCHAR) AS DOC, " _
                & "CSA.fch, " _
                & "CSB.vto, " _
                & "CSB.eur, " _
                & "CSB.txt, " _
                & "Csb.ccc, " _
                & "CLX.CLX " _
                & "FROM CSB INNER JOIN " _
                & "CSA ON CSB.Emp = CSA.emp AND CSB.yea = CSA.yea AND CSB.Csb = CSA.csb INNER JOIN " _
                & "CliBnc ON CSA.emp = CliBnc.emp AND CSA.bnc = CliBnc.cli INNER JOIN " _
                & "CLX ON CLX.EMP=CSB.EMP AND CLX.CLI=CSB.CLI " _
                & "WHERE CSA.emp =@EMP AND " _
                & "CSA.DESCOMPTAT=1 AND " _
                & "(@FCH2 BETWEEN CSA.fch AND CSB.vto) AND "

                'evita els reclamats abans de cap d'any
                SQL = SQL & "CSB.CCAVTOYEA<>@YEA " _
                & "ORDER BY CliBnc.Abr, CSB.yea, CSB.Csb, CAST(Csb.DOC AS INT)"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea, "@FCH2", oExercici.FchEnd)
                root.ShowCsvFromDataset(oDs)

            Case tasks.StaffCategoriaSex
                Dim SQL As String = "SELECT  SegSocialGrups.Id, SegSocialGrups.Nom, LaboralCategories.Nom AS CATEGORIA, " _
                & "CliStaff.Abr, CliStaff.Alta, CliStaff.Baja, " _
                & "DATEDIFF(d,(CASE WHEN YEAR(Alta) = @YEA THEN ALTA ELSE @FCH1 END), (CASE WHEN YEAR(BAJA) = @YEA THEN BAJA ELSE @FCH2 END)) AS DIAS,  " _
                & "(CASE WHEN CliStaff.Sex = 1 THEN 1 ELSE 0 END) AS MALE, " _
                & "(CASE WHEN SEX = 2 THEN 1 ELSE 0 END) AS FEMALE " _
                & "FROM CLISTAFF LEFT OUTER JOIN " _
                & "LaboralCategories ON CliStaff.Categoria LIKE LaboralCategories.Guid LEFT OUTER JOIN " _
                & "SegSocialGrups ON LaboralCategories.SegSocialGrup = SegSocialGrups.Id " _
                & "WHERE CliStaff.emp = @EMP AND " _
                & "CliStaff.Alta <= @FCH2 AND " _
                & "(CliStaff.Baja IS NULL OR CliStaff.Baja >= @FCH1) " _
                & "ORDER BY SegSocialGrups.Id, LaboralCategories.Ord, clistaff.ABR"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea, "@FCH1", oExercici.FchIni, "@FCH2", oExercici.FchEnd)
                root.ShowCsvFromDataset(oDs)
            Case tasks.StaffAge
                Dim SQL As String = "SELECT CliGral.RaoSocial, CliStaff.Nac, DATEDIFF(yy,CliStaff.Nac,@FCH2) as YEARS, CliStaff.NumSS, CliStaff.Alta, CliStaff.Baja, " _
                & "DATEDIFF(d,(CASE WHEN YEAR(Alta) = @YEA THEN ALTA ELSE @FCH1 END), (CASE WHEN YEAR(BAJA) = @YEA THEN BAJA ELSE @FCH2 END)) AS DIAS  " _
                & "FROM CliStaff INNER JOIN " _
                & "CliGral ON CliStaff.emp = CliGral.emp AND CliStaff.cli = CliGral.Cli " _
                & "WHERE CliStaff.emp = @EMP AND " _
                & "CliStaff.Alta <= @FCH2 AND " _
                & "(CliStaff.Baja IS NULL OR CliStaff.Baja >= @FCH1) " _
                & "ORDER BY CliStaff.Nac"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea, "@FCH1", oExercici.FchIni, "@FCH2", oExercici.FchEnd)
                root.ShowCsvFromDataset(oDs)
            Case tasks.LastRebutsPrestecsILeasing
                Dim sCtaCredits As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.creditsBancaris).Id
                Dim sCtaLeasings As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.LeasingACurt).Id
                Dim SQL As String = "SELECT CCA.auxCca, CCB.fch, CLX.clx, CCB.eur, CCA.Guid " _
                & "FROM CCB INNER JOIN " _
                & "CLX ON CCB.Emp = CLX.Emp AND CCB.cli = CLX.cli INNER JOIN " _
                & "CCA ON Ccb.CcaGuid = Cca.Guid " _
                & "WHERE CCB.Emp = @EMP AND CCB.yea =@YEA AND CCB.dh = 1 AND " _
                & "(CCB.cta LIKE '" & sCtaCredits & "' OR CCB.cta LIKE '" & sCtaLeasings & "') " _
                & "ORDER BY CCB.fch DESC"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea)
                root.ShowCsvFromDataset(oDs)

            Case tasks.LlibreNomines
                Dim sCtaDevengat As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.Nomina).Id
                Dim sCtaDietas As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.Dietas).Id
                Dim sCtaIndemnz As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.Indemnitzacions).Id
                Dim sCtaSegSocial As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.SegSocialDevengo).Id
                Dim sCtaIrpf As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.IrpfTreballadors).Id
                Dim sCtaLiquid As String = oExercici.Plan.Cta(DTOPgcPlan.ctas.PagasTreballadors).Id
                Dim sUrl As String = BLL.BLLWebPage.Url(DTOWebPage.Ids.Doc)
                Dim sTitleRow As String = "assentament;document;data;treballador;devengat;dietes;indemnitzacions;seg.social;Irpf;altres;liquid"
                Dim SQL As String = "SELECT CCA.auxCca, CCA.GUID, CCA.fch, MAX(CliGral.RaoSocial) AS NOM, " _
                & "SUM(CASE WHEN CTA LIKE '" & sCtaDevengat & "' THEN (CASE WHEN DH=1 THEN EUR ELSE -EUR END) ELSE 0 END) AS DEVENGAT, " _
                & "SUM(CASE WHEN CTA LIKE '" & sCtaDietas & "' THEN (CASE WHEN DH=1 THEN EUR ELSE -EUR END) ELSE 0 END) AS DIETAS, " _
                & "SUM(CASE WHEN CTA LIKE '" & sCtaIndemnz & "' THEN (CASE WHEN DH=1 THEN EUR ELSE -EUR END) ELSE 0 END) AS INDEMNZ,  " _
                & "SUM(CASE WHEN CTA LIKE '" & sCtaSegSocial & "' THEN (CASE WHEN DH=2 THEN EUR ELSE -EUR END) ELSE 0 END) AS SEGSOCIAL, " _
                & "SUM(CASE WHEN CTA LIKE '" & sCtaIrpf & "' THEN (CASE WHEN DH=2 THEN EUR ELSE -EUR END) ELSE 0 END) AS IRPF, " _
                & "0 AS ALTRES, " _
                & "SUM(CASE WHEN CTA LIKE '" & sCtaLiquid & "' THEN (CASE WHEN DH=2 THEN EUR ELSE -EUR END) ELSE 0 END) AS LIQUID " _
                & "FROM CCA INNER JOIN " _
                & "CCB ON Ccb.CcaGuid = Cca.Guid LEFT OUTER JOIN " _
                & "CliGral ON CCB.Emp = CliGral.emp AND CCB.cli = CliGral.Cli " _
                & "WHERE CCA.emp = @EMP AND CCA.yea = @YEA AND CCA.ccd = @COD " _
                & "GROUP BY CCA.auxCca, CCA.guid, CCA.fch, CCA.txt " _
                & "ORDER BY CCA.auxCca"

                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea, "@COD", DTOCca.CcdEnum.Nomina)
                Dim oTb As DataTable = oDs.Tables(0)
                For Each oRow As DataRow In oTb.Rows
                    oRow("ALTRES") = CDec(oRow("DEVENGAT")) + CDec(oRow("DIETAS")) + CDec(oRow("INDEMNZ")) - CDec(oRow("SEGSOCIAL")) - CDec(oRow("IRPF")) - CDec(oRow("LIQUID"))
                Next
                root.ShowCsvFromDataset(oDs, , sTitleRow)

            Case tasks.Models145
                ShowExcelMods145()

            Case tasks.llibreContractes

                Dim SQL As String = "SELECT CONTRACT_CODIS.Nom AS CODINOM, CONTRACT.Num, CONTRACT.FchFrom, CONTRACT.Nom, CLX.clx, CONTRACT.Guid " _
                & "FROM CONTRACT INNER JOIN " _
                & "CONTRACT_CODIS ON CONTRACT.Codi = CONTRACT_CODIS.Id INNER JOIN " _
                & "CLX ON CONTRACT.ContactGuid = CLX.Guid " _
                & "WHERE CONTRACT.EMP=@EMP AND YEAR(CONTRACT.FCHFROM)<=@YEA AND (CONTRACT.FCHTO IS NULL OR YEAR(CONTRACT.FCHTO)>=@YEA) " _
                & "ORDER BY CODINOM, CONTRACT.FchFrom"

                Dim sTitleRow As String = "codi;numero;data;concepte;contractant;guid"
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea)
                root.ShowCsvFromDataset(oDs, , sTitleRow)

            Case tasks.VendesUltims10Albarans
                Dim oAlbs As Albs = Get10UltimsAlbs(oExercici, DTOPurchaseOrder.Codis.client)
                ShowExcelFromAlbs(oAlbs)

            Case tasks.VendesPrimers10AlbaransAnySeguent
                Dim oAlbs As Albs = Get10PrimersAlbs(oExercici.Next, DTOPurchaseOrder.Codis.client)
                ShowExcelFromAlbs(oAlbs)

            Case tasks.CompresUltims10Albarans
                Dim oAlbs As Albs = Get10UltimsAlbs(oExercici, DTOPurchaseOrder.Codis.proveidor)
                ShowExcelFromAlbs(oAlbs)

            Case tasks.CompresPrimers10AlbaransAnySeguent
                Dim oAlbs As Albs = Get10PrimersAlbs(oExercici.Next, DTOPurchaseOrder.Codis.proveidor)
                ShowExcelFromAlbs(oAlbs)

        End Select

    End Sub

    Private Function Get10UltimsAlbs(oExercici As Exercici, oCod As DTOPurchaseOrder.Codis) As Albs
        Dim oAlbs As New Albs
        Dim SQL As String = "SELECT TOP 20 Guid FROM ALB WHERE EMP=@EMP AND YEA=@YEA AND COD=@Cod ORDER BY ALB DESC"
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea, "@Cod", CInt(oCod))
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oAlb As New Alb(oGuid)
            oAlbs.Add(oAlb)
        Loop
        oDrd.Close()
        Return oAlbs
    End Function

    Private Function Get10PrimersAlbs(oExercici As Exercici, oCod As DTOPurchaseOrder.Codis) As Albs
        Dim oAlbs As New Albs
        Dim SQL As String = "SELECT TOP 20 Guid FROM ALB WHERE EMP=@EMP AND YEA=@YEA AND COD=@Cod ORDER BY ALB"
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", oExercici.Emp.Id, "@YEA", oExercici.Yea, "@Cod", CInt(oCod))
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oAlb As New Alb(oGuid)
            oAlbs.Add(oAlb)
        Loop
        oDrd.Close()
        Return oAlbs
    End Function

    Private Sub ShowExcelMods145()
        Dim SQLX As String = "SELECT CLIDOCS.cli, CliGral.RaoSocial, CliGral.NIF, MAX(CLIDOCS.fch) AS FCH " _
        & "FROM            CLIDOCS INNER JOIN " _
        & "CliStaff ON CLIDOCS.emp = CliStaff.emp AND CLIDOCS.cli = CliStaff.cli AND CLIDOCS.src = 8 INNER JOIN " _
        & "CliGral ON CliStaff.emp = CliGral.emp AND CliStaff.cli = CliGral.Cli " _
        & "WHERE        (CliStaff.Baja IS NULL OR CliStaff.Baja >= @Fch) AND CLIDOCS.fch < @Fch " _
        & "GROUP BY CLIDOCS.cli, CliGral.RaoSocial, CliGral.NIF, CLIDOCS.fch"

        Dim SQL As String = "SELECT X.RaoSocial, X.NIF, X.FCH, CliDocs.Guid FROM (" & SQLX & ") AS X INNER JOIN " _
        & "CliDocs ON X.Cli=CliDocs.Cli AND X.Fch=CliDocs.Fch " _
        & "ORDER BY X.RaoSocial"

        Dim oExercici As Exercici = CurrentExercici()
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Fch", oExercici.FchEnd)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oExcel As New MatExcel

        For Each oRow As DataRow In oTb.Rows
            Dim oArray As New ArrayList
            Dim oDoc As New CliDoc(GuidHelper.GetGuid(oRow("Guid")))
            oArray.Add(New DTOExcelCell(oRow("RaoSocial").ToString, BLL.BLLDocFile.DownloadUrl(oDoc.DocFile, True)))
            oArray.Add(New DTOExcelCell(oRow("Nif").ToString))
            oArray.Add(New DTOExcelCell(oRow("Fch").ToString))
            oExcel.AddRow(oArray)
        Next

        oExcel.Application.Visible = True

    End Sub

    Private Sub ShowExcelFromAlbs(oAlbs As Albs)
        Dim oExcel As New MatExcel

        For Each oAlb As Alb In oAlbs
            Dim oArray As New ArrayList
            oArray.Add(New DTOExcelCell(oAlb.Id, oAlb.Url(True)))
            oArray.Add(New DTOExcelCell(oAlb.Fch))
            oArray.Add(New DTOExcelCell(oAlb.Client.Nom_o_NomComercial))
            If oAlb.Cod = DTOPurchaseOrder.Codis.client Then
                Dim oFra As Fra = oAlb.Fra
                If Not oFra Is Nothing Then
                    oArray.Add(New DTOExcelCell(oFra.Id, BLL.BLLDocFile.DownloadUrl(oFra.Cca.DocFile, True)))
                    oArray.Add(New DTOExcelCell(oFra.Fch))
                    Dim oCca As Cca = oFra.Cca
                    If oCca IsNot Nothing Then
                        Dim iCcaNum As Integer = IIf(oCca.AuxCca > 0, oCca.AuxCca, oCca.Id)
                        oArray.Add(New DTOExcelCell(iCcaNum))
                    End If
                End If
            End If
            oExcel.AddRow(oArray)
        Next

        oExcel.Application.Visible = True

    End Sub

    Private Sub AddTask(ByVal oTask As tasks, ByVal sNom As String)
        Dim oValuesArray As String() = New String() {CInt(oTask).ToString, sNom}
        DataGridView1.Rows.Add(oValuesArray)
    End Sub


    Private Function CurrentExercici() As Exercici
        Static oEmp as DTOEmp
        If oEmp Is Nothing Then oEmp =BLL.BLLApp.Emp
        Dim iYea As Integer = NumericUpDownYea.Value
        Dim retVal As New Exercici(oEmp, iYea)
        Return retVal
    End Function

    Private Function CurrentTask() As tasks
        Dim retVal As tasks = tasks.NotSet
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retVal = CType(oRow.Cells(Cols.id).Value, tasks)
        End If
        Return retVal
    End Function

    Private Sub LaunchProgressBar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iRowsCount As Integer = CInt(sender)
        With ProgressBar1
            .Minimum = 0
            .Maximum = iRowsCount
            .Visible = True
        End With
        Cursor = Cursors.Default
        Application.DoEvents()
    End Sub

    Private Sub onNewRow(ByVal sender As Object, ByVal e As System.EventArgs)
        ProgressBar1.Increment(1)
        Application.DoEvents()
    End Sub

    Private Sub onNewPage(ByVal sender As Object, ByVal e As System.EventArgs)
        TextBoxStatusBar.Text = "pag. " & sender.ToString
        Application.DoEvents()
    End Sub

    Private Sub ExecutarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExecutarToolStripMenuItem.Click
        DoTask()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        DoTask()
    End Sub

    Private Sub SaveFile(ByVal oLlibre As Llibre_Comptable, ByVal oFormat As FileFormats)
        Dim oExercici As Exercici = oLlibre.Exercici
        Dim sExtension As String = oFormat.ToString
        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = sExtension
            .AddExtension = True
            .FileName = oExercici.Emp.Org.NIF & "." & oExercici.Yea.ToString & "." & oLlibre.FilenameRoot & "." & sExtension
            Select Case oFormat
                Case FileFormats.Csv
                    .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
                Case FileFormats.Pdf
                    .Filter = "fitxers pdf (*.pdf)|*.pdf|tots els fitxers (*.*)|*.*"
            End Select
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "guardar " & oLlibre.Concepte(mLang) & " a " & oExercici.Emp.Org.Nom
            Dim Rc As DialogResult = .ShowDialog()
            If Rc = Windows.Forms.DialogResult.OK Then
                Cursor = Cursors.WaitCursor

                AddHandler oLlibre.onDataRead, AddressOf LaunchProgressBar
                AddHandler oLlibre.onNewRow, AddressOf onNewRow
                AddHandler oLlibre.onNewPage, AddressOf onNewPage
                oLlibre.Save(.FileName)
                MsgBox(oLlibre.Concepte(mLang) & " grabat a " & .FileName)
                ProgressBar1.Visible = False
            End If
        End With

    End Sub

    Private Function CurrentYea() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Sub LinkLabelAlbsAndFras_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")


        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

        Dim oLang As DTOLang = DTOLang.FromTag("CAT")
        Dim SQL As String = ""
        Dim oDrd As SqlDataReader
        Dim iRow As Integer = 1
        Dim sTxt As String = ""
        Dim sUrl As String = ""
        Dim oAlb As Alb
        Dim oFra As Fra
        Dim oCca As Cca
        Dim oRange As Excel.Range

        Dim iYea As Integer = CurrentYea()

        iRow += 1
        oSheet.Cells(iRow, 1) = "Documentacio de vendes"
        iRow += 1

        oSheet.Cells(iRow, 2) = "Ultims 10 albarans de l'exercisi i les seves factures"
        iRow += 1
        SQL = "SELECT TOP 10 Guid FROM ALB WHERE EMP=@EMP AND YEA=@YEA AND COD=@COD ORDER BY ALB DESC"
        oDrd = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", CurrentExercici.Emp.Id, "@YEA", iYea, "@COD", CInt(DTOPurchaseOrder.Codis.client))
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            oAlb = New Alb(oGuid)
            sTxt = oAlb.FullConcepteAmbClientIimport(oLang)
            sUrl = oAlb.Url
            oRange = oSheet.Cells(iRow, 3)
            oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)
            If oAlb.Fra.Exists Then
                oFra = oAlb.Fra
                sTxt = oFra.FullConcept(oLang, False)
                sUrl = BLL.BLLDocFile.DownloadUrl(oFra.Cca.DocFile, True)
                oRange = oSheet.Cells(iRow, 4)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)

                oCca = oFra.Cca
                sTxt = "assentament " & oCca.AuxCca.ToString & " del " & oCca.fch.ToShortDateString & " " & oCca.Txt
                sUrl = oCca.UrlCca
                oRange = oSheet.Cells(iRow, 5)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)
            End If
            iRow += 1
        Loop
        oDrd.Close()
        iRow += 2

        oSheet.Cells(iRow, 2) = "Primers 10 albarans de l'exercisi següent i les seves factures"
        iRow += 1
        SQL = "SELECT TOP 10 Guid FROM ALB WHERE EMP=@EMP AND YEA=@YEA ORDER BY ALB"
        oDrd = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", CurrentExercici.Emp.Id, "@YEA", iYea + 1)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            oAlb = New Alb(oGuid)
            sTxt = oAlb.FullConcepteAmbClientIimport(oLang)
            sUrl = oAlb.Url
            oRange = oSheet.Cells(iRow, 3)
            oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)
            If oAlb.Fra.Exists Then
                oFra = oAlb.Fra
                sTxt = oFra.FullConcept(oLang, False)
                sUrl = BLL.BLLDocFile.DownloadUrl(oFra.Cca.DocFile, True)
                oRange = oSheet.Cells(iRow, 4)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)

                oCca = oFra.Cca
                sTxt = "assentament " & oCca.AuxCca.ToString & " del " & oCca.fch.ToShortDateString & " " & oCca.Txt
                sUrl = oCca.UrlCca
                oRange = oSheet.Cells(iRow, 5)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)
            End If
            iRow += 1
        Loop
        oDrd.Close()
        iRow += 2



        iRow += 1
        oSheet.Cells(iRow, 1) = "Documentacio de compres"
        iRow += 1

        Dim oPgcplan As PgcPlan = PgcPlan.FromYear(iYea)
        Dim oCta As PgcCta = oPgcplan.Cta(DTOPgcPlan.ctas.compras)

        oSheet.Cells(iRow, 2) = "Ultimes 10 factures de compra de l'exercisi"
        iRow += 1
        SQL = "SELECT  TOP (16) CCA.Guid FROM CCA INNER JOIN " _
        & "CCB ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE  CCA.emp =@EMP AND CCA.YEA=@YEA AND CCB.cta LIKE @CTA " _
        & "ORDER BY CCA.auxCca DESC"
        oDrd = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", CurrentExercici.Emp.Id, "@YEA", iYea, "@CTA", oCta.Id)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            oCca = New Cca(oGuid)
            sTxt = oCca.Txt
            oSheet.Cells(iRow, 3) = oCca.fch.ToShortDateString
            If oCca.DocExists Then
                sUrl = BLL.BLLDocFile.DownloadUrl(oCca.DocFile, True)
                oRange = oSheet.Cells(iRow, 4)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)
            Else
                oSheet.Cells(iRow, 4) = sTxt
            End If

            sTxt = "assentament " & oCca.AuxCca.ToString & " del " & oCca.fch.ToShortDateString & " " & oCca.Txt
            sUrl = oCca.UrlCca
            oRange = oSheet.Cells(iRow, 5)
            oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)

            iRow += 1
        Loop
        oDrd.Close()
        iRow += 1

        oPgcplan = PgcPlan.FromYear(iYea + 1)
        oCta = oPgcplan.Cta(DTOPgcPlan.ctas.compras)
        oSheet.Cells(iRow, 2) = "Primeres 10 factures compra de l'exercisi següent"
        iRow += 1
        SQL = "SELECT  TOP (16) CCA.Guid FROM CCA INNER JOIN " _
        & "CCB ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE  CCA.emp =@EMP AND CCA.YEA=@YEA AND CCB.cta LIKE @CTA " _
        & "ORDER BY CCA.auxCca"
        oDrd = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", CurrentExercici.Emp.Id, "@YEA", iYea + 1, "@CTA", oCta.Id)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            oCca = New Cca(oGuid)
            sTxt = oCca.Txt
            oSheet.Cells(iRow, 3) = oCca.fch.ToShortDateString
            If oCca.DocExists Then
                sUrl = BLL.BLLDocFile.DownloadUrl(oCca.DocFile, True)
                oRange = oSheet.Cells(iRow, 4)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)
            Else
                oSheet.Cells(iRow, 4) = sTxt
            End If

            sTxt = "assentament " & oCca.AuxCca.ToString & " del " & oCca.fch.ToShortDateString & " " & oCca.Txt
            sUrl = oCca.UrlCca
            oRange = oSheet.Cells(iRow, 5)
            oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)

            iRow += 1
        Loop
        oDrd.Close()
        iRow += 1

        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

    End Sub


    Private Sub LaunchProgressBar(ByVal iMax As Integer)
        With ProgressBar1
            .Maximum = iMax
            .Value = 0
            .Visible = True
        End With
    End Sub

    Private Sub IncrementProgressBar()
        With ProgressBar1
            .Increment(1)
        End With
        'Application.DoEvents
    End Sub

    Private Sub SumesISaldosFullDigits()
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")


        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

        Dim iRow As Integer = 1

        iRow += 1
        oSheet.Cells(iRow, 1) = "Sumes i Saldos exercisi " & CurrentYea()
        iRow += 2

        Dim SQL As String = "SELECT  CCB.cta + LEFT('00000', 5 - { fn LENGTH(CCB.cta) }) + LEFT('000000', 6 - { fn LENGTH(CCB.cli) }) + CAST(CCB.cli AS VARCHAR) AS FULLDIGITCTA, " _
        & "(CASE WHEN PGCCTA.ESP IS NULL THEN '' ELSE PGCCTA.Esp END) AS CTADSC, " _
        & "(CASE WHEN CLX IS NULL THEN '' ELSE CLX END) AS NOM, " _
        & "SUM(CASE WHEN DH = 1 THEN EUR ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN DH = 2 THEN EUR ELSE 0 END) AS HAB, " _
        & "CASE WHEN SUM(CASE WHEN DH = 1 THEN EUR ELSE -EUR END)>0 THEN SUM(CASE WHEN DH = 1 THEN EUR ELSE -EUR END) ELSE 0 END AS XDEB, " _
        & "CASE WHEN SUM(CASE WHEN DH = 2 THEN EUR ELSE -EUR END)>0 THEN SUM(CASE WHEN DH = 2 THEN EUR ELSE -EUR END) ELSE 0 END AS XHAB " _
        & "FROM  CCB LEFT OUTER JOIN " _
        & "PGCCTA ON CCB.PgcPlan = PGCCTA.PgcPlan AND CCB.cta = PGCCTA.Id LEFT OUTER JOIN " _
        & "CLX ON CCB.Emp = CLX.Emp AND CCB.cli = CLX.cli " _
        & "WHERE CCB.Emp =@EMP AND " _
        & "CCB.yea =@YEA " _
        & "GROUP BY CCB.cta, CCB.cli, CLX.clx, PGCCTA.Esp " _
        & "ORDER BY CCB.cta, CCB.cli"

        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", CurrentExercici.Emp.Id, "@YEA", CurrentYea)

        oSheet.Cells(iRow, 1) = "compte"
        oSheet.Cells(iRow, 2) = "descripció"
        oSheet.Cells(iRow, 3) = "titular"
        oSheet.Cells(iRow, 4) = "deure"
        oSheet.Cells(iRow, 5) = "haber"
        oSheet.Cells(iRow, 6) = "saldo deutor"
        oSheet.Cells(iRow, 7) = "saldo creditor"
        iRow += 1

        oSheet.Range("D:G").HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        oSheet.Columns("D:G").NumberFormat = "#,##0.00;-#,##0.00;#"

        oSheet.Range("A:A").HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        oSheet.Columns("A:A").NumberFormat = "@"

        Dim iCols As Integer = 7

        Do While oDrd.Read
            For iCol As Integer = 1 To iCols
                oSheet.Cells(iRow, iCol) = oDrd(iCol - 1)
            Next
            iRow += 1
        Loop
        oDrd.Close()

        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
    End Sub

    Private Sub BalSituacio(iYea As Integer, iDigits As Integer)

    End Sub

    Private Sub Deutors()
        CarteraDeDeutorsYCreditors(Pnd.Codis.Deutor)
    End Sub

    Private Sub Creditors()
        CarteraDeDeutorsYCreditors(Pnd.Codis.Creditor)
    End Sub

    Private Sub CarteraDeDeutorsYCreditors(oAD As Pnd.Codis)
        Dim oExercici As Exercici = CurrentExercici()
        Dim sNom As String = "cartera " & IIf(oAD = Pnd.Codis.Deutor, "cobraments", "pagaments")
        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = "csv"
            .AddExtension = True
            .FileName = oExercici.Emp.Org.NIF & "." & oExercici.Yea.ToString & "." & sNom & "." & .DefaultExt
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "guardar " & sNom & " " & oExercici.Yea.ToString
            If .ShowDialog Then
                Dim SQL As String = "SELECT PND.Cta, CLX.clx, PND.fra, PND.fch, PND.eur, PND.vto " _
                & "FROM            PND INNER JOIN " _
                & "CLX ON PND.ContactGuid = CLX.Guid " _
                & "WHERE PND.EMP=@EMP AND " _
                & "PND.CTA LIKE @CTA AND " _
                & "YEAR(PND.fch) <= @YEA AND " _
                & "(PND.STATUS < " & CInt(Pnd.StatusCod.saldat).ToString & " OR YEAR(PND.vto) > @YEA) " _
                & "ORDER BY PND.Cta, CLX.clx"

                Dim sr As New IO.StreamWriter(.FileName, False, System.Text.Encoding.Default)
                Dim iYea As Integer = CurrentExercici.Yea
                Dim sClx As String = "[NotSet]"
                Dim oPlan As PgcPlan = PgcPlan.FromYear(oExercici.Yea)
                Dim sCta As String = IIf(oAD = Pnd.Codis.Deutor, oPlan.Cta(DTOPgcPlan.ctas.clients).Id, oPlan.Cta(DTOPgcPlan.ctas.proveidorsEur).Id)
                Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@YEA", iYea.ToString, "@EMP", oExercici.Emp.Id, "@CTA", sCta)
                Dim oTb As DataTable = oDs.Tables(0)
                Dim oRow As DataRow
                For i As Integer = 0 To oTb.Rows.Count - 1
                    oRow = oTb.Rows(i)
                    If oRow("CLX").ToString <> sClx Then
                        sClx = oRow("CLX").ToString
                        Dim DcEur As Decimal = 0
                        For j As Integer = i To oTb.Rows.Count - 1
                            If oTb.Rows(j)("CLX").ToString = sClx Then
                                DcEur += CDec(oTb.Rows(j)("EUR"))
                            Else
                                Exit For
                            End If
                        Next
                        sr.Write(DcEur)
                        sr.Write(";")
                        sr.Write(";")
                        sr.Write(oRow("CTA").ToString & ";")
                        sr.Write(oRow("CLX").ToString & ";")
                        sr.WriteLine()
                    End If

                    Dim dtFch As Date = CDate(oRow("FCH"))
                    Dim dtVto As Date = CDate(oRow("VTO"))
                    sr.Write(";")
                    sr.Write(CDec(oRow("EUR")) & ";")
                    sr.Write(";")
                    sr.Write(";")

                    sr.Write(New Date(dtFch.Year, dtFch.Month, dtFch.Day) & ";")
                    sr.Write(oRow("FRA").ToString & ";")
                    sr.WriteLine(New Date(dtVto.Year, dtVto.Month, dtVto.Day))

                Next
                sr.Flush()
                sr.Close()
            End If
        End With



    End Sub




End Class