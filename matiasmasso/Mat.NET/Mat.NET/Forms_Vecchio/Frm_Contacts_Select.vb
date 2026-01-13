Public Class Frm_Contacts_Select

    Public Event onItemSelected(sender As System.Object, e As MatEventArgs)


    Public Sub New(oContacts As Contacts)
        MyBase.New()
        Me.InitializeComponent()
        Xl_Contacts1.DataSource = oContacts

        Me.Width = Me.Width + Xl_Contacts1.WidthAdjustment
        Me.Height = Xl_Contacts1.AdjustedHeight
    End Sub

    Public Sub New(oContacts As List(Of DTOContact))
        MyBase.New()
        Me.InitializeComponent()
        Xl_Contacts1.Load(oContacts)

        Me.Width = Me.Width + Xl_Contacts1.WidthAdjustment
        Me.Height = Xl_Contacts1.AdjustedHeight
    End Sub

    Public ReadOnly Property SelectedObject As Contact
        Get
            Dim retval As Contact = Xl_Contacts1.SelectedObject
            Return retval
        End Get
    End Property
    Public ReadOnly Property SelectedObject2 As DTOContact
        Get
            Dim retval As DTOContact = Xl_Contacts1.SelectedObject2
            Return retval
        End Get
    End Property


    Private Sub Xl_Contacts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.onItemSelected
        RaiseEvent onItemSelected(sender, e)
        Me.Close()
    End Sub
End Class