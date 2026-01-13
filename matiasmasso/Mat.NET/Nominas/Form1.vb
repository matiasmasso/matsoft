Imports MatHelperCFwk
Imports MatHelperStd

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim exs As New List(Of Exception)
        Dim oDocfile As New DTODocFile
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "fitxers Pdf|*.pdf|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim bytes As Byte() = {}
                If DocfileHelper.Load(.FileName, bytes, 350, 400, exs) Then
                    DocfileHelper.LoadFromStream(bytes, oDocfile, exs, .FileName)
                End If
                Dim src = MatHelperCFwk.PdfHelper.readText(.FileName, exs)
                If exs.Count = 0 Then
                    Dim segments = src.Split(vbLf).ToList
                    If NominaEscuraHelper.isNominaEscura(segments) Then
                        If oDocfile.Stream IsNot Nothing AndAlso Not IO.Directory.Exists(oDocfile.Filename) Then
                            FileSystemHelper.SaveStream(oDocfile.Stream, exs, oDocfile.Filename)
                            If exs.Count = 0 Then
                                Dim oFrm As New Frm_NominasFactory(oDocfile.Filename)
                                oFrm.Show()
                            End If
                        End If
                    Else
                        MsgBox("aquest fitxer no es de nomines", MsgBoxStyle.Exclamation)
                    End If
                End If
            End If
        End With
    End Sub
End Class
