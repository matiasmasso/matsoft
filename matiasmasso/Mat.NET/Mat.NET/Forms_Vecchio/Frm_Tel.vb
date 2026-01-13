Public Class Frm_Tel
    Private mTel As Tel

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByRef oTel As Tel)
        MyBase.New()
        Me.InitializeComponent()
        mTel = oTel
        With mTel
            Select Case .Cod
                Case Tel.Cods.tel
                    PictureBox1.Image = My.Resources.tel64
                Case Tel.Cods.fax
                    PictureBox1.Image = My.Resources.fax64
                Case Tel.Cods.movil
                    PictureBox1.Image = My.Resources.movil64
            End Select

            Xl_Pais1.Country = New DTOCountry(.Country.Guid)
            TextBoxNum.Text = .Num
            TextBoxObs.Text = .Obs
            CheckBoxPrivat.Checked = (.Privat = MaxiSrvr.TriState.Verdadero)
        End With
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNum.TextChanged, _
     TextBoxObs.TextChanged, _
      CheckBoxPrivat.CheckedChanged

        ButtonOk.Enabled = True

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mTel
            .Num = TextBoxNum.Text
            .Obs = TextBoxObs.Text
            .Privat = IIf(CheckBoxPrivat.Checked, MaxiSrvr.TriState.Verdadero, MaxiSrvr.TriState.Falso)
            Dim exs as New List(Of exception)
            If .Update( exs) Then
                RaiseEvent AfterUpdate(mTel, EventArgs.Empty)
                Me.Close()
            Else
                MsgBox("error al gravar telefon/fax" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class