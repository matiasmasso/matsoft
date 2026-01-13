Public Class CustomerTarifa


    Shared Function Load(oUserOrCustomer As DTOBaseGuid, Optional DtFch As Date = Nothing, Optional oMgz As DTOMgz = Nothing, Optional oLang As DTOLang = Nothing, Optional IncludeObsoletos As Boolean = False) As DTOCustomerTarifa.Compact
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Now()
        If oLang Is Nothing Then oLang = DTOLang.ESP

        Dim oRol As DTORol = Nothing
        Dim oCustomer As DTOCustomer = Nothing
        If TypeOf oUserOrCustomer Is DTOUser Then
            oRol = DirectCast(oUserOrCustomer, DTOUser).Rol
            Dim oCustomers As List(Of DTOCustomer) = BEBL.User.GetCustomers(oUserOrCustomer)
            If oCustomers.Count > 0 Then
                oCustomer = BEBL.Customer.CcxOrMe(oCustomers.First)
            End If
        ElseIf TypeOf oUserOrCustomer Is DTOCustomer Then
            oCustomer = BEBL.Customer.Find(oUserOrCustomer.Guid).CcxOrMe()
            If oCustomer Is Nothing Then
                oRol = New DTORol(DTORol.Ids.unregistered)
            Else
                oCustomer = BEBL.Customer.CcxOrMe(oCustomer)
                oRol = oCustomer.Rol
            End If
        ElseIf oUserOrCustomer IsNot Nothing Then
            oCustomer = BEBL.Customer.Find(oUserOrCustomer.Guid)
            If oCustomer IsNot Nothing Then
                oCustomer = BEBL.Customer.CcxOrMe(oCustomer)
                BEBL.Customer.Load(oCustomer)
                oRol = CType(oUserOrCustomer, DTOContact).Rol
            End If
        Else
            'Consumer
            oRol = New DTORol(DTORol.Ids.unregistered)
        End If

        Dim retval = DTOCustomerTarifa.Compact.Factory(oCustomer, DtFch)
        retval.Brands = CustomerTarifaItemsLoader.Items(oUserOrCustomer, DtFch, oMgz, IncludeObsoletos:=IncludeObsoletos)
        If oRol Is Nothing And oUserOrCustomer IsNot Nothing Then oRol = DirectCast(oUserOrCustomer, DTOCustomer).Rol
        Select Case oRol.id
            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                If retval.Customer IsNot Nothing Then
                    Dim oCustomCosts As List(Of DTOPricelistItemCustomer) = BEBL.PriceListItemsCustomer.Active(oCustomer, DtFch)
                    Dim oCliProductDtos = BEBL.CliProductDtos.All(oCustomer) 'Discount over customer cost price
                    Dim oDtos = BEBL.CustomerTarifaDtos.Active(oCustomer, DtFch)
                    retval.CostEnabled = oDtos.Count > 0

                    For Each oBrand In retval.Brands
                        Dim oProductBrand = oBrand.ToProductBrand()
                        For Each oCategory In oBrand.Categories
                            Dim oProductCategory = oCategory.ToProductCategory(oProductBrand)
                            For Each oSku In oCategory.Skus
                                'If oSku.Ean13 IsNot Nothing AndAlso oSku.Ean13.Value = "5010415510013" Then Stop
                                Dim oProductSku = oSku.ToProductSku(oProductCategory)
                                Dim oCost As DTOAmt = BEBL.PriceListCustomer.GetCustomerCost(oProductSku, oCustomCosts, oDtos)
                                oSku.Price = DTOAmt.Compact.Factory(oCost)
                                Dim oCliProductDto As DTOCliProductDto = DTOProductSku.GetCliProductDto(oProductSku, oCliProductDtos)
                                If oCliProductDto IsNot Nothing Then
                                    oSku.CustomerDto = oCliProductDto.Dto
                                End If
                            Next
                        Next
                    Next
                End If
        End Select

        Return retval
    End Function



End Class
