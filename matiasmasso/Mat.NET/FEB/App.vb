Public Class App
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), id As DTOApp.AppTypes) As Task(Of DTOApp)
        Return Await Api.Fetch(Of DTOApp)(exs, "App", CInt(id).ToString)
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oApp As DTOApp) As Boolean
        Dim pApp = Api.FetchSync(Of DTOApp)(exs, "App", CInt(oApp.Id).ToString())
        If exs.Count = 0 Then
            DTOBaseGuid.CopyPropertyValues(Of DTOApp)(pApp, oApp, exs)
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oApp As DTOApp) As Task(Of Boolean)
        Return Await Api.Update(Of DTOApp)(oApp, exs, "App")
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oApp As DTOApp) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOApp)(oApp, exs, "App")
    End Function


    Shared Async Function InitializeAsync(exs As List(Of Exception), oAppType As DTOApp.AppTypes, ApiRootUrl As String, Optional ApiLocalPort As Integer = 0, Optional UseLocalApi As Boolean = False, Optional WebRootUrl As String = "", Optional WebLocalPort As String = "") As Task(Of DTOApp)
        Api.Initialize(ApiRootUrl, ApiLocalPort, UseLocalApi)

        Dim retval As New DTOApp(oAppType)
        With retval
            .Curs = Await Curs.AllAsync(exs)
            .Taxes = Await Taxes.AllAsync(exs)
            .Cur = .Curs.FirstOrDefault(Function(x) x.Tag = DTOCur.Ids.EUR.ToString())
            .PgcPlan = Await PgcPlan.FromYear(DTO.GlobalVariables.Today().Year, exs)
            .WebLocalPort = WebLocalPort
        End With
        Return retval
    End Function

    Shared Function InitializeSync(exs As List(Of Exception), oAppType As DTOApp.AppTypes, rootUrl As String, Optional localPort As Integer = 0, Optional useLocalApi As Boolean = False, Optional WebRootUrl As String = "", Optional WebLocalPort As String = "") As DTOApp
        Api.Initialize(rootUrl, localPort, useLocalApi)

        Dim retval As New DTOApp(oAppType)
        With retval
            .Curs = Curs.AllSync(exs)
            .Taxes = Taxes.AllSync(exs)
            .Cur = .Curs.FirstOrDefault(Function(x) x.Tag = DTOCur.Ids.EUR.ToString())
            .PgcPlan = PgcPlan.FromYearSync(DTO.GlobalVariables.Today().Year, exs)
        End With
        Return retval
    End Function

    Shared Sub Initialize(rootUrl As String, Optional localPort As Integer = 0, Optional useLocalApi As Boolean = False) 'for MatSched
        Api.Initialize(rootUrl, localPort, useLocalApi)
    End Sub

End Class


Public Class Apps
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOApp))
        Return Await Api.Fetch(Of List(Of DTOApp))(exs, "Apps")
    End Function

End Class