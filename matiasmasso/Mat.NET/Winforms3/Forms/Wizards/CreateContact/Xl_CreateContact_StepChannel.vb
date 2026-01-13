

Public Class Xl_CreateContact_StepChannel
    Implements IWizardStep

    Private _Values As List(Of DTOContactClass)
    Private _DefaultValue As DTOContactClass
    Private _AllowEvents As Boolean
    Property WizardStep As Integer Implements IWizardStep.WizardStep
    Public ReadOnly Property ContactClass As DTOContactClass
        Get
            Return Xl_ContactClasses1.Value
        End Get
    End Property

    Public Event requestForNextStep(sender As Object, e As MatEventArgs)

    Public Sub New(iWizardStep As Integer, oContact As DTOContact, values As List(Of DTOContactClass))
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        _Values = values
        _DefaultValue = If(oContact Is Nothing, Nothing, oContact.ContactClass)
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Xl_ContactClasses1.Load(FilteredValues, _DefaultValue, DTO.Defaults.SelectionModes.Selection)
    End Sub

    Private Function FilteredValues() As List(Of DTOContactClass)
        Dim retval As List(Of DTOContactClass) = Nothing
        Dim oDiversos As DTODistributionChannel = DTODistributionChannel.Wellknown(DTODistributionChannel.Wellknowns.Diversos)
        If RadioButtonClient.Checked Then
            retval = _Values.Where(Function(x) x.DistributionChannel.UnEquals(oDiversos)).ToList
        Else
            retval = _Values.Where(Function(x) x.DistributionChannel.Equals(oDiversos)).ToList
        End If
        Return retval
    End Function

    Private Sub RadioButtonClient_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonClient.CheckedChanged
        If _AllowEvents Then refresca()
    End Sub

    Private Sub Xl_ContactClasses1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ContactClasses1.onItemSelected
        RaiseEvent requestForNextStep(Me, New MatEventArgs(Me))
    End Sub

    Private Sub Xl_ContactClasses1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ContactClasses1.RequestToAddNew
        Dim oContactClass As New DTOContactClass
        Dim oFrm As New Frm_ContactClass(oContactClass)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ContactClasses1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ContactClasses1.RequestToRefresh
        Dim exs As New List(Of Exception)
        _Values = Await FEB.ContactClasses.All(exs)
        If exs.Count = 0 Then
            refresca()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class
