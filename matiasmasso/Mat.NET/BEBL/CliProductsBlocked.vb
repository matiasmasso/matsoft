Public Class CliProductBlocked
    Shared Function Find(oContact As DTOContact, oProduct As DTOProduct) As DTOCliProductBlocked
        Dim retval As DTOCliProductBlocked = CliProductBlockedLoader.Find(oContact, oProduct)
        Return retval
    End Function

    Shared Function Update(oCliProductBlocked As DTOCliProductBlocked, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliProductBlockedLoader.Update(oCliProductBlocked, exs)
        Return retval
    End Function

    Shared Function Delete(oCliProductBlocked As DTOCliProductBlocked, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliProductBlockedLoader.Delete(oCliProductBlocked, exs)
        Return retval
    End Function

    Shared Function AltresEnExclusiva(oContact As DTOContact, oProduct As DTOProduct) As List(Of DTOContact)
        Dim retval As List(Of DTOContact) = CliProductBlockedLoader.AltresEnExclusiva(oContact, oProduct)
        Return retval
    End Function

    Shared Function IsAllowed(items As List(Of DTOCliProductBlocked), oProduct As DTOProduct) As Boolean
        Dim isIncluded As Boolean
        Dim isExcluded As Boolean

        If items Is Nothing Then
            'no hi ha client, el destinatari es el consumidor
            isIncluded = True
            isExcluded = False
        Else

            'chequeja que el producte no estigui exclos
            isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oProduct) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))

            If Not isExcluded Then
                'chequeja que els parents del producte no estiguin exclosos
                If TypeOf oProduct Is DTOProductCategory Then
                    Dim oBrand As DTOProductBrand = DirectCast(oProduct, DTOProductCategory).brand
                    isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))
                ElseIf TypeOf oProduct Is DTOProductSku Then
                    Dim oCategory As DTOProductCategory = DirectCast(oProduct, DTOProductSku).category
                    isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oCategory) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))
                    If Not isExcluded Then
                        Dim oBrand As DTOProductBrand = oCategory.brand
                        isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))
                    End If
                End If

            End If


            If Not isExcluded Then
                Dim oCodDist As DTOProductBrand.CodDists = BEBL.Product.BrandCodDist(oProduct) 'potential performance issue if oProduct.Category.Brand is nothing
                Select Case oCodDist
                    Case DTOProductBrand.CodDists.DistribuidorsOficials
                        'chequeja que el producte estigui inclos
                        isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oProduct) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))

                        isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oProduct) And x.cod = DTOCliProductBlocked.Codis.standard)

                        If Not isIncluded And Not isExcluded Then
                            'chequeja que els parents del producte estiguin inclosos

                            If TypeOf oProduct Is DTOProductCategory Then
                                Dim oBrand As DTOProductBrand = DirectCast(oProduct, DTOProductCategory).brand
                                isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))

                                isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And x.cod = DTOCliProductBlocked.Codis.standard)

                            ElseIf TypeOf oProduct Is DTOProductSku Then
                                Dim oCategory As DTOProductCategory = DirectCast(oProduct, DTOProductSku).category
                                isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oCategory) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))

                                isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oCategory) And x.cod = DTOCliProductBlocked.Codis.standard)

                                If Not isIncluded And Not isExcluded Then
                                    Dim oBrand As DTOProductBrand = oCategory.brand
                                    isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))
                                    isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And x.cod = DTOCliProductBlocked.Codis.standard)
                                End If

                            End If
                        End If
                    Case Else
                        isIncluded = True
                End Select

            End If


        End If
        Dim retval As Boolean = isIncluded And Not isExcluded
        Return retval
    End Function

End Class

