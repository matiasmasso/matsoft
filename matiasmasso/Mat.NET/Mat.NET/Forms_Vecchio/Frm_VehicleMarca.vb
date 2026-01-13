

Public Class Frm_VehicleMarca
    Private mMarca As VehicleMarca

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMarca As VehicleMarca)
        MyBase.New()
        Me.InitializeComponent()
        mMarca = oMarca
        TextBoxNom.Text = mMarca.Nom
        ButtonDel.Enabled = mMarca.AllowDelete
    End Sub

    Private Sub TextBoxNom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mMarca
            .Nom = TextBoxNom.Text
            .Logo = Xl_Image1.Bitmap
            .Update()
        End With
        RaiseEvent AfterUpdate(mMarca, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mMarca.AllowDelete Then
            If mMarca.Delete Then
                RaiseEvent AfterUpdate(mMarca, EventArgs.Empty)
                Me.Close()
            Else
                MsgBox("cal eliminar primer els models de la marca", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox("cal eliminar primer els models de la marca", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub
End Class