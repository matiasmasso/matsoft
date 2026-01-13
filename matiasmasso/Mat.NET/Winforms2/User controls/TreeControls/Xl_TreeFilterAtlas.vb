Public Class Xl_TreeFilterAtlas
    Inherits TreeView

    Private _AllItems As IEnumerable(Of DTOContact)
    Private _SelectedItems As IEnumerable(Of DTOContact)
    Private _SelectedZonas As New List(Of DTOAtlas.area)
    Private _SelectedLocations As New List(Of DTOAtlas.area)
    Private _SelectedContacts As New List(Of DTOAtlas.area)
    Private _PropertiesSet As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)


    Public Shadows Sub load(oAllItems As IEnumerable(Of DTOContact), oSelectedItems As IEnumerable(Of DTOContact))
        _SelectedItems = oSelectedItems
        _AllItems = oAllItems

        If Not _PropertiesSet Then SetProperties()
        If oAllItems Is Nothing Then
        ElseIf oAllItems.Count = 0 Then
        Else
            LoadNodes()
            CheckParentNodes()
        End If
    End Sub


    Public Function SelectedItems() As IEnumerable(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        For Each oCountryNode As TreeNode In MyBase.Nodes
            For Each oZonaNode As TreeNode In oCountryNode.Nodes
                For Each oLocationNode As TreeNode In oZonaNode.Nodes
                    For Each oContactNode As TreeNode In oLocationNode.Nodes
                        If oContactNode.Checked Then
                            retval.Add(oContactNode.Tag)
                        End If
                    Next
                Next
            Next
        Next
        Return retval
    End Function


    Private Sub LoadNodes()
        Dim oLang = Current.Session.Lang
        MyBase.Nodes.Clear()

        For Each oCountry In DTOContact.Countries(_AllItems)
            Dim oCountryNode = MyBase.Nodes.Add(oCountry.Guid.ToString, oCountry.LangNom.Tradueix(oLang))
            oCountryNode.Tag = oCountry
            For Each oZona In DTOContact.Zonas(_AllItems).Where(Function(x) x.Country.Equals(oCountry))
                Dim oZonaNode = oCountryNode.Nodes.Add(oZona.Guid.ToString, oZona.Nom)
                oZonaNode.Tag = oZona
                For Each oLocation In DTOContact.Locations(_AllItems).Where(Function(x) x.Zona.Equals(oZona))
                    Dim oLocationNode = oZonaNode.Nodes.Add(oLocation.Guid.ToString, oLocation.Nom)
                    oLocationNode.Tag = oLocation
                    For Each oContact In _AllItems.Where(Function(x) x.Address.Zip.Location.Equals(oLocation))
                        Dim oContactNode = oLocationNode.Nodes.Add(oContact.Guid.ToString, DTOCustomer.NomAndNomComercial(DTOCustomer.FromContact(oContact)))
                        oContactNode.Checked = _SelectedItems.Any(Function(x) x.Equals(oContact))
                        oContactNode.Tag = oContact
                    Next
                Next
            Next
        Next
    End Sub

    Private Sub CheckParentNodes()
        For Each oCountryNode As TreeNode In MyBase.Nodes
            oCountryNode.Checked = True
            For Each oZonaNode As TreeNode In oCountryNode.Nodes
                oZonaNode.Checked = True
                For Each oLocationNode As TreeNode In oZonaNode.Nodes
                    oLocationNode.Checked = True
                    For Each oContactNode As TreeNode In oLocationNode.Nodes
                        If Not oContactNode.Checked Then
                            oLocationNode.Checked = False
                            Exit For
                        End If
                    Next
                    If Not oLocationNode.Checked Then
                        oZonaNode.Checked = False
                        Exit For
                    End If
                Next
                If Not oZonaNode.Checked Then
                    oCountryNode.Checked = False
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub SetProperties()
        MyBase.CheckBoxes = True
        _PropertiesSet = True
    End Sub

    Private Sub CheckAllChildNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
        For Each node As TreeNode In treeNode.Nodes
            node.Checked = nodeChecked

            If node.Nodes.Count > 0 Then
                Me.CheckAllChildNodes(node, nodeChecked)
            End If
        Next
    End Sub

    Private Sub node_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles MyBase.AfterCheck
        If e.Action <> TreeViewAction.Unknown Then
            If e.Node.Nodes.Count > 0 Then
                Me.CheckAllChildNodes(e.Node, e.Node.Checked)
            End If
            RaiseEvent AfterUpdate(Me, New MatEventArgs(SelectedItems()))
        End If
    End Sub
End Class
