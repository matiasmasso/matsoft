Public Class Frm_Leads
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oMode As bll.dEFAULTS.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()

        Dim oUsers As List(Of DTOUser) = BLL.BLLUsers.All()
        Xl_Leads1.Load(oUsers, oMode)
    End Sub

    Private Sub Xl_Leads1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Leads1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class