Public Class DTOBaseGuid2
    Property Guid As Guid
    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Public Sub New()
        MyBase.New()
        _IsNew = True
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New()
        _Guid = oGuid
    End Sub

    Shared Function Opcional(Of T As DTOBaseGuid2)(oGuid As Guid) As T
        Dim retval As T = Nothing
        If oGuid <> Nothing Then retval = Activator.CreateInstance(GetType(T), oGuid)
        Return retval
    End Function


    Public Shadows Function UnEquals(oCandidate As DTOBaseGuid2) As Boolean
        Dim retval As Boolean = Not Equals(oCandidate)
        Return retval
    End Function

    Public Shadows Function Equals(oCandidate As DTOBaseGuid2) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            retval = _Guid.Equals(oCandidate.Guid)
        End If
        Return retval
    End Function

    Public Function NotEquals(oCandidate As DTOBaseGuid2) As Boolean
        Dim retval As Boolean = Not Equals(oCandidate)
        Return retval
    End Function

    Public Function NotEquals(oCandidateGuid As Guid) As Boolean
        Dim retval As Boolean = Not _Guid.Equals(oCandidateGuid)
        Return retval
    End Function

    Public Sub RenewGuid()
        'per clonar objectes
        _Guid = System.Guid.NewGuid
        _isNew = True
    End Sub

    Public Function GuionLessGuid() As String
        Dim retval As String = _Guid.ToString("N")
        Return retval
    End Function


    Shared Function CopyPropertyValues(Of T)(ByVal from As T, ByRef [to] As T, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oProperty As System.Reflection.PropertyInfo
        Try
            Dim oType As Type = GetType(T)
            Dim oProperties = oType.GetProperties()

            For Each oProperty In oProperties
                Try
                    If oProperty.CanWrite Then
                        Try
                            Dim propertyValue = oProperty.GetValue(from)
                            oProperty.SetValue([to], propertyValue)
                        Catch ex As Exception

                        End Try
                    End If

                Catch ex As Exception
                End Try
            Next
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    'Public Function Trimmed(Of T As New)() As T
    'Dim retval = Activator.CreateInstance(GetType(T), _Guid)
    'Return retval
    'End Function

    Public Function Trimmed() As DTOBaseGuid2
        Dim oType As Type = Me.GetType()
        Dim retval = Activator.CreateInstance(Me.GetType())
        Try
            Dim oProperties = oType.GetProperties()
            For Each oProperty In oProperties
                If GetType(DTOBaseGuid2).IsAssignableFrom(oProperty.PropertyType) Then
                    Try
                        Dim oBaseGuid2 As DTOBaseGuid2 = oProperty.GetValue(Me)
                        If oBaseGuid2 IsNot Nothing Then
                            Dim oTrimmedPropertyValue = Activator.CreateInstance(oProperty.PropertyType, oBaseGuid2.Guid)
                            oProperty.SetValue(retval, oTrimmedPropertyValue)
                        End If

                    Catch ex As Exception
                        oProperty.SetValue(retval, oProperty.GetValue(Me))
                    End Try
                Else
                    oProperty.SetValue(retval, oProperty.GetValue(Me))
                End If
            Next

        Catch ex As Exception
            Stop
        End Try
        Return retval
    End Function

    Shared Function Trim(Of T)(oSource As T, exs As List(Of Exception)) As T
        Dim oType As Type = oSource.GetType()
        Dim retval = Activator.CreateInstance(oSource.GetType())
        Try
            Dim oProperties = oType.GetProperties()
            For Each oProperty In oProperties
                If GetType(DTOBaseGuid2).IsAssignableFrom(oProperty.PropertyType) Then
                    Try
                        Dim oBaseGuid2 As DTOBaseGuid2 = oProperty.GetValue(oSource)
                        If oBaseGuid2 IsNot Nothing Then
                            Dim oTrimmedPropertyValue = Activator.CreateInstance(oProperty.PropertyType, oBaseGuid2.Guid)
                            oProperty.SetValue(retval, oTrimmedPropertyValue)
                        End If

                    Catch ex As Exception
                        oProperty.SetValue(retval, oProperty.GetValue(oSource))
                    End Try
                ElseIf oProperty.PropertyType.IsPrimitive OrElse oProperty.PropertyType = GetType(System.Guid) OrElse oProperty.PropertyType = GetType(Date) OrElse oProperty.PropertyType = GetType(Decimal) OrElse oProperty.PropertyType = GetType(String) OrElse oProperty.PropertyType = GetType(Integer) OrElse oProperty.PropertyType.IsEnum Then
                    Dim oRawValue = oProperty.GetValue(oSource)
                    oProperty.SetValue(retval, oRawValue)
                Else
                    Dim oRawValue = oProperty.GetValue(oSource)
                    If oRawValue IsNot Nothing Then
                        Dim oTrimmedValue = DTOBaseGuid2.Trim(oRawValue, exs)
                        oProperty.SetValue(retval, oTrimmedValue)
                    End If
                End If
            Next

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Function UrlSegment(segment As String) As String
        Dim retval = String.Format("/{0}/{1}", segment, _Guid.ToString())
        Return retval
    End Function

End Class

