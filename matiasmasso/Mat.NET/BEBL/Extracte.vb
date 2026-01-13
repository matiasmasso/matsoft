Public Class Extracte
    Shared Function Years(oEmp As DTOEmp, Optional oCta As DTOPgcCta = Nothing, Optional oContact As DTOContact = Nothing) As List(Of Integer)
        Dim retval As List(Of Integer) = ExtracteLoader.Years(oEmp, oContact, oCta)
        Return retval
    End Function


    Shared Function Ctas(oEmp As DTOEmp, Year As Integer, Optional oContact As DTOContact = Nothing) As List(Of DTOPgcCta)
        Dim retval As List(Of DTOPgcCta) = ExtracteLoader.Ctas(oEmp, Year, oContact)
        Return retval
    End Function


    Shared Function Ccbs(oExtracte As DTOExtracte) As List(Of DTOCcb)
        Dim retval As New List(Of DTOCcb)
        If oExtracte.Cta IsNot Nothing Then
            retval = ExtracteLoader.Ccbs(oExtracte)
        End If
        Return retval
    End Function

End Class
