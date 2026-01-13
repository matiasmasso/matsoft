

Public Class Frm_VehicleModel
    Private _Model As DTOVehicleModel

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oModel As DTOVehicleModel)
        MyBase.New()
        Me.InitializeComponent()
        _Model = oModel
        TextBoxMarca.Text = _Model.Marca.Nom
        TextBoxNom.Text = _Model.Nom
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Model
            .Nom = TextBoxNom.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.VehicleModel.Update(_Model, exs) Then
            RaiseEvent AfterUpdate(_Model, New MatEventArgs(_Model))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class