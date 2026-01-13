

Public Class Frm_VehicleMarca
    Private _Marca As DTOVehicle.Marca

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMarca As DTOVehicle.Marca)
        MyBase.New()
        Me.InitializeComponent()
        _Marca = oMarca
        TextBoxNom.Text = _Marca.Nom
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Marca
            .Nom = TextBoxNom.Text
            '.Logo = Xl_Image1.Bitmap
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.VehicleMarca.Update(_Marca, exs) Then
            RaiseEvent AfterUpdate(_Marca, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB.VehicleMarca.Delete(_Marca, exs) Then
            RaiseEvent AfterUpdate(_Marca, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class