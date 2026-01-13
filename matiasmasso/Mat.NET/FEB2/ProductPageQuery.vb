Public Class ProductPageQuery

    Shared Async Function Load(oEmp As DTOEmp, Brand As String, Catchall As String) As Task(Of DTOProductPageQuery)
        Dim exs As New List(Of Exception)
        Dim retval As New DTOProductPageQuery

        Dim oBrand = Await FEB2.ProductBrand.FromNom(exs, oEmp, Brand)
        If oBrand IsNot Nothing Then
            With retval
                .Product = oBrand
                .Tab = DTOProduct.Tabs.general
            End With

            If Catchall = "" Then
                retval.Product = oBrand
            Else
                Dim sSegments() As String = Catchall.Split("/")
                Select Case sSegments.Count
                    Case 1
                        If IsDept(retval.Product, sSegments(0), retval) Then
                            CType(retval.Product, DTODept).brand = oBrand
                        ElseIf IsCategory(retval.Product, sSegments(0), retval) Then
                        ElseIf IsTab(retval.Product, sSegments(0), retval) Then
                        ElseIf IsLocation(retval.Product, sSegments(0), retval) Then
                        ElseIf IsZona(retval.Product, sSegments(0), retval) Then
                        Else
                            retval = Nothing
                        End If
                    Case 2
                        If IsDept(retval.Product, sSegments(0), retval) Then
                            CType(retval.Product, DTODept).brand = oBrand
                        ElseIf IsCategory(retval.Product, sSegments(0), retval) Then
                            If IsSKU(retval.Product, sSegments(1), retval) Then
                            ElseIf IsLocation(retval.Product, sSegments(1), retval) Then
                            ElseIf IsTab(retval.Product, sSegments(1), retval) Then
                            ElseIf sSegments(1) = "compatibilidad" Then
                                retval.aux = sSegments(1)
                            End If
                        ElseIf IsTab(retval.Product, sSegments(0), retval) Then
                        ElseIf IsLocation(retval.Product, sSegments(0), retval) Then
                        ElseIf IsZona(retval.Product, sSegments(0), retval) Then
                        ElseIf sSegments(0).ToLower = "zip" Then
                            'ViewBag.pill = "SalePoints"
                            'ViewBag.areaKey = sSegments(1)
                        Else
                            retval = Nothing
                        End If
                    Case 3
                        If IsDept(retval.Product, sSegments(0), retval) Then
                            CType(retval.Product, DTODept).brand = oBrand
                        ElseIf IsCategory(oBrand, sSegments(0), retval) Then
                            If IsSKU(retval.Product, sSegments(1), retval) Then
                                If sSegments(2) = "imagen" Then
                                    retval.aux = "imagen"
                                ElseIf IsTab(retval.Product, sSegments(2), retval) Then
                                ElseIf IsLocation(retval.Product, sSegments(2), retval) Then
                                ElseIf IsZona(retval.Product, sSegments(2), retval) Then
                                ElseIf sSegments(0).ToLower = "zip" Then
                                    'ViewBag.pill = "SalePoints"
                                    'ViewBag.areaKey = sSegments(2)
                                End If
                            End If
                        Else
                            retval = Nothing
                        End If
                End Select
            End If

        End If
        Return retval
    End Function



    Protected Shared Function IsDept(oBrand As DTOProductBrand, src As String, ByRef oQuery As DTOProductPageQuery) As Boolean
        Dim retval As Boolean
        Dim exs As New List(Of Exception)
        Dim oDept = FEB2.Dept.FromNomSync(oBrand, src, exs)
        If oDept IsNot Nothing Then
            With oQuery
                .Product = oDept
                .Tab = DTOProduct.Tabs.general
            End With
            retval = True
        End If
        Return retval
    End Function

    Protected Shared Function IsCategory(oBrand As DTOProductBrand, src As String, ByRef oQuery As DTOProductPageQuery) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Dim oCategory = FEB2.ProductCategory.FromNomSync(exs, oBrand, src)
        If oCategory IsNot Nothing Then
            With oQuery
                .Product = oCategory
                .Tab = DTOProduct.Tabs.general
            End With
            retval = True
        End If
        Return retval
    End Function

    Protected Shared Function IsSKU(oProductCategory As DTOProduct, src As String, ByRef oQuery As DTOProductPageQuery) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Dim oSKU = FEB2.ProductSku.FromNomSync(exs, oProductCategory, src)
        If oSKU IsNot Nothing Then
            FEB2.ProductSku.Load(oSKU, exs)
            With oQuery
                .Product = oSKU
                .Tab = DTOProduct.Tabs.general
            End With
            retval = True
        End If
        Return retval
    End Function

    Protected Shared Function IsTab(oProduct As DTOProduct, src As String, ByRef oQuery As DTOProductPageQuery) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Dim oTab As DTOProduct.Tabs
        If [Enum].TryParse(src, oTab) Then
            retval = True
            oQuery.Tab = oTab
            If oTab = DTOProduct.Tabs.distribuidores Then
                Dim oBrand = FEB2.Product.Brand(exs, oProduct)
                FEB2.ProductBrand.Load(oBrand, exs)
                If oBrand.ShowAtlas Then
                    'oQuery.Location = BLL.BLLProductDistributors.BestLocation(oProduct)
                    'If oQuery.Location Is Nothing Then
                    'oQuery.Location = BLLApp.Org.Address.Zip.Location
                    'End If
                Else
                    oQuery.Tab = DTOProduct.Tabs.general
                End If
            End If
        End If
        Return retval
    End Function

    Protected Shared Function IsLocation(oProduct As DTOProduct, src As String, ByRef oQuery As DTOProductPageQuery) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Dim oBrand = FEB2.Product.Brand(exs, oProduct)
        FEB2.ProductBrand.Load(oBrand, exs)
        If oBrand.ShowAtlas Then
            Dim oLocation = FEB2.Location.FromNomSync(exs, src)
            If oLocation IsNot Nothing Then
                With oQuery
                    .Product = oProduct
                    .Tab = DTOProduct.Tabs.distribuidores
                    .Location = oLocation
                End With
                retval = True
            End If
        Else
            With oQuery
                .Product = oProduct
                .Tab = DTOProduct.Tabs.general
            End With
            'retval = True
        End If
        Return retval
    End Function

    Protected Shared Function IsZona(oProduct As DTOProduct, src As String, ByRef oQuery As DTOProductPageQuery) As Boolean
        Dim retval As Boolean
        Dim exs As New List(Of Exception)
        Dim oBrand = FEB2.Product.Brand(exs, oProduct)
        FEB2.ProductBrand.Load(oBrand, exs)
        If oBrand.ShowAtlas Then
            Dim oZona As DTOZona = FEB2.Zona.FromNomSync(exs, src)
            If oZona IsNot Nothing Then
                With oQuery
                    .Product = oProduct
                    .Tab = DTOProduct.Tabs.distribuidores
                    .Location = FEB2.ProductDistributors.BestLocationSync(exs, oProduct, oZona)
                End With
                retval = True
            End If
        Else
            With oQuery
                .Product = oProduct
                .Tab = DTOProduct.Tabs.general
            End With
            retval = True
        End If
        Return retval
    End Function


    Private Function IsDownload(oProduct As DTOProduct, src As String, ByRef oQuery As DTOProductPageQuery) As Boolean
        Dim retval As Boolean
        Dim oSrc As DTOProductDownload.Srcs
        If [Enum].TryParse(src, oSrc) Then
            'ViewBag.Src = oSrc
            ''=============================================================================================================================
            'retval = True
        End If
        Return retval
    End Function



End Class
