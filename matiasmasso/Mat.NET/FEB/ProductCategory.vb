Imports System.Drawing
Imports DTO.DTOFacebook.UserProfile
Imports Newtonsoft.Json.Linq

Public Class ProductCategory

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOProductCategory)
        Return Await Api.Fetch(Of DTOProductCategory)(exs, "ProductCategory", oGuid.ToString())
    End Function

    Shared Function FindSync(oGuid As Guid, exs As List(Of Exception)) As DTOProductCategory
        Return Api.FetchSync(Of DTOProductCategory)(exs, "ProductCategory", oGuid.ToString())
    End Function

    Shared Function FromNomSync(exs As List(Of Exception), oBrand As DTOProductBrand, nom As String) As DTOProductCategory
        Dim retval = Api.ExecuteSync(Of String, DTOProductCategory)(nom, exs, "ProductCategory/fromNom", oBrand.Guid.ToString())
        If retval IsNot Nothing Then
            retval.Brand = oBrand
        End If
        Return retval
    End Function

    Shared Async Function Thumbnail(exs As List(Of Exception), oCategory As DTOProductCategory) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "ProductCategory/thumbnail", oCategory.Guid.ToString())
    End Function

    Shared Async Function Image(exs As List(Of Exception), oCategory As DTOProductCategory) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "ProductCategory/image", oCategory.Guid.ToString())
    End Function

    Shared Function Load(ByRef oProductCategory As DTOProductCategory, exs As List(Of Exception), Optional includeThumbnail As Boolean = False) As Boolean
        If Not oProductCategory.IsLoaded And Not oProductCategory.IsNew Then
            Dim pProductCategory = Api.FetchSync(Of DTOProductCategory)(exs, "ProductCategory", oProductCategory.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProductCategory)(pProductCategory, oProductCategory, exs)
            End If
            If includeThumbnail Then
                oProductCategory.Thumbnail = Api.FetchImageSync(exs, "ProductCategory/image", oProductCategory.Guid.ToString())
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTOProductCategory, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Image IsNot Nothing Then
                oMultipart.AddFileContent("image", value.Image)
            End If
            retval = Await Api.Upload(oMultipart, exs, "ProductCategory")
        End If
        Return retval
    End Function

    Shared Async Function SortSkus(exs As List(Of Exception), oCategory As DTOProductCategory, oDictionary As Dictionary(Of Guid, Integer)) As Task(Of Boolean)
        Dim retval As Boolean = Await Api.Execute(Of Dictionary(Of Guid, Integer), Boolean)(oDictionary, exs, "productCategory/sortSkus", oCategory.Guid.ToString())
        Return retval
    End Function

    Shared Async Function Delete(oProductCategory As DTOProductCategory, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProductCategory)(oProductCategory, exs, "ProductCategory")
    End Function


    Shared Function Url(oCategory As DTOProductCategory, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False, Optional lang As DTOLang = Nothing) As String
        Dim retval As String = ""
        Dim oBrand As DTOProductBrand = oCategory.Brand
        If oBrand IsNot Nothing Then
            retval = MmoUrl.Factory(AbsoluteUrl, oCategory.UrlFullSegment(oTab, lang))
        End If
        Return retval
    End Function

    Shared Function ThumbnailUrl(oCategory As DTOProductCategory, Optional BlAbsoluteUrl As Boolean = False) As String
        Return ApiUrl("ProductCategory/thumbnail", oCategory.Guid.ToString())
    End Function

    Shared Function UrlBuilderForBlogComments(oCategory As DTOProductCategory, AbsoluteUrl As Boolean) As String
        Dim sTpaNom As String = oCategory.Brand.Nom.Esp
        Dim sStpNom As String = oCategory.Nom.Esp

        Dim sUrl As String = oCategory.GetUrl(DTOLang.ESP(), , AbsoluteUrl)
        Dim retval As String = "<a href='" & sUrl & "?utm_source=blog&utm_medium=comments&utm_campaign=BlogComments' title='" & sTpaNom & " " & sStpNom & "' target='_blank'>" & sTpaNom & " " & sStpNom & "</a>"
        Return retval
    End Function

    Shared Function UrlBuilderForEmails(oCategory As DTOProductCategory, AbsoluteUrl As Boolean) As String
        Dim sTpaNom As String = oCategory.Brand.Nom.Esp
        Dim sStpNom As String = oCategory.Nom.Esp

        Dim sUrl As String = oCategory.GetUrl(DTOLang.ESP(), , AbsoluteUrl)
        Dim retval As String = "<a href='" & sUrl & "?utm_source=email&utm_medium=correspondencia&utm_campaign=mailExchange' title='" & sTpaNom & " " & sStpNom & "' target='_blank'>" & sTpaNom & " " & sStpNom & "</a>"
        Return retval
    End Function
