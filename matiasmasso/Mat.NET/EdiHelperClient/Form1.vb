Public Class Form1
    Private Sub ButtonImport_Click(sender As Object, e As EventArgs) Handles ButtonImport.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            If .ShowDialog = DialogResult.OK Then
                Dim oEdiFile = EdiHelperStd.EdiFile.FromFile(exs, .FileName)
                If exs.Count = 0 Then
                    Select Case oEdiFile.Schema
                        Case EdiHelperStd.EdiFile.Schemas.DESADV
                            Dim oDesadv = EdiHelperStd.Desadv.Factory(exs, oEdiFile.src)
                            If exs.Count = 0 Then
                                MsgBox("Ok", MsgBoxStyle.Information)
                            Else
                                MsgBox(exs.First.Message, MsgBoxStyle.Exclamation)
                            End If
                        Case Else
                            MsgBox("unknown file schema", MsgBoxStyle.Exclamation)
                    End Select
                Else
                    MsgBox(exs.First.Message, MsgBoxStyle.Exclamation)
                End If
            End If
        End With
    End Sub
End Class
