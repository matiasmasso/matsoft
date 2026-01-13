Public Class Risc
    Shared Function CreditLimit(oCcx As DTOCustomer) As DTOAmt
        Dim retval = DTOAmt.Empty
        Dim oCurrentLog As DTOCliCreditLog = BEBL.CliCreditLog.CurrentLog(oCcx)
        If oCurrentLog IsNot Nothing Then
            retval = oCurrentLog.Amt
        End If
        Return retval
    End Function

    Shared Function CreditDisponible(oContact As DTOContact) As DTOAmt
        Dim oAmt = DTOAmt.Empty
        oAmt.Add(BEBL.Risc.CreditLimit(oContact))
        oAmt.Add(BEBL.Risc.EntregatACompte(oContact))
        oAmt.Add(BEBL.Risc.DipositIrrevocable(oContact))
        oAmt.Substract(BEBL.Risc.CreditDisposat(oContact))
        Return oAmt
    End Function

    Shared Function CreditDisposat(oContact As DTOContact) As DTOAmt
        Dim oAmt = DTOAmt.Empty
        oAmt.Add(CliCreditLoader.AlbsPerFacturar(oContact, True))
        oAmt.Add(CliCreditLoader.AlbsPerFacturar(oContact, False))
        oAmt.Add(BEBL.Risc.FrasPendentsDeVencer(oContact))
        Return oAmt
    End Function

    Shared Function DipositIrrevocable(oContact As DTOContact) As DTOAmt
        Dim DcEur As Decimal = BEBL.PgcSaldo.FromCtaCod(DTOPgcPlan.Ctas.DipositIrrevocableDeCompra, oContact, DTO.GlobalVariables.Today())
        Dim retval = DTOAmt.Factory(DcEur)
        Return retval
    End Function


    Shared Function EntregatACompte(oContact As DTOContact) As DTOAmt
        Dim DcEur As Decimal = BEBL.PgcSaldo.FromCtaCod(DTOPgcPlan.Ctas.Clients_Anticips, oContact, DTO.GlobalVariables.Today())
        Dim retval As DTOAmt = DTOAmt.Factory(-DcEur)
        Return retval
    End Function

    Shared Function FrasPendentsDeVencer(oContact As DTOContact) As DTOAmt
        Dim oPnds As List(Of DTOPnd) = PndsLoader.All(oContact.emp, oContact, ,, DTOPnd.Codis.Deutor, True)
        Dim retval = DTOAmt.Empty
        For Each oPnd In oPnds
            retval.Add(oPnd.Amt)
        Next
        Return retval
    End Function

    Shared Function SdoAlbsNoCredit(oContact As DTOContact) As DTOAmt
        Dim retval As DTOAmt = CliCreditLoader.AlbsPerFacturar(oContact, False)
        Return retval
    End Function

    Shared Function SdoAlbsACredit(oContact As DTOContact) As DTOAmt
        Dim retval As DTOAmt = CliCreditLoader.AlbsPerFacturar(oContact, True)
        Return retval
    End Function
End Class
