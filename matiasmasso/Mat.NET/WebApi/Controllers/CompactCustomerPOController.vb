Imports System.Web.Http
Public Class CompactCustomerPOController
    Inherits _BaseController

    <HttpPost>
    <Route("api/CustomerPo")>
    Public Function CustomerPO(<FromBody> oPO As DTOCompactCustomerPO) As DTOTaskResult
        Dim retval As DTOTaskResult = BEBL.CompactCustomerPO.Upload(oPO)
        Return retval
    End Function

End Class
