Public Class Frm_Rep
    Private _Rep As Rep

    Private Enum Tabs
        General
        RepcomFollowUp
    End Enum

    Public Sub New(oRep As Rep)
        MyBase.New()
        Me.InitializeComponent()
        _Rep = oRep
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.RepcomFollowUp
                Static BlLoadedRepcomFollowUp As Boolean
                If Not BlLoadedRepcomFollowUp Then
                    BlLoadedRepcomFollowUp = True
                    Xl_RepComFollowUp1.Rep = _Rep
                End If

        End Select
    End Sub
End Class