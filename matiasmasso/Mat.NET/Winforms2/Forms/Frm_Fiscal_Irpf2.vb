Public Class Frm_Fiscal_Irpf2
    Private _Ctas As List(Of DTOPgcCta)
    Private _AllowEvents As Boolean

    Private Async Sub Frm_Fiscal_Irpf2_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If Await LoadCombos(exs) Then
            _Ctas = Await FEB.PgcCtas.All(exs)
            If exs.Count = 0 Then
                Await refresca()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oYearMonth = YearMonth()
        ProgressBar1.Visible = True
        Dim oIrpf = Await FEB.Irpf.Factory(exs, Current.Session.Emp, oYearMonth.year, oYearMonth.month)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Fiscal_Irpf1.Load(oIrpf)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim exs As New List(Of Exception)
        If Await Save(exs) Then
            Await refresca()
            MsgBox("Assentaments registrats correctament", MsgBoxStyle.Information)
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

#Region "Ccas"
    Private Async Function Save(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oCcas As List(Of DTOCca) = Ccas()
        Dim retval As Boolean = Await FEB.Ccas.Update(exs, oCcas)
        Return retval
    End Function

    Private Function Ccas() As List(Of DTOCca)
        Dim retval As New List(Of DTOCca)
        retval.AddRange(TancamentSubComptes)
        Dim oMod111 As DTOCca = Mod111()
        If oMod111 IsNot Nothing Then
            retval.Add(oMod111)
            retval.Add(Mod111Payment)
        End If
        Dim oMod115Payment As DTOCca = Mod115Payment()
        If oMod115Payment IsNot Nothing Then
            retval.Add(oMod115Payment)
        End If
        Return retval
    End Function

    Private Function Mod111Payment() As DTOCca
        Dim retval As DTOCca = Nothing
        Dim oBanc As DTOBanc = Xl_BancsComboBox1.SelectedItem
        Dim DtFch As Date = YearMonth.LastFch.AddDays(20)
        Dim oUser As DTOUser = Current.Session.User
        Dim oLang As DTOLang = DTOLang.CAT
        Dim oBaseQuotas As List(Of DTOBaseQuota) = Xl_Fiscal_Irpf1.Ctas
        Dim oTreballadors As DTOBaseQuota = oBaseQuotas.FirstOrDefault(Function(x) DirectCast(x.Source, DTOCce).Cta.Codi = DTOPgcPlan.Ctas.IrpfTreballadors)
        Dim oProfessionals As DTOBaseQuota = oBaseQuotas.FirstOrDefault(Function(x) DirectCast(x.Source, DTOCce).Cta.Codi = DTOPgcPlan.Ctas.IrpfProfessionals)
        If Not (oProfessionals Is Nothing And oTreballadors Is Nothing) Then
            retval = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.IRPF, 111)
            retval.Concept = String.Format("{0}-Hisenda-Mod 111 IRPF {1} {2}", oBanc.AbrOrNom(), oLang.MesAbr(YearMonth.Month), YearMonth.Year)
            Dim oAmt = DTOAmt.factory
            If oTreballadors IsNot Nothing Then oAmt.Add(oTreballadors.Quota)
            If oProfessionals IsNot Nothing Then oAmt.Add(oProfessionals.Quota)
            retval.AddDebit(oAmt, _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Irpf))
            retval.AddSaldo(_Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.bancs), oBanc)
        End If
        Return retval
    End Function

    Private Function Mod115Payment() As DTOCca
        Dim retval As DTOCca = Nothing
        Dim oBanc As DTOBanc = Xl_BancsComboBox1.SelectedItem
        Dim DtFch As Date = YearMonth.LastFch.AddDays(20)
        Dim oUser As DTOUser = Current.Session.User
        Dim oLang As DTOLang = DTOLang.CAT
        Dim oBaseQuotas As List(Of DTOBaseQuota) = Xl_Fiscal_Irpf1.Ctas
        Dim oLloguers As DTOBaseQuota = oBaseQuotas.FirstOrDefault(Function(x) DirectCast(x.Source, DTOCce).Cta.Codi = DTOPgcPlan.Ctas.IrpfLloguers)
        If oLloguers IsNot Nothing Then
            retval = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.IRPF, 111)
            retval.Concept = String.Format("{0}-Hisenda-Mod 115 Lloguers {1} {2}", oBanc.AbrOrNom(), oLang.MesAbr(YearMonth.Month), YearMonth.Year)
            retval.AddDebit(oLloguers.Quota, _Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IrpfLloguers))
            retval.AddSaldo(_Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.bancs), oBanc)
        End If
        Return retval
    End Function

    Private Function Mod111() As DTOCca
        Dim retval As DTOCca = Nothing
        Dim DtFch As Date = YearMonth.LastFch
        Dim oUser As DTOUser = Current.Session.User
        Dim oLang As DTOLang = DTOLang.CAT
        Dim oBaseQuotas As List(Of DTOBaseQuota) = Xl_Fiscal_Irpf1.Ctas
        Dim oTreballadors As DTOBaseQuota = oBaseQuotas.FirstOrDefault(Function(x) DirectCast(x.Source, DTOCce).Cta.Codi = DTOPgcPlan.Ctas.IrpfTreballadors)
        Dim oProfessionals As DTOBaseQuota = oBaseQuotas.FirstOrDefault(Function(x) DirectCast(x.Source, DTOCce).Cta.Codi = DTOPgcPlan.Ctas.IrpfProfessionals)
        If Not (oProfessionals Is Nothing And oTreballadors Is Nothing) Then
            retval = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.IRPF, 111)
            retval.Concept = String.Format("Hisenda-Mod 111 IRPF {0} {1}", oLang.MesAbr(YearMonth.Month), YearMonth.Year)
            If oTreballadors IsNot Nothing Then
                retval.AddDebit(oTreballadors.Quota, DirectCast(oTreballadors.Source, DTOCce).Cta)
            End If
            If oProfessionals IsNot Nothing Then
                retval.AddDebit(oProfessionals.Quota, DirectCast(oProfessionals.Source, DTOCce).Cta)
            End If
            retval.AddSaldo(_Ctas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Irpf))
        End If
        Return retval
    End Function

    Private Function TancamentSubComptes() As List(Of DTOCca)
        Dim retval As New List(Of DTOCca)
        Dim DtFch As Date = YearMonth.LastFch
        Dim oUser As DTOUser = Current.Session.User
        Dim oLang As DTOLang = DTOLang.CAT
        Dim oBaseQuotas As List(Of DTOBaseQuota) = Xl_Fiscal_Irpf1.SubCtas
        Dim oCta As New DTOPgcCta()
        Dim oCca As DTOCca = Nothing
        For Each oBaseQuota As DTOBaseQuota In oBaseQuotas
            Dim oSource As DTOCcd = oBaseQuota.Source
            If oSource.Cta.UnEquals(oCta) Then
                If oCca IsNot Nothing Then oCca.AddSaldo(oCta)
                oCta = oSource.Cta
                oCca = DTOCca.Factory(DtFch, oUser, DTOCca.CcdEnum.IRPF, oCta.Codi)
                oCca.Concept = String.Format("Hisenda-{0} {1} {2}", oCta.Nom.Tradueix(oLang), oLang.MesAbr(YearMonth.Month), YearMonth.Year)
                retval.Add(oCca)
            End If
            oCca.AddDebit(oBaseQuota.Quota, oCta, oSource.Contact)
        Next
        If oCca IsNot Nothing Then oCca.AddSaldo(oCta)
        Return retval
    End Function
