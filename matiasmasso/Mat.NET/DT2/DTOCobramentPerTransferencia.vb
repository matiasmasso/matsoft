Public Class DTOCobramentPerTransferencia

    Property User As DTOUser
    Property Delivery As DTODelivery
    Property Fch As Date
    Property Contact As DTOContact
    Property Banc As DTOBanc
    Property Concepte As String
    Property Amt As DTOAmt
    Property DocFile As DTODocFile

    Shared Function Factory(user As DTOUser,
                            delivery As DTODelivery,
                            fch As Date,
                            contact As DTOContact,
                            banc As DTOBanc,
                            concepte As String,
                            amt As DTOAmt,
                            docFile As DTODocFile) As DTOCobramentPerTransferencia

        Dim retval As New DTOCobramentPerTransferencia
        With retval
            .User = user
            .Delivery = delivery
            .Fch = fch
            .Contact = contact
            .Banc = banc
            .Concepte = concepte
            .Amt = amt
            .DocFile = docFile
        End With
        Return retval
    End Function

End Class
