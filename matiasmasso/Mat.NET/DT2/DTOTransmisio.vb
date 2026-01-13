Public Class DTOTransmisio
    Inherits DTOBaseGuid

    Property emp As DTOEmp
    Property id As Integer
    Property fch As DateTimeOffset
    Property mgz As DTOMgz
    Property amt As DTOAmt
    Property deliveries As List(Of DTODelivery)

    Property deliveriesCount As Integer
    Property invoicedDeliveriesCount As Integer
    Property noFacturablesCount As Integer
    Property linesCount As Integer
    Property unitsCount As Integer

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oEmp As DTOEmp, oMgz As DTOMgz, oDeliveries As List(Of DTODelivery)) As DTOTransmisio
        Dim retval As New DTOTransmisio
        With retval
            .Emp = oEmp
            .Fch = DateTimeOffset.Now
            .Mgz = oEmp.Mgz
            .Deliveries = oDeliveries
        End With
        Return retval
    End Function

    Public Function FileNameDades() As String
        Dim s As String = FilePrefix() & "dades." & MyBase.Guid.ToString & ".xml"
        Return s
    End Function

    Public Function FileNameDocs() As String
        Return FilePrefix() & "documentacio." & MyBase.Guid.ToString & ".pdf"
    End Function

    Public Function FilePrefix() As String
        Return "M+O." & _Fch.Year.ToString & "." & Format(_Id, "0000") & "."
    End Function
End Class
