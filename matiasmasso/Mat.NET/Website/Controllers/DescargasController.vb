Public Class DescargasController
    Inherits _MatController

    '
    ' GET: /Descargas

    Function Index() As ActionResult
        Return View()
    End Function

    Async Function FromSrc(Src As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        ContextHelper.NavViewModel.ResetCustomMenu()
        Dim oSrc As DTOProductDownload.Srcs = SrcFromText(Src)
        Select Case oSrc
            Case DTOProductDownload.Srcs.catalogos, DTOProductDownload.Srcs.instrucciones, DTOProductDownload.Srcs.compatibilidad
                ViewBag.Title = Caption(oSrc)
                ViewBag.Canonical = DTOUrl.Factory(oSrc.ToString)
                Dim Model = Await FEB.ProductDownloads.ProductModels(exs, oSrc, ContextHelper.Lang)
                retval = View("Brands", Model)
        End Select

        Return retval
    End Function

    Async Function FromBrand(Src As String, Brand As String) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        ContextHelper.NavViewModel.ResetCustomMenu()
        Dim oSrc As DTOProductDownload.Srcs = SrcFromText(Src)
        Select Case oSrc
            Case DTOProductDownload.Srcs.Catalogos, DTOProductDownload.Srcs.Instrucciones, DTOProductDownload.Srcs.Compatibilidad
                Dim oBrand = Await FEB.ProductBrand.FromNom(exs, Website.GlobalVariables.Emp, Brand)
                If oBrand IsNot Nothing Then
                    FEB.ProductBrand.Load(oBrand, exs)
                    Dim sCaption As String = Caption(oSrc)
                    Dim oBreadCrumb As New DTOBreadCrumb(ContextHelper.Lang())
                    oBreadCrumb.AddItem(ContextHelper.Tradueix("productos", "productes", "products", "produtos"), "/")
                    oBreadCrumb.AddItem(ContextHelper.Tradueix("descargas", "descarregues", "downloads", "descargas"), "/")
                    oBreadCrumb.AddItem(sCaption, "/" & Src)
                    oBreadCrumb.AddItem(oBrand.Nom.Tradueix(ContextHelper.Lang()))
                    ViewBag.Breadcrumb = oBreadCrumb

                    Dim sTitle As String = String.Format(ContextHelper.Tradueix("{0} de {1}", "{0} de {1}", "{1} {0}"), sCaption, oBrand.Nom.Tradueix(ContextHelper.Lang()))

                    ViewBag.Title = sTitle
                    Dim oBrandCanonicalSegments = DTOUrlSegment.Collection.Factory(oBrand.UrlSegments)
                    Dim oBrandCanonicalLangText = oBrandCanonicalSegments.Canonical()
                    ViewBag.Canonical = DTOUrl.Factory(DTOLangText.Factory(oSrc.ToString), oBrandCanonicalLangText)
                    ViewBag.Src = oSrc

                    Dim oDownloads = Await FEB.ProductDownloads.FromProductOrChildren(exs, oBrand, False, True, oSrc)
                    If exs.Count = 0 Then
                        retval = View("Files", oDownloads)
                    Else
                        'retval = Await MyBase.ErrorResult(exs)
                    End If

                End If
        End Select

        Return retval
    End Function


    Private Function SrcFromText(SrcNom As String) As DTOProductDownload.Srcs
        Dim retval As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet
        [Enum].TryParse(SrcNom, True, retval)
        Return retval
    End Function

    Private Function Caption(oSrc As DTOProductDownload.Srcs) As String
        Dim retval As String = ""
        Select Case oSrc
            Case DTOProductDownload.Srcs.catalogos
                retval = ContextHelper.Tradueix("Catálogos de producto", "catalegs de producte", "product catalogs")
            Case DTOProductDownload.Srcs.instrucciones
                retval = ContextHelper.Tradueix("Manuales de instrucciones", "Manuals de instruccions", "user manuals", "Manuais de usuário")
            Case DTOProductDownload.Srcs.compatibilidad
                retval = ContextHelper.Tradueix("Listas de compatibilidad", "Llistats de compatibilitat", "Compatibility lists", "Listas de compatibilidade")
        End Select
        Return retval
    End Function

End Class