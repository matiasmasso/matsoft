Public Class Frm_CustomerIbans
    Private _Contact As DTOContact

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oContact As DTOContact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
    End Sub

    Private Async Sub Frm_Ibans_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub


    Private Sub Xl_Contact_Ibans1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToAddNew
        Dim oIbans As List(Of DTOIban) = Xl_Contact_Ibans1.Values
        If oIbans.Exists(Function(x) x.FchTo = Nothing) Then
            UIHelper.WarnError("Cal posar primer data de caducitat al mandat vigent")
        Else
            Dim LastFchTo As Date = oIbans.Max(Function(x) x.FchTo)
            Dim oIban As New DTOIban
            With oIban
                .Cod = DTOIban.Cods.Client
                .Titular = _Contact
                .FchFrom = IIf(LastFchTo = Nothing, Today, LastFchTo.AddDays(1))
            End With
            Dim oFrm As New Frm_Iban(oIban)
            AddHandler oFrm.AfterUpdate, AddressOf refresca
            oFrm.Show()
        End If
    End Sub

    Private Async Sub Xl_Contact_Ibans1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contact_Ibans1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oIbans = Await FEB2.Ibans.FromContact(exs, _Contact, , DTOIban.Cods.Client)
        If exs.Count = 0 Then
            Xl_Contact_Ibans1.Load(oIbans)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class
