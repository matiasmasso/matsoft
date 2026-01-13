Public Class SurveyController
    Inherits _MatController
    Function Index(survey As Guid, user As Guid) As ActionResult
        Dim oSurvey As DTOSurvey = BLLSurvey.Find(survey)
        Dim oUser As DTOUser = BLLUser.Find(user)
        Dim oParticipant As DTOSurveyParticipant = BLLSurveyParticipant.FindOrNew(oSurvey, oUser)
        BLLSurveyParticipant.Load(oParticipant)
        Return View("Survey", oParticipant)
    End Function

    <HttpPost>
    Function Update(data As String) As JsonResult
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim oSurveyResult = jss.Deserialize(Of DTOSurveyParticipant)(data)
        oSurveyResult.Fch = Now

        Dim myData As Object = Nothing
        Dim exs As New List(Of Exception)
        If BLLSurveyParticipant.Update(oSurveyResult, exs) Then
            myData = New With {.success = True}
        Else
            myData = New With {.success = False, .message = BLL.BLLExceptions.ToFlatString(exs)}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function
End Class