Public Class CliProductsBlocked

    Shared Function All(oContact As DTOContact) As List(Of DTOCliProductBlocked)
        BEBL.Contact.Load(oContact)
        Dim retval As List(Of DTOCliProductBlocked) = CliProductsBlockedLoader.All(oContact)
        Return retval
    End Function

    Shared Function DistribuidorsOficialsActiveEmails(oBrand As DTOProductBrand) As List(Of DTOEmail)
        Return CliProductsBlockedLoader.DistribuidorsOficialsActiveEmails(oBrand)
    End Function



    Shared Function Delete(oCliProductsBlocked As List(Of DTOCliProductBlocked), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliProductsBlockedLoader.Delete(oCliProductsBlocked, exs)
        Return retval
    End Function

    Shared Function IsAllowed(items As List(Of DTOCliProductBlocked), oBrand As DTOProductBrand) As Boolean
        Dim retval As Boolean
        If items Is Nothing Then
            'no hi ha client, el destinatari es el consumidor
            retval = True
        Else
            Select Case oBrand.codDist
                Case DTOProductBrand.CodDists.Free
                    retval = Not items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))
                Case DTOProductBrand.CodDists.DistribuidorsOficials
                    retval = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))
            End Select
        End If
        Return retval
    End Function


    Shared Function IsAllowed(items As List(Of DTOCliProductBlocked), oProduct As DTOProduct) As Boolean
        Dim isIncluded As Boolean
        Dim isExcluded As Boolean

        If items Is Nothing Then
            'no hi ha client, el destinatari es el consumidor
            isIncluded = True
            isExcluded = False
        Else

            'chequeja que el producte no estigui exclos
            isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oProduct) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))

            If Not isExcluded Then
                'chequeja que els parents del producte no estiguin exclosos
                If TypeOf oProduct Is DTOProductCategory Then
                    Dim oBrand As DTOProductBrand = DirectCast(oProduct, DTOProductCategory).brand
                    isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))
                ElseIf TypeOf oProduct Is DTOProductSku Then
                    Dim oCategory As DTOProductCategory = DirectCast(oProduct, DTOProductSku).category
                    isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oCategory) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))
                    If Not isExcluded Then
                        Dim oBrand As DTOProductBrand = oCategory.brand
                        isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclos _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.noAplicable _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.altresEnExclusiva))
                    End If
                End If

            End If


            If Not isExcluded Then
                Dim oCodDist As DTOProductBrand.CodDists = BEBL.Product.BrandCodDist(oProduct) 'potential performance issue if oProduct.Category.Brand is nothing
                Select Case oCodDist
                    Case DTOProductBrand.CodDists.DistribuidorsOficials
                        'chequeja que el producte estigui inclos
                        isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oProduct) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))

                        isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oProduct) And x.cod = DTOCliProductBlocked.Codis.standard)

                        If Not isIncluded And Not isExcluded Then
                            'chequeja que els parents del producte estiguin inclosos

                            If TypeOf oProduct Is DTOProductCategory Then
                                Dim oBrand As DTOProductBrand = DirectCast(oProduct, DTOProductCategory).brand
                                isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))

                                isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And x.cod = DTOCliProductBlocked.Codis.standard)

                            ElseIf TypeOf oProduct Is DTOProductSku Then
                                Dim oCategory As DTOProductCategory = DirectCast(oProduct, DTOProductSku).category
                                isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oCategory) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))

                                isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oCategory) And x.cod = DTOCliProductBlocked.Codis.standard)

                                If Not isIncluded And Not isExcluded Then
                                    Dim oBrand As DTOProductBrand = oCategory.brand
                                    isIncluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And (x.cod = DTOCliProductBlocked.Codis.exclusiva _
                                                                                  Or x.cod = DTOCliProductBlocked.Codis.distribuidorOficial))
                                    isExcluded = items.Exists(Function(x) CType(x.product, DTOProduct).Equals(oBrand) And x.cod = DTOCliProductBlocked.Codis.standard)
                                End If

                            End If
                        End If
                    Case Else
                        isIncluded = True
                End Select

            End If


        End If
        Dim retval As Boolean = isIncluded And Not isExcluded
        Return retval
    End Function


End Class
