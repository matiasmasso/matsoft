Public Class Frm_Contacts
    Private _Channels As List(Of DTODistributionChannel)
    Private _DefaultValue As DTOContact
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOContact = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Contacts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Contacts1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Contacts1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.RequestToAddNew
        Dim oContact As New DTOContact
        Dim oFrm As New Frm_Contact(oContact)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Contacts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Contacts1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Channels = Await FEB.DistributionChannels.AllWithContacts(Current.Session.Emp, exs)
        If exs.Count = 0 Then
            Xl_DistributionChannels1.Load(_Channels)

            Dim oChannel As DTODistributionChannel = CurrentChannel()
            Dim oClasses As List(Of DTOContactClass) = oChannel.ContactClasses
            Xl_ContactClasses1.Load(oClasses)

            Dim oClass As DTOContactClass = CurrentClass()
            If oClass IsNot Nothing Then
                Dim oContacts As List(Of DTOContact) = oClass.Contacts
                Await Xl_Contacts1.Load(CurrentClass.Contacts)
            End If
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function CurrentChannel() As DTODistributionChannel
        Dim retval As DTODistributionChannel = Xl_DistributionChannels1.Value
        Return retval
    End Function

    Private Function CurrentClass() As DTOContactClass
        Dim retval As DTOContactClass = Xl_ContactClasses1.Value
        Return retval
    End Function

    Private Async Sub Xl_ContactClasses1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ContactClasses1.ValueChanged
        Await Xl_Contacts1.Load(CurrentClass.Contacts)
    End Sub

    Private Sub Xl_ContactClasses1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ContactClasses1.RequestToAddNew
        Dim oClass As New DTOContactClass
        Dim oFrm As New Frm_ContactClass(oClass)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ContactClasses1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ContactClasses1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Await Xl_Contacts1.SetFilter(e.Argument)
    End Sub

    Private Async Sub Xl_DistributionChannels1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_DistributionChannels1.ValueChanged
        Dim oChannel As DTODistributionChannel = CurrentChannel()
        Dim oClasses As List(Of DTOContactClass) = oChannel.ContactClasses
        Xl_ContactClasses1.Load(oClasses)

        Dim oClass As DTOContactClass = CurrentClass()
        If oClass IsNot Nothing Then
            Dim oContacts As List(Of DTOContact) = oClass.Contacts
            Await Xl_Contacts1.Load(CurrentClass.Contacts)
        End If
    End Sub

    Private Sub Xl_DistributionChannels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DistributionChannels1.RequestToAddNew
        Dim oChannel As New DTODistributionChannel
        Dim oFrm As New Frm_DistributionChannel(oChannel)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class