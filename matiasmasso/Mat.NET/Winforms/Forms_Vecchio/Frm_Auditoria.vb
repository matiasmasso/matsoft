Imports Microsoft.Office.Interop
Imports MatHelperStd
Imports System.Data.SqlClient

Public Class Frm_Auditoria
    Private mLang As DTOLang = DTOApp.current.lang
    Private _CancelRequest As Boolean

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
        sumesIsaldos4digits
        sumesISaldos4digitsNextYear1T
        sumesISaldosFulldigitsNextYear1T
        llibreAlbarans
        geoCtas
        deutors
        creditors
        efectesEnCirculacio
        StaffCategoriaSex
        LlibreNomines
        Models145
        llibreContractes
        VendesUltims10Albarans
        VendesPrimers10AlbaransAnySeguent
        CompresUltims10Albarans
        CompresPrimers10AlbaransAnySeguent
        Garanties
    End Enum

    Private Sub Frm_Auditoria_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDefaultYear()
        LoadTasks()
    End Sub

    Private Sub LoadDefaultYear()
        Dim iYears As New List(Of Integer)
        For i As Integer = Today.Year To 1985 Step -1
            iYears.Add(i)
        Next
        Xl_Years1.Load(iYears, Today.Year - 1)
    End Sub

    Private Sub LoadTasks()
        AddTask(tasks.llibreDiariExcel, "llibre diari")
        AddTask(tasks.llibreMajorDeComptesExcel, "llibre major de comptes")
        AddTask(tasks.llibreMajorDeComptesExcelNextYear1T, "llibre major de comptes primer trimestre any següent")
        AddTask(tasks.llibreFacturesEmeses, "llibre factures emeses")
        AddTask(tasks.llibreFacturesRebudes, "llibre factures rebudes")
        AddTask(tasks.llibreFacturesEmesesNextYearFirstQ, "llibre factures emeses primer trimestre any següent")
        AddTask(tasks.llibreFacturesRebudesNextYearFirstQ, "llibre factures rebudes primer trimestre any següent")
        AddTask(tasks.llibreFacturesIntracomunitaries, "llibre factures intracomunitaries")
        AddTask(tasks.sumesIsaldos4digits, "sumes i saldos 4 digits")
        AddTask(tasks.sumesISaldos4digitsNextYear1T, "sumes i saldos 4 digits 1er trimestre any següent")
        AddTask(tasks.llibreAlbarans, "llibre d'albarans")
        AddTask(tasks.Garanties, "garanties darrers tres anys")
        AddTask(tasks.deutors, "cartera de deutors i creditors")
        AddTask(tasks.creditors, "cartera de pagaments")
        AddTask(tasks.geoCtas, "distribucio geografica comptes (CCAA,Espanya,CEE)")
        AddTask(tasks.efectesEnCirculacio, "efectes en circulació")
        AddTask(tasks.LlibreNomines, "llibre de nómines")
        AddTask(tasks.Models145, "models 145")
        AddTask(tasks.StaffCategoriaSex, "personal per categories, sexe i edat")
        AddTask(tasks.llibreContractes, "llibre de contractes")
        AddTask(tasks.VendesUltims10Albarans, "Vendes: ultims 10 albarans")
        AddTask(tasks.VendesPrimers10AlbaransAnySeguent, "Vendes: primers 10 albarans any següent")
        AddTask(tasks.CompresUltims10Albarans, "Compres: ultims 10 albarans")
        AddTask(tasks.CompresPrimers10AlbaransAnySeguent, "Compres: primers 10 albarans any següent")

    End Sub

    Private Async Function DoTask() As Task
        Dim exs As New List(Of Exception)
        Dim oTask As tasks = CurrentTask()
        Dim oExercici As DTOExercici = CurrentExercici()
        Dim oLang As DTOLang = Current.Session.Lang

        Select Case oTask
            Case tasks.llibreDiariExcel
                ProgressBar1.Visible = True
                Dim oStream As Byte() = Await FEB2.LlibreDiari.Excel(exs, oExercici, Current.Session.Lang)
                Dim sFilename As String = String.Format("{0}.{1} Llibre Diari.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
                If UIHelper.ShowExcel(exs, oStream, sFilename) Then
                    ProgressBar1.Visible = False
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreMajorDeComptesExcel
                ProgressBar1.Visible = True
                Dim oStream As Byte() = Await FEB2.LlibreMajor.Excel(exs, oExercici, Current.Session.Lang)
                Dim sFilename As String = String.Format("{0}.{1} Llibre Major.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
                If UIHelper.ShowExcel(exs, oStream, sFilename) Then
                    ProgressBar1.Visible = False
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreMajorDeComptesExcelNextYear1T
                Dim oStream As Byte() = Await FEB2.LlibreMajor.Excel(exs, oExercici.Next, Current.Session.Lang)
                Dim sFilename As String = String.Format("{0}.{1} Llibre Major.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
                If UIHelper.ShowExcel(exs, oStream, sFilename) Then
                    ProgressBar1.Visible = False
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreFacturesEmeses
                ProgressBar1.Visible = True
                Dim oSheet = Await FEB2.Invoices.LlibreDeFactures(exs, oExercici)
                If exs.Count = 0 Then
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreFacturesRebudes
                ProgressBar1.Visible = True
                Dim oSheet = Await FEB2.Bookfras.Excel(exs, oExercici)
                If exs.Count = 0 Then
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreFacturesEmesesNextYearFirstQ
                ProgressBar1.Visible = True
                Dim oNextExercici As DTOExercici = oExercici.Next
                Dim DtFchTo As Date = oExercici.LastFch1stQuarterNextYear
                Dim oSheet = Await FEB2.Invoices.LlibreDeFactures(exs, oNextExercici, DtFchTo)
                If exs.Count = 0 Then
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreFacturesRebudesNextYearFirstQ
                ProgressBar1.Visible = True
                Dim oNextExercici As DTOExercici = oExercici.Next
                Dim DtFchTo As Date = oExercici.LastFch1stQuarterNextYear
                Dim oSheet = Await FEB2.Bookfras.Excel(exs, oNextExercici, DtFchTo)
                If exs.Count = 0 Then
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreFacturesIntracomunitaries
                ProgressBar1.Visible = True
                Dim values = Await FEB2.Bookfras.All(exs, DTOBookFra.Modes.All, oExercici)
                If exs.Count = 0 Then
                    values = values.Where(Function(x) x.ClaveExenta = "E5").ToList
                    Dim oSheet = FEB2.Bookfras.Excel(values, oExercici.Year, "M+O facturas intracomunitarias")
                    ProgressBar1.Visible = False
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.sumesIsaldos4digits
                ProgressBar1.Visible = True
                Dim value = Await FEB2.SumasYSaldos.All(exs, oExercici.Emp, oExercici.LastDayOrToday)
                If exs.Count = 0 Then
                    Dim oSheet = DTOSumasYSaldos.Excel(Current.Session.Emp, value, DTOPgcCta.Digits.Digits4, Nothing)
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.sumesISaldos4digitsNextYear1T
                ProgressBar1.Visible = True
                Dim FchTo As Date = oExercici.LastFch1stQuarterNextYear
                Dim value = Await FEB2.SumasYSaldos.All(exs, oExercici.Emp, FchTo)
                If exs.Count = 0 Then
                    Dim oSheet = DTOSumasYSaldos.Excel(Current.Session.Emp, value, DTOPgcCta.Digits.Digits4, Nothing)
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.llibreAlbarans
                ProgressBar1.Visible = True
                Dim oSheet = Await FEB2.Deliveries.LlibreDeAlbarans(exs, oExercici)
                If exs.Count = 0 Then
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.Garanties
                ProgressBar1.Visible = True
                Dim oSheet As MatHelperStd.ExcelHelper.Sheet = Await FEB2.Garantias.Excel(exs, oExercici)
                If UIHelper.ShowExcel(oSheet, exs) Then
                    ProgressBar1.Visible = False
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If
            Case tasks.deutors
                Await Deutors()
            Case tasks.geoCtas
                Dim oFrm As New Frm_PgcGeos(oExercici)
                oFrm.Show()

            Case tasks.efectesEnCirculacio
                ProgressBar1.Visible = True
                Dim DtFch As Date = oExercici.LastDayOrToday
                Dim oSheet = Await FEB2.Csbs.ExcelPendentsDeVto(oExercici, exs)
                If UIHelper.ShowExcel(oSheet, exs) Then
                    ProgressBar1.Visible = False
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If

            Case tasks.StaffCategoriaSex
                ProgressBar1.Visible = True
                Dim oStaffs = Await FEB2.Staffs.All(exs, oExercici)
                If exs.Count = 0 Then
                    Dim oSheet As MatHelperStd.ExcelHelper.Sheet = DTOStaff.Excel(oStaffs, Current.Session.Lang, oExercici)
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If

            Case tasks.LlibreNomines
                ProgressBar1.Visible = True
                Dim oSheet As MatHelperStd.ExcelHelper.Sheet = Await FEB2.Nominas.Llibre(oExercici, exs)
                If UIHelper.ShowExcel(oSheet, exs) Then
                    ProgressBar1.Visible = False
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If

            Case tasks.Models145
                ProgressBar1.Visible = True
                Dim oSheet = Await FEB2.ContactDocs.ExcelMod145s(oExercici, exs)
                If exs.Count = 0 Then
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If


            Case tasks.llibreContractes
                ProgressBar1.Visible = True
                Dim oContracts As List(Of DTOContract) = Await FEB2.Contracts.All(exs, Current.Session.User)

                If exs.Count = 0 Then
                    oContracts = oContracts.
                        Where(Function(p) p.Privat = False).
                        Where(Function(x) x.fchFrom.Year <= oExercici.Year And (x.fchTo = Nothing Or x.fchTo.Year >= oExercici.Year)).
                        OrderBy(Function(y) y.fchFrom).
                        OrderBy(Function(z) z.Codi.Nom).ToList

                    Dim oSheet As MatHelperStd.ExcelHelper.Sheet = FEB2.Contracts.Excel(oContracts)
                    If UIHelper.ShowExcel(oSheet, exs) Then
                        ProgressBar1.Visible = False
                    Else
                        ProgressBar1.Visible = False
                        UIHelper.WarnError(exs)
                    End If
                Else
                    ProgressBar1.Visible = False
                    UIHelper.WarnError(exs)
                End If

            Case tasks.VendesUltims10Albarans
                ProgressBar1.Visible = True
                Dim oDeliveries = Await Get10UltimsAlbs(exs, oExercici, DTOPurchaseOrder.Codis.Client)
                If exs.Count = 0 Then
                    ShowExcelFromAlbs(oDeliveries)
                Else
                    UIHelper.WarnError(exs)
                End If

            Case tasks.VendesPrimers10AlbaransAnySeguent
                ProgressBar1.Visible = True
                Dim oDeliveries = Await Get10PrimersAlbs(exs, oExercici.Next, DTOPurchaseOrder.Codis.Client)
                If exs.Count = 0 Then
                    ShowExcelFromAlbs(oDeliveries)
                Else
                    UIHelper.WarnError(exs)
                End If

            Case tasks.CompresUltims10Albarans
                ProgressBar1.Visible = True
                Dim oDeliveries = Await Get10UltimsAlbs(exs, oExercici, DTOPurchaseOrder.Codis.Proveidor)
                If exs.Count = 0 Then
                    ShowExcelFromAlbs(oDeliveries)
                Else
                    UIHelper.WarnError(exs)
                End If

            Case tasks.CompresPrimers10AlbaransAnySeguent
                ProgressBar1.Visible = True
                Dim oDeliveries = Await Get10PrimersAlbs(exs, oExercici.Next, DTOPurchaseOrder.Codis.Proveidor)
                ShowExcelFromAlbs(oDeliveries)

        End Select

    End Function

    Private Async Function Get10UltimsAlbs(exs As List(Of Exception), oExercici As DTOExercici, oCod As DTOPurchaseOrder.Codis) As Task(Of List(Of DTODelivery))
        Dim retval As List(Of DTODelivery) = Nothing
        Dim oDeliveries = Await FEB2.Deliveries.All(exs, oExercici)
        If exs.Count = 0 Then
            oDeliveries = oDeliveries.Where(Function(x) x.Cod = oCod).ToList
            retval = oDeliveries.Skip(Math.Max(0, oDeliveries.Count() - 20)).ToList
        End If
        Return retval
    End Function

    Private Async Function Get10PrimersAlbs(exs As List(Of Exception), oExercici As DTOExercici, oCod As DTOPurchaseOrder.Codis) As Task(Of List(Of DTODelivery))
        Dim retval As New List(Of DTODelivery)
        Dim oDeliveries = Await FEB2.Deliveries.All(exs, oExercici)
        If exs.Count = 0 Then
            oDeliveries = oDeliveries.Where(Function(x) x.Cod = oCod).ToList
            retval = oDeliveries.Take(20).ToList
        End If
        Return retval
    End Function


    Private Sub ShowExcelFromAlbs(oDeliveries As List(Of DTODelivery))
        Dim exs As New List(Of Exception)
        Dim oSheet As New ExcelHelper.Sheet
        With oSheet
            .AddColumn("albarà", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("client", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("import", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("factura", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("assentament", ExcelHelper.Sheet.NumberFormats.W50)
        End With
        For Each oDelivery In oDeliveries
            Dim sUrl As String = FEB2.Delivery.Url(oDelivery, True)
            Dim oRow = oSheet.AddRow
            oRow.AddCell(oDelivery.Id, sUrl)
            oRow.AddCell(oDelivery.Fch)
            oRow.AddCell(DTOContact.RaoSocialONomComercial(oDelivery.Customer))
            oRow.AddCellAmt(oDelivery.Import)
            If oDelivery.Cod = DTOPurchaseOrder.Codis.Client Then
                Dim oInvoice As DTOInvoice = oDelivery.Invoice
                If oInvoice IsNot Nothing Then
                    Dim oCca As DTOCca = oInvoice.Cca
                    If oCca Is Nothing Then
                        oRow.AddCell(oInvoice.NumeroYSerie())
                        oRow.AddCell(oInvoice.Fch)
                        oRow.AddCell()
                    Else
                        oRow.AddCell(oInvoice.NumeroYSerie(), FEB2.DocFile.DownloadUrl(oCca.DocFile, True))
                        oRow.AddCell(oInvoice.Fch)
                        oRow.AddCell(oCca.Id)
                    End If
                End If
            End If
        Next

        If UIHelper.ShowExcel(oSheet, exs) Then
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub AddTask(ByVal oTask As tasks, ByVal sNom As String)
        Dim oValuesArray As String() = New String() {CInt(oTask).ToString, sNom}
        DataGridView1.Rows.Add(oValuesArray)
    End Sub


    Private Function CurrentExercici() As DTOExercici
        Dim iYea As Integer = Xl_Years1.Value
        Dim retVal As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, iYea)
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


    Private Sub onNewRow(ByVal sender As Object, ByVal e As System.EventArgs)
        'ProgressBar1.Increment(1)
        'Application.DoEvents()
    End Sub

    Private Sub onNewPage(ByVal sender As Object, ByVal e As System.EventArgs)
        'TextBoxStatusBar.Text = "pag. " & sender.ToString
        'Application.DoEvents()
    End Sub

    Private Async Sub ExecutarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExecutarToolStripMenuItem.Click
        Await DoTask()
    End Sub

    Private Async Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Await DoTask()
    End Sub


    Private Async Function Deutors() As Task
        Await CarteraDeDeutorsYCreditors()
    End Function


    Private Sub CarteraDeDeutorsYCreditors2()
        Dim oSheet As MatHelperStd.ExcelHelper.Sheet = Nothing ' DTOPnd.ExcelCarteraDeDeutorsYCreditors(CurrentExercici)

    End Sub


    Private Async Function CarteraDeDeutorsYCreditors() As Task
        ProgressBar1.Visible = True

        Dim oExercici As DTOExercici = CurrentExercici()
        Dim DtFch As Date = oExercici.LastFch
        Dim oBook As New ExcelHelper.Book("M+O Cartera de deutors i creditors " & oExercici.Year - 1)
        Dim exs As New List(Of Exception)
        oBook.Sheets.Add(Await FEB2.Pnds.CarteraDeDeutorsYCreditors(exs, oExercici.Emp, DTOPnd.Codis.Creditor, DtFch))
        oBook.Sheets.Add(Await FEB2.Pnds.CarteraDeDeutorsYCreditors(exs, oExercici.Emp, DTOPnd.Codis.Deutor, DtFch))
        ProgressBar1.Visible = False

        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oBook, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class