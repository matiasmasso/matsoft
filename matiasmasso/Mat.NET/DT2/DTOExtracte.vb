Public Class DTOExtracte
    Property Exercici As DTOExercici
    Property Cta As DTOPgcCta
    Property Contact As DTOContact
    Property Ccbs As List(Of DTOCcb)
    Property Years As List(Of Integer)

    Shared Function Factory(oExercici As DTOExercici, oCta As DTOPgcCta, oContact As DTOContact)
        Dim retval As New DTOExtracte
        With retval
            .Exercici = oExercici
            .Cta = oCta
            .Contact = oContact
        End With
        Return retval
    End Function

    Shared Function Filename(oCcbs As List(Of DTOCcb), oLang As DTOLang) As String
        Dim retval As String = "M+O.extracte.xlsx"
        If oCcbs.Count > 0 Then
            Dim oCcb As DTOCcb = oCcbs.First
            Dim oCta As DTOPgcCta = oCcb.Cta
            Dim sCta As String = DTOPgcCta.FullNom(oCta, oLang)
            Dim oContact As DTOContact = oCcb.Contact
            If oContact Is Nothing Then
                retval = String.Format("M+O.extracte.{0}.xlsx", sCta)
            Else
                Dim sContact As String = TextHelper.CleanForUrl(oContact.FullNom)
                If sContact = "" Then
                    retval = String.Format("M+O.extracte.{0}.xlsx", sCta)
                Else
                    retval = String.Format("M+O.extracte.{0}.{1}.xlsx", sCta, sContact).Replace(TextHelper.VbChr(34), "")
                End If
            End If
        End If
        Return retval
    End Function

End Class
