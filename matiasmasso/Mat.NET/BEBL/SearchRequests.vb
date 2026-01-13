Public Class SearchRequest

    Shared Function Load(ByRef oSearchRequest As DTOSearchRequest, exs As List(Of Exception)) As Boolean
        With oSearchRequest
            If .User IsNot Nothing Then
                Select Case .User.Rol.Id
                    Case DTORol.Ids.Rep, DTORol.Ids.comercial
                        .Contact = BEBL.User.GetRep(.User)
                End Select
            End If

            Try
                oSearchRequest = LangText.Search(oSearchRequest)

                Log(oSearchRequest)
            Catch ex As Exception
                exs.Add(ex)
            End Try

        End With

        Return (exs.Count = 0)
    End Function

    Shared Sub Log(oSearchRequest As DTOSearchRequest)
        If oSearchRequest.User IsNot Nothing Then
            If oSearchRequest.User.Rol.IsStaff Then
                Exit Sub
            End If
        End If
        SearchRequestLoader.Log(oSearchRequest)
    End Sub

    Shared Function FromCnap(oSearchRequest As DTOSearchRequest, exs As List(Of Exception)) As List(Of DTOSearchRequest.Result)
        Dim retval As New List(Of DTOSearchRequest.Result)

        'Dim oProducts = Await BEBL.Cnap.ProductSearch(oSearchRequest.SearchKey, exs)
        ' If exs.Count = 0 Then
        ' For Each oProduct In oProducts
        ' Dim oItem As New DTOSearchRequest.Result
        ' With oItem
        ' .Caption = oProduct.FullNom()
        ' .Url = FEBL.Product.Url(oProduct)
        ' If TypeOf oProduct Is DTOProductBrand Then
        ' .Cod = DTOSearchRequest.Result.Cods.Brand
        ' ElseIf TypeOf oProduct Is DTOProductCategory Then
        ' .Cod = DTOSearchRequest.Result.Cods.Category
        ' Else
        ' .Cod = DTOSearchRequest.Result.Cods.Sku
        ' End If
        ' End With
        ' retval.Add(oItem)
        'Next
        'End If
        Return retval
    End Function


End Class


Public Class SearchRequests
    Shared Function All(oEmp As DTOEmp) As List(Of DTOSearchRequest)
        Dim retval As List(Of DTOSearchRequest) = SearchRequestsLoader.All(oEmp)
        Return retval
    End Function

End Class