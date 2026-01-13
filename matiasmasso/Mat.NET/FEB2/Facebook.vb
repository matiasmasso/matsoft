Public Class Facebook
    Public Enum AppIds
        NotSet
        LocalHost_65418
        MatiasMasso
        InglesinaSelfiesLogin
        MatiasMasso_Pt
    End Enum


    Shared Function AppOrDeveloperId(oId As AppIds) As String
        Dim retval As String
        If Debugger.IsAttached Then
            retval = AppId(AppIds.LocalHost_65418)
        Else
            retval = AppId(oId)
        End If
        Return retval
    End Function

    Shared Function AppId(oId As AppIds) As String
        Dim retval As String = ""
        Select Case oId
            Case AppIds.LocalHost_65418
                retval = "1504195263194424"
            Case AppIds.MatiasMasso
                retval = "489736407757151"
            Case AppIds.InglesinaSelfiesLogin
                retval = "1579538215608489"
        End Select
        Return retval
    End Function


End Class
