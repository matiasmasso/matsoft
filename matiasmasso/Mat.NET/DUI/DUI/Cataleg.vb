Public Class CatalegResponse
    Property brands As List(Of DUI.Brand2)
    Property catalegHash As String
    Property skuGuidsHash As String

    Property changeCataleg As Boolean
    Property changeSprite As Boolean

End Class

Public Class CatalegRequest
    Property user As Guidnom2

    'reveals if any data has changed at all since last upload
    Property catalegHash As String

    'reveals if product skus are the same since last upload in order to update product images
    Property skuGuidsHash As String

    'reveals last upload date so we know if should refresh product images
    Property lastFch As Date

End Class
