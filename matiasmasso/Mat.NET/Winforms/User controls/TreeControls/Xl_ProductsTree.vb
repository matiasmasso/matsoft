Public Class Xl_ProductsTree
    Inherits TreeView

    Private _IncludeObsolets As Boolean

    Public Shadows Sub Load(oTree As List(Of DTOProductBrand), Optional oDefaultProduct As DTOProduct = Nothing)
        MyBase.SelectedNode = Nothing
        For Each oBrand As DTOProductBrand In oTree
            If _IncludeObsolets Or Not oBrand.obsoleto Then
                Dim oNode As TreeNode = GetNode(oBrand, oDefaultProduct)
                MyBase.Nodes.Add(oNode)
                If oBrand.Equals(oDefaultProduct) Then MyBase.SelectedNode = oNode
            End If
        Next
    End Sub

    Public Function SelectedValue() As DTOProduct
        Dim retval As DTOProduct = Nothing
        Dim oNode As TreeNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            retval = oNode.Tag
        End If
        Return retval
    End Function

    Private Function GetNode(oBrand As DTOProductBrand, oDefaultProduct As DTOProduct) As TreeNode
        Dim retval As New TreeNode(oBrand.Nom.Tradueix(Current.Session.Lang))
        retval.Tag = oBrand
        For Each oCategory As DTOProductCategory In oBrand.Categories
            If _IncludeObsolets Or Not oCategory.obsoleto Then
                Dim oNode As TreeNode = GetNode(oCategory, oDefaultProduct)
                retval.Nodes.Add(oNode)
                If oCategory.Equals(oDefaultProduct) Then MyBase.SelectedNode = oNode
            End If
        Next
        Return retval
    End Function

    Private Function GetNode(oCategory As DTOProductCategory, oDefaultProduct As DTOProduct) As TreeNode
        Dim retval As New TreeNode(oCategory.Nom.Tradueix(Current.Session.Lang))
        retval.Tag = oCategory
        For Each oSku As DTOProductSku In oCategory.Skus
            If _IncludeObsolets Or Not oSku.obsoleto Then
                Dim oNode As TreeNode = GetNode(oSku)
                retval.Nodes.Add(oNode)
                If oSku.Equals(oDefaultProduct) Then MyBase.SelectedNode = oNode
            End If
        Next
        Return retval
    End Function

    Private Function GetNode(oSku As DTOProductSku) As TreeNode
        Dim retval As New TreeNode(oSku.Nom.Tradueix(Current.Session.Lang))
        retval.Tag = oSku
        Return retval
    End Function

End Class
