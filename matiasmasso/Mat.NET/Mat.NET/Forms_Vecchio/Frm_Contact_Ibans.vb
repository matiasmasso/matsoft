Public Class Frm_Contact_Ibans
    Private _Contact As DTOContact

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oContact As DTOContact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
        BLL.BLLContact.Load(_Contact)
        Me.Text = "Domiciliacions bancàries de " & _Contact.FullNom
        refresca()
    End Sub

    Private Sub refresca()
        Dim oIbans As List(Of DTOIban) = BLL.BLLIbans.FromContact(_Contact)
        Xl_Contact_Ibans1.Load(oIbans)
    End Sub

    Private Sub Xl_Contact_Ibans1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToAddNew
        Dim oIban As DTOIban = BLL.BLLIban.NewIban(_Contact)
        Dim oFrm As New Frm_Iban(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf onUpdated
        oFrm.Show()
    End Sub

    Private Sub onUpdated(sender As Object, e As MatEventArgs)
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub Xl_Contact_Ibans1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToRefresh
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub
End Class