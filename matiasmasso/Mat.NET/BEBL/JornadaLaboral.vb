Public Class JornadaLaboral
    Shared Function Find(oGuid As Guid) As DTOJornadaLaboral
        Return JornadaLaboralLoader.Find(oGuid)
    End Function


    Shared Function Log(exs As List(Of Exception), oUser As DTOUser) As DTOJornadaLaboral.Status
        Dim retval As DTOJornadaLaboral.Status = DTOJornadaLaboral.Status.NotSet
        Dim oStaff = BEBL.User.GetStaff(oUser)
        If oStaff Is Nothing Then
            retval = DTOJornadaLaboral.Status.NoStaff
        Else
            Dim lastLog = JornadesLaboralsLoader.Last(oStaff)
            If lastLog Is Nothing Then
                retval = DTOJornadaLaboral.Status.ReadyToEnter
                Dim oLog = DTOJornadaLaboral.Factory(oStaff)
                Update(oLog, exs)
            Else
                If lastLog.IsOpen Then
                    If lastLog.FchFrom.Date = DTO.GlobalVariables.Today() Then
                        retval = DTOJornadaLaboral.Status.ReadyToExit
                        lastLog.FchTo = DTO.GlobalVariables.Now()
                        Update(lastLog, exs)
                    Else
                        retval = DTOJornadaLaboral.Status.MissingExit
                        Dim oLog = DTOJornadaLaboral.Factory(oStaff)
                        Update(oLog, exs)
                    End If
                Else
                    retval = DTOJornadaLaboral.Status.ReadyToEnter
                    Dim oLog = DTOJornadaLaboral.Factory(oStaff)
                    Update(oLog, exs)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function Log(exs As List(Of Exception), mode As Integer, oStaff As DTOStaff) As String 'ToDeprecate
        Dim retval As String = ""
        If oStaff Is Nothing Then
            exs.Add(New Exception(oStaff.Lang.Tradueix("Empleado no reconocido", "Empleat no reconegut", "Unknown employee")))
        Else
            Select Case mode
                Case 1
                    Dim oJornadaLaboral = DTOJornadaLaboral.Factory(oStaff)
                    If JornadaLaboralLoader.Update(oJornadaLaboral, exs) Then
                        retval = String.Format("{0} {1} {2}", oJornadaLaboral.FchFrom.ToString("dd/MM/yy HH:mm"), oStaff.Lang.Tradueix("Entrada", "Entrada", "Entrance"), oStaff.Nom)
                    End If
                Case 2
                    Dim oJornadaLaboral = JornadesLaboralsLoader.Last(oStaff)
                    If oJornadaLaboral Is Nothing OrElse oJornadaLaboral.FchTo <> Nothing Then
                        oJornadaLaboral = DTOJornadaLaboral.Factory(oStaff)
                        oJornadaLaboral.FchTo = oJornadaLaboral.FchFrom
                    Else
                        oJornadaLaboral.FchTo = DTO.GlobalVariables.Now()
                    End If
                    If JornadaLaboralLoader.Update(oJornadaLaboral, exs) Then
                        Dim interval = oJornadaLaboral.FchTo - oJornadaLaboral.FchFrom
                        Dim sInterval = interval.ToString("hh\:mm")
                        retval = String.Format("{0} {1} {2} ({3})", oJornadaLaboral.FchTo.ToString("dd/MM/yy HH:mm"), oStaff.Lang.Tradueix("Salida", "Sortida", "Exit"), oStaff.Nom, sInterval)
                    End If
                Case Else
                    exs.Add(New Exception(oStaff.Lang.Tradueix("Enlace incorrecto", "Enllaç incorrecte", "Invalid link")))
            End Select
        End If
        Return retval
    End Function

    Shared Function Update(oJornadaLaboral As DTOJornadaLaboral, exs As List(Of Exception)) As Boolean
        Return JornadaLaboralLoader.Update(oJornadaLaboral, exs)
    End Function

    Shared Function Delete(oJornadaLaboral As DTOJornadaLaboral, exs As List(Of Exception)) As Boolean
        Return JornadaLaboralLoader.Delete(oJornadaLaboral, exs)
    End Function

End Class



Public Class JornadesLaborals
    Shared Function All(Optional staff As Guid? = Nothing) As Models.JornadesLaboralsModel
        Dim retval As New Models.JornadesLaboralsModel
        Dim oStaff As DTOStaff = Nothing
        If staff Is Nothing Then
            retval = JornadesLaboralsLoader.All()
        Else
            oStaff = BEBL.Staff.Find(staff)
            If oStaff IsNot Nothing Then
                retval = JornadesLaboralsLoader.All(oStaff)
            End If
        End If
        Return retval
    End Function


End Class
