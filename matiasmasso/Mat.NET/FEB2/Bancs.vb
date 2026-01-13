Public Class Banc
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBanc)
        Return Await Api.Fetch(Of DTOBanc)(exs, "Banc", oGuid.ToString())
    End Function

    Shared Function FindSync(oGuid As Guid, exs As List(Of Exception)) As DTOBanc
        Return Api.FetchSync(Of DTOBanc)(exs, "Banc", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oBanc As DTOBanc, exs As List(Of Exception)) As Boolean
        If Not oBanc.IsLoaded And Not oBanc.IsNew Then
            Dim pBanc = Api.FetchSync(Of DTOBanc)(exs, "Banc", oBanc.Guid.ToString())
            If exs.Count = 0 And pBanc IsNot Nothing Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBanc)(pBanc, oBanc, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oBanc As DTOBanc, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOBanc)(oBanc, exs, "Banc")
        oBanc.IsNew = False
    End Function


    Shared Async Function Delete(oBanc As DTOBanc, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBanc)(oBanc, exs, "Banc")
    End Function

    Shared Function BancToReceiveTransfers(oEmp As DTOEmp) As DTOBanc
        Dim exs As New List(Of Exception)
        Dim sGuid = FEB2.Default.EmpValueSync(oEmp, DTODefault.Codis.BancToReceiveTransfers, exs)
        Dim retval As DTOBanc = Nothing
        If sGuid > "" Then
            Dim oGuid = New Guid(sGuid)
            retval = FEB2.Banc.FindSync(oGuid, exs)
        End If
        Return retval
    End Function

    Shared Function BancTransfersReceptionHtml(oEmp As DTOEmp) As String 'web
        Dim oBanc As DTOBanc = BancToReceiveTransfers(oEmp)
        Dim oBranch As DTOBankBranch = oBanc.iban.BankBranch
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(oBanc.abr)
        If oBranch IsNot Nothing Then
            If oBranch.location IsNot Nothing Then
                sb.AppendLine(oBranch.address & " " & oBranch.location.nom)
            End If
        End If
        sb.AppendLine(DTOIban.Formated(oBanc.iban))
        Dim retval As String = sb.ToString.Replace(vbCrLf, "<br/>")
        Return retval
    End Function

End Class
Public Class Bancs

    Shared Async Function AllActiveWithIcons(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOBanc))
        Dim oBancs = Await AllActive(oEmp, exs)
        If exs.Count = 0 And oBancs.Count > 0 Then
            Dim oSprite = Await AllActiveSprite(oEmp, exs)
            If exs.Count = 0 Then
                For i As Integer = 0 To oBancs.Count - 1
                    oBancs(i).Logo = LegacyHelper.SpriteHelper.Extract(oSprite, i, oBancs.Count)
                Next
            End If
        End If
        Return oBancs
    End Function

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOBanc))
        Return Await Api.Fetch(Of List(Of DTOBanc))(exs, "bancs/all", oEmp.Id)
    End Function

    Shared Async Function AllActive(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOBanc))
        Return Await Api.Fetch(Of List(Of DTOBanc))(exs, "bancs", oEmp.Id) 'active bancs
    End Function

    Shared Async Function BancsToReceiveTransfer(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOBanc))
        Return Await Api.Fetch(Of List(Of DTOBanc))(exs, "bancs/BancsToReceiveTransfer", oEmp.Id)
    End Function

    Shared Async Function AllActiveSprite(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Byte())
        Dim retval As Byte() = Await Api.FetchImage(exs, "bancs/sprite", oEmp.Id)
        Return retval
    End Function


    Shared Async Function Sprite(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "bancs/sprite", CInt(oEmp.Id))
    End Function
End Class
