

Public Class Frm_InfoJob
    Private mJob As infojob

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property InfoJob() As InfoJob
        Get
            Return mJob
        End Get
        Set(ByVal value As InfoJob)
            mJob = value
            If mJob IsNot Nothing Then refresca()
        End Set
    End Property

    Private Sub refresca()
        With mJob
            Xl_Image1.Bitmap = .Image
            TextBoxTit.Text = .Nom
            TextBoxDsc.Text = .Dsc
            CheckBoxObsolet.Checked = .Obsoleto
        End With
        ButtonOk.Enabled = False
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxTit.TextChanged, _
     TextBoxDsc.TextChanged, _
      CheckBoxObsolet.CheckedChanged, _
       Xl_Image1.AfterUpdate

        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mJob
            .Image = Xl_Image1.Bitmap
            .Nom = TextBoxTit.Text
            .Dsc = TextBoxDsc.Text
            .Obsoleto = CheckBoxObsolet.Checked
            .Update()
        End With
        RaiseEvent AfterUpdate(mJob, New System.EventArgs)
        Me.Close()
    End Sub
End Class