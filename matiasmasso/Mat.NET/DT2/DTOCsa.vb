Public Class DTOCsa
    Inherits DTOBaseGuid

    Property emp As DTOEmp
    Property id As Integer
    Property fch As Date
    Property banc As DTOBanc
    Property ref As String
    Property items As List(Of DTOCsb)

    Property enabled As Boolean
    Property term As DTOBancTerm
    Property nominal As DTOAmt
    Property nominalMinim As DTOAmt
    Property nominalMaxim As DTOAmt
    Property classificacio As DTOAmt
    Property disponible As DTOAmt
    Property condicions As String
    Property fileFormat As FileFormats
    Property despeses As DTOAmt
    Property efectes As Integer
    Property dias As Integer
    Property importMig As Decimal
    Property tae As Decimal
    Property minVto As Date
    Property maxVto As Date

    Property cca As DTOCca

    Property descomptat As Boolean
    Property andorra As Boolean


    Public Enum Types
        alCobro
        alDescompte
    End Enum

    Public Enum FileFormats
        notSet
        norma58
        norma19
        remesesExportacioLaCaixa
        normaAndorrana
        sepaB2b
        sepaCore
    End Enum


    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOCsb)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTOCsb)
    End Sub


    Shared Function Factory(oEmp As DTOEmp, Optional oBanc As DTOBanc = Nothing, Optional oFileFormat As DTOCsa.FileFormats = DTOCsa.FileFormats.NotSet, Optional Descomptat As Boolean = False) As DTOCsa
        Dim retval As New DTOCsa
        With retval
            .Emp = oEmp
            .Banc = oBanc
            .Fch = Today
            .FileFormat = oFileFormat
            .Descomptat = Descomptat
        End With
        Return retval
    End Function

    Public Function formattedId() As String
        Dim retval As String = String.Format("{0:0000}{1:000}", _Fch.Year, _Id)
        Return retval
    End Function

    Public Function ReadableFormat() As String
        Dim retval As String = String.Format("{0}.{1}", _Fch.Year, _Id)
        Return retval
    End Function

    Shared Function Country(oCsa As DTOCsa) As DTOCountry
        Dim retval As DTOCountry = Nothing
        If oCsa IsNot Nothing AndAlso oCsa.Items.Count > 0 Then
            Dim oFirstCsb As DTOCsb = oCsa.Items.First
            retval = DTOCsb.Country(oFirstCsb)
        End If
        Return retval
    End Function


    Public Function filename() As String
        Dim retval As String = ""
        Select Case _FileFormat
            Case DTOCsa.FileFormats.RemesesExportacioLaCaixa
                retval = String.Format("remesa {0:0000}.{1:0000} export a {2}.txt", _Fch.Year, _Id, DTOBank.NomComercialORaoSocial(_Banc.Iban.BankBranch.Bank))
            Case DTOCsa.FileFormats.SepaCore
                retval = String.Format("remesa {0:0000}.{1:0000} Sepa Core a {2}.xml", _Fch.Year, _Id, DTOBank.NomComercialORaoSocial(_Banc.Iban.BankBranch.Bank))
        End Select
        Return retval
    End Function

    Shared Function TotalNominal(oCsa As DTOCsa) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oCsb As DTOCsb In oCsa.Items
            retval.Add(oCsb.Amt)
        Next
        Return retval
    End Function

    Shared Function TotalDespeses(oCsa As DTOCsa) As DTOAmt
        Dim retval As DTOAmt = DTOCsb.TotalDespeses(oCsa.Items, oCsa.Term)
        Return retval
    End Function

    Shared Function GetTAE(oCsa As DTOCsa) As Decimal
        Dim iDias As Integer = DiasVencimientoMedioPonderado(oCsa)
        Dim oDespeses As DTOAmt = TotalDespeses(oCsa)
        Dim oNominal As DTOAmt = TotalNominal(oCsa)

        Dim retval As Decimal
        If oNominal.eur > 0 Then
            retval = 360 * oDespeses.eur / (oNominal.eur * iDias)
        End If
        Return retval
    End Function

    Shared Function DiasVencimientoMedioPonderado(oCsa As DTOCsa) As Integer
        Dim retval As Integer
        Dim DcSumaDeNominalesPorVencimientos As Decimal
        Dim DcTotalNominal As Decimal = TotalNominal(oCsa).eur
        For Each oCsb As DTOCsb In oCsa.Items
            Dim iDias As Integer = DateDiff(DateInterval.Day, oCsa.Fch, oCsb.Vto)
            DcSumaDeNominalesPorVencimientos += oCsb.Amt.eur * iDias
        Next
        If DcTotalNominal > 0 Then
            retval = DcSumaDeNominalesPorVencimientos / DcTotalNominal
        End If
        Return retval
    End Function

    Shared Function SepaFormat(oAmt As DTOAmt) As String
        Dim retval As String = SepaFormat(oAmt.Eur)
        Return retval
    End Function

    Shared Function SepaFormat(DcEur As Decimal) As String
        Dim retval As String = DcEur.ToString("F2").Replace(",", ".")
        Return retval
    End Function

    Shared Function SepaFormat(DtFch As Date) As String
        Dim retval As String = String.Format(DtFch, "yyyy-MM-dd")
        Return retval
    End Function

    Shared Function SepaNormalized(src As String) As String
        Dim validChars() As Char = DTOCsa.SepaAllowedChars()
        Dim sb As New System.Text.StringBuilder
        For Each oChar As Char In src.ToArray
            If validChars.Contains(oChar) Then
                sb.Append(oChar)
            Else
                Dim iHex32 As Integer = Convert.ToInt32(oChar)
                Dim HexValue As String = iHex32.ToString("X4")
                Dim sAlt As String = String.Format("\u{0}", HexValue)
                sb.Append(sAlt)
            End If
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function SepaAllowedChars() As Char()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("abcdefghijklmnopqrstuvwxyz")
        sb.AppendLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ")
        sb.AppendLine("0123456789")
        sb.AppendLine("/-?:().,'+ ")
        Dim src As String = sb.ToString
        Dim retval() As Char = src.ToArray
        Return retval
    End Function



