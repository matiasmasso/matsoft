Public Class Xl_Filename
    Inherits Xl_LookupTextboxButton

    Public Property Filename As String
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Sub Xl_Fiename_onLookUpRequest(sender As Object, e As MatEventArgs) Handles Me.onLookUpRequest
        Dim oDlg As New OpenFileDialog
        With oDlg
            If .ShowDialog = DialogResult.OK Then
                MyBase.Text = .FileName
                RaiseEvent AfterUpdate(Me, New MatEventArgs(.FileName))
            End If
        End With
    End Sub

    Private Sub TextBox1_DragEnter(sender As Object, e As DragEventArgs) Handles TextBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            '    or this tells us if it is an Outlook attachment drop
            e.Effect = DragDropEffects.Copy
        Else
            '    or none of the above
            e.Effect = DragDropEffects.None
        End If

    End Sub

    Private Sub TextBox1_DragDrop(sender As Object, e As DragEventArgs) Handles TextBox1.DragDrop
        Dim exs As New List(Of Exception)

        Dim oDocFiles As New List(Of DTODocFile)
        If DragDropHelper.GetDroppedDocFiles(e, oDocFiles, exs) Then
            If oDocFiles.Count > 0 Then
                Dim sFilename = oDocFiles.First.filename
                Dim tmpFolder = MatHelperStd.FileSystemHelper.PathToTmp()
                MatHelperStd.FileSystemHelper.SaveStream(oDocFiles.First.stream, exs, tmpFolder & "\" & sFilename)
                MyBase.Text = tmpFolder & "\" & sFilename
                RaiseEvent AfterUpdate(Me, New MatEventArgs(tmpFolder & "\" & sFilename))
            End If
        Else
            UIHelper.WarnError(exs, "error al importar fitxers")
        End If
    End Sub

End Class
