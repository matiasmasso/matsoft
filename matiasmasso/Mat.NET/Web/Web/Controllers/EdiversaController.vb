Public Class EdiversaController
    Inherits _MatController

    Public Function Index() As JsonResult
        Dim oEdiversaFiles As New List(Of DTOEdiversaFile)
        Dim exs As New List(Of DTOEdiversaException)
        BLLEdiversaFiles.ReadPending(oEdiversaFiles, exs)
        Dim retval As JsonResult = Json(oEdiversaFiles, JsonRequestBehavior.AllowGet)
        Return retval
    End Function



End Class
