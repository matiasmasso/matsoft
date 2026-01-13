Public Class Frm_Contact_Ibans
    Private _Contact As DTOContact

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oContact As DTOContact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
    End Sub

    Private Async Sub Frm_Contact_Ibans_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Contact.Load(_Contact, exs) Then
            Me.Text = "Domiciliacions bancàries de " & _Contact.FullNom
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oIbans = Await FEB2.Ibans.FromContact(exs, _Contact)
        If exs.Count = 0 Then
            Xl_Contact_Ibans1.Load(oIbans)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_Contact_Ibans1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToAddNew
        Dim oIban = DTOIban.Factory(GlobalVariables.Emp, _Contact, DTOIban.Cods.Client)
        Dim oFrm As New Frm_Iban(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf onUpdated
        oFrm.Show()
    End Sub

    Private Async Sub onUpdated(sender As Object, e As MatEventArgs)
        Await refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Async Sub Xl_Contact_Ibans1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToRefresh
        Await refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub


End Class