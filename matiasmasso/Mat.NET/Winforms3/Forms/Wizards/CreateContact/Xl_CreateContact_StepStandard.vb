Public Class Xl_CreateContact_StepStandard
    Implements IWizardStep

    Private _AllowEvents As Boolean
    Public Property WizardStep As Integer Implements IWizardStep.WizardStep
    ReadOnly Property RaoSocial As String
        Get
            Return TextBoxRaoSocial.Text
        End Get
    End Property

    ReadOnly Property NomComercial As String
        Get
            Return TextBoxNomComercial.Text
        End Get
    End Property

    ReadOnly Property Nif As DTONif
        Get
            Return Xl_LookupNif1.Nif
        End Get
    End Property

    ReadOnly Property Address As DTOAddress
        Get
            Return Xl_LookupAddress1.Address
        End Get
    End Property

    Public Sub New(iWizardStep As Integer, oContact As DTOContact)
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        With oContact
            TextBoxRaoSocial.Text = .Nom
            TextBoxNomComercial.Text = .NomComercial
            Xl_LookupNif1.Load(.Nifs.PrimaryNif)
            Xl_LookupAddress1.Load(.Address)
        End With
        _AllowEvents = True
    End Sub


End Class

