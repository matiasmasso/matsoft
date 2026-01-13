Public Class Xl_CreateContact_StepFinish

    Implements IWizardStep

    Property WizardStep As Integer Implements IWizardStep.WizardStep

    Public Sub New(iWizardStep As Integer, oContact As DTOContact)
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        TextBoxResum.Text = DTOContact.Resum(oContact)
    End Sub

End Class
