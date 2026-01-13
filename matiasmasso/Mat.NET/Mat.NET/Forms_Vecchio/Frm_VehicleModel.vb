

Public Class Frm_VehicleModel
    Private mModel As VehicleModel

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oModel As VehicleModel)
        MyBase.New()
        Me.InitializeComponent()
        mModel = oModel
        TextBoxMarca.Text = mModel.Marca.Nom
        TextBoxNom.Text = mModel.Nom
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mModel
            .Nom = TextBoxNom.Text
            .Update()
        End With
        RaiseEvent AfterUpdate(mModel, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class