Public Class TextHelper
    Shared Function Excerpt(sLongText As String, Optional ByVal MaxLen As Integer = 0, Optional BlAppendEllipsis As Boolean = True) As String
        If sLongText > "" Then
            If sLongText.IndexOf("<more/>") >= 0 Then
                sLongText = sLongText.Substring(0, sLongText.IndexOf("<more/>"))
            Else
                If sLongText > "" Then
                    If MaxLen > 0 Then
                        Dim ellipsis As String = IIf(BlAppendEllipsis, "...", "")
                        If sLongText.Length > MaxLen - ellipsis.Length Then
                            Dim iLastBlank As Integer = sLongText.Substring(0, MaxLen).LastIndexOf(" ")
                            If iLastBlank > 0 Then
                                sLongText = sLongText.Substring(0, iLastBlank) & ellipsis
                            Else
                                sLongText = sLongText.Substring(0, MaxLen - ellipsis.Length) & ellipsis
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return sLongText
    End Function
End Class
