Public Class EdiversaInvRpt

    Shared Function Send(oEmp As DTOEmp, oCustomer As DTOCustomer, exs As List(Of Exception)) As Boolean
        Dim oEdiversa As DTOEdiversaFile = EdiFile(oEmp, oCustomer)
        Dim retval As Boolean = BEBL.EdiversaFile.Update(oEdiversa, exs)
        Return retval
    End Function

    Shared Function EdiFile(oEmp As DTOEmp, oCustomer As DTOCustomer) As DTOEdiversaFile
        Dim retval As New DTOEdiversaFile()
        BEBL.Contact.Load(oCustomer)
        With retval
            .Fch = DTO.GlobalVariables.Today()
            .Stream = EdiSrc(oEmp, oCustomer)
            .IOCod = DTOEdiversaFile.IOcods.Outbox
            .Tag = DTOEdiversaFile.Tags.INVRPT_D_96A_UN_EAN008.ToString
            .Sender = New DTOEdiversaContact()
            .Sender.Ean = oEmp.org.GLN
            .Receiver = New DTOEdiversaContact
            .Receiver.Ean = oCustomer.GLN
        End With
        Return retval
    End Function

    Shared Function EdiSrc(oEmp As DTOEmp, oCustomer As DTOCustomer) As String
        Dim oOrg As DTOContact = oEmp.org
        BEBL.Contact.Load(oCustomer)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("INVRPT_D_96A_UN_EAN008")
        sb.AppendLine(String.Format("BGM|{0:yyyyMMddHHmmss}|35|9", DTO.GlobalVariables.Now()))
        sb.AppendLine(String.Format("DTM|{0:yyyyMMdd}||{0:yyyyMMdd}", DTO.GlobalVariables.Today()))
        sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}", oOrg.GLN.Value, oOrg.nom, oOrg.Address.Text, oOrg.address.Zip.Location.nom, oOrg.address.Zip.ZipCod, oOrg.address.Zip.Location.Zona.Country.ISO))
        sb.AppendLine(String.Format("NADBY|{0}|{1}", oCustomer.GLN.Value, oCustomer.Nom.RemoveDiacritics()))
        sb.AppendLine(String.Format("NADWH|{0}", "MCN34"))
        sb.AppendLine(String.Format("CUX|EUR"))

        Dim lineNumber As Integer
        Dim oSkus As List(Of DTOProductSku) = BEBL.Mgz.InvRpt(oCustomer, oEmp.Mgz)
        For Each oSku As DTOProductSku In oSkus

            lineNumber += 1
            sb.AppendLine(String.Format("LIN|{0}|EN|{1}", oSku.ean13.value, lineNumber))
            sb.AppendLine(String.Format("QTYLIN|145|{0}", oSku.stock))
            sb.AppendLine(String.Format("PRIQTY|AAA|{0}", DTOEdiversaFile.EdiFormat(oSku.price.Eur)))
        Next
        Dim retval As String = sb.ToString
        Return retval

    End Function

    Shared Function Excel(oEmp As DTOEmp, oCustomer As DTOCustomer) As MatHelper.Excel.Sheet

        Dim retval As New MatHelper.Excel.Sheet("InvRpt")
        With retval
            .AddColumn("linia", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("EAN", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("ref", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("marca", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("nom", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("stock", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("preu", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        Dim lineNumber As Integer
        Dim oSkus As List(Of DTOProductSku) = BEBL.Mgz.InvRpt(oCustomer, oEmp.Mgz)
        For Each oSku As DTOProductSku In oSkus
            lineNumber += 1

            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            oRow.AddCell(lineNumber)
            oRow.AddCell(oSku.ean13.value)
            oRow.AddCell(oSku.id)
            oRow.addCell(oSku.category.brand.nom.Esp)
            oRow.AddCell(oSku.RefYNomLlarg.Esp)
            oRow.AddCell(oSku.stock)
            oRow.AddCellAmt(oSku.price)

        Next
        Return retval

    End Function

End Class
