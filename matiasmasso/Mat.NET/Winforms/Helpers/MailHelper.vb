Module MailHelper

    Function MailConfirmationBase(oEmp As DTOEmp, oTemplate As DTODefault.MailingTemplates, sTo As String, copyToInfo As Boolean, sSubject As String, ParamArray params() As String) As Boolean
        Dim retval As Boolean

        Dim oBcc As New System.Net.Mail.MailAddressCollection()
        If copyToInfo Then
            oBcc.Add("info@matiasmasso.es")
        End If

        Dim sBodyUrl As String = FEBL.Mailing.BodyUrl(oTemplate, params)

        Dim exs As New List(Of Exception)
        If BLL.MailHelper.SendMail(oEmp, sTo:=sTo, oBcc:=oBcc, sSubject:=sSubject, sBody:=sBodyUrl, oBodyFormat:=DTOEnums.OutputFormat.URL, exs:=exs) Then
            retval = True
        Else
            UIHelper.WarnError(exs)
        End If
        Return retval
    End Function
End Module
