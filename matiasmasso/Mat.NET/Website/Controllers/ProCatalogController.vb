Imports MatHelperStd

Public Class ProCatalogController
    Inherits _MatController

    Public Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        If exs.Count = 0 Then
            If oUser Is Nothing Then
                retval = MyBase.UnauthorizedView()
            Else
                Select Case oUser.Rol.id
                    Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                        ViewBag.Title = ContextHelper.Lang.Tradueix("Catálogo", "Catàleg", "Catalogue")
                        ViewBag.SideMenuItems = MyBase.SideMenuItems()
                        retval = View("Catalog")
                    Case Else
                        retval = MyBase.UnauthorizedView()
                End Select
            End If
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Public Async Function Search(searchKey As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oEmp = Website.GlobalVariables.Emp
        Dim oSkus = Await FEB.ProductSkus.SimpleSearch(exs, searchKey, oEmp)
        ViewBag.Title = ContextHelper.Lang.Tradueix("Catálogo", "Catàleg", "Catalogue")
        ViewBag.SideMenuItems = MyBase.SideMenuItems()
        ViewBag.SearchKey = searchKey
        Return View(oSkus)
    End Function

    Public Async Function Tree() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim retval As JsonResult = Nothing
        Dim values = Await FEB.ProductCategories.CompactTree(exs, Website.GlobalVariables.Emp, ContextHelper.Lang, RebuildCirularReferences:=False)
        values = values.Where(Function(x) x.obsoleto = False).ToList()
        If exs.Count = 0 Then
            retval = Json(New With {Key .success = True, Key .data = values}, JsonRequestBehavior.AllowGet)
        Else
            retval = Json(New With {Key .success = False, Key .message = ExceptionsHelper.ToFlatString(exs)}, JsonRequestBehavior.AllowGet)
        End If

        Return retval
    End Function

    Public Async Function Skus(id As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim retval As JsonResult = Nothing
        Dim oCategory As New DTOProductCategory(id)
        Dim oMgz = Website.GlobalVariables.Emp.Mgz
        Dim values = Await FEB.ProductSkus.All(exs, oCategory, MyBase.Lang, oMgz)
        If exs.Count = 0 Then
            retval = Json(New With {Key .success = True, Key .data = values}, JsonRequestBehavior.AllowGet)
        Else
            retval = Json(New With {Key .success = False, Key .message = ExceptionsHelper.ToFlatString(exs)}, JsonRequestBehavior.AllowGet)
        End If

        Return retval
    End Function

    Public Async Function Sku(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oSku = Await FEB.ProductSku.Find(exs, id)
        If exs.Count = 0 Then
            If oSku Is Nothing Then
                Return Await ErrorNotFoundResult()
            Else
                ViewBag.Title = oSku.FullNom(ContextHelper.Lang)
                ViewBag.SideMenuItems = MyBase.SideMenuItems()
                Return View(oSku.ViewModel(ContextHelper.Lang))
            End If
        Else
            Return Await ErrorResult(exs)
        End If
    End Function

    'Public Async Function ResourceUpload(id As Guid) As Threading.Tasks.Task(Of ActionResult)
    'Dim exs As New List(Of Exception)
    'Dim retval As ActionResult = Nothing
    'Dim oProduct = Await FEB.Product.Find(exs, id)
    'If exs.Count = 0 Then
    'If oProduct Is Nothing Then
    'Return Await ErrorNotFoundResult()
    'Else
    'Dim oMediaResource As New DTOMediaResource()
    '           oMediaResource.Product = oProduct
    'Dim model = oMediaResource.ViewModel
    '           ViewBag.Title = oProduct.FullNom(ContextHelper.Lang)
    '          ViewBag.SideMenuItems = MyBase.SideMenuItems()
    'Return View(model)
    'End If
    'Else
    'Return Await ErrorResult(exs)
    'End If
    'End Function

    <HttpPost>
    Public Async Function ResourceUpload() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oFile As HttpPostedFileBase = Request.Files(0)
        Dim oByteArray As Byte()
        Using binaryReader As New System.IO.BinaryReader(oFile.InputStream)
            oByteArray = binaryReader.ReadBytes(oFile.ContentLength)
        End Using

        Dim lang = Request.Form("Lang")
        Dim productGuid As New Guid(Request.Form("ProductGuid"))
        Dim oGuid As New Guid(Request.Form("Guid"))
        Dim sCod = Request.Form("Cod")

        Dim sFilename = oFile.FileName
        Dim oDocfile As New DTODocFile
        If LegacyHelper.DocfileHelper.LoadFromStream(oByteArray, oDocfile, exs) Then
            Dim oMediaResource = New DTOMediaResource(MyBase.User, oByteArray)
            With oMediaResource
                If sFilename = "" Then
                    .Mime = MimeHelper.GuessMime(oByteArray)
                Else
                    .Mime = MimeHelper.GetMimeFromExtension(sFilename)
                    .Nom = sFilename
                End If
                .Width = oDocfile.Size.Width
                .Height = oDocfile.Size.Height
                .HRes = oDocfile.HRes
                .VRes = oDocfile.VRes
                .Thumbnail = oDocfile.Thumbnail
                .Cod = CInt(sCod)
                If Not String.IsNullOrEmpty(lang) Then
                    .Lang = DTOLang.Factory(lang)
                End If
            End With

            Dim url = String.Format("~/Recursos/{0}", DTOMediaResource.TargetFilename(oMediaResource))
            Dim path As String = Server.MapPath(url)


            If MatHelperStd.FileSystemHelper.SaveStream(oMediaResource.Stream, exs, path) Then
                If Await FEB.MediaResource.Update(oMediaResource, exs) Then
                    retval = Await Index()
                Else
                    retval = Await ErrorResult(exs)
                End If
            Else
                retval = Await ErrorResult(exs)
            End If
        End If

        retval = Await Index()
        Return retval
    End Function

End Class
