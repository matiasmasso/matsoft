

Public Class Frm_TestSdoMed

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim DtFch As Date = Today
        Dim oCta As PgcCta = PgcPlan.FromYear(DtFch.Year).Cta(DTOPgcPlan.ctas.impagats)
        Dim oEmp As new DTOEmp(1)
        Dim oContact As Contact = MaxiSrvr.Contact.FromNum(oEmp, 4576)
        Dim oCcd As New Ccd(oContact, DtFch.Year, oCta)

        Dim iColfch As Integer = 0
        Dim iColEur As Integer = 1
        Dim iColSdo As Integer = 2
        Dim iColDias As Integer = 3
        Dim SQL As String = "SELECT CCB.fch, SUM(CASE WHEN CCB.dh = 1 THEN CCB.eur ELSE - CCB.eur END) AS eur " _
        & "FROM Ccb  INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE  CCB.cta LIKE @CTA AND CCB.Emp =@EMP AND CCB.cli =@CLI and CCB.fch<=@FCH AND CCA.CCD>" & DTOCca.CcdEnum.AperturaExercisi & " " _
        & "GROUP BY CCB.fch " _
        & "ORDER BY CCB.fch DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oCcd.Contact.Emp.Id, "@CTA", oCcd.Cta.Id, "@CLI", oCcd.Contact.Id, "@FCH", Today)
        Dim oTb As DataTable = oDs.Tables(0)
        oTb.Columns.Add("SDO", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("DIAS", System.Type.GetType("System.Int32"))

        Dim oRow As DataRow = Nothing
        Dim DcSumSdo As Decimal = 0
        Dim DcSumSdoPonderat As Decimal = 0
        Dim DtMinValue As Date = DtFch.AddMonths(-6)
        Dim BlPararQuanSaldoZero As Boolean = False
        If oTb.Rows.Count > 0 Then
            oTb.Rows(0)(iColSdo) = oCcd.Saldo(DtFch).Eur
            oTb.Rows(0)(iColDias) = DateDiff(DateInterval.Day, oTb.Rows(0)(iColfch), DtFch)
            DcSumSdo = oTb.Rows(0)(iColSdo)
            DcSumSdoPonderat = oTb.Rows(0)(iColSdo) * oTb.Rows(0)(iColDias)
            For i As Integer = 1 To oTb.Rows.Count - 1
                oTb.Rows(i)(iColSdo) = oTb.Rows(i - 1)(iColSdo) - oTb.Rows(i - 1)(iColEur)
                BlPararQuanSaldoZero = oTb.Rows(i - 1)(iColfch) < DtMinValue
                If BlPararQuanSaldoZero And oTb.Rows(i)(iColSdo) = 0 Then Exit For

                oTb.Rows(i)(iColDias) = DateDiff(DateInterval.Day, oTb.Rows(i)(iColfch), oTb.Rows(i - 1)(iColfch))
                DcSumSdo += oTb.Rows(i)(iColSdo)
                DcSumSdoPonderat += oTb.Rows(i)(iColSdo) * oTb.Rows(i)(iColDias)
            Next
        End If

        Dim iDias As Integer = DcSumSdoPonderat / DcSumSdo
        'MatExcel.GetExcelFromDataset(oDs).Visible = True
        Stop
    End Sub
End Class