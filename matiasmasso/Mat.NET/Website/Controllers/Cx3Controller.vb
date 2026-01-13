Public Class Cx3Controller
    Inherits _MatController


    Function MakeCall(id As String) As ActionResult
        Return View(viewName:="MakeCall", model:=id)
    End Function

End Class
