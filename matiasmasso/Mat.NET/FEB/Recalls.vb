Public Class Recall
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTORecall)
        Return Await Api.Fetch(Of DTORecall)(exs, "Recall", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oRecall As DTORecall) As Boolean
        If Not oRecall.IsLoaded And Not oRecall.IsNew Then
            Dim pRecall = Api.FetchSync(Of DTORecall)(exs, "Recall", oRecall.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORecall)(pRecall, oRecall, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRecall As DTORecall) As Task(Of Boolean)
        Return Await Api.Update(Of DTORecall)(oRecall, exs, "Recall")
        oRecall.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oRecall As DTORecall) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORecall)(oRecall, exs, "Recall")
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oUser As DTOUser, oRecall As DTORecall) As Task(Of DTORecallCli)
        Dim oCustomers = Await Customers.FromUser(exs, oUser)
        Dim retval As New DTORecallCli
        With retval
            .UsrLog = DTOUsrLog.Factory(oUser)
            .Recall = oRecall
            .ContactEmail = oUser.EmailAddress
            .ContactNom = oUser.NickName

            If oCustomers.Count > 0 Then
                .Customer = oCustomers.First
            Else
                Dim oContacts = Await Contacts.All(exs, oUser)
                If exs.Count = 0 AndAlso oContacts.Count > 0 Then
                    .Customer = DTOCustomer.FromContact(oContacts.First)
                End If
            End If

            If .Customer IsNot Nothing Then
                Contact.Load(.Customer, exs)
                .ContactTel = Await Contact.Tel(exs, .Customer)
                Dim oAddress As DTOAddress = .Customer.Address
                .Address = oAddress.Text
                .Location = DTOAddress.Location(oAddress).Nom
                .Country = DTOAddress.Country(oAddress)
            End If
        End With
        Return retval
    End Function

End Class

Public Class Recalls
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTORecall))
        Return Await Api.Fetch(Of List(Of DTORecall))(exs, "Recalls")
    End Function

End Class
