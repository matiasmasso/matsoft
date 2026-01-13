Public Class Frm_Client_Risc
    Private _Ccx As DTOCustomer
    Private mDirtyClassificacions As Boolean = True

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Tabs
        Gral
        Classificacions
    End Enum

    Public Sub New(ByVal oCustomer As DTOCustomer)
        MyBase.New()
        Me.InitializeComponent()
        Dim exs As New List(Of Exception)
        _Ccx = FEB.Customer.CcxOrMe(EXS, oCustomer)
    End Sub

    Private Async Sub Frm_Client_Risc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text & " " & _Ccx.FullNom

        Dim exs As New List(Of Exception)
        If Not Await Xl_Contact_Logo1.Load(exs, _Ccx) Then
            UIHelper.WarnError(exs)
        End If

        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oAmtSdoFras = Await FEB.Risc.FrasPendentsDeVencer(_Ccx, exs)
        Dim oAmtSdoAlbsACredit = Await FEB.Risc.SdoAlbsACredit(_Ccx, exs)
        Dim oAmtSdoAlbsNoCredit = Await FEB.Risc.SdoAlbsNoCredit(_Ccx, exs)
        Dim oAmtSdoEntregatACompte = Await FEB.Risc.EntregatACompte(_Ccx, exs)
        Dim oAmtDiposit = Await FEB.Risc.DipositIrrevocable(_Ccx, exs)
        Dim oClassificacio = Await FEB.Risc.CreditLimit(_Ccx, exs)
        Dim oDisposat = Await FEB.Risc.CreditDisposat(_Ccx, exs)
        Dim oDisponible = Await FEB.Risc.CreditDisponible(_Ccx, exs)

        If exs.Count = 0 Then
            If oAmtSdoFras.Eur <> 0 Then TextBoxSdoCta.Text = DTOAmt.CurFormatted(oAmtSdoFras)
            If oAmtSdoAlbsACredit IsNot Nothing AndAlso oAmtSdoAlbsACredit.Eur <> 0 Then TextBoxSdoAlbsCredit.Text = DTOAmt.CurFormatted(oAmtSdoAlbsACredit)
            If oAmtSdoAlbsNoCredit IsNot Nothing AndAlso oAmtSdoAlbsNoCredit.Eur <> 0 Then TextBoxSdoAlbsNoCredit.Text = DTOAmt.CurFormatted(oAmtSdoAlbsNoCredit)

            If oAmtSdoEntregatACompte.Eur <> 0 Then TextBoxEntregatACompte.Text = DTOAmt.CurFormatted(oAmtSdoEntregatACompte)
            If oAmtDiposit.Eur <> 0 Then TextBoxDiposit.Text = DTOAmt.CurFormatted(oAmtDiposit)

            If oDisposat.Eur <> 0 Then TextBoxDisposat.Text = DTOAmt.CurFormatted(oDisposat)
            If oClassificacio.Eur <> 0 Then TextBoxClassificacio.Text = DTOAmt.CurFormatted(oClassificacio)
            If oDisponible.Eur <> 0 Then TextBoxDisponible.Text = DTOAmt.CurFormatted(oDisponible)


            Await SetSdoDue()
            'SetSdoImpagats()
            SetIndexImpagats()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Sub ButtonExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonExit.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonAlbsCredit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAlbsCredit.Click
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await FEB.Deliveries.Headers(exs, Current.Session.Emp, contact:=_Ccx, group:=True)
        If exs.Count = 0 Then
            oDeliveries = oDeliveries.Where(Function(x) x.Facturable = True And x.Invoice Is Nothing).
            ToList

            Dim oFrm As New Frm_Deliveries(oPurpose:=Xl_Deliveries.Purposes.MultipleCustomers, oDeliveries:=oDeliveries, sCaption:="Albarans del Grup de " & _Ccx.FullNom)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonPnds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPnds.Click
        Dim oFrm As New Frm_Extracte(_Ccx)
        oFrm.Show()
    End Sub

    Public Async Function SetSdoDue() As Task
        Dim exs As New List(Of Exception)
        Dim oPnds = Await FEB.Pnds.All(exs, GlobalVariables.Emp, _Ccx)
        oPnds = oPnds.Where(Function(x) _
                      x.Status < DTOPnd.StatusCod.saldat _
            And x.Cta.Codi = DTOPgcPlan.Ctas.Clients _
            And x.Vto < DTO.GlobalVariables.Today()).ToList

        Dim DcSaldo As Decimal = oPnds.Sum(Function(x) IIf(x.Cod = DTOPnd.Codis.Deutor, x.Amt.Eur, -x.Amt.Eur))

        Dim DcDiasPonderats As Decimal
        Dim tmp As Decimal = oPnds.Sum(Function(x) DTOPnd.EurDeutor(x))
        If tmp <> 0 Then
            DcDiasPonderats = oPnds.Sum(Function(x) DTOPnd.DueDays(x) * DTOPnd.EurDeutor(x)) / tmp
        End If

        If DcSaldo <> 0 Then
            TextBoxSdoDue.Text = DTOAmt.CurFormatted(DTOAmt.Factory(DcSaldo))
            PictureBoxDue.Visible = True
        End If
        If DcDiasPonderats > 0 Then
            TextBoxDueDias.Text = DcDiasPonderats.ToString
        End If
    End Function


    Public Async Sub SetIndexImpagats(Optional ByVal iDesdeElsDarrersMesos As Integer = 6)
        Dim exs As New List(Of Exception)
        Dim DtFchFrom As Date = DTO.GlobalVariables.Today().AddMonths(-iDesdeElsDarrersMesos)
        Dim sFchFrom As String = DtFchFrom.ToString(System.Globalization.CultureInfo.InvariantCulture)
        Dim DcTot As Decimal = 0
        Dim iIndex = Await FEB.Csbs.IndexImpagats(exs, GlobalVariables.Emp, _Ccx, iDesdeElsDarrersMesos)
        If exs.Count = 0 Then
            Dim iCreditProtocol_MaxImpagatsIndex = Await FEB.Default.EmpInteger(Current.Session.Emp, DTODefault.Codis.CreditProtocol_MaxImpagatsIndex, exs)
            If exs.Count = 0 Then
                PictureBoxWarnIndexImpagats.Visible = iIndex > iCreditProtocol_MaxImpagatsIndex
                LabelIndexImpagats.Text = "index d'impagats darrers 6 mesos: "
                TextBoxIndexImpagats.Text = iIndex.ToString & "%"
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonImpagats_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonImpagats.Click
        Dim exs As New List(Of Exception)
        Dim oCta = Await FEB.PgcCta.FromCod(DTOPgcPlan.Ctas.impagats, Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Extracte(_Ccx, oCta)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        Await refresca()
        mDirtyClassificacions = True
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Private Async Sub ButtonSdoDue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSdoDue.Click
        Dim exs As New List(Of Exception)
        Dim oCta = Await FEB.PgcCta.FromCod(DTOPgcPlan.Ctas.Clients, Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Extracte(_Ccx, oCta)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub ButtonLimit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonLimit.Click
        Dim exs As New List(Of Exception)
        Dim oCliCreditLog As New DTOCliCreditLog
        With oCliCreditLog
            .Customer = _Ccx
            FEB.Contact.Load(.Customer, exs)
        End With
        Dim oFrm As New Frm_CliCreditLimit(oCliCreditLog)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Classificacions
                If mDirtyClassificacions Then
                    Dim exs As New List(Of Exception)
                    Dim oLogs = Await FEB.CliCreditLogs.All(_Ccx, exs)
                    If exs.Count = 0 Then
                        Xl_CliCreditLogs1.Load(oLogs)
                        mDirtyClassificacions = False
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub



    Private Sub ButtonDiasImpagat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonDiasImpagat.Click
        'Dim oSheet =ExcelHelper.Sheet.Factory(GetDsDiasImpagat)
        'Dim exs As New List(Of Exception)
        'If Not UIHelper.ShowExcel(oSheet, exs) Then
        'UIHelper.WarnError(exs)
        'End If
    End Sub

    Private Async Sub ButtonAlbsNoCredit_Click(sender As Object, e As EventArgs) Handles ButtonAlbsNoCredit.Click
        Dim exs As New List(Of Exception)
        Dim oDeliveries = Await FEB.Deliveries.Headers(exs, Current.Session.Emp, contact:=_Ccx, group:=True)
        If exs.Count = 0 Then
            oDeliveries = oDeliveries.Where(Function(x) x.Facturable = True And x.Invoice Is Nothing).
            ToList

            Dim oFrm As New Frm_Deliveries(oPurpose:=Xl_Deliveries.Purposes.MultipleCustomers, oDeliveries:=oDeliveries, sCaption:="Albarans del Grup de " & _Ccx.FullNom)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_CliCreditLogs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_CliCreditLogs1.RequestToAddNew
        Dim exs As New List(Of Exception)
        Dim oItem As New DTOCliCreditLog
        With oItem
            .Customer = _Ccx
        End With
        If FEB.Contact.Load(oItem.Customer, exs) Then
            Dim oFrm As New Frm_CliCreditLimit(oItem)
            AddHandler oFrm.AfterUpdate, AddressOf RefrescaCreditLogs
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub RefrescaCreditLogs(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oLogs = Await FEB.CliCreditLogs.All(_Ccx, exs)
        If exs.Count = 0 Then
            Xl_CliCreditLogs1.Load(oLogs)
            mDirtyClassificacions = False
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_CliCreditLogs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CliCreditLogs1.RequestToRefresh
        RefrescaCreditLogs(sender, e)
    End Sub


End Class