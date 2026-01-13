

Public Class Frm_Cobrament_TransferenciaPrevia
    Private _Delivery As DTODelivery
    Private _Allowevents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Sub New(ByVal oDelivery As DTODelivery)
        MyBase.New()
        Me.InitializeComponent()
        _Delivery = oDelivery
        Refresca()
    End Sub

    Private Async Sub Refresca()
        Dim exs As New List(Of Exception)
        Xl_Contact21.Contact = FEB2.Customer.CcxOrMe(exs, _Delivery.Customer)
        DateTimePicker1.Value = Today
        Dim oGuid = Await FEB2.Default.EmpGuid(Current.Session.Emp, DTODefault.Codis.BancNominaTransfers, exs)
        If exs.Count = 0 Then
            Dim oDefaultBanc As New DTOBanc(oGuid)
            Await Xl_Bancs_Select1.Load(oDefaultBanc)
            Xl_Amt1.Amt = _Delivery.Liquid
            TextBoxConcepte.Text = GetConcepte()
            Xl_Bancs_Select1.Focus()
            If Xl_Bancs_Select1.Banc IsNot Nothing Then ButtonOk.Enabled = True
            _Allowevents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)

        Dim oDocfile As DTODocFile = Nothing
        If Xl_DocFile1.IsDirty Then
            oDocfile = Xl_DocFile1.Value
        End If

        Dim value = DTOCobramentPerTransferencia.Factory(user:=Current.Session.User,
                                                delivery:=_Delivery,
                                                fch:=DateTimePicker1.Value,
                                                contact:=Xl_Contact21.Contact,
                                                banc:=Xl_Bancs_Select1.Banc,
                                                concepte:=TextBoxConcepte.Text,
                                                amt:=Xl_Amt1.Amt,
                                                docFile:=oDocfile)

        Dim oCca = Await FEB2.Delivery.CobraPerTransferenciaPrevia(exs, value)
        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oCca))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar el cobrament:")
        End If

    End Sub


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Contact21.AfterUpdate,
     DateTimePicker1.ValueChanged,
       TextBoxConcepte.TextChanged,
        Xl_DocFile1.AfterFileDropped

        If _Allowevents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_Bancs_Select1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Bancs_Select1.AfterUpdate
        TextBoxConcepte.Text = GetConcepte()
        If _Allowevents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Function GetConcepte() As String
        Dim oBanc As DTOBanc = Xl_Bancs_Select1.Banc
        Dim sBanc As String = ""
        If oBanc IsNot Nothing Then
            sBanc = oBanc.AbrOrNom()
        End If
        Dim retval As String = String.Format("{0}-transf.albará {1} de {2}", sBanc, _Delivery.Id, Xl_Contact21.Contact.FullNom)
        Return retval
    End Function

End Class