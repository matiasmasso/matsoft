Public Class Xl_CreateContact_StepStaff
    Implements IWizardStep

    Private _AllowEvents As Boolean
    Public Property WizardStep As Integer Implements IWizardStep.WizardStep
    ReadOnly Property RaoSocial As String
        Get
            Return TextBoxRaoSocial.Text
        End Get
    End Property
    ReadOnly Property Address As DTOAddress
        Get
            Return Xl_LookupAddress1.Address
        End Get
    End Property

    ReadOnly Property Nickname As String
        Get
            Return TextBoxNickname.Text
        End Get
    End Property

    ReadOnly Property Nif As DTONif
        Get
            Return Xl_LookupNif1.Nif
        End Get
    End Property

    ReadOnly Property NumSegSocial As String
        Get
            Return Xl_NumSegSocial1.Value
        End Get
    End Property

    ReadOnly Property Sex As DTOUser.Sexs
        Get
            Return Xl_Sex1.Sex
        End Get
    End Property

    ReadOnly Property Birth As Date
        Get
            Return DateTimePickerBirth.Value
        End Get
    End Property

    ReadOnly Property Alta As Date
        Get
            Return DateTimePickerAlta.Value
        End Get
    End Property

    ReadOnly Property StaffPos As DTOStaffPos
        Get
            Return Xl_LookupStaffPos1.StaffPos
        End Get
    End Property

    ReadOnly Property Iban As DTOIban
        Get
            Return Xl_IbanCompact1.Iban
        End Get
    End Property

    Public Sub New(iWizardStep As Integer, oStaff As DTOStaff)
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        With oStaff
            TextBoxRaoSocial.Text = .Nom
            Xl_LookupAddress1.Load(.Address)
            TextBoxNickname.Text = .Abr
            Xl_Sex1.Sex = .Sex
            Xl_LookupNif1.Load(.Nifs.PrimaryNif)
            Xl_NumSegSocial1.Load(.NumSs)
            If .Birth <> Nothing Then
                DateTimePickerBirth.Value = .Birth
            End If
            If .Alta <> Nothing Then
                DateTimePickerAlta.Value = .Alta
            End If
            'Xl_Laboral_Categoria1.LaboralCategoria = .LaboralCategoria
            Xl_LookupStaffPos1.StaffPos = .StaffPos
            Xl_IbanCompact1.Load(.Iban)
        End With
        _AllowEvents = True
    End Sub



End Class
