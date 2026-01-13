

Public Class Frm_IntlAgent
    Private mIntlAgent As DTOIntlAgent
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oIntlAgent As DTOIntlAgent)
        MyBase.New()
        Me.InitializeComponent()
        mIntlAgent = oIntlAgent
        'Me.Text = mObject.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mIntlAgent
            Xl_ContactPrincipal.Contact = .Principal
            Xl_ContactAgent.Contact = mIntlAgent
            Xl_Lookup_Area1.Area = .Areas.First
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        Xl_ContactPrincipal.AfterUpdate, _
         Xl_ContactAgent.AfterUpdate, _
           CheckBoxObsolet.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oPrincipal As DTOContact = Xl_ContactPrincipal.Contact
        Dim oAgent As DTOContact = Xl_ContactAgent.Contact
        Dim oArea As DTOArea = Xl_Lookup_Area1.Area

        UIHelper.WarnError("per implementar")
        Dim oIntlAgent As New DTOIntlAgent
        With oIntlAgent
            '.Principal = oPrincipal
            '.Agents = {oAgent}
            '.Areas = oArea
            '.Obsoleto = CheckBoxObsolet.Checked
            '.Update()
            'RaiseEvent AfterUpdate(oIntlAgent, System.EventArgs.Empty)
            'Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        UIHelper.WarnError("per implementar")
    End Sub
End Class