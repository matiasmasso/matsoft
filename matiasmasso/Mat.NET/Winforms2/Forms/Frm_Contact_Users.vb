Public Class Frm_Contact_Users
    Private _Contact As DTOContact
    Private _DefaultValue As DTOUser
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oContact As DTOContact, Optional oDefaultValue As DTOUser = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Users_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Users1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Users1.ItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Users1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Users1.RequestToAddNew
        Dim oUser As New DTOUser
        Dim oFrm As New Frm_User(oUser)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Users1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Users1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oContact = Await FEB.Contact.Find(_Contact.Guid, exs)
        If exs.Count = 0 Then
            ProgressBar1.Visible = False
            Xl_Users1.Load(oContact.Emails, _DefaultValue, DTO.Defaults.SelectionModes.selection)
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function


End Class