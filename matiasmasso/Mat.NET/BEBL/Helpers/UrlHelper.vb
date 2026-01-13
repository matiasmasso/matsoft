Public Class UrlHelper

    'Public Const URLAPI As String = "https://matiasmasso-api.azurewebsites.net/"
    Public Const URLAPI As String = "https://api.matiasmasso.es/"

    Shared Function ApiUrl(ByVal ParamArray UrlSegments() As String) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(URLAPI)

        For i As Integer = 0 To UBound(UrlSegments)
            Dim sSegment As String = UrlSegments(i).Trim
            If Not sb.ToString.EndsWith("/") Then sb.Append("/")
            If sSegment.StartsWith("/") Then sSegment = sSegment.Substring(1)
            sb.Append(sSegment)
        Next i

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function Image(ByVal oType As DTO.Defaults.ImgTypes, ByVal oGuid As Guid, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Dim retval As String = Factory(AbsoluteUrl, "img", CInt(oType).ToString, oGuid.ToString())
        Return retval
    End Function

    Shared Function Image(ByVal oType As DTO.Defaults.ImgTypes, sHash As String, Optional ByVal AbsoluteUrl As Boolean = False) As String
        Dim retval As String = FromSegments(AbsoluteUrl, "img", CInt(oType).ToString, sHash) ' BaseGuid.GetBase64FromGuid(oGuid))
        Return retval
    End Function

    Shared Function Factory(AbsoluteUrl As Boolean, ByVal ParamArray UrlSegments() As String) As String
        Return DTOWebDomain.Default(AbsoluteUrl).Url(UrlSegments)
    End Function

    Shared Function LandingPage(oSku As DTOProductSku, oLang As DTOLang, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        Dim oCategory As DTOProductCategory = oSku.Category
        If oCategory Is Nothing Then
            retval = FromSegments(AbsoluteUrl, "sku", oSku.Guid.ToString())
        Else
            Dim oBrand As DTOProductBrand = oCategory.Brand
            If oBrand IsNot Nothing Then
                retval = FromSegments(AbsoluteUrl, DTOProductBrand.urlSegment(oBrand), DTOProductCategory.urlSegment(oCategory), DTOProductSku.urlSegment(oSku))
            End If
        End If

        If oTab <> DTOProduct.Tabs.general Then
            retval = retval & "/" & DTOProduct.TabUrlSegment(oTab).Tradueix(oLang)
        End If

        Return retval
    End Function

    Shared Function LandingPage(oCategory As DTOProductCategory, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        Dim oBrand As DTOProductBrand = oCategory.brand
        If oBrand IsNot Nothing Then
            retval = FromSegments(AbsoluteUrl, DTOProductBrand.urlSegment(oBrand), DTOProductCategory.urlSegment(oCategory))
        End If

        If oTab <> DTOProduct.Tabs.general Then
            retval = retval & "/" & oTab.ToString
        End If

        Return retval
    End Function



    Shared Function Dox(AbsoluteUrl As Boolean, doxcod As DTODocFile.Cods, ParamArray oParams() As String) As String
        Dim oParamsDictionary As New Dictionary(Of String, String)
        oParamsDictionary.Add("dox", doxcod)
        For i As Integer = 0 To oParams.Length - 2 Step 2
            oParamsDictionary.Add(oParams(i), oParams(i + 1))
        Next

        Dim sUrlFriendlyBase64Json As String = MatHelperStd.CryptoHelper.UrlFriendlyBase64Json(oParamsDictionary)
        Dim retval As String = DTOWebDomain.Default(AbsoluteUrl).Url("dox/" & sUrlFriendlyBase64Json)
        Return retval
    End Function


    Shared Function Doc(oCod As DTODocFile.Cods, oParameters As Dictionary(Of String, String), Optional AbsoluteUrl As Boolean = False) As String
        Dim sBase64Json As String = MatHelperStd.CryptoHelper.UrlFriendlyBase64Json(oParameters)
        Dim retval As String = FromSegments(AbsoluteUrl, "doc", CInt(oCod), sBase64Json)
        Return retval
    End Function

    Shared Function Doc(AbsoluteUrl As Boolean, oCod As DTODocFile.Cods, ParamArray params() As String) As String
        Dim oParams As New List(Of String)
        oParams.Add("doc")
        oParams.Add(oCod)
        oParams.AddRange(params)
        Dim retval As String = FromSegments(AbsoluteUrl, oParams.ToArray)
        Return retval
    End Function

    Shared Function Doc(oPurchaseOrder As DTOPurchaseOrder)
        Dim retval As String = ""
        If oPurchaseOrder IsNot Nothing Then
            retval = Doc(oPurchaseOrder.docFile)
        End If
        Return retval
    End Function

    Shared Function Doc(oDelivery As DTODelivery)
        Dim retval As String = FromSegments(True, "doc", CInt(DTODocFile.Cods.alb), oDelivery.Guid.ToString())
        Return retval
    End Function
    Shared Function Doc(oInvoice As DTOInvoice)
        Dim retval As String = ""
        If oInvoice IsNot Nothing Then
            retval = Doc(oInvoice.docFile)
        End If
        Return retval
    End Function

    Shared Function Doc(oDocfile As DTODocFile)
        Dim retval As String = ""
        If oDocfile IsNot Nothing Then
            retval = FromSegments(True, "doc", DTODocFile.Cods.download, CryptoHelper.StringToHexadecimal(oDocfile.hash))
        End If
        Return retval
    End Function

    Shared Function FromSegments(AbsoluteUrl As Boolean, ByVal ParamArray UrlSegments() As String) As String
        Dim retval As String = DTOWebDomain.Default(AbsoluteUrl).Url(UrlSegments)
        Return retval
    End Function


End Class

