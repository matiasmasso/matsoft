Public Class DTOCustomerCluster
    Inherits DTOGuidNom

    Property Holding As DTOHolding
    Property Obs As String

    Property Customers As List(Of DTOCustomer)

    Public Sub New()
        MyBase.New()
        _Customers = New List(Of DTOCustomer)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Customers = New List(Of DTOCustomer)
    End Sub

    Shared Shadows Function Factory(oHolding As DTOHolding) As DTOCustomerCluster
        Dim retval As New DTOCustomerCluster
        retval.Holding = oHolding
        Return retval
    End Function
End Class
