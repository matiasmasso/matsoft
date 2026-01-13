Public Class Xl_HighRes
    Private mHighRes As maxisrvr.HighRes

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Property HighRes() As maxisrvr.HighRes
        Get
            Return mHighRes
        End Get
        Set(ByVal value As maxisrvr.HighRes)
            If value IsNot Nothing Then
                mHighRes = value
                Refresca()
            End If
        End Set
    End Property

    Private Sub Refresca()
        Select Case mHighRes.Format
            Case maxisrvr.HighRes.Formats.jpg
                Dim oImg As Image = maxisrvr.GetImgFromByteArray(mHighRes.Stream)
                Dim oThumb As Image = BLL.ImageHelper.GetThumbnailToFit(oImg, PictureBox1.Width, PictureBox1.Height)
                PictureBox1.Image = oThumb
            Case maxisrvr.HighRes.Formats.zip
                PictureBox1.Image = My.Resources.Zip86
        End Select
        TextBox1.Text = mHighRes.Features
    End Sub

    Private Sub ImportFromFile()
        Dim oDlg As New System.Windows.Forms.OpenFileDialog
        Dim oResult As System.Windows.Forms.DialogResult

        With oDlg
            .Title = "importar fitxer d'alta resolució"
            .Filter = "*.jpg|*.jpg;*.jpeg|*.gif|*.gif|*.zip|*.zip|tots els arxius|*.*"
            .FilterIndex = 4
            oResult = .ShowDialog
            Select Case oResult
                Case System.Windows.Forms.DialogResult.OK
                    Dim sFilename As String = .FileName
                    Dim sExtension As String = .FileName.Substring(.FileName.LastIndexOf("."))
                    Dim oByteArray As Byte() = Nothing
                    Dim exs as New List(Of exception)
                    Dim oFormat As HighRes.Formats
                    Select Case UCase(sExtension)
                        Case ".JPG", ".JPEG", ".GIF"
                            oFormat = MaxiSrvr.HighRes.Formats.jpg
                        Case ".ZIP"
                            oFormat = MaxiSrvr.HighRes.Formats.zip
                    End Select

                    If BLL.FileSystemHelper.GetStreamFromFile(.FileName, oByteArray, exs) Then
                        mHighRes = New MaxiSrvr.HighRes(oByteArray, oFormat)
                        RaiseEvent AfterUpdate(mHighRes, New System.EventArgs)
                        Refresca()
                    Else
                        MsgBox("error al importar el fitxer" & Environment.NewLine & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                    End If
            End Select

        End With
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        ImportFromFile()
    End Sub
End Class
