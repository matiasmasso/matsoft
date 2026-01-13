Imports Newtonsoft.Json.Linq


Public Class Frm_IntegrationValidations
    Private _urlLocal = "http://localhost:55836/api/"
    Private _urlRemote = "https://matiasmasso-api.azurewebsites.net/api/"

    Private Async Sub ButtonPost_Click(sender As Object, e As EventArgs) Handles ButtonPost.Click
        Dim exs As New List(Of Exception)
        TextBoxResponse.Clear()
        ProgressBar1.Visible = True

        Dim url As String = TextBoxUrl.Text
        Try
            Dim jsonInput = JObject.Parse(TextBoxRequest.Text)
            Dim oJObject As String = Await FEB2.ApiRequest.Execute(Of JObject, Object)(jsonInput, exs, TextBoxUrl.Text)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then

                Dim retval = JsonHelper.Serialize(oJObject, exs)
                If exs.Count = 0 Then
                    TextBoxResponse.Text = retval
                Else
                    TextBoxResponse.Text = ExceptionsHelper.ToFlatString(exs)
                End If
            Else
                TextBoxResponse.Text = ExceptionsHelper.ToFlatString(exs)
            End If

        Catch ex As Exception
            ProgressBar1.Visible = False
            TextBoxResponse.Text = ex.Message
        End Try

    End Sub

    Private Sub Frm_IntegrationValidations_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBoxUrl.Text = _urlLocal
    End Sub

    Private Sub RadioButtonRemote_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonRemote.CheckedChanged
        If RadioButtonRemote.Checked Then
            TextBoxUrl.Text = TextBoxUrl.Text.Replace(_urlLocal, _urlRemote)
        Else
            TextBoxUrl.Text = TextBoxUrl.Text.Replace(_urlRemote, _urlLocal)
        End If
    End Sub

    Private Sub RadioButtonLocal_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonLocal.CheckedChanged

    End Sub
End Class