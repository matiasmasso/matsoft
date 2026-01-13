Public Class Aecoc
    Shared Function NextEanToContact(oEmp As DTOEmp, ByVal oContact As DTOContact, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oEan As DTOEan = AECOCLoader.GetNextEan(oEmp)

        If AECOCLoader.Update(oEan, oContact, exs) Then
            oContact.GLN = oEan
            retval = True
        End If

        Return retval
    End Function

    Shared Function NextEanToSku(oEmp As DTOEmp, ByRef oSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oEan As DTOEan = AECOCLoader.GetNextEan(oEmp)

        If AECOCLoader.Update(oEan, oSku, exs) Then
            oSku.Ean13 = oEan
            retval = True
        End If

        Return retval
    End Function
End Class
