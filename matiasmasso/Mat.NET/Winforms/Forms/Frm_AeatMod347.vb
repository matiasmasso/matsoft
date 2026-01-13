Public Class Frm_AeatMod347
    Private _Mod347 As DTOAeatMod347
    Private _AllowEvents As Boolean

    Private Async Sub Frm_AeatMod347_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oExercici = DTOExercici.Past(Current.Session.Emp)
        Dim exs As New List(Of Exception)
        _Mod347 = Await FEB2.AeatMod347.Factory(exs, oExercici)
        If exs.Count = 0 Then
            Dim dcMinimDeclarable = Await FEB2.AeatMod347.MinimDeclarable(exs)
            If exs.Count = 0 Then
                Xl_AeatMod347_items1.Load(_Mod347, dcMinimDeclarable)
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        If _AllowEvents Then
            Dim sSearchKey As String = TextBoxSearch.Text
            If sSearchKey.Length > 3 Then
                TextBoxSearch.ForeColor = Color.Black
                Xl_AeatMod347_items1.Filter = sSearchKey
            Else
                Xl_AeatMod347_items1.ClearFilter()
                TextBoxSearch.ForeColor = Color.Gray
            End If
        End If
    End Sub

    Private Async Sub ExportarACsvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarACsvToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oCsv = Await FEB2.AeatMod347.Csv(exs, _Mod347)
        If exs.Count = 0 Then
            Dim src As String = oCsv.ToString()
            If exs.Count = 0 Then
                Dim oDlg As New SaveFileDialog
                With oDlg
                    .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                    .AddExtension = True
                    .DefaultExt = "csv"
                    '.FileName = "Model 347 " & _Mod347.Exercici.Year
                    .Filter = "fitxers csv|*.csv|(tots els fitxers)|*.*"
                    .Title = "Exportar Model 347 a Csv"
                    If .ShowDialog Then
                        Dim sFilename As String = .FileName
                        My.Computer.FileSystem.WriteAllText(sFilename, src, False)
                    End If
                End With
            Else
                UIHelper.WarnError(exs, "error al redactar el csv del Model 347")
            End If
        Else
            UIHelper.WarnError(exs, "error al redactar el csv del Model 347")
        End If

    End Sub
End Class