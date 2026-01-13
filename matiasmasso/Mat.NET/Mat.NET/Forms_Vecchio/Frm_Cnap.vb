

Public Class Frm_Cnap
    Private _Cnap As DTOCnap = Nothing

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterDelete(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oCnap As DTOCnap)
        MyBase.New()
        Me.InitializeComponent()
        _Cnap = oCnap

        With _Cnap
            If .Parent IsNot Nothing Then
                TextBoxParent.Text = .Parent.NomShort_ESP
            End If
            TextBoxCod.Text = .Id
            TextBoxNomShort_ESP.Text = .NomShort_ESP
            TextBoxNomShort_CAT.Text = .NomShort_CAT
            TextBoxNomShort_ENG.Text = .NomShort_ENG
            TextBoxNomLong_ESP.Text = .NomLong_ESP
            TextBoxNomLong_CAT.Text = .NomLong_CAT
            TextBoxNomLong_ENG.Text = .NomLong_ENG
        End With
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

        With _Cnap
            .Id = TextBoxCod.Text
            .NomShort_ESP = TextBoxNomShort_ESP.Text
            .NomShort_CAT = TextBoxNomShort_CAT.Text
            .NomShort_ENG = TextBoxNomShort_ENG.Text
            .NomLong_ESP = TextBoxNomLong_ESP.Text
            .NomLong_CAT = TextBoxNomLong_CAT.Text
            .NomLong_ENG = TextBoxNomLong_ENG.Text
        End With

        Dim exs as New List(Of exception)
        If CnapLoader.Update(_Cnap, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cnap))
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar el codi")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxCod.TextChanged, _
     TextBoxNomShort_ESP.TextChanged, _
      TextBoxNomShort_CAT.TextChanged, _
       TextBoxNomShort_ENG.TextChanged, _
        TextBoxNomLong_ESP.TextChanged, _
         TextBoxNomLong_ENG.TextChanged

        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem " & BLL_Cnap.Nom(_Cnap, BLL_Cnap.NomCodis.Short, BLL.BLLSession.Current) & "?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If CnapLoader.Delete(_Cnap, exs) Then
                RaiseEvent AfterDelete(_Cnap, EventArgs.Empty)
            Else
                UIHelper.WarnError( exs, "error al eliminar el codi")
            End If
        End If
    End Sub
End Class