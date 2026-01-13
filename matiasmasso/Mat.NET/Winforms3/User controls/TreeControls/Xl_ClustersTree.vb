Public Class Xl_ClustersTree
    Inherits TreeView

    Private _Holding As DTOHolding
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oHolding As DTOHolding)
        _Holding = oHolding
        MyBase.Nodes.Clear()
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        MyBase.Nodes.Clear()

        Dim oNodeHolding = NodeHolding()
        MyBase.Nodes.Add(oNodeHolding)

        For Each oCluster In _Holding.Clusters
            Dim oNodeCluster = NodeCluster(oCluster)
            oNodeHolding.Nodes.Add(oNodeCluster)

            For Each oCustomer In oCluster.Customers
                oNodeCluster.Nodes.Add(NodeSalePoint(oCustomer))
            Next
        Next
        SetContextMenu()
    End Sub

    Private Async Function SetContextMenu() As Task
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If oNode.Tag IsNot Nothing Then
                If TypeOf oNode.Tag Is DTOHolding Then
                    Dim oHolding As DTOHolding = oNode.Tag
                    Dim oMenu_Holding As New Menu_Holding(oHolding)
                    AddHandler oMenu_Holding.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Holding.Range)
                    oContextMenu.Items.Add("Afegir Cluster", Nothing, AddressOf AddCluster)
                ElseIf TypeOf oNode.Tag Is DTOCustomerCluster Then
                    Dim oCluster As DTOCustomerCluster = oNode.Tag
                    Dim oMenu_Cluster As New Menu_CustomerCluster(oCluster)
                    AddHandler oMenu_Cluster.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Cluster.Range)
                ElseIf TypeOf oNode.Tag Is DTOCustomer Then
                    Dim oCustomer As DTOCustomer = oNode.Tag
                    Dim oContactMenu = Await FEB.ContactMenu.Find(exs, oCustomer)
                    Dim oMenu_Customer As New Menu_Contact(oCustomer, oContactMenu)
                    AddHandler oMenu_Customer.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Customer.Range)
                End If
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Function

    Private Sub AddCluster()
        Dim oCluster = DTOCustomerCluster.Factory(_Holding)
        Dim oFrm As New Frm_CustomerCluster(oCluster)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub Xl_ClustersTree_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        Await SetContextMenu()
    End Sub

    Private Function NodeHolding() As TreeNode
        Dim retval As New TreeNode(_Holding.Nom)
        retval.Expand()
        retval.Tag = _Holding
        Return retval
    End Function

    Private Function NodeCluster(oCluster As DTOCustomerCluster) As TreeNode
        Dim retval As New TreeNode(oCluster.Nom)
        retval.Tag = oCluster
        Return retval
    End Function

    Private Function NodeSalePoint(oCustomer As DTOCustomer) As TreeNode
        Dim retval As New TreeNode(oCustomer.Ref)
        retval.Tag = oCustomer
        Return retval
    End Function


End Class
