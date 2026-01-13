Public Class SepaCoreHelper
    Shared Function SepaCoreXML(exs As List(Of Exception), oEmp As DTOEmp, oCsa As DTOCsa) As String

        'Raiz
        Dim oXmlDocument As New DTOXmlDocument(True)
        Dim oRootSegment As DTOXmlSegment = oXmlDocument.AddSegment("Document")
        oRootSegment.AddAttributes("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02", "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Dim oMainSegment As DTOXmlSegment = oRootSegment.AddSegment("CstmrDrctDbtInitn")

        'cabecera
        Dim oGrpHdr As DTOXmlSegment = oMainSegment.AddSegment("GrpHdr")
        Dim oMsgId As DTOXmlSegment = oGrpHdr.AddSegment("MsgId", oCsa.sepaMsgId())
        Dim oCreDtTm As DTOXmlSegment = oGrpHdr.AddSegment("CreDtTm", DTO.GlobalVariables.Now().ToString("yyyy-MM-ddTHH:mm:ss"))
        Dim oNbOfTxs As DTOXmlSegment = oGrpHdr.AddSegment("NbOfTxs", oCsa.items.Count)
        Dim oCtrlSum As DTOXmlSegment = oGrpHdr.AddSegment("CtrlSum", oCsa.sepaFormatSuma())

        Dim oParteIniciadora As DTOXmlSegment = oGrpHdr.AddSegment("InitgPty")
        Dim oOrgNm As DTOXmlSegment = oParteIniciadora.AddSegment("Nm", oEmp.Org.nom)
        Dim oOrgId As DTOXmlSegment = oParteIniciadora.AddSegment("Id")
        Dim oOrgId2 As DTOXmlSegment = oOrgId.AddSegment("OrgId")
        Dim oOrgIdOthr As DTOXmlSegment = oOrgId2.AddSegment("Othr")
        Dim oOrgIdOthrId As DTOXmlSegment = oOrgIdOthr.AddSegment("Id", oCsa.sepaIdentificacioPresentador())

        'Dim oOrgIdOthrSchmeNm As DTOXmlSegment = oOrgIdOthr.AddSegment("SchmeNm")
        'Dim oOrgIdOthrSchmeNmCd As DTOXmlSegment = oOrgIdOthrSchmeNm.AddSegment("Cd", "SEPA")

        Dim PmtInfId As Integer
        Dim Query = oCsa.items.GroupBy(Function(g) New With {Key g.vto}).Select(Function(group) New With {.Vto = group.Key.vto,
                                                              .Sum = group.Sum(Function(x) x.amt.Eur)})
        For Each oPmtInf In Query
            PmtInfId += 1
            Dim oPmtInfCsbs As List(Of DTOCsb) = oCsa.items.FindAll(Function(x) x.vto = oPmtInf.Vto)

            'información del pago
            Dim oPaymentInfo As DTOXmlSegment = oMainSegment.AddSegment("PmtInf") 'Payment info
            Dim oPaymentInfoId As DTOXmlSegment = oPaymentInfo.AddSegment("PmtInfId", "PmtInfId-" & String.Format(PmtInfId, "000"))
            Dim oPaymentMethod As DTOXmlSegment = oPaymentInfo.AddSegment("PmtMtd", "DD") 'Direct Debit
            Dim oNbOfTxs2 As DTOXmlSegment = oPaymentInfo.AddSegment("NbOfTxs", oPmtInfCsbs.Count)
            Dim oCtrlSum2 As DTOXmlSegment = oPaymentInfo.AddSegment("CtrlSum", DTOCsa.sepaFormat(oPmtInf.Sum))
            Dim oPaymentTypeInfo As DTOXmlSegment = oPaymentInfo.AddSegment("PmtTpInf")
            Dim oServiceLevel As DTOXmlSegment = oPaymentTypeInfo.AddSegment("SvcLvl")
            Dim oServiceLevelCode As DTOXmlSegment = oServiceLevel.AddSegment("Cd", "SEPA")
            Dim oLocalInstrument As DTOXmlSegment = oPaymentTypeInfo.AddSegment("LclInstrm")
            Dim oLocalInstrumentCode As DTOXmlSegment = oLocalInstrument.AddSegment("Cd", "CORE")
            Dim oSequenceType As DTOXmlSegment = oPaymentTypeInfo.AddSegment("SeqTp", "RCUR")
            Dim oCategoryPurpose As DTOXmlSegment = oPaymentTypeInfo.AddSegment("CtgyPurp")
            Dim oCategoryPurposeCode As DTOXmlSegment = oCategoryPurpose.AddSegment("Cd", "TRAD")

            Dim oRequestedCollectionDate As DTOXmlSegment = oPaymentInfo.AddSegment("ReqdColltnDt", DTOCsa.sepaFormat(oPmtInf.Vto))

            Dim oCreditor As DTOXmlSegment = oPaymentInfo.AddSegment("Cdtr")
            Dim oCreditorNm As DTOXmlSegment = oCreditor.AddSegment("Nm", oEmp.Org.nom)
            Dim oCreditorPostalAddress As DTOXmlSegment = oCreditor.AddSegment("PstlAdr")
            Dim oCreditorPostalAddressCountry As DTOXmlSegment = oCreditorPostalAddress.AddSegment("Ctry", DTOAddress.Country(oEmp.Org.address).ISO)
            Dim oCreditorAdrLine1 As DTOXmlSegment = oCreditorPostalAddress.AddSegment("AdrLine", DTOCsa.sepaNormalized(oEmp.Org.Address.Text))
            Dim oCreditorAdrLine2 As DTOXmlSegment = oCreditorPostalAddress.AddSegment("AdrLine", DTOCsa.sepaNormalized(DTOAddress.ZipyCit(oEmp.Org.address)))


            Dim oCreditorAccount As DTOXmlSegment = oPaymentInfo.AddSegment("CdtrAcct")
            Dim oCreditorAccountId As DTOXmlSegment = oCreditorAccount.AddSegment("Id")
            Dim oCreditorAccountIban As DTOXmlSegment = oCreditorAccountId.AddSegment("IBAN", oCsa.banc.iban.Digits)
            Dim oCreditorCurrency As DTOXmlSegment = oCreditorAccount.AddSegment("Ccy", "EUR")

            Dim oCreditorAgent As DTOXmlSegment = oPaymentInfo.AddSegment("CdtrAgt")
            Dim oFinancialInstitutionIdentification As DTOXmlSegment = oCreditorAgent.AddSegment("FinInstnId")
            Dim oFinancialInstitutionBIC As DTOXmlSegment = oFinancialInstitutionIdentification.AddSegment("BIC", oCsa.banc.iban.BankBranch.Bank.Swift)

            'afegit per DB: --------------
            'opcional a la resta de bancs; si s'inclou el valor als Sepa Core ha de ser sempre SLEV (Service Level)
            Dim oChrgBr As DTOXmlSegment = oPaymentInfo.AddSegment("ChrgBr", "SLEV")
            '-----------------------------

            Dim oCreditorId As DTOXmlSegment = oPaymentInfo.AddSegment("CdtrSchmeId")
            Dim oCreditorIdId As DTOXmlSegment = oCreditorId.AddSegment("Id")
            Dim oCreditorPrvtId As DTOXmlSegment = oCreditorIdId.AddSegment("PrvtId")
            Dim oCreditorPrvtIdOthr As DTOXmlSegment = oCreditorPrvtId.AddSegment("Othr")
            Dim oCreditorPrvtIdOthrId As DTOXmlSegment = oCreditorPrvtIdOthr.AddSegment("Id", oCsa.sepaIdentificacioPresentador())
            Dim oCreditorPrvtIdSchmeNm As DTOXmlSegment = oCreditorPrvtIdOthr.AddSegment("SchmeNm")
            Dim oCreditorPrvtIdSchmeNmPrtry As DTOXmlSegment = oCreditorPrvtIdSchmeNm.AddSegment("Prtry", "SEPA")

            For Each item As DTOCsb In oPmtInfCsbs
                Dim oDebitInfo As DTOXmlSegment = oPaymentInfo.AddSegment("DrctDbtTxInf")
                Dim oPaymentId As DTOXmlSegment = oDebitInfo.AddSegment("PmtId")
                Dim oInstructionIdentification As DTOXmlSegment = oPaymentId.AddSegment("InstrId", item.GuionLessGuid)
                Dim oEndToEndId As DTOXmlSegment = oPaymentId.AddSegment("EndToEndId", item.FormattedId())
                Dim oInstructedAmount As DTOXmlSegment = oDebitInfo.AddSegment("InstdAmt", DTOCsa.sepaFormat(item.amt))
                oInstructedAmount.AddAttribute("Ccy", "EUR")

                Dim oDirectDebitTransaction As DTOXmlSegment = oDebitInfo.AddSegment("DrctDbtTx")
                Dim oMandateRelatedInformation As DTOXmlSegment = oDirectDebitTransaction.AddSegment("MndtRltdInf")
                Dim oMandateId As DTOXmlSegment = oMandateRelatedInformation.AddSegment("MndtId", item.iban.GuionLessGuid)
                Dim oDtOfSgntr As DTOXmlSegment = oMandateRelatedInformation.AddSegment("DtOfSgntr", DTOCsa.sepaFormat(item.iban.fchFrom))

                'afegit per Cx: --------------
                Dim oCreditorId2 As DTOXmlSegment = oDirectDebitTransaction.AddSegment("CdtrSchmeId")
                Dim oCreditorIdId2 As DTOXmlSegment = oCreditorId2.AddSegment("Id")
                Dim oCreditorPrvtId2 As DTOXmlSegment = oCreditorIdId2.AddSegment("PrvtId")
                Dim oCreditorPrvtIdOthr2 As DTOXmlSegment = oCreditorPrvtId2.AddSegment("Othr")
                Dim oCreditorPrvtIdOthrId2 As DTOXmlSegment = oCreditorPrvtIdOthr2.AddSegment("Id", oCsa.sepaIdentificacioPresentador())
                Dim oCreditorPrvtIdSchmeNm2 As DTOXmlSegment = oCreditorPrvtIdOthr2.AddSegment("SchmeNm")
                Dim oCreditorPrvtIdSchmeNmPrtry2 As DTOXmlSegment = oCreditorPrvtIdSchmeNm2.AddSegment("Prtry", "SEPA")
                '-----------------------------

                Dim oDebtorAgent As DTOXmlSegment = oDebitInfo.AddSegment("DbtrAgt")
                Dim oDebtorAgentFinInstnId As DTOXmlSegment = oDebtorAgent.AddSegment("FinInstnId")
                Dim oDebtorAgentFinInstnIdBIC As DTOXmlSegment = oDebtorAgentFinInstnId.AddSegment("BIC", item.iban.BankBranch.Bank.Swift)

                Dim oDebtor As DTOXmlSegment = oDebitInfo.AddSegment("Dbtr")
                Dim oDebtorName As DTOXmlSegment = oDebtor.AddSegment("Nm", DTOCsa.sepaNormalized(item.contact.nom))

                Dim oDebtorAccount As DTOXmlSegment = oDebitInfo.AddSegment("DbtrAcct")
                Dim oDebtorAccountId As DTOXmlSegment = oDebtorAccount.AddSegment("Id")
                Dim oDebtorAccountIdIban As DTOXmlSegment = oDebtorAccountId.AddSegment("IBAN", item.iban.Digits)

                Dim oRemittanceInformation As DTOXmlSegment = oDebitInfo.AddSegment("RmtInf")

                If item.txt = "" Then
                    Dim sMsg As String = String.Format("falta concepte a l'efecte de {0} de {1}", DTOAmt.CurFormatted(item.amt), item.contact.nom)
                    exs.Add(New Exception(sMsg))
                Else
                    Dim oRemittanceInformationUnstructured As DTOXmlSegment = oRemittanceInformation.AddSegment("Ustrd", DTOCsa.sepaNormalized(item.txt))
                End If

            Next
        Next

        Dim retval As String = DTOXmlDocument.ToString(oXmlDocument)
        Return retval
    End Function

End Class
