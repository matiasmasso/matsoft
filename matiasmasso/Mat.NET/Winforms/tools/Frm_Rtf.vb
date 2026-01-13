Imports Microsoft.Office.Interop.Word

Public Class Frm_Rtf


    Private Sub Refresca()
        Dim exs as New List(Of exception)
        Dim oRtf As New RtfDocument()
        Dim sFilename As String = TextBoxFilename.Text
        If oRtf.Open(sFilename, exs) Then
            AddTabShapes(oRtf, 1)
            For iPage As Integer = 0 To oRtf.Pages.Count - 1

                Dim oTabPage As New TabPage("pag." & iPage.ToString)
                TabControl1.TabPages.Add(oTabPage)

                Dim oTextShapes As List(Of String) = oRtf.Pages(iPage).TextShapes
                Dim oAnalyzer As New Xl_FlatFileFixedLengthAnalyzer(oTextShapes)
                oAnalyzer.Dock = DockStyle.Fill
                oTabPage.Controls.Add(oAnalyzer)

            Next
            oRtf.Close()
        Else
            MsgBox("error al importar el fitxer" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Public Sub AddTabShapes(oRtf As RtfDocument, iPage As Integer)
        Dim oTabPage As New TabPage("Shapes")
        TabControl1.TabPages.Add(oTabPage)
        Dim oShapes As List(Of Shape) = oRtf.Pages(iPage).Shapes
        Dim oXl_RtfShapes As New Xl_RtfShapes
        oXl_RtfShapes.Load(oShapes)
        oXl_RtfShapes.Dock = DockStyle.Fill
        oTabPage.Controls.Add(oXl_RtfShapes)
    End Sub

    Private Sub ButtonBrowse_Click(sender As Object, e As EventArgs) Handles ButtonBrowse.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "*rtf docs |*.rtf| all docs|*.*"
            If .ShowDialog Then
                TextBoxFilename.Text = .FileName
                Refresca()
            End If
        End With
    End Sub
End Class