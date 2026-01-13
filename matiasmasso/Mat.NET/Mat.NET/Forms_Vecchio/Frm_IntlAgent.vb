

Public Class Frm_IntlAgent
    Private mIntlAgent As IntlAgent
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oIntlAgent As IntlAgent)
        MyBase.new()
        Me.InitializeComponent()
        mIntlAgent = oIntlAgent
        'Me.Text = mObject.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mIntlAgent
            Xl_ContactPrincipal.Contact = .Principal
            Xl_ContactAgent.Contact = .Agent
            Xl_Lookup_Area1.Area = .Area
            If .Exists Then
                ButtonDel.Enabled = .Exists
            End If
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
        Dim oPrincipal As Contact = Xl_ContactPrincipal.Contact
        Dim oAgent As Contact = Xl_ContactAgent.Contact
        Dim oArea As DTOArea = Xl_Lookup_Area1.Area

        Dim oIntlAgent As New IntlAgent(oPrincipal, oAgent, oArea)
        With oIntlAgent
            .Obsoleto = CheckBoxObsolet.Checked
            .Update()
            RaiseEvent AfterUpdate(oIntlAgent, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mIntlAgent.allowDelete Then
            mIntlAgent.delete()
            Me.Close()
        End If
    End Sub
End Class