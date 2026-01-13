Imports DTO.Integracions

Public Class Frm_Vivace_DESADV
    Private _Desadv As Vivace.DESADV

    Private Async Sub Xl_Filename1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Filename1.AfterUpdate
        Await procesa(e.Argument)
    End Sub

    Private Async Function procesa(filename As String) As Task
        Dim exs As New List(Of Exception)
        _Desadv = Vivace.DESADV.Factory(exs, filename)
        Dim skuIds = _Desadv.Segments.Select(Function(x) x.FieldValue(Vivace.DESADV.FieldIds.Articulo)).Distinct().ToList()
        Dim oSkus = Await FEB.ProductSkus.SearchFromSkuIds(exs, GlobalVariables.Emp, skuIds)
        For Each oSegment In _Desadv.Segments
            Dim oField = oSegment.Fields(Vivace.DESADV.FieldIds.Articulo)
            Dim oSku = oSkus.FirstOrDefault(Function(x) x.id = oField.Value)
            If oSku IsNot Nothing AndAlso oSku.ean13 IsNot Nothing Then
                oField.Value = oSku.ean13.value
            End If
        Next
    End Function

    Private Async Sub Importar_Click(sender As Object, e As EventArgs) Handles Importar.Click
        Dim oDlg As New OpenFileDialog()
        With oDlg
            If .ShowDialog = DialogResult.OK Then
                Xl_Filename1.Text = .FileName
                Await procesa(.FileName)
            End If
        End With
    End Sub

    Private Sub Exportar_Click(sender As Object, e As EventArgs) Handles Exportar.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New FolderBrowserDialog
        With oDlg
            If .ShowDialog = DialogResult.OK Then
                Dim ediMessages As List(Of String) = _Desadv.EdiMessages()
                For Each src In ediMessages
                    Dim msgId As String = TextHelper.RegexValue(src, TextHelper.RegexSelectBetween("UNH\+", "\+DESADV"))
                    Dim filename = String.Format("{0}/DESADV {1}.txt", .SelectedPath, msgId)
                    MatHelperStd.FileSystemHelper.SaveTextToFile(src, filename, exs)
                Next

                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub

    Private Async Sub FtpToTradeInn_Click(sender As Object, e As EventArgs) Handles FtpToTradeInn.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.tradeInn)
        Dim ediMessages As List(Of String) = _Desadv.EdiMessages()
        If ediMessages.Count = 0 Then
            ProgressBar1.Visible = False
            UIHelper.WarnError(New Exception("no hi han missatges DESADV per enviar"))
        Else
            Dim ediMessage = ediMessages.First()
            Dim oByteArray = System.Text.Encoding.UTF8.GetBytes(ediMessage)
            Dim msgId As String = TextHelper.RegexValue(ediMessage, TextHelper.RegexSelectBetween("UNH\+", "\+DESADV"))
            Dim filename = String.Format("DESADV {0}.txt", msgId)
            Dim msg = Await FEB.Ftpserver.Send(exs, oCustomer, DTOFtpserver.Path.Cods.Desadv, oByteArray, filename)
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                MsgBox("Desadv enviat correctament per Ftp" & vbCrLf & msg, MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class