Imports Winforms

Public Class Xl_CreateContact_StepCustomer
    Implements IWizardStep

    Private _AllowEvents As Boolean
    Private _ChangedIva As Boolean

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

    ReadOnly Property Incoterm As DTOIncoterm
        Get
            Return Xl_LookupIncoterm1.Value
        End Get
    End Property

    ReadOnly Property Nifs As DTONif.Collection
        Get
            Dim retval As New DTONif.Collection
            If Xl_LookupNif1.Nif IsNot Nothing Then retval.Add(Xl_LookupNif1.Nif)
            If Xl_LookupNif2.Nif IsNot Nothing Then retval.Add(Xl_LookupNif2.Nif)
            Return retval
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

    ReadOnly Property IVA As Boolean
        Get
            Return CheckBoxIva.Checked
        End Get
    End Property

    ReadOnly Property Req As Boolean
        Get
            Dim retval As Boolean = (CheckBoxIva.Checked And CheckBoxReq.Checked)
            Return retval
        End Get
    End Property


    Public Sub New(iWizardStep As Integer, oContact As DTOContact, oIncoterms As List(Of DTOIncoterm))
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        With DirectCast(oContact, DTOCustomer)
            TextBoxRaoSocial.Text = .Nom
            TextBoxNomComercial.Text = .NomComercial
            Xl_LookupAddress1.Load(.Address)
            EnableNifs()
            If .Incoterm Is Nothing Then .Incoterm = DTOIncoterm.Factory("DAP")
            Xl_LookupIncoterm1.Load(oIncoterms, .Incoterm)
            ' TextBoxNif.Text = .Nif
            'TextBoxNif2.Text = .Nif2
            CheckBoxIva.Checked = .Iva
            CheckBoxReq.Checked = .Req
        End With

        _AllowEvents = True
    End Sub

    Private Sub EnableNifs()
        Dim oAddress As DTOAddress = Xl_LookupAddress1.Address
        Dim oCountry As DTOCountry = DTOAddress.Country(oAddress)
        If oCountry Is Nothing Then
            LabelNif.Text = "NIF"
            LabelNif2.Visible = False
            Xl_LookupNif2.Visible = False
        Else
            If DTOAddress.Country(oAddress).Equals(DTOCountry.Wellknown(DTOCountry.Wellknowns.Andorra)) Then
                LabelNif.Text = "Reg.Tributari (NRT)"
                LabelNif2.Text = "Numero de comerç"
                LabelNif2.Visible = True
                Xl_LookupNif2.Visible = True
            Else
                LabelNif.Text = "NIF"
                LabelNif2.Visible = False
                Xl_LookupNif2.Visible = False
            End If

        End If
    End Sub

    Private Sub Xl_LookupAddress1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupAddress1.AfterUpdate
        _AllowEvents = False
        EnableNifs()
        SuggestIVA()
        SuggestReq()
        _AllowEvents = True
    End Sub

    Private Sub SuggestIVA()
        Dim oAddress As DTOAddress = Xl_LookupAddress1.Address
        CheckBoxIva.Checked = (DTOAddress.ExportCod(oAddress) = DTOInvoice.ExportCods.Nacional)
        CheckBoxReq.Visible = CheckBoxIva.Checked

    End Sub

    Private Sub SuggestReq()
        If CheckBoxIva.Checked Then
            Dim oAddress As DTOAddress = Xl_LookupAddress1.Address
            Dim oNif = Xl_LookupNif1.Nif
            If oNif IsNot Nothing Then
                Dim oFormaJuridica = DTOContact.formaJuridica(oAddress, oNif.Value)
                CheckBoxReq.Checked = (oFormaJuridica = DTOContact.FormasJuridicas.PersonaFisica)
            End If
        End If
    End Sub

    Private Sub CheckBoxIva_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIva.CheckedChanged
        CheckBoxReq.Visible = CheckBoxIva.Checked
        If _AllowEvents Then
            _ChangedIva = True
            If DTOAddress.Country(Xl_LookupAddress1.Address) IsNot Nothing Then
                SuggestReq()
            End If
        End If
    End Sub
End Class
