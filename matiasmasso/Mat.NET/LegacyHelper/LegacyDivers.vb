Public Class LegacyDivers
    Shared Function SepaMandato(exs As List(Of Exception), oEmp As DTOEmp, oIban As DTOIban, sSwift As String, oSepaTexts As List(Of DTOSepaText), oLang As DTOLang) As DTODocFile
        Dim oMandato As New PdfSepaMandato(oEmp, oIban, sSwift, oLang)
        Dim oByteArray() As Byte = oMandato.Pdf(oSepaTexts)
        Dim retval = DocfileHelper.Factory(exs, oByteArray)
        Return retval
    End Function

End Class
