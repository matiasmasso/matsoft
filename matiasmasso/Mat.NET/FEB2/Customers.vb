Public Class Customer

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOCustomer)
        Return Await Api.Fetch(Of DTOCustomer)(exs, "Customer", oGuid.ToString())
    End Function

    Shared Async Function Exists(exs As List(Of Exception), oCustomerCandidate As DTOContact) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Customer/Exists", oCustomerCandidate.Guid.ToString())
    End Function

    Shared Function ExistsSync(exs As List(Of Exception), oCustomerCandidate As DTOContact) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "Customer/Exists", oCustomerCandidate.Guid.ToString())
    End Function

    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid) As DTOCustomer
        Return Api.FetchSync(Of DTOCustomer)(exs, "Customer", oGuid.ToString())
    End Function

    Shared Function IsGroupSync(exs As List(Of Exception), oContact As DTOContact) As Boolean
        Dim retval As Boolean
        If TypeOf oContact Is DTOCustomer AndAlso DirectCast(oContact, DTOCustomer).ccx IsNot Nothing Then
            retval = True
        Else
            retval = Api.FetchSync(Of Boolean)(exs, "Customer/IsGroup", oContact.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Async Function EFrasEnabled(exs As List(Of Exception), oContact As DTOContact) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Customer/EFrasEnabled", oContact.Guid.ToString())
    End Function

    Shared Async Function Children(exs As List(Of Exception), oContact As DTOContact) As Task(Of List(Of DTOCustomer))
        Return Await Api.Fetch(Of List(Of DTOCustomer))(exs, "Customer/Children", oContact.Guid.ToString())
    End Function

    Shared Function Load(ByRef oCustomer As DTOCustomer, exs As List(Of Exception)) As Boolean
        If Not oCustomer.IsLoaded And Not oCustomer.IsNew Then
            Dim pCustomer = Api.FetchSync(Of DTOCustomer)(exs, "Customer", oCustomer.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCustomer)(pCustomer, oCustomer, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCustomer)(oCustomer, exs, "Customer")
        oCustomer.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCustomer)(oCustomer, exs, "Customer")
    End Function

    Shared Function CcxOrMe(exs As List(Of Exception), oCustomer As DTOCustomer) As DTOCustomer
        Dim retval As DTOCustomer = Nothing
        If FEB2.Customer.Load(oCustomer, exs) Then
            retval = oCustomer.CcxOrMe
        End If
        Return retval
    End Function

    Shared Function Factory(exs As List(Of Exception), oContact As DTOContact) As DTOCustomer
        Dim retval = DTOCustomer.FromContact(oContact)
        With retval
            .Emp = oContact.Emp
            .IVA = FEB2.Customer.GuessIVA(oContact)
            .Req = FEB2.Customer.GuessREQ(exs, oContact)
            .FraPrintMode = DTOCustomer.FraPrintModes.Email
            .AlbValorat = True
            .cashCod = DTOCustomer.CashCodes.TransferenciaPrevia
            .PaymentTerms = New DTOPaymentTerms
            .PaymentTerms.Cod = DTOPaymentTerms.CodsFormaDePago.Comptat
        End With
        Return retval
    End Function


    Shared Function ShippingAddressOrDefault(oCustomer As DTOCustomer) As DTOAddress
        Dim exs As New List(Of Exception)
        Dim retval As DTOAddress = Nothing
        Dim oAddresses As List(Of DTOAddress) = FEB2.Addresses.AllSync(oCustomer, exs)
        If oAddresses IsNot Nothing Then
            retval = oAddresses.Find(Function(x) x.Codi = DTOAddress.Codis.Entregas)
            If retval Is Nothing Then
                retval = oAddresses.Find(Function(x) x.Codi = DTOAddress.Codis.Fiscal)
            End If
        End If
        Return retval
    End Function

    Shared Async Function ShippingAddressOrDefaultAsync(oCustomer As DTOCustomer) As Task(Of DTOAddress)
        Dim exs As New List(Of Exception)
        Dim retval As DTOAddress = Nothing
        Dim oAddresses As List(Of DTOAddress) = Await FEB2.Addresses.All(oCustomer, exs)
        If oAddresses IsNot Nothing Then
            retval = oAddresses.Find(Function(x) x.Codi = DTOAddress.Codis.Entregas)
            If retval Is Nothing Then
                retval = oAddresses.Find(Function(x) x.Codi = DTOAddress.Codis.Fiscal)
            End If
        End If
        Return retval
    End Function

    Shared Function PaymentTermsText(oCustomer As DTOCustomer, oLang As DTOLang) As List(Of String) 'MVC  \Views\CustomerBasket\Closure_.vbhtml
        Dim exs As New List(Of Exception)
        FEB2.Customer.Load(oCustomer, exs)
        Dim sb As New System.Text.StringBuilder
        Select Case oCustomer.PaymentTerms.Cod
            Case DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria
                sb.Append(oLang.Tradueix("Efecto domiciliado Sepa Core", "Efecte domiciliat Sepa Core", "Sepa Core Bank draft"))
                If oCustomer.PaymentTerms.Months = 0 Then
                    sb.AppendLine(" " & oLang.Tradueix(" a la vista", " a la vista", " at sight"))
                Else
                    sb.AppendLine(String.Format(" {0:00} {1}", 30 * oCustomer.PaymentTerms.Months, oLang.Tradueix("dias", "dies", "days")))
                End If

                Dim oIban = FEB2.Iban.FromContactSync(exs, oCustomer, DTOIban.Cods.Client)
                oCustomer.PaymentTerms.Iban = oIban
                sb.AppendLine(oLang.Tradueix("Cuenta", "Compte", "Account") & " " & DTOIban.Formated(oIban))
                sb.AppendLine(DTOIban.BankNom(oIban))
                sb.AppendLine(DTOIban.BranchLocationAndAdr(oIban))
        End Select
        Dim src As String = sb.ToString
        Dim retval As List(Of String) = src.Split(vbCrLf).ToList
        Return retval
    End Function


    Shared Function IsInsolvent(oContact As DTOContact) As Boolean
        Return False 'TODO
    End Function

    Shared Function ValidateCustomer(oCustomer As DTOCustomer, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        If oCustomer.IsConsumer Then
            retval = True
        Else
            FEB2.Customer.Load(oCustomer, exs)
            With oCustomer
                If .contactClass Is Nothing Then
                    exs.Add(New Exception("falta classificar el client per assignar-li un canal de distribució"))
                End If
                Dim oParent = oCustomer.ccxOrMe()
                If .PrimaryNifValue() = "" Then
                    If oParent.PrimaryNifValue() = "" Then
                        Dim oCountry As DTOCountry = DTOAddress.Country(oCustomer.Address)
                        Dim missingNif As Boolean = DTOCountry.IsEsp(oCountry)
                        If oCountry.ExportCod = DTOInvoice.ExportCods.intracomunitari Then missingNif = True
                        If missingNif Then
                            exs.Add(New Exception("falta el NIF"))
                        End If
                    End If
                End If
                If oParent.cashCod = DTOCustomer.CashCodes.notSet Then exs.Add(New Exception("falta la forma de pagament"))
            End With
            retval = exs.Count = 0
        End If
        Return retval
    End Function


    Shared Function GuessIVA(oContact As DTOContact) As Boolean
        Dim retval As Boolean = (DTOAddress.ExportCod(oContact.Address) = DTOInvoice.ExportCods.Nacional)
        Return retval
    End Function

    Shared Function GuessREQ(exs As List(Of Exception), oContact As DTOContact) As Boolean
        Dim retval As Boolean
        If FEB2.Customer.GuessIVA(oContact) Then
            Dim sNom As String = oContact.Nom
            If TypeOf (oContact) Is DTOCustomer Then
                sNom = FEB2.Customer.CcxOrMe(exs, oContact).Nom
            End If
            If Not sNom.EndsWith("S.A.") And Not sNom.EndsWith("S.L.") And Not sNom.EndsWith("S.C.") And Not sNom.EndsWith("S.C.P.") Then
                retval = True
            End If
        End If
        Return retval
    End Function

    Shared Async Function SkuPrice(exs As List(Of Exception), oCustomer As DTOCustomer, oSku As DTOProductSku, Optional DtFch As Date = Nothing) As Task(Of DTOAmt)
        FEB2.Customer.Load(oCustomer, exs)
        FEB2.ProductSku.Load(oSku, exs)
        Dim oCcx As DTOCustomer = FEB2.Customer.CcxOrMe(exs, oCustomer)
        Dim oTarifaDtos As List(Of DTOCustomerTarifaDto) = Await FEB2.CustomerTarifaDtos.Active(exs, oCcx)
        Dim retval = DTOProductSku.GetCustomerCost(oSku, oTarifaDtos, DtFch)
        Return retval
    End Function

End Class


Public Class Customers

    Shared Async Function FromUser(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOCustomer))
        Return Await Api.Fetch(Of List(Of DTOCustomer))(exs, "Customers/FromUser", oUser.Guid.ToString())
    End Function

    Shared Async Function RaonsSocialsWithInvoices(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOCustomer))
        Return Await Api.Fetch(Of List(Of DTOCustomer))(exs, "Customers/RaonsSocialsWithInvoices", oUser.Guid.ToString())
    End Function

End Class