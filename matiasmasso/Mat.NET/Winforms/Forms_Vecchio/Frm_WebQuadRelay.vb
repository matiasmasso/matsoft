Public Class Frm_WebQuadRelay

    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Dim o As New maxisrvr.WebRelayQUAD(maxisrvr.WebRelayQUAD.OpRequest.Relay1_PULSE)
    End Sub

    Private Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        Dim o As New maxisrvr.WebRelayQUAD(maxisrvr.WebRelayQUAD.OpRequest.Relay2_PULSE)
    End Sub

    Private Sub Button3_Click(sender As Object, e As System.EventArgs) Handles Button3.Click
        Dim o As New maxisrvr.WebRelayQUAD(maxisrvr.WebRelayQUAD.OpRequest.Relay3_PULSE)
    End Sub

    Private Sub Button4_Click(sender As Object, e As System.EventArgs) Handles Button4.Click
        Dim o As New maxisrvr.WebRelayQUAD(maxisrvr.WebRelayQUAD.OpRequest.Relay4_PULSE)
    End Sub
End Class