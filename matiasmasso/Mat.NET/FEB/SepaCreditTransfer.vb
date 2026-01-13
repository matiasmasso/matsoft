Public Class SepaCreditTransfer

    Shared Async Function XML(oEmp As DTOEmp, oBancTransferPool As DTOBancTransferPool, exs As List(Of Exception)) As Task(Of String)
        BancTransferPool.Load(oBancTransferPool, exs)
        Dim oIbanEmisor As DTOIban = Await Iban.FromContact(exs, oBancTransferPool.BancEmissor, DTOIban.Cods.Banc)

        'Raiz
        Dim oXmlDocument As New DTOXmlDocument(True)
        Dim oRootSegment As DTOXmlSegment = oXmlDocument.AddSegment("Document")
        oRootSegment.AddAttributes("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.001.001.03", "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Dim oMainSegment As DTOXmlSegment = oRootSegment.AddSegment("CstmrCdtTrfInitn")

        'cabecera
        Dim oGrpHdr As DTOXmlSegment = oMainSegment.AddSegment("GrpHdr")
        Dim oMsgId As DTOXmlSegment = oGrpHdr.AddSegment("MsgId", oBancTransferPool.MsgId())
        Dim oCreDtTm As DTOXmlSegment = oGrpHdr.AddSegment("CreDtTm", DTO.GlobalVariables.Now().ToString("yyyy-MM-ddTHH:mm:ss"))
        Dim oNbOfTxs As DTOXmlSegment = oGrpHdr.AddSegment("NbOfTxs", oBancTransferPool.Beneficiaris.Count)
        Dim oCtrlSum As DTOXmlSegment = oGrpHdr.AddSegment("CtrlSum", DTOCsa.SepaFormat(oBancTransferPool.Total())) ' FormatSuma(oBancTransferPool.Items))

        Dim oParteIniciadora As DTOXmlSegment = oGrpHdr.AddSegment("InitgPty")
        Dim oOrgNm As DTOXmlSegment = oParteIniciadora.AddSegment("Nm", DTOCsa.SepaNormalized(oEmp.Org.Nom))
        Dim oOrgId As DTOXmlSegment = oParteIniciadora.AddSegment("Id")
        Dim oOrgId2 As DTOXmlSegment = oOrgId.AddSegment("OrgId")
        Dim oOrgIdOthr As DTOXmlSegment = oOrgId2.AddSegment("Othr")
        'La Caixa nomes admet el NIF pelat aqui:
        'Dim oOrgIdOthrId As DTOXmlSegment = oOrgIdOthr.AddSegment("Id", IdentificacioPresentador(oBancTransferPool))
        Dim oOrgIdOthrId As DTOXmlSegment = oOrgIdOthr.AddSegment("Id", oEmp.Org.PrimaryNifValue())

        'información del pago
        Dim oPaymentInfo As DTOXmlSegment = oMainSegment.AddSegment("PmtInf") 'Payment info
        Dim oPaymentInfoId As DTOXmlSegment = oPaymentInfo.AddSegment("PmtInfId", oBancTransferPool.Cca.formattedId())
        Dim oPaymentMethod As DTOXmlSegment = oPaymentInfo.AddSegment("PmtMtd", "TRF") 'Direct Debit

        Dim oPaymentTypeInfo As DTOXmlSegment = oPaymentInfo.AddSegment("PmtTpInf")
        Dim oServiceLevel As DTOXmlSegment = oPaymentTypeInfo.AddSegment("SvcLvl")
        Dim oServiceLevelCode As DTOXmlSegment = oServiceLevel.AddSegment("Cd", "SEPA")


        Dim oFchExec As DTOXmlSegment = oPaymentInfo.AddSegment("ReqdExctnDt", oBancTransferPool.Fch.ToString("yyyy-MM-dd"))
        Dim oDbtr As DTOXmlSegment = oPaymentInfo.AddSegment("Dbtr")
        Dim oMyNom As DTOXmlSegment = oDbtr.AddSegment("Nm", DTOCsa.SepaNormalized(oEmp.Org.Nom))
        Dim oDbtrAcct As DTOXmlSegment = oPaymentInfo.AddSegment("DbtrAcct")
        Dim oDbtrAcctId As DTOXmlSegment = oDbtrAcct.AddSegment("Id")
        Dim oDbtrIBAN As DTOXmlSegment = oDbtrAcctId.AddSegment("IBAN", oIbanEmisor.Digits)
        Dim oDbtrAgt As DTOXmlSegment = oPaymentInfo.AddSegment("DbtrAgt")
        Dim oFinInstnId As DTOXmlSegment = oDbtrAgt.AddSegment("FinInstnId")

        Dim sSwift = oIbanEmisor.bankBranch.Bank.Swift
        If String.IsNullOrEmpty(sSwift) Then
            exs.Add(New Exception("Falta el swift del banc emisor"))
        ElseIf sSwift.Length < 11 Then
            exs.Add(New Exception("El Swift '" & sSwift & "'del banc emisor no te 11 digits"))
        End If

        Dim oDbtrAgtBIC As DTOXmlSegment = oFinInstnId.AddSegment("BIC", oIbanEmisor.BankBranch.Bank.Swift)
        Dim oChrgBr As DTOXmlSegment = oPaymentInfo.AddSegment("ChrgBr", "SHAR")


        For Each item As DTOBancTransferBeneficiari In oBancTransferPool.Beneficiaris
            Dim oCdtTrfTxInf As DTOXmlSegment = oPaymentInfo.AddSegment("CdtTrfTxInf")
            Dim oPmtId As DTOXmlSegment = oCdtTrfTxInf.AddSegment("PmtId")
            Dim oEndToEndId As DTOXmlSegment = oPmtId.AddSegment("EndToEndId", GuidHelper.ToGuionLess(item.Guid))

            Dim oAmt As DTOXmlSegment = oCdtTrfTxInf.AddSegment("Amt")
            Dim oInstdAmt As DTOXmlSegment = oAmt.AddSegment("InstdAmt", DTOCsa.SepaFormat(item.Amt))
            Dim oCurrency As DTOXmlAttribute = oInstdAmt.AddAttribute("Ccy", item.Amt.Cur.Tag.ToUpper)

            Dim oUltmtDbtr As DTOXmlSegment = oCdtTrfTxInf.AddSegment("UltmtDbtr")
            Dim oUltmtDbtrNm As DTOXmlSegment = oUltmtDbtr.AddSegment("Nm", oEmp.Org.Nom)

            Dim oCdtrAgt As DTOXmlSegment = oCdtTrfTxInf.AddSegment("CdtrAgt")
            Dim oCdtrAgtFinInstnId As DTOXmlSegment = oCdtrAgt.AddSegment("FinInstnId")

            sSwift = item.BankBranch.Bank.Swift
            If String.IsNullOrEmpty(sSwift) Then
                exs.Add(New Exception(String.Format("Falta el swift del banc de {0}", item.Contact.nom)))
            ElseIf sSwift.Length < 11 Then
                exs.Add(New Exception(String.Format("El Swift '{0}' del banc de {1} no te 11 digits", sSwift, item.Contact.nom)))
            End If
            Dim oCdtrBIC As DTOXmlSegment = oCdtrAgtFinInstnId.AddSegment("BIC", sSwift)

            Dim oCdtr As DTOXmlSegment = oCdtTrfTxInf.AddSegment("Cdtr")
            If item.IsOurBankAccount Then
                oCdtr.AddSegment("Nm", DTOCsa.SepaNormalized(oEmp.Org.Nom))
            Else
                oCdtr.AddSegment("Nm", DTOCsa.SepaNormalized(item.Contact.Nom))
            End If

            Dim oCdtrAcct As DTOXmlSegment = oCdtTrfTxInf.AddSegment("CdtrAcct")
            Dim oCdtrAcctId As DTOXmlSegment = oCdtrAcct.AddSegment("Id")
            Dim oCdtrIBAN As DTOXmlSegment = oCdtrAcctId.AddSegment("IBAN", item.Account)

            If item.Concepte > "" Then
                Dim oRmtInf As DTOXmlSegment = oCdtTrfTxInf.AddSegment("RmtInf")
                Dim oUstrd As DTOXmlSegment = oRmtInf.AddSegment("Ustrd", TextHelper.VbLeft(item.Concepte, 140))
            End If
        Next

        Dim retval As String = DTOXmlDocument.ToString(oXmlDocument)
        Return retval

    End Function


End Class
