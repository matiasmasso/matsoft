Public Class NominaController
    Inherits _BaseController

    <HttpPost>
    <Route("api/nominas")>
    Public Function Nominas(target As DTOGuidNom) As List(Of DUI.Nomina)
        Dim retval As New List(Of DUI.Nomina)
        Dim oStaff As New DTOStaff(target.Guid)
        Dim oNomines As List(Of DTONomina) = BLLNomines.All(oStaff)
        For Each oNomina As DTONomina In oNomines
            Dim item As New DUI.Nomina
            With item
                .Fch = oNomina.Cca.Fch
                .Devengat = String.Format("{0:F2}", oNomina.Devengat.Eur + oNomina.Dietes.Eur)
                .Liquid = String.Format("{0:F2}", oNomina.Liquid.Eur)
                .ThumbnailUrl = BLLDocFile.ThumbnailUrl(oNomina.Cca.DocFile, True)
                .FileUrl = BLLDocFile.DownloadUrl(oNomina.Cca.DocFile, True)
            End With
            retval.Add(item)
        Next
        Return retval
    End Function
End Class
