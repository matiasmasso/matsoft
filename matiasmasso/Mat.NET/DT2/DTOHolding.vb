Public Class DTOHolding
    Inherits DTOGuidNom
    Property Emp As DTOEmp
    Property Companies As List(Of DTOContact)
    Property Clusters As List(Of DTOCustomerCluster)

    Public Enum wellknowns
        NotSet
        ElCorteIngles
        Prenatal
    End Enum

    Public Sub New()
        MyBase.New
        _Companies = New List(Of DTOContact)
        _Clusters = New List(Of DTOCustomerCluster)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Companies = New List(Of DTOContact)
        _Clusters = New List(Of DTOCustomerCluster)
    End Sub

    Overloads Shared Function Factory(oEmp As DTOEmp, sNom As String) As DTOHolding
        Dim retval As New DTOHolding
        With retval
            .Emp = oEmp
            .nom = sNom
        End With
        Return retval
    End Function

    Shared Function wellknown(id As wellknowns) As DTOHolding
        Dim retval As DTOHolding = Nothing
        Select Case id
            Case wellknowns.ElCorteIngles
                retval = New DTOHolding(New Guid("8B07C540-1DA6-48E2-B137-563BFDD4218B"))
            Case wellknowns.Prenatal
                retval = New DTOHolding(New Guid("1908D0F6-4B81-464F-B5CC-BE1B70AC8B3B"))
        End Select
        Return retval
    End Function
End Class
