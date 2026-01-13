

Public Class Xl_Pdc_Sortides
    Private mPdc As Pdc
    Private mRootNode As TreeNodeObj

    Private Enum NodeCods
        Pdc
        Pnc
        Arc
    End Enum

    Public WriteOnly Property Pdc() As pdc
        Set(ByVal value As Pdc)
            mPdc = value
            LoadTree()
        End Set
    End Property

    Private Sub LoadTree()
        mRootNode = New TreeNodeObj("comanda " & mPdc.Id & " del " & mPdc.Fch.ToShortDateString, mPdc, NodeCods.Pdc)
        TreeView1.Nodes.Add(mRootNode)
        For Each oItem As LineItmPnc In mPdc.Itms
            mRootNode.Nodes.Add(GetPncNode(oItem))
        Next
        mRootNode.ExpandAll()
    End Sub

    Private Function GetPncNode(ByVal oItem As LineItmPnc) As TreeNodeObj
        Dim s As String = oItem.Qty.ToString & " x " & oItem.Sku.NomLlarg
        Dim oNode As New TreeNodeObj(s, oItem, NodeCods.Pnc)

        For Each oArc As LineItmArc In oItem.Arcs
            s = oArc.Qty & " x " & oArc.Preu.CurFormatted & " alb." & oArc.Alb.Id & " del " & oArc.Alb.Fch.ToShortDateString
            If oArc.Alb.Invoice IsNot Nothing Then
                BLLInvoice.Load(oArc.Alb.Invoice)
                s = s & " (fra." & oArc.Alb.Invoice.Num & ")"
            End If
            Dim oArcNode As New TreeNodeObj(s, oArc, NodeCods.Arc)
            oNode.Nodes.Add(oArcNode)
        Next
        Return oNode
    End Function

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Dim oNode As TreeNodeObj = DirectCast(e.Node, TreeNodeObj)
        PropertyGrid1.SelectedObject = oNode.Obj
        SetContextMenu(oNode)
    End Sub

    Private Sub SetContextMenu(ByVal oNode As TreeNodeObj)
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Select Case oNode.Cod
            Case NodeCods.Pdc
                Dim oMenu_Pdc As New Menu_Pdc(CType(oNode.Obj, Pdc))
                oContextMenu.Items.AddRange(oMenu_Pdc.Range)
            Case NodeCods.Pnc
                Dim oItm As LineItmPnc = oNode.Obj
                If oItm.Sku IsNot Nothing Then
                    Dim oMenu_Sku As New Menu_ProductSku(oItm.Sku)
                    oMenuItem = New ToolStripMenuItem("article...")
                    oMenuItem.DropDownItems.AddRange(oMenu_Sku.Range)
                    oContextMenu.Items.Add(oMenuItem)

                    oMenuItem = New ToolStripMenuItem("representant")
                    oContextMenu.Items.Add(oMenuItem)
                    Dim s As String = ""
                    If oItm.RepCom IsNot Nothing Then
                        s = oItm.RepCom.Rep.NickName
                    End If
                    oMenuItem.DropDownItems.Add(New ToolStripMenuItem(s))
                End If
            Case NodeCods.Arc
                Dim oItm As LineItmArc = oNode.Obj
                If oItm.Sku IsNot Nothing Then
                    Dim oMenu_Sku As New Menu_ProductSku(oItm.Sku)
                    oMenuItem = New ToolStripMenuItem("article...")
                    oMenuItem.DropDownItems.AddRange(oMenu_Sku.Range)
                    oContextMenu.Items.Add(oMenuItem)

                    oMenuItem = New ToolStripMenuItem("representant")
                    oContextMenu.Items.Add(oMenuItem)
                    Dim s As String = ""
                    If oItm.RepCom Is Nothing Then
                        s = "sense representant"
                        oMenuItem.DropDownItems.Add(New ToolStripMenuItem(s))
                    Else
                        s = oItm.RepCom.Rep.NickName
                        oMenuItem.DropDownItems.Add(New ToolStripMenuItem(s))
                        If oItm.RepCom.Rep.ComisionStandard > 0 Then
                            Dim oFra As DTOInvoice = oItm.Alb.Invoice
                            If oFra Is Nothing Then
                                s = "pendent de facturar"
                                oMenuItem.DropDownItems.Add(New ToolStripMenuItem(s))
                            Else
                                Dim oInvoice As New DTOInvoice(oFra.Guid)
                                Dim oRep As New DTORep(oItm.RepCom.Rep.Guid)
                                Dim oRepLiq As DTORepLiq = BLLRepLiq.Find(oRep, oInvoice)
                                If oRepLiq Is Nothing Then
                                    s = oItm.Alb.Invoice.Fpg
                                    oMenuItem.DropDownItems.Add(New ToolStripMenuItem(s, Nothing, AddressOf ShowFra))
                                Else
                                    s = "liquidació " & oRepLiq.Id & " del " & oRepLiq.Fch.ToShortDateString
                                    oMenuItem.DropDownItems.Add(New ToolStripMenuItem(s, Nothing, AddressOf ShowRepLiq))
                                End If
                            End If

                        End If
                    End If

                End If
        End Select

        TreeView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ShowRepLiq(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oNode As TreeNodeObj = TreeView1.SelectedNode
        Dim oLineItmArc As LineItmArc = oNode.Obj

        Dim oInvoice = oLineItmArc.Alb.Invoice
        Dim oRep As New DTORep(oLineItmArc.RepCom.Rep.Guid)
        Dim oRepLiq As DTORepLiq = BLLRepLiq.Find(oRep, oInvoice)

        Dim oFrm As New Frm_RepLiq(oRepLiq)
        oFrm.Show()
    End Sub

    Private Sub ShowFra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oNode As TreeNodeObj = TreeView1.SelectedNode
        Dim oLineItmArc As LineItmArc = oNode.Obj
        'root.ShowFra(oLineItmArc.Alb.Fra)
        Dim oInvoice = oLineItmArc.Alb.Invoice
        Dim oFrm As New Frm_Invoice(oInvoice)
        oFrm.Show()

    End Sub
End Class
