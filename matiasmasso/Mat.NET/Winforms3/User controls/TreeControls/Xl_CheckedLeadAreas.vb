Public Class Xl_CheckedLeadAreas
    Inherits Xl_CheckedTreeView

    Private _CheckedLocations As List(Of DTOLeadAreas.Location)
    Private _AllowEvents As Boolean
    Private _ContactClasses As DTOContactClass.Collection
    Public Property isDirty As Boolean

    Public Shadows Sub Load(oLeadAreas As DTOLeadAreas, oContactClasses As DTOContactClass.Collection, Optional oCheckedLocations As List(Of DTOLeadAreas.Location) = Nothing)
        _ContactClasses = oContactClasses
        LoadSource(oLeadAreas, oCheckedLocations)
    End Sub

    Public Shadows Sub Load(oLeadAreas As DTOLeadAreas, Optional oCheckedLocations As List(Of DTOLeadAreas.Location) = Nothing)
        LoadSource(oLeadAreas, oCheckedLocations)
    End Sub

    Private Sub LoadSource(oLeadAreas As DTOLeadAreas, Optional oCheckedLocations As List(Of DTOLeadAreas.Location) = Nothing)
        MyBase.CheckBoxes = True

        If oCheckedLocations Is Nothing Then
            _CheckedLocations = Me.CheckedLocations()
        Else
            _CheckedLocations = oCheckedLocations
        End If

        Dim oTopNodeTag As Object = MyBase.TopNodeTag()
        Me.SuspendLayout()

        MyBase.Nodes.Clear()
        For Each oCountry In oLeadAreas.Countries
            Dim oCountryNode As New TreeNode()
            oCountryNode.Tag = oCountry
            For Each oZona In oCountry.Zonas
                Dim oZonaNode As New TreeNode()
                oZonaNode.Tag = oZona
                For Each oLocation In oZona.Locations
                    Dim oLeads = FilteredLeads(oLocation)
                    If oLeads.Count > 0 Then
                        Dim oLocationNode As New TreeNode()
                        oLocationNode.Checked = _CheckedLocations.Any(Function(x) x.Guid.Equals(oLocation.Guid))
                        oLocationNode.Tag = oLocation
                        oLocationNode.Text = String.Format("{0} ({1:#,0})", oLocation.Nom, oLeads.Count)
                        If MyBase.ExpandedTags.Contains(oLocation) Then oLocationNode.Expand()
                        If oLocation.Equals(oTopNodeTag) Then MyBase.TopNode = oLocationNode
                        oZonaNode.Nodes.Add(oLocationNode)
                    End If
                Next
                If oZonaNode.Nodes.Count > 0 Then
                    oZonaNode.Tag = oZona
                    oZonaNode.Text = String.Format("{0} ({1:#,0})", oZona.Nom, FilteredLeads(oZona).Count)
                    If MyBase.ExpandedTags.Contains(oZona) Then oZonaNode.Expand()
                    If oZona.Equals(oTopNodeTag) Then MyBase.TopNode = oZonaNode
                    oCountryNode.Nodes.Add(oZonaNode)
                End If
            Next
            If oCountryNode.Nodes.Count > 0 Then
                oCountryNode.Tag = oCountry
                oCountryNode.Text = oCountry.Nom
                If MyBase.ExpandedTags.Contains(oCountry) Then oCountryNode.Expand()
                If oCountry.Equals(oTopNodeTag) Then MyBase.TopNode = oCountryNode
                MyBase.Nodes.Add(oCountryNode)
            End If
        Next

        Me.ResumeLayout()
        _AllowEvents = True

    End Sub

    Private Function FilteredLeads(oLocation As DTOLeadAreas.Location) As List(Of DTOLeadAreas.Lead)
        Dim retval As List(Of DTOLeadAreas.Lead) = oLocation.Leads
        If _ContactClasses IsNot Nothing Then
            retval = retval.Where(Function(x) _ContactClasses.Any(Function(y) y.Guid.Equals(x.ContactClass))).ToList()
        End If
        Return retval
    End Function

    Private Function FilteredLeads(oZona As DTOLeadAreas.Zona) As List(Of DTOLeadAreas.Lead)
        Dim retval As New List(Of DTOLeadAreas.Lead)
        If _ContactClasses Is Nothing Then
            retval = oZona.Locations.SelectMany(Function(x) x.Leads).ToList()
        Else
            retval = oZona.Locations.ForContactClasses(_ContactClasses).SelectMany(Function(x) x.Leads).ToList()
        End If
        Return retval
    End Function



    Public Function CheckedLocations() As List(Of DTOLeadAreas.Location)
        Dim retval As New List(Of DTOLeadAreas.Location)
        For Each oCountryNode In MyBase.Nodes
            For Each oZonaNode In oCountryNode.nodes
                For Each oLocationNode In oZonaNode.Nodes
                    If oLocationNode.Checked Then
                        retval.Add(oLocationNode.Tag)
                    End If
                Next
            Next
        Next
        Return retval
    End Function

End Class


