Public Class ProductDistributors
    Inherits _FeblBase

    Shared Async Function FromManufacturer(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOProductRetailer))
        Return Await Api.Fetch(Of List(Of DTOProductRetailer))(exs, "ProductDistributors/FromManufacturer", oProveidor.Guid.ToString())
    End Function

    Shared Async Function FromBrand(exs As List(Of Exception), oBrand As DTOProductBrand) As Task(Of List(Of DTOProductRetailer))
        Return Await Api.Fetch(Of List(Of DTOProductRetailer))(exs, "ProductDistributors/FromBrand", oBrand.Guid.ToString())
    End Function

    Shared Async Function FromUser(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "ProductDistributors/FromUser", oUser.Guid.ToString())
    End Function
    Shared Async Function FromRep(exs As List(Of Exception), oRep As DTORep) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "ProductDistributors/FromRep", oRep.Guid.ToString())
    End Function
    Shared Function FromRepSync(exs As List(Of Exception), oRep As DTORep) As List(Of DTOProductDistributor)
        Return Api.FetchSync(Of List(Of DTOProductDistributor))(exs, "ProductDistributors/FromRep", oRep.Guid.ToString())
    End Function

    Shared Async Function DistribuidorsOficials(exs As List(Of Exception), oUser As DTOUser, oBrand As DTOProductBrand, Optional oIncentiu As DTOIncentiu = Nothing) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "ProductDistributors/DistribuidorsOficials", oUser.Guid.ToString, oBrand.Guid.ToString, OpcionalGuid(oIncentiu))
    End Function

    Shared Async Function Zonas(exs As List(Of Exception), oProduct As DTOProduct, oCountry As DTOCountry) As Task(Of List(Of DTOZona))
        Return Await Api.Fetch(Of List(Of DTOZona))(exs, "ProductDistributors/Zonas", oProduct.Guid.ToString, oCountry.Guid.ToString())
    End Function

    Shared Async Function BestLocation(exs As List(Of Exception), oProduct As DTOProduct, Optional oParentArea As DTOArea = Nothing) As Task(Of DTOLocation)
        Return Await Api.Fetch(Of DTOLocation)(exs, "ProductDistributors/BestLocation", oProduct.Guid.ToString, OpcionalGuid(oParentArea))
    End Function
    Shared Function BestLocationSync(exs As List(Of Exception), oProduct As DTOProduct, Optional oParentArea As DTOArea = Nothing) As DTOLocation
        Return Api.FetchSync(Of DTOLocation)(exs, "ProductDistributors/BestLocation", oProduct.Guid.ToString, OpcionalGuid(oParentArea))
    End Function

    Shared Async Function PerChannel(exs As List(Of Exception), oUser As DTOUser, Optional iYear As Integer = 0) As Task(Of ExcelHelper.Book)
        Return Await Api.Fetch(Of ExcelHelper.Book)(exs, "ProductDistributors/PerChannel", oUser.Guid.ToString, iYear)
    End Function

    Shared Async Function ManufacturerCsv(oProveidor As DTOProveidor) As Task(Of String)
        Dim exs As New List(Of Exception)
        Dim sb As New System.Text.StringBuilder
        Dim oDistributors = Await FEB2.ProductDistributors.FromManufacturer(exs, oProveidor)
        For Each item As DTOProductRetailer In oDistributors
            sb.Append(TextHelper.VbChr(34) & item.Country & TextHelper.VbChr(34) & ";")
            sb.Append(TextHelper.VbChr(34) & item.Region & TextHelper.VbChr(34) & ";")
            sb.Append(TextHelper.VbChr(34) & item.Location & TextHelper.VbChr(34) & ";")
            sb.Append(TextHelper.VbChr(34) & item.Name & TextHelper.VbChr(34) & ";")
            sb.AppendLine(TextHelper.VbChr(34) & item.Address & TextHelper.VbChr(34))
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function UrlJSON(oBrand As DTOProductBrand, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Dox(AbsoluteUrl,
                                                    DTODocFile.Cods.JSONSalePoints,
                                                    "brand", oBrand.Guid.ToString())
        Return retval
    End Function
    Shared Function UrlXML(oBrand As DTOProductBrand, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Dox(AbsoluteUrl,
                                                    DTODocFile.Cods.XMLSalePoints,
                                                    "brand", oBrand.Guid.ToString())
        Return retval
    End Function

    Shared Function UrlStoreLocatorExcel(oProveidor As DTOProveidor, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Dox(AbsoluteUrl,
                                                    DTODocFile.Cods.ExcelSalePoints,
                                                    "proveidor", oProveidor.Guid.ToString())
        Return retval
    End Function

    Shared Function PerChannelUrl(iYear As Integer, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Dox(AbsoluteUrl,
                                                    DTODocFile.Cods.SellOutPerChannel,
                                                    "year", iYear)
        Return retval
    End Function

    Shared Function StoreLocatorAttachments(oProveidor As DTOProveidor, exs As List(Of Exception)) As ArrayList
        Dim retval As New ArrayList

        Dim sTemporaryPath As String = MatHelperStd.FileSystemHelper.TmpFolder & "\"
        Dim sUrl As String = UrlStoreLocatorExcel(oProveidor, True)
        Dim oMemoryStream As New IO.MemoryStream

        If FileSystemHelper.DownloadStream(sUrl, oMemoryStream, exs) Then
            Dim sfilename As String = String.Format("M+O Store locator {0:yyyy.MM.dd.HH.mm}", DateTime.Now)
            Dim sFilepath As String = FileSystemHelper.GetTmpFileName(MimeCods.Xlsx, sfilename)
            FileSystemHelper.SaveStream(oMemoryStream.ToArray, exs, sFilepath)
            retval.Add(sFilepath)
        End If

        Return retval
    End Function
End Class
