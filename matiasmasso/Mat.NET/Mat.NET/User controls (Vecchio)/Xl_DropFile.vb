Public Class Xl_DropFile
    Private _DocFiles As List(Of DTODocFile)

    Public Event onFileDropped(ByVal sender As Object, ByVal e As DropEventArgs)

    Public Enum Status
        Ready
        Wait
    End Enum

    Private Sub Xl_DropFile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PictureBox1.AllowDrop = True
    End Sub

    Public ReadOnly Property DocFiles As List(Of DTODocFile)
        Get
            Return _DocFiles
        End Get
    End Property

    Private Sub PictureBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.Copy
            DisplayStatus(Status.Wait)

            '    or this tells us if it is an Outlook attachment drop
        ElseIf (e.Data.GetDataPresent("FileGroupDescriptor")) Then
            e.Effect = DragDropEffects.Copy
            DisplayStatus(Status.Wait)

            '    or none of the above
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub PictureBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PictureBox1.DragDrop
        DisplayStatus(Status.Wait)

        Dim exs as New List(Of exception)
        Dim oTargetCell As DataGridViewCell = Nothing

        If DragDropHelper.GetDroppedDocFiles(e, _DocFiles, exs) Then
            Dim oArgs As New DropEventArgs(_DocFiles)
            RaiseEvent onFileDropped(_DocFiles, oArgs)
            DisplayStatus(Status.Ready)
        Else
            MsgBox("error al importar fitxers" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
            DisplayStatus(Status.Ready)
        End If
    End Sub

    Private Sub PictureBox1_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DragLeave
        DisplayStatus(Status.Ready)
    End Sub

    Public Sub DisplayStatus(ByVal oStatus As Status)
        Select Case oStatus
            Case Status.Ready
                PictureBox1.Image = My.Resources.AddFile
                Cursor = Cursors.Default
            Case Status.Wait
                PictureBox1.Image = My.Resources.AddFileGroc
                Cursor = Cursors.WaitCursor
        End Select
        Application.DoEvents()
    End Sub
End Class
