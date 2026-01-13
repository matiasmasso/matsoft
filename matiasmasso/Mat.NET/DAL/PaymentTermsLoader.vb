Imports System.Xml

Public Class PaymentTermsLoader
    Shared Function Factory(sXMLString As String) As DTOPaymentTerms
        Dim retval As DTOPaymentTerms = Nothing
        If sXMLString > "" Then
            Dim oXMLDoc As New XmlDataDocument
            If sXMLString.StartsWith("<") Then oXMLDoc.LoadXml(sXMLString)

            retval = New DTOPaymentTerms
            With retval
                .Cod = oXMLDoc.DocumentElement.Attributes("MODO").Value

                If oXMLDoc.DocumentElement.Attributes("IBAN") IsNot Nothing Then
                    .Iban = New DTOIban
                    .Iban.Digits = oXMLDoc.DocumentElement.Attributes("IBAN").Value
                End If

                If oXMLDoc.DocumentElement.Attributes("NBANC") IsNot Nothing Then
                    .NBanc = BancLoader.Find(New Guid(oXMLDoc.DocumentElement.Attributes("NBANC").Value))
                End If

                If oXMLDoc.DocumentElement.SelectNodes("PLAZO/ITM").Count > 0 Then
                    .Plazos = New List(Of DTOPaymentTerms.Plazo)
                    For Each oNode As XmlElement In oXMLDoc.DocumentElement.SelectNodes("PLAZO/ITM")
                        Dim oPlazo As New DTOPaymentTerms.Plazo
                        If [Enum].TryParse(oNode.InnerText, oPlazo.Period) Then
                            .Plazos.Add(oPlazo)
                        End If
                    Next
                End If

            End With
        End If
        Return retval
    End Function

End Class