#End Region


    Private Function YearMonth() As DTOYearMonth
        Dim iYear As Integer = ComboBoxYea.SelectedItem
        Dim iMonth As Integer = ComboBoxMonth.SelectedIndex + 1
        Dim retval As New DTOYearMonth(iYear, iMonth)
        Return retval
    End Function

    Private Async Function LoadCombos(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oYearMonth As DTOYearMonth = DTOYearMonth.Previous

        Dim iYeas As New List(Of Integer)
        For i As Integer = DTO.GlobalVariables.Today().Year To 1985 Step -1
            iYeas.Add(i)
        Next
        ComboBoxYea.DataSource = iYeas
        ComboBoxYea.SelectedItem = oYearMonth.Year

        Dim oLang As DTOLang = Current.Session.Lang
        Dim oMonths As New List(Of ListItem2)
        For i As Integer = 1 To 12
            oMonths.Add(New ListItem2(i, oLang.MesAbr(i)))
        Next
        ComboBoxMonth.DataSource = oMonths
        ComboBoxMonth.DisplayMember = "value"
        ComboBoxMonth.ValueMember = "key"
        ComboBoxMonth.SelectedIndex = oYearMonth.Month - 1

        Await Xl_BancsComboBox1.LoadDefaultsFor(DTODefault.Codis.BancNominaTransfers, exs)
        Return exs.Count = 0
    End Function

    Private Async Sub ComboBoxYea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles _
        ComboBoxYea.SelectedIndexChanged,
         ComboBoxMonth.SelectedIndexChanged

        If _AllowEvents Then
            Await refresca()
        End If
    End Sub
End Class