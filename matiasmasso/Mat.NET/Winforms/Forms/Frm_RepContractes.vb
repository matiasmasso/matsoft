Public Class Frm_RepContractes
    Private _Contact As DTOContact
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oContact As DTOContact = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Contact = oContact
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Contracts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Contact IsNot Nothing Then
            If FEB2.Contact.Load(_Contact, exs) Then
                Me.Text = "Contractes de " & _Contact.NomComercialOrDefault()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
        refresca()
    End Sub

    Private Sub Xl_Contracts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Contracts1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Contracts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contracts1.RequestToAddNew
        Dim oContract As New DTOContract
        Dim oFrm As New Frm_Contract(oContract)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Contracts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contracts1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        Dim oCodi = DTOContractCodi.Wellknown(DTOContractCodi.Wellknowns.Reps)
        Dim oContracts As List(Of DTOContract) = Await FEB2.Contracts.All(exs, Current.Session.User, oCodi, _Contact)
        If exs.Count = 0 Then
            Xl_Contracts1.Load(oContracts, Nothing, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class