Imports System.Net
Imports System.Net.Sockets

Public Class Tamariu
    Shared Function Read() As DTO.Tamariu
        Return TamariuLoader.Read()
    End Function

    Shared Sub SetOk()
        TamariuLoader.Update(True)
    End Sub

    Shared Function SetKo() As Boolean
        Return TamariuLoader.Update(False)
    End Function

    Shared Async Function NotifyKo() As Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim oEmp = BEBL.Emp.Find(DTOEmp.Ids.MatiasMasso)
        Dim r = Await MailToSubscriptors(exs, oEmp)
        Return True
    End Function

    Shared Async Function CheckPort() As Task(Of Boolean)
        Dim hostname As String = "2.139.175.182" ' "192.168.1.77"
        Dim port As String = "1081"

        'Dim client As New TcpClient(AddressFamily.InterNetwork)

        Using client As New TcpClient(AddressFamily.InterNetwork)
            Dim c = client.BeginConnect(IPAddress.Parse(hostname), port, Nothing, Nothing)
            Dim success = c.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1))
            If success Then
                SetOk()
            Else
                If SetKo() Then
                    Await NotifyKo()
                End If
            End If
        End Using



        'client.BeginConnect(hostname, port,
        '                Sub(x)
        '                    Dim tcp As TcpClient = CType(x.AsyncState, TcpClient)
        '                    Try
        '                        tcp.EndConnect(x)
        '                        SetOk()
        '                    Catch ex As Exception
        '                        'error
        '                        SetKo()
        '                        Dim exs As New List(Of Exception)
        '                        Dim oEmp = BEBL.Emp.Find(DTOEmp.Ids.MatiasMasso)
        '                        Dim r = await MailToSubscriptors(exs, oEmp)
        '                        'send email
        '                    End Try
        '                    tcp.Close()
        '                End Sub, client
        '                )
        Return (False)
    End Function

    Shared Async Function MailToSubscriptors(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of Boolean)
        Dim oTo As New System.Net.Mail.MailAddressCollection
        Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.TamariuCheckPort)
        Dim oSubscriptors As List(Of DTOSubscriptor) = BEBL.Subscriptors.All(oSsc)

        If oSubscriptors.Count > 0 Then
            Dim sRecipients = oSubscriptors.Select(Function(x) x.EmailAddress).ToList

            Dim sSubject = "Alarma de tall de llum a Tamariu"
            Dim sBody = "El Loxone no respon; probablement estiguem sense llum"

            Dim oMailMessage = DTOMailMessage.Factory(sRecipients, sSubject, sBody)
            Await MailMessageHelper.Send(oEmp, oMailMessage, exs)
        End If
        Dim retVal As Boolean = exs.Count = 0
        Return retVal
    End Function

End Class
