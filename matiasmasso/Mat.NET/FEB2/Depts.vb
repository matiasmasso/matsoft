Public Class Dept

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTODept)
        Return Await Api.Fetch(Of DTODept)(exs, "Dept", oGuid.ToString())
    End Function

    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid) As DTODept
        Return Api.FetchSync(Of DTODept)(exs, "Dept", oGuid.ToString())
    End Function

    Shared Async Function Content(exs As List(Of Exception), oDept As DTODept, oLang As DTOLang) As Threading.Tasks.Task(Of String)
        Dim oLangText = Await FEB2.LangText.Find(exs, oDept.Guid, DTOLangText.Srcs.ProductText)
        Dim retval As String = ""
        If oLangText IsNot Nothing Then
            retval = oLangText.Tradueix(oLang)
        End If
        Return retval
    End Function


    Shared Function FromNomSync(oBrand As DTOProductBrand, nom As String, exs As List(Of Exception)) As DTODept
        Return Api.FetchSync(Of DTODept)(exs, "Dept/fromNom", oBrand.Guid.ToString, nom)
    End Function

    Shared Function Load(ByRef oDept As DTODept, includeBanner As Boolean, exs As List(Of Exception)) As Boolean
        If Not oDept.IsLoaded And Not oDept.IsNew Then
            Dim pDept = Api.FetchSync(Of DTODept)(exs, "Dept", oDept.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTODept)(pDept, oDept, exs)
            End If
            If includeBanner Then
                oDept.Banner = Api.FetchImageSync(exs, "Dept/banner", oDept.Guid.ToString())
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Upload(oDept As DTODept, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(oDept, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("Banner", oDept.Banner)
            retval = Await Api.Upload(oMultipart, exs, "Dept")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oDept As DTODept, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTODept)(oDept, exs, "Dept")
    End Function

    Shared Function CategoriesSync(oDept As DTODept) As List(Of DTOProductCategory)
        Dim exs As New List(Of Exception)
        Return Api.FetchSync(Of List(Of DTOProductCategory))(exs, "Dept/categories", oDept.Guid.ToString())
    End Function

    Shared Function Url(oDept As DTODept, Optional oLang As DTOLang = Nothing, Optional AbsoluteUrl As Boolean = False) As String
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim sSegment = MatHelperStd.UrlHelper.EncodedUrlSegment(oDept.nom.Tradueix(oLang))
        Dim oDomain = DTOWebDomain.Default(AbsoluteUrl)
        Return oDomain.Url(DTOProductBrand.urlSegment(oDept.brand), sSegment)
    End Function

    Shared Function BannerUrl(oDept As DTODept, Optional AbsoluteUrl As Boolean = False) As String
        Return Api.Url("dept/banner", oDept.Guid.ToString())
        'Return UrlHelper.Image(DTO.Defaults.ImgTypes.DeptBanner, oDept.Guid, AbsoluteUrl)
    End Function
End Class

Public Class Depts

    Shared Async Function All(exs As List(Of Exception), Optional oBrand As DTOProductBrand = Nothing) As Task(Of List(Of DTODept))
        Dim retval As New List(Of DTODept)
        If oBrand Is Nothing Then
            retval = Await Api.Fetch(Of List(Of DTODept))(exs, "Depts")
        Else
            retval = Await Api.Fetch(Of List(Of DTODept))(exs, "Depts", oBrand.Guid.ToString)
        End If
        Return retval
    End Function


    Shared Async Function AllWithFilters(exs As List(Of Exception), oBrand As DTOProductBrand) As Task(Of List(Of DTODept))
        Dim retval = Await Api.Fetch(Of List(Of DTODept))(exs, "Depts/AllWithFilters", oBrand.Guid.ToString)
        For Each oDept In retval
            For Each oCategory In oDept.Categories
                oCategory.brand = oDept.brand
            Next
        Next
        Return retval
    End Function

    Shared Function AllSync() As List(Of DTODept)
        Dim exs As New List(Of Exception)
        Return Api.FetchSync(Of List(Of DTODept))(exs, "Depts")
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oBrand As DTOProductBrand) As Task(Of List(Of DTODept))
        Dim retval = Await Api.Fetch(Of List(Of DTODept))(exs, "Depts/headers", oBrand.Guid.ToString())
        For Each item In retval
            item.Brand = oBrand
        Next
        Return retval
    End Function

    Shared Async Function Swap(exs As List(Of Exception), dept1 As DTODept, dept2 As DTODept) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Depts/swap", dept1.Guid.ToString, dept2.Guid.ToString)
    End Function

End Class
