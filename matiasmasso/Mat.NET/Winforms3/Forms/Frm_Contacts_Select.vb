Public Class Frm_Contacts_Select
    Private _Contact As DTOContact

    Public Event onItemSelected(sender As System.Object, e As MatEventArgs)

    Public Sub New(oContacts As List(Of DTOContact))
        MyBase.New()
        Me.InitializeComponent()
        Xl_Contacts1.Load(oContacts, Nothing, DTO.Defaults.SelectionModes.Selection)

        Me.Width = Me.Width + Xl_Contacts1.WidthAdjustment
        Me.Height = Xl_Contacts1.AdjustedHeight
    End Sub

    Public ReadOnly Property Contact As DTOContact
        Get
            Return _Contact
        End Get
    End Property


    Private Sub Xl_Contacts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.onItemSelected
        _Contact = e.Argument
        RaiseEvent onItemSelected(sender, e)
        Me.Close()
    End Sub
End Class