Public Class Xl_WortenProductCategories
    Inherits TreeView

    Private _Catalog As DTO.Integracions.Worten.Catalog
    Private _Locale As String = "es_ES"

    Public Shadows Sub Load(oCatalog As DTO.Integracions.Worten.Catalog)
        MyBase.Nodes.Clear()
        _Catalog = oCatalog
        AddChildNodes(MyBase.Nodes)
    End Sub

    Private Sub AddChildNodes(oNodes As TreeNodeCollection, Optional parent_code As String = "")
        Dim items = _Catalog.childHierarchies(parent_code, _Locale)
        For Each item In items
            Dim oNode = oNodes.Add(item.Caption(_Locale))
            AddChildNodes(oNode.Nodes, item.code)
        Next
    End Sub


End Class
