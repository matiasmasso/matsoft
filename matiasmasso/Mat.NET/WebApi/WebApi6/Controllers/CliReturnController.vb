Public Class CliReturnController
    Inherits _BaseController

    <HttpPost>
    <Route("api/cliReturns")>
    Public Function All(user As DTOGuidNom) As List(Of DTOCliReturn)
        Dim retval As List(Of DTOCliReturn) = BLLCliReturns.All
        Return retval
    End Function

    <HttpPost>
    <Route("api/cliReturn/update")>
    Public Function Update(value As DTOCliReturn) As DTOCliReturn
        Dim exs As New List(Of Exception)
        Dim retval As DTOCliReturn = Nothing
        If BLLCliReturn.Update(value, exs) Then
            retval = value
        End If
        Return retval
    End Function

End Class
