

Public Class Frm_OCS_IncomingCall



    Public WriteOnly Property Telefon As telefon
        Set(ByVal value As telefon)
            Dim oContacts As Contacts = Contacts.FromTelefon(value)
            LabelNom.Text = oContacts(0).Clx & vbCrLf & Format(Now, "HH:mm")
            Xl_Contact_Logo1.Contact = oContacts(0)
        End Set
    End Property

    Public Sub DropCall()
        PictureBoxCall.Image = My.Resources.CallClosed
    End Sub
End Class