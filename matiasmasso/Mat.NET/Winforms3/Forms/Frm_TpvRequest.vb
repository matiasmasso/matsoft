Public Class Frm_TpvRequest
    Private _Contact As DTOContact


    Public Sub New(Optional oContact As DTOContact = Nothing)
        MyBase.New
        InitializeComponent()
        _Contact = oContact
    End Sub

    Private Sub Frm_TpvRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Contact IsNot Nothing Then
            FEB.Contact.Load(_Contact, exs)
            If exs.Count = 0 Then
                Xl_Contact21.Contact = _Contact
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oTpv As New DTOTpvLog
        With oTpv
            .Titular = Xl_Contact21.Contact
            .Ds_ProductDescription = TextBoxConcept.Text
            .Ds_Amount = CInt(Xl_Eur1.Amt.Eur * 100)
        End With
        UIHelper.ToggleProggressBar(Panel1, True)
        Dim oConfig = Await FEB.PaymentGateway.ProductionEnvironment(exs, Current.Session.Emp)
        oTpv = Await FEB.TpvRedSys.BookRequest(oTpv, oConfig, Current.Session.User, oTpv.Titular.Lang, DTOTpvRequest.Modes.Free, Nothing, Xl_Eur1.Amt, TextBoxConcept.Text, exs)
        UIHelper.ToggleProggressBar(Panel1, False)
        If exs.Count = 0 Then
            Dim sUrl = FEB.TpvLog.CustomRequestUrl(oTpv)
            UIHelper.CopyLink(sUrl)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_Eur1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Eur1.AfterUpdate
        If Xl_Eur1.Amt.IsPositive Then
            ButtonOk.Enabled = True
        End If
    End Sub


End Class