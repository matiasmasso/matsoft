Public Class Xl_Tree_DeliveryPackages
    Inherits TreeView

    Private _Delivery As DTODelivery

    Private Enum Images
        shipment
        pallet
        package
        product
    End Enum

    Public Shadows Sub Load(oDelivery As DTODelivery)
        _Delivery = oDelivery
        MyBase.Nodes.Clear()

        Dim oImageList As New ImageList()
        oImageList.Images.Add(My.Resources.delivery_van)
        oImageList.Images.Add(My.Resources.pallet)
        oImageList.Images.Add(My.Resources.empty_box)
        oImageList.Images.Add(My.Resources.full_box)
        MyBase.ImageList = oImageList

        Dim oRootNode = RootNode(oDelivery)
        MyBase.Nodes.Add(oRootNode)
        oRootNode.Expand()

        For Each oPallet In oDelivery.Pallets
            Dim oPalletNode = PalletNode(oPallet)
            oRootNode.Nodes.Add(oPalletNode)
        Next

        For Each oPackage In oDelivery.Packages.OrderBy(Function(x) x.Num).ToList()
            Dim oPackageNode = PackageNode(oPackage)
            oRootNode.Nodes.Add(oPackageNode)
        Next

        SetContextMenu()
    End Sub

    Private Function PalletNode(oPallet As DTODelivery.Pallet) As TreeNode
        Dim caption = String.Format("Pallet {0}", oPallet.SSCC)
        Dim retval As New TreeNode(caption, Images.pallet, Images.pallet)
        retval.Tag = oPallet
        retval.Expand()
        For Each oPackage In oPallet.Packages
            Dim oPackageNode = PackageNode(oPackage)
            retval.Nodes.Add(oPackageNode)
            oPackageNode.Expand()
        Next
        Return retval
    End Function

    Private Function PackageNode(oPackage As DTODelivery.Package) As TreeNode
        Dim caption = String.Format("Bulto num.{0} matrícula {1}", oPackage.Num, oPackage.SSCC)
        Dim retval As New TreeNode(caption, Images.package, Images.package)
        retval.Expand()

        retval.Tag = oPackage
        For Each oItem In oPackage.Items
            Dim itemCaption As String = ""
            If oItem.DeliveryItem Is Nothing Then
                itemCaption = String.Format("Linia??: {0} x sku??", oItem.QtyInPackage)
            Else
                itemCaption = String.Format("Linia {0:000}: {1} x {2}", oItem.DeliveryItem.lin, oItem.QtyInPackage, oItem.DeliveryItem.sku.nomLlarg.Tradueix(Current.Session.Lang))
            End If
            Dim oItemNode As New TreeNode(itemCaption, Images.product, Images.product)
            oItemNode.Tag = oItem
            retval.Nodes.Add(oItemNode)
        Next
        Return retval
    End Function

    Private Function RootNode(oDelivery As DTODelivery) As TreeNode
        Dim expedition As String = ""
        Dim fch As Date = Nothing
        Dim iPallets As Integer
        Dim iPackages As Integer
        Dim fchErr As Boolean

        If oDelivery.Pallets.Count > 0 Then
            expedition = oDelivery.Pallets.First().Expedition
            Dim oFirstPallet = oDelivery.Pallets.First()
            Try
                fch = oDelivery.Pallets.First().Fch
            Catch ex As Exception
                fchErr = True
            End Try
            iPallets = oDelivery.Pallets.Count
        End If
        If oDelivery.Packages.Count > 0 Then
            expedition = oDelivery.Packages.First().Expedition
            fch = oDelivery.Packages.First().Fch
            iPackages = oDelivery.Packages.Count
        End If

        Dim caption As String = String.Format("Expedició {0} del {1:dd/MM/yy}", expedition, fch)
        If fch = Nothing Then
            If fchErr Then
                caption = String.Format("Expedició {0} (error en la data)", expedition)
            Else
                caption = String.Format("Expedició {0}", expedition)
            End If
        End If

        caption += String.Format(" unitats en albarà {0} unitats en bultos {1}", oDelivery.Items.Sum(Function(x) x.Qty), oDelivery.Packages.SelectMany(Function(x) x.Items).Sum(Function(x) x.QtyInPackage))
        'Dim caption As String = String.Format("Expedició {0}", expedition)
        Dim tmp As String = ""
        If iPallets > 0 And iPackages > 0 Then
            tmp = String.Format(" ({0} palets + {1} bultos)", iPallets, iPackages)
        ElseIf iPallets > 0 Then
            tmp = String.Format(" ({0} palets)", iPallets)
        ElseIf iPackages > 0 Then
            tmp = String.Format(" ({0} bultos)", iPackages)
        End If
        If tmp > "" Then caption = caption & tmp

        If oDelivery.Transportista IsNot Nothing Then
            caption = String.Format("{0} per {1}", caption, oDelivery.Transportista.Nom)
        End If
        If oDelivery.Tracking.isNotEmpty Then
            caption = String.Format("{0} tracking # {1} ", caption, oDelivery.Tracking)
        End If
        Dim retval As New TreeNode(caption, Images.shipment, Images.shipment)
        Return retval
    End Function

    Private Sub Xl_Tree_DeliveryPackages_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        setContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If TypeOf oNode.Tag Is DTODelivery.Package Then
                oContextMenu.Items.Clear()
            ElseIf TypeOf oNode.Tag Is DTODelivery.Package.Item Then
                Dim oMenu As New Menu_DeliveryItem(CType(oNode.Tag, DTODelivery.Package.Item).DeliveryItem)
                oContextMenu.Items.AddRange(oMenu.Range)
            Else
                Dim oRepProduct As DTORepProduct = oNode.Tag
                Dim oMenu As New Menu_Delivery({_Delivery}.ToList())
                oContextMenu.Items.AddRange(oMenu.Range)
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

End Class
