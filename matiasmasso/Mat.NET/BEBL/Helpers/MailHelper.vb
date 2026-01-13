Imports System.IO

Public Module MailHelper

    Public Enum WellKnownRecipients
        Admin
        Info
        Cuentas
    End Enum


    Public Function MailAdmin(ByVal sSubject As String, Optional ByVal sBody As String = "") As DTOTaskResult
        Dim oUser = BEBL.User.Find(DTOUser.WellKnowns.matias)
        Dim oMailMsg = MailMsg.Factory(oUser.Emp, oUser.EmailAddress, sSubject, sBody)
        Dim retval = oMailMsg.Send()
        Return retval
    End Function

    Public Function MailInfo(ByVal sSubject As String, Optional ByVal sBody As String = "") As DTOTaskResult
        Dim oUser = BEBL.User.Find(DTOUser.WellKnowns.info)
        Dim oMailMsg = MailMsg.Factory(oUser.Emp, oUser.EmailAddress, sSubject, sBody)
        Dim retval = oMailMsg.Send()
        Return retval
    End Function


End Module
