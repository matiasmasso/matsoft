Public Class ImmobleController
    Inherits _BaseController

    <HttpGet>
    <Route("api/immobles/")>
    Public Function ImmoblesGet() As List(Of DTOImmoble)
        Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.matias)
        Dim retval As List(Of DTOImmoble) = BLLImmobles.All(oUser)
        Return retval
    End Function

    <HttpPost>
    <Route("api/immobles")>
    Public Function Immobles(user As DTOGuidNom) As List(Of DTOImmoble)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim retval As List(Of DTOImmoble) = BLLImmobles.All(oUser)
        Return retval
    End Function

    <HttpPost>
    <Route("api/immoble/update")>
    Public Function ImmobleUpdate(oImmoble As DTOImmoble) As DTOImmoble
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = BLLImmoble.Update(oImmoble, exs)
        Return oImmoble
    End Function

End Class
