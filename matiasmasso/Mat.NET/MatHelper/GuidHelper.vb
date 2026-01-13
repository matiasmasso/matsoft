Public Class GuidHelper

    Shared Function IsGuid(ByVal sGuidStringCandidate As Object) As Boolean
        Dim retval As Boolean = False
        Try
            If sGuidStringCandidate IsNot Nothing Then
                Dim oGuid As New Guid(sGuidStringCandidate.ToString)
                retval = True
            End If
        Catch ex As Exception

        End Try
        Return retval
    End Function


    Shared Function GetGuid(ByVal oGuidCandidate As Object) As Guid
        Dim retVal As Guid = Nothing
        If oGuidCandidate IsNot Nothing Then
            If Not IsDBNull(oGuidCandidate) Then
                If TypeOf (oGuidCandidate) Is Guid Then
                    If Not oGuidCandidate.Equals(Guid.Empty) Then
                        retVal = oGuidCandidate
                    End If
                ElseIf TypeOf (oGuidCandidate) Is String Then
                    Try
                        retVal = New Guid(oGuidCandidate.ToString)
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If
        Return retVal
    End Function

    Shared Function GetGuidFromBase64(sBase64 As String) As Guid
        Dim retval As Guid = Nothing

        Try
            Dim sCleanBase64 As String = sBase64.Replace("_", "/")
            sCleanBase64 = sCleanBase64.Replace("-", "+")
            Dim sBase64WithSufix As String = IIf(Right(sCleanBase64, 2) = "==", sCleanBase64, sCleanBase64 & "==")
            Dim oBuffer() As Byte = Convert.FromBase64String(sBase64WithSufix)
            retval = New Guid(oBuffer)
        Catch ex As Exception

        End Try

        Return retval
    End Function


    Shared Function GetBase64FromGuid(oGuid As Guid) As String
        Dim retval As String = Convert.ToBase64String(oGuid.ToByteArray())
        retval = retval.Replace("/", "_")
        retval = retval.Replace("+", "-")
        Return retval.Substring(0, 22)
    End Function

    Shared Function ToGuionLess(oGuid As Guid) As String
        Dim retval As String = oGuid.ToString("N")
        Return retval
    End Function

    Shared Function FromGuionLess(value As String) As Guid
        Dim retval As New Guid(value)
        Return retval
    End Function
End Class