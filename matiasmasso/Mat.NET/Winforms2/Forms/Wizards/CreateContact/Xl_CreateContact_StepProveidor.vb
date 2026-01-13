Public Class Xl_CreateContact_StepProveidor
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

    ReadOnly Property Incoterm As DTOIncoterm
        Get
            Return Xl_LookupIncoterm1.Value
        End Get
    End Property

    Public Sub New(iWizardStep As Integer, oContact As DTOProveidor, oIncoterms As List(Of DTOIncoterm))
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        With oContact
            TextBoxRaoSocial.Text = .Nom
            TextBoxNomComercial.Text = .NomComercial
            Xl_LookupNif1.Load(.Nifs.PrimaryNif)
            Xl_LookupAddress1.Load(.Address)
            If .IncoTerm Is Nothing Then .IncoTerm = DTOIncoterm.Factory("DAP")
            Xl_LookupIncoterm1.Load(oIncoterms, .IncoTerm)
        End With
        _AllowEvents = True
    End Sub


End Class


