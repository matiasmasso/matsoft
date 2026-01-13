

Public Class Xl_Contact_Banc
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents Xl_IBAN1 As Xl_Iban
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxAbr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxSepaCoreIdentificador As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_AmountClassificacio As Xl_Amount
    Friend WithEvents Xl_AmountComisioGestioCobrBase As Xl_Amount
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxNormaRMECedent As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxConditionsUnpayments As TextBox
    Friend WithEvents TextBoxConditionsTransfers As TextBox
    Friend WithEvents Label8 As Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxAbr = New System.Windows.Forms.TextBox()
        Me.TextBoxSepaCoreIdentificador = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_IBAN1 = New Mat.Net.Xl_Iban()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_AmountClassificacio = New Mat.Net.Xl_Amount()
        Me.Xl_AmountComisioGestioCobrBase = New Mat.Net.Xl_Amount()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNormaRMECedent = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxConditionsUnpayments = New System.Windows.Forms.TextBox()
        Me.TextBoxConditionsTransfers = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Abreviatura:"
        '
        'TextBoxAbr
        '
        Me.TextBoxAbr.Location = New System.Drawing.Point(203, 11)
        Me.TextBoxAbr.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxAbr.MaxLength = 10
        Me.TextBoxAbr.Name = "TextBoxAbr"
        Me.TextBoxAbr.Size = New System.Drawing.Size(126, 20)
        Me.TextBoxAbr.TabIndex = 1
        '
        'TextBoxSepaCoreIdentificador
        '
        Me.TextBoxSepaCoreIdentificador.Location = New System.Drawing.Point(203, 35)
        Me.TextBoxSepaCoreIdentificador.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxSepaCoreIdentificador.MaxLength = 16
        Me.TextBoxSepaCoreIdentificador.Name = "TextBoxSepaCoreIdentificador"
        Me.TextBoxSepaCoreIdentificador.Size = New System.Drawing.Size(126, 20)
        Me.TextBoxSepaCoreIdentificador.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Identificador Sepa Core:"
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.Location = New System.Drawing.Point(20, 177)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Classificació:"
        '
        'Xl_AmountClassificacio
        '
        Me.Xl_AmountClassificacio.Amt = Nothing
        Me.Xl_AmountClassificacio.Location = New System.Drawing.Point(203, 85)
        Me.Xl_AmountClassificacio.Name = "Xl_AmountClassificacio"
        Me.Xl_AmountClassificacio.ReadOnly = False
        Me.Xl_AmountClassificacio.Size = New System.Drawing.Size(126, 20)
        Me.Xl_AmountClassificacio.TabIndex = 17
        '
        'Xl_AmountComisioGestioCobrBase
        '
        Me.Xl_AmountComisioGestioCobrBase.Amt = Nothing
        Me.Xl_AmountComisioGestioCobrBase.Location = New System.Drawing.Point(203, 111)
        Me.Xl_AmountComisioGestioCobrBase.Name = "Xl_AmountComisioGestioCobrBase"
        Me.Xl_AmountComisioGestioCobrBase.ReadOnly = False
        Me.Xl_AmountComisioGestioCobrBase.Size = New System.Drawing.Size(126, 20)
        Me.Xl_AmountComisioGestioCobrBase.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(137, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Comisio Gestio Cobro Base:"
        '
        'TextBoxNormaRMECedent
        '
        Me.TextBoxNormaRMECedent.Location = New System.Drawing.Point(203, 59)
        Me.TextBoxNormaRMECedent.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxNormaRMECedent.MaxLength = 16
        Me.TextBoxNormaRMECedent.Name = "TextBoxNormaRMECedent"
        Me.TextBoxNormaRMECedent.Size = New System.Drawing.Size(126, 20)
        Me.TextBoxNormaRMECedent.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(103, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Cedent norma RME:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 241)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Condicions impagaments:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 324)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Condicions transferencies:"
        '
        'TextBoxConditionsUnpayments
        '
        Me.TextBoxConditionsUnpayments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxConditionsUnpayments.Location = New System.Drawing.Point(20, 258)
        Me.TextBoxConditionsUnpayments.Multiline = True
        Me.TextBoxConditionsUnpayments.Name = "TextBoxConditionsUnpayments"
        Me.TextBoxConditionsUnpayments.Size = New System.Drawing.Size(309, 54)
        Me.TextBoxConditionsUnpayments.TabIndex = 26
        '
        'TextBoxConditionsTransfers
        '
        Me.TextBoxConditionsTransfers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxConditionsTransfers.Location = New System.Drawing.Point(20, 340)
        Me.TextBoxConditionsTransfers.Multiline = True
        Me.TextBoxConditionsTransfers.Name = "TextBoxConditionsTransfers"
        Me.TextBoxConditionsTransfers.Size = New System.Drawing.Size(309, 54)
        Me.TextBoxConditionsTransfers.TabIndex = 27
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 161)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Iban:"
        '
        'Xl_Contact_Banc
        '
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxConditionsTransfers)
        Me.Controls.Add(Me.TextBoxConditionsUnpayments)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxNormaRMECedent)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_AmountComisioGestioCobrBase)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_AmountClassificacio)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxSepaCoreIdentificador)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxAbr)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Xl_IBAN1)
        Me.Name = "Xl_Contact_Banc"
        Me.Size = New System.Drawing.Size(342, 417)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Private mAllowEvents As Boolean
    Private _Banc As DTOBanc

    Public Shadows Async Sub Load(oBanc As DTOBanc)
        _Banc = oBanc
        With _Banc
            TextBoxAbr.Text = .Abr
            TextBoxNormaRMECedent.Text = .NormaRMECedent
            TextBoxSepaCoreIdentificador.Text = .SepaCoreIdentificador
            Xl_AmountClassificacio.Amt = .Classificacio
            Xl_AmountComisioGestioCobrBase.Amt = DTOAmt.Factory(.ComisioGestioCobr)
            TextBoxConditionsUnpayments.Text = .ConditionsUnpayments
            TextBoxConditionsTransfers.Text = .ConditionsTransfers
            Await Xl_IBAN1.Load(.Iban, _Banc, DTOIban.Cods.banc)
        End With
        mAllowEvents = True
    End Sub

    Public ReadOnly Property Banc As DTOBanc
        Get
            With _Banc
                .Abr = TextBoxAbr.Text
                .SepaCoreIdentificador = TextBoxSepaCoreIdentificador.Text
                .NormaRMECedent = TextBoxNormaRMECedent.Text
                .Classificacio = Xl_AmountClassificacio.Amt
                .ComisioGestioCobr = Xl_AmountComisioGestioCobrBase.Amt.Eur
                .ConditionsUnpayments = TextBoxConditionsUnpayments.Text
                .ConditionsTransfers = TextBoxConditionsTransfers.Text
                .Iban = Xl_IBAN1.Value
            End With
            Return _Banc
        End Get
    End Property


    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxAbr.TextChanged,
        TextBoxSepaCoreIdentificador.TextChanged,
        TextBoxNormaRMECedent.TextChanged,
        Xl_AmountClassificacio.AfterUpdate,
        Xl_AmountComisioGestioCobrBase.AfterUpdate,
         TextBoxConditionsUnpayments.TextChanged,
          TextBoxConditionsTransfers.TextChanged

        If mAllowEvents Then
            RaiseEvent AfterUpdate(Me, Nothing)
        End If
    End Sub

    Private Sub Xl_IBAN1_RequestToChange(sender As Object, e As MatEventArgs) Handles Xl_IBAN1.RequestToChange
        Dim oFrm As New Frm_Contact_Ibans(_Banc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub

    Private Async Sub RefreshIban(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oIban = Await FEB.Iban.FromContact(exs, _Banc, DTOIban.Cods.banc)
        If exs.Count = 0 Then
            Await Xl_IBAN1.Load(oIban)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_IBAN1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_IBAN1.RequestToAddNew
        Dim oIban As New DTOIban
        With oIban
            .Cod = DTOIban.Cods.banc
            .FchApproved = DTO.GlobalVariables.Today()
            .Titular = _Banc
        End With
        Dim oFrm As New Frm_IbanCcc(oIban)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshIban
        oFrm.Show()
    End Sub

End Class
