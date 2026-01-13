Public Class Xl_RepProductsTree
    Inherits TreeView

    Private _Values As List(Of DTORepProduct)
    Private _Lang As DTOLang
    Private _AllowPersist As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTORepProduct), Optional allowPersist As Boolean = True)
        _Lang = Current.Session.Lang
        _Values = values
        _AllowPersist = allowPersist
        refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTORepProduct)
        Get
            Return _Values
        End Get
    End Property

    Private Sub refresca()
        MyBase.Nodes.Clear()
        If _Values IsNot Nothing Then
            For Each oChannel As DTODistributionChannel In ChannelQuery()
                Dim oChannelNode As TreeNode = AddChannel(oChannel)
                oChannelNode.Expand()

                For Each areaItem In AreaQuery(oChannel)
                    Dim oAreaNode = AddArea(areaItem, oChannelNode)
                    For Each product As DTORepProduct In ProductQuery(oChannel, areaItem)
                        Dim oProductNode As TreeNode = AddProduct(product, oAreaNode)
                    Next
                Next
            Next
        End If
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If TypeOf oNode.Tag Is DTODistributionChannel Then
                Dim oChannel As DTODistributionChannel = oNode.Tag
                Dim oMenu As New Menu_DistributionChannel(oChannel)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
                oContextMenu.Items.Add("-")
                oContextMenu.Items.Add("clonar", Nothing, AddressOf Do_ClonChannel)
            ElseIf TypeOf oNode.Tag Is DTOArea Then
                Dim oArea As DTOArea = oNode.Tag
                Dim oMenu As New Menu_Area(oArea)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
                oContextMenu.Items.Add("-")
                oContextMenu.Items.Add("traspasar", Nothing, AddressOf Do_TraspasFullArea)
                oContextMenu.Items.Add("clonar", Nothing, AddressOf Do_ClonArea)
            ElseIf TypeOf oNode.Tag Is DTORepProduct Then
                Dim oRepProduct As DTORepProduct = oNode.Tag
                Dim oMenu As New Menu_RepProduct(oRepProduct)
                AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                oContextMenu.Items.AddRange(oMenu.Range)
                oContextMenu.Items.Add("-")
            End If
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ClonChannel()
        Dim oNode As TreeNode = MyBase.SelectedNode
        Dim oTag As DTODistributionChannel = oNode.Tag
        Dim oRepProducts As List(Of DTORepProduct) = _Values.Where(Function(x) x.DistributionChannel.Equals(oTag)).ToList
        Dim clones As New List(Of DTORepProduct)
        For Each item As DTORepProduct In oRepProducts
            Dim clon As New DTORepProduct
            With clon
                .Rep = item.Rep
                .Area = item.Area
                .Product = item.Product
                .FchFrom = item.FchFrom
                .FchTo = item.FchTo
                .Cod = item.Cod
                .ComStd = item.ComStd
                .ComRed = item.ComRed
            End With
            clones.Add(clon)
        Next

        Dim oFrm As New Frm_RepProduct(clones, _AllowPersist)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_TraspasFullArea()
        Dim items As New List(Of DTORepProduct)
        Dim oNode As TreeNode = MyBase.SelectedNode
        For Each oChildNode As TreeNode In oNode.Nodes
            Dim oRepProduct As DTORepProduct = oChildNode.Tag
            items.Add(oRepProduct)
        Next
        Dim oFrm As New Frm_RepTraspas(items)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ClonArea()
        Dim oNode As TreeNode = MyBase.SelectedNode
        Dim oChannel As DTODistributionChannel = oNode.Parent.Tag
        Dim oArea As DTOArea = oNode.Tag
        Dim oRepProducts As List(Of DTORepProduct) = _Values.Where(Function(x) x.DistributionChannel.Equals(oChannel) And x.Area.Equals(oArea)).ToList
        Dim clones As New List(Of DTORepProduct)
        For Each item As DTORepProduct In oRepProducts
            Dim clon As New DTORepProduct
            With clon
                .Rep = item.Rep
                .DistributionChannel = oChannel
                .Product = item.Product
                .FchFrom = item.FchFrom
                .FchTo = item.FchTo
                .Cod = item.Cod
                .ComStd = item.ComStd
                .ComRed = item.ComRed
            End With
            clones.Add(clon)
        Next

        Dim oFrm As New Frm_RepProduct(clones, _AllowPersist)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Public Function DistributionChannel() As DTODistributionChannel
        Dim retval As DTODistributionChannel = Nothing
        Dim oNode As TreeNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            Do
                If TypeOf oNode.Tag Is DTODistributionChannel Then
                    retval = oNode.Tag
                    Exit Do
                ElseIf TypeOf oNode.Tag Is DTORepProduct Then
                    Dim oRepProduct As DTORepProduct = oNode.Tag
                    retval = oRepProduct.DistributionChannel
                    Exit Do
                End If
                oNode = oNode.Parent
            Loop Until oNode Is Nothing
        End If

        Return retval
    End Function

    Public Function Area() As DTOArea
        Dim retval As DTOArea = Nothing
        Dim oNode As TreeNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            Do
                If TypeOf oNode.Tag Is DTOArea Then
                    retval = oNode.Tag
                    Exit Do
                ElseIf TypeOf oNode.Tag Is DTORepProduct Then
                    Dim oRepProduct As DTORepProduct = oNode.Tag
                    retval = oRepProduct.Area
                    Exit Do
                End If
                oNode = oNode.Parent
            Loop Until oNode Is Nothing
        End If
        Return retval
    End Function

    Public Function Product() As DTOProduct
        Dim retval As DTOProduct = Nothing
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            Do
                If TypeOf oNode.Tag Is DTOProduct Then
                    retval = oNode.Tag
                    Exit Do
                ElseIf TypeOf oNode.Tag Is DTORepProduct Then
                    Dim oRepProduct As DTORepProduct = oNode.Tag
                    retval = oRepProduct.Product
                    Exit Do
                End If
                oNode = oNode.Parent
            Loop Until oNode Is Nothing
        End If
        Return retval
    End Function


    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        If _AllowPersist Then
            RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
        Else
            _Values.Add(e.Argument)
            refresca()
        End If
    End Sub

    Private Function ChannelQuery() As List(Of DTODistributionChannel)
        Dim retval As New List(Of DTODistributionChannel)
        If _Values IsNot Nothing Then
            If _Values.Count > 0 Then
                retval = _Values.Where(Function(x) x.FchTo = Nothing Or x.FchTo >= DTO.GlobalVariables.Today()).
            GroupBy(Function(g) New With {Key g.DistributionChannel.Guid,
                                          Key g.DistributionChannel.LangText.Esp,
                                          Key g.DistributionChannel.LangText.Cat,
                                          Key g.DistributionChannel.LangText.Eng,
                                          Key g.DistributionChannel.LangText.Por
                                            }).
                Select(Function(group) New DTODistributionChannel With {
                    .Guid = group.Key.Guid,
                    .LangText = New DTOLangText(group.Key.Esp, group.Key.Cat, group.Key.Eng, group.Key.Por)
                           }).ToList
            End If
        End If
        Return retval
    End Function

    Private Function AreaQuery(oChannel As DTODistributionChannel) As List(Of DTOArea)
        Dim retval As New List(Of DTOArea)
        If _Values IsNot Nothing Then

            Dim Query = _Values.Where(Function(x) x.FchTo = Nothing Or x.FchTo >= DTO.GlobalVariables.Today()).
                Where(Function(x) x.DistributionChannel.Equals(oChannel)).
                GroupBy(Function(g) g.Area.Guid).
                Select(Function(group) group.First()).ToList

            Dim sortedAreas = Query.OrderBy(Function(x) DirectCast(x.area, DTOArea).nom).ToList
            For Each item In sortedAreas
                retval.Add(item.area)
            Next
        End If
        Return retval
    End Function

    Private Function ProductQuery(oChannel As DTODistributionChannel, oArea As DTOArea) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)
        If _Values IsNot Nothing Then
            Dim onArea = _Values.Where(Function(x) x.area.Guid.Equals(oArea.Guid)).ToList
            Dim onChannel = onArea.Where(Function(x) x.distributionChannel.Guid = oChannel.Guid).ToList
            Dim onRange = onChannel.Where(Function(x) x.FchTo = Nothing Or x.FchTo >= DTO.GlobalVariables.Today()).ToList

            retval = onRange.OrderBy(Function(x) DirectCast(x.product, DTOProduct).FullNom(Current.Session.Lang)).ToList
        End If
        Return retval
    End Function

    Private Function AddChannel(oChannel As DTODistributionChannel) As TreeNode
        Dim sCaption As String = oChannel.LangText.Tradueix(_Lang)
        Dim oNode As New TreeNode(sCaption)
        oNode.Tag = oChannel
        MyBase.Nodes.Add(oNode)
        Return oNode
    End Function

    Private Function AddArea(oArea As DTOArea, oParentNode As TreeNode) As TreeNode
        Dim sCaption As String = oArea.Nom
        Dim oNode As New TreeNode(sCaption)
        oNode.Tag = oArea
        oParentNode.Nodes.Add(oNode)
        Return oNode
    End Function

    Private Function AddProduct(item As DTORepProduct, oParentNode As TreeNode) As TreeNode
        Dim oProduct As DTOProduct = item.Product
        Dim sCaption As String = oProduct.FullNom()
        Dim oNode As New TreeNode(sCaption)
        oNode.Tag = item
        If item.FchFrom > DTO.GlobalVariables.Today() Then oNode.ForeColor = Color.Red
        oParentNode.Nodes.Add(oNode)
        Return oNode
    End Function

    Private Sub Xl_RepProductsTree_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        SetContextMenu()
    End Sub

    Private Sub Xl_RepProductsTree_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseDoubleClick
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If TypeOf oNode.Tag Is DTODistributionChannel Then
                Dim oChannel As DTODistributionChannel = oNode.Tag
                Dim oFrm As New Frm_DistributionChannel(oChannel)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            ElseIf TypeOf oNode.Tag Is DTOCountry Then
                Dim oCountry As DTOCountry = oNode.Tag
                Dim oFrm As New Frm_Country(oCountry)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            ElseIf TypeOf oNode.Tag Is DTOZona Then
                Dim oZona As DTOZona = oNode.Tag
                Dim oFrm As New Frm_Zona(oZona)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            ElseIf TypeOf oNode.Tag Is DTOLocation Then
                Dim oLocation As DTOLocation = oNode.Tag
                Dim oFrm As New Frm_Location(oLocation)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            ElseIf TypeOf oNode.Tag Is DTORepProduct Then
                Dim oRepProducts As New List(Of DTORepProduct)
                Dim oRepProduct As DTORepProduct = oNode.Tag
                oRepProducts.Add(oRepProduct)
                Dim oFrm As New Frm_RepProduct(oRepProducts, _AllowPersist)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
            End If
        End If

    End Sub
End Class
