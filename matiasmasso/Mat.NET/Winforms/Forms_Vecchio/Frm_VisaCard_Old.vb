

Public Class Frm_VisaCard_Old
    Private mVisa As VisaCard

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Visa() As VisaCard
        Set(ByVal value As VisaCard)
            mVisa = value
            TextBox1.Text = mVisa.Nom
            Xl_Image1.Bitmap = mVisa.Img
            Me.Text = Me.Text & " (#" & mVisa.Id & ")"
        End Set
    End Property

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBox1.TextChanged, _
     Xl_Image1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mVisa
            .Nom = TextBox1.Text
            .Img = Xl_Image1.Bitmap
            .Update()
        End With
        RaiseEvent AfterUpdate(mVisa, e)
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta tarja de credit?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            mVisa.delete()
            RaiseEvent AfterUpdate(mVisa, e)
            Me.Close()
        End If
    End Sub
End Class