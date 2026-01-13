

Public Class Frm_CliImgs
    Private mContact As contact

    Public WriteOnly Property Contact() As contact
        Set(ByVal value As contact)
            mContact = value
            Me.Text = "IMATGES DE " & mContact.Clx
            Xl_CliImgs1.Contact = mContact
        End Set
    End Property
End Class