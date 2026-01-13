Public Class AeatModel

    Shared Function Find(oGuid As Guid, oUser As DTOUser) As DTOAeatModel
        Dim retval As DTOAeatModel = AeatModelLoader.Find(oGuid, oUser)
        Return retval
    End Function

    Shared Function Update(oAeatModel As DTOAeatModel, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AeatModelLoader.Update(oAeatModel, exs)
        Return retval
    End Function

    Shared Function Delete(oAeatModel As DTOAeatModel, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AeatModelLoader.Delete(oAeatModel, exs)
        Return retval
    End Function

End Class



Public Class AeatModels
    Shared Function All(oUser As DTOUser) As DTOAeatModel.Collection
        Dim retval = AeatModelsLoader.All(oUser)
        Return retval
    End Function

End Class

