
Imports System.Data.SqlClient

Public Class Frm_Client_Risc
    Private mCcx As Client
    Private mDirtyClassificacions As Boolean = True
    Private mDirtyAsnef As Boolean = True

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        Gral
        Classificacions
        Asnef
    End Enum

    Public Sub New(ByVal oClient As Client)
        MyBase.New()
        Me.InitializeComponent()
        mCcx = oClient.CcxOrMe
        Me.Text = Me.Text & " " & mCcx.Clx
        Xl_Contact_Logo1.Contact = mCcx
        refresca()
    End Sub


    Private Sub refresca()
        Dim oAmtSdoFras As maxisrvr.Amt = mCcx.Credit_FrasPendentsDeVencer
        Dim oAmtSdoAlbsCredit As maxisrvr.Amt = mCcx.Credit_AlbsACreditPerFacturar
        Dim oAmtSdoAlbsNoCredit As maxisrvr.Amt = mCcx.Credit_AlbsNoCreditPerFacturar
        Dim oAmtSdoEntregatACompte As MaxiSrvr.Amt = mCcx.Credit_EntregatACompte
        Dim oAmtDiposit As MaxiSrvr.Amt = BLL_Risc.DipositIrrevocable(mCcx)
        Dim oDisposat As MaxiSrvr.Amt = mCcx.CreditDisposat
        Dim oClassificacio As maxisrvr.Amt = mCcx.CreditLimit
        Dim oDisponible As maxisrvr.Amt = mCcx.CreditDisponible

        If oAmtSdoFras.Eur <> 0 Then TextBoxSdoCta.Text = oAmtSdoFras.CurFormat
        If oAmtSdoAlbsCredit.Eur <> 0 Then TextBoxSdoAlbsCredit.Text = oAmtSdoAlbsCredit.CurFormat
        If oAmtSdoAlbsNoCredit.Eur <> 0 Then TextBoxSdoAlbsNoCredit.Text = oAmtSdoAlbsNoCredit.CurFormat

        If oAmtSdoEntregatACompte.Eur <> 0 Then TextBoxEntregatACompte.Text = oAmtSdoEntregatACompte.CurFormat
        If oAmtDiposit.Eur <> 0 Then TextBoxDiposit.Text = oAmtDiposit.CurFormat

        If oDisposat.Eur <> 0 Then TextBoxDisposat.Text = oDisposat.CurFormat
        If oClassificacio.Eur <> 0 Then TextBoxClassificacio.Text = oClassificacio.CurFormat
        If oDisponible.Eur <> 0 Then TextBoxDisponible.Text = oDisponible.CurFormat


        SetSdoDue()
        SetSdoImpagats()
        SetIndexImpagats()
    End Sub


    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub

    Private Sub ButtonAlbsCredit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAlbsCredit.Click
        root.ShowClientGroupAlbs(mCcx, True)
    End Sub

    Private Sub ButtonPnds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPnds.Click
        Dim oFrm As New Frm_CliCtasOld(mCcx, True)
        oFrm.Show()
    End Sub

    Public Sub SetSdoDue()
        Dim oCta As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.clients)
        Dim oDrd As SqlDataReader = Nothing
        Dim SQL As String = ""

        SQL = "SELECT " _
        & "SUM(CASE WHEN AD LIKE 'D' THEN EUR ELSE - EUR END) as SALDO, " _
        & "SUM(DATEDIFF(d,VTO,GETDATE())*(CASE WHEN AD LIKE 'D' THEN EUR ELSE - EUR END))/SUM(CASE WHEN AD LIKE 'D' THEN EUR ELSE - EUR END) AS DIAS " _
        & "FROM Pnd " _
        & "WHERE Emp=@EMP AND cli=@CLI AND Cta LIKE @CTA AND (Status < 10) AND VTO<GETDATE()"
        oDrd = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", mCcx.Emp.Id, "@CLI", mCcx.Id, "@CTA", oCta.Id)
        oDrd.Read()
        If Not IsDBNull(oDrd("SALDO")) Then
            TextBoxSdoDue.Text = New MaxiSrvr.Amt(CDec(oDrd("SALDO"))).CurFormat
            PictureBoxDue.Visible = True
        End If
        If Not IsDBNull(oDrd("DIAS")) Then
            TextBoxDueDias.Text = CInt(oDrd("dias")).ToString
        End If
        oDrd.Close()
    End Sub

    Public Sub SetSdoImpagats()
        Dim oCcd As New Ccd(mCcx, Today.Year, PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.impagats))
        Dim oAmt As maxisrvr.Amt = oCcd.Saldo(Today)
        Select Case oAmt.Eur
            Case Is > 0
                TextBoxSdoImpagats.Text = oAmt.CurFormat
                PictureBoxWarnImpagats.Visible = True
                'LabelDiesImpagat.Visible = False
                'PictureBoxWarnDiasImpagat.Visible = False
                'TextBoxDiasImpagat.Visible = False
            Case Is <= 0
                TextBoxSdoImpagats.Text = oAmt.CurFormat
        End Select
        Dim iDias As Integer = DiesMitjanaRecuperacioImpagats()
        TextBoxDiasImpagat.Text = iDias
        PictureBoxWarnDiasImpagat.Visible = iDias > 15
    End Sub

    Public Function GetPreCash() As maxisrvr.Amt
        Dim SQL As String = ""
        Dim oDrd As SqlDataReader = Nothing
        Dim DcPreCash As Decimal = 0

        'transferencia previa
        Dim sPortsCod As String = CInt(DTO.DTOCustomer.PortsCodes.Altres).ToString
        Dim sCashCod As String = CInt(DTO.DTOCustomer.CashCodes.TransferenciaPrevia).ToString
        SQL = "SELECT SUM(ALB.EUR+ALB.PT2) AS CASH " _
        & "FROM ALB INNER JOIN " _
        & "CliClient ON ALB.Emp = CliClient.Emp AND " _
        & "(ALB.CliGuid =@CcxGuid OR CLICLIENT.CcxGuid=@CcxGuid) " _
        & "WHERE ALB.fra = 0 AND ALB.facturable = 1 AND " _
        & "ALB.PortsCod <> " & sPortsCod & " AND (ALB.CASHCOD=" & sCashCod & " OR ALB.CASHCOD=" & CInt(DTO.DTOCustomer.CashCodes.Visa) & ") "
        oDrd = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@CcxGuid", mCcx.Guid.ToString)
        oDrd.Read()
        If Not IsDBNull(oDrd("CASH")) Then
            DcPreCash = CDec(oDrd("CASH"))
        End If
        oDrd.Close()

        Dim oAmt As New maxisrvr.Amt(DcPreCash)
        Return oAmt

    End Function

    Public Function GetCash() As maxisrvr.Amt
        Dim DcCash As Decimal = 0
        Dim sCashCod As String = CInt(DTO.DTOCustomer.CashCodes.Reembols).ToString
        Dim SQL As String = "SELECT SUM(ALB.EUR+ALB.PT2) AS CASH " _
        & "FROM ALB INNER JOIN " _
        & "CliClient ON (ALB.CliGuid =@CcxGuid OR CLICLIENT.CcxGuid=@CcxGuid) " _
        & "WHERE ALB.emp=@EMP AND ALB.fra = 0 AND ALB.facturable = 1 AND " _
        & "ALB.CashCod=" & sCashCod & " AND (Alb.cobro IS NOT NULL) "
        Dim oDrd As SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@CcxGuid", mCcx.Guid.ToString)
        oDrd.Read()
        If Not IsDBNull(oDrd("CASH")) Then
            DcCash = CDec(oDrd("CASH"))
        End If
        oDrd.Close()

        Dim oAmt As New maxisrvr.Amt(DcCash)
        Return oAmt
    End Function

    Public Sub SetIndexImpagats(Optional ByVal iDesdeElsDarrersMesos As Integer = 6)
        Dim DtFchFrom As Date = Today.AddMonths(-iDesdeElsDarrersMesos)
        Dim DcTot As Decimal = 0
        Dim DcImpagats As Decimal = 0
        Dim SQL As String = "SELECT SUM(CASE WHEN RECLAMAT = 0 THEN eur ELSE 0 END) AS TOT, " _
        & "SUM(CASE WHEN IMPAGAT = 1 THEN EUR ELSE 0 END) AS IMPAGATS " _
        & "FROM Csb " _
        & "WHERE Emp =@EMP AND cli =@CLI AND (vto BETWEEN @FCHFROM AND GETDATE())"

        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@EMP", mCcx.Emp.Id, "@CLI", mCcx.Id, "@FCHFROM", DtFchFrom)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("TOT")) Then DcTot = CDec(oDrd("TOT"))
            If Not IsDBNull(oDrd("IMPAGATS")) Then DcImpagats = CDec(oDrd("IMPAGATS"))
            If DcImpagats > 0 Then
                Dim iIndex As Integer = 100 * DcImpagats / DcTot
                Dim sCreditProtocol_MaxImpagatsIndex As String = BLL.BLLDefault.EmpValue(DTODefault.Codis.CreditProtocol_MaxImpagatsIndex)
                If IsNumeric(sCreditProtocol_MaxImpagatsIndex) Then
                    PictureBoxWarnIndexImpagats.Visible = iIndex > CInt(sCreditProtocol_MaxImpagatsIndex)
                End If
                LabelIndexImpagats.Text = "index d'impagats darrers 6 mesos: (" & Format(DcImpagats, "#,##0.00") & "/" & Format(DcTot, "#,##0.00") & ")"
                TextBoxIndexImpagats.Text = iIndex.ToString & "%"
            End If
        End If
        oDrd.Close()

    End Sub

    Private Sub ButtonImpagats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonImpagats.Click
        Dim oCcd As New Ccd(mCcx, Today.Year, PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.impagats))
        Dim oFrm As New Frm_CliCtasOld(oCcd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        refresca()
        mDirtyClassificacions = True
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Private Sub ButtonSdoDue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSdoDue.Click
        Dim oCcd As New Ccd(mCcx, Today.Year, PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.clients))
        Dim oFrm As New Frm_CliCtasOld(oCcd)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub ButtonLimit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLimit.Click
        Dim oCliCreditLog As New DTOCliCreditLog
        With oCliCreditLog
            .Customer = New DTOCustomer(mCcx.Guid)
            BLL.BLLContact.Load(.Customer)
        End With
        Dim oFrm As New Frm_CliCreditLimit(oCliCreditLog)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Classificacions
                If mDirtyClassificacions Then
                    Xl_Client_CreditLimit1.Client = mCcx
                    mDirtyClassificacions = False
                End If
            Case Tabs.Asnef
                If mDirtyAsnef Then
                    Xl_Asnef_logs1.Contact = mCcx
                    mDirtyAsnef = True
                End If
        End Select
    End Sub

    Private Sub Xl_Client_CreditLimit1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Client_CreditLimit1.AfterUpdate
        refresca()
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Private Function GetDsDiasImpagat() As DataSet
        Dim DtFch As Date = Today
        Dim oCta As PgcCta = PgcPlan.FromYear(DtFch.Year).Cta(DTOPgcPlan.ctas.impagats)
        Dim oCcd As New Ccd(mCcx, DtFch.Year, oCta)
        Dim SQL As String = "SELECT CCB.fch, SUM(CASE WHEN CCB.dh = 1 THEN CCB.eur ELSE - CCB.eur END) AS eur " _
        & "FROM Ccb  INNER JOIN " _
        & "CCA ON Ccb.CcaGuid = Cca.Guid " _
        & "WHERE  CCB.cta LIKE @CTA AND CCB.Emp =@EMP AND CCB.cli =@CLI and CCB.fch<=@FCH AND CCA.CCD>" & DTOCca.CcdEnum.AperturaExercisi & " " _
        & "GROUP BY CCB.fch " _
        & "ORDER BY CCB.fch DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", oCcd.Contact.Emp.Id, "@CTA", oCcd.Cta.Id, "@CLI", oCcd.Contact.Id, "@FCH", DtFch)
        Dim oTb As DataTable = oDs.Tables(0)
        oTb.Columns.Add("SDO", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("DIAS", System.Type.GetType("System.Int32"))

        Dim iColfch As Integer = 0
        Dim iColEur As Integer = 1
        Dim iColSdo As Integer = 2
        Dim iColDias As Integer = 3

        Dim oRow As DataRow = Nothing
        Dim DcSumSdo As Decimal = 0
        Dim DcSumSdoPonderat As Decimal = 0
        Dim DtMinValue As Date = DtFch.AddMonths(-6)
        Dim BlPararQuanSaldoZero As Boolean = False
        If oTb.Rows.Count > 0 Then
            oTb.Rows(0)(iColSdo) = oCcd.Saldo(DtFch).Eur
            oTb.Rows(0)(iColDias) = DateDiff(DateInterval.Day, oTb.Rows(0)(iColfch), DtFch)
            For i As Integer = 1 To oTb.Rows.Count - 1
                oTb.Rows(i)(iColSdo) = oTb.Rows(i - 1)(iColSdo) - oTb.Rows(i - 1)(iColEur)
                BlPararQuanSaldoZero = oTb.Rows(i - 1)(iColfch) < DtMinValue
                If BlPararQuanSaldoZero And oTb.Rows(i)(iColSdo) = 0 Then Exit For

                oTb.Rows(i)(iColDias) = DateDiff(DateInterval.Day, oTb.Rows(i)(iColfch), oTb.Rows(i - 1)(iColfch))
            Next
        End If
        Return oDs
    End Function

    Private Function DiesMitjanaRecuperacioImpagats() As Integer
        Dim DtFch As Date = Today
        Dim oCta As PgcCta = PgcPlan.FromYear(DtFch.Year).Cta(DTOPgcPlan.ctas.impagats)
        Dim oCcd As New Ccd(mCcx, DtFch.Year, oCta)

        Dim iColfch As Integer = 0
        Dim iColEur As Integer = 1
        Dim iColSdo As Integer = 2
        Dim iColDias As Integer = 3

        Dim oDs As DataSet = GetDsDiasImpagat()
        Dim oTb As DataTable = oDs.Tables(0)

        Dim oRow As DataRow = Nothing
        Dim DcSumSdo As Decimal = 0
        Dim DcSumSdoPonderat As Decimal = 0
        Dim DtMinValue As Date = DtFch.AddMonths(-6)
        Dim BlPararQuanSaldoZero As Boolean = False
        If oTb.Rows.Count > 0 Then
            DcSumSdo = oTb.Rows(0)(iColSdo)
            DcSumSdoPonderat = oTb.Rows(0)(iColSdo) * oTb.Rows(0)(iColDias)
            For i As Integer = 1 To oTb.Rows.Count - 1
                BlPararQuanSaldoZero = oTb.Rows(i - 1)(iColfch) < DtMinValue
                If BlPararQuanSaldoZero And oTb.Rows(i)(iColSdo) = 0 Then Exit For

                DcSumSdo += oTb.Rows(i)(iColSdo)
                DcSumSdoPonderat += oTb.Rows(i)(iColSdo) * oTb.Rows(i)(iColDias)
            Next
        End If

        Dim iDias As Integer
        If DcSumSdo <> 0 Then
            iDias = DcSumSdoPonderat / DcSumSdo
        End If
        Return iDias
    End Function

    Private Sub ButtonDiasImpagat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDiasImpagat.Click
        MatExcel.GetExcelFromDataset(GetDsDiasImpagat).Visible = True
    End Sub

    Private Sub ButtonAlbsNoCredit_Click(sender As Object, e As EventArgs) Handles ButtonAlbsNoCredit.Click
        root.ShowClientGroupAlbs(mCcx, True)
    End Sub
End Class