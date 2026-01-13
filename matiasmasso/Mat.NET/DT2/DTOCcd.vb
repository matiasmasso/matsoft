Public Class DTOCcd
    Property Exercici As DTOExercici
    Property Cta As DTOPgcCta
    Property Contact As DTOContact
    Property Fch As Date
    Property Ccbs As List(Of DTOCcb)

    Public Sub New()
        MyBase.New
    End Sub

    Shared Function Factory(oExercici As DTOExercici, oCta As DTOPgcCta, oContact As DTOContact, Optional FchTo As Date = Nothing) As DTOCcd
        Dim retval As New DTOCcd
        With retval
            .Exercici = oExercici
            .Cta = oCta
            .Contact = oContact
            .Fch = FchTo
        End With
        Return retval
    End Function

    Public Function Unequals(oCcb As DTOCcb) As Boolean
        Dim retval As Boolean
        If oCcb.Contact Is Nothing Then
            retval = UnEquals(oCcb.Cca.Exercici, oCcb.Cta.Guid, Nothing)
        Else
            retval = UnEquals(oCcb.Cca.Exercici, oCcb.Cta.Guid, oCcb.Contact.Guid)
        End If
        Return retval
    End Function

    Public Function UnEquals(oExercici As DTOExercici, oCtaGuid As Object, oContactGuid As Object) As Boolean
        Dim retval As Boolean = True
        If _Exercici IsNot Nothing Then
            If _Exercici.Equals(oExercici) Then
                If oCtaGuid Is Nothing Then
                    retval = _Cta IsNot Nothing
                ElseIf Not Convert.IsDBNull(oCtaGuid) Then
                    If TypeOf oCtaGuid Is System.Guid Then
                        If oCtaGuid.Equals(_Cta.Guid) Then
                            If _Contact Is Nothing Then
                                If Convert.IsDBNull(oContactGuid) Then
                                    retval = False
                                Else
                                    retval = oContactGuid <> Nothing
                                End If
                            Else
                                If Not Convert.IsDBNull(oContactGuid) Then
                                    If TypeOf oContactGuid Is Guid Then
                                        retval = Not oContactGuid.Equals(_Contact.Guid)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return retval
    End Function


    Shared Function FullNom(oCcd As DTOCcd, oLang As DTOLang) As String
        Dim retval As String = String.Format("{0}{1:00000} {2} {3}", oCcd.Cta.Id, oCcd.Contact.Id, oCcd.Cta.Nom.Tradueix(oLang), oCcd.Contact.Nom)
        Return retval
    End Function
End Class
