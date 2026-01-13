Imports LegacyHelper

Public Class PluginController
    Inherits _MatController

    Async Function SkuColors(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oCategory As New DTOProductCategory(id)
        Dim oSkus As List(Of DTOProductSku) = Await FEB.ProductSkus.All(exs, oCategory, MyBase.Lang)
        If exs.Count = 0 Then
            Dim url = oCategory.SpriteSkuColorsUrl(DTOProductPlugin.ItemWidth, DTOProductPlugin.ItemHeight)
            Dim oSprite = MatHelperStd.SpriteHelper.Factory(url, DTOProductPlugin.ItemWidth, DTOProductPlugin.ItemHeight)
            For Each item In oSkus
                oSprite.addItem(item.Nom.Tradueix(ContextHelper.Lang), item.GetUrl(ContextHelper.Lang))
            Next

            Return View("HScrollPlugin", oSprite)
        Else
            Return View("error")
        End If
    End Function

    Async Function Skus(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oPlugin = Await FEB.ProductPlugin.Find(exs, id)
        'oPlugin.items = oPlugin.items.Where(Function(x) x.product.obsoleto = False).ToList
        If exs.Count = 0 Then
            Dim Model = oPlugin.Sprite(ContextHelper.Lang())
            Return View("HScrollPlugin", Model)
        Else
            Return Await MyBase.PluginErrorResult(exs)
        End If
    End Function

End Class
