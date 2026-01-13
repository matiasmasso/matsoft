Public Class GuidHelper

    Shared Function IsGuid(ByVal sGuidStringCandidate As Object) As Boolean
        Dim retval As Boolean = False
        Try
            Dim oGuid As New Guid(sGuidStringCandidate.ToString)
            retval = True
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
End Class
