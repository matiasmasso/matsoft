Public Class Frm_Rols
    Private _SelectionMode As BLL.Defaults.SelectionModes

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oSelectionMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _SelectionMode = oSelectionMode
    End Sub

    Private Sub Frm_Rols_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oRols As List(Of DTORol) = BLL.BLLRols.All(BLL.BLLSession.Current.User.Lang)
        Xl_Rols1.Load(oRols, _SelectionMode)
    End Sub


    Private Sub Xl_Rols1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Rols1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub
End Class