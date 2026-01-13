Public Class AmazonSellerOrder

    Shared Function ImportUrl(oUser As DTOUser, url As String, exs As List(Of Exception)) As DTOImportUrl
        Dim retval As DTOImportUrl = Nothing
        Dim oStream As System.IO.MemoryStream = Nothing
        Dim oCredencials = BEBL.Credencial.Find(DTOCredencial.Wellknown(DTOCredencial.Wellknowns.AmazonSeller).Guid)
        If MatHelperStd.FileSystemHelper.DownloadStream(exs, url, oStream, oCredencials.usuari, oCredencials.password) Then
            Dim oMimeCod = MatHelperStd.MimeHelper.GuessMime(oStream.ToArray)
            Dim tmp As String = ""

            FileSystemHelper.DownloadHtml(exs, url, tmp, oCredencials.usuari, oCredencials.password)
            Select Case oMimeCod
                Case MimeCods.Pdf
                    Dim src As String = LegacyHelper.iTextPdfHelper.readText(oStream.ToArray, exs)
                    If exs.Count = 0 Then
                        Dim lines = src.Split(vbLf).ToList
                        If DTO.AmazonSellerOrder.MatchSource(lines) Then
                            Dim oCountries = BEBL.Countries.All(DTOLang.ESP)
                            Dim oDocfile As DTODocFile = Nothing
                            Dim oAmazonSeller As New DTO.AmazonSellerOrder(oDocfile, lines, oCountries)
                            retval = New DTOImportUrl
                            With retval
                                .Cod = DTOImportUrl.Cods.AmazonSellerOrder
                                .Content = oAmazonSeller
                            End With
                        End If
                    End If

                Case Else
                    exs.Add(New Exception("fitxer no reconegut"))
            End Select
        Else
            exs.Add(New Exception("error al descarregar la url"))
        End If
        Return retval
    End Function

End Class
