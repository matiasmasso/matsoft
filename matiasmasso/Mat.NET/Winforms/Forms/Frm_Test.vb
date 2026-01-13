Public Class Frm_Test
    Private Async Sub ButtonUpload_Click(sender As Object, e As EventArgs) Handles ButtonUpload.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = ".mp4|*.mp4"
            If .ShowDialog = DialogResult.OK Then
                Dim oHelper = New MatHelperStd.YouTubeUploadHelper

                Try
                    Dim o As New YouTubeUploadHelper()
                    Await o.Upload(.FileName)
                Catch ex As AggregateException
                    UIHelper.WarnError(ex)
                End Try
            End If
        End With
    End Sub
End Class