Public Class DTOECITransmGroup

    Inherits DTOBaseGuid

    Property Ord As Integer
    Property Nom As String
    Property Platform As DTOContact

    Property Items As List(Of DTOECITransmCentre)

    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOECITransmCentre)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTOECITransmCentre)
    End Sub

    Shared Function SelectedDeliveries(oGroup As DTOECITransmGroup, oSrc As List(Of DTODelivery), ByRef rest As List(Of DTODelivery)) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        rest = New List(Of DTODelivery)
        For Each oDelivery As DTODelivery In oSrc
            If DTOECITransmGroup.Belongs(oGroup, oDelivery) Then
                retval.Add(oDelivery)
            Else
                rest.Add(oDelivery)
            End If
        Next
        Return retval
    End Function


    Shared Function SortDeliveries(oDeliveries As List(Of DTODelivery)) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = oDeliveries.
            OrderBy(Function(x) x.Platform.Guid.ToString()).
            OrderBy(Function(x) x.Customer.Ref).
            OrderBy(Function(x) DTOEci.NumeroDeComanda(x)).
            ToList
        Return retval
    End Function

    Shared Function Belongs(oGroup As DTOECITransmGroup, oDelivery As DTODelivery) As Boolean
        Dim retval As Boolean = oGroup.MatchesPlatform(oDelivery) And oGroup.MatchesCenter(oDelivery)
        Return retval
    End Function
    Shared Function Belongs(oGroup As DTOECITransmGroup, oOrder As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean = oGroup.MatchesPlatform(oOrder) And oGroup.MatchesCenter(oOrder)
        Return retval
    End Function

    Public Function MatchesPlatform(oDelivery As DTODelivery) As Boolean
        Dim retval = _Platform.Equals(oDelivery.platform)
        Return retval
    End Function

    Public Function MatchesPlatform(oOrder As DTOPurchaseOrder) As Boolean
        Dim retval = _Platform.Equals(oOrder.platform)
        Return retval
    End Function

    Public Function MatchesCenter(oDelivery As DTODelivery) As Boolean
        Dim retval As Boolean
        If _Items.Count = 0 Then
            retval = True 'si no hi ha centres, s'admet qualsevol centre
        Else
            retval = _Items.Any(Function(x) x.Centre.Equals(oDelivery.customer))
        End If
        Return retval
    End Function

    Public Function MatchesCenter(oOrder As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean
        If _Items.Count = 0 Then
            retval = True 'si no hi ha centres, s'admet qualsevol centre
        Else
            retval = _Items.Any(Function(x) x.Centre.Equals(oOrder.customer))
        End If
        Return retval
    End Function

    Shared Function Contacts(oECITransmGroup As DTOECITransmGroup) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        For Each item In oECITransmGroup.Items
            retval.Add(item.Centre)
        Next
        Return retval
    End Function

    Shared Function Sort(oDeliveries As List(Of DTODelivery)) As List(Of DTODelivery)
        Dim retval As List(Of DTODelivery) = oDeliveries.
            OrderBy(Function(x) x.Platform.Guid.ToString()).
            OrderBy(Function(x) x.Customer.Ref).
            OrderBy(Function(x) DTOEci.NumeroDeComanda(x)).
            ToList
        Return retval
    End Function

    Shared Function SortedDeliveries(oGroups As List(Of DTOECITransmGroup), oDeliveries As List(Of DTODelivery), exs As List(Of Exception)) As List(Of DTODelivery)
        Dim retval As New List(Of DTODelivery)
        Dim rest As List(Of DTODelivery) = oDeliveries
        For Each oGroup As DTOECITransmGroup In oGroups
            If rest.Count = 0 Then Exit For

            Dim src As List(Of DTODelivery) = rest
            Dim oGroupDeliveries As List(Of DTODelivery) = DTOECITransmGroup.SelectedDeliveries(oGroup, src, rest)
            Dim oSortedGroupDeliveries = oGroupDeliveries.
                OrderBy(Function(x) x.customer.ref).
                OrderBy(Function(x) DTOEci.NumeroDeComanda(x)).
                ToList
            retval.AddRange(oSortedGroupDeliveries)
        Next

        If rest.Count > 0 Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine(String.Format("{0} albarans no s'han pogut assignar a cap grup de transmisió:", rest.Count))
            For Each oDelivery In rest
                sb.AppendLine(String.Format("centre {0} comanda {1}", oDelivery.customer.ref, DTOEci.NumeroDeComanda(oDelivery)))
            Next
        End If
        Return retval
    End Function


End Class
