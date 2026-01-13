Public Class PriceListSupplier
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOPriceListSupplier)
        Return Await Api.Fetch(Of DTOPriceListSupplier)(exs, "PriceList_Supplier", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oPriceListSupplier As DTOPriceListSupplier) As Boolean
        If Not oPriceListSupplier.IsLoaded And Not oPriceListSupplier.IsNew Then
            Dim pPriceListSupplier = Api.FetchSync(Of DTOPriceListSupplier)(exs, "PriceList_Supplier", oPriceListSupplier.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPriceListSupplier)(pPriceListSupplier, oPriceListSupplier, exs)
                For Each item In oPriceListSupplier.Items
                    item.Parent = oPriceListSupplier
                Next
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOPriceListSupplier) As Task(Of Boolean)
        Dim retval As Boolean
        If Validate(exs, value) Then
            Dim serialized = Api.Serialize(value, exs)
            If exs.Count = 0 Then
                Dim oMultipart As New ApiHelper.MultipartHelper()
                oMultipart.AddStringContent("Serialized", serialized)
                If value.DocFile IsNot Nothing Then
                    oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                    oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
                End If
                retval = Await Api.Upload(oMultipart, exs, "PriceList_Supplier/upload")
            End If
        End If
        Return retval
    End Function

    Shared Function Validate(exs As List(Of Exception), oPriceList As DTOPriceListSupplier) As Boolean
        For Each oItem As DTOPriceListItem_Supplier In oPriceList.Items
            Try
                If oItem.Ref.Length > 20 Then
                    exs.Add(New Exception("la ref. '" & oItem.Ref & " no pot passar de 20 caracters"))
                End If

                If Not String.IsNullOrEmpty(oItem.EAN) Then
                    Dim sEan As String = oItem.EAN

                    If sEan.Length = 11 Then sEan = "0" & sEan
                    If sEan.Length = 12 Then sEan = "0" & sEan

                    If sEan.Length <> 0 And sEan.Length <> 13 Then
                        exs.Add(New Exception("el codi EAN '" & oItem.EAN & " no te 13 digits a " & oItem.Ref))
                    End If
                End If

                If oItem.Ref.Length > 20 Then
                    exs.Add(New Exception("la descripció de la ref. '" & oItem.Ref & " no pot pasar dels 50 caracters"))
                End If

            Catch ex As Exception
                exs.Add(New Exception(String.Format("Error de sistema al validar la ref.{0} (linia {1})", oItem.Ref, oPriceList.Items.IndexOf(oItem))))
                exs.Add(ex)
            End Try
        Next

        Return exs.Count = 0
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPriceListSupplier As DTOPriceListSupplier) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPriceListSupplier)(oPriceListSupplier, exs, "PriceList_Supplier")
    End Function
End Class

Public Class PriceListsSupplier
    Inherits _FeblBase

    Shared Async Function FromProveidor(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOPriceListSupplier))
        Return Await Api.Fetch(Of List(Of DTOPriceListSupplier))(exs, "PriceListSuppliers/FromProveidor", oProveidor.Guid.ToString())
    End Function

    Shared Async Function Costs(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "PriceListSuppliers/Costs", oProveidor.Guid.ToString())
    End Function

End Class
