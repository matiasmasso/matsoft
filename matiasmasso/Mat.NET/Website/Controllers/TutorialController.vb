Public Class TutorialController
    Inherits _MatController

    Public Function Index(guid As Guid) As ActionResult
        ViewBag.Title = ContextHelper.Lang.Tradueix("Tutoriales App M+O", "Tutorials App M+O", "M+O App Tutorials")
        Return View()
    End Function



End Class
