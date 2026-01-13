Public Class EdiversaInvRpt

    Shared Function InvRptEdiFile(oEmp As DTOEmp, oCustomer As DTOCustomer) As DTOEdiversaFile
        BEBL.Contact.Load(oCustomer)
        Dim retval As New DTOEdiversaFile()
        With retval
            .Fch = DTO.GlobalVariables.Today()
            .Stream = EdiSrc(oEmp, oCustomer)
            .IOCod = DTOEdiversaFile.IOcods.Outbox
            .Tag = DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008.ToString
            .Sender = New DTOEdiversaContact()
            .Sender.Ean = oEmp.Org.GLN
            .Receiver = New DTOEdiversaContact
            .Receiver.Ean = oCustomer.GLN
        End With
        Return retval
    End Function

    Shared Function EdiSrc(oEmp As DTOEmp, oCustomer As DTOCustomer) As String
        Dim oOrg As DTOContact = oEmp.Org
        BEBL.Contact.Load(oCustomer)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("INVRPT_D_96A_UN_EAN008")
        sb.AppendLine(String.Format("BGM|{0:yyyyMMddHHmmss}|35|9", DTO.GlobalVariables.Now()))
        sb.AppendLine(String.Format("DTM|{0:yyyyMMdd}||{0:yyyyMMdd}", DTO.GlobalVariables.Today()))
        sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}", oOrg.GLN.Value, oOrg.Nom, oOrg.Address.Text, oOrg.Address.Zip.Location.Nom, oOrg.Address.Zip.ZipCod, oOrg.Address.Zip.Location.Zona.Country.ISO))
        sb.AppendLine(String.Format("NADBY|{0}|{1}", oCustomer.GLN.Value, oCustomer.Nom))
        sb.AppendLine(String.Format("NADWH|{0}", "MCN34"))
        sb.AppendLine(String.Format("CUX|EUR"))

        Dim lineNumber As Integer
        Dim oSkus As List(Of DTOProductSku) = BEBL.Mgz.InvRpt(oCustomer, oEmp.Mgz)
        For Each oSku As DTOProductSku In oSkus

            lineNumber += 1
            sb.AppendLine(String.Format("LIN|{0}|EN|{1}", oSku.Ean13.Value, lineNumber))
            sb.AppendLine(String.Format("QTYLIN|145|{0}", oSku.Stock))
            sb.AppendLine(String.Format("PRIQTY|AAA|{0}", DTOEdiversaFile.EdiFormat(oSku.Price.Eur)))
        Next
        Dim retval As String = sb.ToString
        Return retval

    End Function


End Class
