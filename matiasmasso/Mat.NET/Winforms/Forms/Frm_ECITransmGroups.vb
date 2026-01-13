Public Class Frm_ECITransmGroups

    Private _ECITransmGroup As DTOECITransmGroup
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)



    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await RefrescaGroups()
    End Sub

    Private Async Function RefrescaGroups() As Task
        Dim exs As New List(Of Exception)
        Dim oGroups = Await FEB2.ECITransmGroups.All(exs)
        If exs.Count = 0 Then
            Xl_ECITransmGroups1.Load(oGroups)
            RefrescaGroup()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentGroup() As DTOECITransmGroup
        Dim retval As DTOECITransmGroup = Xl_ECITransmGroups1.Value
        Return retval
    End Function

    Private Sub RefrescaGroup()
        _AllowEvents = False
        Dim oGroup As DTOECITransmGroup = CurrentGroup()

        If oGroup Is Nothing Then
            TextBoxNom.Clear()
            Xl_Contact2Platform.Contact = Nothing
            CheckBoxCentres.Checked = False
            Xl_Contacts_Editable1.Load(New List(Of DTOContact))
        Else
            With oGroup
                TextBoxNom.Text = .Nom
                Xl_Contact2Platform.Contact = .Platform
                CheckBoxCentres.Checked = .Items.Count > 0
                Xl_Contacts_Editable1.Load(DTOECITransmGroup.Contacts(oGroup))
            End With
        End If
        Xl_Contact2Platform.Enabled = True
        Xl_Contacts_Editable1.Enabled = CheckBoxCentres.Checked
        _AllowEvents = True
    End Sub

    Private Async Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged
        If _AllowEvents Then
            Await SaveGroup()
        End If
    End Sub

    Private Sub Xl_ECITransmGroups1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ECITransmGroups1.ValueChanged
        RefrescaGroup()
    End Sub


    Private Async Sub CheckBoxCentres_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCentres.CheckedChanged
        If _AllowEvents Then
            Xl_Contacts_Editable1.Enabled = CheckBoxCentres.Checked
            If Not CheckBoxCentres.Checked Then
                Await SaveGroup()
            End If
        End If
    End Sub

    Private Async Sub Xl_Contacts_Editable1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contacts_Editable1.AfterUpdate
        Await SaveGroup()
    End Sub

    Private Async Function SaveGroup() As Task
        Dim oGroup As DTOECITransmGroup = CurrentGroup()
        Dim oContacts As List(Of DTOContact) = Xl_Contacts_Editable1.Values

        With oGroup
            .Nom = TextBoxNom.Text

            .Platform = Xl_Contact2Platform.Contact

            .Items = New List(Of DTOECITransmCentre)
            If CheckBoxCentres.Checked Then
                For Each oContact As DTOContact In oContacts
                    Dim item As New DTOECITransmCentre()
                    item.Parent = oGroup
                    item.Centre = oContact
                    .Items.Add(item)
                Next
            End If

            Dim exs As New List(Of Exception)
            If Await FEB2.ECITransmGroup.Update(oGroup, exs) Then
            Else
                UIHelper.WarnError(exs)
            End If
        End With
    End Function

    Private Async Sub Xl_ECITransmGroups1_RequestToDelete(sender As Object, e As MatEventArgs) Handles Xl_ECITransmGroups1.RequestToDelete
        Dim oGroup As DTOECITransmGroup = e.Argument
        Dim exs As New List(Of Exception)
        If Await FEB2.ECITransmGroup.Delete(oGroup, exs) Then
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ECITransmGroups1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ECITransmGroups1.RequestToRefresh
        Await RefrescaGroups()
    End Sub

    Private Async Sub Xl_Contact2Platform_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contact2Platform.AfterUpdate
        Await SaveGroup()
    End Sub
End Class


