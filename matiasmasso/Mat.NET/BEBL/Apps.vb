Public Class App

    Shared Function Find(id As DTOApp.AppTypes) As DTOApp
        Return AppLoader.Find(id)
    End Function

    Shared Function Update(oApp As DTOApp, exs As List(Of Exception)) As Boolean
        Return AppLoader.Update(oApp, exs)
    End Function

    Shared Function Delete(oApp As DTOApp, exs As List(Of Exception)) As Boolean
        Return AppLoader.Delete(oApp, exs)
    End Function

End Class



Public Class Apps
    Shared Function All() As List(Of DTOApp)
        Dim retval = AppsLoader.All()
        Return retval
    End Function
End Class