#Region "SepaCore"
    Shared Function SepaCoreXML(oEmp As DTOEmp, oCsa As DTOCsa, exs As List(Of Exception)) As String

        'Raiz
        Dim oXmlDocument As New DTOXmlDocument(IncludeProlog:=True)
        Dim oRootSegment As DTOXmlSegment = oXmlDocument.AddSegment("Document")
        oRootSegment.AddAttributes("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02", "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Dim oMainSegment As DTOXmlSegment = oRootSegment.AddSegment("CstmrDrctDbtInitn")

        'cabecera
        Dim oGrpHdr As DTOXmlSegment = oMainSegment.AddSegment("GrpHdr")
        Dim oMsgId As DTOXmlSegment = oGrpHdr.AddSegment("MsgId", SepaMsgId(oCsa))
        Dim oCreDtTm As DTOXmlSegment = oGrpHdr.AddSegment("CreDtTm", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"))
        Dim oNbOfTxs As DTOXmlSegment = oGrpHdr.AddSegment("NbOfTxs", oCsa.Items.Count)
        Dim oCtrlSum As DTOXmlSegment = oGrpHdr.AddSegment("CtrlSum", SepaFormatSuma(oCsa.Items))

        Dim oParteIniciadora As DTOXmlSegment = oGrpHdr.AddSegment("InitgPty")
        Dim oOrgNm As DTOXmlSegment = oParteIniciadora.AddSegment("Nm", oEmp.Org.Nom)
        Dim oOrgId As DTOXmlSegment = oParteIniciadora.AddSegment("Id")
        Dim oOrgId2 As DTOXmlSegment = oOrgId.AddSegment("OrgId")
        Dim oOrgIdOthr As DTOXmlSegment = oOrgId2.AddSegment("Othr")
        Dim oOrgIdOthrId As DTOXmlSegment = oOrgIdOthr.AddSegment("Id", SepaIdentificacioPresentador(oCsa))

        'Dim oOrgIdOthrSchmeNm As DTOXmlSegment = oOrgIdOthr.AddSegment("SchmeNm")
        'Dim oOrgIdOthrSchmeNmCd As DTOXmlSegment = oOrgIdOthrSchmeNm.AddSegment("Cd", "SEPA")

        Dim PmtInfId As Integer
        Dim Query = oCsa.Items.GroupBy(Function(g) New With {Key g.Vto}).Select(Function(group) New With {.Vto = group.Key.Vto,
                                                              .Sum = group.Sum(Function(x) x.Amt.Eur)})
        For Each oPmtInf In Query
            PmtInfId += 1
            Dim oPmtInfCsbs As List(Of DTOCsb) = oCsa.Items.FindAll(Function(x) x.Vto = oPmtInf.Vto)

            'información del pago
            Dim oPaymentInfo As DTOXmlSegment = oMainSegment.AddSegment("PmtInf") 'Payment info
            Dim oPaymentInfoId As DTOXmlSegment = oPaymentInfo.AddSegment("PmtInfId", "PmtInfId-" & String.Format(PmtInfId, "000"))
            Dim oPaymentMethod As DTOXmlSegment = oPaymentInfo.AddSegment("PmtMtd", "DD") 'Direct Debit
            Dim oNbOfTxs2 As DTOXmlSegment = oPaymentInfo.AddSegment("NbOfTxs", oPmtInfCsbs.Count)
            Dim oCtrlSum2 As DTOXmlSegment = oPaymentInfo.AddSegment("CtrlSum", DTOCsa.SepaFormat(oPmtInf.Sum))
            Dim oPaymentTypeInfo As DTOXmlSegment = oPaymentInfo.AddSegment("PmtTpInf")
            Dim oServiceLevel As DTOXmlSegment = oPaymentTypeInfo.AddSegment("SvcLvl")
            Dim oServiceLevelCode As DTOXmlSegment = oServiceLevel.AddSegment("Cd", "SEPA")
            Dim oLocalInstrument As DTOXmlSegment = oPaymentTypeInfo.AddSegment("LclInstrm")
            Dim oLocalInstrumentCode As DTOXmlSegment = oLocalInstrument.AddSegment("Cd", "CORE")
            Dim oSequenceType As DTOXmlSegment = oPaymentTypeInfo.AddSegment("SeqTp", "RCUR")
            Dim oCategoryPurpose As DTOXmlSegment = oPaymentTypeInfo.AddSegment("CtgyPurp")
            Dim oCategoryPurposeCode As DTOXmlSegment = oCategoryPurpose.AddSegment("Cd", "TRAD")

            Dim oRequestedCollectionDate As DTOXmlSegment = oPaymentInfo.AddSegment("ReqdColltnDt", DTOCsa.SepaFormat(oPmtInf.Vto))

            Dim oCreditor As DTOXmlSegment = oPaymentInfo.AddSegment("Cdtr")
            Dim oCreditorNm As DTOXmlSegment = oCreditor.AddSegment("Nm", oEmp.Org.Nom)
            Dim oCreditorPostalAddress As DTOXmlSegment = oCreditor.AddSegment("PstlAdr")
            Dim oCreditorPostalAddressCountry As DTOXmlSegment = oCreditorPostalAddress.AddSegment("Ctry", DTOAddress.Country(oEmp.Org.Address).ISO)
            Dim oCreditorAdrLine1 As DTOXmlSegment = oCreditorPostalAddress.AddSegment("AdrLine", DTOCsa.SepaNormalized(oEmp.Org.Address.Text))
            Dim oCreditorAdrLine2 As DTOXmlSegment = oCreditorPostalAddress.AddSegment("AdrLine", DTOCsa.SepaNormalized(DTOAddress.ZipyCit(oEmp.Org.Address)))


            Dim oCreditorAccount As DTOXmlSegment = oPaymentInfo.AddSegment("CdtrAcct")
            Dim oCreditorAccountId As DTOXmlSegment = oCreditorAccount.AddSegment("Id")
            Dim oCreditorAccountIban As DTOXmlSegment = oCreditorAccountId.AddSegment("IBAN", oCsa.Banc.Iban.Digits)
            Dim oCreditorCurrency As DTOXmlSegment = oCreditorAccount.AddSegment("Ccy", "EUR")

            Dim oCreditorAgent As DTOXmlSegment = oPaymentInfo.AddSegment("CdtrAgt")
            Dim oFinancialInstitutionIdentification As DTOXmlSegment = oCreditorAgent.AddSegment("FinInstnId")
            Dim oFinancialInstitutionBIC As DTOXmlSegment = oFinancialInstitutionIdentification.AddSegment("BIC", oCsa.Banc.Iban.BankBranch.Bank.Swift)

            'afegit per DB: --------------
            'opcional a la resta de bancs; si s'inclou el valor als Sepa Core ha de ser sempre SLEV (Service Level)
            Dim oChrgBr As DTOXmlSegment = oPaymentInfo.AddSegment("ChrgBr", "SLEV")
            '-----------------------------

            Dim oCreditorId As DTOXmlSegment = oPaymentInfo.AddSegment("CdtrSchmeId")
            Dim oCreditorIdId As DTOXmlSegment = oCreditorId.AddSegment("Id")
            Dim oCreditorPrvtId As DTOXmlSegment = oCreditorIdId.AddSegment("PrvtId")
            Dim oCreditorPrvtIdOthr As DTOXmlSegment = oCreditorPrvtId.AddSegment("Othr")
            Dim oCreditorPrvtIdOthrId As DTOXmlSegment = oCreditorPrvtIdOthr.AddSegment("Id", SepaIdentificacioPresentador(oCsa))
            Dim oCreditorPrvtIdSchmeNm As DTOXmlSegment = oCreditorPrvtIdOthr.AddSegment("SchmeNm")
            Dim oCreditorPrvtIdSchmeNmPrtry As DTOXmlSegment = oCreditorPrvtIdSchmeNm.AddSegment("Prtry", "SEPA")

            For Each item As DTOCsb In oPmtInfCsbs
                Dim oDebitInfo As DTOXmlSegment = oPaymentInfo.AddSegment("DrctDbtTxInf")
                Dim oPaymentId As DTOXmlSegment = oDebitInfo.AddSegment("PmtId")
                Dim oInstructionIdentification As DTOXmlSegment = oPaymentId.AddSegment("InstrId", item.GuionLessGuid)
                Dim oEndToEndId As DTOXmlSegment = oPaymentId.AddSegment("EndToEndId", item.FormattedId())
                Dim oInstructedAmount As DTOXmlSegment = oDebitInfo.AddSegment("InstdAmt", DTOCsa.SepaFormat(item.Amt))
                oInstructedAmount.AddAttribute("Ccy", "EUR")

                Dim oDirectDebitTransaction As DTOXmlSegment = oDebitInfo.AddSegment("DrctDbtTx")
                Dim oMandateRelatedInformation As DTOXmlSegment = oDirectDebitTransaction.AddSegment("MndtRltdInf")
                Dim oMandateId As DTOXmlSegment = oMandateRelatedInformation.AddSegment("MndtId", item.Iban.GuionLessGuid)
                Dim oDtOfSgntr As DTOXmlSegment = oMandateRelatedInformation.AddSegment("DtOfSgntr", DTOCsa.SepaFormat(item.Iban.FchFrom))

                'afegit per Cx: --------------
                Dim oCreditorId2 As DTOXmlSegment = oDirectDebitTransaction.AddSegment("CdtrSchmeId")
                Dim oCreditorIdId2 As DTOXmlSegment = oCreditorId2.AddSegment("Id")
                Dim oCreditorPrvtId2 As DTOXmlSegment = oCreditorIdId2.AddSegment("PrvtId")
                Dim oCreditorPrvtIdOthr2 As DTOXmlSegment = oCreditorPrvtId2.AddSegment("Othr")
                Dim oCreditorPrvtIdOthrId2 As DTOXmlSegment = oCreditorPrvtIdOthr2.AddSegment("Id", SepaIdentificacioPresentador(oCsa))
                Dim oCreditorPrvtIdSchmeNm2 As DTOXmlSegment = oCreditorPrvtIdOthr2.AddSegment("SchmeNm")
                Dim oCreditorPrvtIdSchmeNmPrtry2 As DTOXmlSegment = oCreditorPrvtIdSchmeNm2.AddSegment("Prtry", "SEPA")
                '-----------------------------

                Dim oDebtorAgent As DTOXmlSegment = oDebitInfo.AddSegment("DbtrAgt")
                Dim oDebtorAgentFinInstnId As DTOXmlSegment = oDebtorAgent.AddSegment("FinInstnId")
                Dim oDebtorAgentFinInstnIdBIC As DTOXmlSegment = oDebtorAgentFinInstnId.AddSegment("BIC", item.Iban.BankBranch.Bank.Swift)

                Dim oDebtor As DTOXmlSegment = oDebitInfo.AddSegment("Dbtr")
                Dim oDebtorName As DTOXmlSegment = oDebtor.AddSegment("Nm", DTOCsa.SepaNormalized(item.Contact.Nom))

                Dim oDebtorAccount As DTOXmlSegment = oDebitInfo.AddSegment("DbtrAcct")
                Dim oDebtorAccountId As DTOXmlSegment = oDebtorAccount.AddSegment("Id")
                Dim oDebtorAccountIdIban As DTOXmlSegment = oDebtorAccountId.AddSegment("IBAN", item.Iban.Digits)

                Dim oRemittanceInformation As DTOXmlSegment = oDebitInfo.AddSegment("RmtInf")

                If item.Txt = "" Then
                    Dim sMsg As String = String.Format("falta concepte a l'efecte de {0} de {1}", DTOAmt.CurFormatted(item.Amt), item.Contact.Nom)
                    exs.Add(New Exception(sMsg))
                Else
                    Dim oRemittanceInformationUnstructured As DTOXmlSegment = oRemittanceInformation.AddSegment("Ustrd", DTOCsa.SepaNormalized(item.Txt))
                End If

            Next
        Next

        Dim retval As String = DTOXmlDocument.ToString(oXmlDocument)
        Return retval
    End Function

    Protected Shared Function SepaMsgId(oCsa As DTOCsa) As String
        Dim retval As String = String.Format("{0}{1}{2:yyyyMMddhhmmss}", IIf(oCsa.Descomptat, "FSDD", ""), oCsa.formattedId(), Now)
        Return retval
    End Function

    Protected Shared Function SepaIdentificacioPresentador(oCsa As DTOCsa) As String
        Dim retval As String = oCsa.Banc.SepaCoreIdentificador ' sb.ToString

        'provisional: DB necessita el sufixe 001 per anticips de crèdit
        If oCsa.banc.Equals(DTOBanc.wellknown(DTOBanc.wellknowns.DeutscheBank)) And Not oCsa.descomptat Then
            Dim sNouSufixe As String = "001"
            retval = String.Format("{0}{1}{2}", retval.Substring(0, 4), sNouSufixe, retval.Substring(7))
        End If

        Return retval
    End Function

    Shared Function SepaFormatSuma(items As List(Of DTOCsb)) As String
        Dim DcEur As Decimal = items.Sum(Function(x) x.Amt.Eur)
        Dim retval As String = DTOCsa.SepaFormat(DcEur)
        Return retval
    End Function

    Shared Function ValidateSepaCore(oCsa As DTOCsa, ByRef exs As List(Of Exception)) As Boolean

        'check venciments antics
        Dim iCount As Integer
        For Each oCsb As DTOCsb In oCsa.Items
            If oCsb.Vto < Today Then iCount += 1
        Next
        If iCount > 0 Then
            'exs.Add(New Exception("hi han " & iCount & " venciments caducats"))
        End If

        For Each item As DTOCsb In oCsa.Items
            Dim oIban As DTOIban = item.Iban
            If oIban Is Nothing Then
                exs.Add(New Exception(item.Contact.Nom & " no té compte corrent"))
            Else
                If oIban.Digits = "" Then exs.Add(New Exception(item.Contact.Nom & " sense compte corrent"))
                If oIban.IsActive() Then
                    If oIban.BankBranch Is Nothing Then
                        exs.Add(New Exception("bank no registrat al compte de " & item.Contact.Nom))
                    Else
                        Dim oBankBranch As DTOBankBranch = oIban.BankBranch
                        If oBankBranch.Bank Is Nothing Then
                            exs.Add(New Exception("entitat bancaria no registrada al compte " & DTOIban.Formated(oIban)))
                        Else
                            Dim oBank As DTOBank = oBankBranch.Bank
                            If oBank.Swift = "" Then
                                exs.Add(New Exception("falta codi BIC al banc " & DTOBank.NomComercialORaoSocial(oBank)))
                            ElseIf Not DTOBank.ValidateBIC(oBank.Swift) Then
                                exs.Add(New Exception("codi BIC '" & oBank.Swift & "' erroni al banc " & DTOBank.NomComercialORaoSocial(oBank)))
                            End If
                        End If
                    End If
                Else
                    exs.Add(New Exception("falta mandato de " & item.Contact.Nom))
                End If
            End If
        Next

        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function
#End Region
End Class
