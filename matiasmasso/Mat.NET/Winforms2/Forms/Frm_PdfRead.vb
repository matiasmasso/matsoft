Public Class Frm_PdfRead
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
                Display(oDocFiles.First)
            End If
        Else
            UIHelper.WarnError(exs, "error al importar fitxers")
        End If
    End Sub

    Private Sub Display(oDocfile As DTODocFile)
        Dim exs As New List(Of Exception)
        Dim src = LegacyHelper.iTextPdfHelper.readText(oDocfile.Stream, exs)
        If exs.Count = 0 Then
            Dim segments = src.Split(vbLf).ToList
            Dim sb As New Text.StringBuilder
            For i As Integer = 0 To segments.Count - 1
                Dim line = String.Format("{0:000} {1}", i, segments(i))
                sb.AppendLine(line)
            Next
            TextBox1.Text = sb.ToString
        Else
            UIHelper.WarnError(exs)
        End If


    End Sub
End Class