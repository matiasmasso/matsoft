Public Class Frm_SegSocNewCca
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mAllowEvents As Boolean



    Private Sub Frm_SegSocNewCca_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadDefaults()
    End Sub

    Private Sub LoadDefaults()

        For iMes As Integer = 1 To 12
            ComboBoxMes.Items.Add(BLL.BLLApp.Lang.MesAbr(iMes))
        Next

        'get last
        Dim oLastCca As DTOCca = BLLCcas.Last(DTOCca.CcdEnum.SegSocTc1)

        Dim DtFch As Date = Today
        If oLastCca IsNot Nothing Then
            DtFch = oLastCca.Fch
            DtFch = DtFch.AddMonths(1)
        End If

        With NumericUpDownYea
            .Minimum = 1985
            .Maximum = 2100
            .Value = DtFch.Year
        End With

        ComboBoxMes.SelectedIndex = DtFch.Month - 1
        Dim oGuid As New Guid(BLL.BLLDefault.EmpValue(DTODefault.Codis.BancNominaTransfers))
        Dim oBanc As New Banc(oGuid)
        Xl_DropdownList_BancsNostres1.Banc = oBanc
        SetFchDevengo()
        SetFchPagament()
        SetTxt()
        mAllowEvents = True
    End Sub

    Private Sub SetFchDevengo()
        Dim DtFirstDayOfMonth As New Date(CurrentYea, ComboBoxMes.SelectedIndex + 1, 1)
        Dim DtLastDayOfMonth As Date = DtFirstDayOfMonth.AddDays(-1).AddMonths(1)
        DateTimePicker1.Value = DtLastDayOfMonth
    End Sub

    Private Sub SetFchPagament()
        Dim DtFirstDayOfDevengoMonth As New Date(DateTimePicker1.Value.Year, DateTimePicker1.Value.Month, 1)
        Dim DtFirstDayOfPagamentMonth As Date = DtFirstDayOfDevengoMonth.AddMonths(1)
        Dim DtLastDayOfMonth As Date = DtFirstDayOfPagamentMonth.AddDays(-1).AddMonths(1)
        DateTimePicker2.Value = DtLastDayOfMonth
    End Sub

    Private Sub SetTxt()
        Dim sPeriod As String = LangAccounts.MesAbr(DateTimePicker1.Value.Month) & " " & CurrentYea().ToString
        TextBox1.Text = "Butlletí Seg.Social " & sPeriod
        TextBox2.Text = Xl_DropdownList_BancsNostres1.Banc.Abr & "-Pagament Seg.Social " & sPeriod
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If Xl_DocFile1.Value Is Nothing Then
            Dim RC As MsgBoxResult = MsgBox("no hi ha PDF. Ho tirem endavant així?", MsgBoxStyle.OkCancel, "MAT.NET")
            If RC <> MsgBoxResult.Ok Then Exit Sub
        End If
        SaveCcaDevengo()
        SaveCcaPagament()
        Me.Close()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DateTimePicker1.ValueChanged
        If mAllowEvents Then
            SetFchPagament()
            SetTxt()
        End If
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker2.ValueChanged
        If mAllowEvents Then
            SetTxt()
        End If
    End Sub

    Private Function CurrentYea() As Integer
        Return NumericUpDownYea.Value
    End Function

    Private Sub SaveCcaDevengo()
        Dim oPlan As PgcPlan = PgcPlan.FromYear(CurrentYea)
        Dim oCca As new cca(BLL.BLLApp.emp)
        With oCca
            .fch = DateTimePicker1.Value
            .Txt = TextBox1.Text
            .Ccd = DTOCca.CcdEnum.SegSocTc1
            .ccbs.Add(New Ccb(oPlan.Cta(DTOPgcPlan.ctas.SegSocialDevengo), , Xl_Amt1.Amt, DTOCcb.DhEnum.Debe))
            .ccbs.Add(New Ccb(oPlan.Cta(DTOPgcPlan.ctas.SegSocialCreditor), , Xl_Amt1.Amt, DTOCcb.DhEnum.Haber))
            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If

            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With
    End Sub

    Private Sub SaveCcaPagament()
        Dim oPlan As PgcPlan = PgcPlan.FromYear(CurrentYea)
        Dim oCca As new cca(BLL.BLLApp.emp)
        With oCca
            .fch = DateTimePicker2.Value
            .Txt = TextBox2.Text
            .Ccd = DTOCca.CcdEnum.Pagament
            .ccbs.Add(New Ccb(oPlan.Cta(DTOPgcPlan.ctas.SegSocialCreditor), , Xl_Amt1.Amt, DTOCcb.DhEnum.Debe))
            .ccbs.Add(New Ccb(oPlan.Cta(DTOPgcPlan.ctas.bancs), Xl_DropdownList_BancsNostres1.Banc, Xl_Amt1.Amt, DTOCcb.DhEnum.Haber))

            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If

        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_Amt1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Amt1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub


    Private Sub Xl_FileDocument1_AfterUpdate(sender As Object, e As EventArgs)
        LabelUploadInfo.Visible = False
    End Sub
End Class