End Class


Public Class ProductCategories
    Inherits _FeblBase

    Shared Async Function Minified(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOProductCategory))
        Dim min = Await Api.Fetch(Of List(Of JObject))(exs, "productCategories/min", oEmp.Id)
        Dim retval = min.Select(Function(x) Models.Min.ProductCategory.Expand(x)).ToList()
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOProductCategory))
        Dim retval = Await Api.Fetch(Of List(Of DTOProductCategory))(exs, "productCategories", oEmp.Id)
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception),
                        oBrand As DTOProductBrand,
                        Optional oMgz As DTOMgz = Nothing,
                        Optional IncludeObsolets As Boolean = False,
                        Optional oSortOrder As DTOProductCategory.SortOrders = DTOProductCategory.SortOrders.Alfabetic,
                        Optional skipEmptyCategories As Boolean = False) As Task(Of List(Of DTOProductCategory))

        Return Await Api.Fetch(Of List(Of DTOProductCategory))(exs, "productCategories",
                                                               oBrand.Guid.ToString,
                                                               OpcionalGuid(oMgz),
                                                                OpcionalBool(IncludeObsolets),
                                                                oSortOrder,
                                                                OpcionalBool(skipEmptyCategories))
    End Function

    Shared Async Function All(exs As List(Of Exception), oDept As DTODept) As Task(Of List(Of DTOProductCategory))
        Return Await Api.Fetch(Of List(Of DTOProductCategory))(exs, "productCategories/fromDept", oDept.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOProductCategory))
        Return Await Api.Fetch(Of List(Of DTOProductCategory))(exs, "productCategories/fromCustomer", oCustomer.Guid.ToString())
    End Function

    Shared Async Function CompactTree(exs As List(Of Exception), oEmp As DTOEmp, oLang As DTOLang, Optional RebuildCirularReferences As Boolean = True) As Task(Of List(Of DTOProductBrand))
        Dim oCatalog As DTOCatalog = Await Api.Fetch(Of DTOCatalog)(exs, "productCategories/compactTree", oEmp.Id, oLang.Tag)
        Dim retval As New List(Of DTOProductBrand)
        If oCatalog IsNot Nothing Then
            For Each item In oCatalog
                Dim oBrand As New DTOProductBrand(item.Guid)
                oBrand.Nom.Esp = item.Nom
                oBrand.CodDist = item.CodDist
                oBrand.obsoleto = item.Obsoleto
                retval.Add(oBrand)
                For Each item2 In item.Categories
                    Dim oCategory As New DTOProductCategory(item2.Guid)
                    With oCategory
                        .Nom.Esp = item2.Nom
                        .obsoleto = item2.Obsoleto
                        .IsBundle = item2.IsBundle
                    End With
                    oBrand.Categories.Add(oCategory)
                Next
            Next
            If RebuildCirularReferences Then RebuildCircularReferences(retval, oEmp)
        End If
        Return retval
    End Function

    Shared Sub RebuildCircularReferences(oBrands As List(Of DTOProductBrand), oEmp As DTOEmp)
        For Each oBrand In oBrands
            oBrand.IsNew = False
            oBrand.Emp = oEmp
            For Each oCategory In oBrand.Categories
                oCategory.IsNew = False
                oCategory.Brand = oBrand
            Next
        Next
    End Sub


    Shared Async Function Move(exs As List(Of Exception), values As List(Of DTOProductCategory)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOProductCategory), Boolean)(values, exs, "productCategories/move")
    End Function

End Class