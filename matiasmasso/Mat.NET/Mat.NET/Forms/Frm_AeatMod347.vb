Public Class Frm_AeatMod347
    Private _Mod347 As DTO.DTOAeatMod347
    Private _AllowEvents As Boolean

    Private Sub Frm_AeatMod347_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oExercici As DTOExercici = BLL.BLLApp.LastExercici
        Dim exs As New List(Of Exception)
        _Mod347 = BLL.BLLAeatMod347.Load(oExercici, exs)
        Xl_AeatMod347_items1.Load(_Mod347)
        _AllowEvents = True
    End Sub


    Private Sub DesarFitxerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesarFitxerToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oFlatFile As DTOFlatFile = BLL.BLLAeatMod347.FlatFile(_Mod347, exs)
        If exs.Count > 0 Then
            UIHelper.WarnError(exs, "error al redactar el fitxer")
        Else
            If UIHelper.SaveFlatFileDialog(oFlatFile, "Model 347") Then
                Me.Close()
            End If
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

    Private Sub ExportarACsvToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarACsvToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oCsv As DTOCsv = BLL.BLLAeatMod347.Csv(_Mod347, exs)
        Dim src As String = BLL.BLLCsv.ToString(oCsv)
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

    End Sub
End Class