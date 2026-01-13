Public Class AeatDoc
    Shared Function Find(oGuid As Guid) As DTOAeatDoc
        Dim retval As DTOAeatDoc = AeatDocLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function LastFromModel(oEmp As DTOEmp, oCod As DTOAeatModel.Cods) As DTOAeatDoc
        'a MVC/Views/DocsMercantils/DocsMercantils.vbhtml
        Return AeatDocLoader.LastFromModel(oEmp, oCod)
    End Function

    Shared Function Update(oAeatDoc As DTOAeatDoc, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AeatDocLoader.Update(oAeatDoc, exs)
        Return retval
    End Function

    Shared Function Delete(oAeatDoc As DTOAeatDoc, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AeatDocLoader.Delete(oAeatDoc, exs)
        Return retval
    End Function

End Class

Public Class AeatDocs
    Shared Function All(oEmp As DTOEmp, oModel As DTOAeatModel) As List(Of DTOAeatDoc)
        Dim retval As List(Of DTOAeatDoc) = AeatDocsLoader.All(oEmp, oModel)
        Return retval
    End Function

    Shared Function Exercicis(oEmp As DTOEmp) As List(Of DTOExercici)
        Dim retval As List(Of DTOExercici) = AeatDocsLoader.Exercicis(oEmp)
        Return retval
    End Function

End Class
