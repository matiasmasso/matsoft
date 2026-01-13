Imports Winforms
Public Class Xl_CreateContact_StepRep
    Implements IWizardStep

    Private _Rep As DTORep
    Private _AllowEvents As Boolean

    Public Property WizardStep As Integer Implements IWizardStep.WizardStep

    Public ReadOnly Property RaoSocial As String
        Get
            Return TextBoxRaoSocial.Text
        End Get
    End Property

    Public ReadOnly Property Nickname As String
        Get
            Return TextBoxNickname.Text
        End Get
    End Property

    Public ReadOnly Property Nif As DTONif
        Get
            Return Xl_LookupNif1.Nif
        End Get
    End Property

    Public ReadOnly Property Address As DTOAddress
        Get
            Return Xl_LookupAddress1.Address
        End Get
    End Property

    Public ReadOnly Property FchAlta As Date
        Get
            Return DateTimePickerAlta.Value
        End Get
    End Property

    Public ReadOnly Property ComStd As Decimal
        Get
            Return Xl_PercentComStd.Value
        End Get
    End Property

    Public ReadOnly Property ComRed As Decimal
        Get
            Return Xl_PercentComRed.Value
        End Get
    End Property

    Public ReadOnly Property RepProducts As List(Of DTORepProduct)
        Get
            Return Xl_RepProductsTree1.Values
        End Get
    End Property

    Public Sub New(iWizardStep As Integer, oRep As DTORep)
        MyBase.New
        InitializeComponent()
        _WizardStep = iWizardStep
        _Rep = oRep
        With _Rep
            TextBoxRaoSocial.Text = .Nom
            TextBoxNickname.Text = .NickName
            Xl_LookupNif1.Nif = .Nifs.PrimaryNif
            Xl_LookupAddress1.Load(.Address)
            If .FchAlta >= DateTimePickerAlta.MinDate Then
                DateTimePickerAlta.Value = .FchAlta
            End If
            Xl_PercentComStd.Value = .ComisionStandard
            Xl_PercentComRed.Value = .ComisionReducida
            Xl_RepProductsTree1.Load(.RepProducts, allowPersist:=False)
        End With
        _AllowEvents = True
    End Sub

    Private Sub Xl_RepProductsTree1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepProductsTree1.RequestToAddNew
        Dim oRepProduct As New DTORepProduct()
        With oRepProduct
            .Rep = _Rep
            .Rep.Nom = TextBoxRaoSocial.Text
            .Rep.NickName = TextBoxNickname.Text
            .ComStd = Xl_PercentComStd.Value
            .ComRed = Xl_PercentComRed.Value
            .FchFrom = DateTimePickerAlta.Value
            .Cod = DTORepProduct.Cods.Included
        End With
        Dim values As New List(Of DTORepProduct)
        values.Add(oRepProduct)
        Dim oFrm As New Frm_RepProduct(values, AllowPersist:=False)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaRepProducts
        oFrm.Show()
    End Sub

    Private Sub refrescaRepProducts(sender As Object, e As MatEventArgs)
        Dim items As List(Of DTORepProduct) = Xl_RepProductsTree1.Values
        If items Is Nothing Then items = New List(Of DTORepProduct)
        Dim itemsToAdd As List(Of DTORepProduct) = e.Argument
        items.AddRange(itemsToAdd)
        Xl_RepProductsTree1.Load(items, allowPersist:=False)
    End Sub
End Class
