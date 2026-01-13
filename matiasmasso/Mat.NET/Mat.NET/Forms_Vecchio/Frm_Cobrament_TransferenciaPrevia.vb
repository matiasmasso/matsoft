

Public Class Frm_Cobrament_TransferenciaPrevia
    Private mPdc As Pdc = Nothing
    Private mAlb As Alb = Nothing
    Private mCca As Cca = Nothing
    Private mContact As Contact = Nothing
    Private mPrefixe As String = ""
    Private mAmt As MaxiSrvr.Amt
    Private _Allowevents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPdc As Pdc)
        MyBase.New()
        Me.InitializeComponent()
        mPdc = oPdc
        mContact = mPdc.Client.CcxOrMe
        mAmt = oPdc.TotalVatIncluded
        mPrefixe = "-transf.comanda " & mPdc.Id & " de "
        Refresca()
    End Sub

    Public Sub New(ByVal oAlb As Alb)
        MyBase.New()
        Me.InitializeComponent()
        mAlb = oAlb
        mContact = mAlb.Client.CcxOrMe
        mAmt = mAlb.TotalCash
        mPrefixe = "-transf.albará " & mAlb.Id & " de "
        Refresca()
    End Sub

    Public ReadOnly Property Pdc() As Pdc
        Get
            Return mPdc
        End Get
    End Property

    Public ReadOnly Property Alb() As Alb
        Get
            Return mAlb
        End Get
    End Property

    Public ReadOnly Property Cca() As Cca
        Get
            Return mCca
        End Get
    End Property

    Private Sub Refresca()
        Xl_Contact1.Contact = mContact
        DateTimePicker1.Value = Today
        Dim oDefaultBanc As New DTOBanc(New Guid(BLL.BLLDefault.EmpValue(DTODefault.Codis.BancNominaTransfers)))
        Xl_Bancs_Select1.Banc = oDefaultBanc
        Xl_Amt1.Amt = mAmt
        BuildConcepte()
        _Allowevents = True
    End Sub

    Private Sub BuildConcepte()
        Dim oBanc As DTOBanc = Xl_Bancs_Select1.Banc
        Dim sBanc As String = ""
        If oBanc IsNot Nothing Then
            sBanc = oBanc.Abr
        End If
        Dim s As String = sBanc & mPrefixe & Xl_Contact1.Contact.Clx
        TextBoxConcepte.Text = s
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim iYea As Integer = DateTimePicker1.Value.Year
        Dim oCtaBancs As PgcCta = PgcPlan.FromYear(iYea).Cta(DTOPgcPlan.ctas.bancs)
        Dim oCtaClients As PgcCta = PgcPlan.FromYear(iYea).Cta(DTOPgcPlan.ctas.clients_Anticips)

        mCca = New Cca(mContact.Emp)
        With mCca
            .Ccd = DTOCca.CcdEnum.CobramentACompte
            .fch = DateTimePicker1.Value
            .Txt = TextBoxConcepte.Text

            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If

            Dim oBanc As New Banc(Xl_Bancs_Select1.Banc.Guid)
            .ccbs.Add(New Ccb(oCtaBancs, oBanc, Xl_Amt1.Amt, DTOCcb.DhEnum.Debe))
            .ccbs.Add(New Ccb(oCtaClients, Xl_Contact1.Contact, Xl_Amt1.Amt, DTOCcb.DhEnum.Haber))

            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With

        If mAlb IsNot Nothing Then
            mAlb.RetencioCod = DTODelivery.RetencioCods.Free
            mAlb.SetUser(BLL.BLLSession.Current.User)

            Dim exs As New List(Of Exception)
            If Not mAlb.Update( exs) Then
                MsgBox( BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End If

        RaiseEvent AfterUpdate(Me, EventArgs.Empty)
        Me.Close()
    End Sub


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Contact1.AfterUpdate, _
     DateTimePicker1.ValueChanged, _
       TextBoxConcepte.TextChanged, _
        Xl_DocFile1.AfterFileDropped

        If _Allowevents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_Bancs_Select1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Bancs_Select1.AfterUpdate
        BuildConcepte()
    End Sub


End Class