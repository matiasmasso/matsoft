Public Class Frm_SepaMandatos

    Private _Iban As DTOIban
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oIban As DTOIban)
        MyBase.New()
        _Iban = oIban
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_SepaMandatos_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Iban.Load(_Iban, exs) Then
            Me.Text = "Mandatos Sepa " & DTOIban.BankNom(_Iban) & " " & DTOIban.Formated(_Iban)
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_SepaMandatos1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_SepaMandatos1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_SepaMandatos1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_SepaMandatos1.RequestToAddNew
        Dim oSepaMandato As New DTOSepaMandato
        With oSepaMandato
            .Iban = _Iban
            .FchFrom = Today
            .UsrLog = DTOUsrLog.Factory(Current.Session.User)
        End With

        Dim oFrm As New Frm_SepaMandato(oSepaMandato)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_SepaMandatos1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SepaMandatos1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oSepaMandatos = Await FEB2.SepaMandatos.All(exs, _Iban)
        If exs.Count = 0 Then
            Xl_SepaMandatos1.Load(oSepaMandatos)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class